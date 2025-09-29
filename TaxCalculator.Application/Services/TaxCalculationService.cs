using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.Abstractions;
using TaxCalculator.Domain;
using TaxCalculator.Persistence.Abstractions;
using TaxCalculator.Persistence.Extensions;
using TaxCalculator.Persistence.Models;
namespace TaxCalculator.Application.Services
{
    public class TaxCalculationService(ITaxCalculationRulesRepository taxCalculationRulesRepository)
        : ITaxCalculationService
    {
        private List<TaxCalculationRule>? _taxCalculationRules;

        public async Task<TaxReport> CreateTaxReport(double grossAnnualSalary, CancellationToken cancellationToken)
        {
            if (grossAnnualSalary < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(grossAnnualSalary), "The value should not be negative.");
            }
            // Rules may be read only once during a startup into a singleton service.
            // In this case it's made to emulate some async operation.
            _taxCalculationRules =
                await taxCalculationRulesRepository.GetCalculationRules().ToListAsyncSafe(cancellationToken);

            var taxReport = new TaxReport(grossAnnualSalary);

            taxReport.AnnualTaxPaid = CalculateAnnualTax(taxReport.AnnualSalaryGross);
            taxReport.AnnualSalaryNet = CalculateAnnualNetSalary(taxReport.AnnualSalaryGross, taxReport.AnnualTaxPaid);

            return taxReport;
        }

        private double CalculateAnnualTax(double grossAnnualSalary)
        {
            var taxToPay = 0.0;

            // No rules - no taxes.
            if (_taxCalculationRules is null or { Count: 0 })
            {
                return 0;
            }

            foreach (var rule in _taxCalculationRules)
            {
                if (rule.AnnualSalaryMax != null && grossAnnualSalary > rule.AnnualSalaryMax)
                {
                    taxToPay += (rule.AnnualSalaryMax.Value - rule.AnnualSalaryMin) * rule.TaxRate;
                }
                else
                {
                    taxToPay += (grossAnnualSalary - rule.AnnualSalaryMin) * rule.TaxRate;
                    return taxToPay;
                }
            }

            return taxToPay;
        }

        private static double CalculateAnnualNetSalary(double grossAnnualSalary, double annualTaxPaid)
        {
            return grossAnnualSalary - annualTaxPaid;
        }
    }
}
