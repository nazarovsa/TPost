using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using TPost.Core;

namespace TPost.Hosting.Quartz
{
    public sealed class PostJob : IJob
    {
        private readonly IPostStore _store;
        private readonly IPostCrawlerManager _crawlerManager;
        private readonly IPostPublisher _postPublisher;
        private readonly ILogger<PostJob> _logger;

        public PostJob(ILogger<PostJob> logger,
            IPostStore store,
            IPostCrawlerManager crawlerManager,
            IPostPublisher postPublisher)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _crawlerManager = crawlerManager ?? throw new ArgumentNullException(nameof(crawlerManager));
            _postPublisher = postPublisher ?? throw new ArgumentNullException(nameof(postPublisher));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var post = await _store.GetAndRemoveOne(CancellationToken.None);

            if (post == null)
            {
                await _crawlerManager.RenewPostStore();
                post = await _store.GetAndRemoveOne(CancellationToken.None);
            }

            await _postPublisher.Publish(post);
            _logger.LogInformation("Post published: {@post}", post);
        }
    }
}