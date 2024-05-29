using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

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

            if (IsPrivateIP(ip))
            {
                Console.WriteLine("This is a private IP address and cannot be geolocated using public services.");
            }
            else
            {
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

                        if (ipInfo != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"Country: {ipInfo.Country ?? "N/A"}");
                            Console.WriteLine($"City: {ipInfo.City ?? "N/A"}");
                            Console.WriteLine($"Region: {ipInfo.Region ?? "N/A"}");
                            Console.WriteLine($"Coordinates: {ipInfo.Loc ?? "N/A"}");
                            Console.WriteLine($"ASN: {ipInfo.Org ?? "N/A"}");
                            Console.WriteLine($"Postal Code: {ipInfo.Postal ?? "N/A"}");

                            if (!string.IsNullOrEmpty(ipInfo.Loc))
                            {
                                string[] Coords = ipInfo.Loc.Split(',');
                                Console.WriteLine($"Google Maps: https://www.google.com/maps/?q={Coords[0]},{Coords[1]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data received from the API.");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"HTTP Request Error: {ex.Message}");
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected Error: {ex.Message}");
                    }
                }
            }
        }

        static bool IsPrivateIP(string ipAddress)
        {
            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            // Check for 10.x.x.x
            if (octets[0] == 10)
                return true;

            // Check for 172.16.x.x to 172.31.x.x
            if (octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31)
                return true;

            // Check for 192.168.x.x
            if (octets[0] == 192 && octets[1] == 168)
                return true;

            return false;
        }
    }
}
