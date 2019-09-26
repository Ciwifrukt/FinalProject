using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models
{
    public class LoactionCoord
    {
        public int Id { get; set; }
        public string Longitude { get; set; }

        public string Latitude { get; set; }
        public string CityName { get; set; }


    }
}
