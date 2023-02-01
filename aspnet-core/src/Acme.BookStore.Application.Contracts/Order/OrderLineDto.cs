using System.ComponentModel.DataAnnotations.Schema;
using System;
using Abp.Application.Services.Dto;

namespace Acme.BookStore.Order
{
    public class OrderLineDto
    {
        public Guid ProductID { get; private set; }
        public Guid OrderId { get; private set; }
        [ForeignKey(nameof(OrderId))]

        public int _Quentity { get; set; }

    }
}