using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FlightBuddy.LocalDb
{
    public class Flight
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string OriginCode { get; set; }

        [MaxLength(50)]
        public string DestinationCode { get; set; }

        [MaxLength(50)]
        public string Origin { get; set; }

        [MaxLength(50)]
        public string Destination { get; set; }
       
    }
}
