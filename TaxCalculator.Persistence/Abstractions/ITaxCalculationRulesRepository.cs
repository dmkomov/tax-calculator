using TaxCalculator.Persistence.Models;

namespace TaxCalculator.Persistence.Abstractions
{
    public interface ITaxCalculationRulesRepository
    {
        IQueryable<TaxCalculationRule> GetCalculationRules();
    }
}
