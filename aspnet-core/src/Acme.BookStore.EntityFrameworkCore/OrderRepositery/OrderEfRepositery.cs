using Acme.BookStore.EntityFrameworkCore;
using Acme.BookStore.OrderModul;
using Acme.BookStore.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.OrderRepositery
{
    [ExposeServices(typeof(IOrderRepositery))]
    public class OrderEfRepositery : EfCoreRepository<BookStoreDbContext, Orders, Guid>, IOrderRepositery
    {
        public OrderEfRepositery(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Orders>> GetOrderBYcustomerId(Guid customerId,
            bool includeDetails = false, 
            CancellationToken cancellationToken = default)
        {
            var dbset = await GetQueryableAsync();
            return  await dbset.IncludeDetails(includeDetails)
                .Where(x=>x.CustomerID== customerId).
                ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<IQueryable<Orders>> WithDetailsAsync()
        {
            var dbset = await GetQueryableAsync();
            return dbset.Include(x=>x.OrderLines);
             
        }
    }
}
