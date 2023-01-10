using Acme.BookStore.Emailsend;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Account.Emailing.Templates;
using Volo.Abp.Account.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TextTemplating;
using Volo.Abp.UI.Navigation.Urls;

namespace BOOKSTore.email
{
    public class AccountEmailer
        : IAccountEmailer, ITransientDependency
    {
        protected ITemplateRenderer TemplateRenderer { get; }

        protected IStringLocalizer<AccountResource> StringLocalizer { get; }
        protected IAppUrlProvider AppUrlProvider { get; }
        protected ICurrentTenant CurrentTenant { get; }
        public readonly IEmailServices _emailService;

        public AccountEmailer(

            ITemplateRenderer templateRenderer,
            IStringLocalizer<AccountResource> stringLocalizer,
            IAppUrlProvider appUrlProvider,
            IEmailServices emailService,
            ICurrentTenant currentTenant)
        {

            StringLocalizer = stringLocalizer;
            AppUrlProvider = appUrlProvider;
            CurrentTenant = currentTenant;
            TemplateRenderer = templateRenderer;
            _emailService = emailService;
        }

        public virtual async Task SendPasswordResetLinkAsync(
            IdentityUser user,
            string resetToken,
            string appName,
            string returnUrl = null,
            string returnUrlHash = null)
        {

            Debug.Assert(CurrentTenant.Id == user.TenantId, "This method can only work for current tenant!");

            var url = await AppUrlProvider.GetResetPasswordUrlAsync(appName);

            //TODO: Use AbpAspNetCoreMultiTenancyOptions to get the key
            var link = $"{url}?userId={user.Id}&{TenantResolverConsts.DefaultTenantKey}={user.TenantId}&resetToken={UrlEncoder.Default.Encode(resetToken)}";

            if (!returnUrl.IsNullOrEmpty())
            {
                link += "&returnUrl=" + NormalizeReturnUrl(returnUrl);
            }

            if (!returnUrlHash.IsNullOrEmpty())
            {
                link += "&returnUrlHash=" + returnUrlHash;
            }

            var emailContent = await TemplateRenderer.RenderAsync(
                AccountEmailTemplates.PasswordResetLink,
                new { link = link }
            );


            await _emailService.SendEmailAsync(new EmailData
            {
                IshtmlTemplet = true,
                EmailToName = user.Name,
                EmailSubject = "Forgot Password",
                EmailToId = user.Email,
                EmailBody = emailContent
            });

            //await EmailSender.SendAsync(
            //    user.Email,
            //    StringLocalizer["PasswordReset"],
            //    emailContent
            //);
        }

        protected virtual string NormalizeReturnUrl(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
            {
                return returnUrl;
            }

            //Handling openid connect login
            if (returnUrl.StartsWith("/connect/authorize/callback", StringComparison.OrdinalIgnoreCase))
            {
                if (returnUrl.Contains("?"))
                {
                    var queryPart = returnUrl.Split('?')[1];
                    var queryParameters = queryPart.Split('&');
                    foreach (var queryParameter in queryParameters)
                    {
                        if (queryParameter.Contains("="))
                        {
                            var queryParam = queryParameter.Split('=');
                            if (queryParam[0] == "redirect_uri")
                            {
                                return HttpUtility.UrlDecode(queryParam[1]);
                            }
                        }
                    }
                }
            }

            return returnUrl;
        }
    }
}
