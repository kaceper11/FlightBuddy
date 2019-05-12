using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FlightBuddy.LocalDb
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string UserId { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string MobileNumber { get; set; }
    }
}
