using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using System;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Enumeration;
using Vetapp.Engine.DataAccessLayer.Data;

namespace RESTAPI.Repository
{
    public class KeyRepository : IKeyRepository
    {
        private readonly AppSettings _settings;
        private readonly IMemoryCache _memoryCache;

        private const string CACHEKEYLIST = "APIKeyList";

        public KeyRepository(IOptions<AppSettings> settings, IMemoryCache memoryCache)
        {
            _settings = settings.Value;
            _memoryCache = memoryCache;
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
                List<string> keys = _memoryCache.Get(CACHEKEYLIST) as List<string>;

                if (keys == null)
                {
                    BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
                    EnumApikey enumApikey = new EnumApikey();
                    System.Collections.ArrayList lstApikey = busFacCore.ApikeyGetList(enumApikey);
                    if ((lstApikey != null) && (lstApikey.Count > 0))
                    {
                        keys = new List<string>();
                        foreach (Apikey k in lstApikey)
                        {
                            keys.Add(k.Token);
                        }
                        // keep item in cache as long as it is requested at least
                        // once every 5 minutes...
                        // but in any case make sure to refresh it every hour
                        _memoryCache.Set(CACHEKEYLIST, keys,
                            new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                    }
                }

                return keys;
            }
        }
    }
}
