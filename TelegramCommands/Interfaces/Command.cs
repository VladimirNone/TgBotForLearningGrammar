using GrammarDatabase.DTOs;
using GrammarDatabase.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInfrastructure.Implementations;

namespace TelegramInfrastructure.Interfaces
{
    public abstract class Command
    {
        protected Bot TelegramBot { get; set; }
        protected IUnitOfWork UnitOfWork { get; set; }
        public string CommandName { get; protected set; }

        public Command(Bot telegramBot, IUnitOfWork unitOfWork)
        {
            TelegramBot = telegramBot;
            UnitOfWork = unitOfWork;
        }

        protected Command(Bot telegramBot)
        {
            TelegramBot = telegramBot;
        }

        protected void ChangeLastClientCommand(Client client, string fullCommandName)
        {
            client.NameLastCommand = fullCommandName;
        }

        public abstract Task Execute(Client client, ClientMessage clientMessage);
        public abstract void Undo(Client client);

    }
}