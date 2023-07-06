﻿using GrammarDatabase.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

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

        public abstract Task Execute(Client client, Message? clientMessage);
        public abstract void Undo();

    }
}