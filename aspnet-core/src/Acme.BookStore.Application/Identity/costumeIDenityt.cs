using Acme.BookStore.ExtraProperty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace Acme.BookStore.Identity
{
    public class costumeIDenityt : IdentityUserAppService
    {

        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        public costumeIDenityt(IdentityUserManager userManager, IIdentityUserRepository userRepository, IIdentityRoleRepository roleRepository, IOptions<IdentityOptions> identityOptions) : base(userManager, userRepository, roleRepository, identityOptions)
        {
            UserManager = userManager;
            RoleRepository = roleRepository;
            IdentityOptions = identityOptions;

        }


        [Authorize(IdentityPermissions.Users.Create)]
        public override async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            await IdentityOptions.SetAsync();

            var user = new IdentityUser(
                GuidGenerator.Create(),
                input.UserName,
                input.Email,
                CurrentTenant.Id
            );
           var abc= input.ExtraProperties.GetValueOrDefault("Profilepic");
           var gender= input.ExtraProperties.GetValueOrDefault("Gender");
           var title= input.ExtraProperties.GetValueOrDefault("Title");

            byte[] bytes = System.Convert.FromBase64String(abc.ToString());
            user.SetProperty(UserConsts.profilephotoPropertyName, bytes);
            user.SetProperty(UserConsts.TitlePropertyName,title );
            user.SetProperty(UserConsts.GenderPropertyName, gender);
            //input.MapExtraPropertiesTo(user);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            await UpdateUserByInput(user, input);
            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }
    }
}
