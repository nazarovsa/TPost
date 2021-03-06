using System.Threading;
using System.Threading.Tasks;

namespace TPost.Core
{
    public interface IPostStore
    {
        Task<IPost> GetAndRemoveOne(CancellationToken cancellationToken = default);

        Task Add(IPost post, CancellationToken cancellationToken = default);
    }
}