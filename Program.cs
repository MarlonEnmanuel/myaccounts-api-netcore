using Microsoft.AspNetCore.Mvc;
using MyAccounts.AppConfig;
using MyAccounts.Database.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(AppOptions.SetControllerOptions).AddJsonOptions(AppOptions.SetJsonOptions);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(AppOptions.SetSwaggerGen);

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// App services
builder.Services.Configure<ApiBehaviorOptions>(AppOptions.SetApiBehavior);
builder.Services.AddAppContext(builder.Configuration);
builder.Services.AddAppAutentication(builder.Configuration);
builder.Services.AddAppDendencies();

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
