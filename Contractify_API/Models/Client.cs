using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contractify_API.Models
{
    public class Client
    {
        private readonly MongoHelper<Client> _client;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
        public string CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string LegalEntityName { get; set; }
        public string RegdLegalAddress { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string Email { get; set; }
        public string OfficeAddress { get; set; }
        public string PaymentTerm { get; set; }
        public string Logo { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public List<ContactPerson> ContactPersons { get; set; }

        public Client()
        {
            _client = new MongoHelper<Client>();
        }

        public string Create(Client client)
        {
            if (!IsClientExist(client.Email))
            {
                client.CreatedDate = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt");
                _client.Collection.Save(client);
                return "Company Created";
            }
            else
            {
                return "Company Already Exists";
            }
        }

    
        public bool IsClientExist(string email)
        {
            var query = Query<Client>.EQ(x => x.Email, email);
            var client = _client.Collection.FindOne(query);

            if (client != null)
            {
                return true;
            }

            return false;
        }

        public List<Client> GetAllClient(string companyId)
        {
            var query = Query<Client>.EQ(x => x.CompanyId, companyId);
            var result = _client.Collection.Find(query);

            return result.ToList();
        }

        public Client GetClientById(string id)
        {
            var query = Query<Client>.EQ(x => x.ClientId, id);
            return _client.Collection.FindOne(query);
        }

        public bool UpdateClient(Client client)
        {
            bool isUpdated = false;
            var query = Query<Client>.EQ(x => x.ClientId, client.ClientId);
            Client oldClient = GetClientById(client.ClientId.ToString());

            if (client.ContactPersons != null)
            {
                foreach (var cPerson in client.ContactPersons)
                {
                    oldClient.ContactPersons.Add(cPerson);
                }
            }

            Client company = new Client
            {
                ClientId = client.ClientId,
                CompanyId = oldClient.CompanyId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                CompanyName = client.CompanyName,
                LegalEntityName = client.LegalEntityName,
                RegdLegalAddress = client.RegdLegalAddress,
                PrimaryPhoneNo = client.PrimaryPhoneNo,
                SecondaryPhoneNo = client.SecondaryPhoneNo,
                GSTIN = client.GSTIN,
                PAN = client.PAN,
                Email = client.Email,
                OfficeAddress = client.OfficeAddress,
                Logo = oldClient.Logo,
                PaymentTerm = client.PaymentTerm,
                ContactPersons = oldClient.ContactPersons,
                CreatedDate = oldClient.CreatedDate,
                UpdatedDate = System.DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt")
            };

            var replacement = Update<Client>.Replace(company);
            var result = _client.Collection.Update(query, replacement);

            if (result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }

            return isUpdated;
        }

        public bool DeleteClient(string id)
        {
            bool isDeleted = false;
            var query = Query<Client>.EQ(x => x.ClientId, id);
            var result = _client.Collection.Remove(query);

            if (result.DocumentsAffected > 0)
            {
                isDeleted = true;
            }
            return isDeleted;
        }

        public bool DeleteContactPerson(string clientId,ContactPerson contact)
        {
            bool isDeleted = false;
            var query = Query<Client>.EQ(x => x.ClientId, clientId);
            var pull = Update<Client>.Pull(x => x.ContactPersons, contact);
            var result = _client.Collection.Update(query, pull);
            if (result.DocumentsAffected > 0)
            {
                isDeleted = true;
            }
            return isDeleted;
        }

        public bool UpdateClientLogo(Client client)
        {
            bool isUpdated = false;
            var query = Query<Client>.EQ(x => x.ClientId, client.ClientId);
            var set = Update<Client>.Set(x => x.Logo, client.Logo);
            var result = _client.Collection.Update(query, set);
            if (result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }
            return isUpdated;
        }

        public List<ContactPerson> GetClientContactPersons(string id)
        {
            List<ContactPerson> contacts = new List<ContactPerson>();

            Client client = new Client();
            client = GetClientById(id);
            contacts = client.ContactPersons;
            return contacts;

        }

    }
}