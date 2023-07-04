using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarDatabase.Entities
{
    public class Answer : Entity
    {
        public string AnswerText { get; set; }

        public List<Sentence> Sentences { get; set; } 
    }
}
