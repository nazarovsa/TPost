using System.Threading;
using System.Threading.Tasks;

namespace TPost.Domain
{
    public interface IPostCrawlerManager
    {
        Task RenewPostStore(int count = 10, CancellationToken cancellationToken = default);
    }
}