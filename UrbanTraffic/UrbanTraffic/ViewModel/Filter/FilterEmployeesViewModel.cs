using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanTraffic.ViewModel.Filter
{
    public class FilterEmployeesViewModel
    {
        public FilterEmployeesViewModel(string Name, string Surname, string MiddleName)
        {
            SelectedName = Name;
            SelectedSurname = Surname;
            SelectedMiddleName = MiddleName;
        }

        public string SelectedName { get; set; }
        public string SelectedSurname { get; set; }
        public string SelectedMiddleName { get; set; }
    }
}
