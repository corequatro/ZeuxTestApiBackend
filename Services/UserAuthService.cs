// //UserAuthService.cs
// // Copyright (c) 2019 01 03All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System;
using System.Threading.Tasks;
using ZeuxApiServer.Interface.UserAuthService;
using ZeuxApiServer.Model.UserAuthService;

namespace ZeuxApiServer.Services
{
    public class UserAuthService : IUserAuthService
    {
        private bool IsUserOk(string login, string password, UserEntity dummyUser) => (dummyUser.UserLogin.Equals(login) && dummyUser.UserPassword.Equals(password));
        public async Task<bool> HasCorrectCredentials(string modelUsername, string modelPassword)
        {
            //TODO it's dummy user password. We must store password in db only in encrypted form
            var dummyUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserLogin = "testUser",
                UserPassword = "testPassword"
            };

            return IsUserOk(modelUsername, modelPassword, dummyUser);
        }
    }
}