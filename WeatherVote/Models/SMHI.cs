using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models
{
    public class SMHI
    {

        public class Rootobject
        {
            public DateTime approvedTime { get; set; }
            public DateTime referenceTime { get; set; }
            public Geometry geometry { get; set; }
            public Timesery[] timeSeries { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[][] coordinates { get; set; }
        }

        public class Timesery
        {
            public DateTime validTime { get; set; }
            public Parameter[] parameters { get; set; }
        }

        public class Parameter
        {
            public string name { get; set; }
            public string levelType { get; set; }
            public int level { get; set; }
            public string unit { get; set; }
            public float[] values { get; set; }
        }

        //public Rootobject xxx { get; set; }
        //public string TempDay { get; set; }
        //readonly HttpClient client = new HttpClient();
        //public async Task OnGetAsync()
        //{
        //    var s = "";
        //    var url = "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/17.9446/lat/59.40719/data.json";

        //    //var respons = await client.GetAsync(url);
        //    //s = await respons.Content.ReadAsStringAsync();
        //    // Above three lines can be replaced with new helper method below
        //    s = await client.GetStringAsync(url);

        //    var d = new DateTime(2019, 9, 11, 15, 0, 0);
        //    xxx = JsonConvert.DeserializeObject<Rootobject>(s);
        //    var temperature = xxx.timeSeries.Single(x => x.validTime == d).parameters.Single(x => x.);

        //}
    }
}
