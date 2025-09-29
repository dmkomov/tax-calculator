using Moq;
using TaxCalculator.Application.Abstractions;
using TaxCalculator.Application.Services;
using TaxCalculator.Persistence.Abstractions;
using TaxCalculator.Persistence.Models;

namespace TaxCalculator.Application.Tests
{
    public class TaxCalculationServiceTests
    {
        private ITaxCalculationService _taxCalculationService;

        [SetUp]
        public void Setup()
        {
            var repository = new Mock<ITaxCalculationRulesRepository>();

            repository.Setup(x => x.GetCalculationRules()).Returns(CreateTestRules().AsQueryable());

            _taxCalculationService = new TaxCalculationService(repository.Object);
        }

        [TestCase(0, 0, 0)]
        [TestCase(5000, 5000, 0)]
        [TestCase(8000, 7400, 600)]
        [TestCase(17500, 15000, 2500)]
        [TestCase(25000, 20000, 5000)]
        [TestCase(45000, 32000, 13000)]
        [TestCase(100000, 65000, 35000)]
        
        public async Task CreateTaxReport_CreatesValidReport(double salaryGross, double salaryNet, double taxPaid)
        {
            var taxReport = await _taxCalculationService.CreateTaxReport(salaryGross, CancellationToken.None);

            Assert.That(taxReport.AnnualSalaryGross, Is.EqualTo(salaryGross));
            Assert.That(taxReport.MonthlySalaryGross, Is.EqualTo(salaryGross/12));
            Assert.That(taxReport.AnnualSalaryNet, Is.EqualTo(salaryNet));
            Assert.That(taxReport.MonthlySalaryNet, Is.EqualTo(salaryNet/12));
            Assert.That(taxReport.AnnualTaxPaid, Is.EqualTo(taxPaid));
            Assert.That(taxReport.MonthlyTaxPaid, Is.EqualTo(taxPaid/12));
        }

        [Test]
        public void CreateTaxReport_InvalidSalaryValue_ThrowsException()
        {
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _taxCalculationService.CreateTaxReport(-10000, CancellationToken.None));

            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("The value should not be negative. (Parameter 'grossAnnualSalary')"));
            }
        }

        /// Creates test tax calculation rules.
        private static List<TaxCalculationRule> CreateTestRules()
        {
            return
            [
                new TaxCalculationRule
                {
                    Id = 1, Priority = 1, BandName = "A", AnnualSalaryMin = 0, AnnualSalaryMax = 5000, TaxRate = 0
                },

                new TaxCalculationRule
                {
                    Id = 2, Priority = 2, BandName = "B", AnnualSalaryMin = 5000, AnnualSalaryMax = 20000, TaxRate = 0.2
                },

                new TaxCalculationRule
                {
                    Id = 3, Priority = 3, BandName = "C", AnnualSalaryMin = 20000, AnnualSalaryMax = null, TaxRate = 0.4
                }
            ];
        }
    }
}