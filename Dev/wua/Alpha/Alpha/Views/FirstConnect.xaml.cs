using Facebook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace Alpha.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FirstConnect : Page
    {
        public FirstConnect()
        {
            this.InitializeComponent();
        }

        private void LogAdButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }



        private const string AppId = "740198296080898";
        private const string ExtendedPermissions ="publish_actions, user_managed_groups, user_groups";


        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await AuthenticateFacebookAsync();
            var md = new MessageDialog("you'r token" + result);
            await md.ShowAsync();
        }

        private async Task<string> AuthenticateFacebookAsync()
        {
            try
            {
                var fb = new FacebookClient();

                var redirectUri =
                  WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();

                var loginUri = fb.GetLoginUrl(new
                {
                    client_id = AppId,
                    redirect_uri = redirectUri,
                    scope = ExtendedPermissions,
                    display = "popup",
                    response_type = "f2dd67015d5091b36b927bcd01831513"
                });

                var callbackUri = new Uri(redirectUri, UriKind.Absolute);

                var authenticationResult =
                  await
                    WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.None,
                    loginUri, callbackUri);

                return ParseAuthenticationResult(fb, authenticationResult);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ParseAuthenticationResult(FacebookClient fb,WebAuthenticationResult result)
        {
            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.ErrorHttp:
                    return "Error";
                case WebAuthenticationStatus.Success:

                    var oAuthResult = fb.ParseOAuthCallbackUrl(new Uri(result.ResponseData));
                    return oAuthResult.AccessToken;
                case WebAuthenticationStatus.UserCancel:
                    return "Operation aborted";
            }
            return null;
        }

    }
}
