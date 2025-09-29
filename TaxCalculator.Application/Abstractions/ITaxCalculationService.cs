using TaxCalculator.Domain;

namespace TaxCalculator.Application.Abstractions
{
    public interface ITaxCalculationService
    {
        Task<TaxReport> CreateTaxReport(double grossAnnualSalary, CancellationToken cancellationToken);
    }
}
