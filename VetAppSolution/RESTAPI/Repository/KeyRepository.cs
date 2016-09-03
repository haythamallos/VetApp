using System.Collections.Generic;
using RESTAPI.Models;
using Microsoft.Extensions.Options;
using RESTAPI.Facade;
using Microsoft.Extensions.Caching.Memory;

namespace RESTAPI.Repository
{
    public class KeyRepository : IKeyRepository
    {
        private readonly AppSettings _settings;

        public KeyRepository(IOptions<AppSettings> settings, IMemoryCache memoryCache)
        {
            _settings = settings.Value;
        }
        public bool CheckValidUserKey(string key)
        {
            bool bReturn = false;
            if ((key != null) && (APIKeys.Contains(key)))
            {
                bReturn = true;
            }
            return bReturn;
        }

        private List<string> APIKeys
        {
            get
            {
                BusFacCore busFacCore = new BusFacCore(_settings);
                List<Apikey> lstApikey = busFacCore.ApikeyGetList();
                List<string> keys = new List<string>();
                //var keys = HttpContext.Current.Cache[APIKEYLIST] as List<string>;

                //if (keys == null)
                //{
                //    keys = PopulateAPIKeys();
                //}
                //else if (keys.Count == 0)
                //{
                //    keys = PopulateAPIKeys();
                //}

                return keys;
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

        //private List<string> PopulateAPIKeys()
        //{
        //    List<string> keyList = new List<string>();
        //    BusFacCore busFacCore = new BusFacCore(_settings);
        //    List<Apikey> lstApikey = busFacCore.ApikeyGetList();
        //    return keyList;
        //}

    }
}
