using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EventMyLife.ViewModel
{
    public class ProfilClass
    {
        public string Photoprofil { get; set; }
        public string Nomprofil { get; set; }
        public string Statuperso { get; set; }
        public int Nbbadges { get; set; }
        public int Nbpropo { get; set; }
        public int Nbparti { get; set; }
        public int Nbcontact { get; set; }

        public ProfilClass(string photoprofil, string nomprofil, string statuperso, int nbbadges, int nbpropo, int nbparti, int nbcontact)
        {
            Photoprofil = photoprofil;
            Nomprofil = nomprofil;
            Statuperso = statuperso;
            Nbbadges = nbbadges;
            Nbpropo = nbpropo;
            Nbparti = nbparti;
            Nbcontact = nbcontact;
        }
    }
}
