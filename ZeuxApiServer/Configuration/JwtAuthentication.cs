﻿// //JwtAuthentication.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System;
using Microsoft.IdentityModel.Tokens;

namespace ZeuxApiServer.Configuration
{
    public class JwtAuthentication
    {
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(SecurityKey));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}