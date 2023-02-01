using Acme.BookStore.ProductModule;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.OrderModul
{
    public class OrderLine : CreationAuditedAggregateRoot
    {
        public OrderLine()
        {
        }
        
        public OrderLine(Guid orderID , Guid ProductId, int quentity)
        {
            OrderId=orderID ;
            ProductID=ProductId ;
            Quentity= quentity;

        }

        public Guid ProductID { get; private set; }
        [ForeignKey(nameof(ProductID))]

        public Product Product { get; set; }
        public Guid OrderId { get;private set; }
        [ForeignKey(nameof(OrderId))]
        public Orders Orders { get; set; }
        public int Quentity { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { OrderId, ProductID };
        }
    }
}