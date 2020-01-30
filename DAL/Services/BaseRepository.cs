using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class BaseRepository<T>
    {
        public readonly DataResolver DataResolver;

        public BaseRepository(DataResolver dataResolver)
        {
            DataResolver = dataResolver;
        }
    }
}
