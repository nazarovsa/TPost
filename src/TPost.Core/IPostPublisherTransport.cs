using System.Threading;
using System.Threading.Tasks;

namespace TPost.Core
{
    public interface IPostPublisherTransport
    {
        Task Publish(IPost post, CancellationToken cancellationToken = default);
    }
}