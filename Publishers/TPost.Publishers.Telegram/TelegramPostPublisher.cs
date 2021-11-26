using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TPost.Domain;

namespace TPost.Publishers.Telegram
{
    public sealed class TelegramPostPublisher : IPostPublisher
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly TelegramPublisherOptions _telegramPublisherOptions;

        public TelegramPostPublisher(ITelegramBotClient telegramBotClient,
            IOptionsSnapshot<TelegramPublisherOptions> telegramPublisherOptions)
        {
            _telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
            _telegramPublisherOptions = telegramPublisherOptions?.Value ??
                                        throw new ArgumentNullException(nameof(telegramPublisherOptions));
        }

        public async Task Publish(IPost post, CancellationToken cancellationToken = default)
        {
            foreach (var receiverId in _telegramPublisherOptions.ReceiverIds)
            {
                await _telegramBotClient.SendTextMessageAsync(
                    receiverId,
                    post.Content,
                    _telegramPublisherOptions.ParseMode,
                    cancellationToken: cancellationToken);
            }
        }
    }
}