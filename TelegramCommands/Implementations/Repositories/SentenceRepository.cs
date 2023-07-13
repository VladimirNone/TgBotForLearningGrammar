using GrammarDatabase;
using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TelegramInfrastructure.Interfaces;

namespace TelegramInfrastructure.Implementations.Repositories
{
    public class SentenceRepository : GeneralRepository<Sentence>, ISentenceRepository
    {
        public SentenceRepository(GrammarDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckSentenceAnswer(int sentenceId, string clientAnswer)
        {
            var sentence = await DbSet.Where(x => x.Id == sentenceId).Include(h => h.Answers).FirstOrDefaultAsync();
            return sentence.Answers.Any(h=>h.AnswerText.ToLower() == clientAnswer.ToLower());
        }

        public async Task<Sentence> GetRandomSentence(List<string> inRules)
        {
            var random = new Random().Next(0, DbSet.Include(h => h.GrammarRule).Where(h => inRules.Count == 0 || inRules.Contains(h.GrammarRule.CommandName)).Count() - 1);
            return await DbSet.Where(h=> inRules.Count == 0 || inRules.Contains(h.GrammarRule.CommandName)).Skip(random).Take(1).FirstOrDefaultAsync();
        }
    }
}
