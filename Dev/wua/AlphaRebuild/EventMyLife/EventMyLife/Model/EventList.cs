using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.Model
{
    public class EventList
    {
        public string Nomevent { get; set; }
        public string Photoevent { get; set; }

        public EventList(string nomevent, string photoevent)
        {
            Nomevent = nomevent;
            Photoevent = photoevent;
        }
    }
}
