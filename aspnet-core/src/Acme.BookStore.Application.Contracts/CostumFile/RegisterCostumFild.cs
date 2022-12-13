using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Account;

namespace BOOKSTore.CostumFile
{
    public class RegisterCostumFild : RegisterDto
    {
        public string Whatsapp { get; set; }  
        public string PhoneNumber { get; set; }
        public char Gender { get; set; }
    }
}
