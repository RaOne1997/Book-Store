﻿using InstamojoAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

using BOOKSTore.payment;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Acme.BookStore;

namespace BOOKSTore.Payment
{
    public class Payment_Services : BookStoreAppService
    {
        IRepository<Book, Guid> _repository;
        private IConfiguration _configuration;
        public Payment_Services(IRepository<Book, Guid> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }


        public async Task<Paymentreturn> StartAsync(string id)
        {

         var asa =  _configuration.GetValue<string>("Credential:username");
            var a = _configuration.GetValue<string>("Credential:username");
            string Insta_client_id = "test_3goYA7nGppFHIIr3THm6H8lxocji0SDQyyD",
                  Insta_client_secret = "test_8lbcz6MmbYH5Zjp5LxaiLlDKzIAJE5xD9X3rIH8gYjxRXsssBjFsByUjTaO3RJt77c5289T1xDRuT15w2Sm8apgO1qkabICa3i4rbcd53WjS5RwuN3GXPrPCcqr",
                  Insta_Endpoint = InstamojoConstants.INSTAMOJO_API_ENDPOINT,
                  Insta_Auth_Endpoint = InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
            Instamojo objClass = await InstamojoImplementation.getApi(Insta_client_id, Insta_client_secret, Insta_Endpoint, Insta_Auth_Endpoint);
            return await CreatePaymentOrder(objClass, id);

        }



        public async Task<GetPaymentDetails> CallbackAsync(string payment)
        {
            string Insta_client_id = "test_3goYA7nGppFHIIr3THm6H8lxocji0SDQyyD",
                  Insta_client_secret = "test_8lbcz6MmbYH5Zjp5LxaiLlDKzIAJE5xD9X3rIH8gYjxRXsssBjFsByUjTaO3RJt77c5289T1xDRuT15w2Sm8apgO1qkabICa3i4rbcd53WjS5RwuN3GXPrPCcqr",
                  Insta_Endpoint = InstamojoConstants.INSTAMOJO_API_ENDPOINT,
                  Insta_Auth_Endpoint = InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
            Instamojo objClass = await InstamojoImplementation.getApi(Insta_client_id, Insta_client_secret, Insta_Endpoint, Insta_Auth_Endpoint);

            return  await objClass.GetpaymentdetailAsync(payment);
        }


        public async Task<CreateRefundResponce> Refund(Refund payment)
        {
            string Insta_client_id = "test_3goYA7nGppFHIIr3THm6H8lxocji0SDQyyD",
                  Insta_client_secret = "test_8lbcz6MmbYH5Zjp5LxaiLlDKzIAJE5xD9X3rIH8gYjxRXsssBjFsByUjTaO3RJt77c5289T1xDRuT15w2Sm8apgO1qkabICa3i4rbcd53WjS5RwuN3GXPrPCcqr",
                  Insta_Endpoint = InstamojoConstants.INSTAMOJO_API_ENDPOINT,
                  Insta_Auth_Endpoint = InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
            Instamojo objClass = await InstamojoImplementation.getApi(Insta_client_id, Insta_client_secret, Insta_Endpoint, Insta_Auth_Endpoint);

            return  objClass.createNewRefundRequest(payment);
        }
        public async Task<Paymentreturn> CreatePaymentOrder(Instamojo objClass, string ID)
        {
            var book = await _repository.FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(ID)));

            PaymentOrder objPaymentRequest = new PaymentOrder();
            //Required POST parameters
            objPaymentRequest.name = book.Name;
            objPaymentRequest.email = "foo@example.com";
            objPaymentRequest.phone = "9969156561";
            objPaymentRequest.description = book.Name; ;
            objPaymentRequest.amount = book.Price; ;
            objPaymentRequest.currency = "USD";
            objPaymentRequest.allow_repeated_payments = false;
            objPaymentRequest.send_email = false;



            string randomName = Path.GetRandomFileName();
            randomName = randomName.Replace(".", string.Empty);
            objPaymentRequest.transaction_id = "test" + randomName;
            //HttpContext.Session.SetString("tranid", objPaymentRequest.transaction_id);

            //objPaymentRequest.redirect_url = "https://swaggerhub.com/api/saich/pay-with-instamojo/1.0.0";
            objPaymentRequest.redirect_url = "http://localhost:4200/books/paymentstatus";
            objPaymentRequest.webhook_url = "https://your.server.com/webhook";
            //Extra POST parameters 

            if (objPaymentRequest.validate())
            {
                var msg = string.Empty;
                if (objPaymentRequest.emailInvalid)
                {
                    msg = "Email is not valid";
                }
                if (objPaymentRequest.nameInvalid)
                {
                    msg = "Name is not valid";
                }
                if (objPaymentRequest.phoneInvalid)
                {
                    msg = "Phone is not valid";
                }
                if (objPaymentRequest.amountInvalid)
                {
                    msg = "Amount is not valid";
                }
                if (objPaymentRequest.currencyInvalid)
                {
                    msg = "Currency is not valid";
                }
                if (objPaymentRequest.transactionIdInvalid)
                {
                    msg = "Transaction Id is not valid";
                }
                if (objPaymentRequest.redirectUrlInvalid)
                {
                    msg = "Redirect Url Id is not valid";
                }
                if (objPaymentRequest.webhookUrlInvalid)
                {
                    msg = "Webhook URL is not valid";
                }
                return new Paymentreturn { ReturnUrl = null, Transaction_id = null };

            }
            else
            {
                var msg = "";
                var url = "";
                try
                {
                    CreatePaymentOrderResponse objPaymentResponse = objClass.createNewPaymentRequest(objPaymentRequest);
                    // Response.Redirect(objPaymentResponse.payment_options.payment_url);
                    url = objPaymentResponse.payment_options.payment_url;
                }
                catch (ArgumentNullException ex)
                {
                    msg = ex.Message;
                }
                catch (WebException ex)
                {
                    msg = ex.Message;
                }
                catch (IOException ex)
                {
                    msg = ex.Message;
                }
                catch (InvalidPaymentOrderException ex)
                {
                    if (!ex.IsWebhookValid())
                    {
                        msg = "Webhook is invalid";
                    }

                    if (!ex.IsCurrencyValid())
                    {
                        msg = "Currency is Invalid";
                    }

                    if (!ex.IsTransactionIDValid())
                    {
                        msg = "Transaction ID is Inavlid";
                    }
                }
                catch (ConnectionException ex)
                {
                    msg = ex.Message;
                }
                catch (BaseException ex)
                {
                    msg = ex.Message;
                }
                catch (Exception ex)
                {
                    msg = "Error:" + ex.Message;
                }
                return new Paymentreturn { ReturnUrl = url, Transaction_id = objPaymentRequest.transaction_id }; ;
            }
        }



    }


}
