using Alerts.Application.Interfaces;
using Alerts.Application.Interfaces.Services;
using Alerts.Persistence;
using Alerts.WebApp;
using Alerts.WebApp.Servives.AlertsService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence();

builder.Services.AddSingleton<IAlertsService, AlertService>();


var scope = builder.Services.BuildServiceProvider().CreateScope();
var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

DatabaseInitializer.Initialize(applicationDbContext);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();