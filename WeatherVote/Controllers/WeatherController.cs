using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherVote.Models;
<<<<<<< HEAD
using WeatherVote.Models.Services;
using WeatherVote.Services;
=======
using WeatherVote.Services;
using WeatherVote.ViewModels;
>>>>>>> 6b9c28961ff93d1a79a6a0d0abbd777d62cbbf01

namespace WeatherVote.Controllers
{
    public class WeatherController : Controller
    {
<<<<<<< HEAD
        private readonly SMHIService _smhiService;
        private readonly YrService _yrservice;
        LoactionCoord loc = new LoactionCoord();
        public Weather w = new Weather();

        public WeatherController(YrService YrService)
        {
            _yrservice = YrService;
=======
       
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
>>>>>>> 6b9c28961ff93d1a79a6a0d0abbd777d62cbbf01
        }


        public ActionResult Index()
        {
<<<<<<< HEAD
            
            return View("Index");
        }        

        // GET: Weather/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public async Task<IActionResult> GetWeather(float lat, float lon)
        {
            var pos = new LoactionCoord { Latitude = lat, Longitude = lon };
            //var openWeatherWeather = await _service.GetAllWeather(pos);
            //var smhiWeather = await _smhiService.GetAllWeather(pos);
            var yrWeather = new Weather { Loc = pos };
            yrWeather = await _yrservice.GetAllWeather(pos);


            return View("Index");
        }
=======
            return View();
        }
        
>>>>>>> 6b9c28961ff93d1a79a6a0d0abbd777d62cbbf01


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
