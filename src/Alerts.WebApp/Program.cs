using Alerts.Application.Interfaces;
using Alerts.Application.Interfaces.Services;
using Alerts.Persistence;
using Alerts.WebApp.Servives.AlertsService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

 var scope = builder.Services.BuildServiceProvider().CreateScope();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAlertsService, AlertService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));
});


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