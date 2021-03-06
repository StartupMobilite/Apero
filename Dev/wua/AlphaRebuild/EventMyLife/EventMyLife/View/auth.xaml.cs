﻿using EventMyLife.Model;
using EventMyLife.ViewModel;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
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
    public sealed partial class auth : Page
    {
        public auth()
        {
            this.InitializeComponent();
        }

        private async void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = false;
            try
            {
                
                // Login the user and then load data from the mobile app.
                if (await AuthenticateAsync())
                {
                    // Hide the login button and load items from the mobile app.
                    ButtonLogin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    //await InitLocalStoreAsync(); //offline sync support.
                    Frame.Navigate(typeof(MainPage));
                }
            }
            catch
            {
                
            }
            ButtonLogin.IsEnabled = true;
        }

        // Define a member variable for storing the signed-in user. 
        private MobileServiceUser user;

        // Define a method that performs the authentication process
        // using a Facebook sign-in. 
        private async System.Threading.Tasks.Task<bool> AuthenticateAsync()
        {
            string message;
            bool success = false;

            // This sample uses the Facebook provider.
            var provider = MobileServiceAuthenticationProvider.Facebook;

            // Use the PasswordVault to securely store and access credentials.
            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = null;

            try
            {
                // Try to get an existing credential from the vault.
                credential = vault.FindAllByResource(provider.ToString()).FirstOrDefault();
            }
            catch (Exception)
            {
                // When there is no matching resource an error occurs, which we ignore.
            }

            if (credential != null)
            {
                // Create a user from the stored credentials.
                user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;

                // Set the user from the stored credentials.
                App.MobileService.CurrentUser = user;

                // Consider adding a check to determine if the token is 
                // expired, as shown in this post: http://aka.ms/jww5vp.

                success = true;
                message = string.Format("Cached credentials for user - {0}", user.UserId);
            }
            else
            {
                try
                {
                    // Login with the identity provider.
                    user = await App.MobileService.LoginAsync(provider);
                    // Create and store the user credentials.
                    credential = new PasswordCredential(provider.ToString(),
                        user.UserId, user.MobileServiceAuthenticationToken);
                    vault.Add(credential);

                    success = true;
                    message = string.Format("You are now logged in - {0}", user.UserId);
                }
                catch (MobileServiceInvalidOperationException)
                {
                    message = "You must log in. Login Required";
                }
            }
            try
            {

                var unJToken = await App.MobileService.InvokeApiAsync("userInfo", HttpMethod.Get, null);
                JsonClass jparser = new JsonClass();
                App.MyProfile = jparser.userDeserialize(unJToken.ToString());
                App.MyProfile.IdProvider = App.MobileService.CurrentUser.UserId.ToString();
                var su = new UserGest();
                su.sendUserInf(App.MyProfile);
                message = string.Format("Bonjour {0} {1} !", App.MyProfile.Given_name,App.MyProfile.Family_name);
                /*
                var dlgUser = new MessageDialog(unJToken.ToString());
                dlgUser.Commands.Add(new UICommand("Fermer"));
                await dlgUser.ShowAsync();*/

            }
            catch
            {
                Debug.WriteLine("Erreur InvokeApiAsync");

            }
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
            return success;
        }

    }
}
