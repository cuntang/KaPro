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
using System.IO;
using KaPro.Utilities;
using RestSharp;
using RestSharp.Authenticators;

namespace KaPro
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        WebClient client = new WebClient();
        delegate void delegateUpdateWebBrowser(string content);

        private void updateWebBrowser(string content)
        {
           // loginPage.NavigateToString(content);
        }
        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }


        private void Login_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/PageSignin.xaml", UriKind.Relative));
        }

        private void Token_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/TokenPage.xaml", UriKind.Relative));
        }

    }
}