using TaxCalculator.Persistence.Models;

namespace TaxCalculator.Persistence.Seeding
{
    public static class DbInitializer
    {
        public static void SeedData(TaxCalculationDbContext dbContext)
        {
            if (dbContext.TaxCalculationRules.Any())
            {
                return;
            }

            // Tax calculation is valid when the rules are configured properly - there are no intersections in tax bands.
            // This may require an additional logic to check in case these rules are provided by a user.
            var rules = new List<TaxCalculationRule>
            {
                new() { Priority = 1, AnnualSalaryMin = 0, AnnualSalaryMax = 5000, BandName = "Band A", TaxRate = 0 },
                new() { Priority = 2, AnnualSalaryMin = 5000, AnnualSalaryMax = 20000, BandName = "Band B", TaxRate = 0.2 },
                new() { Priority = 3, AnnualSalaryMin = 20000, AnnualSalaryMax = null, BandName = "Band C", TaxRate = 0.4 }
            };

            dbContext.TaxCalculationRules.AddRange(rules);
            dbContext.SaveChanges();
        }
    }
}
