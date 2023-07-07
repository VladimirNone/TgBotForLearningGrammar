using GrammarDatabase.Entities;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramInfrastructure;
using TelegramInfrastructure.Implementations.Repositories;
using TelegramInfrastructure.Interfaces;

namespace EngTrainerTelegramBot.Controllers
{
    [ApiController]
    [Route("/")]
    public class TelegramBotController : ControllerBase
    {
        public Bot TelegramBot { get; set; }
        public CommandDeterminer CommandDeteminer { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public TelegramBotController(Bot tgBot, CommandDeterminer cmdDeteminer, IUnitOfWork unitOfWork)
        {
            TelegramBot = tgBot;
            CommandDeteminer = cmdDeteminer;
            UnitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Update update)
        {
            var client = new Client() { ChatId = update.Message.Chat.Id, UserName = update.Message.Chat.Username };
            var clientFromDb = await UnitOfWork.GetRepository<Client>().GetEntityByPropertyAsync(h => h.ChatId == client.ChatId);
            
            CommandDeteminer.DetermineCommand(clientFromDb ?? client, update.Message);

            await CommandDeteminer.ExecuteCommand();

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("I'm ready");
        }
    }
}
