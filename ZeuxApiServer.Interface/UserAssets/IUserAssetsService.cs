// //IUserAssetsService.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System.Collections.Generic;
using System.Threading.Tasks;
using ZeuxApiServer.Model.UserAssetsService;

namespace ZeuxApiServer.Interface.UserAssets
{
    public interface IUserAssetsService
    {
        Task<IList<UserAsset>> GetUserAssets(UserAssetsFilter filter);
    }
}