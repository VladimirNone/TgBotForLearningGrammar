using GrammarDatabase.DTOs;
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
            ClientMessage message;
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                message = new ClientMessage() { ChatId = update.Message.Chat.Id, Text = update.Message.Text, Username = update.Message.Chat.Username };
            }
            else //if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                message = new ClientMessage() { ChatId = update.CallbackQuery.Message.Chat.Id, Text = update.CallbackQuery.Data, Username = update.CallbackQuery.Message.Chat.Username };
            }

            var client = new Client() { ChatId = message.ChatId, UserName = message.Username };
            var clientFromDb = await UnitOfWork.GetRepository<Client>().GetEntityByPropertyAsync(h => h.ChatId == client.ChatId);

            CommandDeteminer.DetermineCommand(clientFromDb ?? client, message);

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
