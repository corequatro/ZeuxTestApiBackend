using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using ZeuxApiTests.Base;

namespace ZeuxApiTests.Tests
{
    public class UserAssetsApiTest : BaseServiceTest
    {
        private string UserAssetsApiUrl(string method) => $"api/v1/UserAssetsApi/{method}";
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



    }
}