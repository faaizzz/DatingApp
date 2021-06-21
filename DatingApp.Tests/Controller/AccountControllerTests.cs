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

        [Fact]
        public async Task Verify_Valid_LoginTest()
        {
            var userName = "testuser";
            LoginDto loginDto = new LoginDto {Username = userName, Password= "Test@123" };

            var response = await _client.PostAsJsonAsync("",loginDto);
            var returnValue = await response.Content.ReadFromJsonAsync<UserDto>();

            Assert.Equal(userName, returnValue.Username);
            //Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task Verify_InValid_User_LoginTest()
        {
            var userName = "testuserinvalid";
            LoginDto loginDto = new LoginDto { Username = userName, Password = "Test@123" };

            var response = await _client.PostAsJsonAsync("", loginDto);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Verify_InValid_Password_LoginTest()
        {
            var userName = "testuser";
            LoginDto loginDto = new LoginDto { Username = userName, Password = "Test@1234" };

            var response = await _client.PostAsJsonAsync("", loginDto);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
