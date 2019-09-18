using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WeatherVote.Models;
using WeatherVote.Services;
using WeatherVote.ViewModels;


namespace WeatherVote.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;

        }


        public ActionResult Index()
        {  
            return View();
        }


        public ActionResult Like()
        {
            return View();
        }


        public async Task<IActionResult> GetWeather(decimal lat, decimal lon)
        {
            var position = new LoactionCoord { Latitude = Decimal.Round(lat, 3).ToString(new CultureInfo("en")), Longitude = Decimal.Round(lon, 3).ToString(new CultureInfo("en")) };
            var openWeatherWeather = await _weatherService.OpenWeatherWeather(position);
            var smhiWeather = await _weatherService.SMHIWeather(position);
            var yrWeather = await _weatherService.YRWeather(position);

            var allWeathers = new WeatherVM { 
                Weathers = new List<Weather> { openWeatherWeather, smhiWeather, yrWeather }
            };

            return View("WeatherAgency", allWeathers);
        }

        public IActionResult WeatherAgency()
        {
            return View();
        }

    }
}
