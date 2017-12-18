using System;
using System.Collections.Generic;

namespace UrbanTraffic
{
    public partial class TransportService
    {
        public int Id { get; set; }
        public int? TypeOfTransportId { get; set; }
        public int RoutesId { get; set; }
        public int EmployeesId { get; set; }
        public DateTime Date { get; set; }
        public int Change { get; set; }

        public Employees Employees { get; set; }
        public Routes Routes { get; set; }
        public TypeOfTransport TypeOfTransport { get; set; }
    }
}
