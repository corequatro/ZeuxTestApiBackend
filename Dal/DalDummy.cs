using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeuxApiServer.Interface;
using ZeuxApiServer.Model;
using ZeuxApiServer.Model.UserAssetsService;

namespace ZeuxApiServer.Dal
{
    public class DalDummy<T> : IDal<T> where T : BaseIdentity
    {
        public async Task<IQueryable<T>> Get()
        {
            if (typeof(T) == typeof(UserAsset))
            {
                var list = new List<UserAsset>();
                var typeslist = Enum.GetValues(typeof(ProductTypeEnum)).Cast<ProductTypeEnum>().ToList();
                foreach (var type in typeslist)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        list.Add(new UserAsset
                        {
                            Name = "savings asset" + i,
                            Currency = CurrencyEnum.Uk,
                            ProductType = type,
                            Price = 100 + i,
                            Returns = 2 + i,
                            Id = Guid.NewGuid()
                        });
                    }
                }

                return list.OfType<T>().AsQueryable();
            }

            return null;
        }
    }
}
