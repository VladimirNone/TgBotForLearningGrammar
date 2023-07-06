using GrammarDatabase;
using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TelegramInfrastructure.Implementations.Repositories
{
    public class SentenceRepository : GeneralRepository<Client>
    {
        public SentenceRepository(GrammarDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Client?> GetEntityByPropertyAsync(Expression<Func<Client, bool>> predicate)
        {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }

    }
}
