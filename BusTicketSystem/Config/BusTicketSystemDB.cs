using BusTicketSystem.DTO;
using System;
using System.Data.Entity;
using System.Linq;
using static BusTicketSystem.Config.DBInitialize;

namespace BusTicketSystem.Config
{
    public class BusTicketSystemDB : DbContext
    {
        public BusTicketSystemDB() : base("name=BusTicketSystemDB")
        {
            Console.WriteLine("Connection string:");
            Console.WriteLine(this.Database.Connection.ConnectionString);
            Database.SetInitializer<BusTicketSystemDB>(new BusTicketSystemInitializer());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}