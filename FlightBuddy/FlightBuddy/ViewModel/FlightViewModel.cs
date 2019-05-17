using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBuddy.ViewModel
{
    public class FlightViewModel
    {
        public FlightViewModel()
        {
            Route = string.Concat(Origin, " - ", Destination);
            Time = string.Concat(LeaveTimeAirport, " - ", ArrivalTimeAirport);
        }

        public string Id { get; set; }

        public string FlightNumber { get; set; }

        public string AirlineCode { get; set; }

        public string OriginCode { get; set; }

        public string DestinationCode { get; set; }

        public string Origin { get; set; }

        public string Airline { get; set; }

        public string Destination { get; set; }

        public string LeaveTimeAirport { get; set; }

        public string ArrivalTimeAirport { get; set; }

        public string Route { get; set; }

        public string Time { get; set; }
    }
}
