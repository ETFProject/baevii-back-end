using baevii_back_end;
using baevii_back_end.Configuration;
using baevii_back_end.DTO;
using baevii_back_end.Models;
using baevii_back_end.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin() // Allows any origin
                  .AllowAnyMethod()  // Allows any HTTP method (GET, POST, PUT, DELETE, etc.)
                  .AllowAnyHeader(); // Allows any HTTP headers
        });
});

builder.Services.Configure<PrivyConfiguration>(builder.Configuration.GetSection("Privy"));
builder.Services.AddTransient<PrivyAuthHandler>();

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
})
.AddHttpMessageHandler<PrivyAuthHandler>(); ;

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

// Use CORS middleware - This must be placed after UseRouting and before UseAuthorization (if present) and endpoint mapping.
// In Minimal APIs, the routing middleware is implicitly added.
app.UseCors("AllowAllOrigins"); // Apply the "AllowAllOrigins" policy globally [1][5]

//app.UseHttpsRedirection();

app.MapGet("/readyness", () => Results.Ok());
app.MapGet("/liveness", () => Results.Ok());

app.MapGet("/wallets", async (BeaviiDbContext dbContext, IHttpClientFactory httpClientFactory) =>
{
    return Results.Ok(dbContext.walletInfos);
});

app.MapPost("/privy", (PrivyWebhook privyWebhook, ILogger<Program> logger) =>
{
    logger.LogInformation($"Privy MsgType {privyWebhook.type} received");
    switch (privyWebhook.type)
    {
        case "user.wallet_created":
            break;
        case "user.created":
            break;
        default:
            logger.LogWarning($"Unimplemented msg received (type {privyWebhook.type}");
            break;
    }
});

//HttpClient privyClient = httpClientFactory.CreateClient("Privy");
//HttpResponseMessage responseMessage = await privyClient.GetAsync("wallets");
//string content = await responseMessage.Content.ReadAsStringAsync();

await app.RunAsync();