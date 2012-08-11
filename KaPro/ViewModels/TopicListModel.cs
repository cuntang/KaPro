using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using RestSharp;
using KaPro.Utilities;
using System.Collections.Generic;
using System.ComponentModel;

namespace KaPro
{
    public class TopicListModel: INotifyPropertyChanged
    {
        public ObservableCollection<TopicModel> Items { get; private set; }
        public ObservableCollection<VideoModel> VideoItems { get; private set; }
       // parent topic string used to get video or excercise
        public string parentTopic { get; set; }
        // property represent main topic title displayed in page title
        private string _Topic;
        public string Topic
        {
            get
            {
                if (_Topic != "root") { return _Topic; }
                else
                {
                    return "Main";
                }
            }
            set { if (_Topic!=value){
                _Topic = value;
                NotifyPropertyChanged("Topic");
            }}
        }
        public TopicListModel()
        {
            isDataLoaded = false;
            isVideoLoaded = false;
            this.Items = new ObservableCollection<TopicModel>();
            this.VideoItems = new ObservableCollection<VideoModel>();
        }
        private bool _dLoaded;
        public bool isDataLoaded //{ set; get; }
        { get { return _dLoaded; }
            set {
                if (value != _dLoaded)
                {
                    _dLoaded = value;
                    NotifyPropertyChanged("isDataLoaded");
                }
            }
        }
        private bool _vloaded;
        public bool isVideoLoaded { get{ return _vloaded;} set
        {
            if (value != _vloaded)
            {
                _vloaded = value;
                NotifyPropertyChanged("isVideoLoaded");
            }
        } }

        public void LoadData(string topic)
        {
            if (!isDataLoaded)
            {
                KaApi apiCall = new KaApi();
                RestRequest request = new RestRequest(Constants.TopicUrl+topic);
                request.RootElement = "children";
                apiCall.Execute<List<TopicModel>>(request, parseTopic);
                isDataLoaded = true;
            }
        }

        public void LoadVideoData(string topic)
        {
            if (!isVideoLoaded)
            {
                KaApi apiCall = new KaApi();
                RestRequest request = new RestRequest(Constants.TopicUrl + parentTopic+"/videos");
                apiCall.Execute<List<VideoModel>>(request, parseVideo);
            }
        }
        private void parseVideo(List<VideoModel> data)
        {
            if (data != null)
            {
                this.VideoItems.Clear();
                foreach (VideoModel video in data)
                {
                    this.VideoItems.Add(video);
                }
                isVideoLoaded = true;
            }
        }
        private void parseTopic(List<TopicModel> data)
        {
            if (data != null) {
                this.Items.Clear();
                this.VideoItems.Clear();
                foreach (TopicModel topic in data)
                {
                    if (!topic.Hide)
                    {
                        if (topic.Kind == "Topic") { this.Items.Add(topic); }
                        else if (topic.Kind == "Video") {
                            if (this.Items.Count == 0)
                            {
                                TopicModel tmpTopic = new TopicModel();
                                tmpTopic.Title = parentTopic.Replace('-',' ');
                                this.Items.Add(tmpTopic);
                                LoadVideoData(topic.Id);
                            }


                        }
                    }
                        
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
