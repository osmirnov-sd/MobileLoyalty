using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuthControllersLibrary
{
    public class UiController : ControllerBase
    {

        [Route("GetAds")]
        [HttpPost]
        public IActionResult GetAds()
        {
            return new OkObjectResult(new { Ok = "Ok" });
        }

    }
}
