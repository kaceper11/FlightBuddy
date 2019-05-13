using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FlightBuddy.FlightSearchApi;
using FlightBuddy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlightBuddy.TestUnit
{
    [TestClass]
    public class ApiResponseTest
    {
        private readonly string airportCodeFrom = "SGN";

        private readonly string airportCodeTo = "DAD";

        private readonly DateTime date = DateTime.Now;

        [TestMethod]
        public async Task FlightsResponse()
        {
            //Mock<IFlightsApi> mock = new Mock<IFlightsApi>();

            //var response = await mock.Object.GetFlights(this.airportCodeFrom, this.airportCodeTo, this.date);

            string airportCodeFrom = "SGN";
            string airportCodeTo = "DAD";
            DateTime date = DateTime.Now;

            string url = string.Concat("https://apidojo-kayak-v1.p.rapidapi.com/flights/create-session?origin1=",
                airportCodeFrom, "&destination1=",
                airportCodeTo, "&departdate1=", date.ToString("yyyy-MM-dd"), "&cabin=e&currency=USD&adults=1&bags=0");

            using (var client = new HttpClient())
            {
                string json = string.Empty;
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "apidojo-kayak-v1.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key",
                    "987a76dbf3msh5eca1e7d503b088p1678cejsn88b0a05e0bd5");

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }

                var flightRoot = JsonConvert.DeserializeObject<FlightResponse>(json);

                var xd = string.Empty;
            }
        }

        [TestMethod]
        public async Task AirportsResponse()
        {
            //Mock<IAirportsApi> mock = new Mock<IAirportsApi>();

            //var response = mock.Object.GetAirports();

            const string url = "https://iatacodes.org/api/v6/airports?api_key=38d77104-d093-4840-b2e6-5cbf7d66cfc7";

            using (var client = new HttpClient())
            {
                string json = string.Empty;
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }

                var airportRoot = JsonConvert.DeserializeObject<AirportList>(json);
                var xd = string.Empty;
            }
        }

        [TestMethod]
        public async Task AirportsResponse2()
        {
            const string url = "https://iatacodes.org/api/v6/airports?api_key=38d77104-d093-4840-b2e6-5cbf7d66cfc7";

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
                var xd = string.Empty;
            }
        }


    }
}
