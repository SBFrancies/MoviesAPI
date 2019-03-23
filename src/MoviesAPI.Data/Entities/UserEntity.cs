using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApi.Data.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Ratings = new HashSet<RatingEntity>();
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public ICollection<RatingEntity> Ratings { get; set; }
    }
}
