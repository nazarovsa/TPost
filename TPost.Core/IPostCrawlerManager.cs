using System.Threading;
using System.Threading.Tasks;

namespace TPost.Core
{
    public interface IPostCrawlerManager
    {
        Task RenewPostStore(int count = 10, CancellationToken cancellationToken = default);
    }
}