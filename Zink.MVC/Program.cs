using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;
using Zink.MVC.Data;
using Zink.MVC.ServiceModel.Request;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
       .AddFluentValidation(options =>
       {
           // Validate child properties and root collection elements
           options.ImplicitlyValidateChildProperties = true;
           options.ImplicitlyValidateRootCollectionElements = true;
           // Automatic registration of validators in assembly
           options.RegisterValidatorsFromAssemblyContaining<QuestionRequestValidator>();
       });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
       .AddFluentValidation(options =>
       {
           // Validate child properties and root collection elements
           options.ImplicitlyValidateChildProperties = true;
           options.ImplicitlyValidateRootCollectionElements = true;
           // Automatic registration of validators in assembly
           options.RegisterValidatorsFromAssemblyContaining<QuestionRequestValidator>();
       });

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseMySql(builder.Configuration.GetConnectionString("Default"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default")));
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => x.LoginPath = "/admin/account/login");


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
