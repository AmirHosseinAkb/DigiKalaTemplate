using DigiKala.Core.Services;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Data.Context;
using DigiKala.Core.Convertors;
using Microsoft.EntityFrameworkCore;
using DigiKala.Core.Extensions;
using DigiKala.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authentication.Cookies;

var webApplcationOptions =
    new WebApplicationOptions()
    {
        EnvironmentName = Microsoft.Extensions.Hosting.Environments.Development
    };

var builder = WebApplication.CreateBuilder(webApplcationOptions);

#region DbContext

builder.Services.AddDbContext<DigiKalaContext>(context =>

    context.UseSqlServer(builder.Configuration.GetConnectionString("DigiKalaConnection"))
);
#endregion

#region IOC

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IViewRenderService, RenderViewToString>();

#endregion

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});
#endregion
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options=>

    options.IdleTimeout=TimeSpan.FromDays(1)

);
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Errors/Error404");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCustomStaticFiles();

app.UseRouting();

app.UseCultureCookie();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapRazorPages();

app.Run();
