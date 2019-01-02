// //GenerateTokenModel.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System.ComponentModel.DataAnnotations;

namespace ZeuxApiServer.Model.UserAuthApi
{
    public class GenerateTokenModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}