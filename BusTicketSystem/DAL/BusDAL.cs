using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusTicketSystem.Config;
using BusTicketSystem.DTO;

namespace BusTicketSystem.DAL
{
    public class BusDAL
    {
        public List<Bus> GetAllBuses()
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Buses.ToList();
            }
        }

        public List<Bus> GetActiveBuses()
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Buses.Where(b => b.IsActive).ToList();
            }
        }

        public Bus GetBusById(int id)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Buses.Find(id);
            }
        }

        public List<Bus> GetBusesByRouteId(int routeId)
        {
            using (var context = new Config.BusTicketSystemDB())
            {
                return context.Routes
                    .Include(r => r.Buses)
                    .FirstOrDefault(r => r.Id == routeId)?.Buses.ToList() ?? new List<Bus>();
            }
        }

        public bool AddBus(Bus bus)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    context.Buses.Add(bus);
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateBus(Bus bus)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    context.Entry(bus).State = EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBus(int id)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    var bus = context.Buses.Find(id);
                    if (bus != null)
                    {
                        context.Buses.Remove(bus);
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

        public bool AssignBusToRoute(int busId, int routeId)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    var bus = context.Buses.Include(b => b.Routes).FirstOrDefault(b => b.Id == busId);
                    var route = context.Routes.Find(routeId);

                    if (bus != null && route != null)
                    {
                        if (!bus.Routes.Any(r => r.Id == routeId))
                        {
                            bus.Routes.Add(route);
                            return context.SaveChanges() > 0;
                        }
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveBusFromRoute(int busId, int routeId)
        {
            try
            {
                using (var context = new Config.BusTicketSystemDB())
                {
                    var bus = context.Buses.Include(b => b.Routes).FirstOrDefault(b => b.Id == busId);

                    if (bus != null)
                    {
                        var route = bus.Routes.FirstOrDefault(r => r.Id == routeId);
                        if (route != null)
                        {
                            bus.Routes.Remove(route);
                            return context.SaveChanges() > 0;
                        }
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
