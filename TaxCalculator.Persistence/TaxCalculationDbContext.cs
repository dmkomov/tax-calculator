using Microsoft.EntityFrameworkCore;
using TaxCalculator.Persistence.Models;

namespace TaxCalculator.Persistence
{
    public class TaxCalculationDbContext(DbContextOptions<TaxCalculationDbContext> options) : DbContext(options)
    {
        public DbSet<TaxCalculationRule> TaxCalculationRules { get; set; }
    }
}
