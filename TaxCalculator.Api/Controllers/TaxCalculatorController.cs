using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.Commands;
using TaxCalculator.Domain;

namespace TaxCalculator.Api.Controllers
{
    /// <summary>
    ///     Responsible for calculating annual/month tax payments.
    /// </summary>
    // TODO: add versioning
    [Route("api/tax-calculator")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Returns a tax report according to the provided annual salary value.
        /// </summary>
        [HttpGet]
        public async Task<TaxReport> GetTaxReport(
            [Range(0, int.MaxValue,
                ErrorMessage = "The salary value should be greater than or equal to 0.")]
            int salary)
        {
            return await _mediator.Send(new CreateTaxReportCommand { GrossAnnualSalary = salary });
        }

        /// <summary>
        ///     Returns a tax report according to the provided annual salary value.
        /// </summary>
        /// <remarks>
        ///     Does not create any resource in the DB in the current implementation.
        ///     More likely to be a GET method. Created as POST according to the task.
        /// </remarks>
        [HttpPost]
        public async Task<TaxReport> CreateTaxReport([Range(0, int.MaxValue,
                ErrorMessage = "The salary value should be greater than or equal to 0.")]
            int salary)
        {
            return await _mediator.Send(new CreateTaxReportCommand { GrossAnnualSalary = salary });
        }
    }
}
