using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BOOKSTore.Employee.DTO
{
    public class EmployeeDTO : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
