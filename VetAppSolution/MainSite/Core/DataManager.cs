using RESTUtilLib;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using Vetapp.Client.Proxy;

namespace MainSite.Core
{
    public class DataManager
    {
        private ClaimsIdentity _claimsIdentity = null;
        private UserProxy _userProxy = null;
        private string _apiurl = null;
        private string _apikey = null;

        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        public bool HasError
        {
            get { return _hasError; }
        }
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public DataManager()
        {
            init();
        }
        public DataManager(ClaimsIdentity claimsIdentity)
        {
            _claimsIdentity = claimsIdentity;
            _userProxy = getUserProxy();
            init();
        }
        private void init()
        {
            _apiurl = System.Configuration.ConfigurationManager.AppSettings["Apiurl"];
            _apikey = System.Configuration.ConfigurationManager.AppSettings["Apikey"];
        }
        private UserProxy getUserProxy()
        {
            UserProxy userProxy = new UserProxy();
            foreach (var claim in _claimsIdentity.Claims)
            {
                switch (claim.Type)
                {
                    case "access_token":
                        userProxy.AuthAccessToken = claim.Value;
                        break;
                    case "id_token":
                        userProxy.AuthIdToken = claim.Value;
                        break;
                    case "user_id":
                        userProxy.AuthUserid = claim.Value;
                        break;
                    case "name":
                        userProxy.AuthName = claim.Value;
                        break;
                    case "email":
                        userProxy.EmailAddress = claim.Value;
                        break;
                    case "nickname":
                        userProxy.AuthNickname = claim.Value;
                        break;
                    case "connection":
                        userProxy.AuthConnection = claim.Value;
                        break;
                    case "picture":
                        userProxy.Profileimageurl = claim.Value;
                        break;
                    case "provider":
                        userProxy.AuthProvider = claim.Value;
                        break;
                    default:
                        break;
                }
            }
            return userProxy;
        }

        public void SaveUserIfNotExist()
        {
            StringBuilder parameters = new StringBuilder();
            string url = _apiurl + "/api/user";

            string jsonBody = ToJson(_userProxy, _userProxy.GetType());
            var Headers = new List<KeyValuePair<string, string>>();
            Headers.Add(new KeyValuePair<string, string>("user-key", _apikey));
            HttpWebRequest request = RESTUtil.createPostRequest(url, jsonBody, "POST", Headers);

            string responseBody = null;
            string responseStatusCode = null;
            HttpWebResponse response = RESTUtil.ExecuteAction(request, ref responseBody, ref responseStatusCode);
            if (responseStatusCode != "OK")
            {
                _hasError = true;
            }

        }

        public string ToJson(object Obj, Type ObjType)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Obj);
        }
    }
}