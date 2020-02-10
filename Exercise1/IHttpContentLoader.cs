using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise1
{
    public interface IHttpContentLoader
    {
        Task PrintContentLengthAsync(IList<string> urlList);
    }
}
