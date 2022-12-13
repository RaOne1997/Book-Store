using System;
using System.Collections.Generic;

namespace BulkeyBook.Models.Razorepay
{
    public class RazorPayOptionsModel
    {
        public string Key { get; set; }
        public decimal AmountInSubUnits { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLogUrl { get; set; }
        public string OrderId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileContact { get; set; }
        public string ProfileEmail { get; set; }
        public Dictionary<string, string> Notes { get; set; }
    }


    public class Paymentdetails
    {
        
        public int count { get; set; }
        public List<Items> items { get; set; }
    }



    public class Items
    {
        public string? entity { get; set; }
        public string? id { get; set; }
        public long? amount { get; set; }
        public string? currency { get; set; }
        public string? status { get; set; }
        public string? order_id { get; set; }
        public string? invoice_id { get; set; }
        public string? international { get; set; }
        public string? method { get; set; }
        public long? amount_refunded { get; set; }
        public string? refund_status { get; set; }
        public bool? captured { get; set; }
        public string? description { get; set; }
        public string? card_id { get; set; }
        public string? bank { get; set; }
        public string? wallet { get; set; }
        public string? vpa { get; set; }
        public string? email { get; set; }
        public object? notes { get; set; }
        public string? contact { get; set; }
        public long? fee { get; set; }
        public long? tax { get; set; }
        public string? error_code { get; set; }
        public string? error_description { get; set; }
        public string? error_source { get; set; }
        public string? error_step { get; set; }
        public string? error_reason { get; set; }
        public string? transaction_id { get; set; }
        public object? acquirer_data { get; set; }
        public long? created_at { get; set; }
    }

    public class RefundReSopnce
    {
        public string id { get; set; }
        public string entity { get; set; }
        public long amount { get; set; }
        public string receipt { get; set; }
        public string currency { get; set; }
        public string payment_id { get; set; }
        public object notes { get; set; }
        public string receipts { get; set; }
        public object acquirer_data { get; set; }

        public long created_at { get; set; }
        public string batch_id { get; set; }
        public string status { get; set; }
        public string speed_processed { get; set; }
        public string speed_requested { get; set; }
    }

    public class RefundData
    {

        public long amount { get; set; }
        public string speed { get; set; }
        public Dictionary<string, object> notes { get; set; }
        public string receipt { get; set; }
    }
}
