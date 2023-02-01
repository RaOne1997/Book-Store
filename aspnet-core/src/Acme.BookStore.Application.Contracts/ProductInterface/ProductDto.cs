using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.ProductInterface
{
    public class ProductDto : FullAuditedEntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime expiredate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public double Price { get; set; }
        public long Quentity { get; set; }
    }
}
