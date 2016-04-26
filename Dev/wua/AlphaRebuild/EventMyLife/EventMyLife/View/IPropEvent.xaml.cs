using EventMyLife.Model;
using EventMyLife.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
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
        public List<string> themesList
        {
            get
            {
                return new List<string>() { "Sport", "Jeux Video", "Cinema", "Bar" };
            }
        }

        public IPropEvent()
        {
            this.InitializeComponent();
            ThemeCombo.ItemsSource = themesList;
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        public bool verifForm()
        {
            bool success = true;
            if (!(TextBoxTitre.Text.Length > 1))
            {
                success = false;
            }
            if (!(TextBoxNbrPartMax.Text.Length > 0))
            {
                success = false;
            }
            else
            {
                if (int.Parse(TextBoxNbrPartMax.Text.ToString()) < 2)
                {
                    success = false;
                }
            }
            if (!(ThemeCombo.SelectedItem.ToString() ==""))
            {
                success = false;
            }
            if (!(TextBoxAdress.Text.Length > 5))
            {
                success = false;
            }
            else
            {
                if(verifaddress(TextBoxAdress.Text).Result == false)
                {
                    success = false;
                }
            }

            return success;
        }
        public async Task<bool> verifaddress(string address)
        {
            Geoloc gs = new Geoloc();
            MapLocationFinderResult listAdress = await gs.geocode(address,App.gs.myLocation);
            if (listAdress.Locations.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public  async Task<List<string>> addressList(string address)
        {
            List<string> addressListR = new List<string>();
            try
            {
                MapLocationFinderResult listAdress = await App.gs.geocode(address, App.gs.myLocation);
                string tmp = "";
                foreach (var item in listAdress.Locations.ToList())
                {
                    try
                    {
                        Debug.WriteLine(string.Format("element :", item.DisplayName.ToString()));

                        tmp = string.Format("{0} {1} {2} {3}", item.Address.StreetNumber.ToString(),item.Address.Street, item.Address.PostCode.ToString(), item.Address.Town.ToString());
                        Debug.WriteLine(tmp);
                        addressListR.Add(tmp);
                    }
                    catch
                    {
                        Debug.WriteLine(tmp);
                    }
                }
            }
            catch
            {
                Debug.WriteLine("erreur addresslist");
            }

            return addressListR;
        }



        private void JePropose1_Click(object sender, RoutedEventArgs e)
        {

            var NewEvent = new Event();

                    NewEvent.TitreEvent = TextBoxTitre.Text;
                    NewEvent.NbParticipEvent = int.Parse(TextBoxNbrPartMax.Text.ToString());
                    NewEvent.ThemeEvent = ThemeCombo.SelectedItem.ToString();
                    NewEvent.AdresseEvent = TextBoxAdress.Text;
                    NewEvent.DateEvent = string.Format("{0}/{1}/{2}",
                CalendarStart.Date.Day.ToString(),
                CalendarStart.Date.Month.ToString(),
                CalendarStart.Date.Year.ToString());
                    NewEvent.DescripEvent = DescriptionTextboxTitre.Text;
                    NewEvent.PhotoEvent = ImageTextBox.Text;
                    NewEvent.IdUser = App.MobileService.CurrentUser.UserId.ToString();
            var eventSending = new EventGest();
            eventSending.sendEvent(NewEvent);
            Frame.GoBack();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            List<string> list = new List<string>();
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                try {
                    Debug.WriteLine("debut recup list :");
                    Debug.WriteLine(adressSugest.Text);
                    list =  await addressList(sender.Text);
                    Debug.WriteLine("reussi");
                    sender.ItemsSource = list;
                }
                catch
                {
                    Debug.WriteLine(list);

                    Debug.WriteLine("Fail recuplist");
                }

                //sender.ItemsSource = dataset;
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                adressSugest.Text = args.QueryText;
            }
            else
            {
                // Use args.QueryText to determine what to do.
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }
    }
}
