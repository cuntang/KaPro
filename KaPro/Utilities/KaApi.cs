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

        public void Execute<T>(RestRequest request, Action<T> callback, bool oauth=false, IDictionary param=null ) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = Constants.BaseUrl;
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
                    callback(response.Data);
                });
        }
    }

}
