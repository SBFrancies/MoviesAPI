using MoviesApi.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MoviesApi.UnitTests.HelpersTests
{
    public class ExtensionTests
    {
        [Theory]
        [InlineData(2.91, 3.0)]
        [InlineData(3.249, 3.0)]
        [InlineData(3.25, 3.5)]
        [InlineData(3.6, 3.5)]
        [InlineData(3.75, 4.0)]
        public void CanRoundToNearestHalf(decimal input, decimal expectedOutput)
        {
            Assert.Equal(expectedOutput, input.RoundToNearestPointFive());
        }
    }
}
