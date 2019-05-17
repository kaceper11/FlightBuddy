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

                if (flightRoot != null)
                {
                    var listOfFlights = flightRoot.Flights.Select(d => d.Value).ToList();

                    foreach (var flight in listOfFlights)
                    {
                        string airLine = string.Empty;
                        AirportDetail origin = new AirportDetail();
                        AirportDetail destination = new AirportDetail();


                        if (flightRoot.AirLines.TryGetValue(flight.AirlineCode, out airLine))
                        {
                            flight.Airline = airLine;
                        }

                        if (flightRoot.AirportDetails.TryGetValue(flight.OriginCode, out origin))
                        {
                            flight.Origin = origin.Name;
                        }

                        if (flightRoot.AirportDetails.TryGetValue(flight.DestinationCode, out destination))
                        {
                            flight.Destination = destination.Name;
                        }

                        flight.Route = string.Concat(flight.Origin, " - ", flight.Destination);
                        flight.Time = string.Concat(flight.LeaveTimeAirport.ToString("dd/MM/yyyy HH:mm"), " - ", flight.ArrivalTimeAirport.ToString("dd/MM/yyyy HH:mm"));

                    }

                    return listOfFlights;
                }

               return new List<Flight>();
            }
        }
    }
}
