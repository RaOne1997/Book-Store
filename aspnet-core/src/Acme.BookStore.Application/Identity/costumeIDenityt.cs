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

namespace Acme.BookStore.Identity
{
    public class costumeIDenityt : IdentityUserAppService
    {

        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IFileAppService _fileAppService { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        private IRepository<IdentityUser,Guid> _IdentityUserRepository { get; }
        public costumeIDenityt(IdentityUserManager userManager, IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository, IOptions<IdentityOptions> identityOptions
            , IFileAppService fileAppService,
            IRepository<IdentityUser, Guid> IdentityUserRepository )
            : base(userManager, userRepository, roleRepository, identityOptions)
        {
            UserManager = userManager;
            RoleRepository = roleRepository;
            IdentityOptions = identityOptions;
            _fileAppService = fileAppService;
            UserRepository = userRepository;
            _IdentityUserRepository = IdentityUserRepository;


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
                    Name = input.Email
                };
               await _fileAppService.SaveBlobAsync(bac);
                //byte[] bytes = System.Convert.FromBase64String(abc.ToString());
                //user.SetProperty(UserConsts.profilephotoPropertyName, bytes);
            }
            else
            {
                var acb = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = "Default" });
                var bac = new SaveBlobInputDto
                {
                   
                    Content =  Convert.ToBase64String(acb.Content),
                    Name = input.Email
                };
                await _fileAppService.SaveBlobAsync(bac);
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
                var image = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = VARIABLE.Email });
                var defaultImage = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = "Default" });

                VARIABLE.SetProperty(UserConsts.profilephotoPropertyName, image.Content!=null? image.Content: defaultImage.Content);
                userlist.Add(VARIABLE);
            }

            var abc = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(userlist);
            return new PagedResultDto<IdentityUserDto>(
                count,
                abc

            );
        }


        public virtual async Task<PagedResultDto<UserData>> GetListAsyncsss(GetIdentityUsersInput input)
        {
            var user =await  _IdentityUserRepository.GetQueryableAsync();

            var queary = (from a in user.ToList()
                          select new UserData { Email=a.Email,
                          Gender = a.GetProperty<char>("Gender"),
                          Name = a.Name
                          }).ToList();

            return new PagedResultDto<UserData>(
                0,
               queary
            );
        }


    }
}
