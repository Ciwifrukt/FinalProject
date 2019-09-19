using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WeatherVote.Models;

namespace WeatherVote.Services
{
    public class LocationService
    {
        private readonly HttpService _http;

        public LocationService(HttpService http)
        {
            _http = http;
        }

        public async Task<string> LocationName()
        {
            var token = "4bec219c058595";
            var ipstring = GetLocalIPAddress();
            var url = $"https://ipinfo.io/?token={token}";
            var locstring = await _http.Get(url);
            var loc = JsonConvert.DeserializeObject<CityLoc.Rootobject>(locstring);
            return loc.city;
        }


        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
