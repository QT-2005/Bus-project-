using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketSystem.DTO
{
    [Table("Accounts")]
    public class Account
    {
        public Account()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
