
using Acme.BookStore.Books;
using Acme.BookStore.Permissions;
using Acme.BookStore.ProductInterface;
using Acme.BookStore.ProductModule;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Acme.BookStore.ProductService
{

    public class ProductAppService : CrudAppService<Product, ProductDto, //Used to show books
                 Guid, //Primary key of the book entity
                 PagedAndSortedResultRequestDto,
                 CreadeProductDto,
                 UpdareProductDto
        >, IProductAppService
    {
        private readonly IRepository<Product, Guid> _repository;
        public ProductAppService(IRepository<Product, Guid> repository) : base(repository)
        {
            _repository = repository;
            GetPolicyName = BookStorePermissions.Products.Default;
            GetListPolicyName = BookStorePermissions.Products.Default;
            CreatePolicyName = BookStorePermissions.Products.Create;
            UpdatePolicyName = BookStorePermissions.Products.Edit;
            DeletePolicyName = BookStorePermissions.Products.Delete;
        }
        //[Authorize(BookStorePermissions.Products.AllowExalDownload)]
        public async Task<List<ProductDto>> GetAvailableProduct()
        {
            var queary = await _repository.GetQueryableAsync();
            var AvailableProduct = queary.Where(x => x.IsAvailable == true).ToList();
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(AvailableProduct);
        }
    }
}
