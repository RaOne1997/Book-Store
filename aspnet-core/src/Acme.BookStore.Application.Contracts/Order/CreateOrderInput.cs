using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Acme.BookStore.Permissions.BookStorePermissions;

namespace Acme.BookStore.Order
{
    public class CreateOrderInput
    {
        [Required]
        public Guid customerId { get; set; }
        [Required]
        public Dictionary<Guid, int> Products { get; set; }
    }
}
