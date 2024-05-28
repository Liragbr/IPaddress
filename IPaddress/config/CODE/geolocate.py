import requests

class Data:
    def __init__(self, city, region, country, loc, org, postal):
        self.city = city
        self.region = region
        self.country = country
        self.loc = loc
        self.org = org
        self.postal = postal

def main():
    print("Geolocate")
    ip = input("Enter IP address: ")
    url = f"https://ipinfo.io/{ip}/json"

    try:
        response = requests.get(url)
        response.raise_for_status()

        print("[+] Request Successfully Made")
        ip_info = response.json()

        print(f"Country: {ip_info['country']}")
        print(f"City: {ip_info['city']}")
        print(f"Region: {ip_info['region']}")
        print(f"Coordinates: {ip_info['loc']}")
        print(f"ASN: {ip_info['org']}")
        print(f"Postal Code: {ip_info['postal']}")

        coords = ip_info['loc'].split(',')
        print(f"Google Maps: https://www.google.com/maps/?q={coords[0]},{coords[1]}")
    except requests.RequestException as ex:
        print(f"Error: {ex}")

if __name__ == "__main__":
    main()
