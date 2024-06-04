# Geolocate IP Address

## Description

The **Geolocate** program is a C# console application that allows the user to enter an IP address and obtain geolocation information related to that IP, such as country, city, region, coordinates, ASN (Autonomous System Number), and postal code. The application uses the **ipinfo.io** service to fetch the data and presents it to the user in an organized manner.

## Code Structure

The code is structured into two main classes: `Data` and `Program`.

## Data Class

The `Data` class is used to model the data returned by the **ipinfo.io** API. It contains the following properties:

- `City` (string): Represents the city associated with the IP address.
- `Region` (string): Represents the region (state or province) associated with the IP address.
- `Country` (string): Represents the country associated with the IP address.
- `Loc` (string): Represents the latitude and longitude coordinates associated with the IP address.
- `Org` (string): Represents the organization (ASN) associated with the IP address.
- `Postal` (string): Represents the postal code associated with the IP address.

## Program Class

The `Program` class contains the `Main` method, which is the entry point of the application, and an additional `GetIpInfoAsync` method to obtain information about the IP.

**Main Method**

1. Sets the console window title to "Geolocate".
2. Prompts the user to enter an IP address.
3. Checks if the entered IP is not empty or invalid.
4. Calls the `GetIpInfoAsync` method to get the geolocation information of the IP.
5. If the information is successfully retrieved, it is displayed in the console in a clear and organized manner.
6. If an error occurs, an appropriate error message is displayed.

**GetIpInfoAsync Method**

1. Builds the URL to call the **ipinfo.io** API.
2. Uses `HttpClient` to make a GET request to the API.
3. Checks if the API response is successful.
4. Deserializes the JSON response into a `Data` object.
5. Returns the `Data` object containing the geolocation information.
6. In case of an error, catches specific exceptions and displays appropriate error messages.

## Detailed Features

### User Input

The user is prompted to enter an IP address through the console. The program validates that the input is not empty before proceeding.

### API Call

The API call is made using the `HttpClient` library as follows:

- The URL is dynamically constructed based on the IP entered by the user.
- The GET request is made asynchronously.
- The `EnsureSuccessStatusCode` method is used to ensure that the response is successful (status code 2xx).

### Response Processing

- The API response, which is in JSON format, is read as a string.
- The JSON string is deserialized into a `Data` class object using `JsonConvert.DeserializeObject<Data>` from the Newtonsoft.Json library.

### Displaying Results

The deserialized data is displayed in the console. The information displayed includes:

- Country
- City
- Region
- Coordinates
- ASN (Organization)
- Postal Code

### Google Maps Link

The latitude and longitude coordinates are used to create a link to Google Maps, allowing the user to view the location on a map.

### Error Handling

The program is robust in terms of error handling, including:

- Validation of invalid inputs.
- Catching HTTP request exceptions (`HttpRequestException`).
- Catching general exceptions for other types of errors.

## How to Run the Program

### Prerequisites

- .NET Core SDK installed on the system.
- Internet access to make requests to the **ipinfo.io** API.

### Execution Steps

1. Clone the repository or copy the source code to your development environment.
2. Navigate to the project directory in the terminal.
3. Restore dependencies with the command `dotnet restore`.
4. Build the project with the command `dotnet build`.
5. Run the program with the command `dotnet run`.

### Usage Example

```
dotnet run
Enter IP address: 8.8.8.8
```
## Expected Output
```
[+] Request Successfully Made
Country: US
City: Mountain View
Region: California
Coordinates: 37.3860,-122.0838
ASN: AS15169 Google LLC
Postal Code: 94035
Google Maps: https://www.google.com/maps/?q=37.3860,-122.0838
```

## Final Considerations
This program demonstrates the basic use of HttpClient to make HTTP requests, the use of third-party libraries for JSON deserialization, and good error-handling practices in C# console applications. It serves as a solid base for those who want to learn about integrating with external APIs and manipulating data in C#.
