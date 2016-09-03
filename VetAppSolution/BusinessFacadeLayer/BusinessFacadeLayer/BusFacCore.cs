using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veteran.Engine.BusinessFacadeLayer
{
    public class BusFacCore
    {
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        public bool HasError
        {
            get { return _hasError; }
        }
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
        public BusFacCore()
        {
        }
       
    }
}
