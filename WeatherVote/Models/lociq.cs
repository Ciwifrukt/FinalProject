using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models
{
    public class lociq
    {

        public class Rootobject
        {
            public string place_id { get; set; }
            public string licence { get; set; }
            public string osm_type { get; set; }
            public string osm_id { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
            public string display_name { get; set; }
            public Address address { get; set; }
            public string[] boundingbox { get; set; }
        }

        public class Address
        {
            public string road { get; set; }
            public string neighbourhood { get; set; }
            public string suburb { get; set; }
            public string city_district { get; set; }
            public string city { get; set; }
            public string county { get; set; }
            public string state { get; set; }
            public string postcode { get; set; }
            public string country { get; set; }
            public string country_code { get; set; }
        }

    }
}
