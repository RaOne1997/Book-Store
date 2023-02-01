using Acme.BookStore.EntityFrameworkCore;
using Acme.BookStore.OrderModul;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Acme.BookStore.Ordertest
{
    public class OrderefcoreTest : BookStoreEntityFrameworkCoreTestBase
    {
        private readonly IOrderRepositery _ordersRepository;
        //private readonly IRepository<Orders, Guid> _ordersRepository;

        public OrderefcoreTest()
        {
            _ordersRepository = GetRequiredService<IOrderRepositery>();
        }

        [Fact]

        public async Task Get_Seeded_Order()
        {
            var seededdata = await _ordersRepository.GetListAsync(includeDetails:true);

            seededdata.Count.ShouldBe(1);

            var firstorder = seededdata.First();
            firstorder.ShouldNotBeNull();  
            firstorder.OrderLines.Count.ShouldBe(2);
        }
    }
}
