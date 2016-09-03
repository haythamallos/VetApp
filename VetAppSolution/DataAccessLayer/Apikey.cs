using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veteran.Engine.DataAccessLayer
{
    public class Apikey : DbContext
    {
        public long ApikeyId { get; set; }
        public string DateCreated { get; }
        public string DateModified { get; }
        public string DateExpiration { get; }
        public bool IsDisabled { get; set; }
        public string ApiauthToken { get; set; }
        public string Notes { get; set; }

    }
}
