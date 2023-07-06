using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarDatabase.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public long ChatId { get; set; }
        public bool HasPrivilege { get; set; }
        public bool IsAdmin { get; set; }
        public string NameLastCommand { get; set; }
    }
}
