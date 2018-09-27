using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contractify.Models
{
    public class ContractServiceDetailsPromises
    {
		public int ServicePromiseID { get; set; }
		public string ServicePromiseShortDesc { get; set; }
		public string ServicePromiseLongDesc { get; set; }
		public float ServicePromisePrice { get; set; }
		public float ServicePromiseNegotiationMargin { get; set; }
	}
}
