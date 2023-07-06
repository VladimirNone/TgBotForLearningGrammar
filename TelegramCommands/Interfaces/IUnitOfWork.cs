using GrammarDatabase;
using GrammarDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramInfrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IGeneralRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : Entity;
        Task Commit();
        Task Rollback();
    }
}
