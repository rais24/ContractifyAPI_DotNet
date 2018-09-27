using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contractify.Models
{
    public class ContractData
    {
		public int ContractID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndTime { get; set; }
		public ContractPromiseeDetails PromiseeDetails;
		public ContractPromiserData PromiserDetails;
		public string ContractType { get; set; }
		public string ContractCurrency { get; set;}
		public List<ContractServiceDetailsData> Services;
		public List<ContractServiceAgreementDetails> AgreementDetails;
		public bool IsContractInternational;
	}
}
