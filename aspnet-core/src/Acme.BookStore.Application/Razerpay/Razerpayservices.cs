
using Acme.BookStore;
using BulkeyBook.Models.Razorepay;
using InstamojoAPI;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Order = Razorpay.Api.Order;
using Refund = Razorpay.Api.Refund;

namespace BOOKSTore.Razerpay
{
    public class Razerpayservices : BookStoreAppService
    {
        private const string _key = "rzp_test_nhrFMzGsypzvM5";
        private const string _secret = "wPORnpniDaEMurRzKiT0YTm5";
        RazorpayClient client;
        public Razerpayservices()
        {
            client = new RazorpayClient(_key, _secret);
        }
        public RazorPayOptionsModel GetPayment(RegistrationModel registration)
        {
            OrderModel order = new OrderModel()
            {
                OrderAmount = registration.Amount,
                Currency = "INR",
                Payment_Capture = 1,    // 0 - Manual capture, 1 - Auto capture
                Notes = new Dictionary<string, string>()
                {
                    { "note 1", "first note while creating order" }, { "note 2", "you can add max 15 notes" },
                    { "note for account 1", "this is a linked note for account 1" }, { "note 2 for second transfer", "it's another note for 2nd account" }
                }
            };

            var orderId = CreateOrder(order);

            RazorPayOptionsModel razorPayOptions = new RazorPayOptionsModel()
            {
                Key = _key,
                AmountInSubUnits = order.OrderAmountInSubUnits,
                Currency = order.Currency,
                Name = "Skynet",
                Description = "for Dotnet",
                ImageLogUrl = "",
                OrderId = orderId,
                ProfileName = registration.Name,
                ProfileContact = registration.Mobile,
                ProfileEmail = registration.Email,
                Notes = new Dictionary<string, string>()
                {
                    { "note 1", "this is a payment note" }, { "note 2", "here also, you can add max 15 notes" }
                }
            };
            return razorPayOptions;
        }

        private string CreateOrder(OrderModel order)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(_key, _secret);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", order.OrderAmountInSubUnits);
                options.Add("currency", order.Currency);
                options.Add("payment_capture", order.Payment_Capture);
                options.Add("notes", order.Notes);
                options.Add("receipt", "recpt_442244sqw5");
                var abc = client.Payment.All();
                Order orderResponse = client.Order.Create(options);
                var orderId = orderResponse.Attributes["id"].ToString();
                return orderId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Paymentdetails AllPayment()
        {
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("count", 50);
                options.Add("skip", 0);
                RazorpayClient client = new RazorpayClient(_key, _secret);
                List<Razorpay.Api.Payment> result = client.Payment.All(options);


                var abc = new List<Items>();

                foreach (var x in result)
                {
                    if (x.Attributes["notes"].Count == null)
                    {
                        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(x.Attributes["notes"]));
                        var item = new Items();
                        item = ObjectExtensions.ToObject<Items>(x.Attributes);
                        item.notes = values;
                        abc.Add(item);
                    }
                    else
                    {
                        {
                            var item = new Items();
                            item = ObjectExtensions.ToObject<Items>(x.Attributes);
                            item.notes = new Dictionary<String, string>();
                            abc.Add(item);

                        }

                    }


                }

                //Add(ObjectExtensions.ToObject<Items>(x.Attributes))
                return new Paymentdetails { count = result.Count(), items = abc };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public RefundReSopnce FullRefund(string paymentId, RefundData refundData)
        {
            try
            {
                Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("amount", payment.Attributes["amount"]);
                data.Add("notes", refundData.notes);
                Refund refund = payment.Refund(data);
                var result = new RefundReSopnce();

                if (refund.Attributes["notes"].Count == null)
                {
                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(refund.Attributes["notes"]));

                    result = ObjectExtensions.ToObject<RefundReSopnce>(refund.Attributes);
                    result.notes = values;
                }
                else
                {
                    {
                       
                        result = ObjectExtensions.ToObject<RefundReSopnce>(refund.Attributes);
                        result.notes = new Dictionary<string, string>();
                   

                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public RefundReSopnce PartialRefund(string paymentId, RefundData refundData)
        {
            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("amount", refundData.amount);
            data.Add("notes", refundData.notes);
            Refund refunds = payment.Refund(data);
            RefundReSopnce abc = ObjectExtensions.ToObject<RefundReSopnce>(refunds.Attributes);
            return abc;

        }
    }

}
