using EventMyLife;
using EventMyLife.Model;
using EventMyLife.View;
using EventMyLife.ViewModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EventMyLife.ViewModel
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ViewModels = new MainPageViewModel();
            var MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(new MenuItem("Accueil", Symbol.Home, "HomePage"));

            MenuItems.Add(new MenuItem("Carte Des Evenements", Symbol.Map, "EventMap"));

            MenuItems.Add(new MenuItem("Contacts", Symbol.Contact, "ContactsPage"));

            MenuItems.Add(new MenuItem("Favorits", Symbol.Favorite, "FavoritsPage"));

            MenuItems.Add(new MenuItem("A Propos", Symbol.Document, "AboutPage"));

            MenuItems.Add(new MenuItem("Logout", Symbol.DisconnectDrive, "Logout"));

            MenuItems.Add(new MenuItem("Mes Events", Symbol.GoToToday, "MesEvents"));

            menusListView.ItemsSource = MenuItems;

            MyFrame.Navigated += (s, e) =>
            {
                if (MyFrame.CanGoBack)
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                }
                else
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                }
                if(e.NavigationMode == NavigationMode.Back)
                {
                    string pageName = e.SourcePageType.Name;
                    foreach(var item in menusListView.Items)
                    {
                        var menu = item as MenuItem;
                        if(menu.Id == pageName)
                        {
                            menusListView.SelectedItem = item;
                            titleText.Text = menu.Title;
                            return;
                        }
                    }
                }
            };

            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (MyFrame.CanGoBack)
                {
                    MyFrame.GoBack();
                    a.Handled = true;
                }
            };
        
    }


        public async void senditem(TodoItem toItem)
        {
            await App.MobileService.GetTable<TodoItem>().InsertAsync(toItem);
        }

        public MainPageViewModel ViewModels { get; set; }

        private void splitViewButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menu = e.ClickedItem as MenuItem;
            if (menu.Id == "HomePage")
            {
                MyFrame.Navigate(typeof(HomePage));
            }
            if (menu.Id == "EventMap")
            {
                MyFrame.Navigate(typeof(EventMap));
            }
            if (menu.Id == "ContactsPage")
            {
                MyFrame.Navigate(typeof(Contacts));
            }
            if (menu.Id == "FavoritsPage")
            {
                MyFrame.Navigate(typeof(EventMap));
            }
            if (menu.Id == "AboutPage")
            {
                MyFrame.Navigate(typeof(About));
            }
            if(menu.Id=="Logout")
            {
                LogoutFunct();
            }
            if(menu.Id== "MesEvents")
            {
                MyFrame.Navigate(typeof(MyEvents));
            }
            titleText.Text = menu.Title;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MyFrame.Navigate(typeof(auth));
            menusListView.SelectedIndex = 0;
            titleText.Text = "Authentification";
        }

        public void setTitleName(string pageName)
        {
            titleText.Text = pageName;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LogoutFunct();
        }

        private async void LogoutFunct()
        {
            try
            {
                await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Facebook);
            }
            catch
            {

            }
        }
    }
}
