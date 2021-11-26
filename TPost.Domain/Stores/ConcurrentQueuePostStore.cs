using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TPost.Domain.Stores
{
    public sealed class ConcurrentQueuePostStore : IPostStore
    {
        private readonly ConcurrentQueue<IPost> _queue = new();

        private readonly HashSet<string> _dequeuedKeys = new(StringComparer.InvariantCulture);

        public Task<IPost> GetAndRemoveOne(CancellationToken cancellationToken = default)
        {
            if (_queue.TryDequeue(out var post))
            {
                _dequeuedKeys.Add(post.Key);
                return Task.FromResult(post);
            }

            return Task.FromResult<IPost>(null);
        }

        public Task Add(IPost post, CancellationToken cancellationToken = default)
        {
            if (!_dequeuedKeys.Contains(post.Key))
                _queue.Enqueue(post);

            return Task.CompletedTask;
        }

        public int Count => _queue.Count;
    }
}