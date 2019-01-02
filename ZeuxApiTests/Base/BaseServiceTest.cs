// //BaseTest.cs
// // Copyright (c) 2019 01 02All Rights Reserved
// // Bogdan Lyashenko
// // corequatro@gmail.com

using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ZeuxApiServer;

namespace ZeuxApiTests.Base
{
    public class BaseServiceTest
    {
        protected TestServer TestServer { get; set; }
        protected HttpClient Client { get; set; }

        private string _user;
        protected BaseServiceTest()
        {
            TestServer = InitTestServer();
            Client = TestServer.CreateClient();

        }

        [SetUp]
        public void SetUp()
        {
       
        }

        private TestServer InitTestServer()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder();
            webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            webHostBuilder.UseStartup<Startup>();
            webHostBuilder.UseConfiguration(new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
            return new TestServer(webHostBuilder);
        }
    }
}