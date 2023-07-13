using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarDatabase.DTOs
{
    public class ClientMessage
    {
        public string Text { get; set; }
        public long ChatId { get; set; }
        public string Username { get; set; }
    }
}
