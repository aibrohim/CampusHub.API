using System;
using CampusHub.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CampusHub.Scheduler;

public class EmailSenderWorker : BackgroundService
{
	private readonly ILogger<EmailSenderWorker> _logger;
	private readonly IServiceProvider _serviceProvider;
	private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

	public EmailSenderWorker(
	ILogger<EmailSenderWorker> logger,
	IServiceProvider serviceProvider,
	IDbContextFactory<MainDbContext> dbContextFactory
)
	{
		_logger = logger;
		_serviceProvider = serviceProvider;
		_dbContextFactory = dbContextFactory;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			using (var scope = _serviceProvider.CreateScope())
            {

				using var context = await _dbContextFactory.CreateDbContextAsync();
				var buildings = await context.Buildings.Include(b => b.Rooms).ToListAsync();

				foreach (var user in buildings)
				{
					Console.WriteLine(user.Name);
				}
			}

			// Wait for 24 hours until the next execution
			await Task.Delay(3 * 1000, stoppingToken);
		}
	 }
}

