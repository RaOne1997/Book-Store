using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace BOOKSTore.Email
{
    public class EmailSettings : AuditedAggregateRoot<Guid>
    {
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }


    public class AppSettings
    {

        public string path { get; set; }
    }
    public class App
    {
        public string SelfUrl { get; set; }
        public string ClientUrl { get; set; }
        public string CorsOrigins { get; set; }
        public string RedirectAllowedUrls { get; set; }
    }
}
