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

            var yrroot = new Rootobject();

            var url = $"https://api.met.no/weatherapi/locationforecast/1.9/?lat=60.10&lon=9.58";
            var weatherInfo = await _http.Get(url);
            doc.LoadXml(weatherInfo);
            
            string data = JsonConvert.SerializeXmlNode(doc);
            data = data.Replace("@", "");

            var a = DateTime.Now;
            DateTime b = new DateTime(a.Year, a.Month, a.Day, a.Hour, 0, 0, 0);

            yrroot = JsonConvert.DeserializeObject<Models.Rootobject>(data);
            var temp = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == b).location.temperature.value;
            var humid = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == b).location.humidity.value;
            var wind = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == b).location.windSpeed.mps;
            //var rain = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == b).location.precipitation.value;
            temp = temp.Replace(".", ",");


            return new Models.Weather {Temperatur = float.Parse(temp) };

        }
    }
}
