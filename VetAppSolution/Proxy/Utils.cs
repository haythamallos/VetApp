using System;

namespace Proxy
{
    public class Utils
    {
        public static string ToJson(object Obj, Type ObjType)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Obj);
        }
    }
}
