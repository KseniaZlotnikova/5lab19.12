using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanTraffic.ViewModel.Account
{
    public class FilterRoutesViewModel
    {
        public FilterRoutesViewModel(string Name, float TravelTime, float Distance)
        {
            SelectedName = Name;
            SelectedTravelTime = TravelTime;
            SelectedDistance = Distance;
        }

        public string SelectedName { get; set; }
        public float SelectedTravelTime { get; set; }
        public float SelectedDistance { get; set; }
    }
}
