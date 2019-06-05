using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBuddy.Model
{
    public class AirportList
    {
        public AirportList()
        {
        }

        public IEnumerable<Airport> Response { get; set; }

    }

    public class Airport
    {
        public Airport() {}

        public string Code { get; set; }

        public string Name { get; set; }        
    }
}
