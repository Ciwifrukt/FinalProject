using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            return View("Index");
        }        

        // GET: Weather/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        



        public async Task<IActionResult> GetWeather(float lat, float lon)
        {
            var position = new LoactionCoord { Latitude = lat, Longitude = lon };
            var openWeatherWeather = await _weatherService.OpenWeatherWeather(position);
            var smhiWeather = await _weatherService.SMHIWeather(position);
            var yrWeather = await _weatherService.YRWeather(position);

            var allWeathers = new WeatherVM { 
                Weathers = new List<Weather> { openWeatherWeather, smhiWeather, yrWeather }
            };

            return View("Start", allWeathers);
        }

    }
}
