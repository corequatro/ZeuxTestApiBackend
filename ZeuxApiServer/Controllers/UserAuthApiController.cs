// //UserAuthApiController.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ZeuxApiServer.Configuration;
using ZeuxApiServer.Model.UserAuthApi;

namespace ZeuxApiServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAuthApiController : Controller
    {
        private readonly IOptions<JwtAuthentication> _jwtAuthentication;

        public UserAuthApiController(IOptions<JwtAuthentication> jwtAuthentication)
        {
            _jwtAuthentication = jwtAuthentication;
        }


        [Route("generateToken")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> PostAsync([FromBody]GenerateTokenModel model)
        {
            var token = new JwtSecurityToken(
                     issuer: _jwtAuthentication.Value.ValidIssuer,
                     audience: _jwtAuthentication.Value.ValidAudience,
                     claims: new[]
                     {
                   new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     },
                     expires: DateTime.UtcNow.AddDays(30),
                     notBefore: DateTime.UtcNow,
                     signingCredentials: _jwtAuthentication.Value.SigningCredentials);

            return Json(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}