using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.Model
{
    public class ProfilAdvanceClass
    {
        public string Pseudo { get; set; }
        public string Newmdp { get; set; }
        public string Confnewmdp { get; set; }
        public string Mail { get; set; }
        public bool Discut { get; set; }
        public int Tel { get; set; }

        public ProfilAdvanceClass(string pseudo, string newmdp, string confnewmdp, string mail, bool discut, int tel)
        {
            Pseudo = pseudo;
            Newmdp = newmdp;
            Confnewmdp = confnewmdp;
            Mail = mail;
            Discut = discut;
            Tel = tel;
        }
    }
}
