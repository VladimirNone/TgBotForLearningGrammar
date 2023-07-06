using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramInfrastructure;

namespace EngTrainerTelegramBot.Controllers
{
    [ApiController]
    [Route("/")]
    public class TelegramBotController : ControllerBase
    {
        public Bot TelegramBot { get; set; }
        public CommandDeterminer CommandDeteminer { get; set; }

        public TelegramBotController(Bot tgBot, CommandDeterminer cmdDeteminer)
        {
            TelegramBot = tgBot;
            CommandDeteminer = cmdDeteminer;
        }

        [HttpPost]
        public IActionResult Post(Update update)
        {
            var chatId = update.Message.Chat.Id;
            
            var messageText = update.Message.Text;
             
            //CommandDeteminer.DetermineCommand(new GrammarDatabase.Entities.Client() { ChatId = chatId }, messageText);

            CommandDeteminer.ExecuteCommand();

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("I'm ready");
        }
    }
}
