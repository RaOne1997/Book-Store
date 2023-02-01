using Acme.BookStore.ProductConsts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.ProductInterface
{
    public class UpdareProductDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [StringLength(ProductConst.NameMaxLength)]
        public string Name { get; set; }
        [Required]
        public DateTime expiredate { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
        [Required]
        public double Price { get; set; }
        [Required]
        public long Quentity { get; set; }
    }
}
