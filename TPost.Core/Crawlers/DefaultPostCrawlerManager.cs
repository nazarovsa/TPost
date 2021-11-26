using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TPost.Core.Crawlers
{
    public sealed class DefaultPostCrawlerManager : IPostCrawlerManager
    {
        private readonly IPostStore _store;
        private readonly IReadOnlyCollection<IPostCrawler> _crawlers;

        public DefaultPostCrawlerManager(IPostStore store, IEnumerable<IPostCrawler> crawlers)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _crawlers = crawlers?.ToArray() ?? throw new ArgumentNullException(nameof(crawlers));
        }

        public async Task RenewPostStore(int count = 10, CancellationToken cancellationToken = default)
        {
            var newPosts = new List<IPost>();
            foreach (var postCrawler in _crawlers)
            {
                var posts = await postCrawler.GetPosts(cancellationToken);
                newPosts.AddRange(posts.Take(count / _crawlers.Count));
            }

            var random = new Random();
            newPosts = newPosts
                .OrderBy(x => random.Next())
                .ToList();

            foreach (var newPost in newPosts)
                await _store.Add(newPost, cancellationToken);
        }
    }
}