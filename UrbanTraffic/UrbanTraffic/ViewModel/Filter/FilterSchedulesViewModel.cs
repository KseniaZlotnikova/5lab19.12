using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanTraffic.ViewModel.Filter
{
    public class FilterSchedulesViewModel
    {
        public FilterSchedulesViewModel(string Name, float ArrivaTime)
        {
            SelectedName = Name;
            SelectedArrivaTime = ArrivaTime;
        }

        public string SelectedName { get; set; }
        public float SelectedArrivaTime { get; set; }

    }
}
