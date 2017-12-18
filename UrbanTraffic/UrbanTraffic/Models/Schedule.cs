using System;
using System.Collections.Generic;

namespace UrbanTraffic
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int TypeOfTransportId { get; set; }
        public float ArrivaTime { get; set; }
        public string DayOfTheWeek { get; set; }
        public int Year { get; set; }

        public TypeOfTransport TypeOfTransport { get; set; }
    }
}
