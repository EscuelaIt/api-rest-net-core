using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace BeerApi.Integration.Tests
{
    public class BeerControllerTests : 
        IClassFixture<WebApplicationFactory<BeerApi.Startup>>
    {

        private readonly WebApplicationFactory<BeerApi.Startup> _factory;
        public BeerControllerTests(WebApplicationFactory<BeerApi.Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task FindByIdShouldReturnTheBeerWithId()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/beers/2");
            var json =  await response.Content.ReadAsStringAsync();

            var beer = JsonConvert.DeserializeObject<Beer>(json);
            Assert.Equal(2, beer.Id);

        }

        [Fact]
        public async Task FindByIdShouldReturnNotFoundIfBeerDoNotExists()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/beers/1000");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
