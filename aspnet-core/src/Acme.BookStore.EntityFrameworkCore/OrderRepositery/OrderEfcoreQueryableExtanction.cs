using Acme.BookStore.OrderModul;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Acme.BookStore.OrderRepositery
{
    public static  class OrderEfcoreQueryableExtanction
    {
        public static IQueryable<Orders> IncludeDetails(this IQueryable<Orders> query, bool includeDetails = false)
        {

            if (!includeDetails)
            {
                return query;
            }
            return query.Include(x => x.OrderLines);
        }
    }
}
