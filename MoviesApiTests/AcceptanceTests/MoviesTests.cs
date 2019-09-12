using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using MoviesApi.DbModels;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MoviesApiTests.AcceptanceTests
{
    [TestFixture]
    public class MoviesTests
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
        public void Get_Top_Rated_Movies_Returns_Valid_Result()
        {
            var _response = _client.GetAsync("movies/topRated").Result;

            var responseResult = JsonConvert.DeserializeObject<List<Movie>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Count, 5);
            Assert.AreEqual(responseResult[0].Title, "Rambo");
        }

        [Test]
        public void Get_By_Filter_Movies_Returns_Valid_Result()
        {
            var _response = _client.GetAsync("movies/filter?genre=Drama").Result;

            var responseResult = JsonConvert.DeserializeObject<List<Movie>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Count, 2);
            Assert.AreEqual(responseResult[0].Title, "Rambo 2");
        }

        [Test]
        public void Get_By_Filter_Movies_Returns_400_When_No_Filter()
        {
            var _response = _client.GetAsync("movies/filter").Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
        }

        [Test]
        public void Get_Top_Rated_By_User_Movies_Returns_Valid_Result()
        {
            var _response = _client.GetAsync("movies/topRated/user/2").Result;

            var responseResult = JsonConvert.DeserializeObject<List<Movie>>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            Assert.AreEqual(responseResult.Count, 5);
            Assert.AreEqual(responseResult[0].Title, "Rambo");
        }

        [Test]
        public void Get_Top_Rated_By_User_Movies_Returns_400_When_Not_Valid_UserId()
        {
            var _response = _client.GetAsync("movies/topRated/user/second").Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
        }

        [TearDown]
        public void DisposeTest()
        {
            if (_client != null) _client.Dispose();
        }
    }
}