using MediatR;
using TaxCalculator.Domain;

namespace TaxCalculator.Application.Commands
{
    public class CreateTaxReportCommand : IRequest<TaxReport>
    {
        public int GrossAnnualSalary { get; set; }
    }
}
