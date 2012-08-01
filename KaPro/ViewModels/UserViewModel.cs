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

namespace KaPro
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _accessToken;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                if (value != _accessToken)
                {
                    _accessToken = value;
                    NotifyPropertyChanged("AccessToken");
                }
            }
        }

        private string _tokenSecret;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string TokenSecret
        {
            get
            {
                return _tokenSecret;
            }
            set
            {
                if (value != _tokenSecret)
                {
                    _tokenSecret = value;
                    NotifyPropertyChanged("TokenSecret");
                }
            }
        }

        private string _userName;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    NotifyPropertyChanged("UserName");
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