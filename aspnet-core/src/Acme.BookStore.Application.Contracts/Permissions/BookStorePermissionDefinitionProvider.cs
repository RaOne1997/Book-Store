﻿using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(BookStorePermissions.GroupName);

        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));

        var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));

        //var ProductServicesGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));
        var productPermission = bookStoreGroup.AddPermission(BookStorePermissions.Products.Default, L("Permission:Product"));
        productPermission.AddChild(BookStorePermissions.Products.Create, L("Permission:product.Create"));
        productPermission.AddChild(BookStorePermissions.Products.Edit, L("Permission:product.Edit"));
        productPermission.AddChild(BookStorePermissions.Products.Delete, L("Permission:product.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
