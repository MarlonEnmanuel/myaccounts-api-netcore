using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace MyAccounts.AppConfig.JsonConverters
{
    public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, AppConstants.DATEONLY_FORMAT);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            var stringDate = value.ToString(AppConstants.DATEONLY_FORMAT);
            writer.WriteStringValue(stringDate);
        }
    }

    public sealed class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (dateString.IsNullOrEmpty()) return null;
            return DateOnly.ParseExact(dateString!, AppConstants.DATEONLY_FORMAT);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            if (value == null) writer.WriteNullValue();
            var stringDate = value!.Value.ToString(AppConstants.DATEONLY_FORMAT);
            writer.WriteStringValue(stringDate);
        }
    }
}
