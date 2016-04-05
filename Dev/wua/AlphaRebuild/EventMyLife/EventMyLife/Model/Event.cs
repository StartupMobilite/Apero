using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.Model
{
    public class Event
    {
        public string Photoevent { get; set; }
        public string Titreevent { get; set; }
        public string Themeevent { get; set; }
        public DateTime Dateevent { get; set; }
        public int Nbparticipevent { get; set; }
        public DateTime Dureeevent { get; set; }
        public string Adresseevent { get; set; }
        public string Descripevent { get; set; }

        public Event(string phtoevent, string titreevent, string themeevent, DateTime dateevent, int nbparticipevent, DateTime dureeevent, string adresseevent, string descripevent)
        {
            Photoevent = phtoevent;
            Titreevent = titreevent;
            Themeevent = themeevent;
            Dateevent = dateevent;
            Nbparticipevent = nbparticipevent;
            Dureeevent = dureeevent;
            Adresseevent = adresseevent;
            Descripevent = descripevent;
        }
    }
}
