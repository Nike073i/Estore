using Estore.BL.Auth;
using Estore.BL.Catalog;
using Estore.BL.General;
using Estore.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IAuthDal, AuthDal>();
builder.Services.AddSingleton<IDbSessionDal, DbSessionDal>();
builder.Services.AddSingleton<IUserTokenDal, UserTokenDal>();
builder.Services.AddSingleton<IAuthorDal, AuthorDal>();
builder.Services.AddSingleton<IProductDal, ProductDal>();
builder.Services.AddSingleton<IProductSearchDal, ProductSearchDal>();


builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddScoped<IWebCookie, WebCookie>();
builder.Services.AddSingleton<IProduct, Product>();
builder.Services.AddSingleton<IAuthor, Author>();

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

DbHelper.ConnString = app.Configuration.GetConnectionString("Default")
	?? throw new InvalidOperationException("Не задана строка подключения к БД");

app.Run();
