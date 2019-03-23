using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;

namespace MoviesApi.UnitTests.DbFixtures
{
    public class MoviesEfDatabaseFixture
    {
        private readonly string _connectionString =
            $@"Server=(LocalDB)\MSSQLLocalDB;Database=movies-db-test{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true";

        public MoviesEfDatabaseFixture()
        {
            Context = CreateContext();
            Context.Database.Migrate();
        }

        public MoviesDbContext Context { get; set; }

        public MoviesDbContext CreateContext()
        {
            return new MoviesDbContextFactory().CreateDbContext(new[] {_connectionString});
        }

        public string ConnectionString => _connectionString;

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}
