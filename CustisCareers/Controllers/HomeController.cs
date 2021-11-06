using CustisCareers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;

namespace CustisCareers.Controllers
{
    public class HomeController : Controller
    {

        string client_id = "RP8UM22DLATRJ880AAAAVC83ERFDUV3L6PEP7BLHJBDTH0078OP45R2LMI9KS7OH";
        string secret = "NTH5OP3O918OKTHDQMCDTIP6GK5VVI75O5OUBMNO12512DI9MABTL35UG1I6QGNV";
        string code = "RAJTGJPFJGEAIJP7LURHRUA34EVLARFSGC4L57V7QBUCBLK1K3370D44B2L5704G";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //    //var webRequest = System.Net.WebRequest.Create("https://api.hh.ru/vacancies");

            HttpWebRequest webRequest =
                            (HttpWebRequest)HttpWebRequest.Create(
                                $"https://api.hh.ru/resumes/mine");
            //HttpWebRequest webRequest = 
            //    (HttpWebRequest)HttpWebRequest.Create(
            //        "https://api.hh.ru/vacancies");

            webRequest.AllowAutoRedirect = true;
            HttpWebResponse httpWebResponse = null;

            try
            {
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.ContentType = "application/application/x-www-form-urlencoded";
                    webRequest.Headers.Add("User-Agent: api-test-agent");
                    webRequest.Headers.Add("Authorization: Bearer SDG0U5H3PLQGE5SUNPER52M5KR5P6V7GV8II2IRKNIIDQEJMUCV78CACG46S7K7U");

                    httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                    using (System.IO.Stream s = httpWebResponse.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            String.Format("Response: {0}", jsonResponse);

                            ViewBag.vacs = string.Format("Response: {0}", jsonResponse);

                            ViewBag.respuri = httpWebResponse.ResponseUri;
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Message.Contains("302"))
                    ViewBag.resp = $"{e.Response} \n {e.HResult}";

                ViewBag.respuri = httpWebResponse.ResponseUri;
            }

            //return Redirect("https://hh.ru/oauth/authorize?response_type=code&client_id=RP8UM22DLATRJ880AAAAVC83ERFDUV3L6PEP7BLHJBDTH0078OP45R2LMI9KS7OH");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
