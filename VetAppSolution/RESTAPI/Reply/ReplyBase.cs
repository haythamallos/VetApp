using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Reply
{
    public class ReplyBase
    {
        private bool _hasError = false;
        private string _errorMessage = string.Empty;
        private string _errorStacktrace = string.Empty;

        protected AppSettings _settings = null;

        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
            set { _errorStacktrace = value; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        protected void SetResponseReply()
        { }
    }
}
