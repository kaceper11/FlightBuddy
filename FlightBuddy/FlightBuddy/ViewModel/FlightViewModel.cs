using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBuddy.ViewModel
{
    public class FlightViewModel
    {
        public string FlightNumber { get; set; }

        public string AirlineCode { get; set; }

        public string OriginCode { get; set; }

        public string DestinationCode { get; set; }

        public DateTime LeaveTimeAirport { get; set; }

        public DateTime ArrivalTimeAirport { get; set; }
    }
}
