using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApi.Common.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTime { get; set; }

        public Genre Genre { get; set; }

        public decimal? AverageRating { get; set; }
    }
}
