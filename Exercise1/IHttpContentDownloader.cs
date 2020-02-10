using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise1
{
    public interface IHttpContentDownloader
    {
        Task PrintContentLengthAsync(IList<string> urlList, char cancelKey);
    }
}
