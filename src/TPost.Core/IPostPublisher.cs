using System.Threading;
using System.Threading.Tasks;

namespace TPost.Core
{
    public interface IPostPublisher
    {
        Task Publish(IPost post, CancellationToken cancellationToken = default);
    }
}
