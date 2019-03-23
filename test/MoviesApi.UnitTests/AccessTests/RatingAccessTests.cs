using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using MoviesApi.Access;
using MoviesApi.Common.Models;
using MoviesApi.UnitTests.DbFixtures;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.UnitTests.AccessTests
{
    public class RatingAccessTests : MoviesFixtureContext, IClassFixture<MoviesEfDatabaseFixture>
    {
        [Fact]
        public async Task CanAddAndUpdateRatingWithoutError()
        {
            Guid userId = Guid.Parse("B18A8A2D-0369-48A4-8CA8-8AF187487594");
            Guid movieId = Guid.Parse("B839D79D-A339-416D-ADE0-936EDBCF6691");

            var sut = CreateSystemUnderTest();

            await sut.UpsertRatingAsync(userId, movieId, 8);

            await sut.UpsertRatingAsync(userId, movieId, 4);
        }

        private RatingAccess CreateSystemUnderTest()
        {
            return new RatingAccess(CreateContext);
        }
    }
}
