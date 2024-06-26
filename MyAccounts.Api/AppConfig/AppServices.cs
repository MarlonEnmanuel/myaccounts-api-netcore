﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAccounts.Api.AppConfig.Filters;
using MyAccounts.Api.AppConfig.JsonConverters;
using MyAccounts.Api.Database;
using MyAccounts.Api.Modules.General;
using MyAccounts.Api.Modules.Payments;
using MyAccounts.Api.Modules.Security;
using MyAccounts.Api.Modules.Shared;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Principal;
using System.Text;

namespace MyAccounts.Api.AppConfig
{
    public static class AppServices
    {
        public static void AddAppDendencies(this IServiceCollection services)
        {
            // shared
            services.AddScoped<IAppSettings, AppSettings>();
            services.AddScoped<IDtoService, DtoService>();

            // security module
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IPrincipalService, PrincipalService>();

            // others modules
            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<IPaymentService, PaymentService>();
        }

        public static void AddAppContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("MyAccounts")
                                    ?? throw new InvalidOperationException("Connection string 'Accounts' not found.");

            services.AddDbContext<MyAccountsContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void AddAppAutentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtKey = configuration["Jwt:Key"]
                            ?? throw new InvalidOperationException("Setting string 'Accounts' not found."); ;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                };
            });
        }

        public static void AddAppConfiguration(this IServiceCollection services)
        {
            // json data type converters
            services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateTimeJsonConverter());
            });

            // swagger map types
            services.Configure<SwaggerGenOptions>(options =>
            {
                options.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Format = $"\"{AppConstants.DATEONLY_FORMAT}\"",
                });
                options.MapType<DateTime>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Format = $"\"{AppConstants.DATETIME_FORMAT}\"",
                });
            });

            // global configurations
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // no validar atributos de DTOs
            });
        }

        public static void AddAppFilters(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
            });
        }

        public static void AddAppPrincipal(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>()?.HttpContext?.User!);
        }
    }
}
