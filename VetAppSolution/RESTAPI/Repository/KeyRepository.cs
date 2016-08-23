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
    }
}
