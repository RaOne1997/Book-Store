
using Acme.BookStore.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Acme.BookStore.Regrist
{
    public interface ICustomerIdentity: IApplicationService
    {
       
            Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input);
            Task<IdentityUserDto> GetAsync(Guid id);
            Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input);
                        Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input);
        
    }
}
