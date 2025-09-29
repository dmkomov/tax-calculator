using TaxCalculator.Application.JsonConverters;

namespace TaxCalculator.Api.Extensions
{
    /// <summary>
    ///     Adds output JSON serializer settings.
    /// </summary>
    public static class JsonConfigurationExtensions
    {
        public static void AddJsonConfiguration(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DoubleJsonConverter());
            });
        }
    }
}
