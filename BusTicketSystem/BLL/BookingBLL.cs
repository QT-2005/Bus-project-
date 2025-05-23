using System;
using System.Collections.Generic;
using BusTicketSystem.DAL;
using BusTicketSystem.DTO;

namespace BusTicketSystem.BLL
{
    public class BookingBLL
    {
        private readonly BookingDAL _bookingDAL;
        private readonly RouteDAL _routeDAL;

        public BookingBLL()
        {
            _bookingDAL = new BookingDAL();
            _routeDAL = new RouteDAL();
        }

        public List<Booking> GetAllBookings()
        {
            return _bookingDAL.GetAllBookings();
        }

        public Booking GetBookingById(int id)
        {
            return _bookingDAL.GetBookingById(id);
        }

        public List<Booking> GetBookingsByAccountId(int accountId)
        {
            return _bookingDAL.GetBookingsByAccountId(accountId);
        }

        public List<Booking> SearchBookings(FilterBooking filter)
        {
            return _bookingDAL.GetBookingsByFilter(filter);
        }

        public List<string> GetBookedSeats(int busId, int routeId, DateTime travelDate)
        {
            return _bookingDAL.GetBookedSeats(busId, routeId, travelDate);
        }

        public bool CreateBooking(Booking booking)
        {
            // Validate booking data
            if (booking.TravelDate < DateTime.Now.Date)
            {
                return false; // Cannot book for past dates
            }

            // Check if seat is available
            var bookedSeats = _bookingDAL.GetBookedSeats(booking.BusId, booking.RouteId, booking.TravelDate);
            if (bookedSeats.Contains(booking.SeatNumber))
            {
                return false; // Seat already booked
            }

            // Get route price
            var route = _routeDAL.GetRouteById(booking.RouteId);
            if (route != null)
            {
                booking.TotalPrice = route.Price;
            }

            booking.Status = "Confirmed";
            return _bookingDAL.AddBooking(booking);
        }

        public bool UpdateBooking(Booking booking)
        {
            // Validate booking data
            if (booking.TravelDate < DateTime.Now.Date)
            {
                return false; // Cannot book for past dates
            }

            // Get original booking
            var originalBooking = _bookingDAL.GetBookingById(booking.Id);
            if (originalBooking == null)
            {
                return false;
            }

            // If seat changed, check if new seat is available
            if (originalBooking.SeatNumber != booking.SeatNumber)
            {
                var bookedSeats = _bookingDAL.GetBookedSeats(booking.BusId, booking.RouteId, booking.TravelDate);
                if (bookedSeats.Contains(booking.SeatNumber))
                {
                    return false; // Seat already booked
                }
            }

            return _bookingDAL.UpdateBooking(booking);
        }

        public bool CancelBooking(int id)
        {
            var booking = _bookingDAL.GetBookingById(id);
            if (booking == null)
            {
                return false;
            }

            // Cannot cancel past bookings
            if (booking.TravelDate < DateTime.Now.Date)
            {
                return false;
            }

            return _bookingDAL.CancelBooking(id);
        }

        public bool DeleteBooking(int id)
        {
            return _bookingDAL.DeleteBooking(id);
        }
    }
}
