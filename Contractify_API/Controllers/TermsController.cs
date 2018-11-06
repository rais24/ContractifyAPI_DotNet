using Contractify_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class TermsController : ApiController
    {
        [HttpGet]
        [Route("api/terms/getTerms/{id}")]
        public IHttpActionResult GetTerms(string id)
        {
            Terms nterm = new Terms();
            return Ok(nterm.GetTerms(id));
        }

        [HttpPost]
        [Route("api/terms/create")]
        public IHttpActionResult Create(Terms term)
        {
            Terms nterm = new Terms();
            return Ok(nterm.Create(term));
        }

        [HttpPost]
        [Route("api/terms/update")]
        public IHttpActionResult Update(Terms term)
        {
            Terms nterm = new Terms();
            return Ok(nterm.Update(term));
        }

        [HttpPost]
        [Route("api/terms/delete")]
        public IHttpActionResult Delete(Terms term)
        {
            Terms nterm = new Terms();
            return Ok(nterm.Delete(term.Id));
        }
    }
}
