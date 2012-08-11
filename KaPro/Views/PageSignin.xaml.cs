using System;
using System.Net;
using System.Windows;
using KaPro.Utilities;
using Microsoft.Phone.Controls;
using RestSharp;
using RestSharp.Authenticators;

namespace KaPro.Views
{
    public partial class PageSignin : PhoneApplicationPage
    {
       
        public PageSignin()
        {
            InitializeComponent();
        }

        private void StartLogin()
        {
           /* OAuthBase oauth = new OAuthBase();
            String nonce = oauth.GenerateNonce();
            String Timestamp = oauth.GenerateTimeStamp();
            String normUrl, normParameters;
            Uri baseUrl = new Uri(Constants.BaseUrl + Constants.OauthRequestToken);
            String signature = oauth.GenerateSignature(baseUrl,
                Constants.OauthConsumerKey, Constants.OauthConsumerSecret, null, null, "GET", Timestamp, nonce,
                out normUrl, out normParameters);
            Uri fullUri = new Uri(normUrl + "?"+normParameters + "&oauth_signature=" + signature); 
            LoginPage.Navigate(fullUri);
            //HttpWebRequest httpReq = (HttpWebRequest)HttpWebRequest.Create(fullUri);
            //httpReq.BeginGetResponse(HTTPWebRequestCallBack, httpReq); */
            var client = new RestClient(Constants.BaseUrl);
            client.Authenticator = OAuth1Authenticator.ForRequestToken(Constants.OauthConsumerKey, Constants.OauthConsumerSecret);
            var request = new RestRequest(Constants.OauthRequestToken);
            client.ExecuteAsync(request, response =>
            {   //hacking returned html for zooming and relative url to absolute
                string html = response.Content;
                string hackstring = "<meta name=\"viewport\" content=\"width=480,user-scalable=yes\" />";
                html = html.Insert(html.IndexOf("<head>", 0) + 6, hackstring);
                html=html.Replace("href=\"/api",String.Format("href=\"{0}api",Constants.BaseUrl));
                html = html.Replace("src=\"/images", String.Format("src=\"{0}images", Constants.BaseUrl));
                LoginPage.Visibility = Visibility.Visible;
                LoginPage.NavigateToString(html);
                SigninProgress.Visibility = Visibility.Collapsed;
            });
        }

        private static string GetQueryParameter(string input, string parameterName)
        {
            foreach (string item in input.Split('&'))
            {
                var parts = item.Split('=');
                if (parts[0] == parameterName)
                {
                    return parts[1];
                }
            }
            return String.Empty;
        }


        private void GetAccessToken(string uri)
        {
            var requestToken = GetQueryParameter(uri, "oauth_token");
            var requestTokenSecret = GetQueryParameter(uri, "oauth_token_secret");
            var client = new RestClient(Constants.BaseUrl);
            client.Authenticator = OAuth1Authenticator.ForAccessToken(Constants.OauthConsumerKey, Constants.OauthConsumerSecret, requestToken, requestTokenSecret);
            var request = new RestRequest(Constants.OauthAccessToken);
            client.ExecuteAsync(request, response =>
            {
                App.UserModel.AccessToken = GetQueryParameter(response.Content, "oauth_token");
                App.UserModel.TokenSecret = GetQueryParameter(response.Content, "oauth_token_secret");
            });
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            SigninProgress.IsIndeterminate = true;
            SigninProgress.Visibility = Visibility.Visible;
            LoginPage.Visibility = Visibility.Collapsed;
            StartLogin();
        }

        // Catch WebBrowser navigating event to default_callback? which includes request token and secret
        private void LoginPage_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.AbsoluteUri.Contains("default_callback?"))
            {
                var arguments = e.Uri.AbsoluteUri.Split('?');
                if (arguments.Length < 1)
                    return;
                GetAccessToken(arguments[1]);
                this.NavigationService.GoBack();
            }
        }
    }
}