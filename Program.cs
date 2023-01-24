using MyAccounts;
using MyAccounts.Database.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom configuration
IoC.Configure(builder);

var app = builder.Build();

// Temporaly Configure create database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<MyAccountsContext>()!;
    context.Database.EnsureDeleted();
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
