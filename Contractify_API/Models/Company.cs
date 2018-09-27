using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class Company
    {
        private readonly MongoHelper<Company> _company;

        [BsonId]
        public ObjectId CompanyId { get; set; }

        public string FirstName { get; set; }

        public string  LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }


        public Company()
        {
            _company = new MongoHelper<Company>();
        }

        public string Create(Company company)
        {
            if (!IsUserExist(company.Email))
            {
                _company.Collection.Save(company);
                return "User Created";
            }
            else
            {
                return "User Already Exists"; 
            }
        }

        public List<Company> GetCompanies()
        {
           return _company.Collection.FindAll().ToList();
        }

        public bool IsUserExist(string email)
        {
            var company = _company.Collection.FindAll().Where(x => x.Email == email).SingleOrDefault();

            if (company != null)
            {
                return true;
            }

            return false;
        }

        public Company ValidateLogin(Company company)
        {
            return _company.Collection.FindAll().Where(x => x.Email == company.Email && x.Password == company.Password).SingleOrDefault();
        }

    }
}