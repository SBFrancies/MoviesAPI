using MoviesApi.Common.Interface;
using MoviesApi.Common.Models;
using MoviesApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Common.Extensions;
using MoviesApi.Data.Entities;

namespace MoviesApi.Access
{
    public class MovieAccess : IMovieAccess
    {
        private readonly Func<MoviesDbContext> _dbContextFactory;

        public MovieAccess(Func<MoviesDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public IReadOnlyCollection<Movie> GetMovies(string title, int? year, Genre genres)
        {
            using (MoviesDbContext context = _dbContextFactory())
            {
                IQueryable<MovieEntity> movies = context.Movies.Include(a => a.Ratings).AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    movies = movies.Where(a => a.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase));
                }

                if (year != null)
                {
                    movies = movies.Where(a => a.YearOfRelease == year);
                }
                
                if (genres != Genre.None)
                {
                    movies = movies.Where(a => a.Genre.HasFlag(genres));
                }

                return movies.Select(a => PopulateMovieFromEntity(a)).ToList();
            }
        }

        public IReadOnlyCollection<Movie> GetMoviesByRating(int take)
        {
            using (MoviesDbContext context = _dbContextFactory())
            {
                return context.Movies
                    .Include(a => a.Ratings)
                    .OrderByDescending(a => a.Ratings.Average(b => b.Rating))
                    .ThenBy(a => a.Title)
                    .Take(take)
                    .Select(a => PopulateMovieFromEntity(a))
                    .ToList();
            }
        }

        public IReadOnlyCollection<Movie> GetMoviesByUserRating(Guid userId, int take)
        {
            using (MoviesDbContext context = _dbContextFactory())
            {
                return context.Ratings
                    .Include(a => a.Movie)
                    .Where(a => a.User.Id == userId)
                    .OrderByDescending(a => a.Rating)
                    .ThenBy(a => a.Movie.Title)
                    .Take(take)
                    .Select(a => PopulateMovieFromEntity(a.Movie))
                    .ToList();
            }
        }

        private Movie PopulateMovieFromEntity(MovieEntity entity)
        {
            return new Movie
            {
                Id = entity.Id,
                Genre = entity.Genre,
                RunningTime = entity.RunningTime,
                Title = entity.Title,
                YearOfRelease = entity.YearOfRelease,
                AverageRating = entity.Ratings == null || !entity.Ratings.Any() 
                ? null 
                : (decimal?)entity.Ratings.Average(a => a.Rating).RoundToNearestPointFive(),
            };
        }
    }
}
