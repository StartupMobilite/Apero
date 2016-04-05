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
        public string NameEvent { get; set; }
        public string PhotoEvent { get; set; }

        public EventList(string nameEvent, string photoEvent)
        {
            NameEvent = nameEvent;
            PhotoEvent = photoEvent;
        }
    }
}
