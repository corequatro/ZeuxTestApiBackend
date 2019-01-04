using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using NUnit.Framework;
using ZeuxApiServer.Model.UserAssetsService;
using ZeuxApiServer.Model.UserAuthApi;
using ZeuxApiTests.Base;

namespace ZeuxApiTests.Tests
{
    public class UserAssetsApiTest : BaseServiceTest
    {
        private string UserAssetsApiUrl(string method) => $"api/v1/UserAssetsApi/{method}";
        private string UserLoginApiUrl(string method) => $"api/v1/UserAuthApi/{method}";


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetUserAssets_NoJwtToken_Unauthorized()
        {
            var response = await Client.GetAsync(UserAssetsApiUrl("getAsync"));
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetUserAssets_JwtTokenExsistsNotValid_Unauthorized()
        {
            var response = await Client.PostAsync(UserLoginApiUrl("generateToken"), new StringContent(JsonConvert.SerializeObject(new GenerateTokenModel()
            {
                Username = "testUser",
                Password = "testPassword"

            }), Encoding.UTF8, "application/json"));
            var resultStr = await response.Content.ReadAsStringAsync();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, Guid.NewGuid().ToString());
            var assetsResponse = await Client.GetAsync(UserAssetsApiUrl("getAsync"));
            Assert.AreEqual(HttpStatusCode.Unauthorized, assetsResponse.StatusCode);
        }


        [Test]
        public async Task GetUserAssets_JwtTokenExsists_Success()
        {
            var response = await Client.PostAsync(UserLoginApiUrl("generateToken"), new StringContent(JsonConvert.SerializeObject(new GenerateTokenModel()
            {
                Username = "testUser",
                Password = "testPassword"

            }), Encoding.UTF8, "application/json"));
            var resultStr = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(resultStr);
            var token = result.token.Value;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var assetsResponse = await Client.GetAsync(UserAssetsApiUrl("getAsync"));
            Assert.AreEqual(HttpStatusCode.OK, assetsResponse.StatusCode);
        }

        [TestCase(ProductTypeEnum.Funds, 10)]
        [TestCase(ProductTypeEnum.P2P, 10)]
        [TestCase(ProductTypeEnum.Savings, 10)]
        [TestCase(null, 30 )]
        [Test]
        public async Task GetUserAssets_FilteredByType_CorrectAmmountReturnedForEachCase(ProductTypeEnum? typeIEnum, int count)
        {
            var response = await Client.PostAsync(UserLoginApiUrl("generateToken"), new StringContent(JsonConvert.SerializeObject(new GenerateTokenModel()
            {
                Username = "testUser",
                Password = "testPassword"
            }), Encoding.UTF8, "application/json"));

            var resultStr = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(resultStr);
            var token = result.token.Value;
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var assetsResponse = await Client.GetAsync(UserAssetsApiUrl("getAsync") + "?productType=" + typeIEnum);
            var responseItemHtml = await assetsResponse.Content.ReadAsStringAsync();
            var itemResult = JsonConvert.DeserializeObject<List<UserAsset>>(responseItemHtml);
            Assert.AreEqual(HttpStatusCode.OK, assetsResponse.StatusCode);
            Assert.AreEqual(count, itemResult.Count);
        }
    }
}