
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;

namespace Exercise1
{
    /// <summary>
    /// Class to Download & Print the content length
    /// </summary>
    public class HttpContentLoader : IHttpContentLoader
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpContentLoader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// This Method takes list of URLs and print the sum of content length
        /// </summary>
        /// <param name="urlList"></param>
        /// <returns></returns>
        public async Task PrintContentLengthAsync(IList<string> urlList)
        {
            CancellationTokenSource cancelToken = new CancellationTokenSource();
            Console.WriteLine("Press 'c' to cancel.");
            
            // Run the Cancel token key task
            _ = Task.Factory.StartNew(() =>
              {
                  if (Console.ReadKey().KeyChar == 'c')
                      cancelToken.Cancel();
              });
            try
            {
                var contentLengthQueue = new ConcurrentQueue<long>();
                var tasks = urlList
                    .Select(async s =>
                    {
                        contentLengthQueue.Enqueue(
                            await GetContentLengthAsync(s, cancelToken.Token)
                            );
                    });

                // Wait for all tasks to be completed
                await Task.WhenAll(tasks);

                // Dequeue all items 
                long totalLength = 0;
                while (contentLengthQueue.TryDequeue(out long itemLength))
                {
                    totalLength += itemLength;
                }
                Console.WriteLine($"The Length of all resources = {totalLength}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nDownload Canceled.");
            }
            catch (Exception)
            {
                Console.WriteLine("\nDownload Failed.");
            }            
        }

        /// <summary>
        /// This method returns the length of downloaded content for given the URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<long> GetContentLengthAsync(string url, CancellationToken cancellationToken)
        {
            using(var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(url, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.Headers.ContentLength ?? 0;
                }
            }
            return 0;
        }
    }
}
