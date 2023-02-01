using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.OrderS
{
    public enum OrderStatus
    {
        Approved,
        Processing,
        PaymentPending,
        Canceled,
        Shiped
    }
}
