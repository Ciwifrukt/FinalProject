using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherVote.Services { 
    public class HttpService
    {
        public async Task<string> Get(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.PostAsync(url, null))
            using (HttpContent content = response.Content)
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Not successful status code");

                return await content.ReadAsStringAsync();
            }
        }
    }
}