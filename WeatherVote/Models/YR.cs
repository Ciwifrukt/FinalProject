using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models
{


    public class Rootobject
    {
        public Weatherdata weatherdata { get; set; }
    }

    public class Weatherdata
    {
        public string xmlnsxsi { get; set; }
        public string xsinoNamespaceSchemaLocation { get; set; }
        public DateTime created { get; set; }
        public Meta meta { get; set; }
        public Product product { get; set; }
    }

    public class Meta
    {
        public Model[] model { get; set; }
    }

    public class Model
    {
        public string name { get; set; }
        public DateTime termin { get; set; }
        public DateTime runended { get; set; }
        public DateTime nextrun { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }

    public class Product
    {
        public string _class { get; set; }
        public Time[] time { get; set; }
    }

    public class Time
    {
        public string datatype { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public Location location { get; set; }
    }

    public class Location
    {
        public string altitude { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Temperature temperature { get; set; }
        public Winddirection windDirection { get; set; }
        public Windspeed windSpeed { get; set; }
        public Windgust windGust { get; set; }
        public Areamaxwindspeed areaMaxWindSpeed { get; set; }
        public Humidity humidity { get; set; }
        public Pressure pressure { get; set; }
        public Cloudiness cloudiness { get; set; }
        public Fog fog { get; set; }
        public Lowclouds lowClouds { get; set; }
        public Mediumclouds mediumClouds { get; set; }
        public Highclouds highClouds { get; set; }
        public Temperatureprobability temperatureProbability { get; set; }
        public Windprobability windProbability { get; set; }
        public Dewpointtemperature dewpointTemperature { get; set; }
        public Precipitation precipitation { get; set; }
        public Symbol symbol { get; set; }
        public Mintemperature minTemperature { get; set; }
        public Maxtemperature maxTemperature { get; set; }
        public Symbolprobability symbolProbability { get; set; }
    }

    public class Temperature
    {
        public string id { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
    }

    public class Winddirection
    {
        public string id { get; set; }
        public string deg { get; set; }
        public string name { get; set; }
    }

    public class Windspeed
    {
        public string id { get; set; }
        public string mps { get; set; }
        public string beaufort { get; set; }
        public string name { get; set; }
    }

    public class Windgust
    {
        public string id { get; set; }
        public string mps { get; set; }
    }

    public class Areamaxwindspeed
    {
        public string mps { get; set; }
    }

    public class Humidity
    {
        public string value { get; set; }
        public string unit { get; set; }
    }

    public class Pressure
    {
        public string id { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
    }

    public class Cloudiness
    {
        public string id { get; set; }
        public string percent { get; set; }
    }

    public class Fog
    {
        public string id { get; set; }
        public string percent { get; set; }
    }

    public class Lowclouds
    {
        public string id { get; set; }
        public string percent { get; set; }
    }

    public class Mediumclouds
    {
        public string id { get; set; }
        public string percent { get; set; }
    }

    public class Highclouds
    {
        public string id { get; set; }
        public string percent { get; set; }
    }

    public class Temperatureprobability
    {
        public string unit { get; set; }
        public string value { get; set; }
    }

    public class Windprobability
    {
        public string unit { get; set; }
        public string value { get; set; }
    }

    public class Dewpointtemperature
    {
        public string id { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
    }

    public class Precipitation
    {
        public string unit { get; set; }
        public string value { get; set; }
        public string minvalue { get; set; }
        public string maxvalue { get; set; }
    }

    public class Symbol
    {
        public string id { get; set; }
        public string number { get; set; }
    }

    public class Mintemperature
    {
        public string id { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
    }

    public class Maxtemperature
    {
        public string id { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
    }

    public class Symbolprobability
    {
        public string unit { get; set; }
        public string value { get; set; }
    }

    //    public class YR
    //    {

    //        public class Rootobject
    //        {
    //            public Weatherdata weatherdata { get; set; }
    //        }

    //        public class Weatherdata
    //        {
    //            public Location location { get; set; }
    //            public Credit credit { get; set; }
    //            public Links links { get; set; }
    //            public Meta meta { get; set; }
    //            public Sun sun { get; set; }
    //            public Forecast forecast { get; set; }
    //        }

    //        public class Location
    //        {
    //            public string name { get; set; }
    //            public string type { get; set; }
    //            public string country { get; set; }
    //            public Timezone timezone { get; set; }
    //            public Location1 location { get; set; }
    //        }

    //        public class Timezone
    //        {
    //            public string _id { get; set; }
    //            public string _utcoffsetMinutes { get; set; }
    //        }

    //        public class Location1
    //        {
    //            public string _altitude { get; set; }
    //            public string _latitude { get; set; }
    //            public string _longitude { get; set; }
    //            public string _geobase { get; set; }
    //            public string _geobaseid { get; set; }
    //        }

    //        public class Credit
    //        {
    //            public Link link { get; set; }
    //        }

    //        public class Link
    //        {
    //            public string _text { get; set; }
    //            public string _url { get; set; }
    //        }

    //        public class Links
    //        {
    //            public Link1[] link { get; set; }
    //        }

    //        public class Link1
    //        {
    //            public string _id { get; set; }
    //            public string _url { get; set; }
    //        }

    //        public class Meta
    //        {
    //            public DateTime lastupdate { get; set; }
    //            public DateTime nextupdate { get; set; }
    //        }

    //        public class Sun
    //        {
    //            public DateTime _rise { get; set; }
    //            public DateTime _set { get; set; }
    //        }

    //        public class Forecast
    //        {
    //            public Tabular tabular { get; set; }
    //        }

    //        public class Tabular
    //        {
    //            public Time[] time { get; set; }
    //        }

    //        public class Time
    //        {
    //            public Symbol symbol { get; set; }
    //            public Precipitation precipitation { get; set; }
    //            public Winddirection windDirection { get; set; }
    //            public Windspeed windSpeed { get; set; }
    //            public Temperature temperature { get; set; }
    //            public Pressure pressure { get; set; }
    //            public DateTime _from { get; set; }
    //            public DateTime _to { get; set; }
    //            public string _period { get; set; }
    //        }

    //        public class Symbol
    //        {
    //            public string _number { get; set; }
    //            public string _numberEx { get; set; }
    //            public string _name { get; set; }
    //            public string _var { get; set; }
    //        }

    //        public class Precipitation
    //        {
    //            public string _value { get; set; }
    //        }

    //        public class Winddirection
    //        {
    //            public string _deg { get; set; }
    //            public string _code { get; set; }
    //            public string _name { get; set; }
    //        }

    //        public class Windspeed
    //        {
    //            public string _mps { get; set; }
    //            public string _name { get; set; }
    //        }

    //        public class Temperature
    //        {
    //            public string _unit { get; set; }
    //            public string _value { get; set; }
    //        }

    //        public class Pressure
    //        {
    //            public string _unit { get; set; }
    //            public string _value { get; set; }
    //        }

    //    }
}
