using System.Collections.Generic;
using BusTicketSystem.DAL;
using BusTicketSystem.DTO;

namespace BusTicketSystem.BLL
{
    public class BusBLL
    {
        private readonly BusDAL _busDAL;

        public BusBLL()
        {
            _busDAL = new BusDAL();
        }

        public List<Bus> GetAllBuses()
        {
            return _busDAL.GetAllBuses();
        }

        public List<Bus> GetActiveBuses()
        {
            return _busDAL.GetActiveBuses();
        }

        public Bus GetBusById(int id)
        {
            return _busDAL.GetBusById(id);
        }

        public List<Bus> GetBusesByRouteId(int routeId)
        {
            return _busDAL.GetBusesByRouteId(routeId);
        }

        public bool AddBus(Bus bus)
        {
            return _busDAL.AddBus(bus);
        }

        public bool UpdateBus(Bus bus)
        {
            return _busDAL.UpdateBus(bus);
        }

        public bool DeleteBus(int id)
        {
            return _busDAL.DeleteBus(id);
        }

        public bool AssignBusToRoute(int busId, int routeId)
        {
            return _busDAL.AssignBusToRoute(busId, routeId);
        }

        public bool RemoveBusFromRoute(int busId, int routeId)
        {
            return _busDAL.RemoveBusFromRoute(busId, routeId);
        }
    }
}
