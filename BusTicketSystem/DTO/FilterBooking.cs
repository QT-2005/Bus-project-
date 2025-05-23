using System;

namespace BusTicketSystem.DTO
{
    public class FilterBooking
    {
        public int? AccountId { get; set; }
        public int? BusId { get; set; }
        public int? RouteId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Status { get; set; }
    }
}
