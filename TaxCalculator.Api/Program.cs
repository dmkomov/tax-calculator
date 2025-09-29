using TaxCalculator.Api.Extensions;
using TaxCalculator.Application.Abstractions;
using TaxCalculator.Application.Commands;
using TaxCalculator.Application.Services;

namespace TaxCalculator.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<ITaxCalculationService, TaxCalculationService>();

            // Configure JSON
            builder.Services.AddControllers().AddJsonConfiguration();

            // Add MediatR
            builder.Services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTaxReportCommand).Assembly));

            // Configure EF with in-memory database
            builder.Services.AddInMemoryDatabase(name: "TaxCalculationDatabase");
            builder.Services.AddRepositories();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // Seed data into the DB
            app.SeedData();

            app.Run();
        }
    }
}
