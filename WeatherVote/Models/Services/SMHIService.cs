using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherVote.Models.Services
{
    public class SMHIService
    {

        public async Task<Weather> GetAllWeather()
        {
            var smhiroot = new SMHI.Rootobjectsmhi();
            var w = new Weather();
            var client = new HttpClient();
            var s = "";
            var url = "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/17.9446/lat/59.40719/data.json";

            s = await client.GetStringAsync(url);

            var d = new DateTime(2019, 9, 16, 15, 0, 0);
            smhiroot = JsonConvert.DeserializeObject<SMHI.Rootobjectsmhi>(s);
            var temperature = smhiroot.timeSeries.Single(x => x.validTime == d).parameters.Single(x => x.name == "t").values;
            w.Temperatur = temperature[0];

            return w;
        }

    }
}
