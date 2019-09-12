using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MoviesApi.DbModels;
using MoviesApi.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MoviesApiTests.AcceptanceTests
{
    [TestFixture]
    public class RatingTests
    {
        private HttpClient _client;
        private const string ApiUrl = "https://localhost:5001/api/";

        [SetUp]
        public void Init()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiUrl)
            };
        }

        [Test]
        public void Post_Valid_Rating_Returns_200()
        {
            var ratingRequest = new MovieRatingRequest
            {
                userId = 2,
                movieId = 2,
                rating = 3
            };
            var _response = PostObj("ratings", ratingRequest);

            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Test]
        public void Post_InValid_Rating_Returns_404()
        {
            var ratingRequest = new MovieRatingRequest
            {
                userId = 45,
                movieId = 2,
                rating = 3
            };
            var _response = PostObj("ratings", ratingRequest);

            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }

        private HttpResponseMessage PostObj(string uri, MovieRatingRequest movieRatingRequest)
        {
            var myContent = JsonConvert.SerializeObject(movieRatingRequest);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            return _client.PostAsync(uri, stringContent).Result;
        }

        [TearDown]
        public void DisposeTest()
        {
            if (_client != null) _client.Dispose();
        }
    }
}