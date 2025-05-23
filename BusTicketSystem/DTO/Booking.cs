using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketSystem.DTO
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int BusId { get; set; }

        public int RouteId { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime TravelDate { get; set; }

        [Required]
        [StringLength(10)]
        public string SeatNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [ForeignKey("BusId")]
        public virtual Bus Bus { get; set; }

        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
    }
}
