using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherVote.Models;

namespace WeatherVote.Services
{
    public class WeatherService
    {
        private readonly HttpService _http;
        public WeatherService(HttpService http)
        {
            _http = http;
        }

        public async Task<Models.Weather> OpenWeatherWeather(LoactionCoord location)
        {
            var apiKey = "cfdc9335f6a03abf829ab28b3249154b";
            //var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={location.Latitude}&lon={location.Longitude}&appid={apiKey}";
            var currentWeatherurl = $"http://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&appid={apiKey}";
            var currentOWjsonString = await GetApiString(currentWeatherurl);
            var currentOWrRO = JsonConvert.DeserializeObject<OpenWeatherCurrent.Rootobject>(currentOWjsonString);

            var temp = currentOWrRO.main.temp;
            var descr = currentOWrRO.weather[0].description;
            var hum = currentOWrRO.main.humidity;
            var wind = currentOWrRO.wind.speed;

            var rain = currentOWrRO.rain._3h == null ? 0 : currentOWrRO.rain._3h;
            var snow = currentOWrRO.snow._3h == null ? 0 : currentOWrRO.snow._3h;
            var prec = rain == 0 ? snow == 0 ? 0 : snow : rain;

            return new Models.Weather { Temperatur = temp,
                Loc = location,
                Description = descr,
                Humidity = hum,
                Precipitation =(float)prec,
                Wind = wind};
            
        }



        public async Task<Weather> SMHIWeather(LoactionCoord location)
        {
            var url = $"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{location.Longitude}/lat/{location.Latitude}/data.json";
            var SMHIjsonString = await GetApiString(url);
            var smhiRootObject = JsonConvert.DeserializeObject<SMHI.Rootobjectsmhi>(SMHIjsonString);
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,0,0);
           
            var temp = GetSmhiValue("t", now, smhiRootObject);
            var hum = GetSmhiValue("r", now, smhiRootObject);
            var prec = GetSmhiValue("pmean", now, smhiRootObject);
            var wind = GetSmhiValue("ws", now, smhiRootObject);
            var descr = GetDescSMHI(GetSmhiValue("Wsymb2", now, smhiRootObject));

            return new Weather { Temperatur = temp,
                Loc = location,
                Description = descr,
                Humidity = hum,
                Precipitation = (float)prec,
                Wind = wind
            };

           
        }

 

        public async Task<Models.Weather> YRWeather(LoactionCoord location)
        {

            var weather = new Weather {  };

            return weather;

        }

        private async Task<string> GetApiString(string url)
        {
            var jsonString = await _http.Get(url);
            return jsonString;

        }

        private float GetSmhiValue(string v, DateTime time, SMHI.Rootobjectsmhi smhiRootObject)
        {
            return smhiRootObject.timeSeries.First(x => x.validTime == time).parameters.First(x => x.name == "t").values.First();
        }

        public string GetDescSMHI(float v)
        {
            switch (v)
            {
                case 1:
                    return "Clear sky";
                case 2:
                    return "Nearly clear sky";
                case 3:
                    return "Variable cloudiness";
                case 4:
                    return "Halfclear sky";
                case 5:
                    return "Cloudy sky";
                case 6:
                    return "Overcast";
                case 7:
                    return "Fog";
                case 8:
                    return "Light rain showers";
                case 9:
                    return "Moderate rain showers";
                case 10:
                    return "Heavy rain showers";
                case 11:
                    return "Thunderstorm";
                case 12:
                    return "Light sleet showers";
                case 13:
                    return "Moderate sleet showers";
                case 14:
                    return "Heavy sleet showers";
                case 15:
                    return "Light snow showers";
                case 16:
                    return "Moderate snow showers";
                case 17:
                    return "Heavy snow showers";
                case 18:
                    return "Light rain";
                case 19:
                    return "Moderate rain";
                case 20:
                    return "Heavy rain";
                case 21:
                    return "Thunder";
                case 22:
                    return "Light sleet";
                case 23:
                    return "Moderate sleet";
                case 24:
                    return "Heavy sleet";
                case 25:
                    return "Light snowfall";
                case 26:
                    return "Moderate snowfall";
                case 27:
                    return "Heavy snowfall";

                default:
                    return null;
            }
        }
    }
}
