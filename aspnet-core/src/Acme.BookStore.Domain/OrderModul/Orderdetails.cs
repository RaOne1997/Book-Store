using System;
using System.Collections.Generic;

namespace Acme.BookStore.OrderModul
{
    public class Orderdetails
    {
        //public Orderdetails(IRepository<Product, Guid> productRepository)
        //{
        //    ProductRepository = productRepository;
        //}

        public Guid CustomerID { get; set; }
        public List<Orders> Orders { get; set; }
        //IRepository<Product, Guid> ProductRepository { get; set; }


        //public async Task<List<productlist>> productName(List<Orders> Orders) 
        //{
        //    var list =new  List<productlist>();

        //    foreach (var o in Orders)
        //    {
        //        foreach (var o2 in o.OrderLines)
        //        {

        //            var product = await ProductRepository.FindAsync(o2.ProductID);
        //            var prd = new productlist
        //            {

        //            productname= product.Name,
        //            Quentity=o2.Quentity,
        //            };
        //            list.Add(prd);


        //        }

        //    }
        //    return list;

        //}

    }
}
