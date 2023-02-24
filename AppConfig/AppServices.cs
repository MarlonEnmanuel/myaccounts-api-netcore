using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAccounts.AppConfig.Filters;
using MyAccounts.AppConfig.JsonConverters;
using MyAccounts.Database.Context;
using MyAccounts.Modules.General;
using MyAccounts.Modules.Payments;
using MyAccounts.Modules.Security;
using MyAccounts.Modules.Shared.Validation;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Principal;
using System.Text;

namespace MyAccounts.AppConfig
{
    public static class AppServices
    {
        public static void AddAppDendencies(this IServiceCollection services)
        {
            // shared
            services.AddScoped<IAppSettings, AppSettings>();
            services.AddScoped<IValidatorService, ValidatorService>();

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
            services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateTimeJsonConverter());
            });

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

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public static void AddAppFilters(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<AppValidationFilter>();
                options.Filters.Add<AppExceptionFilter>();
            });
        }

        public static void AddAppPrincipal(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>()?.HttpContext?.User!);
        }
    }
}
