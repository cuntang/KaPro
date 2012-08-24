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
using System.Windows.Navigation;
using System.ComponentModel;

namespace KaPro.Views
{
    public partial class TopicPivot : PhoneApplicationPage
    {   // topic of this Page
        private string topic { get; set; }
        // sub topic list 
        private TopicListModel topics = new TopicListModel();
        // Constructor
        public TopicPivot()
        {
            InitializeComponent();
            DataContext = topics;
            progressPivot.IsIndeterminate = true;
            progressPivot.Visibility = Visibility.Visible;
            topics.PropertyChanged += new PropertyChangedEventHandler(topics_PropertyChanged);
        }

        void topics_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string a = e.PropertyName;
            if (a == "isVideoLoaded")
            {
            }
            if (a == "isVideoLoaded" || a == "isDataLoaded")
                progressPivot.Visibility = Visibility.Collapsed;
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

        private void ListBox_Tapped(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            var tapped = (sender as StackPanel).DataContext as EntryModel;
            if (tapped.Kind == "Topic")
            {
                e.Handled = true;
                this.NavigationService.Navigate(new Uri("/Views/TopicPivot.xaml?topic=" + tapped.Id + "&title=" + tapped.Title, UriKind.Relative));
            }
            else if (tapped.Kind == "Video")
            {//direct play video
            }
        }

        private void ListBox_Hold(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {

        }

        private void TopicPage_loaded(object sender, RoutedEventArgs e)
        {
            if (!topics.isDataLoaded)
            {
                this.topics.LoadData(topic);
            }
    
        }

        private void PivotItem_changed(object sender, PivotItemEventArgs e)
        {   EntryModel topic = e.Item.Content as EntryModel;
            if (topic!=null&& topic.subItems.Count==0)
            {
                progressPivot.Visibility = Visibility.Visible;
                this.topics.isDataLoaded = false;
                this.topics.LoadData(topic.Id);
            }
        }
    }
}