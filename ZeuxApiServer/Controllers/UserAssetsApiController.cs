using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeuxApiServer.Model;
using ZeuxApiServer.Model.UserAssetsApiController;

namespace ZeuxApiServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAssetsApiController : Controller
    {
        [Route("getAsync")]
        [HttpGet]
        public async Task<JsonResult> GetAsync([FromQuery]UserAssetsFilter filter)
        {
            return Json("test result");
        }

    }
}
