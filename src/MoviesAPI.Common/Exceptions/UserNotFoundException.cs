using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApi.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
            
        }
    }
}
