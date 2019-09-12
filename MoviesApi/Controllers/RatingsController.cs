using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Exceptions;
using MoviesApi.Middleware;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <returns>Creates or updates users movie rating</returns>
        /// <response code="200">Rating saved successfully</response>
        /// <response code="404">Provided Id that doesnt exist</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(HttpStatusCodeException))]
        public ActionResult Post([FromBody] MovieRatingRequest movieRatingRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _ratingService.RateMovie(movieRatingRequest.movieId, movieRatingRequest.userId, movieRatingRequest.rating);
                    return Ok();
                }
                catch (RepositoryException ex)
                {
                    if (ex.Type == RepositoryException.ExeptionType.NotFound)
                        throw new HttpStatusCodeException(404, ex.Message);

                    throw new HttpStatusCodeException(500, "Failed to save movie rating.");
                }
                catch (Exception ex)
                {
                    throw new HttpStatusCodeException(500, "Something went wrong.");
                }
            }
            else
            {
                throw new HttpStatusCodeException(400, "You must provide a valid rating.");
            }
        }
    }
}