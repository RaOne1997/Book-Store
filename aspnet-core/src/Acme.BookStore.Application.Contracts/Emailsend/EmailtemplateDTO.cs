using Abp.Application.Services.Dto;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Emailsend
{
    public class EmailtemplateDTO 
    {
        public byte[]? templeteData { get; set; }
        [Display(Name = "File")]
        public IFormFile? uplodeTemplateFile { get; set; }
        public string TemplateName { get; set; }
  
        public TemplateType TemplateType { get; set; }
        public bool isActive { get; set; }
    }

    public class EmailSendvalue
    {
        public string subject { get; set; }
        public string body { get; set; }
        }
}
