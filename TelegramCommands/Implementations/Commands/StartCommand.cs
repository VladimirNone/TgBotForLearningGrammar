using GrammarDatabase.DTOs;
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

        public override async Task Execute(Client client, ClientMessage clientMessage)
        {
            if (client.Id == 0)
            {
                await UnitOfWork.GetRepository<Client>().AddEntityAsync(client);
            }

            await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, $"Здравствуйте {client.UserName}! Вас приветствует бот, обучающий грамматике английского языка! Для начала тренироваки нажми на /startTrainGrammar");
            ChangeLastClientCommand(client, CommandName);
        }

        public override void Undo(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
