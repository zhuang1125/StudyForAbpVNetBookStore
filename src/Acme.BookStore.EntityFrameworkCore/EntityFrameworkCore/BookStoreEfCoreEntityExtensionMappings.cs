using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Acme.BookStore.EntityFrameworkCore
{
    public static class BookStoreEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            BookStoreGlobalFeatureConfigurator.Configure();
            BookStoreModuleExtensionConfigurator.Configure();

            OneTimeRunner.Run(() =>
            {
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


                //ObjectExtensionManager.Instance
                //.AddOrUpdateProperty<IdentityUser, string>(
                //"SocialSecurityNumber",
                //options =>
                //{
                //    options.MapEfCore(b => b.HasMaxLength(32));
                //}
                //);


                //ObjectExtensionManager.Instance
                //.AddOrUpdate<IdentityUser>(options =>
                //{
                //    options.AddOrUpdateProperty<string>("SocialSecurityNumber");
                //    options.AddOrUpdateProperty<bool>("IsSuperUser");
                //}
                //);

                //ObjectExtensionManager.Instance
                //.AddOrUpdate<IdentityUserCreateDto>(objConfig =>
                //{
                //    //Define two properties with their own validation rules

                //    objConfig.AddOrUpdateProperty<string>("Password", propertyConfig =>
                //    {
                //        propertyConfig.Attributes.Add(new RequiredAttribute());
                //    });

                //    objConfig.AddOrUpdateProperty<string>("PasswordRepeat", propertyConfig =>
                //    {
                //        propertyConfig.Attributes.Add(new RequiredAttribute());
                //    });

                //    //Write a common validation logic works on multiple properties

                //    objConfig.Validators.Add(context =>
                //    {
                //        if (context.ValidatingObject.GetProperty<string>("Password") !=
                //            context.ValidatingObject.GetProperty<string>("PasswordRepeat"))
                //        {
                //            context.ValidationErrors.Add(
                //                new ValidationResult(
                //                    "Please repeat the same password!",
                //                    new[] { "Password", "PasswordRepeat" }
                //                )
                //            );
                //        }
                //    });
                //});
            });
        }
    }
}
