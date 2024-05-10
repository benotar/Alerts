using System.Text;
using Alerts.Application.Configurations;
using Alerts.Application.Interfaces.Services;
using Alerts.Persistence;
using Alerts.WebApp;
using Alerts.WebApp.Servives.AlertsService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddCustomConfiguration();

builder.Services.AddPersistence();

builder.Services.AddSingleton<IAlertsService, AlertService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var scope = builder.Services.BuildServiceProvider().CreateScope();

var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

DatabaseInitializer.Initialize(applicationDbContext);

var jwtConfiguration = scope.ServiceProvider.GetService<JwtConfiguration>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "MyAlertServer",
        ValidateAudience = true,
        ValidAudience = "MyWpfAuthClient",
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey)),
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();