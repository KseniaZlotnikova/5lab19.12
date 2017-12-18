using System;
using System.Collections.Generic;

namespace UrbanTraffic
{
    public partial class Routes
    {
        public Routes()
        {
            TransportService = new HashSet<TransportService>();
        }

        public int Id { get; set; }
        public int StoppingId { get; set; }
        public float TravelTime { get; set; }
        public float Distance { get; set; }
        public bool ExpressRoute { get; set; }

        public Stopping Stopping { get; set; }
        public ICollection<TransportService> TransportService { get; set; }
    }
}
