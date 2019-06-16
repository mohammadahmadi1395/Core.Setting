using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace His.Reception.DAL.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ToPagedQuery<T>(this IQueryable<T> query, int pageSize, int pageNumber)
        {
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}
