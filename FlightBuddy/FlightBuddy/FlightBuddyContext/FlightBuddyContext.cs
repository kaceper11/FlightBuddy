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
            this.Flights = App.MobileService.GetTable<Flight>();
            this.Users = App.MobileService.GetTable<User>();
            this.UserFlights = App.MobileService.GetTable<UserFlight>();
            this.UserFriends = App.MobileService.GetTable<UserFriend>();
        }

        public IMobileServiceTable<Flight> Flights { get; set; }

        public IMobileServiceTable<User> Users { get; set; }

        public IMobileServiceTable<UserFlight> UserFlights { get; set; }

        public IMobileServiceTable<UserFriend> UserFriends { get; set; }


        // User queries

        public void AddUser(User user)
        {
            this.Users.InsertAsync(user);           
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return  (await this.Users.ToListAsync()).FirstOrDefault(user => user.Email == email);
        }

        public async Task<User> GetUserById(string userId)
        {
            return (await this.Users.ToListAsync()).FirstOrDefault(user => user.Id == userId);
        }

        public async void UpdateUser(User user)
        {
            await this.Users.UpdateAsync(user);
        }

        public async Task<string> GetUsersProfilePicture(string userId)
        {
            return (await this.Users.ToListAsync()).FirstOrDefault(user => user.Id == userId).ProfilePictureUrl ?? string.Empty;
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            return !(await this.Users.ToListAsync()).Any(user => user.Email == email);
        }

        public async Task<IEnumerable<FlightViewModel>> GetUserFlights(string userId)
        {
            return  from user in (await this.Users.ToListAsync())
                join userFlight in (await this.UserFlights.ToListAsync()) on user.Id equals userFlight.UserId
                   join flight in (await this.Flights.ToListAsync()) on userFlight.FlightId equals flight.Id
                   where user.Id == userId
                   select new FlightViewModel()
                   {
                       Id = flight.Id,
                       AirlineCode = flight.AirlineCode,
                       Destination = flight.Destination,
                       Origin = flight.Origin,
                       Airline = flight.Airline,
                       ArrivalTimeAirport = flight.ArriveTimeAirport.ToString("dd/MM/yyyy HH:mm"),
                       DestinationCode = flight.DestinationCode,
                       FlightNumber = flight.FlightNumber,
                       LeaveTimeAirport = flight.LeaveTimeAirport.ToString("dd/MM/yyyy HH:mm"),
                       OriginCode = flight.OriginCode,
                       Route = string.Concat(flight.Origin, " - ", flight.Destination),
                       Time = string.Concat(flight.LeaveTimeAirport.ToString("dd/MM/yyyy HH:mm"), " - ", flight.ArriveTimeAirport.ToString("dd/MM/yyyy HH:mm"))
                   };
        }

        public async Task<IEnumerable<string>> GetUserFriendsHelper(string userId)
        {
            var userFriends = (from user in (await this.Users.ToListAsync())
                join userFriend in (await this.UserFriends.ToListAsync()) on user.Id equals userFriend.UserId
                where user.Id == userId
                select userFriend.FriendId).ToList();

            return from user in (await this.Users.ToListAsync())
                where userFriends.Contains(user.Id)
                select user.Id;

        }

        public async Task<IEnumerable<UserFriendViewModel>> GetUserFriends(string userId)
        {
            var friendsToReturn = new List<string>();

            var userFriends = (from user in (await this.Users.ToListAsync())
                               join userFriend in (await this.UserFriends.ToListAsync()) on user.Id equals userFriend.UserId
                where user.Id == userId
                select userFriend.FriendId).ToList();

            foreach (var friendId in userFriends)
            {
                var friendFriends = await this.GetUserFriendsHelper(friendId);
                if (friendFriends.Any(u => u == userId))
                {
                    friendsToReturn.Add(friendId);
                }

            }

            return from user in (await this.Users.ToListAsync())
                   where friendsToReturn.Contains(user.Id)
                   select new UserFriendViewModel()
                    {
                        Id = user.Id,
                        Name = user.Name, 
                        Bio = user.Bio ?? string.Empty,
                        ProfilePictureUrl = user.ProfilePictureUrl ?? string.Empty,
                        FacebookId = user.FacebookId ?? string.Empty
                   };

        }

        public async Task<IEnumerable<FlightViewModel>> GetCommonFlights(string userId, string friendId)
        {
            var userFlights = await this.GetUserFlights(userId);
            var friendFlights = await this.GetUserFlights(friendId);

            return from userFlight in userFlights
                join friendFlight in friendFlights on userFlight.Id equals friendFlight.Id
                select new FlightViewModel()
                {
                    AirlineCode = userFlight.AirlineCode,
                    Destination = userFlight.Destination,
                    Origin = userFlight.Origin,
                    Airline = userFlight.Airline,
                    ArrivalTimeAirport = userFlight.ArrivalTimeAirport,
                    DestinationCode = userFlight.DestinationCode,
                    FlightNumber = userFlight.FlightNumber,
                    LeaveTimeAirport = userFlight.LeaveTimeAirport,
                    OriginCode = userFlight.OriginCode,
                    Route = string.Concat(userFlight.Origin, " - ", userFlight.Destination),
                    Time = string.Concat(userFlight.LeaveTimeAirport, " - ", userFlight.ArrivalTimeAirport)
                };
        }


        // Flight queries

        public void AddFlight(Flight flight)
        {
            this.Flights.InsertAsync(flight);
        }

        public async Task<Flight> GetFlight(string flightId)
        {
            return (await this.Flights.ToListAsync()).FirstOrDefault(flight => flight.Id == flightId);
        }

        public async Task<string> GetFlightIdByFlightNumber(Flight flight)
        {
            return  (await this.Flights.ToListAsync()).SingleOrDefault(f => f.FlightNumber == flight.FlightNumber).Id;
        }

        public async Task<bool> CheckIfFlightExists(Flight flight)
        {
            return !(await this.Flights.ToListAsync()).Any(f => f.FlightNumber == flight.FlightNumber);
        }

        public async Task<IEnumerable<UserFriendViewModel>> GetFlightParticipants(string flightId)
        {
            return from user in (await this.Users.ToListAsync())
                join userFlight in (await this.UserFlights.ToListAsync()) on user.Id equals userFlight.UserId
                join flight in (await this.Flights.ToListAsync()) on userFlight.FlightId equals flight.Id
                where flight.Id == flightId && user.Id != App.User.Id
                select new UserFriendViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    MobileNumber = user.MobileNumber,
                    Name = user.Name,
                    Bio = user.Bio ?? string.Empty,
                    ProfilePictureUrl = user.ProfilePictureUrl ?? string.Empty,
                    FacebookId = user.FacebookId ?? string.Empty                                       
                };
        }

        // UserFriend queries

        public void AddUserFriend(UserFriend userFriend)
        {
            this.UserFriends.InsertAsync(userFriend);
        }

        public async Task<bool> CheckIfUserFriendExists(UserFriend userFriend)
        {
            return !(await this.UserFriends.ToListAsync()).Any(uf => uf.FriendId == userFriend.FriendId
                                                                           && uf.UserId == userFriend.UserId);
        }

        // UserFlight queries

        public void AddUserFlight(UserFlight userFlight)
        {
            this.UserFlights.InsertAsync(userFlight);
        }

        public async Task<bool> CheckIfUserFlightExists(UserFlight userFlight)
        {
            return !(await this.UserFlights.ToListAsync()).Any(uf => uf.FlightId == userFlight.FlightId
                                                                           && uf.UserId == userFlight.UserId);
        }
    }
}
