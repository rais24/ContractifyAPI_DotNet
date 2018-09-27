﻿using Contractify_API.Models;
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
            Company newCompany = new Company();
            string msg = newCompany.Create(company);

            return Ok(msg);
        }

        [Route("api/company/login")]
        [HttpPost]
        public IHttpActionResult Login(Company company)
        {
            Company validatedCompany = new Company();
            validatedCompany = validatedCompany.ValidateLogin(company);

            return Ok(validatedCompany);

        }
    }
}