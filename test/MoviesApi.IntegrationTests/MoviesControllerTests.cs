using MoviesAPI.API;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MoviesApi.Common.Models;
using Newtonsoft.Json;
using Xunit;

namespace MoviesApi.IntegrationTests
{
    public class MoviesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MoviesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/v1/movies?title=titanic")]
        [InlineData("/v1/movies/topFive")]
        [InlineData("/v1/movies/022B41A2-7E40-4104-98A9-7CD8DCC67A0F/topFive")]
        public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetReturns400WhenNoCriteriaSet()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/v1/movies/");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetReturns404WhenNoMoviesFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/v1/movies?title=not-a-movie");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetUserTopFiveReturns404WhenNoMoviesFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/v1/movies/022B41A2-7E40-4104-98A9-7CD8DCC67A0E/topFive");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpsertReturnsSuccessStatusCodeWhenValuesAreValid()
        {
            var client = _factory.CreateClient();

            Rating rating = new Rating
            {
               UserId = Guid.Parse("022B41A2-7E40-4104-98A9-7CD8DCC67A0F"),
               MovieId = Guid.Parse("1ABAADAD-A1EE-403D-A17F-31321EC5859F"),
               Score =  9,
            };

            var response = await client.PutAsync("/v1/movies/",
                new StringContent(JsonConvert.SerializeObject(rating), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpsertReturns404WhenMovieNotFound()
        {
            var client = _factory.CreateClient();

            Rating rating = new Rating
            {
                UserId = Guid.Parse("022B41A2-7E40-4104-98A9-7CD8DCC67A0F"),
                MovieId = Guid.Parse("1ABAADAD-A1EE-403D-A17F-31321EC5859E"),
                Score = 9,
            };

            var response = await client.PutAsync("/v1/movies/",
                new StringContent(JsonConvert.SerializeObject(rating), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpsertReturns404WhenUserNotFound()
        {
            var client = _factory.CreateClient();

            Rating rating = new Rating
            {
                UserId = Guid.Parse("022B41A2-7E40-4104-98A9-7CD8DCC67A0E"),
                MovieId = Guid.Parse("1ABAADAD-A1EE-403D-A17F-31321EC5859F"),
                Score = 9,
            };

            var response = await client.PutAsync("/v1/movies/",
                new StringContent(JsonConvert.SerializeObject(rating), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        [InlineData(int.MaxValue)]
        public async Task UpsertReturns400WhenModelIsInvalid(int score)
        {
            var client = _factory.CreateClient();

            Rating rating = new Rating
            {
                UserId = Guid.Parse("022B41A2-7E40-4104-98A9-7CD8DCC67A0F"),
                MovieId = Guid.Parse("1ABAADAD-A1EE-403D-A17F-31321EC5859F"),
                Score = score,
            };

            var response = await client.PutAsync("/v1/movies/",
                new StringContent(JsonConvert.SerializeObject(rating), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
