using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contractify.Models
{
    public class ContractServiceDetailsData
    {
		public string ServiceID { get; set; }
		public string ServiceName { get; set; }
		public string ServicePrice { get; set; }
		public List<ContractServiceDetailsPromises> ServicePromises;
	}
}
