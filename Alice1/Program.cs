using Alice1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MainContext>(options =>
{
    options.UseSqlServer("Server=KOMPUTER\\SQLEXPRESS;Database=AliceDB;Trusted_Connection=True;TrustServerCertificate=True;");
});



var app = builder.Build();
// Создаем экземпляр класса Startup
var startup = new Startup();

// Вызываем метод Configure через экземпляр класса Startup и передаем ему app
startup.Configure(app);



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapPost("/webhook", async context =>
//{
//    var requestBody = await context.Request.ReadFromJsonAsync<WebhookPayload>();
//    Console.WriteLine("ok");
//    context.Response.StatusCode = 200;


//await context.Response.WriteAsync("");  });


app.Run();
//public record WebhookPayload (string user_id, string session_id);