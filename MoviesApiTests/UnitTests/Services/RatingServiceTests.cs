using NUnit.Framework;
using MoviesApi.Services;
using MoviesApi.Repos;
using Moq;

namespace MoviesApiTests.UnitTests.Services
{
    [TestFixture]
    public class RatingServiceTests
    {
        private IRatingService _ratingService;
        private Mock<IMoviesRepo> _moviesRepo;

        [SetUp]
        public void Init()
        {
            _moviesRepo = new Mock<IMoviesRepo>();
            _moviesRepo.Setup(x => x.RateMovie(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
            _ratingService = new RatingService(_moviesRepo.Object);
        }

        [Test]
        public void RateService_Should_Call_MoviesRepo()
        {
            //Act
            _ratingService.RateMovie(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            //Assert
            _moviesRepo.Verify(x => x.RateMovie(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }
    }
}