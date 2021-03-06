using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.BookStore.Permissions
{
    public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var myGroup = context.AddGroup(BookStorePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));


            var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));

            var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
            booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
            booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
            booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));


            var authorsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Authors.Default, L("Permission:Authors"));
            authorsPermission.AddChild(BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));
            authorsPermission.AddChild(BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));
            authorsPermission.AddChild(BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));

            var organizationUnitsPermission = bookStoreGroup.AddPermission(BookStorePermissions.OrganizationUnits.Default, L("Permission:OrganizationUnits"));
            organizationUnitsPermission.AddChild(BookStorePermissions.OrganizationUnits.Create, L("Permission:OrganizationUnits.Create"));
            organizationUnitsPermission.AddChild(BookStorePermissions.OrganizationUnits.Edit, L("Permission:OrganizationUnits.Edit"));
            organizationUnitsPermission.AddChild(BookStorePermissions.OrganizationUnits.Delete, L("Permission:OrganizationUnits.Delete"));

            var todoPermission = bookStoreGroup.AddPermission(BookStorePermissions.Todo.Default, L("Permission:Todo"));
            todoPermission.AddChild(BookStorePermissions.Todo.Create, L("Permission:Create"));
            todoPermission.AddChild(BookStorePermissions.Todo.Update, L("Permission:Update"));
            todoPermission.AddChild(BookStorePermissions.Todo.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BookStoreResource>(name);
        }
    }
}
