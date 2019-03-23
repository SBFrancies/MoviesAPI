using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesApi.Common.Models
{
    public class Rating
    {
        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

        [Range(0, 10)]
        public decimal Score { get; set; }
    }
}
