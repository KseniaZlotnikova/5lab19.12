using System;
using System.Collections.Generic;

namespace UrbanTraffic
{
    public partial class Stopping
    {
        public Stopping()
        {
            Routes = new HashSet<Routes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeOfTransportId { get; set; }
        public bool EndingStation { get; set; }
        public bool ControlRoom { get; set; }

        public TypeOfTransport TypeOfTransport { get; set; }
        public ICollection<Routes> Routes { get; set; }
    }
}
