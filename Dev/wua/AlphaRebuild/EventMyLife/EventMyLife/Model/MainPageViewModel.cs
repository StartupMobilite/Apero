using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        
        public MainPageViewModel()
        {

        }

    }
}
