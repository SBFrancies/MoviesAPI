using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApi.Common.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal RoundToNearestPointFive(this decimal value) => Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;
    }
}
