﻿using Microsoft.AspNetCore.Mvc;
using MyAccounts.AppConfig.Attributes;
using MyAccounts.AppConfig.JsonConverters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyAccounts.AppConfig
{
    public static class AppOptions
    {
        public static void SetControllerOptions(MvcOptions options)
        {
            options.Filters.Add<AppValidationFilter>();
        }

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

        public static void SetApiBehavior(ApiBehaviorOptions options)
        {
            options.SuppressModelStateInvalidFilter = true;
        }
    }
}
