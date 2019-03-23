using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using MoviesApi.Data;
using MoviesApi.Data.Entities;
using Xunit.Abstractions;

namespace MoviesApi.UnitTests.DbFixtures
{
    public abstract class MoviesFixtureContext : FixtureContext, IDisposable
    {
        private MoviesEfDatabaseFixture _moviesEfDatabaseFixture;
        private readonly Fixture _fixture = new Fixture();

        public MoviesDbContext Context => _moviesEfDatabaseFixture?.Context ?? CreateContext();

        public MoviesDbContext CreateContext()
        {
           _moviesEfDatabaseFixture = new MoviesEfDatabaseFixture();
            return _moviesEfDatabaseFixture.CreateContext();
        }

        public MovieEntity CreateMovieEntity(string title, Guid id)
        {
            return _fixture.Build<MovieEntity>()
                .With(a => a.Id, id)
                .With(a => a.Title, title)
                .Without(a => a.Ratings)
                .Create();
        }

        public RatingEntity CreateRatingEntity()
        {
            return _fixture.Create<RatingEntity>();
        }

        public UserEntity CreateUserEntity(Guid userId)
        {
            UserEntity entity = _fixture.Build<UserEntity>()
                .With(a => a.Id, userId)
                .Create();

            return entity;
        }

        public void Dispose()
        {
            _moviesEfDatabaseFixture.Dispose();
        }
    }
}
