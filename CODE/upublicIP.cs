using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetPublicIP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Get Public IP";
            string url = "https://api.ipify.org";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string publicIP = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Your public IP address is: {publicIP}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }
            }
        }
    }
}
