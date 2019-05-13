using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FlightBuddy.Model
{
    public class Flight
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "flightNumber")]
        public string FlightNumber { get; set; }

        [JsonProperty(PropertyName = "airlineCode")]
        public string AirlineCode { get; set; }

        public string Airline { get; set; }

        [JsonProperty(PropertyName = "originCode")]
        public string OriginCode { get; set; }

        [JsonProperty(PropertyName = "destinationCode")]
        public string DestinationCode { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        [JsonProperty(PropertyName = "leaveTimeAirport")]
        public DateTime LeaveTimeAirport { get; set; }

        [JsonProperty(PropertyName = "arrivalTimeAirport")]
        public DateTime ArrivalTimeAirport { get; set; }
    }

    public class FlightResponse
    {
        [JsonProperty(PropertyName = "searchid")]
        public string SearchId { get; set; }

        [JsonProperty(PropertyName = "morepending")]
        public bool MorePending { get; set; }

        [JsonProperty(PropertyName = "resultcount")]
        public int ResultCount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "error")]
        public bool Error { get; set; }

        [JsonProperty(PropertyName = "shareURL")]
        public string ShareURL { get; set; }

        [JsonProperty(PropertyName = "airlines")]
        public Dictionary<string, string> AirLines { get; set; }

        [JsonProperty(PropertyName = "airlineLogos")]
        public Dictionary<string, string> AirlineLogos { get; set; }

        [JsonProperty(PropertyName = "airportDetails")]
        public Dictionary<string, AirportDetail> AirportDetails { get; set; }

        [JsonProperty(PropertyName = "layoverAirports")]
        public Dictionary<string, string> LayoverAirports { get; set; }

        [JsonProperty(PropertyName = "airportSummary")]
        public string AirportSummary { get; set; }

        [JsonProperty(PropertyName = "departDate")]
        public string DepartDate { get; set; }

        [JsonProperty(PropertyName = "segset")]
        public Dictionary<string, Flight> Flights { get; set; }

    }

    public class AirportDetail
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "ctid")]
        public string Ctid { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public string Lat { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public string Lon { get; set; }
    }
}



