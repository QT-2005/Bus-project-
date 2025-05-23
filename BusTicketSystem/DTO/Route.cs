using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketSystem.DTO
{
    [Table("Routes")]
    public class Route
    {
        public Route()
        {
            Buses = new HashSet<Bus>();
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartureLocation { get; set; }

        [Required]
        [StringLength(100)]
        public string ArrivalLocation { get; set; }


        public double Distance { get; set; }

        public double Duration { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Bus> Buses { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public string DisplayText
        {
            get
            {
                return $"{DepartureLocation} to {ArrivalLocation} - {Price:C}";
            }
        }
    }
}
