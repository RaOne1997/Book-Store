using Acme.BookStore.UrlConst;
using BOOKSTore.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.UI.Navigation.Urls;

namespace Acme.BookStore.healperclass
{
    public class AppUrlProviders   : IAppUrlProviders
    {
        
        protected App IdentityOptions { get; }
        public AppUrlProviders(IOptions<App> settings)
        {
            IdentityOptions = settings.Value;

        }

        public async Task<string> GetResetPasswordUrlAsync(string appName)
        {
            switch (appName)
            {
                case "Angular":
                    return IdentityOptions.ClientUrl + UrlConstclasss.Angular;


            }
            return null;

        }
    }
}
