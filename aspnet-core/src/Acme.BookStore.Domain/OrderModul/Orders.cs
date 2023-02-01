using Acme.BookStore.OrderS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Acme.BookStore.OrderModul
{
    public class Orders : FullAuditedAggregateRoot<Guid>
    {
        private Orders()
        {
           
        }
        public Orders(Guid customerId, Dictionary<Guid,int> productwithQuentitys)
        {
            OrderNumber = GenrateOrderNumber();
            OrderDate = DateTime.Now.Date;
            CustomerID= customerId;
            AddProduct(productwithQuentitys);
        }

        public DateTime? OrderDate { get;private set; }
        public string OrderNumber { get; private set;}
        public OrderStatus OrderStatus { get;private set; }
        public Guid CustomerID { get; set; }
        [ForeignKey(nameof(CustomerID))]
        public IdentityUser User { get; set; }
        public ICollection<OrderLine> OrderLines{get;set;} = new List<OrderLine>();


        public void AddProduct(Dictionary<Guid, int> productwithQuentitys)
        {
            foreach (var productwithQuentity in productwithQuentitys)
            {
                OrderLines.Add(new OrderLine(orderID:Id,ProductId:productwithQuentity.Key,quentity:productwithQuentity.Value));
            }

        }

        private static string GenrateOrderNumber()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var Charsarr = new char[10];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var syz=  new string(Charsarr);
            syz = $"Oid_{syz}";
            return syz;
        }
    }
}
