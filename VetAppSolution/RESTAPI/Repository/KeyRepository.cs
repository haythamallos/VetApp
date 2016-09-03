
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Repository
{
    public class KeyRepository : IKeyRepository
    {
        public bool CheckValidUserKey(string reqkey)
        {
            var userkeyList = new List<string>();
            userkeyList.Add("28236d8ec201df516d0f6472d516d72d");
            userkeyList.Add("38236d8ec201df516d0f6472d516d72c");
            userkeyList.Add("48236d8ec201df516d0f6472d516d72b");

            if (userkeyList.Contains(reqkey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private static List<string> APIKeys
        //{
        //    get
        //    {
        //        // Get from the cache
        //        // Could also use AppFabric cache for scalability
        //        var keys = HttpContext.Current.Cache[APIKEYLIST] as List<string>;

        //        if (keys == null)
        //        {
        //            keys = PopulateAPIKeys();
        //        }
        //        else if (keys.Count == 0)
        //        {
        //            keys = PopulateAPIKeys();
        //        }

        //        return keys;
        //    }
        //}

        //private static List<string> PopulateAPIKeys()
        //{
        //    List<string> keyList = new List<string>();
        //    keyList = getTestKeys();
        //    return keyList;
        //}
        
        //private List<string> getTestKeys()
        //{
        //    List<string> keyList = new List<string>();

        //    keyList.Add("28236d8ec201df516d0f6472d516d72d");
        //    keyList.Add("38236d8ec201df516d0f6472d516d72c");
        //    keyList.Add("48236d8ec201df516d0f6472d516d72b");

        //    return keyList;
        //}
    }
}
