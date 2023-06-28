using Resunet.BL.Auth;
using Resunet.BL.General;
using Resunet.BL.Profile;
using Resunet.BL.Resume;
using Resunet.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAuthDal, AuthDal>();
builder.Services.AddSingleton<IDbSessionDal, DbSessionDal>();
builder.Services.AddSingleton<IUserTokenDal, UserTokenDal>();
builder.Services.AddSingleton<IProfileDal, ProfileDal>();
builder.Services.AddSingleton<ISkillDal, SkillDal>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddScoped<IWebCookie, WebCookie>();
builder.Services.AddSingleton<IProfile, Profile>();
builder.Services.AddSingleton<IResume, Resume>();
builder.Services.AddSingleton<ISkill, Skill>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

app.Run();
