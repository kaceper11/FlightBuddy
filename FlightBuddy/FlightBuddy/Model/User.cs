using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FlightBuddy.Model
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string MobileNumber { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Bio { get; set; }

        public string FacebookId { get; set; }
    }
}
