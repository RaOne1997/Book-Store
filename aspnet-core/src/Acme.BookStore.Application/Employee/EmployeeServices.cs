using BOOKSTore.Employee.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BOOKSTore.Employee
{
    public class EmployeeServices : CrudAppService<EmployeeDetails, EmployeeDTO,
        Guid, PagedAndSortedResultRequestDto>, IEmployeeServices
    {
        public EmployeeServices(IRepository<EmployeeDetails, Guid> repository) : base(repository)
        {
        }
    }
}
