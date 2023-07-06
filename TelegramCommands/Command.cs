using GrammarDatabase.Entities;
using Telegram.Bot;

namespace TelegramInfrastructure
{
    public abstract class Command
    {
        protected Bot TelegramBot { get; set; }
        public string CommandName { get; protected set; }

        public Command(Bot telegramBot)
        {
            TelegramBot = telegramBot;
        }

        public abstract Task Execute(Client client, string clientInput);
        public abstract void Undo();

    }
}