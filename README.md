# Geolocate IP Address - Csharp
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

# Geolocate IP Address - Python 
This repository contains a Python script that retrieves geolocation information for a given IP address using the ipinfo.io API. The script fetches and displays details such as country, city, region, coordinates, ISP (ASN), and postal code associated with the IP address.

## Features
- IP Geolocation: Fetches comprehensive geolocation data for any IP address.
- Simple Interface: Command-line based for ease of use.
- Detailed Information: Displays country, city, region, coordinates, ASN, and postal code.
- Google Maps Link: Provides a direct link to Google Maps for the location.
- Error Handling: Gracefully handles errors and provides informative messages.

## Usage
- Run the script:
```python geolocate_ip.py```

- Enter the IP address when prompted:
```Enter IP address: ```

- Example
```Example output for IP address 8.8.8.8:

Country: US
City: Mountain View
Region: California
Coordinates: 37.386,-122.0838
ASN: AS15169 Google LLC
Postal Code: 94035
Google Maps: https://www.google.com/maps/?q=37.386,-122.0838
```

## Code Explanation
The script `geolocate_ip.py` performs geolocation of an IP address using the `ipinfo.io` API. Here’s a breakdown of the script:

`get_ip_info(ip)`
- **Purpose:** Fetch geolocation data for a given IP address.

Functionality:
- Sends a GET request to the ipinfo.io API endpoint with the specified IP.
- If the request is successful, returns the JSON response.
- Handles exceptions and prints an error message if the request fails.

`display_info(ip_info)`
- **Purpose:** Display the geolocation information in a readable format.

Functionality:
- Checks if the ip_info is not None.
- Prints details including country, city, region, coordinates, ASN, and postal code.
- Constructs a Google Maps link using the coordinates.

`Main Block`
- Input: Prompts the user to enter an IP address.

Process:
- Calls get_ip_info(ip) to retrieve geolocation data.
- Calls display_info(ip_info) to display the retrieved information.

Here’s the code:

```python
import requests

def get_ip_info(ip):
    try:
        response = requests.get(f'https://ipinfo.io/{ip}/json')
        response.raise_for_status()
        return response.json()
    except requests.exceptions.RequestException as e:
        print(f"Error: {e}")
        return None

def display_info(ip_info):
    if ip_info:
        print(f"Country: {ip_info.get('country')}")
        print(f"City: {ip_info.get('city')}")
        print(f"Region: {ip_info.get('region')}")
        print(f"Coordinates: {ip_info.get('loc')}")
        print(f"ASN: {ip_info.get('org')}")
        print(f"Postal Code: {ip_info.get('postal')}")
        coords = ip_info.get('loc').split(',')
        print(f"Google Maps: https://www.google.com/maps/?q={coords[0]},{coords[1]}")

if __name__ == "__main__":
    ip = input("Enter IP address: ")
    ip_info = get_ip_info(ip)
    display_info(ip_info)
```
This explanation should help you understand how each part of the script functions to retrieve and display geolocation data for an IP address.
