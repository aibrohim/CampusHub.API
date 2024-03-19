using CampusHub.Context;
using CampusHub.Scheduler;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
      services.AddHostedService<Worker>();
      services.AddAppDbContext();
      services.AddHostedService<EmailSenderWorker>();
    })
    .Build();

host.Run();

