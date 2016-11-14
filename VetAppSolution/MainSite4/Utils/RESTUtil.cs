using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Dynamic;

namespace MainSite.Utils
{
    public class RESTUtil
    {
        public async void POSTreq(string url, ExpandoObject dynamicJson)
        {
            Uri requestUri = new Uri(url); 
            string json = "";
            json = JsonConvert.SerializeObject(dynamicJson);
            var objClint = new HttpClient();
            HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            string responJsonText = await respon.Content.ReadAsStringAsync();
        }

        public async void GetRequest(string url)
        {
            Uri geturi = new Uri(url);  
            HttpClient client = new HttpClient();
            HttpResponseMessage responseGet = await client.GetAsync(geturi);
            string response = await responseGet.Content.ReadAsStringAsync();
        }

        //    public static async  HttpClient createPostRequest(string url, object obj)
        //    {
        //        HttpClient client = new HttpClient().PostAsync(url, new JsonContent(obj));

        //        //HttpWebRequest request = null;
        //        //Uri uri = new Uri(url);
        //        //request = (HttpWebRequest)WebRequest.Create(uri);
        //        //request.Method = method;
        //        //request.ContentType = "application/json";
        //        //request.ContentLength = postData.Length;
        //        //if (Headers != null)
        //        //{
        //        //    foreach (var element in Headers)
        //        //    {
        //        //        request.Headers.Add(element.Key, element.Value);
        //        //    }
        //        //}
        //        //using (Stream writeStream = request.GetRequestStream())
        //        //{
        //        //    UTF8Encoding encoding = new UTF8Encoding();
        //        //    byte[] bytes = encoding.GetBytes(postData);
        //        //    writeStream.Write(bytes, 0, bytes.Length);
        //        //}

        //        return client;
        //    }
        //}

    }
}
