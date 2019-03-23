using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoviesApi.Common.Models;

namespace MoviesApi.Common.Interface
{
    /// <summary>
    /// Allows access to movies/>
    /// </summary>
    public interface IMovieAccess
    {
        /// <summary>
        /// Gets a collection of movies
        /// </summary>
        /// <param name="title">The title or partial title of the movie</param>
        /// <param name="year">The year of release of the movie</param>
        /// <param name="genres">The genre(s) of the movie</param>
        /// <returns>A collection of movies</returns>
        IReadOnlyCollection<Movie> GetMovies(string title, int? year, Genre genres);

        /// <summary>
        /// Gets a list of the top movings by rating
        /// </summary>
        /// <param name="take">How many movies to return</param>
        /// <returns>A collection of movies</returns>
        IReadOnlyCollection<Movie> GetMoviesByRating(int take);

        /// <summary>
        /// Gets a list of the movies top rated by a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="take">How many movies to return</param>
        /// <returns>A collection of movies</returns>
        IReadOnlyCollection<Movie> GetMoviesByUserRating(Guid userId, int take);
    }
}
