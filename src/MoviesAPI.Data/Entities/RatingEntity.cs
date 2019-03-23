using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApi.Data.Entities
{
    public class RatingEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

        public decimal Rating { get; set; }

        public DateTimeOffset DateSet { get; set; }

        public virtual UserEntity User { get; set; }

        public virtual MovieEntity Movie { get; set; }
    }
}
