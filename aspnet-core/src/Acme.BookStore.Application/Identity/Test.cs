using Acme.BookStore.Regrist;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace Acme.BookStore.Identity
{
    public class Test : AccountAppService
    {
        public Test(IdentityUserManager userManager, IIdentityRoleRepository roleRepository, IAccountEmailer accountEmailer, IdentitySecurityLogManager identitySecurityLogManager, IOptions<IdentityOptions> identityOptions) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager, identityOptions)
        {
        }

       
        public  async Task<IdentityUserDto> RegisterAsyncss(Regristrationdto input)
        {
            //input.ExtraProperties["Gender"] = 'M';
            await CheckSelfRegistrationAsync().ConfigureAwait(continueOnCapturedContext: false);
            await IdentityOptions.SetAsync().ConfigureAwait(continueOnCapturedContext: false);
            IdentityUser user = new IdentityUser(base.GuidGenerator.Create(), input.UserName, input.EmailAddress, base.CurrentTenant.Id);
            user.SetProperty("Gender", input.Gender.ToString());
            input.MapExtraPropertiesTo(user);
            (await UserManager.CreateAsync(user, input.Password).ConfigureAwait(continueOnCapturedContext: false)).CheckErrors();
            await UserManager.SetEmailAsync(user, input.EmailAddress).ConfigureAwait(continueOnCapturedContext: false);
            await UserManager.AddDefaultRolesAsync(user).ConfigureAwait(continueOnCapturedContext: false);
            return base.ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }



        public override async Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        {
            var user = await GetUserByEmailAsync(input.Email);
            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(user);
            await AccountEmailer.SendPasswordResetLinkAsync(user, resetToken, input.AppName, input.ReturnUrl, input.ReturnUrlHash);
        }
      



    }
}
