using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MainSite.Service
{
    public class BaseService
    {
        protected string ApiUrl { get; set; }
        protected string ClientKey { get; set; }
        public bool HasError { get; set; }
        public string ErrorStacktrace { get; set; }
        public string ErrorMessage { get; set; }

        protected string BuildGet(HttpClient client, string pStrApiExt, KeyValuePair<string, string>[] pKeyValuePair)
        {
            string geturl = null;
            BuildKeyHeader(client);
            string query;
            using (var content = new FormUrlEncodedContent(pKeyValuePair))
            {
                query = content.ReadAsStringAsync().Result;
            }
            geturl = ApiUrl + pStrApiExt + "?" + query;
         
            return geturl;
        }

        protected void BuildKeyHeader(HttpClient client)
        {
            var Headers = new List<KeyValuePair<string, string>>();
            Headers.Add(new KeyValuePair<string, string>("user-key", ClientKey));
            if (Headers != null) { foreach (var element in Headers) { client.DefaultRequestHeaders.Add(element.Key, element.Value); } }
        }


    }
}
