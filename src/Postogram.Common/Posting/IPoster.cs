using System.Threading;
using System.Threading.Tasks;

namespace Postogram
{
    public interface IPoster
    {
        Task<PostResult> Post(Content content, CancellationToken cancellationToken = default);
    }
}
