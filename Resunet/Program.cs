using Estore.BL.Auth;
using Estore.BL.Catalog;
using Estore.BL.General;
using Estore.DAL;
using Estore.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

if (builder.Environment.IsDevelopment())
    InitDevDi(builder);
else
    InitProdDi(builder);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseDalPerfomanceMetric();

DbHelper.ConnString = app.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Не задана строка подключения к БД");

app.Run();

static void InitDevDi(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IAuthDal, AuthDal>();
    builder.Services.AddScoped<IDbSessionDal, DbSessionDal>();
    builder.Services.AddScoped<IUserTokenDal, UserTokenDal>();
    builder.Services.AddScoped<IAuthorDal, AuthorDal>();
    builder.Services.AddScoped<IProductDal, ProductDal>();
    builder.Services.AddScoped<IProductSearchDal, ProductSearchDal>();
    builder.Services.AddScoped<ICartDal, CartDal>();
    builder.Services.AddScoped<IDbHelper, DbHelper>();
    builder.Services.AddScoped<IAuth, Auth>();
    builder.Services.AddScoped<IEncrypt, Encrypt>();
    builder.Services.AddScoped<ICurrentUser, CurrentUser>();
    builder.Services.AddScoped<IDbSession, DbSession>();
    builder.Services.AddScoped<IWebCookie, WebCookie>();
    builder.Services.AddScoped<IProduct, Product>();
    builder.Services.AddScoped<IAuthor, Author>();
    builder.Services.AddScoped<ICart, Cart>();
    builder.Services.AddScoped<IDalMetric, DalMetric>();
}

static void InitProdDi(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IAuthDal, AuthDal>();
    builder.Services.AddSingleton<IDbSessionDal, DbSessionDal>();
    builder.Services.AddSingleton<IUserTokenDal, UserTokenDal>();
    builder.Services.AddSingleton<IAuthorDal, AuthorDal>();
    builder.Services.AddSingleton<IProductDal, ProductDal>();
    builder.Services.AddSingleton<IProductSearchDal, ProductSearchDal>();
    builder.Services.AddSingleton<ICartDal, CartDal>();
    builder.Services.AddSingleton<IDbHelper, DbHelper>();
    builder.Services.AddScoped<IAuth, Auth>();
    builder.Services.AddSingleton<IEncrypt, Encrypt>();
    builder.Services.AddScoped<ICurrentUser, CurrentUser>();
    builder.Services.AddScoped<IDbSession, DbSession>();
    builder.Services.AddScoped<IWebCookie, WebCookie>();
    builder.Services.AddSingleton<IProduct, Product>();
    builder.Services.AddSingleton<IAuthor, Author>();
    builder.Services.AddScoped<ICart, Cart>();
    builder.Services.AddScoped<IDalMetric, DalMetricStub>();
}