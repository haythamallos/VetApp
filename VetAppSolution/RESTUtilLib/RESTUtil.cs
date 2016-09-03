using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace RESTUtilLib
{
    public class RESTUtil
    {
        public static HttpWebRequest createGetRequest(string url)
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(url);
            return request;
        }
        public static HttpWebRequest createPostRequest(string url, string postData, string method)
        {
            HttpWebRequest request = null;
            Uri uri = new Uri(url);
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = method;
            request.ContentType = "application/text";
            request.ContentLength = postData.Length;
            using (Stream writeStream = request.GetRequestStream())
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(postData);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            return request;
        }
        public static HttpWebRequest createPostRequest(string url, string postData, string method, string contenttype)
        {
            HttpWebRequest request = null;
            Uri uri = new Uri(url);
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = method;
            request.ContentType = contenttype;
            request.ContentLength = postData.Length;
            using (Stream writeStream = request.GetRequestStream())
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(postData);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            return request;
        }

        public static void EncodeBaseItems(ref StringBuilder baseRequest, string apikey, string accesskey)
        {
            EncodeAndAddItem(ref baseRequest, "apikey", apikey);
            EncodeAndAddItem(ref baseRequest, "accesstoken", accesskey);
        }

        public static void EncodeAndAddItem(ref StringBuilder paramaters, string key, string dataItem)
        {
            if (paramaters == null)
            {
                paramaters = new StringBuilder();
            }
            if (paramaters.Length != 0)
            {
                paramaters.Append("&");
            }

            paramaters.Append(key);
            paramaters.Append("=");
            paramaters.Append(HttpUtility.UrlEncode(dataItem));
        }

        public static void EncodeAndAddItem(ref StringBuilder paramaters, string key, byte[] binaryItem)
        {
            if (paramaters == null)
            {
                paramaters = new StringBuilder();
            }
            if (paramaters.Length != 0)
            {
                paramaters.Append("&");
            }

            paramaters.Append(key);
            paramaters.Append("=");
            paramaters.Append(Convert.ToBase64String(binaryItem));
        }

        public static HttpWebResponse ExecuteAction(HttpWebRequest request, ref string pStrResponsBody, ref string pStrStatusCode)
        {
            HttpWebResponse response = null;
            try
            {
                //request.KeepAlive = false;
                WebResponse ws = request.GetResponse();

                // Get the response stream
                StreamReader reader = new StreamReader(ws.GetResponseStream());

                // Read the whole contents and return as a string
                string text = reader.ReadToEnd();
                pStrResponsBody = text;

                response = (HttpWebResponse)request.GetResponse();
                pStrStatusCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                pStrResponsBody = ex.Message;
                pStrStatusCode = String.Empty;
            }
            return response;
        }

        /// <summary>
        /// Serialise an object to JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeToSerialise"></param>
        /// <returns></returns>
        public static string Serialize<T>(T typeToSerialise)
        {
            var serializer = new DataContractJsonSerializer(typeToSerialise.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, typeToSerialise);
                string retVal = Encoding.Default.GetString(ms.ToArray());
                return retVal;
            }
        }

        /// <summary>
        /// Deserialise an object fomr JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonToDeserialise"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonToDeserialise)
        {
            T typeToDeserialise = Activator.CreateInstance<T>();
            try
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonToDeserialise)))
                {
                    var serializer = new DataContractJsonSerializer(typeToDeserialise.GetType());
                    typeToDeserialise = (T)serializer.ReadObject(ms);
                }
            }
            catch { }
            return typeToDeserialise;
        }
    }
}
