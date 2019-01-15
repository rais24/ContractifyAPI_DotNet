using Contractify_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class ContractHistoryController : ApiController
    {
        [HttpGet]
        [Route("api/contracthistory/{memberId}")]
        public IHttpActionResult GetAllHistory(string memberId )
        {
           return Ok(new ContractHistory().GetContractHistory(memberId));
        }

        [HttpGet]
        [Route("api/contracthistory/gethistory/{historyId}")]
        public IHttpActionResult GetHistory(string historyId)
        {
            return Ok(new ContractHistory().GetContractHistoryById(historyId));
        }
    }
}
