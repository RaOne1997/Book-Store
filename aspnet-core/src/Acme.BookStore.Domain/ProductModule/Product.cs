using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.ProductModule
{
    public class Product : CreationAuditedAggregateRoot<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime expiredate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public double Price { get; set; }
        public long Quentity { get; set; }
    }
}
