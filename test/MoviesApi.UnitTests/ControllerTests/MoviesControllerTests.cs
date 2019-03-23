using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesApi.Api.Controllers;
using MoviesApi.Common.Interface;
using MoviesApi.Common.Models;
using Xunit;

namespace MoviesApi.UnitTests.ControllerTests
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieAccess> _mockMovieAccess = new Mock<IMovieAccess>();
        private readonly Mock<IRatingAccess> _mockRatingAccess = new Mock<IRatingAccess>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void GetReturns400WithNoCriteriaSet()
        {
            MoviesController sut = CreateSystemUnderTest();

            var result = sut.Get(null, null, Genre.None);

            _mockMovieAccess.Verify(a => a.GetMovies(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<Genre>()), Times.Never);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetReturns404WhenNoMoviesFound()
        {
            _mockMovieAccess.Setup(a => a.GetMovies(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<Genre>()))
                .Returns(new List<Movie>());

            MoviesController sut = CreateSystemUnderTest();

            var result = sut.Get("test", null, Genre.None);

            _mockMovieAccess.Verify(a => a.GetMovies("test", null, Genre.None), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetReturns200WhenMoviesFound()
        {
            List<Movie> movies = _fixture.CreateMany<Movie>(10).ToList();

            _mockMovieAccess.Setup(a => a.GetMovies(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<Genre>()))
                .Returns(movies);

            MoviesController sut = CreateSystemUnderTest();

            var result = sut.Get("test", null, Genre.None);

            _mockMovieAccess.Verify(a => a.GetMovies("test", null, Genre.None), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(10, ((IReadOnlyCollection<Movie>)((OkObjectResult)result).Value).Count);
        }

        [Fact]
        public void GetTop5Returns404WhenNoMoviesFound()
        {
            _mockMovieAccess.Setup(a => a.GetMoviesByRating(5))
                .Returns(new List<Movie>());

            MoviesController sut = CreateSystemUnderTest();

            var result = sut.GetTopFive();

            _mockMovieAccess.Verify(a => a.GetMoviesByRating(5), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetTop5Returns200WhenMoviesFound()
        {
            List<Movie> movies = _fixture.CreateMany<Movie>(5).ToList();

            _mockMovieAccess.Setup(a => a.GetMoviesByRating(5))
                .Returns(movies);

            MoviesController sut = CreateSystemUnderTest();

            var result = sut.GetTopFive();

            _mockMovieAccess.Verify(a => a.GetMoviesByRating(5), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(5, ((IReadOnlyCollection<Movie>)((OkObjectResult)result).Value).Count);
        }

        [Fact]
        public void GetTop5ByUserReturns404WhenNoMoviesFound()
        {
            Guid userId = Guid.NewGuid();
            _mockMovieAccess.Setup(a => a.GetMoviesByUserRating(userId, 5))
                .Returns(new List<Movie>());

            MoviesController sut = CreateSystemUnderTest();

            var result = sut.GetTopFiveByUser(userId);

            _mockMovieAccess.Verify(a => a.GetMoviesByUserRating(userId, 5), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetTop5ByUserReturns200WhenMoviesFound()
        {
            List<Movie> movies = _fixture.CreateMany<Movie>(5).ToList();
            Guid userId = Guid.NewGuid();
            _mockMovieAccess.Setup(a => a.GetMoviesByUserRating(userId, 5))
                .Returns(movies);

            MoviesController sut = CreateSystemUnderTest();

            var result = sut.GetTopFiveByUser(userId);

            _mockMovieAccess.Verify(a => a.GetMoviesByUserRating(userId, 5), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(5, ((IReadOnlyCollection<Movie>)((OkObjectResult)result).Value).Count);
        }

        [Fact]
        public async Task UpsertRatingReturns200WithValidValues()
        {
            Rating rating = new Rating {MovieId = Guid.NewGuid(), UserId = Guid.NewGuid(), Score = 10};

            MoviesController sut = CreateSystemUnderTest();

            var result = await sut.UpsertRatingAsync(rating);

            _mockRatingAccess.Verify(
                a => a.UpsertRatingAsync(rating.UserId, rating.MovieId, rating.Score, It.IsAny<CancellationToken>()),
                Times.Once);
            Assert.IsType<OkResult>(result);
        }

        private MoviesController CreateSystemUnderTest()
        {
            return new MoviesController(_mockMovieAccess.Object, _mockRatingAccess.Object);
        }
    }
}
