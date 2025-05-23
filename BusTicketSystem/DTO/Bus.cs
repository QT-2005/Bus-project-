using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketSystem.DTO
{
    [Table("Buses")]
    public class Bus
    {
        public Bus()
        {
            Routes = new HashSet<Route>();
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string BusNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string BusType { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }


        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public string DisplayText => $"{BusNumber} - {BusType} ({Capacity} seats)";
    }
}
