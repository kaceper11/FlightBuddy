using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using FlightBuddy.ViewModel;
using Microsoft.WindowsAzure.MobileServices;

namespace FlightBuddy.FlightBuddyContext
{
    public class FlightBuddyContext
    {
        public FlightBuddyContext()
        {
            this.FlightsList = App.MobileService.GetTable<Flight>().ToListAsync();
            this.UsersList = App.MobileService.GetTable<User>().ToListAsync();
            this.UserFlightsList = App.MobileService.GetTable<UserFlight>().ToListAsync();
            this.UserFriendsList = App.MobileService.GetTable<UserFriend>().ToListAsync();
            this.Flights = App.MobileService.GetTable<Flight>();
            this.Users = App.MobileService.GetTable<User>();
            this.UserFlights = App.MobileService.GetTable<UserFlight>();
            this.UserFriends = App.MobileService.GetTable<UserFriend>();
        }

        public Task<List<Flight>> FlightsList { get; set; }

        public Task<List<User>> UsersList { get; set; }

        public Task<List<UserFlight>> UserFlightsList { get; set; }

        public Task<List<UserFriend>> UserFriendsList { get; set; }

        public IMobileServiceTable<Flight> Flights { get; set; }

        public IMobileServiceTable<User> Users { get; set; }

        public IMobileServiceTable<UserFlight> UserFlights { get; set; }

        public IMobileServiceTable<UserFriend> UserFriends { get; set; }


        // User queries

        public void AddUser(User user)
        {
            this.Users.InsertAsync(user);
        }

        public User GetUser(string email)
        {
            return this.UsersList.GetAwaiter().GetResult().FirstOrDefault(user => user.Email == email);
        }

        public IEnumerable<FlightViewModel> GetUserFlights(string userId)
        {
            return from user in this.UsersList.GetAwaiter().GetResult()
                join userFlight in this.UserFlightsList.GetAwaiter().GetResult() on user.Id equals userFlight.UserId
                   join flight in this.FlightsList.GetAwaiter().GetResult() on userFlight.FlightId equals flight.Id
                   where user.Id == userId
                   select new FlightViewModel()
                   {
                       AirlineCode = flight.AirlineCode,
                       ArrivalTimeAirport = flight.ArrivalTimeAirport,
                       DestinationCode = flight.DestinationCode,
                       FlightNumber = flight.FlightNumber,
                       LeaveTimeAirport = flight.LeaveTimeAirport,
                       OriginCode = flight.OriginCode
                   };

        }

        public IEnumerable<UserFriendVIewModel> GetUserFriends(string userId)
        {
            var userFriends = (from user in this.UsersList.GetAwaiter().GetResult()
                join userFriend in this.UserFriendsList.GetAwaiter().GetResult() on user.Id equals userFriend.UserId
                where user.Id == userId
                select userFriend.FriendId).ToList();

            return from user in this.UsersList.GetAwaiter().GetResult()
                   where userFriends.Contains(user.Id)
                   select new UserFriendVIewModel()
                    {
                       Email = user.Email,
                        MobileNumber = user.MobileNumber,
                        Name = user.Name
                    };

        }

        public IEnumerable<FlightViewModel> GetCommonFlights(string userId, string friendId)
        {
            var userFlights = this.GetUserFlights(userId);
            var friendFlights = this.GetUserFlights(friendId);
            return userFlights.Intersect(friendFlights);
        }
    }
}
