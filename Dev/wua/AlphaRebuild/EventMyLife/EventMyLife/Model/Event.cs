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
        public string Id { get; set; }
        public string PhotoEvent { get; set; }
        public string TitreEvent { get; set; }
        public string ThemeEvent { get; set; }
        public int NbParticipEvent { get; set; }
        public string AdresseEvent { get; set; }
        public string DescripEvent { get; set; }
        public string DateEvent { get; set; }
        public string IdUser { get; set; }
    }
}
