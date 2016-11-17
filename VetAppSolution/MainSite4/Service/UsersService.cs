using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vetapp.Client.ProxyCore;

namespace MainSite.Service
{
    public class UsersService : BaseService
    {
        public UsersService(string pStrApiUrl, string pStrClientKey)
        {
            ApiUrl = pStrApiUrl;
            ClientKey = pStrClientKey;  
        }

        public async Task<UserProxy> Save(UserProxy pUserProxy)
        {
            UserProxy userProxy = new UserProxy();
            try
            {
                HttpClient client = new HttpClient();
                BuildKeyHeader(client);            
                Uri requestUri = new Uri(ApiUrl + "/api/users");
                string json = JsonConvert.SerializeObject(userProxy);
                HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {

                }
                //string responJsonText = await response.Content.ReadAsStringAsync();
 
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
                string geturl = ApiUrl + "/api/users/5";

                BuildKeyHeader(client);
                HttpResponseMessage result = client.GetAsync(geturl).Result;
                if (result.IsSuccessStatusCode)
                {
                    string payload = await result.Content.ReadAsStringAsync();
                    userProxy = JsonConvert.DeserializeObject<UserProxy>(payload);
                }

                //string apiext = "/api/user";
                //KeyValuePair<string, string>[] keyValuePair = new KeyValuePair<string, string>[]{
                //    new KeyValuePair<string, string>("id", pStrAuthUserID)
                //};
                //string geturl = BuildGet(client, apiext, keyValuePair);

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
