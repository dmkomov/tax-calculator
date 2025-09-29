using Microsoft.EntityFrameworkCore;
using TaxCalculator.Persistence;
using TaxCalculator.Persistence.Abstractions;
using TaxCalculator.Persistence.Repositories;
using TaxCalculator.Persistence.Seeding;

namespace TaxCalculator.Api.Extensions
{
    public static class DbConfigurationExtensions
    {
        public static void AddInMemoryDatabase(this IServiceCollection serviceCollection, string name)
        {
            serviceCollection.AddDbContext<TaxCalculationDbContext>(opt =>
                opt.UseInMemoryDatabase(name));
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITaxCalculationRulesRepository, TaxCalculationRulesRepository>();
        }

        public static void SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<TaxCalculationDbContext>();
            DbInitializer.SeedData(dbContext);
        }
    }
}
