using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.Model
{
    public class ContactListClass
    {
        public string Nomcontact { get; set; }
        public string Photocontact { get; set; }

        public ContactListClass(string nomcontact, string photocontact)
        {
            Nomcontact = nomcontact;
            Photocontact = photocontact;
        }
    }
}
