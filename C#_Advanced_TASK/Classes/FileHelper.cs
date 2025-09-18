using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    //Create async methods for reading and writing files with proper exception handling.
    internal class FileHelper
    {
        public static async Task WriteTextAsync(string filePath, string content)
        {
            try
            {
                await File.WriteAllTextAsync(filePath, content);
                Console.WriteLine("File written successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
        }

        public static async Task<string> ReadTextAsync(string filePath)
        {
            try
            {
                string content = await File.ReadAllTextAsync(filePath);
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
