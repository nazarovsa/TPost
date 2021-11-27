using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPost.Core;
using TPost.Core.Crawlers;
using TPost.Core.Publishers;
using TPost.Core.Stores;
using TPost.Host.Sample.Crawlers;
using TPost.Hosting;

namespace TPost.Host.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = TPostHostBuilder.GetBuilder(args)
                .ConfigureServices((ctx, services) =>
                {
                    // Uncomment to add telegram bot publisher
                    // services.AddTelegramBotPublisher(ctx.Configuration);

                    services.AddScoped<IPostCrawler, AnekdotMeCrawler>();
                    services.AddScoped<IPostCrawler, AnekdotRuCrawler>();
                    services.AddScoped<IPostCrawlerManager, DefaultPostCrawlerManager>();

                    services.AddSingleton<IPostStore, ConcurrentQueuePostStore>();

                    services.AddScoped<IPostPublisherTransport, ConsolePostPublisher>();

                    // Used just for current sample to make crawlers work
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                })
                .Build();
            
                host.Run();
        }
    }
}