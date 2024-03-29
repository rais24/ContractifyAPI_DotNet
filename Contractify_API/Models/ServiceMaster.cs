﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class ServiceMaster
    {
        private readonly MongoHelper<ServiceMaster> _service;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ServiceId { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ParentId { get; set; }
        public string ShortDescription { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ServiceMaster()
        {
            _service = new MongoHelper<ServiceMaster>();
        }

        public ServiceMaster(string ServiceId = null,
           string CompanyId = null,
           string Name = null,
           string Price = "0",
           string ParentId = "0",
           string ShortDescription = null,
           string Notes = null)
        {
            this.ServiceId = ServiceId;
            this.CompanyId = CompanyId;
            this.Name = Name;
            this.Price = Price;
            this.ParentId = ParentId;
            this.ShortDescription = ShortDescription;
            this.Notes = Notes;
        }

        public string CreateService(ServiceMaster service)
        {
            try
            {
                service.CreatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));
                if (!IsServiceExist(service.CompanyId,service.Name))
                {
                    _service.Collection.Save(service);
                    return "Service Created";
                }
                else
                {
                    return "Service Already Exists";
                }
            }
            catch
            {
                return "Some Error Encountered";
            }
        }

        public bool IsServiceExist(string companyId,string serviceName)
        {
            var query = Query.And(Query<ServiceMaster>.EQ(x => x.Name, serviceName),
                Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));
            var service = _service.Collection.FindOne(query);

            if (service != null)
            {
                return true;
            }

            return false;
        }

        public List<ServiceMaster> GetAllMasterService(string companyId)
        {
            var query = Query.And(Query<ServiceMaster>.EQ(x => x.CompanyId, companyId),
                                  Query<ServiceMaster>.EQ(x => x.ParentId,"0"));
            return _service.Collection.Find(query).ToList();
        }

        public List<ServiceMaster> GetSubServices(string serviceId)
        {
            var query = Query<ServiceMaster>.EQ(x => x.ParentId, serviceId);
            return _service.Collection.Find(query).ToList();
        }

        public string CreateSubService(ServiceMaster service)
        {
            if (!IsSubServiceExist(service.Name, service.ParentId))
            {
                _service.Collection.Save(service);
                return "Service Created";
            }
            else
            {
                return "Service Already Exists";
            }

        }

        public bool IsSubServiceExist(string serviceName,string parentId)
        {
            var query = Query.And(
                Query<ServiceMaster>.EQ(x => x.Name, serviceName),
                Query<ServiceMaster>.EQ(x => x.ParentId, parentId));
            var service = _service.Collection.FindOne(query);

            if (service != null)
            {
                return true;
            }

            return false;
        }

        public List<ServiceMaster> GetAllService(string companyId)
        {
            var query = Query<ServiceMaster>.EQ(x => x.CompanyId, companyId);
            return _service.Collection.Find(query).ToList();
        }

        public bool UpdateService(ServiceMaster service)
        {
            bool isUpdated = false;
            service.UpdatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));

            var query = Query.And(Query<ServiceMaster>.EQ(x => x.CompanyId, service.CompanyId),
                                  Query<ServiceMaster>.EQ(x => x.ServiceId, service.ServiceId),
                                  Query<ServiceMaster>.EQ(x => x.ParentId, service.ParentId));

            var replacement = Update<ServiceMaster>.Replace(service);
            var result = _service.Collection.Update(query, replacement);

            if (result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }

            return isUpdated;

        }

        public bool DeleteSubSubService(string companyId,string serviceId)
        {
            bool isDeleted = false;
            var query = Query.And(Query<ServiceMaster>.EQ(x => x.ServiceId, serviceId),
                                  Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));

            var result = _service.Collection.Remove(query);

            if (result.DocumentsAffected > 0)
            {
                isDeleted = true;
            }
            return isDeleted;

        }

        public bool DeleteSubService(string companyId, string serviceId)
        {
            bool isDeleted = false;

            // query for sub sub services of related sub service
            var ssubQuery = Query.And(Query<ServiceMaster>.EQ(x => x.ParentId, serviceId),
                                      Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));

            // query for sub service
            var query = Query.And(Query<ServiceMaster>.EQ(x => x.ServiceId, serviceId),
                                  Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));

            _service.Collection.Remove(ssubQuery);

            var result = _service.Collection.Remove(query);
            if (result.DocumentsAffected > 0)
            {
                isDeleted = true;
            }


            return isDeleted;
        }

        public bool DeleteMasterService(string companyId,string serviceId)
        {
            bool isDeleted = false;

            // query for sub service list from master service id
            var subQuery = Query.And(Query<ServiceMaster>.EQ(x => x.ParentId, serviceId),
                                     Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));

            List<ServiceMaster> subServiceList = _service.Collection.Find(subQuery).ToList();

            foreach(ServiceMaster service in subServiceList)
            {
                // query for sub sub service from sub service id
                var ssubQuery = Query.And(Query<ServiceMaster>.EQ(x => x.ParentId, service.ServiceId),
                                          Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));

                // query for sub service id on service id
                var sQuery = Query.And(Query<ServiceMaster>.EQ(x => x.ServiceId, service.ServiceId),
                                          Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));

                _service.Collection.Remove(ssubQuery);
                _service.Collection.Remove(sQuery);
            }

            // query for master service on service id
            var query = Query.And(Query<ServiceMaster>.EQ(x => x.ServiceId, serviceId),
                                     Query<ServiceMaster>.EQ(x => x.CompanyId, companyId));

            var result = _service.Collection.Remove(query);

            if(result.DocumentsAffected > 0)
            {
                isDeleted = true;
            }

            return isDeleted;
        }

    }
}