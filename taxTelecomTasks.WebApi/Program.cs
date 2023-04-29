using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using AspNetCoreRateLimit;
using taxTelecomTasks.WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

StringController.initSemaphore( builder.Configuration.GetValue<int>("Settings:ParallelLimit"));
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
