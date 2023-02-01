using Acme.BookStore.Books;
using Acme.BookStore.Email;
using BOOKSTore;
using BOOKSTore.Email;
using BOOKSTore.Employee;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Acme.BookStore.Currency_code;
using Acme.BookStore.ProductModule;
using Acme.BookStore.ProductConsts;
using Acme.BookStore.OrderModul;

namespace Acme.BookStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BookStoreDbContext :
    AbpDbContext<BookStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */


    #region"Useradded DBset"
    public DbSet<Product> Products { get; set; }
    public DbSet<Orders> Orders { get; set; }
    //public DbSet<OrderLine> orderLines { get; set; }

    #endregion

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<CurrencyCodes> CurrencyCodes { get; set; }

    


    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<EmployeeDetails> Empyee { get; set; }
    public DbSet<EmailSettings> EmailSettings { get; set; }
    public DbSet<Emailtemplate> Emailtemplates { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<Book>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + "Books",
                 BOOKSToreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

        builder.Entity<EmployeeDetails>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + "Employee",
                 BOOKSToreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });


        builder.Entity<EmailSettings>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + "EmailConfigration",
                 BOOKSToreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

          builder.Entity<CurrencyCodes>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + "CurrencyCodes",
                 BOOKSToreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
           
        });

        builder.Entity<Emailtemplate>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + nameof(Emailtemplate),
                 BOOKSToreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            //b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        }); 
        builder.Entity<Orders>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + nameof(Orders),
                 BOOKSToreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            //b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        }); builder.Entity<OrderLine>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + nameof(OrderLine),
                 BOOKSToreConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasKey(op => new { op.OrderId, op.ProductID });

            //auto configure for the base class props
            //b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });
        builder.Entity<Product>(b =>
        {
            b.ToTable(BOOKSToreConsts.DbTablePrefix + nameof(Product),
                 BOOKSToreConsts.DbSchema); 
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(ProductConst.NameMaxLength);
            b.Property(x=>x.Price).IsRequired().HasColumnType("decimal(10,4)");
            b.Property(x => x.IsAvailable).IsRequired();
        });





        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(BookStoreConsts.DbTablePrefix + "YourEntities", BookStoreConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.ConfigureBlobStoring();
        }
}
