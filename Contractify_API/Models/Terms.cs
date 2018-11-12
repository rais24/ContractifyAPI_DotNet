using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class Terms
    {
        private readonly MongoHelper<Terms> _terms;
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool Status { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

        public Terms()
        {
            _terms = new MongoHelper<Terms>();
        }

        public bool Create(Terms terms)
        {
            bool isCreated = false;
            terms.CreatedDate = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt");
            var result = _terms.Collection.Save(terms);

            if (result.DocumentsAffected > 0)
            {
                isCreated = true;
            }

            return isCreated;
        }

        public bool Delete(string id)
        {
            bool isDeleted = false;

            var query = Query<Terms>.EQ(x => x.Id, id);
            var result = _terms.Collection.Remove(query);

            if (result.DocumentsAffected > 0)
            {
                isDeleted = true;
            }
            return isDeleted;
        }

        public bool Update(Terms terms)
        {
            bool isUpdated = false;

            var query = Query.And(Query<Terms>.EQ(x => x.Id, terms.Id),
                        Query<Terms>.EQ(x => x.CompanyId, terms.CompanyId));
            var replacement = Update<Terms>.Combine(Update<Terms>.Set(x => x.Type, terms.Type),
                            Update<Terms>.Set(x => x.Description, terms.Description),
                            Update<Terms>.Set(x => x.Status, terms.Status),
                            Update<Terms>.Set(x => x.UpdatedDate, DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt")));

            var result = _terms.Collection.Update(query, replacement);

            if (result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }

            return isUpdated;
        }

        public List<Terms> GetTerms(string companyId)
        {
            var query = Query<Terms>.EQ(x => x.CompanyId, companyId);
            var result = _terms.Collection.Find(query);

            return result.ToList();
        }
    }
}