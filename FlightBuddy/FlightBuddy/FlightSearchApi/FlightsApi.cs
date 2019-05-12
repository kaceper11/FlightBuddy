using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using Newtonsoft.Json;

namespace FlightBuddy.FlightSearchApi
{
    public class FlightsApi : IFlightsApi
    {
        public async Task<IEnumerable<Flight>> GetFlights(string airportCodeFrom, string airportCodeTo, string date)
        {
            string url = string.Concat("https://apidojo-kayak-v1.p.rapidapi.com/flights/create-session?origin1=", airportCodeFrom, "&destination1=", 
                airportCodeTo,"&departdate1=", date,"&cabin=e&currency=USD&adults=1&bags=0");
            var list = new List<Flight>();

            using (var client = new HttpClient())
            {
                string json = string.Empty;
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "apidojo-kayak-v1.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "987a76dbf3msh5eca1e7d503b088p1678cejsn88b0a05e0bd5");

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }

                var flightRoot = JsonConvert.DeserializeObject<FlightResponse>(json);
                return flightRoot.Flights.Select(d => d.Value).ToList();
                
            }
        }
    }
}
