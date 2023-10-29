using FluentValidation;
using MyAccounts.Api.AppConfig;
using MyAccounts.Api.Database.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
