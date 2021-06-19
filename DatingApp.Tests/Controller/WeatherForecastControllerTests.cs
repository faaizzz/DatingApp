using API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DatingApp.Tests.Controller
{
    public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public WeatherForecastControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/WeatherForecast");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsExpectedResponse()
        {
            var response = await _client.GetAsync("");

            var result  = await _client.GetFromJsonAsync<WeatherForecast[]>("");
        }
    }
}
