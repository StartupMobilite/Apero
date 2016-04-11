using EventMyLife.Model;
using EventMyLife.ViewModel;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace EventMyLife.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class IPropEvent : Page
    {
        public IPropEvent()
        {
            this.InitializeComponent();
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void JePropose1_Click(object sender, RoutedEventArgs e)
        {

            var NewEvent = new Event
            {
                TitreEvent = TextBoxTitre.Text,
                NbParticipEvent = int.Parse(TextBoxNbrPartMax.Text.ToString()),
                ThemeEvent = TextBoxTheme.Text,
                AdresseEvent = TextBoxAdress.Text,
                DateEvent = CalendarStart.Date,
                DescripEvent = DescriptionTextboxTitre.Text,
                PhotoEvent = ImageTextBox.Text
            };
            var eventSending = new EventGest();
            eventSending.sendEvent(NewEvent);
            Frame.GoBack();
        }

    }
}
