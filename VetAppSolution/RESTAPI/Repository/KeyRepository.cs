using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Repository
{
    public class KeyRepository : IKeyRepository
    {
        public bool CheckValidApiKey(string apikey)
        {
            return true;
        }
    }
}
