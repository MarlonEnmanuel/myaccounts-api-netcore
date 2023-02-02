using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAccounts.Database.Context;
using MyAccounts.Modules.General;
using MyAccounts.Modules.Payments;
using MyAccounts.Modules.Security;
using System.Text;

namespace MyAccounts.AppConfig
{
    public static class AppServices
    {
        public static void AddAppDendencies(this IServiceCollection services)
        {
            services.AddScoped<IAppSettings, AppSettings>();
            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<ISecurityService, SecurityService>();
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
    }
}
