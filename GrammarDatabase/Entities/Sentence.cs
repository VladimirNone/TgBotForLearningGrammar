using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarDatabase.Entities
{
    public class Sentence : Entity
    {
        public string SentenceText { get; set; }

        public int GrammarRuleId { get; set; }
        public GrammarRule GrammarRule { get; set; }


        public List<Answer> Answers { get; set; }
    }
}
