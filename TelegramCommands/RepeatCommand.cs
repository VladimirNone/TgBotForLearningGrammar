using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramInfrastructure
{
    public class RepeatCommand : Command
    {
        public override async Task Execute(TelegramBotClient bot, long chatId, string userInput)
        {
            await bot.SendTextMessageAsync(chatId, userInput);
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
