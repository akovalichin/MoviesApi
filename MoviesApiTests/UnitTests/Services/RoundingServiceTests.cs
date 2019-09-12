using MoviesApi.Services;
using NUnit.Framework;

namespace MoviesApiTests.UnitTests.Services
{
    [TestFixture]
    public class RoundingServiceTests
    {
        private IRoundingService _roundingService;

        [SetUp]
        public void Init()
        {
            _roundingService = new RoundingService();
        }

        [TestCase(2.91, 3.00)]
        [TestCase(3.249, 3.00)]
        [TestCase(3.25, 3.50)]
        [TestCase(3.6, 3.50)]
        [TestCase(3.75, 4.00)]
        [Test]
        public void DoubleRounding(double input, double output)
        {
            var result = _roundingService.RoundToNearestHalf(input);
            Assert.AreEqual(result, output);
        }
    }
}