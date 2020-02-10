using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // add DI
            var serviceProvider = new ServiceCollection()
                .AddScoped<IHttpContentLoader, HttpContentLoader>()
                .AddHttpClient()
                .BuildServiceProvider();

            var contentService = serviceProvider.GetService<IHttpContentLoader>();

            // print the content length 
            await contentService.PrintContentLengthAsync(GetUrls());
        }

        private static IList<string>  GetUrls()
        {
            var urls = new List<string>
            {
                "https://msdn.microsoft.com/library/aa578028.aspx",
                "https://msdn.microsoft.com/library/ms404677.aspx",
                "https://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }
    }
}
