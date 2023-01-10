using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Currency_code
{
    public class CurrencyCodes : AuditedAggregateRoot<Guid>
    {
        public string CurrencyCode { get; set; }
        public string  CounteryName { get; set; }

    }
}
