using CurrencyConverter.BuisinessLayer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .AddJsonFile("exchangeRates.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables("USD_TO_INR")
       .AddUserSecrets(Assembly.GetExecutingAssembly());
// Add services to the container.
builder.Services.Configure<ExchangeRates>(
    builder.Configuration.GetSection(ExchangeRates.exchangeValues));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
