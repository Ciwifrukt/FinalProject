using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models
{
    public class Forecast
    {
        public int Id { get; set; }
        public float Temperatur { get; set; }
        public string ImgIcon { get; set; }
        public string time { get; set; }

    }
}
