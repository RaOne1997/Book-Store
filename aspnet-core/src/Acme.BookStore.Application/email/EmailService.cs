using Acme.BookStore.Books;
using Acme.BookStore.Email;
using Acme.BookStore.Emailsend;
using BOOKSTore.Email;
using BOOKSTore.Email_send;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Emailsend
{
    public class EmailService : CrudAppService<
            EmailSettings, //The Book entity
           EmailSettingsDTO, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            InputEmailConfigration>, IEmailServices
    {
        InputEmailConfigration _emailSettings = null;
        EmailSettings _emailSettingsd = null;
        private readonly IRepository<EmailSettings, Guid> _repository;
        private readonly IRepository<Emailtemplate, Guid> _Emailtemplate;
        private readonly AppSettings templatepath;
        public EmailService(IRepository<EmailSettings, Guid> repository, IOptions<AppSettings> settings, IOptions<EmailSettings> emailsettings, IRepository<Emailtemplate, Guid> Emailtemplate) : base(repository)
        {
            _emailSettingsd = emailsettings.Value;
            _repository = repository;
            templatepath = settings.Value;
            _Emailtemplate = Emailtemplate;
        }

        public async Task<bool> TestSendEmailAsync(InputEmailConfigration emailData, string toemail)
        {
            _emailSettings = emailData;

            var testdata = new EmailData
            {
                EmailToId = toemail,
                EmailBody = "this is an test email ",
                EmailSubject = "Email test",
                EmailToName = "Hello Test"
            };

            return await SendEmailAsync(testdata); ;
        }

        public async Task<bool> SendEmailAsync(EmailData emailData)
        {
            try
            {
                _emailSettings = ObjectMapper.Map<EmailSettings, InputEmailConfigration>(await _repository.FirstOrDefaultAsync(x => x.EmailId.Equals("avarade85@gmail.com")));
                if (emailData != null)
                {
                    _emailSettings = new InputEmailConfigration
                    {
                        EmailId = _emailSettingsd.EmailId,
                        Name = _emailSettingsd.Name,
                        Password = _emailSettingsd.Password,
                        Host = _emailSettingsd.Host,
                        Port = _emailSettingsd.Port,
                        UseSSL = _emailSettingsd.UseSSL,
                    };
                }
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                if (emailData.IshtmlTemplet)
                {
                    var emaildata = await ReplaceDynamicDataAsync(emailData.EmailSubject,emailData.redricturl);
                    if (emaildata == null)
                    {
                        emailMessage.Subject = emailData.EmailSubject;
                        emailMessage.Body = new TextPart("html") { Text = emailData.EmailBody };
                    }
                    else
                    {
                        emailMessage.Subject = emaildata.subject;
                        emailMessage.Body = new TextPart("html") { Text = emaildata.body };
                    }
                }
                else
                {
                    emailBodyBuilder.TextBody = emailData.EmailBody;
                    emailMessage.Subject = emailData.EmailSubject;
                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                }
                emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                await emailClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                await emailClient.AuthenticateAsync(_emailSettings.EmailId, _emailSettings.Password);
                await emailClient.SendAsync(emailMessage);
                await emailClient.DisconnectAsync(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Templatename> GetAlltemplatename()
        {
            DirectoryInfo place = new DirectoryInfo(templatepath.path);
            FileInfo[] Files = place.GetFiles("*.tpl");

            return Files.Select(x => new Templatename
            {
                name = x.Name.Replace(".tpl", ""),
                creationtime = x.CreationTime,

            }).ToList();


        }

        private async Task<EmailSendvalue> ReplaceDynamicDataAsync(string data,string url)
        {
            var template = await _Emailtemplate.FirstOrDefaultAsync(x => x.IsActive == true && x.TemplateName == data);
            if (template != null)
            {
                var streamss = new StreamReader(new MemoryStream(template.TempleteData));
                var abc = await streamss.ReadToEndAsync();
                return new EmailSendvalue { body = abc.Replace("{{resetURl}}", url), subject = template.Subject };
            }

            return null;
            //.Replace("{{action_url}}", "https://www.google.com").Replace("[Product Name]","YogaPoint");
        }



        public string Displaytemplet(string filename)
        {
            DirectoryInfo place = new DirectoryInfo(templatepath.path);
            FileInfo[] Files = place.GetFiles();

            return File.ReadAllText($"{templatepath.path}{filename}.tpl");


        }

        public async Task<string> UploadFileAsync(EmailtemplateDTO form)
        {
            try
            {
                byte[] bytes = System.Convert.FromBase64String(form.TempletseData.ToString());
                var abc = new Emailtemplate
                {
                    TempleteData = bytes,
                    TemplateName = form.TemplateName,
                    TemplateType = form.TemplateType,
                    IsActive = form.isActive,
                    Subject = form.TemplateName

                };
                 await _Emailtemplate.InsertAsync(abc);

                //var streamss = new StreamReader(new MemoryStream());
                //var abc = await streamss.ReadToEndAsync();


                //var filePath = Path.Combine(templatepath.path,
                //Path.GetFileName(form.uplodeTemplateFile.FileName));

                //if (form.uplodeTemplateFile.Length > 0)
                //{
                //    using (var stream = System.IO.File.Create(filePath))
                //    {
                //        await form.uplodeTemplateFile.CopyToAsync(stream);
                //    }
                //}


                return "File Uploaded Successfully!!"; ;
            }
            catch
            {

                return "File upload failed!!";
            }
        }
    }
}