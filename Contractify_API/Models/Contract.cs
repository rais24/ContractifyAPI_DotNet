using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class Contract
    {
        private readonly MongoHelper<Contract> _contract;

        public string ContractId { get; set; }
        public string ContractStatus { get; set; } // proposal,agreement,contract,closed
        public string ClientId { get; set; }
        public string ContactPerson { get; set; }
        public string ContractName { get; set; }
        public string ContractType { get; set; } // digital marketing, technical, both
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string ContractDescription { get; set; }
        public List<ServiceMaster> ContractScope { get; set; }
        public List<Terms> ContractTerms { get; set; }

        public Contract()
        {
            _contract = new MongoHelper<Contract>();
        }

        
    }
}