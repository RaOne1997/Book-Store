using Acme.BookStore.OrderModul;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Acme.BookStore.Order
{
    public class OrderAppServices : BookStoreAppService, IOrderServices, ITransientDependency
    {
        protected readonly OrderManager _ordermanager;

        public OrderAppServices(OrderManager ordermanager)
        {
            _ordermanager = ordermanager;
        }



        public async Task<OrderDTO> CreateAsyncaaa(CreateOrderInput input)
        {
            try
            {
                var order = await _ordermanager.CreateOrderAsync(input.customerId, input.Products);
                var sss = ObjectMapper.Map<Orders, OrderDTO>(order);

                return sss;
            }
            catch (Exception)
            {
                return new OrderDTO();
            }
        }

        public async Task<OrderName> GetOrderByCustomerId(Guid input)
        {
         var abc=  await _ordermanager.GetorderByCustomerID(input);

            var dishn = new Dictionary<string, IEnumerable<Productlist>>();
             var groupbyorderid =    abc.productlists.GroupBy(x=>x.FirstOrDefault().OrderId).ToList();
         
            //var abcs = ObjectMapper.Map<OrderName ,OrderNameDTO>(abc);
            return abc;
        }
    }
}
