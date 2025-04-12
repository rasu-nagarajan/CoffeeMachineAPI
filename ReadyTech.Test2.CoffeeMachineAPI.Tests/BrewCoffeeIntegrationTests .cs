using System.Net;
using System.Text.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using ReadyTech.Test2.CoffeeMachineAPI.Constants;

namespace ReadyTech.Test2.CoffeeMachineAPI.IntegrationTests
{
    public class BrewCoffeeIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BrewCoffeeIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task BrewCoffeeEndpoint_EveryFifthCallReturns503()
        {
            int totalCalls = 14;
            for (int i = 1; i <= totalCalls; i++)
            {
                var response = await _client.GetAsync("/brew-coffee");
                if (i % 5 == 0)
                {
                    Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
                    var body = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    var json = await response.Content.ReadAsStringAsync();
                    var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    Assert.NotNull(dict);
                    Assert.Equal(CoffeeMessages.HotCoffee, dict["message"]);
                }
            }
        }
    }
}
