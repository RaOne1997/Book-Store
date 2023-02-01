using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Order
{
    public class OrderNameDTO
    {
        public string CustomerName { get; set; }
        public List<List<productlistDTO>> productlists { get; set; }
    }

    public class productlistDTO
    {
        public string productname { get; set; }
        public int Quentity { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderId { get; set; }
    }
}
