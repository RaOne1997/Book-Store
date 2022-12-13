//using Acme.BookStore.Extraproperty;
using Acme.BookStore.ExtraProperty;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Acme.BookStore.EntityFrameworkCore;

public static class BookStoreEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        BookStoreGlobalFeatureConfigurator.Configure();
        BookStoreModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {

            ObjectExtensionManager.Instance.MapEfCoreProperty<IdentityUser, int>(
                UserConsts.TitlePropertyName,
                (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasDefaultValue(0);
                    
                }).MapEfCoreProperty<IdentityUser, char>(
                UserConsts.GenderPropertyName,
                (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasDefaultValue('M');
                    propertyBuilder.HasMaxLength(1);
                    entityBuilder.HasCheckConstraint("Check_Gender_Ck", $"{UserConsts.GenderPropertyName}='M' " +
                        $" or {UserConsts.GenderPropertyName} = 'F' " +
                        $"or {UserConsts.GenderPropertyName}= 'O'");
                }).MapEfCoreProperty<IdentityUser, byte[]>(
                UserConsts.profilephotoPropertyName,
                (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasDefaultValue(null);
                    //propertyBuilder.HasMaxLength(1);
                    
                });
            /* You can configure extra properties for the
             * entities defined in the modules used by your application.
             *
             * This class can be used to map these extra properties to table fields in the database.
             *
             * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
             * USE BookStoreModuleExtensionConfigurator CLASS (in the Domain.Shared project)
             * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
             *
             * Example: Map a property to a table field:

                 ObjectExtensionManager.Instance
                     .MapEfCoreProperty<IdentityUser, string>(
                         "MyProperty",
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(128);
                         }
                     );

             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
             */
        });
    }
}
