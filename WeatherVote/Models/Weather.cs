using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models
{
    public class Weather
    {
        public DateTime DateTime { get; set; }
        public float Temperatur { get; set; }
        public float Precipitation { get; set; }
        public string ImgIcon { get; set; }
        public int Humidity { get; set; }
        public int Wind { get; set; }
        public string Description { get; set; }
        public LoactionCoord Loc { get; set; }


    }
}
