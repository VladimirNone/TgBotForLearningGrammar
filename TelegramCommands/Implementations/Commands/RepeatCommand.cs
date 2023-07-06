using GrammarDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInfrastructure.Interfaces;

namespace TelegramInfrastructure.Implementations.Commands
{
    public class RepeatCommand : Command
    {
        public RepeatCommand(Bot telegramBot, IUnitOfWork unitOfWork) : base(telegramBot, unitOfWork)
        {
            CommandName = "/repeat";
        }

        public override async Task Execute(Client client, Message? clientMessage)
        {
            await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, clientMessage.Text);
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
