using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class ContactPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}