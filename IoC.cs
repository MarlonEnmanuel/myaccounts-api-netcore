using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAccounts.Database.Context;
using MyAccounts.Modules.General;
using MyAccounts.Modules.Security;
using System.Text;

namespace MyAccounts
{
    public class IoC
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            ConfigureServices(builder);
            ConfigureContext(builder);
            ConfigureAutentication(builder);
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<GeneralService, GeneralService>();
            builder.Services.AddScoped<ISecurityService, SecurityService>();
        }

        private static void ConfigureContext(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("MyAccounts")
                                    ?? throw new InvalidOperationException("Connection string 'Accounts' not found.");

            builder.Services.AddDbContext<MyAccountsContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void ConfigureAutentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                };
            });
        }
    }
}
