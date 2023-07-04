

using GrammarDatabase;
using Microsoft.EntityFrameworkCore;
using TelegramInfrastructure;

var builder = WebApplication.CreateBuilder(args);

var telegramBotToken = builder.Configuration["TelegramBot:TelegramBotToken"];
var serverUrl = builder.Configuration["TelegramBot:ServerUrl"]; ;
Console.WriteLine($"https://api.telegram.org/bot{telegramBotToken}/setWebhook?url={serverUrl}");

// Add services to the container.
builder.Services.AddDbContext<GrammarDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("GrammarDbConnection")));
builder.Services.AddSingleton<Bot>();
builder.Services.AddControllers().AddNewtonsoftJson();

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

app.UseRouting();

app.MapControllers();

app.Run();
