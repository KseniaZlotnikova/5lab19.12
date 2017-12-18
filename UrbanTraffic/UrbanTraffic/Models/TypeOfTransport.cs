using System;
using System.Collections.Generic;

namespace UrbanTraffic
{
    public partial class TypeOfTransport
    {
        public TypeOfTransport()
        {
            Schedule = new HashSet<Schedule>();
            Stopping = new HashSet<Stopping>();
            TransportService = new HashSet<TransportService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<Stopping> Stopping { get; set; }
        public ICollection<TransportService> TransportService { get; set; }
    }
}
