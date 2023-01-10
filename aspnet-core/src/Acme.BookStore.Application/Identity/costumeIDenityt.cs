using Acme.BookStore.Books;
using Acme.BookStore.ExtraProperty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore.BlobStorage;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Org.BouncyCastle.Utilities;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using System.Reflection.Metadata;
using static System.Reflection.Metadata.BlobBuilder;

namespace Acme.BookStore.Identity
{
    public class costumeIDenityt : IdentityUserAppService
    {

        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IFileAppService _fileAppService { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        public costumeIDenityt(IdentityUserManager userManager, IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository, IOptions<IdentityOptions> identityOptions
            , IFileAppService fileAppService)
            : base(userManager, userRepository, roleRepository, identityOptions)
        {
            UserManager = userManager;
            RoleRepository = roleRepository;
            IdentityOptions = identityOptions;
            _fileAppService = fileAppService;
            UserRepository = userRepository;    
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
            var profileImage = input.ExtraProperties.GetValueOrDefault("Profilepic");
            var gender = input.ExtraProperties.GetValueOrDefault("Gender");
            var title = input.ExtraProperties.GetValueOrDefault("Title");
            title = (Title)Enum.ToObject(typeof(Title), title);

            if (profileImage != null)
            {
                var bac = new SaveBlobInputDto
                {
                    Content = profileImage.ToString(),
                    Name = input.Name
                };
               await _fileAppService.SaveBlobAsync(bac);
                //byte[] bytes = System.Convert.FromBase64String(abc.ToString());
                //user.SetProperty(UserConsts.profilephotoPropertyName, bytes);
            }
            user.SetProperty(UserConsts.TitlePropertyName, title);
            user.SetProperty(UserConsts.GenderPropertyName, gender);
            //input.MapExtraPropertiesTo(user);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            await UpdateUserByInput(user, input);
            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        public override async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            await IdentityOptions.SetAsync();

            var user = await UserManager.GetByIdAsync(id);

            user.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();

            await UpdateUserByInput(user, input);
            var profileImage = input.ExtraProperties.GetValueOrDefault("Profilepic");
            var gender = input.ExtraProperties.GetValueOrDefault("Gender");
            var title = input.ExtraProperties.GetValueOrDefault("Title");
            title = (Title)Enum.ToObject(typeof(Title), title);

            if (profileImage != null)
            {

                if (await _fileAppService.IsExist(input.Name))
                {
                    await _fileAppService.DeleteBlobAsync(input.Name);
                }

                var bac = new SaveBlobInputDto
                {
                    Content = profileImage.ToString(),
                    Name = input.Name
                };
                await _fileAppService.SaveBlobAsync(bac);
                //byte[] bytes = System.Convert.FromBase64String(abc.ToString());
                //user.SetProperty(UserConsts.profilephotoPropertyName, bytes);
            }
            user.SetProperty(UserConsts.TitlePropertyName, title);
            user.SetProperty(UserConsts.GenderPropertyName, gender);

            (await UserManager.UpdateAsync(user)).CheckErrors();

            if (!input.Password.IsNullOrEmpty())
            {
                (await UserManager.RemovePasswordAsync(user)).CheckErrors();
                (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }
        public override async Task<IdentityUserDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.GetByIdAsync(id)
            );
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public override async Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            var count = await UserRepository.GetCountAsync(input.Filter);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            var userlist =new  List<IdentityUser>();
            foreach (var VARIABLE in list)
            {
                
                VARIABLE.SetProperty(UserConsts.profilephotoPropertyName,  _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = VARIABLE.Name }).Result.Content);
                userlist.Add(VARIABLE);
            }

            var abc = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(userlist);
            return new PagedResultDto<IdentityUserDto>(
                count,
                abc

            );
        }

    }
}
