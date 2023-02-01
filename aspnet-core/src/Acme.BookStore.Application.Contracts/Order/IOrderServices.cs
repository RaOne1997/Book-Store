
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Order
{
    public interface IOrderServices :IApplicationService
    {
        Task<OrderDTO> CreateAsyncaaa(CreateOrderInput input);
        //Task<OrderNameDTO> GetOrderByCustomerId(Guid input);
        //Task<List<Dictionary<string, List<OrderLine>>>> GetOrderByCustomerId(Guid input);
    }
}
