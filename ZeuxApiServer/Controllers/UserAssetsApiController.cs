using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeuxApiServer.Interface.UserAssets;
using ZeuxApiServer.Model.UserAssetsService;

namespace ZeuxApiServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAssetsApiController : Controller
    {
        private readonly IUserAssetsService _assetsService;
        public UserAssetsApiController(IUserAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        [Route("getAsync")]
        [HttpGet]
        public async Task<JsonResult> GetAsync([FromQuery]UserAssetsFilter filter)
        {
            var list = await _assetsService.GetUserAssets(filter);
            return Json(list);
        }
    }
}
