namespace RESTAPI.Reply
{
    public class ReplyBase
    {
        private bool _hasError = false;
        private string _errorMessage = string.Empty;
        private string _errorStacktrace = string.Empty;
        private string _statusErrorMessage = string.Empty;

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
        public string StatusErrorMessage
        {
            get { return _statusErrorMessage; }
            set { _statusErrorMessage = value; }
        }
        protected void SetResponseReply()
        { }
    }
}
