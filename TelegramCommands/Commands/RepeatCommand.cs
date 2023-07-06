using GrammarDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramInfrastructure.Commands
{
    public class RepeatCommand : Command
    {
        public RepeatCommand(Bot telegramBot) : base(telegramBot)
        {
            CommandName = "/repeat";
        }

        public override async Task Execute(Client client, string userInput)
        {
            await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, userInput);
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
