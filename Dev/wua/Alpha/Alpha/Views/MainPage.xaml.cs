using AlphaLib1.Events;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Alpha.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public ObservableCollection<Event> allEvents;

        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(FirstConnect));
            NamePageTextBlock.Text = "FirstConnect";
        }

        #region "ajout d'events"

        public ObservableCollection<Event> AllEvents
        {
            get
            {
                return allEvents ?? (allEvents = new ObservableCollection<Event>());
            }
        }

        #endregion

        #region "Barre principale"

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            NamePageTextBlock.Text = "Home";
            MyFrame.Navigate(typeof(Home));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSplitView.IsPaneOpen = !HamburgerSplitView.IsPaneOpen;
        }
        #endregion

        private void AProposListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            NamePageTextBlock.Text = "A Propos";
            MyFrame.Navigate(typeof(APropos));

        }

        private void FavoritesListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            NamePageTextBlock.Text = "Favorits";
            MyFrame.Navigate(typeof(Home));
        }

        private void EventMap_Click(object sender, RoutedEventArgs e)
        {
            NamePageTextBlock.Text = "Event Map";
            MyFrame.Navigate(typeof(EventMap));
        }

        private void ContactListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            NamePageTextBlock.Text = "Contacts";
            MyFrame.Navigate(typeof(Contacts));
        }
    }
}
