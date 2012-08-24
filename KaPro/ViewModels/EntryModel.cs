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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace KaPro
{
    public class EntryModel : INotifyPropertyChanged
    {
        public EntryModel(){
        this.children = new List<EntryModel>();
        this.subItems = new ObservableCollection<EntryModel>();
    }
        private string _id;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("id");
                }
            }
        }

        private string _title;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _url;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (value != _url)
                {
                    _url = value;
                    NotifyPropertyChanged("Url");
                }
            }
        }
        private bool _hide;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public bool Hide
        {
            get
            {
                return _hide;
            }
            set
            {
                if (value != _hide)
                {
                    _hide = value;
                    NotifyPropertyChanged("Hide");
                }
            }
        }

        private string _kind;
        public string Kind
        {
            get
            {
                return _kind;
            }
            set
            {
                if (value != _kind)
                {
                    _kind = value;
                    NotifyPropertyChanged("Kind");
                }
            }
        }

        /// <summary>
        /// The backing field for DownloadUrl.
        /// </summary>
        private Dictionary<string,string> downloadUrl;
        /// <summary>
        /// Gets or sets the DownloadUrl.
        /// </summary>
        /// <value>The Download.</value>
        public Dictionary<string,string> Download_Urls
        {
            get
            {
                return this.downloadUrl;
            }

            set
            {
                if (value != this.downloadUrl)
                {
                    this.downloadUrl = value;
                    Thumbnail = String.Format("http://proxy.boxresizer.com/convert?resize=200x200&source={0}", HttpUtility.UrlEncode(downloadUrl["png"]));

                }
            }
        }
        //Video Entry thumbmail, updated by Download_Urls["png"]
        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                if (value != thumbnail)
                    thumbnail = value;
                NotifyPropertyChanged("Thumbnail");
            }
        }
        /// <summary>
        /// The backing field for Description.
        /// </summary>
        private string description;

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (value != this.description)
                {
                    this.description = value;

                    this.NotifyPropertyChanged("Description");
                }
            }
        }
        /// <summary>
        /// The backing field for Video Duration.
        /// </summary>
        private int duration;

        /// <summary>
        /// Gets or sets the Video Duration.
        /// </summary>
        /// <value>The Video Duration.</value>
        public int Duration
        {
            get
            {
                return this.duration;
            }

            set
            {
                if (value != this.duration)
                {
                    this.duration = value;

                    this.NotifyPropertyChanged("Duration");
                }
            }
        }

        /// <summary>
        /// The backing field for Video ID.
        /// </summary>
        private string readable_id;

        /// <summary>
        /// Gets or sets the Video ID.
        /// </summary>
        /// <value>The Video ID.</value>
        public string Readable_id
        {
            get
            {
                return this.readable_id;
            }

            set
            {
                if (value != this.readable_id)
                {
                    this.readable_id = value;

                    this.NotifyPropertyChanged("Readable_id");
                }
            }
        }
        /// <summary>
        /// The backing field for exercise related video id.
        /// </summary>
        private List<string> related_video_readable_ids;

        /// <summary>
        /// Gets or sets the exercise related video id.
        /// </summary>
        /// <value>The exercise related video id.</value>
        public List<string> Related_video_readable_ids
        {
            get
            {
                return this.related_video_readable_ids;
            }

            set
            {
                if (value != this.related_video_readable_ids)
                {
                    this.related_video_readable_ids = value;

                    this.NotifyPropertyChanged("Related_video_readable_ids");
                }
            }
        }
        /// <summary>
        /// The backing field for exercise ka url.
        /// </summary>
        private string ka_url;

        /// <summary>
        /// Gets or sets the exercise ka url.
        /// </summary>
        /// <value>The exercise ka url.</value>
        public string Ka_url
        {
            get
            {
                return this.ka_url;
            }

            set
            {
                if (value != this.ka_url)
                {
                    this.ka_url = value;

                    this.NotifyPropertyChanged("Ka_url");
                }
            }
        }
        /// <summary>
        /// The backing field for exercise live flag.
        /// </summary>
        private bool live;

        /// <summary>
        /// Gets or sets the exercise live flag.
        /// </summary>
        /// <value>The exercise live flag.</value>
        public bool Live
        {
            get
            {
                return this.live;
            }

            set
            {
                if (value != this.live)
                {
                    this.live = value;
                }
            }
        }
        /// <summary>
        /// The backing field for exercise display name.
        /// </summary>
        private string display_name;

        /// <summary>
        /// Gets or sets the exercise display name.
        /// </summary>
        /// <value>The exercise display name.</value>
        public string Display_name
        {
            get
            {
                return this.display_name;
            }

            set
            {
                if (value != this.display_name)
                {
                    this.display_name = value;

                    this.NotifyPropertyChanged("Display_name");
                }
            }
        }

        /// <summary>
        /// The backing field for exercise prerequisite exercises.
        /// </summary>
        private List<string> prerequisites;

        /// <summary>
        /// Gets or sets the exercise prerequisite exercises.
        /// </summary>
        /// <value>The exercise prerequisite exercises.</value>
        public List<string> Prerequisites
        {
            get
            {
                return this.prerequisites;
            }

            set
            {
                if (value != this.prerequisites)
                {
                    this.prerequisites = value;

                    this.NotifyPropertyChanged("Prerequisites");
                }
            }
        }
        public ObservableCollection<EntryModel> subItems { get; private set; }
        /// <summary>
        /// The backing field for children entryModel.
        /// </summary>
        private List<EntryModel> children;

        /// <summary>
        /// Gets or sets the children entryModel.
        /// </summary>
        /// <value>The children entryModel.</value>
        
        public List<EntryModel> Children
        {
            get
            {
                return this.children;
            }

            set
            {
                if (value != this.children)
                {
                    this.children = value;
                    this.NotifyPropertyChanged("Children");
                    subItems.Clear();
                    foreach (EntryModel entry in children)
                    {
                        subItems.Add(entry);
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
