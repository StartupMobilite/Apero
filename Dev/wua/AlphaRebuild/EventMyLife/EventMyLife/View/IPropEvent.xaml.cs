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
using Windows.UI;
using Windows.UI.Popups;
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
                return new List<string>() {"Autre", "Sport", "Jeux Video", "Cinema", "Bar", "Nature" };
            }
        }

        public IPropEvent()
        {
            this.InitializeComponent();
            ThemeCombo.ItemsSource = themesList;
            ThemeCombo.SelectedIndex = 0;
            CalendarStart.MinDate = System.DateTime.Now;
        }

        #region Boutons
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void JePropose1_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            var dialog = new MessageDialog("");
            dialog.Title = "Proposition d'évenement :";
            dialog.Commands.Add(new UICommand("OK"));
            var eventSending = new EventGest();
            if (await verifForm())
            {
                var NewEvent = new Event();

                NewEvent.TitreEvent = TextBoxTitre.Text;
                NewEvent.NbParticipEvent = int.Parse(TextBoxNbrPartMax.Text);
                NewEvent.ThemeEvent = ThemeCombo.SelectedItem.ToString();
                NewEvent.AdresseEvent = adressSugest.Text;
                NewEvent.DateEvent = CalendarStart.Date.ToString();
                NewEvent.DescripEvent = DescriptionTextboxTitre.Text;
                NewEvent.PhotoEvent = ImageTextBox.Text;
                NewEvent.IdUser = App.MobileService.CurrentUser.UserId.ToString();
                NewEvent.TimeEvent = TimePicker.Time.Hours.ToString();
                NewEvent.Duration = DurationPicker.Time.ToString();
                eventSending.sendEvent(NewEvent);
                Frame.GoBack();
                message = "Envoyé avec succé :D";
            }
            else
            {
                message = "Champs invalide :(";
            }
            dialog.Content = message;
            await dialog.ShowAsync();
        }
        #endregion

        public async Task<bool> verifForm()
        {
            bool success = true;
            int tmp=0;
            if (!(TextBoxTitre.Text.Length > 1))
            {
                TextBoxTitre.Background = new SolidColorBrush(Colors.IndianRed);
                success = false;
            }
            else
            {
                TextBoxTitre.Background = new SolidColorBrush(Colors.Transparent);
            }
            if (!(TextBoxNbrPartMax.Text.Length >= 1))
            {
                TextBoxNbrPartMax.Background = new SolidColorBrush(Colors.IndianRed);
                success = false;
            }
            else
            {
                if(!(int.TryParse(TextBoxNbrPartMax.Text,out tmp)))
                {
                    success = false;
                    TextBoxNbrPartMax.Background = new SolidColorBrush(Colors.IndianRed);
                }
                else
                {
                    TextBoxNbrPartMax.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            if (!(adressSugest.Text.Length > 5))
            {
                adressSugest.Background = new SolidColorBrush(Colors.IndianRed);
                success = false;
            }
            else
            {
                if (await verifaddress(adressSugest.Text) == false)
                {
                    success = false;
                }
                else
                {
                    adressSugest.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
            if(!(CalendarStart.Date != null))
            {
                CalendarStart.Background = new SolidColorBrush(Colors.IndianRed);
                success = false;
            }
            else
            {
                CalendarStart.Background = new SolidColorBrush(Colors.Transparent);
            }
            if(!(TimePicker.Time != null))
            {
                TimePicker.Background = new SolidColorBrush(Colors.IndianRed);
                success = false;

            }
            else
            {
                TimePicker.Background = new SolidColorBrush(Colors.Transparent);
            }
            if(!(DurationPicker.Time != null))
            {
                DurationPicker.Background = new SolidColorBrush(Colors.IndianRed);
                success = false;
            }
            else
            {
                DurationPicker.Background = new SolidColorBrush(Colors.Transparent);
            }
            return success;
        }

        #region findAddress

        //Verifie Si  l'adresse est valide
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

        //recupere une liste d'adresse proche pour la SuggestBox
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

        //verifie si le text de la suggest box a changer et lance la recherche de propositions
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

        //que faire quand une adresse est choisie
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

        #endregion

    }
}
