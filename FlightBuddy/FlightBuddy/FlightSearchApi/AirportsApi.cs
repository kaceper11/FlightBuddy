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
            const string url = "https://iatacodes-iatacodes-v1.p.rapidapi.com/api/v5/airports?lang=en&api_key=38d77104-d093-4840-b2e6-5cbf7d66cfc7";

            using (var client = new HttpClient())
            {
                string json = string.Empty;

                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "iatacodes-iatacodes-v1.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "987a76dbf3msh5eca1e7d503b088p1678cejsn88b0a05e0bd5");

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
