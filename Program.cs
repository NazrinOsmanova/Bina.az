//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();


using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Proje1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddScoped<ProjeBirContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options=>
	{
        options.Cookie.Name = "ProjeBir";
        options.LoginPath = "/Admin/Giris";
        options.AccessDeniedPath = "/Admin/Giris";
	});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




//Scaffold - DbContext "Server=DESKTOP-UAPLAQ7\SQLEXPRESS;Database=ProjeBir;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - force