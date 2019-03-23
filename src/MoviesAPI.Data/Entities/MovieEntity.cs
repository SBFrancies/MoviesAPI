using MoviesApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApi.Data.Entities
{
    public class MovieEntity
    {
        public MovieEntity()
        {
            Ratings = new HashSet<RatingEntity>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTime { get; set; }

        public Genre Genre { get; set; }

        public ICollection<RatingEntity> Ratings { get; set; }
    }
}
