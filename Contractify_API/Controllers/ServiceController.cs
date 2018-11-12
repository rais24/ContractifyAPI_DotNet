using Contractify_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class ServiceController : ApiController
    {
        [Route("api/service/createmasterservice")]
        [HttpPost]
        public IHttpActionResult CreateMasterService(ServiceMaster service)
        {
            return Ok(new ServiceMaster().CreateService(service));
        }

        [HttpGet]
        [Route("api/service/getmasterservice/{companyId}")]
        public IHttpActionResult GetAllMasterService(string companyId)
        {
            return Ok(new ServiceMaster().GetAllMasterService(companyId));
        }

        [HttpGet]
        [Route("api/service/getsubservice/{serviceId}")]
        public IHttpActionResult GetSubServicesByParentId(string serviceId)
        {
            return Ok(new ServiceMaster().GetSubServices(serviceId));
        }

        [HttpPost]
        [Route("api/service/createsubservice")]
        public IHttpActionResult CreateSubService(ServiceMaster service)
        {
            string message = string.Empty;
            ServiceMaster subService = new ServiceMaster();
            var jSubServiceNames = JsonConvert.DeserializeObject<JArray>(service.Name);
            var jSubServicePrices = JsonConvert.DeserializeObject<JArray>(service.Price);
            List<string> subServiceNames = new List<string>();
            List<string> subServicePrices = new List<string>();
            try
            {
                foreach (JValue val in jSubServiceNames)
                {
                    subServiceNames.Add(val.ToString());
                }
                foreach (JValue val in jSubServicePrices)
                {
                    subServicePrices.Add(val.ToString());
                }
                List<ServiceMaster> serviceMasters = new List<ServiceMaster>();
                for (int i = 0; i < subServiceNames.Count; i++)
                {
                    serviceMasters.Add(new ServiceMaster(CompanyId: service.CompanyId, Name: subServiceNames[i], Price: subServicePrices[i], ParentId: service.ParentId));
                }

                foreach (ServiceMaster sub in serviceMasters)
                {
                    message = subService.CreateSubService(sub);
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
            return Ok(message);
        }

        [HttpPost]
        [Route("api/service/createsubsubservice")]
        public IHttpActionResult CreateSubSubService(ServiceMaster service)
        {
            string message = string.Empty;
            ServiceMaster subService = new ServiceMaster();
            var jSubServiceNames = JsonConvert.DeserializeObject<JArray>(service.Name);
            List<string> subServiceNames = new List<string>();
            try
            {
                foreach (JValue val in jSubServiceNames)
                {
                    subServiceNames.Add(val.ToString());
                }
               
                List<ServiceMaster> serviceMasters = new List<ServiceMaster>();
                for (int i = 0; i < subServiceNames.Count; i++)
                {
                    serviceMasters.Add(new ServiceMaster(CompanyId: service.CompanyId, Name: subServiceNames[i], ParentId: service.ParentId));
                }

                foreach (ServiceMaster sub in serviceMasters)
                {
                    message = subService.CreateSubService(sub);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return Ok(message);
        }

        [HttpGet]
        [Route("api/service/getallservice/{companyId}")]
        public IHttpActionResult GetAllService(string companyId)
        {
            return Ok(new ServiceMaster().GetAllService(companyId));
        }

        [HttpPost]
        [Route("api/service/updateservice")]
        public IHttpActionResult UpdateService(ServiceMaster service)
        {
            return Ok(new ServiceMaster().UpdateService(service));
        }

        [HttpGet]
        [Route("api/service/deleteservice/{companyId}/{serviceId}/{serviceType}")]
        public IHttpActionResult DeleteService(string companyId,string serviceId,string serviceType)
        {
            ServiceMaster service = new ServiceMaster();

            if (serviceType.Equals("master"))
            {
                return Ok(service.DeleteMasterService(companyId,serviceId));
            }
            else if (serviceType.Equals("sub"))
            {
                return Ok(service.DeleteSubService(companyId, serviceId));
            }
            else if (serviceType.Equals("ssub"))
            {
                return Ok(service.DeleteSubSubService(companyId, serviceId));
            }
            else
            {
                return Ok("false");
            }
           
        }
    }
}
