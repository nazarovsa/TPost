using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPost.Domain;
using TPost.Domain.Crawlers;
using TPost.Domain.Publishers;
using TPost.Domain.Stores;
using TPost.Host.Sample.Crawlers;

namespace TPost.Host.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DefaultHostBuilder.GetBuilder(args)
                .ConfigureServices((ctx, services) =>
                {
                    // Uncomment to add telegram bot publisher
                    // services.AddTelegramBotPublisher(ctx.Configuration);
                    
                    services.AddScoped<IPostCrawler, AnekdotMeCrawler>();
                    services.AddScoped<IPostCrawler, AnekdotRuCrawler>();
                    services.AddScoped<IPostCrawlerManager, DefaultPostCrawlerManager>();

                    services.AddSingleton<IPostStore, ConcurrentQueuePostStore>();

                    // Uncomment to use telegram publisher
                    // services.AddScoped<IPostPublisherTransport, TelegramPostPublisher>();
                    services.AddScoped<IPostPublisherTransport, ConsolePostPublisher>();
                    
                    // Used just for current sample to make crawlers work
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                })
                .Build()
                .Run();
        }
    }
}