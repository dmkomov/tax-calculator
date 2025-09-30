using TaxCalculator.Persistence.Models;

namespace TaxCalculator.Persistence.Abstractions
{
    public interface ITaxCalculationRulesRepository
    {
        /// Returns all tax calculation rules.
        IQueryable<TaxCalculationRule> GetCalculationRules();
    }
}
