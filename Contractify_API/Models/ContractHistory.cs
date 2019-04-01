using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class ContractHistory
    {
        private readonly MongoHelper<ContractHistory> _history;
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HistoryId { get; set; }
        public string CompanyId { get; set; }
        public Contract Contract { get; set; }
        public string Action { get; set; }
        public DateTime CreatedDate { get; set; }

        public ContractHistory() { _history = new MongoHelper<ContractHistory>(); }

        public bool Save(ContractHistory history)
        {
            var result = _history.Collection.Save(history);

            if(result.DocumentsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public List<ContractHistory> GetContractHistory(string companyId)
        {
            var query = Query<ContractHistory>.EQ(x => x.CompanyId, companyId);

            var fields = new FieldsBuilder<ContractHistory>()
                .Include(x => x.HistoryId)
                .Include(x => x.Contract.CompanyName)
                .Include(x => x.Contract.ContactPerson)
                .Include(x => x.Contract.ContractId)
                .Include(x => x.Contract.ContractDescription)
                .Include(x => x.Action)
                .Include(x => x.CreatedDate);

            return _history.Collection.Find(query).SetFields(fields).ToList();
        }

        public ContractHistory GetContractHistoryById(string historyId)
        {
            var query = Query<ContractHistory>.EQ(x => x.HistoryId, historyId);

            return _history.Collection.FindOne(query);
        }
    }
}