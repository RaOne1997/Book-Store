using Acme.BookStore.Order;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Paypal
{
    public class PaypallApiAppServices : BookStoreAppService,  ITransientDependency
    {
        public IConfiguration Configuration { get; set; }
        public PaypallApiAppServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public async Task<string> RedirectUrlasync()
        {
            try
            {

                HttpClient client = GetPaypalHttpclient();
                Tokens tokens = await GetPaypallAccessToken(client);
                var  createdPayment = await createdPaymentAsync(client, tokens);

                //  createdPayment.links.First(x => x.rel == "appoval_url").href;
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private async Task<Root> createdPaymentAsync(HttpClient client, Tokens tokens)
        {

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "/v1/payments/payment");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer",

                tokens.access_token);

            var form = JObject.FromObject(new
            {
                intent= "sale",
                redirect_urls = new
                {
                    return_url = "https://www.google.com",
                    cancel_url = "https://www.google.com"

                },
                payer = new {payment_method="paypal"},
                transactions = JArray.FromObject(new[]
                {
                    new
                    {
                        amount = new
                        {
                            total=7.45,
                            currency = "USD"
                        }
                    }
                })
            });
            
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(form),
               Encoding.UTF8,"application/json" 
                );
            HttpResponseMessage responce  = await client.SendAsync(requestMessage);
            var data = await responce.Content.ReadAsStringAsync();  
            var  result = JsonConvert.DeserializeObject<Root>(data);  
            return result;

        }

        private async Task<Tokens> GetPaypallAccessToken(HttpClient client)
        {
                byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{Configuration["paypal:clientId"]}" +
                $":{Configuration["paypal:Secret"]}");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string>()
            {
                ["grant_type"] = "client_credentials"
            };
            requestMessage.Content = new FormUrlEncodedContent(form);
                HttpResponseMessage  responce = await client.SendAsync(requestMessage);

            var Content = await responce.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<Tokens>(Content); 

            return token;
        }

        private HttpClient GetPaypalHttpclient()
        {
            var sandbox = Configuration["paypal:ApiUrl"];

            var http = new HttpClient()
            {

                BaseAddress = new Uri(sandbox),
                Timeout = TimeSpan.FromSeconds(30)

            };
            return http;


        }



        private async Task<object> ExecutePaypalPaymentAsync(HttpClient client, Tokens tokens,
            string paymentID,string payerId)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/v1/payments/payment" +
                $"/{paymentID}/execute");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer",

                tokens.access_token);

            var payment = JObject.FromObject(new
            {

                payer_id = payerId
            });

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(payment),
             Encoding.UTF8, "application/json"
              );

            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            var Data = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(Data);

            return result;    
        }



    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Amount
    {
        public string total { get; set; }
        public string currency { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }

    public class Payer
    {
        public string payment_method { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string intent { get; set; }
        public string state { get; set; }
        public Payer payer { get; set; }
        public List<Transaction> transactions { get; set; }
        public DateTime create_time { get; set; }
        public List<Link> links { get; set; }
    }

    public class Transaction
    {
        public Amount amount { get; set; }
        public List<object> related_resources { get; set; }
    }



}


