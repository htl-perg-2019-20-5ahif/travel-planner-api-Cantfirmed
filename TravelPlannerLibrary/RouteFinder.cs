using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelPlannerLibrary {
    public class RouteFinder {
        public List<Route> Routes;
        public RouteFinder(List<Route> Routes) {
            this.Routes = Routes;
        }
        public TimesWithLocations GetFastestRoute(string Dep, string Dest, string StartTime) {
            List<Times> Times = new List<Times>();
            if (Dest.Equals("Linz")) {
                Times = Routes.First(r => r.City == Dep).ToLinz;
            }
            if (Dep.Equals("Linz")) {
                Times = Routes.First(r => r.City == Dest).FromLinz;
            }

            var trip = Times.FirstOrDefault(t => t.Leave.CompareTo(StartTime) >= 0);
            if (trip != null) {
                return new TimesWithLocations {
                    Arrive = trip.Arrive,
                    Leave = trip.Leave,
                    ToCity = Dep,
                    FromCity = Dest,
                };

            }
            //if (Route.City.Equals(Dest) ||  {
            //    return Route;
            //}

            return null;
        }
    }
}
