using Microsoft.EntityFrameworkCore;
using MyAccounts.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add my accounts context
builder.Services.AddDbContext<MyAccountsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyAccounts")
        ?? throw new InvalidOperationException("Connection string 'Accounts' not found."));
});


var app = builder.Build();

// Configure create database
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

app.UseAuthorization();

app.MapControllers();

app.Run();
