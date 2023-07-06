using GrammarDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInfrastructure.Implementations.Repositories;
using TelegramInfrastructure.Interfaces;

namespace TelegramInfrastructure.Implementations.Commands
{
    public class StartCommand : Command
    {
        public StartCommand(Bot telegramBot, IUnitOfWork unitOfWork) : base(telegramBot, unitOfWork)
        {
            CommandName = "/start";
        }

        public override async Task Execute(Client client, Message? clientMessage)
        {
            if (client.Id == 0)
            {
                await UnitOfWork.GetRepository<Client>().AddEntityAsync(client);
            }

            await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, $"Здравствуй {client.UserName}! Тебя приветствует бот, обучающий грамматике английского языка!");
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
