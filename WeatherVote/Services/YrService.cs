using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WeatherVote.Models;

namespace WeatherVote.Services
{
    public class YrService
    {
        private readonly HttpService _http;

        public YrService(HttpService http)
        {
            _http = http;
        }
        public async Task<Models.Weather> GetAllWeather(LoactionCoord pos)
        {
            XmlDocument doc = new XmlDocument();

            var yrroot = new YR.Rootobject();

            var url = $"https://api.met.no/weatherapi/locationforecast/1.9/?lat=60.10&lon=9.58";
            var weatherInfo = await _http.Get(url);
            doc.LoadXml(weatherInfo);
            string data = JsonConvert.SerializeXmlNode(doc);

            yrroot = JsonConvert.DeserializeObject<YR.Rootobject>(data);

            return new Models.Weather {Temperatur = 1 };

        }
    }
}
