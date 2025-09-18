using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Build an async file downloader that downloads multiple files concurrently with progress reporting.
    internal class FileDownloader
    {
        private readonly HttpClient _httpClient = new HttpClient();

        
        public async Task DownloadFileAsync(string url, string destination, IProgress<double>? progress = null)
        {
            try
            {
                var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var totalRead = 0L;
                var buffer = new byte[8192];

                using var stream = await response.Content.ReadAsStreamAsync();
                using var fileStream = System.IO.File.Create(destination);

                int read;
                while ((read = await stream.ReadAsync(buffer)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, read));
                    totalRead += read;
                    if (totalBytes > 0 && progress != null)
                    {
                        progress.Report((double)totalRead / totalBytes * 100);
                    }
                }

                Console.WriteLine($"Downloaded {destination}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading {destination}: {ex.Message}");
            }
        }
        public async Task DownloadFilesAsync(Dictionary<string, string> files) 
        {
            var tasks = new List<Task>();
            foreach (var kvp in files)
            {
                var progress = new Progress<double>(p => Console.WriteLine($"{kvp.Value}: {p:F2}%"));
                tasks.Add(DownloadFileAsync(kvp.Key, kvp.Value, progress));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("All downloads completed.");
        }
    }
}
