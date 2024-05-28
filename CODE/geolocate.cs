using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IPaddress
{
    public class Data
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Loc { get; set; }
        public string Org { get; set; }
        public string Postal { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Geolocate";
            Console.Write("Enter IP address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("[+] Request Successfully Made");
                    string responseData = await response.Content.ReadAsStringAsync();
                    Data ipInfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    Console.WriteLine($"Country: {ipInfo.Country}");
                    Console.WriteLine($"City: {ipInfo.City}");
                    Console.WriteLine($"Region: {ipInfo.Region}");
                    Console.WriteLine($"Coordinates: {ipInfo.Loc}");
                    Console.WriteLine($"ASN: {ipInfo.Org}");
                    Console.WriteLine($"Postal Code: {ipInfo.Postal}");

                    string[] Coords =  ipInfo.Loc.Split(',');
                    Console.WriteLine($"Google Maps: https://www.google.com/maps/?q={Coords[0]},{Coords[1]}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
