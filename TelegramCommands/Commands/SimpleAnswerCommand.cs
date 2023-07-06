using GrammarDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramInfrastructure.Commands
{
    public class SimpleAnswerCommand : Command
    {
        public SimpleAnswerCommand(Bot telegramBot) : base(telegramBot)
        {
            CommandName = "/simple";
        }

        public override async Task Execute(Client client, string userInput)
        {
            await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, "SimpleAnswerCommand answered");
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
