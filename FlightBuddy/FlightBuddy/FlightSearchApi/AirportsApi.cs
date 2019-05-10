using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using Newtonsoft.Json;

namespace FlightBuddy.FlightSearchApi
{
    public class AirportsApi : IAirportsApi
    {
        public async Task<IEnumerable<Airport>> GetAirports()
        {
            const string url = "https://iatacodes.org/api/v6/airports?api_key=38d77104-d093-4840-b2e6-5cbf7d66cfc7";
            var list = new List<Airport>();

            using (var client = new HttpClient())
            {
                string json = string.Empty;
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }

                var airportRoot = JsonConvert.DeserializeObject<AirportList>(json);
                return airportRoot.Response;
            }
        }
    }
}
