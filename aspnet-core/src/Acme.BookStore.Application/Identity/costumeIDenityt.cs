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
using Volo.Abp.Domain.Repositories;
using Acme.BookStore.Employee;
using Newtonsoft.Json.Serialization;
using Acme.BookStore.Regrist;
using Volo.Abp.DependencyInjection;
using Acme.BookStore.OrderModul;
using Volo.Abp.Validation;

namespace Acme.BookStore.Identity
{

    //[ExposeServices(typeof(ICustomerIdentity))]

    public class CostumeIDenityt : BookStoreAppService, ICustomerIdentity
    {

        protected new IdentityUserManager _UserManager { get; }
        protected new IIdentityUserRepository UserRepository { get; }
        protected IFileAppService _fileAppService { get; }
        protected new IOptions<IdentityOptions> IdentityOptions { get; }
        private IRepository<IdentityUser, Guid> _IdentityUserRepository { get; }

        public CostumeIDenityt(IdentityUserManager userManager, IIdentityUserRepository userRepository,
            IOptions<IdentityOptions> identityOptions
            , IFileAppService fileAppService,
            IRepository<IdentityUser, Guid> IdentityUserRepository)

        {
            _UserManager = userManager;
            IdentityOptions = identityOptions;
            _fileAppService = fileAppService;
            UserRepository = userRepository;
            _IdentityUserRepository = IdentityUserRepository;


        }


        [Authorize(IdentityPermissions.Users.Create)]
        public async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            var bac = new SaveBlobInputDto();
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
                 bac = new SaveBlobInputDto
                {
                    Content = profileImage.ToString(),
                    Name = input.Email
                };
                
                //byte[] bytes = System.Convert.FromBase64String(abc.ToString());
                //user.SetProperty(UserConsts.profilephotoPropertyName, bytes);
            }
            else
            {
                var acb = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = "Default" });
                 bac = new SaveBlobInputDto
                {

                    Content = Convert.ToBase64String(acb.Content),
                    Name = input.Email
                };
               
            }
            user.SetProperty(UserConsts.TitlePropertyName, title);
            user.SetProperty(UserConsts.GenderPropertyName, gender);
            //input.MapExtraPropertiesTo(user);

            (await _UserManager.CreateAsync(user, input.Password, false)).CheckErrors();
            await UpdateUserByInput(user, input);
            (await _UserManager.UpdateAsync(user)).CheckErrors();
            await _fileAppService.SaveBlobAsync(bac);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }
        [Authorize(IdentityPermissions.Users.Update)]
        public async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            await IdentityOptions.SetAsync();

            var user = await _UserManager.GetByIdAsync(id);

            user.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            (await _UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();

            await UpdateUserByInput(user, input);
            var profileImage = input.GetProperty<string>("Profilepic");
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
                    Name = input.Email
                };
                await _fileAppService.SaveBlobAsync(bac);
                //byte[] bytes = System.Convert.FromBase64String(abc.ToString());
                //user.SetProperty(UserConsts.profilephotoPropertyName, bytes);
            }
            user.SetProperty(UserConsts.TitlePropertyName, title);
            user.SetProperty(UserConsts.GenderPropertyName, gender);

            (await _UserManager.UpdateAsync(user)).CheckErrors();

            if (!input.Password.IsNullOrEmpty())
            {
                (await _UserManager.RemovePasswordAsync(user)).CheckErrors();
                (await _UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }
        public async Task<IdentityUserDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await _UserManager.GetByIdAsync(id)
            );
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public async Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            var count = await UserRepository.GetCountAsync(input.Filter);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            var userlist = new List<IdentityUser>();
            foreach (var VARIABLE in list)
            {
                var image = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = VARIABLE.Email });
                var defaultImage = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = "Default" });

                VARIABLE.SetProperty(UserConsts.profilephotoPropertyName, image.Content != null ? image.Content : defaultImage.Content);
                userlist.Add(VARIABLE);
            }

            var abc = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(userlist);
            return new PagedResultDto<IdentityUserDto>(
                count,
                abc

            );
        }
        private async Task UpdateUserByInput(IdentityUser user, IdentityUserCreateOrUpdateDtoBase input)
        {
            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                (await _UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
            }

            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await _UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

(await _UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();

            user.Name = input.Name;
            user.Surname = input.Surname;
            (await _UserManager.UpdateAsync(user)).CheckErrors();
            user.SetIsActive(input.IsActive);
            if (input.RoleNames != null)
            {
                (await _UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            }

        }
    }
}
