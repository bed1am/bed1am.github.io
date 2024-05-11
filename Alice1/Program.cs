using Alice1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MainContext>(options =>
{
    options.UseSqlServer("Server=KOMPUTER\\SQLEXPRESS;Database=AliceDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddDistributedMemoryCache(); // Добавляем распределенный кэш в памяти для сессий

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Отмечаем сессию как существенную для работы приложения
    //options.IdleTimeout = TimeSpan.FromMinutes(30); // Устанавливаем время жизни сессии
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession(); // Используем сессии в приложении

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");
});

app.Run();