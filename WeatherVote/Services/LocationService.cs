using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public async Task<string> LocationName(string lat, string lon)
        {
            //var token = "4bec219c058595";
            //var url = $"https://ipinfo.io/?token={token}";
            //var loc = JsonConvert.DeserializeObject<CityLoc.Rootobject>(locstring);


            //var token = "50eb3c42e3463ceb1726d83ecc38a7ce";
            //var ipstring = GetLocalIPAddress();
            //ipstring = Regex.Replace(ipstring, @"\t\n\r", "");
            //var url = $"http://api.ipstack.com/{ipstring}?access_key={token}";
            //var locstring = await _http.Get(url);
            //var loc = JsonConvert.DeserializeObject<ipstack.Rootobject>(locstring);

            var token = "9a5994299d3ef0";
            var url = $"https://eu1.locationiq.com/v1/reverse.php?key={token}&lat={lat}&lon={lon}&format=json";
            var locstring = await _http.Get(url);
            var loc = JsonConvert.DeserializeObject<lociq.Rootobject>(locstring);
            if (loc.address.city != null) { 
            return loc.address.city;
            }
            else
            {
                return loc.address.county;
            }
        }





        public static string GetLocalIPAddress()
        {

            return new WebClient().DownloadString("http://icanhazip.com");
            //var host = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (var ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        return ip.ToString();
            //    }
            //}
            //throw new Exception("No network adapters with an IPv4 address in the system!");
        }


    }
}
