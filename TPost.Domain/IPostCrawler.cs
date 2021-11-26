using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TPost.Domain
{
    public interface IPostCrawler
    {
        Task<IReadOnlyCollection<IPost>> GetPosts(CancellationToken cancellationToken = default);
    }
}