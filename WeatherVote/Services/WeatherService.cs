using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
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
            //var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={location.Latitude}&lon={location.Longitude}&appid={apiKey}";

            var apiKey = "cfdc9335f6a03abf829ab28b3249154b";
            var currentWeatherurl = $"http://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&appid={apiKey}&units=metric ";
            var currentOWjsonString = await _http.Get(currentWeatherurl);
            var currentOWrRO = JsonConvert.DeserializeObject<OpenWeatherCurrent.Rootobject>(currentOWjsonString);

            var temp = currentOWrRO.main.temp;
            var descr = currentOWrRO.weather[0].description;
            var hum = currentOWrRO.main.humidity;
            var wind = currentOWrRO.wind.speed;
            float? prec = 0;
            if (currentOWrRO.rain != null)
            {
                var rain = currentOWrRO.rain._3h == null ? 0 : currentOWrRO.rain._3h;
                var snow = currentOWrRO.snow._3h == null ? 0 : currentOWrRO.snow._3h;
                prec = rain == 0 ? snow == 0 ? 0 : snow : rain;
            }
            return new Models.Weather
            {
                Temperatur = temp,
                Loc = location,
                Description = descr,
                Humidity = hum,
                Precipitation = (float)prec,
                Wind = wind,
                Supplier = new WeatherSupplier { Name = "Open Weather" }
            };
        }

        public async Task<Weather> SMHIWeather(LoactionCoord location)
        {
            var url = $"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{location.Longitude}/lat/{location.Latitude}/data.json";
            var SMHIjsonString = await _http.Get(url);

            var smhiRootObject = JsonConvert.DeserializeObject<SMHI.Rootobjectsmhi>(SMHIjsonString);
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);

            var temp = GetSmhiValue("t", now, smhiRootObject);
            var hum = GetSmhiValue("r", now, smhiRootObject);
            var prec = GetSmhiValue("pmean", now, smhiRootObject);
            var wind = GetSmhiValue("ws", now, smhiRootObject);
            var descr = GetDescSMHI(GetSmhiValue("Wsymb2", now, smhiRootObject));

            return new Weather
            {
                Temperatur = temp,
                Loc = location,
                Description = descr,
                Humidity = hum,
                Precipitation = (float)prec,
                Wind = wind,
                Supplier = new WeatherSupplier { Name = "SMHI" }
            };
        }


        public async Task<Models.Weather> YRWeather(LoactionCoord location)
        {
            XmlDocument doc = new XmlDocument();

            var yrroot = new Rootobject();

            var url = $"https://api.met.no/weatherapi/locationforecast/1.9/?lat={location.Latitude}&lon={location.Longitude}";
            var weatherInfo = await _http.Get(url);
            doc.LoadXml(weatherInfo);

            string data = JsonConvert.SerializeXmlNode(doc);
            data = data.Replace("@", "");

            var a = DateTime.Now;
            DateTime to = new DateTime(a.Year, a.Month, a.Day, a.Hour, 0, 0, 0);
            DateTime from = new DateTime(a.Year, a.Month, a.Day, a.Hour+1, 0, 0, 0);

            //to = "2019-09-19T10:00:00Z"

            yrroot = JsonConvert.DeserializeObject<Models.Rootobject>(data);
            var rootNow = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == to).location;
            //var rootToGetIcon = yrroot.weatherdata.product.time.Where(x => x.from == to && x.to == from).location
            var temp = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == to).location.temperature.value;
            var humid = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == to).location.humidity.value;
            var wind = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == to).location.windSpeed.mps;

            string prec;

            if (rootNow.precipitation == null)
            {
                prec = "0";
            }
            else
            {
                prec = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == to).location.precipitation.value;
            }
            temp = temp.Replace(".", ",");
            humid = humid.Replace(".", ",");
            wind = wind.Replace(".", ",");
            prec = prec.Replace(".", ",");


            return new Models.Weather
            {
                Temperatur = float.Parse(temp),
                Loc = location,
                Description = "",
                Humidity = float.Parse(humid),
                Wind = float.Parse(wind),
                Precipitation = float.Parse(prec),
                Supplier = new WeatherSupplier { Name = "YR.no" }
            };
        }

  

        private float GetSmhiValue(string v, DateTime time, SMHI.Rootobjectsmhi smhiRootObject)
        {
            return smhiRootObject.timeSeries.First(x => x.validTime == time).parameters.First(x => x.name == v).values.First();
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