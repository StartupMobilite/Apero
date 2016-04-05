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
        public string PhotoEvent { get; set; }
        public string TitreEvent { get; set; }
        public string ThemeEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public int Nbparticipevent { get; set; }
        public DateTime DureeEvent { get; set; }
        public string AdresseEvent { get; set; }
        public string Descripevent { get; set; }

        public Event(string photoEvent, string titreEvent, string themeEvent, DateTime dateEvent, int nbparticipevent, DateTime dureeEvent, string adresseEvent, string descripevent)
        {
            PhotoEvent = photoEvent;
            TitreEvent = titreEvent;
            ThemeEvent = themeEvent;
            DateEvent = dateEvent;
            Nbparticipevent = nbparticipevent;
            DureeEvent = dureeEvent;
            AdresseEvent = adresseEvent;
            Descripevent = descripevent;
        }
    }
}
