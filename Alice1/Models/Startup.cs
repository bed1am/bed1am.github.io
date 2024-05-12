using Alice1.Models;
using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
        optionsBuilder.UseSqlServer("Server=KOMPUTER\\SQLEXPRESS;Database=AliceDB;Trusted_Connection=True;TrustServerCertificate=True;");
        var dbContext = new MainContext(optionsBuilder.Options);

        app.UseSession();
        List<string> hookUrls = dbContext.Skills.Select(s => s.hook_url).ToList();
        Console.WriteLine(hookUrls);
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            foreach (var hookUrl in hookUrls)
            {
                endpoints.MapPost(hookUrl, async context =>
                {
                    // Преобразование JSON-данных в модель
                    var requestData = await DeserializeRequestAsync<YourModel>(context);

                    ReqRes reqRes1 = dbContext.ReqRess
                            .Where(reqRes => requestData.request.command.Equals(reqRes.Request) && reqRes.skill.hook_url == hookUrl)
                            .FirstOrDefault();
                    if(reqRes1 != null)
                    requestData.request.res_command = reqRes1.Response;
                    if (requestData.request.command == "")
                    {
                        reqRes1 = dbContext.ReqRess
                            .Where(reqRes => reqRes.Request.Equals(null) && reqRes.skill.hook_url == hookUrl)
                            .FirstOrDefault();
                    }
                    requestData.request.res_command = reqRes1.Response;
                    dbContext.Users.Add(new User
                    {
                        request = requestData.request.command,
                        name = requestData.session.user.user_id,
                        skill = dbContext.Skills.Where(skill => skill.hook_url == hookUrl).First(),
                        date = DateTime.Now.ToString(),

                    });


                    await dbContext.SaveChangesAsync();
                    // Обработка данных
                    // requestData теперь содержит объект вашей модели с данными из JSON

                    // Создание объекта ответа
                    var responseObject = new
                    {
                        version = "1.0",
                        session = requestData.session,
                        response = new
                        {
                            text = requestData.request.res_command,
                            end_session = "false"
                        }
                    };
                    Console.WriteLine(responseObject);
                    // Сериализация объекта ответа в формат JSON
                    var responseJson = JsonConvert.SerializeObject(responseObject);

                    // Устанавливаем код состояния 200 OK и отправляем JSON-ответ
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(responseJson);
                });
            }
        });

    }

    private async Task<T> DeserializeRequestAsync<T>(HttpContext context)
    {
        using var reader = new System.IO.StreamReader(context.Request.Body);
        var requestBody = await reader.ReadToEndAsync();
        return JsonConvert.DeserializeObject<T>(requestBody);
    }
}



public class YourModel
{
    public MetaData meta { get; set; }
    public SessionData session { get; set; }
    public RequestData request { get; set; }
    public StateData state { get; set; }
    public string version { get; set; }
}

public class MetaData
{
    public string locale { get; set; }
    public string timezone { get; set; }
    public string client_id { get; set; }
    public Interfaces interfaces { get; set; }
}

public class Interfaces
{
    public object screen { get; set; }
    public object sayments { get; set; }
    public object account_linking { get; set; }
}

public class SessionData
{
    public int message_id { get; set; }
    public string session_id { get; set; }
    public string skill_id { get; set; }
    public UserData user { get; set; }
    public ApplicationData application { get; set; }
    public string user_id { get; set; }
    public bool New { get; set; }
}

public class UserData
{
    public string user_id { get; set; }
}

public class ApplicationData
{
    public string application_id { get; set; }
}

public class RequestData
{
    public string command { get; set; }
    public string? res_command { get; set; }
    public string original_utterance { get; set; }
    public NluData nlu { get; set; }
    public MarkupData markup { get; set; }
    public string type { get; set; }
}

public class NluData
{
    public string[] tokens { get; set; }
    public Entity[] entities { get; set; }
    public object intents { get; set; }
}

public class Entity
{
    public string type { get; set; }
    public Tokens tokens { get; set; }
    public int value { get; set; }
}

public class Tokens
{
    public int start { get; set; }
    public int end { get; set; }
}

public class MarkupData
{
    public bool dangerous_context { get; set; }
}

public class StateData
{
    public object session { get; set; }
    public object user { get; set; }
    public object application { get; set; }
}

