﻿namespace BusTicketSystem.DTO
{
    public class FilterAccount
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool? IsActive { get; set; }
    }
}
