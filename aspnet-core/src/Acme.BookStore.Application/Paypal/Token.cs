using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Acme.BookStore.Paypal
{

    public class Tokens
    {
        public string scope { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public int expires_in { get; set; }
        public List<string> supported_authn_schemes { get; set; }
        public string nonce { get; set; }
    }


   
  

}


