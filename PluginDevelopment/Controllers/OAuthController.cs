using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace PluginDevelopment.Controllers
{
    public class OAuthController : ApiController
    {
        public HttpResponseMessage Get(string code)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(code, Encoding.UTF8, "text/plain")
            };
        }


        public HttpResponseMessage GetToken()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("", Encoding.UTF8, "text/plain")
            };
        }
    }
}
