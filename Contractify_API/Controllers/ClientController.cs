using Contractify_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class ClientController : ApiController
    {
        [Route("api/client/create")]
        [HttpPost]
        public IHttpActionResult Create(Client client)
        {
            return Ok(new Client().Create(client));
        }

        [Route("api/client/allclient/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllClient(string id)
        {
            return Ok(new Client().GetAllClient(id));
        }

        [Route("api/client/getclient/{id}")]
        [HttpGet]
        public IHttpActionResult GetClient(string id)
        {
            return Ok(new Client().GetClientById(id));
        }

        [Route("api/client/updateclient")]
        [HttpPost]
        public IHttpActionResult UpdateClient(Client client)
        {
            return Ok(new Client().UpdateClient(client));
        }

        [Route("api/client/delete/{id}")]
        [HttpGet]
        public IHttpActionResult Delete(string id)
        {
            return Ok(new Client().DeleteClient(id));
        }

        [Route("api/client/deletecontact")]
        [HttpPost]
        public IHttpActionResult DeleteContact(Client client)
        {
            return Ok(new Client().DeleteContactPerson(client.ClientId, client.ContactPersons.First()));
        }

        [Route("api/client/updateLogo")]
        [HttpPost]
        public IHttpActionResult UpdateLogo(Client client)
        {
            return Ok(new Client().UpdateClientLogo(client));
        }

        [Route("api/client/getclientcontact/{id}")]
        public IHttpActionResult GetClientContact(string id)
        {
            return Ok(new Client().GetClientContactPersons(id));
        }
    }
}
