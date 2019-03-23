using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using MoviesApi.Access;
using MoviesApi.Common.Models;
using MoviesApi.UnitTests.DbFixtures;
using Xunit;
using System.Linq;

namespace MoviesApi.UnitTests.AccessTests
{
    public class MovieAccessTests : MoviesFixtureContext, IClassFixture<MoviesEfDatabaseFixture>
    {
        [Fact]
        public void CanReturnListOfFilms()
        {
            MovieAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Movie> movies = sut.GetMovies(null, null, Genre.None);

            Assert.Equal(14, movies.Count);
        }

        [Fact]
        public void CanReturnListOfFilmsFilteredByPartialTitle()
        {
            MovieAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Movie> movies = sut.GetMovies("Iron Man", null, Genre.None);

            Assert.Equal(3, movies.Count);
        }

        [Fact]
        public void CanReturnListOfFilmsFilteredByYearOfRelease()
        {
            MovieAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Movie> movies = sut.GetMovies(null, 2018, Genre.None);

            Assert.Single(movies);
        }

        [Fact]
        public void CanReturnListOfFilmsFilteredByGenre()
        {
            MovieAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Movie> movies = sut.GetMovies(null, null, Genre.Horror);

            Assert.Equal(2, movies.Count);
        }

        [Fact]
        public void CanReturnListOfFilmsFilteredByMultipleGenres()
        {
            MovieAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Movie> movies = sut.GetMovies(null, null, Genre.Action | Genre.Drama);

            Assert.Single(movies);
        }

        [Fact]
        public void CanReturnTop5Films()
        {
            MovieAccess sut = CreateSystemUnderTest();

            IReadOnlyCollection<Movie> allMovies = sut.GetMovies(null, null, Genre.None)
                .OrderByDescending(a => a.AverageRating)
                .ThenBy(a => a.Title)
                .ToList();

            IReadOnlyCollection<Movie> topFive = sut.GetMoviesByRating(5);

            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(allMovies.ElementAt(i).Id, topFive.ElementAt(i).Id);
            }          
        }

        private MovieAccess CreateSystemUnderTest()
        {
            return new MovieAccess(CreateContext);
        }
    }
}
