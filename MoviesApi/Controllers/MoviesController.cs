using System;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Middleware;
using MoviesApi.DbModels;
using MoviesApi.Services;
using System.Collections.Generic;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        /// <returns>Returns lsit of moveis based on filter</returns>
        /// <response code="400">No filters provided</response>
        /// <response code="404">No results. Nothing found</response>
        [HttpGet("filter")]
        [ProducesResponseType(200, Type = typeof(List<Movie>))]
        [ProducesResponseType(400, Type = typeof(HttpStatusCodeException))]
        [ProducesResponseType(404, Type = typeof(HttpStatusCodeException))]
        public ActionResult<List<Movie>> GetMoviesByFilter([FromQuery]string title = null, int? year = null, string genre = null)
        {
            if (string.IsNullOrEmpty(title) && year == null && string.IsNullOrEmpty(genre))
                throw new HttpStatusCodeException(400, "You must provide at least one filter to search by.");

            var result = _moviesService.GetMoviesByFilter(title, year, genre);

            if (result.Count == 0)
                throw new HttpStatusCodeException(404, "No moveis found using your provided filter.");

            return Ok(result);
        }

        [HttpGet("topRated")]
        public ActionResult<List<Movie>> GetTopRated()
        {
            return Ok(_moviesService.GetTopRated());
        }

        [HttpGet("topRated/user/{id}")]
        public ActionResult<List<Movie>> GetTopRatedByUserId([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(_moviesService.GetTopRatedByUser(id));
            }
            else
            {
                throw new HttpStatusCodeException(400, "You must provide a valid user Id.");
            }
        }
    }
}