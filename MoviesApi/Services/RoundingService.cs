using System;

namespace MoviesApi.Services
{
    public class RoundingService : IRoundingService
    {
        public RoundingService()
        {
        }

        public double RoundToNearestHalf(double value)
        {
            return Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;
        }
    }
}