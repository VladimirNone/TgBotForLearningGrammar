using GrammarDatabase;
using GrammarDatabase.Entities;
using Microsoft.Extensions.DependencyInjection;
using TelegramInfrastructure.Implementations.Repositories;
using TelegramInfrastructure.Interfaces;

namespace TelegramInfrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _services;
        private readonly GrammarDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(GrammarDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _services = serviceProvider;
        }

        public IGeneralRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : Entity
        {
            if (hasCustomRepository)
            {
                var repo = _services.GetService<IGeneralRepository<TEntity>>();
                if (repo != null)
                {
                    return repo;
                }
            }

            var typeEntity = typeof(TEntity);
            if (!_repositories.ContainsKey(typeEntity))
            {
                var generalRepo = new GeneralRepository<TEntity>(_context);
                _repositories.Add(typeEntity, generalRepo);
            }

            return (IGeneralRepository<TEntity>)_repositories[typeEntity];
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            await _context.DisposeAsync();
        }
    }
}
