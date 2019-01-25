using Contractify_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class SalesRepController : ApiController
    {
        [HttpPost]
        [Route("api/salesrep/create")]
        public IHttpActionResult CreateSalesRep(SalesRep rep)
        {
            return Ok(new SalesRep().Create(rep));
        }

        [HttpGet]
        [Route("api/salesrep/allsalesrep/{companyId}")]
        public IHttpActionResult GetAllSalesRep(string companyId)
        {
            return Ok(new SalesRep().GetAll(companyId));
        }

        [HttpGet]
        [Route("api/salesrep/deletesalesrep/{repId}")]
        public IHttpActionResult DeleteSalesRep(string repId)
        {
            return Ok(new SalesRep().DeleteSalesRep(repId));
        }

        [HttpGet]
        [Route("api/salesrep/salesrepinfo/{repId}")]
        public IHttpActionResult GetSalesRepInfo(string repId)
        {
            return Ok(new SalesRep().GetSalesRepInfo(repId));
        }

        [HttpPost]
        [Route("api/salesrep/updatesalesrep")]
        public IHttpActionResult UpdateSalesRep(SalesRep rep)
        {
            return Ok(new SalesRep().UpdateSalesRep(rep));
        }
    }
}
