using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusTicketSystem.Config;
using BusTicketSystem.DTO;

namespace BusTicketSystem.DAL
{
    public class RouteDAL
    {
        public List<Route> GetAllRoutes()
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Routes.ToList();
            }
        }

        public List<Route> GetActiveRoutes()
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Routes.Where(r => r.IsActive).ToList();
            }
        }

        public Route GetRouteById(int id)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Routes.Find(id);
            }
        }

        public List<Route> GetRoutesByFilter(FilterRoute filter)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                var query = context.Routes.AsQueryable();

                if (!string.IsNullOrEmpty(filter.DepartureLocation))
                {
                    query = query.Where(r => r.DepartureLocation.Contains(filter.DepartureLocation));
                }

                if (!string.IsNullOrEmpty(filter.ArrivalLocation))
                {
                    query = query.Where(r => r.ArrivalLocation.Contains(filter.ArrivalLocation));
                }

                if (filter.IsActive.HasValue)
                {
                    query = query.Where(r => r.IsActive == filter.IsActive.Value);
                }

                return query.ToList();
            }
        }

        public List<Route> GetRoutesByBusId(int busId)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Buses
                    .Include(b => b.Routes)
                    .FirstOrDefault(b => b.Id == busId)?.Routes.ToList() ?? new List<Route>();
            }
        }

        public bool AddRoute(Route route)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    context.Routes.Add(route);
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateRoute(Route route)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    context.Entry(route).State = EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteRoute(int id)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    var route = context.Routes.Find(id);
                    if (route != null)
                    {
                        context.Routes.Remove(route);
                        return context.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
