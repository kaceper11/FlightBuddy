using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightBuddy.Model;

namespace FlightBuddy.FlightSearchApi
{
    public interface IFlightsApi
    {
        Task<IEnumerable<Flight>> GetFlights(string airportCodeFrom, string airportCodeTo, DateTime date);
    }
}