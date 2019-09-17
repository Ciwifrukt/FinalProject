using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WeatherVote.Models;
using WeatherVote.Models.Services;

namespace WeatherVote.Controllers
{
    public class WeatherController : Controller
    {
        private readonly SMHIService _smhiService;
        LoactionCoord loc = new LoactionCoord();
        public Weather w = new Weather();

        public WeatherController(SMHIService SmhiService)
        {
            _smhiService = SmhiService;
        }


        public ActionResult Index()
        {
            
            return View("Index", w);
        }
        // GET: Weather
        public async Task<ActionResult> Index2(float lon, float lat, Weather w)
        {
            w.Loc.Longitude = lon;
            w.Loc.Latitude = lat;
            var weather = new Weather();
            weather = await _smhiService.GetAllWeather(w);

            return View("index", weather);
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
            //var yrWeather = await _yrservice.GetAllWeather(pos);


            return View("Index");
        }

        // POST: Weather/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Weather/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Weather/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Weather/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Weather/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
