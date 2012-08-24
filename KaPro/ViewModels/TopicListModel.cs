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
{   //topic list view model represents a level in the main tree structure with one sublevel of elements
    public class TopicListModel: INotifyPropertyChanged
    {
        // property represent main topic title displayed in page title
        private string _Topic;
        private bool _dLoaded;
        //placeholder EntryModel for displaying 'All' in the first pivotitem
        private EntryModel allTopics = new EntryModel();
        // root topic for current view (containing all subelement in the tree
        private EntryModel rootTopic = new EntryModel();
        public BackgroundWorker worker = new BackgroundWorker();

        public TopicListModel()
        {
            isDataLoaded = false;
            this.Items = new ObservableCollection<EntryModel>();
            //initial allTopics
            allTopics.Hide = false;
            allTopics.Id = "all";
            allTopics.Kind = "Topic";
            allTopics.Title = "All";
            //initalize background worker
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.LoadData("root");
        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool isDataLoaded //{ set; get; }
        {
            get { return _dLoaded; }
            set
            {
                if (value != _dLoaded)
                {
                    _dLoaded = value;
                    NotifyPropertyChanged("isDataLoaded");
                }
            }
        }

        public ObservableCollection<EntryModel> Items { get; private set; }
        // parent topic string used to get video or excercise
        public string parentTopic { get; set; }

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
            set
            {
                if (_Topic != value)
                {
                    _Topic = value;
                    NotifyPropertyChanged("Topic");
                }
            }
        }

        public void LoadData(string topic)
        {
            Topic = topic;
            if (!isDataLoaded)
            {
                KaApi apiCall = new KaApi();
                RestRequest request = new RestRequest(Constants.TopicUrl+topic+"/videos");
                //request.RootElement = "children";
                apiCall.ExecuteAsync<List<EntryModel>>(request, parseTopicTree);
            }
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
  
        // callback to invoke UI update, updating UI in UI thread by Dispatcher.BeginInvoke()
        private void parseTopic(EntryModel data)
        {
            if (data == null)
            {
            }
            else { 
            rootTopic = data;
            ((App)App.Current).RootFrame.Dispatcher.BeginInvoke(new Action<List<EntryModel>>(parseTopicView), data.Children);
            }
        }

        private void parseTopicTree(List<EntryModel> data)
        {
            if (data.Count==0)
            {
                KaApi apiCall = new KaApi();
                RestRequest request = new RestRequest(Constants.TopicUrl + _Topic);
                apiCall.ExecuteAsync<EntryModel>(request, parseTopic);
            }
            else
            {
                ((App)App.Current).RootFrame.Dispatcher.BeginInvoke(new Action<List<EntryModel>>(parseTopicView), data);
            }
        }
        private void parseTopicView(List<EntryModel> data)
        {
            if (data != null)
            {
                this.Items.Clear();
                if (this.Topic!="Main")
                    this.Items.Add(allTopics);
                foreach (EntryModel topic in data)
                {
                    if (!topic.Hide)
                    {
                        if (topic.Kind == "Topic") { this.Items.Add(topic); allTopics.subItems.Add(topic); }
                        else if (topic.Kind == "Video")
                        {
                            if (this.Items.Count == 1)
                            {
                                EntryModel tmpTopic = new EntryModel();
                                tmpTopic.Title = parentTopic.Replace('-', ' ');
                                allTopics.subItems.Add(topic);
                                //LoadVideoData(topic.Id);
                            }
                        }
                    }
                }
                isDataLoaded = true;  // this will trigger page status update logic (e.g. progressBar invisible)
            }
        }

    }
}
