using EventMyLife.Model;
using EventMyLife.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            var EventsList = new ObservableCollection<Event>();
            //Remplir eventsList avec la base de données par rapport a la pos exemple cree une fonction connexion recuperation
            EventsList.Add(new Event("toto", "Event1", "Theme1",(new DateTime(2004,10,13,16,53,24)),12,(new DateTime(2004,10,14)),"rue de toto","bonjour description 1"));
            EventsList.Add(new Event("toto", "Event2", "Theme2", (new DateTime(2004, 10, 13, 16, 53, 24)), 12, (new DateTime(2004, 10, 14)), "rue de toto", "bonjour description 1"));
            EventsList.Add(new Event("toto", "Event3", "Theme3", (new DateTime(2004, 10, 13, 16, 53, 24)), 12, (new DateTime(2004, 10, 14)), "rue de toto", "bonjour description 1"));
            eventsListView.ItemsSource = EventsList;
        }

        private void IGiveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void eventsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var eventCliked = e.ClickedItem as Event;
                Frame.Navigate(typeof(EventView),eventCliked);
        }
    }
}
