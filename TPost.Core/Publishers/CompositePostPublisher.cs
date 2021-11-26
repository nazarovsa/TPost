using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TPost.Core.Publishers
{
    public sealed class CompositePostPublisher : IPostPublisher
    {
        private readonly ILogger<CompositePostPublisher> _logger;
        private readonly IEnumerable<IPostPublisherTransport> _publishers;

        public CompositePostPublisher(ILogger<CompositePostPublisher> logger,
            IEnumerable<IPostPublisherTransport> publishers)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishers = publishers ?? throw new ArgumentNullException(nameof(publishers));
        }

        public async Task Publish(IPost post, CancellationToken cancellationToken = default)
        {
            foreach (var publisher in _publishers)
            {
                try
                {
                    await publisher.Publish(post, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to publish {@post} via {@publisherType}", post, publisher.GetType());
                }
            }
        }
    }
}