using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Facebook;

namespace AlphaLib1.Web
{
    class ConnectFb
    {
        public sealed partial class MainPage : Page
        {
            private const string AppId = "740198296080898";
            private const string ExtendedPermissions =
              "publish_actions, user_managed_groups, user_groups";


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
                        response_type = "ab7598c897a14500ef12f009de20a771"
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

            public string ParseAuthenticationResult(FacebookClient fb,
                                                    WebAuthenticationResult result)
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
}
