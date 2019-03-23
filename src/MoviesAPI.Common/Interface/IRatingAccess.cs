using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApi.Common.Interface
{
    /// <summary>
    /// Allows access to rating entities
    /// </summary>
    public interface IRatingAccess
    {
        /// <summary>
        /// Adds or updates a users rating for a movie
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="movieId">The movie ID</param>
        /// <param name="rating">The rating assigned to the movie by the user</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A <see cref="Task"/></returns>
        Task UpsertRatingAsync(Guid userId, Guid movieId, decimal rating, CancellationToken cancellationToken = default);
    }
}
