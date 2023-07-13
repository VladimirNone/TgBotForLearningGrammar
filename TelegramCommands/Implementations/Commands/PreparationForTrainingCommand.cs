using GrammarDatabase.DTOs;
using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramInfrastructure.Interfaces;

namespace TelegramInfrastructure.Implementations.Commands
{
    public class PreparationForTrainingCommand : Command
    {
        private int CountButtonInLine = 3;

        public PreparationForTrainingCommand(Bot telegramBot, IUnitOfWork unitOfWork) : base(telegramBot, unitOfWork)
        {
            CommandName = "/prepareForTraining";
        }

        public override async Task Execute(Client client, ClientMessage clientMessage)
        {
            var splitedClientText = clientMessage.Text.Split();
            
            //Пользователь может нажать на кнопку выбора правила без нажатия на текущую команду и получится,
            //что предыдущая команда передаст свои параметры
            var needAddParamToCommand = client.NameLastCommand.StartsWith(CommandName);
            var commandParams = string.Join(" ", splitedClientText[1..]).Trim();
            var newCommandDescription = string.Join(" ", needAddParamToCommand ? client.NameLastCommand : CommandName, commandParams).Trim();

            ChangeLastClientCommand(client, newCommandDescription);

            var keyboard = await BuildKeyboards(newCommandDescription.Split()[1..]);

            await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, $"Выбери правила, которые ты хочешь включить в тренировку, а после нажми на /startTrainGrammar", replyMarkup: keyboard);
        }

        private async Task<InlineKeyboardMarkup> BuildKeyboards(string[] alreadyUsedRules)
        {
            var grammarRules = await UnitOfWork.GetRepository<GrammarRule>().DbSet.ToListAsync();
            var gramRulesWhichCanBeAdded = grammarRules.Where(h => !alreadyUsedRules.Contains(h.CommandName)).ToList();
            var countRows = gramRulesWhichCanBeAdded.Count / CountButtonInLine + gramRulesWhichCanBeAdded.Count % CountButtonInLine > 0 ? 1 : 0;
            var matrixButtons = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < countRows; i++)
            {
                matrixButtons.Add(new List<InlineKeyboardButton>());
                for (int j = 0; j < (CountButtonInLine > gramRulesWhichCanBeAdded.Count ? gramRulesWhichCanBeAdded.Count : CountButtonInLine) ; j++)
                {

                    var curGrammarRule = gramRulesWhichCanBeAdded[i * CountButtonInLine + j];
                    matrixButtons[i].Add(InlineKeyboardButton.WithCallbackData(curGrammarRule.Name, CommandName + " " + curGrammarRule.CommandName));
                }

            }

            return new InlineKeyboardMarkup(matrixButtons);
        }

        public override void Undo(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
