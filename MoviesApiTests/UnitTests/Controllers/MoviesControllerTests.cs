using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesApi.Controllers;
using MoviesApi.DbModels;
using MoviesApi.Services;
using NUnit.Framework;

namespace MoviesApiTests.UnitTests.Controllers
{
    [TestFixture]
    public class MoviesControllerTests
    {
        private Mock<IMoviesService> _moviesService;
        private MoviesController _moviesController;

        [SetUp]
        public void Init()
        {
            _moviesService = new Mock<IMoviesService>();
            _moviesService.Setup(x => x.GetMoviesByFilter(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(ValidListOfMovies());
            _moviesService.Setup(x => x.GetTopRatedByUser(It.IsAny<int>())).Returns(ValidListOfMovies());
            _moviesService.Setup(x => x.GetTopRated()).Returns(ValidListOfMovies());
            _moviesController = new MoviesController(_moviesService.Object);
        }

        [Test]
        public void GetMoviesByFilter_Should_Call_MoviesService()
        {
            //Act
            var result = _moviesController.GetMoviesByFilter("test", 20, "testGenre");
            //Assert
            _moviesService.Verify(x => x.GetMoviesByFilter(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void GetMoviesByFilter_Should_Return_Movies()
        {
            //Act
            var result = _moviesController.GetMoviesByFilter("test", 20, "testGenre").Result as OkObjectResult;

            //Assert
            Assert.AreEqual(((List<Movie>)result.Value).Count, 2);
        }

        [Test]
        public void GetTopRated_Should_Call_MoviesService()
        {
            //Act
            var result = _moviesController.GetTopRated();

            //Assert
            _moviesService.Verify(x => x.GetTopRated(), Times.Once());
        }

        [Test]
        public void GetTopRated__Should_Return_Movies()
        {
            //Act
            var result = _moviesController.GetTopRated().Result as OkObjectResult;

            //Assert
            Assert.AreEqual(((List<Movie>)result.Value).Count, 2);
        }

        [Test]
        public void GetTopRatedByUser_Should_Call_MoviesService()
        {
            //Act
            var result = _moviesController.GetTopRatedByUserId(2);

            //Assert
            _moviesService.Verify(x => x.GetTopRatedByUser(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetTopRatedByUser__Should_Return_Movies()
        {
            //Act
            var result = _moviesController.GetTopRatedByUserId(2).Result as OkObjectResult;

            //Assert
            Assert.AreEqual(((List<Movie>)result.Value).Count, 2);
        }

        private List<Movie> ValidListOfMovies()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Title = "My Test Movie",
                    YearOfRelease = 2020,
                    Genre = "TestGenre",
                    Id = 35,
                    AverageRating =4,
                    RunningTime = 140
                },
                new Movie
                {
                    Title = "My Test Movie 2",
                    YearOfRelease = 2021,
                    Genre = "TestGenre2",
                    Id = 34,
                    AverageRating =2,
                    RunningTime = 120
                }
            };
        }
    }
}