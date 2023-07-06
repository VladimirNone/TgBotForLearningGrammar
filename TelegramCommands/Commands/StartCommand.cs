using GrammarDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramInfrastructure.Commands
{
    public class StartCommand : Command
    {
        public StartCommand(Bot telegramBot) : base(telegramBot)
        {
            CommandName = "/start";
        }

        public override Task Execute(Client client, string userInput)
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
