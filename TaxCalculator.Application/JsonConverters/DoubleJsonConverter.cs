using System.Text.Json.Serialization;
using System.Text.Json;

namespace TaxCalculator.Application.JsonConverters
{
    /// <summary>
    ///     Json serializer for double values.
    /// </summary>
    public class DoubleJsonConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("F2"));
        }
    }
}
