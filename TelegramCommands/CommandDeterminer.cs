
using GrammarDatabase.Entities;
using TelegramInfrastructure.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TelegramInfrastructure
{
    public class CommandDeterminer
    {
        public List<Command> Commands { get; set; }
        private Command _currentCommand { get; set; }
        private string _clientInput { get; set; }
        private Client _client { get; set; }
        private Bot _tgBot { get; set; }

        public CommandDeterminer(Bot bot)
        {
            _tgBot = bot;
            Commands = new List<Command>(){ 
                new StartCommand(bot),
                new RepeatCommand(bot),
                new SimpleAnswerCommand(bot),
            };
        }

        public void DetermineCommand(Client client, string userMessage)
        {
            _client = client;
            _clientInput = userMessage;
            

            /*if (string.IsNullOrEmpty(userMessage) || !userMessage.StartsWith('/'))
            {
                _currentCommand = Commands.Single(h=>h.CommandName == client.NameLastCommand);
            }

            var command = Commands.SingleOrDefault(h => h.CommandName == client.NameLastCommand);
            _currentCommand = command is not null ? command : Commands.Single(h => h.CommandName == client.NameLastCommand);*/

        }

        public void ExecuteCommand()
        {
            _currentCommand = new SimpleAnswerCommand(_tgBot);
            _currentCommand.Execute(_client, _clientInput);
            _client.NameLastCommand = _currentCommand.CommandName;

            //update client in db
        }

    }
}
