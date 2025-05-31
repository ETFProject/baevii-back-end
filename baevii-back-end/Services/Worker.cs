
namespace baevii_back_end.Services;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1);
        logger.LogInformation("Logging Works!");
    }
}
