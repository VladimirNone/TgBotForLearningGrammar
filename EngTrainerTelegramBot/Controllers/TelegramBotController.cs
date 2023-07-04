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

        public TelegramBotController(Bot tgBot)
        {
            TelegramBot = tgBot;
        }

        [HttpPost]
        public IActionResult Post(Update update)
        {
            var deteminer = new CommandDeterminer();

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            var command = deteminer.DetermineCommand(messageText);

            command.Execute(TelegramBot.GetTelegramBotClient(), chatId, messageText);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("I'm ready");
        }
    }
}
