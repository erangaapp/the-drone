using DroneAPI.Data;
using Models = DroneAPI.Models;
using DroneAPI.Repositories;
using DroneAPI.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using DroneAPI.Models;
using DroneAPI.Models.ModelValidators;

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<DroneRegisterModel>, DroneRegisterModelValidator>();
builder.Services.AddScoped<IValidator<LoadDroneWithMedicationsModel>, LoadDroneWithMedicationsModelValidator>();

builder.Services.AddScoped<IDroneService, DroneService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();

builder.Services.AddScoped<IDroneMedicationRepository, DroneMedicationRepository>();
builder.Services.AddScoped<IDroneRepository, DroneRepository>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DroneAPIDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DroneAPI"));
});


var app = builder.Build();

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
