namespace BusTicketSystem.DTO
{
    public class FilterRoute
    {
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public bool? IsActive { get; set; }
    }
}
