using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
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
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyId { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string CompanyName { get; set; }
        public string LegalEntityName { get; set; }
        public string RegdLegalAddress { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string GSTIN { get; set; }
        public string SecondaryContactName { get; set; }
        [Required]
        public string Email { get; set; }
        public string OfficeAddress { get; set; }
        public string Logo { get; set; }
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
            var query = Query<Company>.EQ(x => x.Email, email);
            var company = _company.Collection.FindOne(query);

            if (company != null)
            {
                return true;
            }

            return false;
        }

        public Company ValidateLogin(Company company)
        {
            var query = Query.And(
                Query<Company>.EQ(x => x.Email, company.Email),
                Query<Company>.EQ(x => x.Password, company.Password)
                );
            return _company.Collection.FindOne(query);
        }

        public Company GetCompanyById(string id)
        {
            var query = Query<Company>.EQ(x => x.CompanyId, id);
            return _company.Collection.FindOne(query);
        }

        public bool UpdateCompany(Company newCompany)
        {
            bool isUpdated = false;
            var query = Query<Company>.EQ(x => x.CompanyId, newCompany.CompanyId);
            Company oldCompany = GetCompanyById(newCompany.CompanyId.ToString());

            if (string.IsNullOrEmpty(newCompany.Password))
            {
                newCompany.Password = oldCompany.Password;
            }
            Company company = new Company
            {
                CompanyId = newCompany.CompanyId,
                FirstName = newCompany.FirstName,
                LastName = newCompany.LastName,
                CompanyName = newCompany.CompanyName,
                LegalEntityName = newCompany.LegalEntityName,
                RegdLegalAddress = newCompany.RegdLegalAddress,
                PrimaryPhoneNo = newCompany.PrimaryPhoneNo,
                SecondaryPhoneNo = newCompany.SecondaryPhoneNo,
                GSTIN = newCompany.GSTIN,
                SecondaryContactName = newCompany.SecondaryContactName,
                Email = newCompany.Email,
                OfficeAddress = newCompany.OfficeAddress,
                Logo = oldCompany.Logo,
                Password = newCompany.Password
            };

            var replacement = Update<Company>.Replace(company);
            var result = _company.Collection.Update(query, replacement);

            if(result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }

            return isUpdated;
        }

        public bool UpdateCompanyLogo(Company newCompany)
        {
            bool isUpdated = false;
            var query = Query<Company>.EQ(x => x.CompanyId, newCompany.CompanyId);
            var set = Update<Company>.Set(x => x.Logo, newCompany.Logo);
            var result = _company.Collection.Update(query, set);
            if (result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}