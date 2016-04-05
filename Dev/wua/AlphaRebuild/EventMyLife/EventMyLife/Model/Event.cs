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
        public int NbParticipEvent { get; set; }
        public DateTime DureeEvent { get; set; }
        public string AdresseEvent { get; set; }
        public string DescripEvent { get; set; }

        public Event(string photoEvent, string titreEvent, string themeEvent, DateTime dateEvent, int nbParticipEvent, DateTime dureeEvent, string adresseEvent, string descripEvent)
        {
            PhotoEvent = photoEvent;
            TitreEvent = titreEvent;
            ThemeEvent = themeEvent;
            DateEvent = dateEvent;
            NbParticipEvent = nbParticipEvent;
            DureeEvent = dureeEvent;
            AdresseEvent = adresseEvent;
            DescripEvent = descripEvent;
        }
    }
}
