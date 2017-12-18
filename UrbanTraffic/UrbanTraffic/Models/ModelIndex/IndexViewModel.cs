using UrbanTraffic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanTraffic.ViewModel;
using UrbanTraffic.ViewModel.Account;
using UrbanTraffic.ViewModel.Filter;

namespace UrbanTraffic.Models
{
    public class IndexViewModel
    {

        public IEnumerable<Employees> Employees { get; set; }
        public IEnumerable<Routes> Routes { get; set; }
        public IEnumerable<Schedule> Schedule { get; set; }
        public IEnumerable<Schedule> Stoppings { get; set; }
        public IEnumerable<TransportService> TransportService { get; set; }
        public IEnumerable<TypeOfTransport> TypeOfTransport { get; set; }


        public PageViewModel PageViewModel { get; set; }

        public FilterEmployeesViewModel FilterEmployeesViewModel { get; set; }
        public FilterRoutesViewModel FilterRoutesViewModel { get; set; }
        public FilterSchedulesViewModel FilterSchedulesViewModel { get; set; }


    }
}
