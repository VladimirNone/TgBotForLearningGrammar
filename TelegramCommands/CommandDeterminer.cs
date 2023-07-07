using GrammarDatabase.Entities;
using Telegram.Bot.Types;
using TelegramInfrastructure.Implementations;
using TelegramInfrastructure.Implementations.Commands;
using TelegramInfrastructure.Interfaces;

namespace TelegramInfrastructure
{
    public class CommandDeterminer
    {
        public List<Command> Commands { get; set; }
        private Command _currentCommand { get; set; }
        private Message? _clientMessage { get; set; }
        private Client _client { get; set; }
        private Bot _tgBot { get; set; }
        private IUnitOfWork _unitOfWork{ get; set; }

        public CommandDeterminer(Bot bot, IUnitOfWork unitOfWork)
        {
            _tgBot = bot;
            _unitOfWork = unitOfWork;

            Commands = new List<Command>(){ 
                new StartCommand(bot, unitOfWork),
                new TrainGrammarCommand(bot, unitOfWork),
                new SimpleAnswerCommand(bot, unitOfWork),
            };
        }

        public void DetermineCommand(Client client, Message? userMessage)
        {
            _client = client;
            _clientMessage = userMessage;
            var clientInput = userMessage.Text;


            if (string.IsNullOrEmpty(clientInput) || !clientInput.StartsWith('/'))
            {
                _currentCommand = Commands.Single(h => h.CommandName == client.NameLastCommand.Split()[0]);
            }

            var command = Commands.SingleOrDefault(h => h.CommandName == clientInput);
            _currentCommand = command is not null ? command : Commands.Single(h => h.CommandName == client.NameLastCommand.Split()[0]);

        }

        public async Task ExecuteCommand()
        {
            try
            {
                await _currentCommand.Execute(_client, _clientMessage);

                await _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
            }
        }

        public async Task UndoCommand()
        {
            try
            {
                _currentCommand.Undo(_client);

                await _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
            }
        }

    }
}
