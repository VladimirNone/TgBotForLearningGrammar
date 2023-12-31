

using GrammarDatabase;
using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using Telegram.Bot;
using TelegramInfrastructure;
using TelegramInfrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var telegramBotToken = builder.Configuration["TelegramBot:TelegramBotToken"];
var serverUrl = builder.Configuration["TelegramBot:ServerUrl"];
Console.WriteLine($"https://api.telegram.org/bot{telegramBotToken}/setWebhook?url={serverUrl}");

// Add services to the container.
builder.Services.AddDbContext<GrammarDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("GrammarDbConnection")));
builder.Services.AddSingleton<Bot>();
builder.Services.AddDbInfrastructure();
builder.Services.AddScoped<CommandDeterminer>();

builder.Services.AddControllers().AddNewtonsoftJson();


var app = builder.Build();

/*var tgBot = app.Services.GetRequiredService<Bot>();
await tgBot.BotClient.SetWebhookAsync(serverUrl);*/

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

app.Use(async (a, context) =>
{
    await context.Invoke();
});

app.MapControllers();

app.Run();