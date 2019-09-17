using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherVote.Models;
using static WeatherVote.Models.OpenWeather;

namespace WeatherVote.Services
{
    public class OpenWeatherService
    {
        private readonly HttpService _http;
        public OpenWeatherService(HttpService http)
        {
            _http = http;
        }
        public async Task<Models.Weather> GetAllWeather(LoactionCoord location)
        {
            var apiKey = "cfdc9335f6a03abf829ab28b3249154b";

            var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={location.Latitude}&lon={location.Longitude}&appid={apiKey}";
            var weatherInfo = await _http.Get(url);
          
            
            var xxx = JsonConvert.DeserializeObject<OpenWeather.Rootobject>(weatherInfo);

            return new Models.Weather { };
            
        }
    }
}
