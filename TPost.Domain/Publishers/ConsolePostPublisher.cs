using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPost.Domain.Publishers
{
    public class ConsolePostPublisher : IPostPublisherTransport
    {
        public Task Publish(IPost post, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Publishing post to Console: {post.Content}");
            return Task.CompletedTask;
        }
    }
}