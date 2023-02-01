using Acme.BookStore.OrderModul;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.ProductModule
{
    public class ProductstoreDataSeedContributer : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly OrderManager _orderModul;

        public ProductstoreDataSeedContributer(IRepository<Product, Guid> productRepository, OrderManager orderModul)
        {
            _productRepository = productRepository;
            _orderModul= orderModul;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _productRepository.GetCountAsync() == 0)
            {
                await _productRepository.InsertAsync(new Product
                {
                    Name = "Satral",
                    Code = "SAR105",
                    expiredate = DateTime.Now.AddYears(2),
                    Price = 1500d,
                    Quentity = 500
                },
                autoSave: true
                );


            }

            //await _orderModul.CreateOrderAsync(new Guid("ab6ee5aa-60f8-f2cb-71eb-3a089a2109c5"), new Dictionary<Guid, int>() { {new Guid("92F2462C-5213-0684-01B6-3A08E2938E1C") ,1} });
        }
    }
}
