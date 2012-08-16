using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
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
using System.ComponentModel;
using Microsoft.Phone.Tasks;

namespace KaPro
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string topic { get; set; }
        // Constructor
        private TopicListModel topics = new TopicListModel();
        public MainPage()
        {

            InitializeComponent();
            DataContext = topics;
            // Set the data context of the listbox control to the sample data
            topic = "root";
            topics.Topic = "root";
            topics.parentTopic = "";
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            topics.PropertyChanged+=new PropertyChangedEventHandler(topics_PropertyChanged);
            progressMain.IsIndeterminate = true;
            progressMain.Visibility = Visibility.Visible;
        }

        void topics_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string a = e.PropertyName;
            if (a == "isVideoLoaded")
            {
                if (FirstListBox.Items.Count == 1)
                {
                    foreach (PivotItem i in MainPivot.Items.Cast<PivotItem>())
                    {
                        string ab = i.Name;
                    }
                    PivotItem pivotItemToShow = MainPivot.Items.Cast<PivotItem>().Single(i => i.Name == "Videos");

                    MainPivot.SelectedItem = pivotItemToShow;
                }
            }
            if (a == "isVideoLoaded" || a == "isDataLoaded")
                progressMain.Visibility = Visibility.Collapsed;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!topics.isDataLoaded)
            {
                this.topics.LoadData(topic);
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

        private void ListBox_Tapped(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            var tapped = (sender as StackPanel).DataContext as TopicModel;
            if (tapped.Kind=="Topic")
            {
                e.Handled = true;
                this.NavigationService.Navigate(new Uri("/MainPage.xaml?topic=" + tapped.Id+"&title="+tapped.Title, UriKind.Relative));
            }
            
            else if (tapped.Kind == "Video")
            {//direct play video
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.topicList.Push(topics);
            base.OnNavigatedFrom(e);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.Keys.Count > 0)
            {
                topic = NavigationContext.QueryString["topic"];
                this.topics.Topic = NavigationContext.QueryString["title"];
                this.topics.parentTopic = topic;
            }
            if (e.NavigationMode == NavigationMode.Back)
                topics = App.topicList.Pop();
            base.OnNavigatedTo(e);
        }

        private void VideoListBox_Tapped(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            var tapped = (sender as Grid).DataContext as VideoModel;

            MediaPlayerLauncher objMediaPlayerLauncher = new MediaPlayerLauncher();
            if (tapped.Download_urls != null)
            {
                objMediaPlayerLauncher.Media = new Uri(tapped.Download_urls["mp4"], UriKind.Absolute);
                objMediaPlayerLauncher.Location = MediaLocationType.Data;
                objMediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop | MediaPlaybackControls.All;
                objMediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;
                objMediaPlayerLauncher.Show();
            }
            else {
                WebBrowserTask wb = new WebBrowserTask();
                wb.Uri = new Uri(tapped.Url);
                wb.Show();
            }
        }

        private void ListBox_Hold(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            var tapped = (sender as Grid).DataContext as TopicModel;
        } 
    }
}