using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class SalesRep
    {
        private readonly MongoHelper<SalesRep> _user;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyId { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public SalesRep() { _user = new MongoHelper<SalesRep>(); }

        public string Create(SalesRep user)
        {
            user.CreatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));
            string msg = "SalesRep Already Exist";

            if (!IsUserExist(email:user.Email, check:true, companyId: user.CompanyId))
            {
                var result = _user.Collection.Save(user);

                if (result.DocumentsAffected > 0 || result.Ok)
                {
                    msg = "SalesRep Created Successfully";
                }
                else
                {
                    msg = "Some Error Occured";
                }
            }
            else
            {
                msg = "Some Error Occured";
            }

            return msg;
        }

        public bool IsUserExist(string email, bool check, string companyId = null)
        {
            var query = Query.And(Query<SalesRep>.EQ(x => x.Email, email),
                Query<SalesRep>.EQ(x => x.CompanyId, companyId));

            var result = _user.Collection.FindOne(query);

            if (result != null)
            {
                return true;
            }
            else
            {
                if (check)
                    return new Company().IsUserExist(email, false);
            }

            return false;
        }

        public bool Update(SalesRep user)
        {
            bool isUpdated = false;

            SalesRep oldUser = GetUserById(user.UserId);

            var query = Query<SalesRep>.EQ(x => x.UserId, user.UserId);

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = oldUser.Password;
            }

            SalesRep newUser = new SalesRep
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                PhoneNo = user.PhoneNo,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = oldUser.CreatedDate,
                UpdatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm tt"))
            };

            var replacement = Update<SalesRep>.Replace(newUser);

            var result = _user.Collection.Update(query, replacement);

            if(result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }
            return isUpdated;
        }

        public SalesRep GetUserById(string userId)
        {
            var query = Query<SalesRep>.EQ(x => x.UserId, userId);

            return _user.Collection.FindOne(query);
        }

        public List<SalesRep> GetAll(string companyId)
        {
            var query = Query<SalesRep>.EQ(x => x.CompanyId, companyId);

            return _user.Collection.Find(query).ToList();
        }

        public bool DeleteSalesRep(string repId)
        {
            bool isDeleted = false;

            var query = Query<SalesRep>.EQ(x => x.UserId, repId);

            var result = _user.Collection.Remove(query);

            if(result.DocumentsAffected > 0)
            {
                isDeleted = true;
            }

            return isDeleted;
        }

        public SalesRep GetSalesRepInfo(string repId)
        {
            SalesRep rep = new SalesRep();

            var query = Query<SalesRep>.EQ(x => x.UserId, repId);

            return _user.Collection.FindOne(query);
        }

        public bool UpdateSalesRep(SalesRep rep)
        {
            bool isUpdated = false;

            var query = Query<SalesRep>.EQ(x => x.UserId, rep.UserId);
            SalesRep oldRep = GetSalesRepInfo(rep.UserId);

            if (string.IsNullOrWhiteSpace(rep.Password))
            {
                rep.Password = oldRep.Password;
            }
            rep.CreatedDate = oldRep.CreatedDate;
            rep.UpdatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));

            var replacement = Update<SalesRep>.Replace(rep);
            var result = _user.Collection.Update(query, replacement);

            if(result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }

            return isUpdated;
        }

        public SalesRep validateLogin(Login rep)
        {
            var query = Query.And(Query<SalesRep>.EQ(x => x.Email, rep.Email),
                Query<SalesRep>.EQ(x => x.Password, rep.Password));

            return _user.Collection.FindOne(query);
        }
    }
}