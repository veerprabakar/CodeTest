using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Add Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddScoped<IHttpContentDownloader, HttpContentDownloader>()
                .AddHttpClient()
                .BuildServiceProvider();

            var contentService = serviceProvider.GetService<IHttpContentDownloader>();

            // print the content length 
            char cancelKey = 'c';
            Console.WriteLine($"Press {cancelKey} to cancel.");
            await contentService.PrintContentLengthAsync(GetUrls(), cancelKey);
        }

        /// <summary>
        /// List of URLs
        /// </summary>
        /// <returns></returns>
        private static IList<string>  GetUrls()
        {
            var urls = new List<string>
            {
                "https://docs.microsoft.com/en-us/previous-versions/azure/dn151108",
                "https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-graph-api",
                "https://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }
    }
}
