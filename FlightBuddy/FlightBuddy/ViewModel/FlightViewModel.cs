using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBuddy.ViewModel
{
    public class FlightViewModel
    {
        public string Id { get; set; }

        public string FlightNumber { get; set; }

        public string AirlineCode { get; set; }

        public string OriginCode { get; set; }

        public string DestinationCode { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime LeaveTimeAirport { get; set; }

        public DateTime ArrivalTimeAirport { get; set; }
    }
}
