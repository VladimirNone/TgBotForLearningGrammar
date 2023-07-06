
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace TelegramInfrastructure
{
    public class Bot
    {
        public TelegramBotClient BotClient { get; private set; }
        private string TelegramToken;

        public Bot(IConfiguration config)
        {
            TelegramToken = config["TelegramBot:TelegramBotToken"];
            BotClient = new TelegramBotClient(TelegramToken);
        }
    }
}
