using System.Collections.Generic;

namespace Acme.BookStore.OrderModul
{
    public class OrderName
    {
        public string CustomerName { get; set; }
        
        public List<List<Productlist>> productlists { get; set; }
    }
}
