using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MoviesApi.Common.Models;
using MoviesApi.Data.Entities;

namespace MoviesApi.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        {
        }

        public DbSet<MovieEntity> Movies { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RatingEntity> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Genre).HasColumnType("int");

                entity.HasData(MovieSeedData());
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasData(UserSeedData());
            });

            modelBuilder.Entity<RatingEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(e => e.Movie)
                    .WithMany(e => e.Ratings)
                    .HasForeignKey(e => e.MovieId)
                    .HasConstraintName("FK_Movies_Ratings");

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Ratings)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_Users_Ratings");

                entity.HasData(RatingSeedData());
            });
        }

        private readonly Guid[] _movieIds = 
        {
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            Guid.NewGuid(), Guid.NewGuid(),
        };

        private readonly Guid[] _userIds =
        {
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            Guid.NewGuid(), Guid.NewGuid(),
        };

        private MovieEntity[] MovieSeedData()
        {
            return new []
            {
                new MovieEntity{Id=_movieIds[0], Title = "Titanic", RunningTime = 200, Genre = Genre.Historical | Genre.Drama, YearOfRelease = 2000},
                new MovieEntity{Id=_movieIds[1], Title = "Die Hard", RunningTime = 120, Genre = Genre.Action, YearOfRelease = 1999},
                new MovieEntity{Id=_movieIds[2], Title = "The Lord of the Rings", RunningTime = 631, Genre = Genre.Fantasy, YearOfRelease = 2007},
                new MovieEntity{Id=_movieIds[3], Title = "Get Out", RunningTime = 110, Genre = Genre.Horror | Genre.Comedy, YearOfRelease = 2018},
                new MovieEntity{Id=_movieIds[4], Title = "27 Dresses", RunningTime = 98, Genre = Genre.Romance | Genre.Comedy, YearOfRelease = 2011},
                new MovieEntity{Id=_movieIds[5], Title = "The Dutchess", RunningTime = 127, Genre = Genre.Historical, YearOfRelease = 2006},
                new MovieEntity{Id=_movieIds[6], Title = "Casino Royale", RunningTime = 163, Genre = Genre.Action | Genre.Thriller, YearOfRelease = 2004},
                new MovieEntity{Id=_movieIds[7], Title = "The Simpsons Movie", RunningTime = 93, Genre = Genre.Animated | Genre.Comedy, YearOfRelease = 2009},
                new MovieEntity{Id=_movieIds[8], Title = "Iron Man", RunningTime = 118, Genre = Genre.Action | Genre.Comedy, YearOfRelease = 2007},
                new MovieEntity{Id=_movieIds[9], Title = "Iron Man 2", RunningTime = 145, Genre = Genre.Action | Genre.Comedy, YearOfRelease = 2010},
                new MovieEntity{Id=_movieIds[10], Title = "Iron Man 3", RunningTime = 123, Genre = Genre.Action | Genre.Comedy, YearOfRelease = 2013},
                new MovieEntity{Id=_movieIds[11], Title = "Batman", RunningTime = 120, Genre = Genre.Action | Genre.Drama, YearOfRelease = 1989},
                new MovieEntity{Id=_movieIds[12], Title = "Saw", RunningTime = 111, Genre = Genre.Horror, YearOfRelease = 2003},
                new MovieEntity{Id=_movieIds[13], Title = "Man on Wire", RunningTime = 87, Genre = Genre.Documentary, YearOfRelease = 2010},
            };
        }

        private UserEntity[] UserSeedData()
        {
            return new []
            {
                new UserEntity{ Id= _userIds[0], FirstName = "Tom", LastName = "Smith", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-18)},
                new UserEntity{ Id= _userIds[1], FirstName = "Jamal", LastName = "Jones", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-72)},
                new UserEntity{ Id= _userIds[2], FirstName = "John", LastName = "Robertson", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-28)},
                new UserEntity{ Id= _userIds[3], FirstName = "Leon", LastName = "Smith", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-46)},
                new UserEntity{ Id= _userIds[4], FirstName = "Katie", LastName = "McDonald", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-32)},
            };
        }

        private RatingEntity[] RatingSeedData()
        {
            return new[]
            {
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[0], MovieId = _movieIds[0], DateSet = DateTimeOffset.UtcNow, Rating = 5},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[0], MovieId = _movieIds[1], DateSet = DateTimeOffset.UtcNow, Rating = 9},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[0], MovieId = _movieIds[2], DateSet = DateTimeOffset.UtcNow, Rating = 5.5m},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[0], MovieId = _movieIds[3], DateSet = DateTimeOffset.UtcNow, Rating = 2.5m},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[0], MovieId = _movieIds[4], DateSet = DateTimeOffset.UtcNow, Rating = 8},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[1], MovieId = _movieIds[5], DateSet = DateTimeOffset.UtcNow, Rating = 1},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[1], MovieId = _movieIds[6], DateSet = DateTimeOffset.UtcNow, Rating = 6},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[1], MovieId = _movieIds[7], DateSet = DateTimeOffset.UtcNow, Rating = 3},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[1], MovieId = _movieIds[8], DateSet = DateTimeOffset.UtcNow, Rating = 9},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[1], MovieId = _movieIds[9], DateSet = DateTimeOffset.UtcNow, Rating = 5.5m},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[2], MovieId = _movieIds[10], DateSet = DateTimeOffset.UtcNow, Rating = 8.5m},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[2], MovieId = _movieIds[11], DateSet = DateTimeOffset.UtcNow, Rating = 6},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[2], MovieId = _movieIds[12], DateSet = DateTimeOffset.UtcNow, Rating = 7},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[2], MovieId = _movieIds[13], DateSet = DateTimeOffset.UtcNow, Rating = 8},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[2], MovieId = _movieIds[0], DateSet = DateTimeOffset.UtcNow, Rating = 9},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[3], MovieId = _movieIds[1], DateSet = DateTimeOffset.UtcNow, Rating = 10},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[3], MovieId = _movieIds[2], DateSet = DateTimeOffset.UtcNow, Rating = 2},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[3], MovieId = _movieIds[3], DateSet = DateTimeOffset.UtcNow, Rating = 3},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[3], MovieId = _movieIds[4], DateSet = DateTimeOffset.UtcNow, Rating = 4.5m},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[3], MovieId = _movieIds[5], DateSet = DateTimeOffset.UtcNow, Rating = 9.5m},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[4], MovieId = _movieIds[6], DateSet = DateTimeOffset.UtcNow, Rating = 2},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[4], MovieId = _movieIds[7], DateSet = DateTimeOffset.UtcNow, Rating = 7},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[4], MovieId = _movieIds[8], DateSet = DateTimeOffset.UtcNow, Rating = 7.5m},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[4], MovieId = _movieIds[9], DateSet = DateTimeOffset.UtcNow, Rating = 5},
                new RatingEntity{Id=Guid.NewGuid(), UserId = _userIds[4], MovieId = _movieIds[10], DateSet = DateTimeOffset.UtcNow, Rating = 6},
            };
        }
    }
}
