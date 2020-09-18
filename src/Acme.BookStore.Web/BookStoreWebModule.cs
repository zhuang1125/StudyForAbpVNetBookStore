using System;
using System.IO;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Acme.BookStore.EntityFrameworkCore;
using Acme.BookStore.Localization;
using Acme.BookStore.MultiTenancy;
using Acme.BookStore.Web.Menus;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Acme.BookStore.Permissions;
using Lsw.Abp.AspNetCore.Mvc.UI.Theme.Stisla;
using EasyAbp.Abp.AspNetCore.Mvc.UI.Theme.LYear;
using EasyAbp.PrivateMessaging.Web;
using EasyAbp.PrivateMessaging;
using EasyAbp.PrivateMessaging.EntityFrameworkCore;
using EasyAbp.Abp.SettingUi;
using EasyAbp.Abp.SettingUi.Web;
using EasyAbp.FileManagement;
using EasyAbp.FileManagement.Web;
using Volo.Abp.BlobStoring;
using EasyAbp.FileManagement.Options;
using EasyAbp.FileManagement.Files;
using EasyAbp.FileManagement.Containers;
using Volo.Abp.BlobStoring.FileSystem;
using EasyAbp.FileManagement.EntityFrameworkCore;







//using System.IO;
//using EasyAbp.FileManagement.Containers;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using EasyAbp.FileManagement.EntityFrameworkCore;
//using EasyAbp.FileManagement.Files;
//using EasyAbp.FileManagement.MultiTenancy;
//using EasyAbp.FileManagement.Options;
//using EasyAbp.FileManagement.Web;
//using Microsoft.OpenApi.Models;
//using Volo.Abp;
//using Volo.Abp.Account;
//using Volo.Abp.Account.Web;
//using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
//using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
//using Volo.Abp.AspNetCore.Serilog;
//using Volo.Abp.AuditLogging.EntityFrameworkCore;
//using Volo.Abp.Autofac;
//using Volo.Abp.BlobStoring;
//using Volo.Abp.BlobStoring.FileSystem;
//using Volo.Abp.Data;
//using Volo.Abp.EntityFrameworkCore;
//using Volo.Abp.EntityFrameworkCore.SqlServer;
//using Volo.Abp.FeatureManagement;
//using Volo.Abp.Identity;
//using Volo.Abp.Identity.EntityFrameworkCore;
//using Volo.Abp.Identity.Web;
//using Volo.Abp.Localization;
//using Volo.Abp.Modularity;
//using Volo.Abp.MultiTenancy;
//using Volo.Abp.PermissionManagement;
//using Volo.Abp.PermissionManagement.EntityFrameworkCore;
//using Volo.Abp.PermissionManagement.Identity;
//using Volo.Abp.SettingManagement.EntityFrameworkCore;
//using Volo.Abp.TenantManagement;
//using Volo.Abp.TenantManagement.EntityFrameworkCore;
//using Volo.Abp.TenantManagement.Web;
//using Volo.Abp.Threading;
//using Volo.Abp.VirtualFileSystem;

namespace Acme.BookStore.Web
{
    [DependsOn(
        typeof(BookStoreHttpApiModule),
        typeof(BookStoreApplicationModule),
        typeof(BookStoreEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpAccountWebIdentityServerModule),


        // typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        // typeof(AbpAspNetCoreMvcUiLYearThemeModule),
        typeof(AbpAspNetCoreMvcUiStislaThemeModule),



        typeof(PrivateMessagingWebModule),
        typeof(PrivateMessagingHttpApiModule),
        typeof(PrivateMessagingApplicationModule),
          typeof(PrivateMessagingEntityFrameworkCoreModule),


      
       // typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpAspNetCoreSerilogModule),



          typeof(SettingUiWebModule),

        typeof(FileManagementHttpApiModule),
        typeof(FileManagementApplicationModule),
          typeof(FileManagementEntityFrameworkCoreModule),
          typeof(FileManagementWebModule),

         typeof(AbpBlobStoringFileSystemModule)

        )]
    public class BookStoreWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(BookStoreResource),
                    typeof(BookStoreDomainModule).Assembly,
                    typeof(BookStoreDomainSharedModule).Assembly,
                    typeof(BookStoreApplicationModule).Assembly,
                    typeof(BookStoreApplicationContractsModule).Assembly,
                    typeof(BookStoreWebModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Books/Index", BookStorePermissions.Books.Default);
                options.Conventions.AuthorizePage("/Books/CreateModal", BookStorePermissions.Books.Create);
                options.Conventions.AuthorizePage("/Books/EditModal", BookStorePermissions.Books.Edit);
            });

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.Configure<LocalFileSystemBlobContainer>(container =>
                {
                    container.IsMultiTenant = true;
                    container.UseFileSystem(fileSystem =>
                    {
                        // fileSystem.BasePath = "C:\\my-files";
                        fileSystem.BasePath = Path.Combine(hostingEnvironment.ContentRootPath, "my-files");
                    });
                });
            });

            Configure<FileManagementOptions>(options =>
            {
                options.DefaultFileDownloadProviderType = typeof(LocalFileDownloadProvider);
                options.Containers.Configure<CommonFileContainer>(container =>
                {
                    // private container never be used by non-owner users (except user who has the "File.Manage" permission).
                    container.FileContainerType = FileContainerType.Public;
                    container.AbpBlobContainerName = BlobContainerNameAttribute.GetContainerName<LocalFileSystemBlobContainer>();
                    container.AbpBlobDirectorySeparator = "/";

                    container.RetainUnusedBlobs = false;
                    container.EnableAutoRename = true;

                    container.MaxByteSizeForEachFile = 5 * 1024 * 1024;
                    container.MaxByteSizeForEachUpload = 10 * 1024 * 1024;
                    container.MaxFileQuantityForEachUpload = 2;

                    container.AllowOnlyConfiguredFileExtensions = true;
                    container.FileExtensionsConfiguration.Add(".jpg", true);
                    container.FileExtensionsConfiguration.Add(".png", true);
                    // container.FileExtensionsConfiguration.Add(".exe", false);

                    container.GetDownloadInfoTimesLimitEachUserPerMinute = 10;
                });
            });

        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "BookStore";
                });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BookStoreWebModule>();

            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    char sept = Path.DirectorySeparatorChar; 
options.FileSets.ReplaceEmbeddedByPhysical<BookStoreDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Acme.BookStore.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Acme.BookStore.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Acme.BookStore.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Acme.BookStore.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreWebModule>(hostingEnvironment.ContentRootPath);


                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new BookStoreMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(BookStoreApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
