using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.OrderModul
{
    public interface IOrderRepositery :IRepository<Orders,Guid>
    {

        Task<List<Orders>> GetOrderBYcustomerId(Guid customerId,bool includeDetails=false ,CancellationToken cancellationToken=default);
    }
}
