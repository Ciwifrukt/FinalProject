using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models
{
    public class Meto
    {

        public class Rootobject
        {
            public Locations Locations { get; set; }
        }

        public class Locations
        {
            public Location[] Location { get; set; }
        }

        public class Location
        {
            public string elevation { get; set; }
            public string id { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string name { get; set; }
            public string region { get; set; }
            public string unitaryAuthArea { get; set; }
            public string obsSource { get; set; }
            public string nationalPark { get; set; }
        }

    }
}
