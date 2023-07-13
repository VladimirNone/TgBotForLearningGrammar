using GrammarDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramInfrastructure.Interfaces
{
    public interface ISentenceRepository : IGeneralRepository<Sentence>
    {
        Task<Sentence> GetRandomSentence(List<string> exceptRules);
        Task<bool> CheckSentenceAnswer(int sentenceId, string clientAnswer);
    }
}
