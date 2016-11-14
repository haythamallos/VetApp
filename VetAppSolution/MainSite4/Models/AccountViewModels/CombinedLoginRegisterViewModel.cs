using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainSite.Models.AccountViewModels
{
    public class CombinedLoginRegisterViewModel
    {
        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }
        public RecoverViewModel Recover { get; set; }
    }
}
