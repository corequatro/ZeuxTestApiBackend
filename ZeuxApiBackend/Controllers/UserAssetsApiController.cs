using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeuxApiBackend.Model;

namespace ZeuxApiBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAssetsApiController : Controller
    {
        // GET api/values

        [Route("getAsync")]
        [HttpGet]
        public async Task<JsonResult> GetAsync([FromQuery] UserAssetFilterModel model)
        {
            return Json("test data");
        }

        
    }
}
