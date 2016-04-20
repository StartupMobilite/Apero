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
    public sealed partial class MyEvents : Page
    {
        public EventGest eventman = new EventGest();

        public MyEvents()
        {
            this.InitializeComponent();
            eventman.recupEvent();
            MyeventsListView.ItemsSource = eventman.MyEvent;
        }


        private void MyeventsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var eventCliked = e.ClickedItem as Event;
            Frame.Navigate(typeof(EventView), eventCliked);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            eventman.recupMyEvent();
            MyeventsListView.ItemsSource = eventman.MyEvent;
        }
    }
}
