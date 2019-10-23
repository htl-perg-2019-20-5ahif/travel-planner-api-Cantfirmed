using System;
using System.Collections.Generic;

namespace TravelPlannerLibrary {

    public class Times {        //Arrival and Departure Times (couldn't think of a better name)
        public string Leave { get; set; }
        public string Arrive { get; set; }
    }

    public class Route {
        public string City { get; set; }

        public List<Times> ToLinz { get; set; }

        public List<Times> FromLinz { get; set; }

    }

    public class TimesWithLocations : Times {
        public string ToCity { get; set; }
        public string FromCity { get; set; }
    }
}
