/// <summary>
/// Copyright (c) 2014 Vetapp Inc.  San Diego, California, USA
/// All Rights Reserved
///
/// File:  Config.cs
/// History
/// ----------------------------------------------------
/// 001	HA	9/3/2014	Created
///
/// </summary>
///
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace Vetapp.Engine.Common
{
    public class Config
    {
        private string _strLogDir = null;
        private bool _bDoLogInfo = false;
        private bool _bDoLogError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;
        private bool _hasError = false;
        private string _strConnectionString = null;
        private string _strStorageConnectionString = null;
        private string _strSmtpServer = null;
        private int _nSmtpServerPort = 25;
        private string _strSmtpServerUsername = null;
        private string _strSmtpServerPassword = null;
        private string _strMailFrom = null;
        private string _strMailSubject = null;
        private string _strRESTLink = null;
        private string _strAPIKey = null;
        private string _strAuthKey = null;
        private string _strMapsAPIKey = null;
        private string _strRedisCacheConnectionString = null;

        private static readonly string KEY_LOG_DIR = "LogDirPath";
        private static readonly string KEY_CONNECTION_STRING = "ConnectionString";
        private static readonly string KEY_STORAGE_CONNECTION_STRING = "StorageConnectionString";
        private static readonly string KEY_REDIS_CACHE_CONNECTION_STRING = "RedisCacheConnectionString";
        private static readonly string KEY_DO_LOG_INFO = "DoLogInfo";
        private static readonly string KEY_DO_LOG_ERROR = "DoLogError";
        private static readonly string KEY_SMTP_SERVER = "CredentialsEmailSmtpServer";
        private static readonly string KEY_SMTP_SERVER_PORT = "CredentialsEmailSmtpServerPort";
        private static readonly string KEY_SMTP_SERVER_USERNAME = "CredentialsEmailSmtpServerUsername";
        private static readonly string KEY_SMTP_SERVER_PASSWORD = "CredentialsEmailSmtpServerPassword";
        private static readonly string KEY_MAIL_FROM = "CredentialsEmailFrom";
        private static readonly string KEY_MAIL_SUBJECT = "CredentialsEmailSubject";
        private static readonly string KEY_REST_LINK = "RESTLink";
        private static readonly string KEY_API_KEY = "APIKey";
        private static readonly string KEY_AUTH_KEY = "AuthKey";
        private static readonly string KEY_MAPS_API_KEY = "MapsAPIKey";

        /// <summary>
        /// Gets the error stacktrace.
        /// </summary>
        /// <value>
        /// The error stacktrace.
        /// </value>
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        /// <summary>HasError Property in class Column and is of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }

        public string RESTLink
        {
            get { return _strRESTLink; }
            set { _strRESTLink = value; }
        }

        public string APIKey
        {
            get { return _strAPIKey; }
            set { _strAPIKey = value; }
        }

        public string AuthKey
        {
            get { return _strAuthKey; }
            set { _strAuthKey = value; }
        }

        public string MailSubject
        {
            get { return _strMailSubject; }
            set { _strMailSubject = value; }
        }

        public int SmtpServerPort
        {
            get { return _nSmtpServerPort; }
            set { _nSmtpServerPort = value; }
        }

        public string SmtpServer
        {
            get { return _strSmtpServer; }
            set { _strSmtpServer = value; }
        }

        public string SmtpServerUsername
        {
            get { return _strSmtpServerUsername; }
            set { _strSmtpServerUsername = value; }
        }

        public string SmtpServerPassword
        {
            get { return _strSmtpServerPassword; }
            set { _strSmtpServerPassword = value; }
        }

        public string MailFrom
        {
            get { return _strMailFrom; }
            set { _strMailFrom = value; }
        }

        public bool DoLogError
        {
            get { return _bDoLogError; }
            set { _bDoLogError = value; }
        }

        public bool DoLogInfo
        {
            get { return _bDoLogInfo; }
            set { _bDoLogInfo = value; }
        }

        public string LogDir
        {
            get { return _strLogDir; }
            set { _strLogDir = value; }
        }

        public string ConnectionString
        {
            get { return _strConnectionString; }
            set { _strConnectionString = value; }
        }

        public string StorageConnectionString
        {
            get { return _strStorageConnectionString; }
            set { _strStorageConnectionString = value; }
        }

        public string RedisCacheConnectionString
        {
            get { return _strRedisCacheConnectionString; }
            set { _strRedisCacheConnectionString = value; }
        }

        public string MapsAPIKey
        {
            get { return _strMapsAPIKey; }
            set { _strMapsAPIKey = value; }
        }

        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(KEY_LOG_DIR + ":  " + LogDir + "\n");
            sbReturn.Append(KEY_CONNECTION_STRING + ":  " + ConnectionString + "\n");
            sbReturn.Append(KEY_STORAGE_CONNECTION_STRING + ":  " + StorageConnectionString + "\n");
            sbReturn.Append(KEY_REDIS_CACHE_CONNECTION_STRING + ":  " + RedisCacheConnectionString + "\n");
            sbReturn.Append(KEY_DO_LOG_INFO + ":  " + DoLogInfo + "\n");
            sbReturn.Append(KEY_DO_LOG_ERROR + ":  " + DoLogError + "\n");
            sbReturn.Append(KEY_SMTP_SERVER + ":  " + SmtpServer + "\n");
            sbReturn.Append(KEY_SMTP_SERVER_PORT + ":  " + SmtpServerPort + "\n");
            sbReturn.Append(KEY_SMTP_SERVER_USERNAME + ":  " + SmtpServerUsername + "\n");
            sbReturn.Append(KEY_SMTP_SERVER_PASSWORD + ":  " + SmtpServerPassword + "\n");
            sbReturn.Append(KEY_MAIL_FROM + ":  " + MailFrom + "\n");
            sbReturn.Append(KEY_MAIL_SUBJECT + ":  " + MailSubject + "\n");
            sbReturn.Append(KEY_REST_LINK + ":  " + RESTLink + "\n");
            sbReturn.Append(KEY_API_KEY + ":  " + APIKey + "\n");
            sbReturn.Append(KEY_AUTH_KEY + ":  " + AuthKey + "\n");

            return sbReturn.ToString();
        }

        public Config()
        {
            AppSettingsReader configurationAppSettings = null;
            configurationAppSettings = new AppSettingsReader();
            try
            {
                // get the log directory
                try
                {
                    LogDir = (string)configurationAppSettings.GetValue(KEY_LOG_DIR, typeof(System.String));
                    if (!Directory.Exists(LogDir))
                    {
                        Directory.CreateDirectory(LogDir);
                    }
                }
                catch (Exception e)
                {
                    LogDir = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    // get the connection string
                    ConnectionString = (string)configurationAppSettings.GetValue(KEY_CONNECTION_STRING, typeof(System.String));
                }
                catch (Exception e)
                {
                    ConnectionString = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    // get the pervasive connection string
                    StorageConnectionString = (string)configurationAppSettings.GetValue(KEY_STORAGE_CONNECTION_STRING, typeof(System.String));
                }
                catch (Exception e)
                {
                    StorageConnectionString = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    // get the pervasive connection string
                    RedisCacheConnectionString = (string)configurationAppSettings.GetValue(KEY_REDIS_CACHE_CONNECTION_STRING, typeof(System.String));
                }
                catch (Exception e)
                {
                    RedisCacheConnectionString = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    DoLogInfo = (bool)configurationAppSettings.GetValue(KEY_DO_LOG_INFO, typeof(System.Boolean));
                }
                catch (Exception e)
                {
                    DoLogInfo = false;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    DoLogError = (bool)configurationAppSettings.GetValue(KEY_DO_LOG_ERROR, typeof(System.Boolean));
                }
                catch (Exception e)
                {
                    DoLogError = false;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    SmtpServer = (string)configurationAppSettings.GetValue(KEY_SMTP_SERVER, typeof(System.String));
                }
                catch (Exception e)
                {
                    SmtpServer = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    string tmp = null;
                    tmp = (string)configurationAppSettings.GetValue(KEY_SMTP_SERVER_PORT, typeof(System.String));
                    try
                    {
                        SmtpServerPort = (int)Convert.ToInt32(tmp);
                    }
                    catch (Exception ex)
                    {
                        SmtpServerPort = 25;
                        _hasError = true;
                        _errorStacktrace = ex.StackTrace.ToString();
                        _errorMessage = ex.Message;
                    }
                }
                catch (Exception e)
                {
                    SmtpServerPort = 25;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    SmtpServerUsername = (string)configurationAppSettings.GetValue(KEY_SMTP_SERVER_USERNAME, typeof(System.String));
                }
                catch (Exception e)
                {
                    SmtpServerUsername = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    SmtpServerPassword = (string)configurationAppSettings.GetValue(KEY_SMTP_SERVER_PASSWORD, typeof(System.String));
                }
                catch (Exception e)
                {
                    SmtpServerPassword = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    MailFrom = (string)configurationAppSettings.GetValue(KEY_MAIL_FROM, typeof(System.String));
                }
                catch (Exception e)
                {
                    MailFrom = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    MailSubject = (string)configurationAppSettings.GetValue(KEY_MAIL_SUBJECT, typeof(System.String));
                }
                catch (Exception e)
                {
                    MailSubject = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }
                try
                {
                    RESTLink = (string)configurationAppSettings.GetValue(KEY_REST_LINK, typeof(System.String));
                }
                catch (Exception e)
                {
                    RESTLink = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }
                try
                {
                    APIKey = (string)configurationAppSettings.GetValue(KEY_API_KEY, typeof(System.String));
                }
                catch (Exception e)
                {
                    APIKey = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }
                try
                {
                    AuthKey = (string)configurationAppSettings.GetValue(KEY_AUTH_KEY, typeof(System.String));
                }
                catch (Exception e)
                {
                    AuthKey = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }

                try
                {
                    MapsAPIKey = (string)configurationAppSettings.GetValue(KEY_MAPS_API_KEY, typeof(System.String));
                }
                catch (Exception e)
                {
                    MapsAPIKey = null;
                    _hasError = true;
                    _errorStacktrace = e.StackTrace.ToString();
                    _errorMessage = e.Message;
                }
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
        }

        public void Test()
        {
            try
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1.  Output configuration settings.");
                Console.WriteLine("q.  Quit.");

                string strAns = "";

                strAns = Console.ReadLine();
                if (strAns != "q")
                {
                    int nAns = 0;
                    nAns = int.Parse(strAns);
                    switch (nAns)
                    {
                        case 1:
                            Console.WriteLine(ToString());
                            break;

                        default:
                            Console.WriteLine("Undefined option.");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
        }
    }
}