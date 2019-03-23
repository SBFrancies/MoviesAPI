using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Api.Filters;
using MoviesApi.Common.Exceptions;
using MoviesApi.Common.Interface;
using MoviesApi.Common.Models;

namespace MoviesApi.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [GeneralExceptionFilter]
    [Route("v1/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieAccess _movieAccess;
        private readonly IRatingAccess _ratingAccess;

        public MoviesController(IMovieAccess moviesAccess, IRatingAccess ratingAccess)
        {
            _movieAccess = moviesAccess ?? throw new ArgumentNullException(nameof(moviesAccess));
            _ratingAccess = ratingAccess ?? throw new ArgumentNullException(nameof(ratingAccess));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<Movie>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get([FromQuery]string title, [FromQuery]int? year, [FromQuery]Genre genres)
        {
            if (!ValidateParamters(title, year, genres))
            {
                return BadRequest();
            }

            var results = _movieAccess.GetMovies(title, year, genres);

            if (!results.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet]
        [Route("topFive")]
        [ProducesResponseType(typeof(IReadOnlyCollection<Movie>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetTopFive()
        {
            var results = _movieAccess.GetMoviesByRating(5);

            return Ok(results);
        }

        [HttpGet]
        [Route("{userId}/topFive")]
        [ProducesResponseType(typeof(IReadOnlyCollection<Movie>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetTopFiveByUser([FromRoute]Guid userId)
        {
            var results = _movieAccess.GetMoviesByUserRating(userId, 5);

            return Ok(results);
        }

        [HttpPut]
        [ModelValidationFilter]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpsertRatingAsync([FromBody]Rating rating, CancellationToken cancellationToken = default)
        {
            try
            {
                await _ratingAccess.UpsertRatingAsync(rating.UserId, rating.MovieId, rating.Score, cancellationToken);
                return Ok();
            }

            catch (Exception exception) when (exception is UserNotFoundException || exception is MovieNotFoundException)
            {
                return NotFound();
            }
        }

        private bool ValidateParamters(string title, int? year, Genre genres)
        {
            if (string.IsNullOrEmpty(title) && year == null && genres == Genre.None)
            {
                return false;
            }

            return true;
        }
    }
}