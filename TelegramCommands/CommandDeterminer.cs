using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramInfrastructure
{
    public class CommandDeterminer
    {
        public Dictionary<string, Command> Commands { get; set; }
        private Command LastUserCommand { get; set; } = new SimpleAnswerCommand();

        public CommandDeterminer()
        {
            Commands = new Dictionary<string, Command>
            {
                { "/repeat", new RepeatCommand() }
            };
        }

        public Command DetermineCommand(string userMessage)
        {
            if(string.IsNullOrEmpty(userMessage) || !userMessage.StartsWith('/'))
            {
                return LastUserCommand;
            }
            
            if(Commands.TryGetValue(userMessage, out var command))
            {
                LastUserCommand = command;
                return command;
            }
            else
            {
                return LastUserCommand;
            }

        }

    }
}
