using baevii_back_end.Configuration;
using baevii_back_end.DTO;
using baevii_back_end.Models;
using baevii_back_end.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PrivyConfiguration>(builder.Configuration.GetSection("Privy"));

builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<BeaviiDbContext>(options =>
    options.UseSqlServer(builder.Configuration["Mssql:ConnStr"], o =>
    {
        o.EnableRetryOnFailure();
        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    }));

builder.Configuration["Serilog:WriteTo:2:Args:connectionString"] = String.Format(builder.Configuration["Serilog:WriteTo:2:Args:connectionString"], builder.Configuration["AzureBlobStorageAccountKey"]);
builder.Services.AddSerilog(configureLogger =>
{
    configureLogger.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddHttpClient("Privy", client =>
{
    client.BaseAddress = new Uri("https://api.privy.io/v1/");
});

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All; // Log everything: headers, body, etc.
    logging.RequestBodyLogLimit = 4096; // Limit for request body logging (bytes)
    logging.ResponseBodyLogLimit = 4096; // Limit for response body logging (bytes)
    logging.CombineLogs = true; // Combine request/response logs into one entry
});

var app = builder.Build();

app.UseHttpLogging();

//swagger
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.MapGet("/readyness", () => Results.Ok());
app.MapGet("/liveness", () => Results.Ok());

app.MapGet("/wallets", (BeaviiDbContext dbContext) =>
{
    return Results.Ok(dbContext.walletInfos);
});

app.MapPost("/privy", (PrivyWebhook privyWebhook, ILogger<Program> logger) =>
{
    logger.LogInformation($"msg = {privyWebhook.message} / type = {privyWebhook.type}");
});

await app.RunAsync();