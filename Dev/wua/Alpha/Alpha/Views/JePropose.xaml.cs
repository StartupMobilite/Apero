using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AlphaLib1.Events;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Alpha.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class JePropose : Page
    {
        public JePropose()
        {
            this.InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void JePropose1_Click(object sender, RoutedEventArgs e)
        {
            
            var NewEvent = new Event();
            NewEvent.email = TextBoxMail.Text;
            NewEvent.titre = TextBoxTitre.Text;
            NewEvent.tel = TextBoxTel.Text;
            NewEvent.nbrPartMax = TextBoxNbrPartMax.Text;
            NewEvent.theme = TextBoxTheme.Text;
            NewEvent.time = TextBoxTime.Text;
            NewEvent.adresse = TextBoxAdress.Text;
            Frame.GoBack();
        }
    }
}
