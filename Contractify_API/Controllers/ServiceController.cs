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
            ServiceMaster nService = new ServiceMaster();
            return Ok(nService.CreateService(service));
        }

        [HttpGet]
        [Route("api/service/getmasterservice/{companyId}")]
        public IHttpActionResult GetAllMasterService(string companyId)
        {
            ServiceMaster nService = new ServiceMaster();
            return Ok(nService.GetAllMasterService(companyId));
        }

        [HttpGet]
        [Route("api/service/getsubservice/{serviceId}")]
        public IHttpActionResult GetSubServicesByParentId(string serviceId)
        {
            ServiceMaster subService = new ServiceMaster();
            return Ok(subService.GetSubServices(serviceId));
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
            ServiceMaster service = new ServiceMaster();
            return Ok(service.GetAllService(companyId));
        }

        [HttpPost]
        [Route("api/service/updateservice")]
        public IHttpActionResult UpdateService(ServiceMaster service)
        {
            ServiceMaster nService = new ServiceMaster();
            return Ok(nService.UpdateService(service));
        }

        [HttpGet]
        [Route("api/service/deleteservice/{serviceId}/{serviceType}")]
        public IHttpActionResult DeleteService(string serviceId,string serviceType)
        {
            return Ok();
        }
    }
}
