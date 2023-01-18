using BOOKSTore.Employee.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;

namespace BOOKSTore.Employee
{
    public class EmployeeServices : IEmployeeServices , ITransientDependency
    {

        private readonly IEmailSender _emailSender;
        public EmployeeServices(IRepository<EmployeeDetails, Guid> repository, IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<EmployeeDTO> CreateAsync(EmployeeDTO input)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _emailSender.SendAsync(
                "varadeabhijeet@gmail.com",     // target email address
                "Email subject",         // subject
                "This is email body..."  // email body
            );
        }

        public Task<EmployeeDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<EmployeeDTO>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDTO> UpdateAsync(Guid id, EmployeeDTO input)
        {
            throw new NotImplementedException();
        }
    }
}
