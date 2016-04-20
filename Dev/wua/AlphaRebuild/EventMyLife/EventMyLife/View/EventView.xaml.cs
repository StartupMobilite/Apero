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
    public sealed partial class EventView : Page
    {
        public Event MenuItems;
        public EventView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Event)
            {
                MenuItems = e.Parameter as Event;
                eventView.DataContext = MenuItems;
            }
        }

        private void IParticipate_Click(object sender, RoutedEventArgs e)
        {
            MenuItems.IdUser.ToString().Insert(0, string.Format("{0}/",App.MobileService.CurrentUser.UserId.ToString() ));
            var eventSending = new EventGest();
            eventSending.updateEvent(MenuItems);
            Frame.GoBack();
        }
    }
}
