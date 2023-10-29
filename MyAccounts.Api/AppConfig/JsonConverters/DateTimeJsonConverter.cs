using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyAccounts.Api.AppConfig.JsonConverters
{
    public sealed class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString()!, AppConstants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var stringDate = value.ToString(AppConstants.DATETIME_FORMAT);
            writer.WriteStringValue(stringDate);
        }
    }

    public sealed class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (dateString.IsNullOrEmpty()) return null;
            return DateTime.ParseExact(dateString!, AppConstants.DATEONLY_FORMAT, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null) writer.WriteNullValue();
            var stringDate = value!.Value.ToString(AppConstants.DATETIME_FORMAT);
            writer.WriteStringValue(stringDate);
        }
    }
}
