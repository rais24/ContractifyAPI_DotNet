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
            Client newClient = new Client();
            return Ok(newClient.Create(client));
        }

        [Route("api/client/allclient/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllClient(string id)
        {
            Client client = new Client();
            return Ok(client.GetAllClient(id));
        }

        [Route("api/client/getclient/{id}")]
        [HttpGet]
        public IHttpActionResult GetClient(string id)
        {
            Client client = new Client();
            return Ok(client.GetClientById(id));
        }

        [Route("api/client/updateclient")]
        [HttpPost]
        public IHttpActionResult UpdateClient(Client client)
        {
            Client nClient = new Client();
            return Ok(client.UpdateClient(client));
        }

        [Route("api/client/delete/{id}")]
        [HttpGet]
        public IHttpActionResult Delete(string id)
        {
            Client client = new Client();
            return Ok(client.DeleteClient(id));
        }

        [Route("api/client/deletecontact")]
        [HttpPost]
        public IHttpActionResult DeleteContact(Client client)
        {
            Client nClient = new Client();
            return Ok(nClient.DeleteContactPerson(client.ClientId, client.ContactPersons.First()));
        }

        [Route("api/client/updateLogo")]
        [HttpPost]
        public IHttpActionResult UpdateLogo(Client client)
        {
            Client nClient = new Client();
            return Ok(nClient.UpdateClientLogo(client));
        }

        [Route("api/client/getclientcontact/{id}")]
        public IHttpActionResult GetClientContact(string id)
        {
            Client nClient = new Client();
            return Ok(nClient.GetClientContactPersons(id));
        }
    }
}
