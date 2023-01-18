using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Email
{
    public class AbpMailKitOptions
    {
       public  SecureSocketOptions secureSocketOptions { get; set; }
    }
}
