using System;

namespace Vetapp.Client.Proxy
{
    public class Utils
    {
        public static string ToJson(object Obj, Type ObjType)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Obj);
        }
    }
}
