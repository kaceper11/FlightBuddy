using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FlightBuddy.Model
{
    public class UserFriend
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FriendId { get; set; }
    }
}
