using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.ViewModel
{
    public class BadgesList
    {
        public Symbol Symbol { get; set; }
        public string Nombadges { get; set; }
        public string Contenubadges { get; set; }

        public BadgesList(string nombadges, Symbol symbol, string contenubadges)
        {
            Nombadges = nombadges;
            Contenubadges = contenubadges;
            Symbol = symbol;
        }
    }
}