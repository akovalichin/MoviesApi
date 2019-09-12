using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoviesApi.DbModels;
using MoviesApi.Exceptions;
using MoviesApi.Repos;
using MoviesApi.Services;
using NUnit.Framework;

namespace MoviesApiTests.UnitTests.Repos
{
    [TestFixture]
    public class MovieRepoTests
    {
        private IMoviesRepo _moviesRepo;
        private Mock<IRoundingService> _roundingService;
        private InMemoryContext _inMemoryContext;

        [SetUp]
        public void Init()
        {
            _roundingService = new Mock<IRoundingService>();
            //this uses the same InMemoryContext as Injected in startup, this is just to avoid duplication
            //obviously in real scanario we would be creating a mock in memory database for test purposes.
            var dbOptions = new DbContextOptionsBuilder<InMemoryContext>()
                        .UseInMemoryDatabase(databaseName: "testDb")
                        .Options;
            _inMemoryContext = new InMemoryContext(dbOptions);
            _moviesRepo = new MoviesRepo(_inMemoryContext, _roundingService.Object);
        }

        [Test]
        public void RateMovie_With_Non_existing_userId_Should_Throw_RepositoryException()
        {
            //Assert
            var ex = Assert.Throws<RepositoryException>(() => _moviesRepo.RateMovie(1, 22, 3));
            Assert.AreEqual(ex.Type, RepositoryException.ExeptionType.NotFound);
        }

        [Test]
        public void RateMovie_Saves_Successfully()
        {
            //Act
            _moviesRepo.RateMovie(4, 3, 4);
            //Assert
            Assert.IsNotNull(_inMemoryContext.Ratings.SingleOrDefault(x => x.MovieId == 4 && x.UserId == 3));
        }

        [Test]
        public void RateMovie_Updates_Successfully()
        {
            //Setup
            _moviesRepo.RateMovie(4, 3, 4);
            //Act
            _moviesRepo.RateMovie(4, 3, 5);
            //Assert
            Assert.IsNotNull(_inMemoryContext.Ratings.SingleOrDefault(x => x.MovieId == 4 && x.UserId == 3 && x.Rating == 5));
        }
    }
}