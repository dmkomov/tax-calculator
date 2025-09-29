using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Persistence.Models
{
    public class TaxCalculationRule
    {
        /// <summary>
        ///     Unique identifier.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///     Represents the order in which rules must be applied.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        ///     Min value of a salary for the rule.
        /// </summary>
        public long AnnualSalaryMin { get; set; }

        /// <summary>
        ///     Max value of a salary for the rule.
        /// </summary>
        /// <remarks>
        ///     May be NULL in case it is the rule with no salary ceiling.
        /// </remarks>
        public long? AnnualSalaryMax { get; set; }

        /// <summary>
        ///     A tax band name.
        /// </summary>
        [MaxLength(256)]
        public string BandName { get; set; } = string.Empty;

        /// <summary>
        ///     A rate used to calculate taxes.
        /// </summary>
        /// <remarks>
        ///     Should be a value between 0 and 1 where 0 means 0% tax rate and 1 - 100%.
        /// </remarks>
        public double TaxRate { get; set; }
    }
}
