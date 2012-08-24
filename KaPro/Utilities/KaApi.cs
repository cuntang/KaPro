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
using KaPro.Utilities;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections;
using RestSharp.Deserializers;
using Newtonsoft.Json;
namespace KaPro
{
    public class KaApi
    {
        private RestClient client;
        public KaApi()
        {
            client = new RestClient();
            //enable response compression with valid user-agent
            client.AddDefaultHeader("Accept-Encoding", "gzip");
            client.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:14.0) Gecko/20100101 Firefox/14.0.1";
            client.BaseUrl = Constants.BaseUrl;
        }

        public void ExecuteAsync<T>(RestRequest request, Action<T> callback, bool oauth=false, IDictionary param=null ) where T : new()
        {

            if (oauth)
                client.Authenticator = OAuth1Authenticator.ForProtectedResource(Constants.OauthConsumerKey,
                    Constants.OauthConsumerSecret, App.UserModel.AccessToken, App.UserModel.TokenSecret);
            if (param != null)
            {
                foreach ( DictionaryEntry entry in param){
                request.AddParameter(entry.Key.ToString(), entry.Value, ParameterType.UrlSegment);
                }
                
            }

         client.ExecuteAsync<T>(request,response=>
                { 
                    //dynamic temp = JsonConvert.DeserializeObject<dynamic>(response.Content);
                    callback(response.Data);
                    
                });
        }
    }

}
