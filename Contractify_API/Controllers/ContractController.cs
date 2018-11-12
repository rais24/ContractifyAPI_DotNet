using Contractify_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class ContractController : ApiController
    {
        [HttpPost]
        [Route("api/contract/create")]
        public IHttpActionResult CreateContract(Contract contract)
        {
            return Ok(new Contract().AddContract(contract));
        }

        [HttpPost]
        [Route("api/contract/update")]
        public IHttpActionResult UpdateContract(Contract contract)
        {
            return Ok(new Contract().UpdateContract(contract));
        }

        [HttpGet]
        [Route("api/contract/delete/{id}")]
        public IHttpActionResult DeleteContract(string id)
        {
            return Ok(new Contract().DeleteContract(id));
        }

        [HttpGet]
        [Route("api/contract/allcontract/{mid}")]
        public IHttpActionResult AllContracts(string mid)
        {
            return Ok(new Contract().GetAllContracts(mid));
        }

        [HttpGet]
        [Route("api/contract/{id}")]
        public IHttpActionResult GetContract(string id)
        {
            return Ok(new Contract().GetContractById(id));
        }

        [HttpGet]
        public IHttpActionResult CloseContract(string id)
        {
            return Ok(new Contract().CloseContract(id));
        }
    }
}
