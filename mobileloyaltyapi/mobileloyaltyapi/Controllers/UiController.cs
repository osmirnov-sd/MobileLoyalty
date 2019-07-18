using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using mobileloyaltyapi.Models;

namespace mobileloyaltyapi.Controllers
{
    public class UiController : ApiController
    {
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Code()
        {
            byte[] imgData = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/qrcode.png"));
            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }

        [System.Web.Http.HttpGet]
        public Profile Profile()
        {
            return new Profile()
            {
                UserName = "Вася пупкин",
                Balance = 1000,
                Transactions = new List<string>
                {
                    "description1","description2"
                }
            };
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<string> Ads()
        {
            return new List<string>
            {
                "ad description",
                "ad description",
                "ad description"
            };
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<string> History()
        {
            return new List<string>
            {
                "record description",
                "record description",
                "record description"
            };
        }
    }
}
