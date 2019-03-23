using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApi.Common.Models
{
    [Flags]
    public enum Genre
    {
        None = 0,
        Comedy = 1,
        Drama = 2,
        Action = 4,
        Thriller = 8,
        Horror = 16,
        Fantasy = 32,
        ScienceFiction = 64,
        Romance = 128,
        Mystery = 256,
        Western = 512,
        Animated = 1024,
        Documentary = 2048,
        Adventure = 4096,
        Historical = 8192,
    }
}
