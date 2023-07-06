using GrammarDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TelegramInfrastructure.Interfaces
{
    public interface IGeneralRepository<TEntity> where TEntity : Entity
    {
        DbSet<TEntity> DbSet { get; }

        Task AddEntityAsync(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> GetEntityAsync(string id);
        Task<TEntity?> GetEntityByPropertyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
