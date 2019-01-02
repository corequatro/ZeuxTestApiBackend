// //IDal.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com


using System.Linq;
using System.Threading.Tasks;
 
using ZeuxApiServer.Model;

namespace ZeuxApiServer.Interface
{
    public interface IDal<T> where T : BaseIdentity
    {
        Task<IQueryable<T>> Get();
    }
}