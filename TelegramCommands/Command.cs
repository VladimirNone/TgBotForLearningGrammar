using Telegram.Bot;

namespace TelegramInfrastructure
{
    public abstract class Command
    {
        public abstract Task Execute(TelegramBotClient bot, long chatId, string userInput);
        public abstract void Undo();
    }
}