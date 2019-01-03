// //IUserAuthService.cs
// // Copyright (c) 2019 01 03All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System.Threading.Tasks;

namespace ZeuxApiServer.Interface.UserAuthService
{
    public interface IUserAuthService
    {
        Task<bool> HasCorrectCredentials(string modelUsername, string modelPassword);
    }
}