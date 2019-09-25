using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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

            var temp1 = decimal.Parse(currentOWrRO.main.temp.ToString());
            var temp = float.Parse(Decimal.Round(temp1, 1).ToString());
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
                ImgIcon = $"/img/weathericons/{currentOWrRO.weather[0].icon}.png",
                Supplier = new WeatherSupplier { Name = "Open Weather" },
                Updated = DateTime.UtcNow.AddHours(2)
                
            };
        }

        public async Task<Weather> SMHIWeather(LoactionCoord location)
        {
            var url = $"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{location.Longitude}/lat/{location.Latitude}/data.json";


            var SMHIjsonString = await _http.Get(url);

            System.IO.File.AppendAllLines("looog.txt", new[] { $"{DateTime.UtcNow.AddHours(2)} {SMHIjsonString}", "", "", "" });

            var smhiRootObject = JsonConvert.DeserializeObject<SMHI.Rootobjectsmhi>(SMHIjsonString);
            DateTime now = new DateTime(DateTime.UtcNow.AddHours(2).Year, DateTime.UtcNow.AddHours(2).Month, DateTime.UtcNow.AddHours(2).Day, DateTime.UtcNow.AddHours(2).Hour, 0, 0);

            var temp = GetSmhiValue("t", now, smhiRootObject);
            var hum = GetSmhiValue("r", now, smhiRootObject);
            var prec = GetSmhiValue("pmean", now, smhiRootObject);
            var wind = GetSmhiValue("ws", now, smhiRootObject);
            var descr = GetDescSMHI(GetSmhiValue("Wsymb2", now, smhiRootObject));
            var imgIconUrl = GetSmhiValue("Wsymb2", now, smhiRootObject);

            return new Weather
            {
                Temperatur = temp,
                Loc = location,
                Description = descr,
                Humidity = hum,
                Precipitation = (float)prec,
                Wind = wind,
                ImgIcon = GetImgIcon(imgIconUrl, now),
                Supplier = new WeatherSupplier { Name = "SMHI" },
                Updated = DateTime.UtcNow.AddHours(2)

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

            var a = DateTime.UtcNow.AddHours(2);
            DateTime to = new DateTime(a.Year, a.Month, a.Day, a.Hour, 0, 0, 0);
            DateTime from = new DateTime(a.Year, a.Month, a.Day, a.Hour+1, 0, 0, 0);

            yrroot = JsonConvert.DeserializeObject<Models.Rootobject>(data);
            var rootNow = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == to).location;
            var rootToGetIcon = yrroot.weatherdata.product.time.FirstOrDefault(x => x.from == to && x.to == from).location.symbol;

            var unformatedDesc = rootToGetIcon.id;
            var imgIconUrl = float.Parse(rootToGetIcon.number);
            string pattern = $"([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))";
            var desc = Regex.Replace(unformatedDesc, pattern , "$1 ");

            var img = rootToGetIcon.number;
            var temp = rootNow.temperature.value;
            var humid = rootNow.humidity.value;
            var wind = rootNow.windSpeed.mps;

            string prec;

            if (rootNow.precipitation == null)
            {
                prec = "0";
            }
            else
            {
                prec = rootNow.precipitation.value;
            }

            temp = temp.Replace(".", ",");
            humid = humid.Replace(".", ",");
            wind = wind.Replace(".", ",");
            prec = prec.Replace(".", ",");

            var correctComma = new CultureInfo("sv-SE");

            return new Models.Weather
            {
                Temperatur = float.Parse(temp, correctComma.NumberFormat),
                Loc = location,
                Description = desc,
                Humidity = float.Parse(humid, correctComma.NumberFormat),
                Wind = float.Parse(wind, correctComma.NumberFormat),
                ImgIcon = GetImgIcon(imgIconUrl, a),
                Precipitation = float.Parse(prec, correctComma.NumberFormat),
                Supplier = new WeatherSupplier { Name = "YR.no" },
                Updated = DateTime.UtcNow.AddHours(2)

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
        public string GetImgIcon(float imgsymbol, DateTime now)
        {
            switch (imgsymbol)
            {
               
                case 1:
                case 2:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return "/img/weathericons/01n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return "/img/weathericons/01m.png";
                    }
                    else
                    {
                        return "/img/weathericons/01d.png";
                    }

                case 3:
                case 4:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/02n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/02m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/02d.png";
                    }

                case 5:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/03n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/03m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/03d.png";
                    }

                case 6:
                    return @"/img/weathericons/04.png";

                case 7:
                    return @"/img/weathericons/15.png";

                case 8:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/40n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/40m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/40d.png";
                    }

                case 9:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/05n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/05m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/05d.png";
                    }

                case 10:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/41n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/41m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/41d.png";
                    }

                case 11:
                    return "Thunderstorm";

                case 12:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/42n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/42m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/42d.png";
                    }

                case 13:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/07n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/07m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/07d.png";
                    }

                case 14:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/43n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/43m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/43d.png";
                    }

                case 15:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/44n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/44m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/44d.png";
                    }

                case 16:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/08n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/08m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/08d.png";
                    }

                case 17:
                    if (now.Hour >= 20 || now.Hour < 6)
                    {
                        return @"/img/weathericons/45n.png";
                    }
                    else if (now.Hour >= 6 && now.Hour <= 8)
                    {
                        return @"/img/weathericons/45m.png";
                    }
                    else
                    {
                        return @"/img/weathericons/45d.png";
                    }

                case 18:
                    return @"/img/weathericons/46.png";

                case 19:
                    return @"/img/weathericons/09.png";
                           
                case 20:    
                    return @"/img/weathericons/10.png";

                case 21:
                    return "Thunder";

                case 22:
                    return @"/img/weathericons/47.png";

                case 23:     
                    return @"/img/weathericons/12.png";

                case 24:     
                    return @"/img/weathericons/48.png";

                case 25:     
                    return @"/img/weathericons/49.png";

                case 26: 
                    return @"/img/weathericons/13.png";
                case 27:     

                    return @"/img/weathericons/50.png";

                default:
                    return null;
            }
        }
    }
}