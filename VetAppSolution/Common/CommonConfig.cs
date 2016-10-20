using System;
using System.Configuration;

namespace Vetapp.Engine.Common
{
    public class CommonConfig
    {
        private string _strStorageConnectionString = null;
        private string _strComputeAccount = null;

        private string _errorMessage = null;
        private string _errorStacktrace = null;
        private bool _hasError = false;

        private static readonly string KEY_STORAGE_ACCOUNT = "StorageConnectionString";
        private static readonly string KEY_COMPUTE_ACCOUNT = "ComputeAccount";

        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public bool HasError
        {
            get { return _hasError; }
        }

        public string StorageConnectionString
        {
            get { return _strStorageConnectionString; }
            set { _strStorageConnectionString = value; }
        }

        public string ComputeAccount
        {
            get { return _strComputeAccount; }
            set { _strComputeAccount = value; }
        }

        public CommonConfig()
        {
            AppSettingsReader configurationAppSettings = null;
            configurationAppSettings = new AppSettingsReader();

            try
            {
                StorageConnectionString = (string)configurationAppSettings.GetValue(KEY_STORAGE_ACCOUNT, typeof(System.String));
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }

            try
            {
                ComputeAccount = (string)configurationAppSettings.GetValue(KEY_COMPUTE_ACCOUNT, typeof(System.String));
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
        }
    }
}