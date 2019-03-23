using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoviesApi.Data
{
    public class MoviesDbContextFactory : IDesignTimeDbContextFactory<MoviesDbContext>
    {
        private const string ConnectionString =
            @"Server=(LocalDB)\MSSQLLocalDB;Database=organisations;Trusted_Connection=True";

        public MoviesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            return new MoviesDbContext(optionsBuilder.Options);
        }
    }
}
