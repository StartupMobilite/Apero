using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.ViewModel
{
    public class MenuItem
    {
        public Symbol Symbol { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }

        public MenuItem(string title, Symbol symbol,string id)
        {
            Id = id;
            Title = title;
            Symbol = symbol;
        }
    }
}
