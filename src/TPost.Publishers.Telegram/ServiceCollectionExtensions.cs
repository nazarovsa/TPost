using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TPost.Core;

namespace TPost.Publishers.Telegram
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure and register Telegram bot
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        public static IServiceCollection AddTelegramBotPublisher(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.Configure<TelegramPublisherOptions>(configuration.GetSection(nameof(TelegramPublisherOptions)));
            
            services.AddScoped<IPostPublisher, TelegramPostPublisher>();
            services.AddTransient<ITelegramBotClient, TelegramBotClient>(c =>
                new TelegramBotClient(c.GetRequiredService<IOptions<TelegramPublisherOptions>>().Value.Token,
                    c.GetRequiredService<IHttpClientFactory>().CreateClient()));
            
            return services;
        }
    }
}