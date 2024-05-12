using Alice1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


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
        options.IdleTimeout = TimeSpan.FromHours(12);
        options.Cookie.Name = ".Alice.Session"; // <--- Add line
        options.Cookie.IsEssential = true;
        //options.Cookie.HttpOnly = true; // Устанавливаем опции сессии, например, параметры cookie
        //options.Cookie.IsEssential = true; // Отмечаем сессию как существенную для работы приложения
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting(); // Переместите эту строку сюда

    app.UseAuthentication(); // Добавьте это перед app.UseAuthorization()
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
