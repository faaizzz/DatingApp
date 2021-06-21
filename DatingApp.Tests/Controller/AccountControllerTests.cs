using API;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DatingApp.Tests.Controller
{
    public class AccountControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AccountControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/api/account/login");
            _client = factory.CreateClient();
        }

        [Theory]
        [Trait("User", "Login_Successful")]
        [InlineData("testuser", "Test@123")]
        public async Task Verify_Valid_LoginTest(string userName, string password)
        {
            LoginDto loginDto = new LoginDto {Username = userName, Password= password };

            var response = await _client.PostAsJsonAsync("",loginDto);
            var returnValue = await response.Content.ReadFromJsonAsync<UserDto>();

            Assert.Equal(userName, returnValue.Username);
            //Assert.Equal(5, result.Count());
        }

        [Theory]
        [Trait("User", "Login_Successful")]
        [InlineData("testuserinvalid", "Test@123")]
        [InlineData("testuserinvalid2", "Test@123")]
        public async Task Verify_InValid_User_LoginTest(string userName, string password)
        {
            LoginDto loginDto = new LoginDto { Username = userName, Password = password };

            var response = await _client.PostAsJsonAsync("", loginDto);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [Trait("User", "Login_Successful")]
        [InlineData("testuser", "Test@1234")]
        [InlineData("testuser", "Test@12345")]
        public async Task Verify_InValid_Password_LoginTest(string userName, string password)
        {
            LoginDto loginDto = new LoginDto { Username = userName, Password = password  };

            var response = await _client.PostAsJsonAsync("", loginDto);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
