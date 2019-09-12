using NUnit.Framework;
using MoviesApi.Services;
using MoviesApi.Repos;
using Moq;
using MoviesApi.DbModels;
using System.Collections.Generic;

namespace MoviesApiTests.UnitTests.Services
{
    [TestFixture]
    public class MovieServiceTests
    {
        private IMoviesService _moviesService;
        private Mock<IMoviesRepo> _moviesRepo;

        [SetUp]
        public void Init()
        {
            _moviesRepo = new Mock<IMoviesRepo>();
            _moviesRepo.Setup(x => x.GetMoviesByFilter(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(ValidListOfMovies());
            _moviesRepo.Setup(x => x.GetTopRatedByUser(It.IsAny<int>())).Returns(ValidListOfMovies());
            _moviesRepo.Setup(x => x.GetTopRated()).Returns(ValidListOfMovies());
            _moviesService = new MoviesService(_moviesRepo.Object);
        }

        [Test]
        public void GetMoviesByFilter_Should_Call_MoviesRepo()
        {
            //Act
            _moviesService.GetMoviesByFilter(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>());
            //Assert
            _moviesRepo.Verify(x => x.GetMoviesByFilter(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void GetTopRated_Should_Call_MoviesRepo()
        {
            //Act
            _moviesService.GetTopRated();
            //Assert
            _moviesRepo.Verify(x => x.GetTopRated(), Times.Once());
        }

        [Test]
        public void GetTopRatedByUser_Should_Call_MoviesRepo()
        {
            //Act
            _moviesService.GetTopRatedByUser(It.IsAny<int>());
            //Assert
            _moviesRepo.Verify(x => x.GetTopRatedByUser(It.IsAny<int>()), Times.Once());
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