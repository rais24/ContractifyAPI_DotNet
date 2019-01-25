using Contractify_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class CompanyController : ApiController
    {
        [Route("api/company/register")]
        [HttpPost]
        public IHttpActionResult Register(Company company)
        {
            return Ok(new Company().Create(company));
        }

        [Route("api/company/account/{id}")]
        [HttpGet]
        public IHttpActionResult Account(string id)
        {
            return Ok(new Company().GetCompanyById(id));
        }

        [Route("api/company/update")]
        [HttpPost]
        public IHttpActionResult Update(Company company)
        {
            return Ok(new Company().UpdateCompany(company));
        }

        [Route("api/company/updateLogo")]
        [HttpPost]
        public IHttpActionResult UpdateLogo(Company company)
        {
            return Ok(new Company().UpdateCompanyLogo(company));
        }
    }
}
