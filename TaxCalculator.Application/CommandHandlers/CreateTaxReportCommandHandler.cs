using MediatR;
using TaxCalculator.Application.Abstractions;
using TaxCalculator.Application.Commands;
using TaxCalculator.Domain;

namespace TaxCalculator.Application.CommandHandlers
{
    public class CreateTaxReportCommandHandler(ITaxCalculationService taxCalculationService)
        : IRequestHandler<CreateTaxReportCommand, TaxReport>
    {
        public async Task<TaxReport> Handle(CreateTaxReportCommand request, CancellationToken cancellationToken)
        {
            return await taxCalculationService.CreateTaxReport(request.GrossAnnualSalary, cancellationToken);
        }
    }
}
