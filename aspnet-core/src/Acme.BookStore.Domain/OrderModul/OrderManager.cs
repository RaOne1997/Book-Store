using Acme.BookStore.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Acme.BookStore.OrderModul
{
    public class OrderManager : DomainService, ITransientDependency
    {
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IOrderRepositery _orderRepositery;
        private readonly IdentityUserManager _identityUserManager;

        public OrderManager(IIdentityUserRepository identityUserRepository,
            IRepository<Product, Guid> productRepository,
            IOrderRepositery orderRepositery,
            IdentityUserManager identityUserManager)
        {
            _identityUserRepository = identityUserRepository;
            _productRepository = productRepository;
            _orderRepositery = orderRepositery;
            _identityUserManager = identityUserManager;
        }

        public async Task<Orders> CreateOrderAsync(Guid customerId, Dictionary<Guid, int> products)
        {


            var Customer = await _identityUserRepository.FindAsync(customerId);

            if (Customer == null)
            {
                throw new BusinessException("Customer Not Found");
            }
            foreach (var product in products)
            {
                var prod = await _productRepository.FindAsync(product.Key);
                var quentity = prod.Quentity;
                if (prod == null)
                {
                    throw new BusinessException("Product Not Found");
                }
                if (product.Value >= quentity)
                {
                    throw new BusinessException($"Please Enter quentity between {quentity}");
                }
            }

            return await _orderRepositery.InsertAsync(new Orders(customerId, products));

        }


        public async Task<OrderName> GetorderByCustomerID(Guid custmerID)
        {
            var orderdetails = await _orderRepositery.GetOrderBYcustomerId(custmerID, true);

            var user = orderdetails.GroupBy(x => x.CustomerID).Select(x => new Orderdetails
            {
                CustomerID = x.Key,
                Orders = x.ToList(),


            }).FirstOrDefault();

            var userdd = (from sswq in user.Orders
                          select
                           sswq.OrderLines.ToList()
                          );

            var pro = (from s in userdd

                       select s.Select(x => new Productlist
                       {
                           productname = finnd(x.ProductID),
                           Quentity = x.Quentity,
                           OrderDate = x.Orders.OrderDate.Value.Date,
                           OrderId = x.Orders.OrderNumber
                       })
                       .ToList()).ToList();



            var pro2 = (from s in userdd

                        select s.GroupBy(xx => xx.Orders.OrderNumber)

                        .Select(x =>
                        new Dictionary<string, List<OrderLine>>()
                        {
                            { x.Key,x.ToList() }

                        }
                        ).FirstOrDefault()
                        ).ToList();

            //var iste = new List<keyvalue>();

            //foreach (var abcs in pro2)
            //{
            //    foreach (var q in abcs)
            //    {
            //        var kk = new keyvalue
            //        {
            //            Id = q.Key,
            //            Name = q.Value
            //        };

            //        iste.Add(kk);
            //    }

            //}

            //var datas = iste.GroupBy(x => x.Id);


            var result = new OrderName
            {
                CustomerName = (await _identityUserManager.FindByIdAsync(user.CustomerID.ToString())).Name,
                productlists = pro.ToList(),

            };

            return result;
        }




        private string finnd(Guid pid)
        {
            var name = (from a in _productRepository.ToListAsync().GetAwaiter().GetResult()
                        where a.Id == pid
                        select a.Name).FirstOrDefault();

            return name;

        }
    }



    public class keyvalue
    {

        public string Id { get; set; }
        public List<Productlist> Name { get; set; }

    }


    public class orderdadaad
    {
        public Guid Custid { get; set; }
        public Dictionary<string, List<OrderLine>> Order { get; set; }
    }
}
