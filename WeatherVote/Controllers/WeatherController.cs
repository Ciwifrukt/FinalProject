using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WeatherVote.Models;
using WeatherVote.Services;
using WeatherVote.ViewModels;
using Xamarin.Forms.Maps;

namespace WeatherVote.Controllers
{

    public class WeatherController : Controller
    {
    public static WeatherVM allWeathers = new WeatherVM();
        private readonly WeatherService _weatherService;
        private readonly LocationService _locationService;
        private readonly VotingContext _context;

        public WeatherController(WeatherService weatherService, VotingContext context, LocationService locationService)
        {
            _weatherService = weatherService;
            _locationService = locationService;
            _context = context;
        }

        public ActionResult Index()
        {  
            return View();
        }


        public ActionResult Like()
        {
            return View();
        }


        public IActionResult AddLikes(string wname, string loc, string typ)
        {
            if(!_context.WeatherSuppliers.Any(x => x.Name == wname)) {
                var w = new WeatherSupplier { Name = wname };
                _context.WeatherSuppliers.Add(w);
                _context.Votes.Add(new Vote { Likes = typ == "like" ? 1 : -1, Supplier = w, Location = loc }) ;
            }
            else if(!_context.Votes.Any(x=>x.Supplier.Name == wname))
            {
                var w = _context.WeatherSuppliers.First(x => x.Name == wname);
                _context.Votes.Add(new Vote { Supplier = w, Likes = typ == "like" ? 1 : -1, Location = loc});

            }
            else
            {
                var vote = _context.Votes.First(x => x.Supplier.Name == wname);
                if(typ=="like")
                vote.Likes++;
                else
                    vote.Likes--;

            }

            _context.SaveChanges();
            var sortedWeather = SortWeathers(allWeathers.Weathers);
            allWeathers.Weathers = sortedWeather;
            return View("Like", allWeathers);
            
        }



      

        public async Task<IActionResult> GetWeather(decimal lat, decimal lon)
        {
            var locname = await _locationService.LocationName();
            var weatherList = _context.Weathers.ToList();
            var position = new LoactionCoord { CityName = locname, Latitude = Decimal.Round(lat, 3).ToString(new CultureInfo("en")), Longitude = Decimal.Round(lon, 3).ToString(new CultureInfo("en")) };

            if (weatherList.Any(x => x.Updated <= DateTime.Now.AddMinutes(10))|| weatherList.Count==0) {

                _context.Weathers.RemoveRange(_context.Weathers);

            var openWeatherWeather = await _weatherService.OpenWeatherWeather(position);
            var smhiWeather = await _weatherService.SMHIWeather(position);
            var yrWeather = await _weatherService.YRWeather(position);
            weatherList = new List<Weather> { openWeatherWeather, yrWeather, smhiWeather };

                foreach (var weather in weatherList)
                {
                _context.Weathers.Add(weather);

                }
                _context.SaveChanges();

            }
            else
            {
                weatherList= _context.Weathers.ToList();
            }
            var sortedWeather = SortWeathers(weatherList);
            foreach (var item in sortedWeather)
            {
                item.Updated = DateTime.Now;
            }

            allWeathers = new WeatherVM {
                Weathers = sortedWeather,
                City = position.CityName,
                Date = DateTime.Now.ToString("dddd, dd MMMM HH:mm")
            };

            return View("Like", allWeathers);
        }

        private List<Weather> SortWeathers(List<Weather> weatherList)
        {
            var votesList = _context.Votes.Include(x => x.Supplier).
                            ToList();

            foreach (var w in weatherList)
            {
                if (!votesList.Any(x => x.Supplier.Name == w.Supplier.Name))
                {
                    _context.Add(new Vote
                    {
                        Supplier = new WeatherSupplier
                        { Name = w.Supplier.Name },
                        Likes = 0,
                        Location = w.Loc.CityName
                    });
                    w.Sorting = 0;
                }
                else
                {
                    w.Sorting = votesList.First(x => x.Supplier.Name == w.Supplier.Name).Likes;
                }
            }
            _context.SaveChanges();

            votesList = _context.Votes.Include(x => x.Supplier).ToList().OrderByDescending(x => x.Likes).ToList();
            var sortedWeather = new List<Weather>();
            foreach (var vote in votesList)
            {
                sortedWeather.Add(weatherList.First(x => x.Supplier.Name == vote.Supplier.Name));
            }
            return sortedWeather;
        }

        public IActionResult WeatherAgency()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

    }
}
