//+---------------------------------------------------------+
//|                       PharmaStore                       |
//|                      =============                      |
//+---------------------------------------------------------+
//|               Medicinal E-Commerce System               |
//+---------------------------------------------------------+
//|                         Author                          |
//|                     --------------                      |
//|                  - Prashant Bhandari                    |
//|                  - Prayash Bhattrai                     |
//+---------------------------------------------------------+

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Context;
using PharmaStore.Data.Identity;
using PharmaStore.Data.Repositories.Implementation;
using PharmaStore.Data.Repositories.Implementation.Base;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Data.Repositories.Interface.Base;
using PharmaStore.Modules.Helpers.Implementation;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Mappers.Implementation;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Services.Implementation;
using PharmaStore.Modules.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

//------------------------[ Add services to the container ]-----------------------------
void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddRazorPages().AddRazorRuntimeCompilation();
    services.AddAntiforgery(options =>
    {
        options.HeaderName = "X-CSRF-TOKEN";
        options.FormFieldName = "Name";
    });
    RegisterElements(services);
}
//--------------------------------------------------------------------------------------

//--------------------------[ Register Elements Here ]---------------------------------- 
void RegisterElements(IServiceCollection services)
{
    AddDbContext(services);
    AddIdentity(services);
    AddAuthentication(services);
    RegisterRepositories(services);
    RegisterServices(services);
    RegisterMappers(services);
    RegisterHelpers(services);
}
//--------------------------------------------------------------------------------------

//-------------------------------[ Add DbContext ]--------------------------------------
void AddDbContext(IServiceCollection services)
{
    services.AddDbContext<AppDbContext>(options =>
    {
        //---------------------------[ Database Connection ]------------------------------------
        var ConnectionString = builder.Configuration.GetConnectionString("MySQLConnection") ??
            throw new InvalidOperationException("Null Connection String.");
        options.UseMySQL(ConnectionString);
        //--------------------------------------------------------------------------------------
    });
}
//--------------------------------------------------------------------------------------

//-------------------------------[ Add Identity ]---------------------------------------
void AddIdentity(IServiceCollection services)
{
    services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        //-----------------------------------------[ Password Setting ]------------------------------------------------
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
        //-------------------------------------------------------------------------------------------------------------
        //------------------------------------------[ Lockout Setting ]------------------------------------------------
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        //-------------------------------------------------------------------------------------------------------------
        //-------------------------------------------[ User Setting ]--------------------------------------------------
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
        //-------------------------------------------------------------------------------------------------------------
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
}
//--------------------------------------------------------------------------------------

//----------------------------[ Add Authentication ]------------------------------------
void AddAuthentication(IServiceCollection services)
{
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
    services.ConfigureApplicationCookie(options =>
    {
        //----------------------[ Cookie Setting ]---------------------------
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60 * 24);
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/account/accessdenied";
        options.LogoutPath = "/account/logout";
        options.SlidingExpiration = true;
        //-------------------------------------------------------------------
    });
}
//--------------------------------------------------------------------------------------

//------------------------[ Register Repositories Here ]--------------------------------
void RegisterRepositories(IServiceCollection services)
{
    services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
    services.AddTransient<IMedicineCategoryRepository, MedicineCategoryRepository>();
    services.AddTransient<IMedicineRepository, MedicineRepository>();
    services.AddTransient<IOrderRepository, OrderRepository>();
    services.AddTransient<IStockRepository, StockRepository>();
    services.AddTransient<ISupplierRepository, SupplierRepository>();
}
//--------------------------------------------------------------------------------------

//--------------------------[ Register Services Here ]----------------------------------
void RegisterServices(IServiceCollection services)
{
    services.AddTransient<IManufacturerService, ManufacturerService>();
    services.AddTransient<IMedicineCategoryService, MedicineCategoryService>();
    services.AddTransient<IMedicineService, MedicineService>();
    services.AddTransient<IOrderService, OrderService>();
    services.AddTransient<IStockService, StockService>();
    services.AddTransient<ISupplierService, SupplierService>();
}
//--------------------------------------------------------------------------------------

//--------------------------[ Register Mappers Here ]-----------------------------------
void RegisterMappers(IServiceCollection services)
{
    services.AddTransient<IManufacturerMapper, ManufacturerMapper>();
    services.AddTransient<IMedicineCategoryMapper, MedicineCategoryMapper>();
    services.AddTransient<IMedicineMapper, MedicineMapper>();
    services.AddTransient<IOrderMapper, OrderMapper>();
    services.AddTransient<IStockMapper,StockMapper>();
    services.AddTransient<ISupplierMapper, SupplierMapper>();
}
//--------------------------------------------------------------------------------------

//--------------------------[ Register Helpers Here ]-----------------------------------
void RegisterHelpers(IServiceCollection services)
{
    services.AddTransient<IFileHelper, FileHelper>();
    services.AddTransient<IPaginationRedirectHelper, PaginationRedirectHelper>();
    services.AddTransient<IToastrHelper, ToastrHelper>();
}
//--------------------------------------------------------------------------------------

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); //Note : app.UseAuthentication() middleware should always be called before the app.UseAuthorization() middleware to ensure that authentication occurs before authorization.
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "admin",
    pattern: "admin/{controller=dashboard}/{action=index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"
);

app.Run();