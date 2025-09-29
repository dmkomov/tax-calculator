namespace TaxCalculator.Domain
{
    /// <summary>
    ///     Represents a tax report. The report is calculated based on the provided gross annual salary and the tax calculation rules.
    /// </summary>
    /// <remarks>
    ///     Tax calculation rules may change eventually.
    /// </remarks>
    public class TaxReport(double annualSalaryGross)
    {
        /// <summary />
        public double AnnualSalaryGross { get; set; } = annualSalaryGross;

        /// <summary />
        public double MonthlySalaryGross => AnnualSalaryGross / 12;

        /// <summary />
        public double AnnualSalaryNet { get; set; }

        /// <summary />
        public double MonthlySalaryNet => AnnualSalaryNet / 12;

        /// <summary />
        public double AnnualTaxPaid { get; set; }

        /// <summary />
        public double MonthlyTaxPaid => AnnualTaxPaid / 12;
    }
}
