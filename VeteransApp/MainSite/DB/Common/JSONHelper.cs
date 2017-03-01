using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Vetapp.Engine.Common
{
    public class JSONHelper
    {
        /// <summary>
        /// Strip leading and trailing \" from a JSON string - typically for scalar responses from facebook API calls (e.g. auth.getSession)
        /// </summary>
        /// <param name="jsonIn"></param>
        /// <returns></returns>
        public static string StripSingleResult(string jsonIn)
        {
            if (jsonIn.StartsWith("\"") && jsonIn.EndsWith("\""))
                return (jsonIn.Replace("\"", ""));
            else
                throw new Exception("Error, string not in correct format for StripSingleResult: " + jsonIn);
        }

        /// <summary>
        /// Strip leading and trailing [,] from a JSON string - typically for complex responses from facebook API calls (e.g. users.getInfo)
        /// </summary>
        /// <param name="jsonIn"></param>
        /// <returns></returns>
        public static string StripSquareBrackets(string jsonIn)
        {
            if (jsonIn.StartsWith("[") && jsonIn.EndsWith("]"))
                return (jsonIn.Substring(1, jsonIn.Length - 2));
            else
                throw new Exception("Error, string not in correct format for StripSquareBrackets: " + jsonIn);
        }

        /// <summary>
        /// Strip leading and trailing [,] from a JSON string and split and return the array of elements
        /// </summary>
        /// <param name="jsonIn"></param>
        /// <returns></returns>
        public static ArrayList ArrayFromJSON(string jsonIn)
        {
            if (jsonIn.StartsWith("[") && jsonIn.EndsWith("]"))
                jsonIn = jsonIn.Substring(1, jsonIn.Length - 2);

            var resultList = new ArrayList();
            resultList.AddRange(jsonIn.Split(','));
            return (resultList);
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
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonToDeserialise)))
            {
                T typeToDeserialise = Activator.CreateInstance<T>();
                var serializer = new DataContractJsonSerializer(typeToDeserialise.GetType());
                typeToDeserialise = (T)serializer.ReadObject(ms);
                return typeToDeserialise;
            }
        }

        public static T download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance
                return !string.IsNullOrEmpty(json_data) ? Deserialize<T>(json_data) : new T();
            }
        }

        public static string GetElementValueFromJSONString(string jsonString, string dataElementName)
        {
            string dataElementValue = null;
            try
            {
                Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(jsonString);
                dataElementValue = json[dataElementName].ToString();

            }
            catch { }
            return dataElementValue;
        }
    }
}