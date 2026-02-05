using InformacioniSistemZU.BusinessModell.RepositoriesBM;
using InformacioniSistemZU.BusinessModell.Services;
using InformacioniSistemZU.DataModel.Repositories;
using InformacioniSistemZU.MainDbContext;
using InformacioniSistemZU.Mapper;
using InformacioniSistemZU.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .WriteTo.File("Logs/InformacioniSistemZU.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Warning()
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
