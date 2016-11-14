using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vetapp.Client.ProxyCore;

namespace MainSite.Service
{
    public class UserService : BaseService
    {
        public UserService(string pStrApiUrl, string pStrClientKey)
        {
            ApiUrl = pStrApiUrl;
            ClientKey = pStrClientKey;  
        }

        public async Task<UserProxy> Save(UserProxy pUserProxy)
        {
            UserProxy userProxy = null;
            try
            {
                HttpClient client = new HttpClient();
                BuildKeyHeader(client);            
                Uri requestUri = new Uri(ApiUrl + "/api/user/save");
                string json = JsonConvert.SerializeObject(pUserProxy);
                HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
                string responJsonText = await response.Content.ReadAsStringAsync();
 
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ex.Message;
                ErrorStacktrace = ex.StackTrace;
            }
            return userProxy;
        }

        public async Task<UserProxy> Load(string pStrAuthUserID)
        {
            UserProxy userProxy = null;
            try
            {
                HttpClient client = new HttpClient();
                string apiext = "/api/user/getuser";
                KeyValuePair<string, string>[] keyValuePair = new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("userid", pStrAuthUserID)
                };
                string geturl = BuildGet(client, apiext, keyValuePair);

                var result = client.GetAsync(geturl).Result;
                if (result.IsSuccessStatusCode)
                {
                    string payload = await result.Content.ReadAsStringAsync();
                    userProxy = JsonConvert.DeserializeObject<UserProxy>(payload);
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ex.Message;
                ErrorStacktrace = ex.StackTrace;
            }
            return userProxy;
        }
    }
}
