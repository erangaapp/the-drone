using DroneAPI.Data;
using DroneAPI.Repositories;
using DroneAPI.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder();
builder.UseEnvironment(EnvironmentName.Development);

builder.ConfigureLogging((context, b) => { b.AddConsole(); });
builder.ConfigureWebJobs(b => {
    b.AddAzureStorageCoreServices();
    //b.AddAzureStorage();
    //b.UseTimers();
});

builder.ConfigureServices(container => {

    container.AddScoped<IDroneService, DroneService>();
    container.AddScoped<IMedicationService, MedicationService>();

    container.AddScoped<IDroneMedicationRepository, DroneMedicationRepository>();
    container.AddScoped<IDroneRepository, DroneRepository>();
    container.AddScoped<IMedicationRepository, MedicationRepository>();
    container.AddScoped<IMedicationRepository, MedicationRepository>();
    container.AddDbContext<DroneAPIDbContext>(options =>
    {
        options.UseSqlServer("DroneAPI");
    });
});

var host = builder.Build();
using (host)
{
    var jobHost = host.Services.GetService(typeof(IJobHost)) as JobHost;
    await host.StartAsync();
    await jobHost.CallAsync("CheckDronesBatteryLevels");
    await host.StopAsync();
}

