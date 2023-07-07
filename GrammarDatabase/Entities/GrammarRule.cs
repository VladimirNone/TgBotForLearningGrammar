using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarDatabase.Entities
{
    public class GrammarRule : Entity
    {
        public string Name { get; set; }
        public string? UseCases { get; set; }

        public List<Sentence> Sentences { get; set; }
    }
}
