// //UserAuthApiController.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ZeuxApiServer.Configuration;
using ZeuxApiServer.Model.UserAuthApi;

namespace ZeuxApiServer.Controllers
{
    public class UserAuthApiController : Controller
    {
        private readonly IOptions<JwtAuthentication> _jwtAuthentication;

        public UserAuthApiController(IOptions<JwtAuthentication> jwtAuthentication)
        {
            _jwtAuthentication = jwtAuthentication ?? throw new ArgumentNullException(nameof(jwtAuthentication));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GenerateToken([FromBody]GenerateTokenModel model)
        {
            // TODO use your actual logic to validate a user
            if (model.Password != "654321")
                return BadRequest("Username or password is invalid");

            var token = new JwtSecurityToken(
                issuer: jwtAuthentication.ValidIssuer,
                audience: jwtAuthentication.ValidAudience,
                claims: new[]
                {
                    // You can add more claims if you want
                    new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                },
                expires: DateTime.UtcNow.AddDays(30),
                notBefore: DateTime.UtcNow,
                signingCredentials: _jwtAuthentication.Value.SigningCredentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}