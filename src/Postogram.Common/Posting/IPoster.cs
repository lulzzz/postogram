using System.Threading.Tasks;

namespace Postogram
{
    public interface IPoster
    {
        Task<PostResult> Post(Content content);
    }
}
