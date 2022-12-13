
using BOOKSTore.Email_send;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Emailsend
{
    public interface IEmailServices : ICrudAppService< //Defines CRUD methods
            EmailSettingsDTO, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto,
            InputEmailConfigration//Used for paging/sorting
            >
    {
    
        Task<bool> SendEmailAsync(EmailData emailData);
        Task<bool> TestSendEmailAsync(InputEmailConfigration emailData, string toemail);
        List<Templatename> GetAlltemplatename();
         Task <string> UploadFileAsync(EmailtemplateDTO form);


    }
}