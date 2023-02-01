
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.ProductInterface
{
    public interface IProductAppService : ICrudAppService<ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreadeProductDto,
            UpdareProductDto>
    {

        Task<List<ProductDto>> GetAvailableProduct();
    }
}
