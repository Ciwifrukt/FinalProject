using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherVote.Models;
using WeatherVote.Services;
using static WeatherVote.Models.YR;

namespace WeatherVote.Controllers
{
    public class WeatherController:Controller
    {
        private readonly OpenWeatherService _service;

        public WeatherController(OpenWeatherService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetWeather(decimal lat, decimal lon)
        {
            var pos = new LoactionCoord { Latitude = lat, Longitude = lon };
            _service.GetAllWeather(pos);
            return View();
        }
    }
}
