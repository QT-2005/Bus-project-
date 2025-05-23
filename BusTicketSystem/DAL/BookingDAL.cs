using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusTicketSystem.Config;
using BusTicketSystem.DTO;

namespace BusTicketSystem.DAL
{
    public class BookingDAL
    {
        public List<Booking> GetAllBookings()
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Bookings
                    .Include(b => b.Account)
                    .Include(b => b.Bus)
                    .Include(b => b.Route)
                    .ToList();
            }
        }

        public Booking GetBookingById(int id)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Bookings
                    .Include(b => b.Account)
                    .Include(b => b.Bus)
                    .Include(b => b.Route)
                    .FirstOrDefault(b => b.Id == id);
            }
        }

        public List<Booking> GetBookingsByAccountId(int accountId)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Bookings
                    .Include(b => b.Account)
                    .Include(b => b.Bus)
                    .Include(b => b.Route)
                    .Where(b => b.AccountId == accountId)
                    .ToList();
            }
        }

        public List<Booking> GetBookingsByFilter(FilterBooking filter)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                var query = context.Bookings
                    .Include(b => b.Account)
                    .Include(b => b.Bus)
                    .Include(b => b.Route)
                    .AsQueryable();

                if (filter.AccountId.HasValue)
                {
                    query = query.Where(b => b.AccountId == filter.AccountId.Value);
                }

                if (filter.BusId.HasValue)
                {
                    query = query.Where(b => b.BusId == filter.BusId.Value);
                }

                if (filter.RouteId.HasValue)
                {
                    query = query.Where(b => b.RouteId == filter.RouteId.Value);
                }

                if (filter.FromDate.HasValue)
                {
                    query = query.Where(b => b.TravelDate >= filter.FromDate.Value);
                }

                if (filter.ToDate.HasValue)
                {
                    query = query.Where(b => b.TravelDate <= filter.ToDate.Value);
                }

                if (!string.IsNullOrEmpty(filter.Status))
                {
                    query = query.Where(b => b.Status == filter.Status);
                }

                return query.ToList();
            }
        }

        public List<string> GetBookedSeats(int busId, int routeId, DateTime travelDate)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                DateTime dateOnly = travelDate.Date;
                DateTime nextDay = dateOnly.AddDays(1);

                return context.Bookings
                    .Where(b => b.BusId == busId &&
                                b.RouteId == routeId &&
                                b.TravelDate >= dateOnly &&
                                b.TravelDate < nextDay &&
                                b.Status != "Cancelled")
                    .Select(b => b.SeatNumber)
                    .ToList();
            }
        }


        public bool AddBooking(Booking booking)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    booking.BookingDate = DateTime.Now;
                    context.Bookings.Add(booking);
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateBooking(Booking booking)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    context.Entry(booking).State = EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool CancelBooking(int id)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    var booking = context.Bookings.Find(id);
                    if (booking != null)
                    {
                        booking.Status = "Cancelled";
                        return context.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBooking(int id)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    var booking = context.Bookings.Find(id);
                    if (booking != null)
                    {
                        context.Bookings.Remove(booking);
                        return context.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
