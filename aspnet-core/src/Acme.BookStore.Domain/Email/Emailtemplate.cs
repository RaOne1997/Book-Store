using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Email
{
    public class Emailtemplate : AuditedAggregateRoot<Guid>
    {
        public byte[] TempleteData { get; set; }
        public string TemplateName { get; set; }
        public TemplateType TemplateType { get; set; }  
        public bool IsActive { get; set; }
        public string Subject { get; set; } 
        

    }
}
