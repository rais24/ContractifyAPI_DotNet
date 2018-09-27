using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contractify.Models
{
    public class ContractPromiseeDetails
    {
		public string PromiseeName { get; set; }
		public string PromiseeCompanyName { get; set; }
		public string PromiseeLegalEntityName { get; set; }
		public string PromiseeRegistedLegalAddress { get; set; }
		public string PromiseePrimaryPhoneNumber { get; set; }
		public string PromiseeSecondaryPhoneNumber { get; set; }
		public string PromiseeGSTINNumber { get; set; }
		public string PromiseeSecondaryContactName { get; set; }
		public string PromiseeSecondaryContactPrimaryPhoneNumber { get; set; }
		public string PromiseeEmailAddress { get; set; }
		public string PromiseeOfficeAddress { get; set; }
		public string PromiseeLogo { get; set; }
	}
}
