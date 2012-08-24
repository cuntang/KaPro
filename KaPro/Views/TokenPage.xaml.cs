using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using KaPro.Views;
using RestSharp;
using KaPro.Utilities;

namespace KaPro.Views
{
    public class User
    {
        public string nickname {get;set;}
        public string email {get;set;}
        public int points { get; set; }
    }
    public partial class TokenPage : PhoneApplicationPage
    {
        public TokenPage()
        {
            InitializeComponent();
            ContentPanel.DataContext =App.UserModel;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            App.UserModel.clearUser();
        }

        private void TokenPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.UserModel.UserName == ""&&App.UserModel.AccessToken!="N/A")
            {
            tokenProgress.IsIndeterminate = true;
            tokenProgress.Visibility = Visibility.Visible;
            KaApi apiCall = new KaApi();
            RestRequest request = new RestRequest(Constants.UserDetails);
            apiCall.ExecuteAsync<User>(request, parseUser,true);
            }

        }
        private void parseUser(User data)
        {
            App.UserModel.UserName = data.nickname;
            App.UserModel.Email = data.email;
            App.UserModel.Other = data.points.ToString();
            tokenProgress.Visibility = Visibility.Collapsed;
        }
    }
}