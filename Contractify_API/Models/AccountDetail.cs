using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class AccountDetail
    {
        public string CompanyId { get; set; }
        public string ClientId { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string IFSCCode { get; set; }
    }
}