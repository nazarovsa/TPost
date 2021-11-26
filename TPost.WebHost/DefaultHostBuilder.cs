using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Serilog;
using TPost.Domain;
using TPost.Domain.Publishers;
using TPost.Host.Quartz;

namespace TPost.Host
{
    public sealed class DefaultHostBuilder
    {
        public static IHostBuilder GetBuilder(params string[] args)
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    builder.AddEnvironmentVariables()
                        .AddJsonFile(
                        $"appsettings.{ctx.HostingEnvironment.EnvironmentName.ToLowerInvariant()}.json", true,
                        true);
                })
                .ConfigureServices((ctx, services) =>
                {
                    var configuration = ctx.Configuration;
                    services.AddScoped<IPostPublisher, CompositePostPublisher>();

                    services.AddQuartz(options =>
                    {
                        options.UseMicrosoftDependencyInjectionJobFactory();

                        // these are the defaults
                        options.UseSimpleTypeLoader();
                        options.UseInMemoryStore();
                        options.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 10; });

                        options.ScheduleJob<PostJob>(trigger => trigger
                            .WithIdentity("Post job trigger")
                            .StartNow()
                            .WithSchedule(
                                CronScheduleBuilder.CronSchedule(configuration.GetValue<string>("PostSchedule")))
                        );
                    });

                    // Quartz.Extensions.Hosting allows you to fire background service that handles scheduler lifecycle
                    services.AddQuartzHostedService(options =>
                    {
                        // when shutting down we want jobs to complete gracefully
                        options.WaitForJobsToComplete = true;
                    });
                })
                .UseSerilog((ctx, cfg) =>
                {
                    cfg.Enrich
                        .FromLogContext()
                        .Enrich.WithThreadId()
                        .ReadFrom.Configuration(ctx.Configuration);
                });
        }
    }
}