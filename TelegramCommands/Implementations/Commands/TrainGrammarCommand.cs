using GrammarDatabase.DTOs;
using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramInfrastructure.Implementations.Repositories;
using TelegramInfrastructure.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TelegramInfrastructure.Implementations.Commands
{
    public class TrainGrammarCommand : Command
    {
        public TrainGrammarCommand(Bot telegramBot, IUnitOfWork unitOfWork) : base(telegramBot, unitOfWork)
        {
            CommandName = "/startTrainGrammar";
        }

        public override async Task Execute(Client client, ClientMessage clientMessage)
        {
            var sentenceRepo = (ISentenceRepository)UnitOfWork.GetRepository<Sentence>(true);

            var commandNameWithParams = BuildCommandNameWithParams(client.NameLastCommand);
            var randomSentence = await sentenceRepo.GetRandomSentence(commandNameWithParams.GetRange(1, commandNameWithParams.Count-1));
            commandNameWithParams.Insert(1, randomSentence.Id.ToString());
             
            if (client.NameLastCommand.StartsWith(CommandName))
            {
                var splitedInfo = client.NameLastCommand.Split();
                var previousSentenceId = int.Parse(splitedInfo[1]);
                var previousBotMessageId = int.Parse(splitedInfo[2]);

                if(await sentenceRepo.CheckSentenceAnswer(previousSentenceId, clientMessage.Text))
                {
                    await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, "Правильный ответ!", replyToMessageId: previousBotMessageId);
                }
                else
                {
                    await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, "Не правильный ответ(", replyToMessageId: previousBotMessageId);
                }
            }

            var botMessage = await TelegramBot.BotClient.SendTextMessageAsync(client.ChatId, randomSentence.SentenceText);
            commandNameWithParams.Insert(2, botMessage.MessageId.ToString());

            ChangeLastClientCommand(client, string.Join(" ", commandNameWithParams));
        }

        private List<string> BuildCommandNameWithParams(string lastClientCommand)
        {
            var resList = new List<string>() { CommandName };
            if(lastClientCommand.StartsWith("/prepareForTraining"))
            {
                resList.AddRange(lastClientCommand.Split()[1..]);
            }
            else if (lastClientCommand.StartsWith(CommandName))
            {
                resList.AddRange(lastClientCommand.Split()[3..].ToList());
            }

            return resList;
        }

        public override void Undo(Client client)
        {
            throw new NotImplementedException();
        }
    }
}


/*            var repo = UnitOfWork.GetRepository<GrammarRule>();

            var rule = new GrammarRule() { Name = "Future Simple", UseCases = "" };

            var sentences = new List<Sentence>()
            {
                new Sentence() { SentenceText = "I ____ (buy) a new car next year.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will buy" } } },
                new Sentence() { SentenceText = "They ____ (take) a trip to Europe next summer.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will not take" }, new Answer() { AnswerText = "won't take" } } },
                new Sentence() { SentenceText = "We ____ (attend) the concert tomorrow evening.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will not attend" }, new Answer() { AnswerText = "won't attend" }, } },
                new Sentence() { SentenceText = "She ____ (take) her exams next month.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will take" }, } },
                new Sentence() { SentenceText = "The train ____ (depart) at 9 a.m.tomorrow.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will depart" }, } },
                new Sentence() { SentenceText = "____ (you, meet) me at the airport?", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will you meet" }, } },
                new Sentence() { SentenceText = "He ____ (quit) his job next week.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will quit" }, } },
                new Sentence() { SentenceText = "They ____ (have) a party on Saturday.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will have" }, } },
                new Sentence() { SentenceText = "We ____ (plan) a holiday next month.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will plan" }, } },
                new Sentence() { SentenceText = "The sun ____ (rise) in the morning.", GrammarRule = rule, Answers = new List<Answer>(){ new Answer() { AnswerText = "will rise" }, } },
            };

            rule.Sentences = sentences;

            await repo.AddEntityAsync(rule);*/