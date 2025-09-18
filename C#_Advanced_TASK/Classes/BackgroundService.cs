using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    //Design an async background service that processes queued items with proper error handling and retry logic.
    internal class BackgroundService
    {
        private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private bool _running = false;

        public void Enqueue(string item)
        {
            _queue.Enqueue(item);
            Console.WriteLine($"Enqueued: {item}");
        }

        public async Task StartProcessingAsync()
        {
            _running = true;
            while (_running)
            {
                if (_queue.TryDequeue(out var item))
                {
                    await ProcessItemWithRetryAsync(item, 3); 
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }
        public void Stop()
        {
            _running = false;
        }

        private async Task ProcessItemWithRetryAsync(string item, int maxRetries)
        {
            int attempt = 0;
            while (attempt < maxRetries)
            {
                try
                {
                    attempt++;
                    await ProcessItemAsync(item);
                    Console.WriteLine($"Processed: {item}");
                    return; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {item}, attempt {attempt}: {ex.Message}");
                    await Task.Delay(1000); 
                }
            }

            Console.WriteLine($"Failed to process {item} after {maxRetries} attempts.");
        }

        private async Task ProcessItemAsync(string item)
        {
            
            await Task.Delay(500);
            if (new Random().Next(0, 3) == 0) 
                throw new Exception("Random failure");
        }
    }
}
