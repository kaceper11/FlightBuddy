using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace FlightBuddy.LocalDb
{
    public class LocalDb
    {
        public LocalDb()
        {

        }

        public void AddUser(User user)
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<User>();
                con.Insert(user);
            }
        }

        public bool CheckIfUserEmpty()
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<User>();
                return con.Table<User>().ToList().Any();
            }
        }

        public void UpdateUser(User user)
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<User>();
                con.Update(user);
            }
        }

        public User GetUser()
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<User>();
                return con.Table<User>().ToList().FirstOrDefault();
            }
        }

        public void DeleteUsers()
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<User>();
                con.DeleteAll<User>();
            }
        }

        public void AddFlight(Flight flight)
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Flight>();
                con.Insert(flight);
            }
        }

        public bool CheckIfFlightEmpty()
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Flight>();
                return con.Table<Flight>().ToList().Any();
            }
        }

        public void DeleteFlights()
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Flight>();
                con.DeleteAll<Flight>();
            }
        }

        public void UpdateFlight(Flight flight)
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Flight>();
                con.Update(flight);
            }
        }

        public Flight GetFlight()
        {
            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Flight>();
                return con.Table<Flight>().ToList().FirstOrDefault();
            }
        }
    }
}
