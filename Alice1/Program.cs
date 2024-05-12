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

    builder.Services.AddDistributedMemoryCache(); // ��������� �������������� ��� � ������ ��� ������
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromHours(12);
        options.Cookie.Name = ".Alice.Session"; // <--- Add line
        options.Cookie.IsEssential = true;
        //options.Cookie.HttpOnly = true; // ������������� ����� ������, ��������, ��������� cookie
        //options.Cookie.IsEssential = true; // �������� ������ ��� ������������ ��� ������ ����������
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
    app.UseRouting(); // ����������� ��� ������ ����

    app.UseAuthentication(); // �������� ��� ����� app.UseAuthorization()
    app.UseAuthorization();
    app.UseSession(); // ���������� ������ � ����������

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
});

app.Run();
