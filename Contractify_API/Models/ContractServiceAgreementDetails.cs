using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contractify.Models
{
    public class ContractServiceAgreementDetails
    {
		public int ServiceAgreementID { get; set; }
		public string ServiceAgreementHeading { get; set; }
		public string ServiceAgreementDetails { get; set; }
		public List<ContractServiceAgreementPointsDetails> ServiceAgreementPointDetails;
	}
}
