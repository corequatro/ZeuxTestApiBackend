// //UserEntity.cs
// // Copyright (c) 2019 01 03All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

namespace ZeuxApiServer.Model.UserAuthService
{
    public class UserEntity : BaseIdentity
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}