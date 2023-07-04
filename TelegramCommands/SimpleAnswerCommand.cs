using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramInfrastructure
{
    public class SimpleAnswerCommand : Command
    {
        public override async Task Execute(TelegramBotClient bot, long chatId, string userInput)
        {
            await bot.SendTextMessageAsync(chatId, "SimpleAnswerCommand answered");
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
