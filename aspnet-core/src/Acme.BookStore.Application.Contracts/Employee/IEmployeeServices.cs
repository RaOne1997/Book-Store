using Acme.BookStore.Books;
using BOOKSTore.Employee.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BOOKSTore.Employee
{
    public interface IEmployeeServices : ICrudAppService< //Defines CRUD methods
            EmployeeDTO, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto //Used for paging/sorting
            >
    {
    }
}
