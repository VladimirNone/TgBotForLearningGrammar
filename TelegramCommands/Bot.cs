
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace TelegramInfrastructure
{
    public class Bot
    {
        private TelegramBotClient botClient;
        private string TelegramToken;

        public Bot(IConfiguration config)
        {
            TelegramToken = config["TelegramBot:TelegramBotToken"];
        }

        public TelegramBotClient GetTelegramBotClient()
        {
            botClient ??= new TelegramBotClient(TelegramToken);
            return botClient;
        }
    }
}
