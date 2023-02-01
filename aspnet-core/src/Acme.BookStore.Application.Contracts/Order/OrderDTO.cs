using Acme.BookStore.OrderS;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Order
{
    public class OrderDTO:FullAuditedEntityDto<Guid>
    {
        public DateTime? OrderDate { get;  set; }
        public string OrderNumber { get;  set; }
        public OrderStatus orderStatus { get;  set; }
        public Guid CustomerID { get; set; }
        public ICollection<OrderLineDto> orderLines { get; set; } = new List<OrderLineDto>();

    }
}
