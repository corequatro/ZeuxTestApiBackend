// //IIdentity.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System;

namespace ZeuxApiServer.Interface
{
    public interface IIdentity
    {
        Guid Id { get; set; }
    }
}