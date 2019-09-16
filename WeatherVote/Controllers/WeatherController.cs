<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherVote.Models;
using WeatherVote.Models.Services;

namespace WeatherVote.Controllers
{
    public class WeatherController : Controller
    {
        private readonly SMHIService _smhiService;
        
            public WeatherController(SMHIService SmhiService)
        {
            _smhiService = SmhiService;
        }

        // GET: Weather
        public async Task<ActionResult> Index()
        {
            var weather = new Weather();
            weather = await _smhiService.GetAllWeather();

            return View("index", weather);
        }

        // GET: Weather/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Weather/Create
        public ActionResult Create()
        {
            return View();
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
=======
﻿using Microsoft.AspNetCore.Mvc;
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
>>>>>>> d9ff67b8ad6f6397be0abaff8e33db544d3881cd
