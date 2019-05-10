using System.Collections.Generic;
using System.Threading.Tasks;
using FlightBuddy.Model;

namespace FlightBuddy.FlightSearchApi
{
    public interface IAirportsApi
    {
        Task<IEnumerable<Airport>> GetAirports();
    }
}