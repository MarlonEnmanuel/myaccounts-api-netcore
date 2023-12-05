using FluentValidation;
using Microsoft.OpenApi.Models;
using MyAccounts.Api.AppConfig;
using MyAccounts.Api.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configuration =>
{
    configuration.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' [espacio] y luego su token en el campo de texto. Ejemplo: 'Bearer 12345abcdef'"
    });
    configuration.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// App libraries
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// App customization
builder.Services.AddAppConfiguration();
builder.Services.AddAppPrincipal();
builder.Services.AddAppFilters();
builder.Services.AddAppContext(builder.Configuration);
builder.Services.AddAppAutentication(builder.Configuration);
builder.Services.AddAppDendencies();

var app = builder.Build();

// Temporaly Configure create database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<MyAccountsContext>()!;
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
