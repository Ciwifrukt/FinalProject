using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherVote.Models;
using WeatherVote.Services;

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

        public IActionResult OpenWeather()
        {
            return View();
        }
        public async Task<IActionResult> GetWeather(float lat, float lon)
        {
            var pos = new LoactionCoord { Latitude = lat, Longitude = lon };
           var x = await _service.GetAllWeather(pos);
            return View();
        }
    }
}
