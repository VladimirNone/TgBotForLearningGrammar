using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TelegramInfrastructure.Interfaces;
using TelegramInfrastructure.Implementations;
using GrammarDatabase.Entities;
using TelegramInfrastructure.Implementations.Repositories;

namespace TelegramInfrastructure
{
    public static class ServiceRegistration
    {
        public static void AddDbInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // This is the registration for custom repository class
            services.AddTransient<IGeneralRepository<Client>, SentenceRepository>();
        }
    }
}
