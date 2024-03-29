﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class Contract
    {
        private readonly MongoHelper<Contract> _contract;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContractId { get; set; }
        public string CompanyId { get; set; }
        public string ContractStatus { get; set; } // proposal,agreement,contract,closed
        public string ClientId { get; set; }
        public string SalesRepId { get; set; }
        public string SalesRepName { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContractPeriod { get; set; }
        public string ContractType { get; set; } // digital marketing, technical, both
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string ContractDescription { get; set; }
        public string ContractPdfPath { get; set; }
        public string Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ServiceMaster> ContractScope { get; set; }
        public List<Terms> ContractTerms { get; set; }

        public Contract()
        {
            _contract = new MongoHelper<Contract>();
        }

        public bool AddContract(Contract contract)
        {
            bool isAdded = false;
            Client client = new Client().GetClientById(contract.ClientId);

            if (!string.IsNullOrEmpty(contract.ContactPerson))
            {
                string[] cname = contract.ContactPerson.Split(null);

                if (cname.Count() > 1)
                {
                    contract.ContactEmail = client.ContactPersons.Where(x => (x.FirstName.Contains(cname[0]) && x.LastName.Contains(cname[1]))).Select(x => x.Email).FirstOrDefault();

                    if (string.IsNullOrEmpty(contract.ContactEmail))
                    {
                        if (client.FirstName.Contains(cname[0]) && client.LastName.Contains(cname[1]))
                            contract.ContactEmail = client.Email;
                    }
                }
                else if(cname.Count() == 1)
                {
                    contract.ContactEmail = client.ContactPersons.Where(x => x.FirstName.Contains(cname[0])).Select(x => x.Email).FirstOrDefault();

                    if (string.IsNullOrEmpty(contract.ContactEmail))
                    {
                        if (client.FirstName.Contains(cname[0]))
                            contract.ContactEmail = client.Email;
                    }
                }
            }
           
            contract.CompanyName = client.CompanyName;
           
            contract.CreatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));
            var result = _contract.Collection.Save(contract);
            string affected = result.LastErrorMessage;
            if (result.DocumentsAffected > 0 || result.Ok)
            {
                ContractHistory history = new ContractHistory
                {
                    CompanyId = contract.CompanyId,
                    Contract = contract,
                    Action = "Created",
                    CreatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"))
                };
                history.Save(history);
                isAdded = true;
            }
                

            return isAdded;
        }

        public bool UpdateContract(Contract contract)
        {
            bool isUpdated = false;
            Client client = new Client().GetClientById(contract.ClientId);

            if (!string.IsNullOrEmpty(contract.ContactPerson))
            {
                string[] cname = contract.ContactPerson.Split(null);

                if (cname.Count() > 1)
                {
                    contract.ContactEmail = client.ContactPersons.Where(x => (x.FirstName.Contains(cname[0]) && x.LastName.Contains(cname[1]))).Select(x => x.Email).FirstOrDefault();
                }
                else if (cname.Count() == 1)
                {
                    contract.ContactEmail = client.ContactPersons.Where(x => x.FirstName.Contains(cname[0])).Select(x => x.Email).FirstOrDefault();
                }
            }
            Contract oContract = GetContractById(contract.ContractId);
            contract.CreatedDate = oContract.CreatedDate;
            contract.UpdatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));
            var query = Query<Contract>.EQ(x => x.ContractId, contract.ContractId);
            var replacement = Update<Contract>.Replace(contract);

            var result = _contract.Collection.Update(query, replacement);

            if (result.DocumentsAffected > 0)
            {
                ContractHistory history = new ContractHistory
                {
                    CompanyId = contract.CompanyId,
                    Contract = contract,
                    Action = "Updated",
                    CreatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"))
                };
                history.Save(history);
                isUpdated = true;
            }
               

            return isUpdated;
        }

        public bool DeleteContract(string contractId)
        {
            bool isDeleted = false;
            Contract contract = GetContractById(contractId);
            var query = Query<Contract>.EQ(x => x.ContractId, contractId);

            var result = _contract.Collection.Remove(query);

            if (result.DocumentsAffected > 0)
            {
                ContractHistory history = new ContractHistory
                {
                    CompanyId = contract.CompanyId,
                    Contract = contract,
                    Action = "Deleted",
                    CreatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"))
                };
                history.Save(history);
                isDeleted = true;
            }
               

            return isDeleted;
        }

        public List<Contract> GetAllContracts(string memberId)
        {
            List<Contract> contracts = new List<Contract>();

            var query = Query<Contract>.EQ(x => x.CompanyId, memberId);

            return _contract.Collection.Find(query).ToList();
        }

        public Contract GetContractById(string contractId)
        {
            var query = Query<Contract>.EQ(x => x.ContractId, contractId);

            return _contract.Collection.FindOne(query);
        }

        public bool CloseContract(string contractId)
        {
            bool isClosed = false;

            var query = Query<Contract>.EQ(x => x.ContractId, contractId);

            var updateQuery = Update<Contract>.Combine(Update<Contract>.Set(x => x.ContractStatus, "Closed"),
                Update<Contract>.Set(x => x.UpdatedDate, Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"))));

            var result = _contract.Collection.Update(query, updateQuery);

            if(result.DocumentsAffected > 0)
            {
                Contract contract = GetContractById(ContractId);
                ContractHistory history = new ContractHistory
                {
                    CompanyId = contract.CompanyId,
                    Contract = contract,
                    Action = "Updated",
                    CreatedDate = Convert.ToDateTime(DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"))
                };
                history.Save(history);
                isClosed = true;
            }

            return isClosed;
        }

        public bool UpdateContractPdf(Contract contract)
        {
            bool isUpdated = false;
            var query = Query<Contract>.EQ(x => x.ContractId, contract.ContractId);
            var set = Update<Contract>.Set(x => x.ContractPdfPath, contract.ContractPdfPath);
            var result = _contract.Collection.Update(query, set);
            if(result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }
            return isUpdated;
        }

        public List<Contract> GetContractsByYearQtr(Contract contract)
        {
            var query = Query.And(Query<Contract>.GTE(x => x.CreatedDate, Convert.ToDateTime(contract.ContractStartDate)),
                Query<Contract>.LTE(x=>x.CreatedDate,Convert.ToDateTime(contract.ContractEndDate)),
                Query<Contract>.EQ(x => x.CompanyId,contract.CompanyId));

            var fields = new FieldsBuilder<Contract>().Exclude(x => x.ContractScope).Exclude(x => x.ContractTerms);

            return _contract.Collection.Find(query).SetFields(fields).ToList();
        }
        
    }
}