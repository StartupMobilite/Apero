using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.ViewModel
{
    public class ContactClass
    {
        public string Photocontact { get; set; }
        public string Nomcontact { get; set; }
        public string Statuperso { get; set; }
        public int Nbbadges  { get; set; }
        public int Nbpropo { get; set; }
        public int Nbparti { get; set; }
        public int Nbcontact { get; set;  }

        public ContactClass(string photocontact, string nomcontact, string statuperso, int nbbadges, int nbpropo, int nbparti, int nbcontact)
        {
            Photocontact = photocontact;
            Nomcontact = nomcontact;
            Statuperso = statuperso;
            Nbbadges = nbbadges;
            Nbpropo = nbpropo;
            Nbparti = nbparti;
            Nbcontact = nbcontact;
        }
    }
}
