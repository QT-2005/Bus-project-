using System.Collections.Generic;
using BusTicketSystem.DAL;
using BusTicketSystem.DTO;

namespace BusTicketSystem.BLL
{
    public class RouteBLL
    {
        private readonly RouteDAL _routeDAL;

        public RouteBLL()
        {
            _routeDAL = new RouteDAL();
        }

        public List<Route> GetAllRoutes()
        {
            return _routeDAL.GetAllRoutes();
        }

        public List<Route> GetActiveRoutes()
        {
            return _routeDAL.GetActiveRoutes();
        }

        public Route GetRouteById(int id)
        {
            return _routeDAL.GetRouteById(id);
        }

        public List<Route> SearchRoutes(FilterRoute filter)
        {
            return _routeDAL.GetRoutesByFilter(filter);
        }

        public List<Route> GetRoutesByBusId(int busId)
        {
            return _routeDAL.GetRoutesByBusId(busId);
        }

        public bool AddRoute(Route route)
        {
            return _routeDAL.AddRoute(route);
        }

        public bool UpdateRoute(Route route)
        {
            return _routeDAL.UpdateRoute(route);
        }

        public bool DeleteRoute(int id)
        {
            return _routeDAL.DeleteRoute(id);
        }
    }
}
