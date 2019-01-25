using Contractify_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("api/login/validatelogin")]
        public IHttpActionResult ValidateLogin(Login login)
        {
            Company comp = new Company().ValidateLogin(login);

            if(comp == null || comp.CompanyId == null)
            {
                SalesRep rep = new SalesRep().validateLogin(login);

                if(rep == null || rep.UserId == null)
                {
                    return Ok(Tuple.Create<Company, SalesRep, string>(null, null, "invalid"));
                }
                else
                {
                    return Ok(Tuple.Create<Company, SalesRep, string>(null, rep, "rep"));
                }
            }
            else
            {
                return Ok(Tuple.Create<Company, SalesRep, string>(comp, null, "comp"));
            }
        }
    }
}
