namespace Vetapp.Engine.Common
{
    public class ErrorCode
    {
        //Attributes
        /// <summary>ErrorCodeID Attribute</summary>
        private long _lErrorCodeID = 0;

        /// <summary>Code Attribute</summary>
        private string _strCode = null;

        /// <summary>Description Attribute</summary>
        private string _strDescription = null;

        /// <summary>VisibleCode Attribute</summary>
        private string _strVisibleCode = null;

        /// <summary>Trace Attribute</summary>
        private string _strTrace = null;

        public long ErrorCodeID
        {
            get { return _lErrorCodeID; }
            set { _lErrorCodeID = value; }
        }

        /// <summary>Code is a Property in the ErrorCode Class of type String</summary>
        public string Code
        {
            get { return _strCode; }
            set { _strCode = value; }
        }

        /// <summary>Description is a Property in the ErrorCode Class of type String</summary>
        public string Description
        {
            get { return _strDescription; }
            set { _strDescription = value; }
        }

        /// <summary>VisibleCode is a Property in the ErrorCode Class of type String</summary>
        public string VisibleCode
        {
            get { return _strVisibleCode; }
            set { _strVisibleCode = value; }
        }

        /// <summary>Trace is a Property in the ErrorCode Class of type String</summary>
        public string Trace
        {
            get { return _strTrace; }
            set { _strTrace = value; }
        }

        public ErrorCode()
        {
        }
    }
}