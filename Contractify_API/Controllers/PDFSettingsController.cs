using Contractify_API.Models;
using System.Web.Http;

namespace Contractify_API.Controllers
{
    public class PDFSettingsController : ApiController
    {
        [HttpGet]
        [Route("api/pdfsettings/getpdfsettings/{companyId}")]
        public IHttpActionResult GetPDFSettings(string companyId)
        {
            return Ok(new PdfSettings().GetSettings(companyId));
        }

        [HttpPost]
        [Route("api/pdfsettings/updatepdfsettings")]
        public IHttpActionResult UpdatePDFSettings(PdfSettings settings)
        {
            return Ok(new PdfSettings().UpdateSettings(settings));
        }

        [HttpPost]
        [Route("api/pdfsettings/updatepdflogo")]
        public IHttpActionResult UpdatePDFLogo(PdfSettings settings)
        {
            return Ok(new PdfSettings().UpdateLogo(settings));
        }
    }
}
