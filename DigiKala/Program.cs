using DigiKala.Core.Services;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Data.Context;
using Microsoft.EntityFrameworkCore;

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

#endregion
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Errors/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
