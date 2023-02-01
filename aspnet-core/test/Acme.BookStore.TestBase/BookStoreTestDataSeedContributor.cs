using Acme.BookStore.OrderModul;
using Acme.BookStore.OrderS;
using Acme.BookStore.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace Acme.BookStore;

public class BookStoreTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly OrderManager _orderrepository;
    private readonly IRepository<Product, Guid> _Productrepository;

    private readonly IdentityUserManager _usermanager;
    private readonly TestData _testdata;

    public BookStoreTestDataSeedContributor(OrderManager orderrepository
        , IdentityUserManager usermanager, TestData testData, IRepository<Product, Guid> productrepository)
    {
        _orderrepository = orderrepository;
        _usermanager = usermanager;
        _testdata = testData;
        _Productrepository = productrepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {

        var IsExist = await _usermanager.FindByEmailAsync(_testdata.CuatomerEmail);
        if (IsExist == null)
        {
            var user = await _usermanager.CreateAsync(new IdentityUser(_testdata.CustomerID,
                _testdata.CuatomerUsername, _testdata.CuatomerEmail));
        }
        var product = new Product
        {
            Code = "TST100",
            Name = "Test0",
            expiredate = DateTime.Now.Date,
            Price = 50d,
            Quentity = 100,
        };
        var product1 = new Product
        {
            Code = "TST101",
            Name = "Test1",
            expiredate = DateTime.Now.Date,
            Price = 30d,
            Quentity = 100,
        };
        var product2 = new Product
        {
            Code = "TST102",
            Name = "Test2",
            expiredate = DateTime.Now.Date,
            Price = 20d,
            Quentity = 100,
        };

        await _Productrepository.InsertAsync(product);
        await _Productrepository.InsertAsync(product1);
        await _Productrepository.InsertAsync(product2);


        var order = new Orders( _testdata.CustomerID, new Dictionary<Guid, int>()
        {
            {product.Id,2},
            {product2.Id,1}
        }
        );



        await _orderrepository.CreateOrderAsync(order.CustomerID, new Dictionary<Guid, int>()
        {
            {product.Id,2},
            {product2.Id,1}
        });
        /* Seed additional test data... */


    }


    public class TestData : ISingletonDependency
    {
        public Guid CustomerID { get; } = Guid.NewGuid();
        public Guid OrderID { get; } = Guid.NewGuid();
        public string CuatomerEmail { get; } = "customer@test.com";
        public string CuatomerUsername { get; } = "customer@test.com";
    }
}
