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
            });
        }

        private MovieEntity[] MovieSeedData()
        {
            return new MovieEntity[]
            {
                new MovieEntity{Id=Guid.NewGuid(), Title = "Titanic", RunningTime = 200, Genre = Genre.Historical | Genre.Drama, YearOfRelease = 2000},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Die Hard", RunningTime = 120, Genre = Genre.Action, YearOfRelease = 1999},
                new MovieEntity{Id=Guid.NewGuid(), Title = "The Lord of the Rings", RunningTime = 631, Genre = Genre.Fantasy, YearOfRelease = 2007},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Get Out", RunningTime = 110, Genre = Genre.Horror | Genre.Comedy, YearOfRelease = 2018},
                new MovieEntity{Id=Guid.NewGuid(), Title = "27 Dresses", RunningTime = 98, Genre = Genre.Romance | Genre.Comedy, YearOfRelease = 2011},
                new MovieEntity{Id=Guid.NewGuid(), Title = "The Dutchess", RunningTime = 127, Genre = Genre.Historical, YearOfRelease = 2006},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Casino Royale", RunningTime = 163, Genre = Genre.Action | Genre.Thriller, YearOfRelease = 2004},
                new MovieEntity{Id=Guid.NewGuid(), Title = "The Simpsons Movie", RunningTime = 93, Genre = Genre.Animated | Genre.Comedy, YearOfRelease = 2009},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Iron Man", RunningTime = 118, Genre = Genre.Action | Genre.Comedy, YearOfRelease = 2007},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Iron Man 2", RunningTime = 145, Genre = Genre.Action | Genre.Comedy, YearOfRelease = 2010},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Iron Man 3", RunningTime = 123, Genre = Genre.Action | Genre.Comedy, YearOfRelease = 2013},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Batman", RunningTime = 120, Genre = Genre.Action | Genre.Drama, YearOfRelease = 1989},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Saw", RunningTime = 111, Genre = Genre.Horror, YearOfRelease = 2003},
                new MovieEntity{Id=Guid.NewGuid(), Title = "Man on Wire", RunningTime = 87, Genre = Genre.Documentary, YearOfRelease = 2010},
            };
        }

        private UserEntity[] UserSeedData()
        {
            return new UserEntity[]
            {
                new UserEntity{ Id= Guid.NewGuid(), FirstName = "Tom", LastName = "Smith", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-18)},
                new UserEntity{ Id= Guid.NewGuid(), FirstName = "Jamal", LastName = "Jones", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-72)},
                new UserEntity{ Id= Guid.NewGuid(), FirstName = "John", LastName = "Robertson", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-28)},
                new UserEntity{ Id= Guid.NewGuid(), FirstName = "Leon", LastName = "Smith", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-46)},
                new UserEntity{ Id= Guid.NewGuid(), FirstName = "Katie", LastName = "McDonald", DateOfBirth = DateTimeOffset.UtcNow.AddYears(-32)},
            };
        }
    }
}
