using System.Web.Mvc;
using RestSharp;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pdf()
        {
            var data = getPDF(@"http://localhost:3066", @"Example-PDF.pdf");
            Response.Headers.Add("Content-Length", data.Length.ToString());
            Response.Headers.Add("Content-Disposition", "inline; filename=Example-PDF.pdf");
            return new FileContentResult(data, "application/pdf");
        }

        //using restsharp
        private byte[] getPDF(string baseUrl, string pdfName)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(pdfName, Method.GET);
            request.AddHeader("Accept", "*/*");
            IRestResponse response = client.Execute(request);
            return response.RawBytes;
        }
    }
}