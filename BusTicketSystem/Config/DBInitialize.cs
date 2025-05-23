using BusTicketSystem.DTO;
using System;
using System.Data.Entity;
using System.Security.Principal;

namespace BusTicketSystem.Config
{
    public static class DBInitialize
    {

        public class BusTicketSystemInitializer : CreateDatabaseIfNotExists<BusTicketSystemDB>
        {
            protected override void Seed(BusTicketSystemDB context)
            {
                // Add admin account
                var adminAccount = new Account
                {
                    Username = "admin",
                    Password = "admin123", // In a real app, this should be hashed
                    FullName = "System Administrator",
                    Email = "admin@busticket.com",
                    Phone = "0123456789",
                    Role = "Admin",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                context.Accounts.Add(adminAccount);

                // Add user account
                var userAccount = new Account
                {
                    Username = "user",
                    Password = "user123", // In a real app, this should be hashed
                    FullName = "Regular User",
                    Email = "user@busticket.com",
                    Phone = "0987654321",
                    Role = "User",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                context.Accounts.Add(userAccount);

                // Add buses
                var bus1 = new Bus
                {
                    BusNumber = "B001",
                    BusType = "Sleeper",
                    Capacity = 40,
                    IsActive = true
                };
                var bus2 = new Bus
                {
                    BusNumber = "B002",
                    BusType = "Seater",
                    Capacity = 50,
                    IsActive = true
                };
                var bus3 = new Bus
                {
                    BusNumber = "B003",
                    BusType = "Double Decker",
                    Capacity = 60,
                    IsActive = true
                };
                var bus4 = new Bus
                {
                    BusNumber = "B004",
                    BusType = "Minibus",
                    Capacity = 20,
                    IsActive = true
                };

                context.Buses.Add(bus1);
                context.Buses.Add(bus2);
                context.Buses.Add(bus3);
                context.Buses.Add(bus4);

                // Add routes
                var route1 = new Route
                {
                    DepartureLocation = "Hanoi",
                    ArrivalLocation = "Ho Chi Minh City",
                    Distance = 1720,
                    Duration = 36, // hours
                    Price = 1200000, // VND
                    IsActive = true
                };
                var route2 = new Route
                {
                    DepartureLocation = "Hanoi",
                    ArrivalLocation = "Da Nang",
                    Distance = 800,
                    Duration = 16, // hours
                    Price = 700000, // VND
                    IsActive = true
                };
                var route3 = new Route
                {
                    DepartureLocation = "Ho Chi Minh City",
                    ArrivalLocation = "Can Tho",
                    Distance = 170,
                    Duration = 4, // hours
                    Price = 200000, // VND
                    IsActive = true
                };
                var route4 = new Route
                {
                    DepartureLocation = "Da Nang",
                    ArrivalLocation = "Hue",
                    Distance = 100,
                    Duration = 3, // hours
                    Price = 150000, // VND
                    IsActive = true
                };

                context.Routes.Add(route1);
                context.Routes.Add(route2);
                context.Routes.Add(route3);
                context.Routes.Add(route4);

                // Save to generate Ids
                context.SaveChanges();

                // Associate buses with routes (many-to-many)
                bus1.Routes.Add(route1);
                bus1.Routes.Add(route2);
                bus2.Routes.Add(route1);

                bus3.Routes.Add(route3);
                bus3.Routes.Add(route4);
                bus4.Routes.Add(route4);

                context.SaveChanges();

                // Add bookings
                var booking1 = new Booking
                {
                    AccountId = userAccount.Id,
                    BusId = bus1.Id,
                    RouteId = route1.Id,
                    BookingDate = DateTime.Now,
                    TravelDate = DateTime.Now.AddDays(7),
                    SeatNumber = "A1",
                    Status = "Confirmed",
                    TotalPrice = route1.Price
                };

                var booking2 = new Booking
                {
                    AccountId = userAccount.Id,
                    BusId = bus1.Id,
                    RouteId = route2.Id,
                    BookingDate = DateTime.Now,
                    TravelDate = DateTime.Now.AddDays(10),
                    SeatNumber = "B2",
                    Status = "Confirmed",
                    TotalPrice = route2.Price
                };

                var booking3 = new Booking
                {
                    AccountId = userAccount.Id,
                    BusId = bus2.Id,
                    RouteId = route1.Id,
                    BookingDate = DateTime.Now,
                    TravelDate = DateTime.Now.AddDays(14),
                    SeatNumber = "C3",
                    Status = "Confirmed",
                    TotalPrice = route1.Price
                };

                var booking4 = new Booking
                {
                    AccountId = userAccount.Id,
                    BusId = bus2.Id,
                    RouteId = route1.Id,
                    BookingDate = DateTime.Now,
                    TravelDate = DateTime.Now.AddDays(3),
                    SeatNumber = "D4",
                    Status = "Pending",
                    TotalPrice = route1.Price
                };

                var booking5 = new Booking
                {
                    AccountId = userAccount.Id,
                    BusId = bus1.Id,
                    RouteId = route2.Id,
                    BookingDate = DateTime.Now,
                    TravelDate = DateTime.Now.AddDays(5),
                    SeatNumber = "E5",
                    Status = "Cancelled",
                    TotalPrice = route2.Price
                };

                context.Bookings.Add(booking1);
                context.Bookings.Add(booking2);
                context.Bookings.Add(booking3);
                context.Bookings.Add(booking4);
                context.Bookings.Add(booking5);

                context.SaveChanges();

                base.Seed(context);
            }
        }

    }
}
