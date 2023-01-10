using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Account;

namespace Acme.BookStore.Regrist

{
    public class Regristrationdto : RegisterDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public char Gender { get; set; }

    }
}
