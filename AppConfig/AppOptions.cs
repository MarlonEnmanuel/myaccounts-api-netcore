using Microsoft.AspNetCore.Mvc;
using MyAccounts.AppConfig.JsonConverters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyAccounts.AppConfig
{
    public static class AppOptions
    {
        public static void SetJsonOptions(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
        }

        public static void SetSwaggerGen(SwaggerGenOptions options)
        {
            options.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
            {
                Type = "string",
                Format = AppConstants.DATE_FORMART,
            });
        }
    }
}
