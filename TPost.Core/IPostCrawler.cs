using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TPost.Core
{
    public interface IPostCrawler
    {
        Task<IReadOnlyCollection<IPost>> GetPosts(CancellationToken cancellationToken = default);
    }
}