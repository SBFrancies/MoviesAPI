using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoviesApi.Common.Exceptions;
using MoviesApi.Common.Interface;
using MoviesApi.Data;
using MoviesApi.Data.Entities;

namespace MoviesApi.Access
{
    public class RatingAccess : IRatingAccess
    {
        private readonly Func<MoviesDbContext> _dbContextFactory;

        public RatingAccess(Func<MoviesDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task UpsertRatingAsync(Guid userId, Guid movieId, decimal rating, CancellationToken cancellationToken = default)
        {
            using (MoviesDbContext context = _dbContextFactory())
            {
                UserEntity user = context.Users.SingleOrDefault(a => a.Id == userId);

                if (user == null)
                {
                    throw new UserNotFoundException($"User with ID {userId} not found");
                }

                MovieEntity movie = context.Movies.SingleOrDefault(a => a.Id == movieId);

                if (movie == null)
                {
                    throw new MovieNotFoundException($"Movie with ID {movieId} not found");
                }

                RatingEntity entity =
                    context.Ratings.SingleOrDefault(a => a.UserId == userId && a.MovieId == movieId) ??
                    new RatingEntity
                    {
                        User = user,
                        Movie = movie,
                    };

                entity.Rating = rating;
                entity.DateSet = DateTimeOffset.UtcNow;

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
