using InformacioniSistemZU.AppSettingsJson;
using InformacioniSistemZU.BusinessModell.RepositoriesBM;
using InformacioniSistemZU.BusinessModell.Services;
using InformacioniSistemZU.DataModel.Repositories;
using InformacioniSistemZU.MainDbContext;
using InformacioniSistemZU.Mapper;
using InformacioniSistemZU.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .WriteTo.File("Logs/InformacioniSistemZU_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConn")));

builder.Services.AddScoped<ILekarRepository, LekarRepository>();
builder.Services.AddScoped<IPacijentRepository, PacijentRepository>();
builder.Services.AddScoped<ILekarService, LekarService>();
builder.Services.AddScoped<IPacijentService, PacijentService>();
builder.Services.AddScoped<ISpecijalnostRepository, SpecijalnostRepository>();
builder.Services.AddScoped<IPregledRepository, PregledRepository>();
builder.Services.AddScoped<IPregledService, PregledService>();

builder.Services.AddAutoMapper(ops => ops.AddProfile<MapperProfiles>());


/*
builder.Services.AddHttpClient<IProveraAktivnostiLekaraService, ProveraAktivnostiLekaraService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7249/");
    client.Timeout = TimeSpan.FromSeconds(10);
});*/

var externalSettings = builder.Configuration.GetSection("ExternalServiceSettings");
builder.Services.Configure<ExternalServiceSettings>(externalSettings);

builder.Services.AddHttpClient<IProveraAktivnostiLekaraService, ProveraAktivnostiLekaraService>((ServiceProvider, client) =>
{
    var settings = externalSettings.Get<ExternalServiceSettings>();
    client.BaseAddress = new Uri(settings.BaseUri);
    client.Timeout = TimeSpan.FromMilliseconds(settings.Timeout);
});

    


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
