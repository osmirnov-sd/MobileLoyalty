namespace AuthControllersLibrary
{
    public class UiController : ControllerBase
    {

        [Route("GetAds")]
        [HttpPost]
        public IActionResult GetAds()
        {
            return new OkObjectResult(new { Ok = "GetAds Ok" });
        }

        [Route("GetHistory")]
        [HttpPost]
        public IActionResult GetHistory()
        {
            return new OkObjectResult(new { Ok = "GetHistory Ok" });
        }

    }
}
