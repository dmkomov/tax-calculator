using TaxCalculator.Persistence.Abstractions;
using TaxCalculator.Persistence.Models;

namespace TaxCalculator.Persistence.Repositories
{
    public class TaxCalculationRulesRepository(TaxCalculationDbContext dbContext) : ITaxCalculationRulesRepository
    {
        public IQueryable<TaxCalculationRule> GetCalculationRules()
        {
            return dbContext.TaxCalculationRules.OrderBy(x => x.Priority);
        }
    }
}
