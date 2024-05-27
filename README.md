# Geolocate IP Address
This C# console application fetches and displays geolocation information for a specified IP address using the `ipinfo.io` API. By entering an IP address, users can retrieve details such as the country, city, region, coordinates, organization, and postal code associated with the IP. Additionally, the application provides a Google Maps link for visualizing the location. This tool is useful for network administrators, developers, and anyone interested in IP geolocation data.

## Features
- IP Geolocation: Retrieves comprehensive geolocation data for any given IP address.
- User-Friendly Interface: Simple console-based interaction for ease of use.
- Detailed Information: Displays country, city, region, coordinates, ASN, and postal code.
- Google Maps Integration: Provides a direct link to Google Maps based on the retrieved coordinates.
- Error Handling: Gracefully handles errors and displays informative messages.

## Code Explanation
### Data Class
The `Data` class represents the structure of the JSON response from the `ipinfo.io` API.


```csharp
public class Data
{
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string Loc { get; set; }
    public string Org { get; set; }
    public string Postal { get; set; }
}
```

## Main Program
The `Program` class contains the main logic of the application.

```Csharp
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

                string[] Coords = ipInfo.Loc.Split(',');
                Console.WriteLine($"Google Maps: https://www.google.com/maps/?q={Coords[0]},{Coords[1]}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
```

## Prerequisites
- .NET Core SDK
- Internet connection to access the ipinfo.io API
- Getting Started

## Usage
Run the application: ```dotnet run``` / Enter an IP address when prompted: ```Enter IP address:``` / View the geolocation information and Google Maps link: The application will display information such as country, city, region, coordinates, ASN, and postal code. It will also provide a link to Google Maps for the given coordinates.

## Example
```
Enter IP address: 151.101.1.140
[+] Request Successfully Made
Country: US
City: San Francisco
Region: California
Coordinates: 37.7757,-122.3952
ASN: AS54113 Fastly
Postal Code: 94107
Google Maps: https://www.google.com/maps/?q=37.7757,-122.3952
```







