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
            @"Server=(LocalDB)\MSSQLLocalDB;Database=movies-db;Trusted_Connection=True";

        public MoviesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder.UseSqlServer(args.Length > 0 ? args[0] : ConnectionString);

            return new MoviesDbContext(optionsBuilder.Options);
        }
    }
}
