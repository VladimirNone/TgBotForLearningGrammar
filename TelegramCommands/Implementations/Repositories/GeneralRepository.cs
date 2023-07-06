using GrammarDatabase;
using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TelegramInfrastructure.Interfaces;

namespace TelegramInfrastructure.Implementations.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : Entity
    {
        public DbSet<T> DbSet { get; protected set; }
        protected GrammarDbContext DbContext { get; private set; }

        public GeneralRepository(GrammarDbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
            DbContext = dbContext;
        }

        public virtual async Task AddEntityAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            await DbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbSet.Update(entity);
        }

        public virtual async Task<T?> GetEntityAsync(string id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<T?> GetEntityByPropertyAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }
    }
}
