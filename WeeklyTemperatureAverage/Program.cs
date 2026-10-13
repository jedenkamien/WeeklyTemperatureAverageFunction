using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



/*DISCLAIMER: This app has been deliberately designed for its scoped functionality and it has not been designed with best coding practices.

For an example of a well architectured solution, please head to the Event-Driven-Route-Publishing-Architecture repository.*/





var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
