using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeuxApiServer.Interface;
using ZeuxApiServer.Interface.UserAssets;
using ZeuxApiServer.Model.UserAssetsService;

namespace ZeuxApiServer.Services
{
    public class UserAssetsService : IUserAssetsService
    {
        private readonly IDal<UserAsset> _dal;

        public UserAssetsService(IDal<UserAsset> dal)
        {
            _dal = dal;
        }

        public async Task<IList<UserAsset>> GetUserAssets(UserAssetsFilter filter)
        {
            var list = await _dal.Get();
            return filter.ProductType.HasValue ? list.Where(i => i.ProductType == filter.ProductType).ToList() : list.ToList();
        }
    }
}
