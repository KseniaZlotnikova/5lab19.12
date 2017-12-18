using System;
using System.Collections.Generic;

namespace UrbanTraffic
{
    public partial class Employees
    {
        public Employees()
        {
            TransportService = new HashSet<TransportService>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public int WorkExperience { get; set; }

        public ICollection<TransportService> TransportService { get; set; }
    }
}
