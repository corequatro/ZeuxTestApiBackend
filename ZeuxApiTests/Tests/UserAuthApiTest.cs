using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using ZeuxApiServer.Model.UserAuthApi;
using ZeuxApiTests.Base;

namespace ZeuxApiTests.Tests
{
    public class UserAuthApiTest : BaseServiceTest
    {
        private string UserLoginApiUrl(string method) => $"api/v1/UserAuthApi/{method}";

        [Test]
        public async Task GenerateToken_WrongLoginOrPassword_UnprocessableEntity()
        {
            var response = await Client.PostAsync(UserLoginApiUrl("generateToken"), new StringContent(JsonConvert.SerializeObject(new GenerateTokenModel()
            {
                Username = "testUser1",
                Password = "testPassword1"

            }), Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Test]
        public async Task GenerateToken_CorrectLoginAndPassword_Success()
        {
            var response = await Client.PostAsync(UserLoginApiUrl("generateToken"), new StringContent(JsonConvert.SerializeObject(new GenerateTokenModel()
            {
                Username = "testUser",
                Password = "testPassword"

            }), Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


    }
}
