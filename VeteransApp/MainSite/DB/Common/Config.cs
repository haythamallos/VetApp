using System;
using System.Configuration;

namespace Vetapp.Engine.Common
{
    public class Config
    {
        private string _strConnectionString = null;
        private string _strSmtpServer = null;
        private int _nSmtpServerPort = 25;
        private string _strSmtpServerUsername = null;
        private string _strSmtpServerPassword = null;
        private string _strMailFrom = null;
        private string _strMailSubject = null;

        private static readonly string KEY_CONNECTION_STRING = "ConnectionString";
        private static readonly string KEY_SMTP_SERVER = "CredentialsEmailSmtpServer";
        private static readonly string KEY_SMTP_SERVER_PORT = "CredentialsEmailSmtpServerPort";
        private static readonly string KEY_SMTP_SERVER_USERNAME = "CredentialsEmailSmtpServerUsername";
        private static readonly string KEY_SMTP_SERVER_PASSWORD = "CredentialsEmailSmtpServerPassword";
        private static readonly string KEY_MAIL_FROM = "CredentialsEmailFrom";
        private static readonly string KEY_MAIL_SUBJECT = "CredentialsEmailSubject";


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

        public string ConnectionString
        {
            get { return _strConnectionString; }
            set { _strConnectionString = value; }
        }

        public Config()
        {
            AppSettingsReader configurationAppSettings = null;
            configurationAppSettings = new AppSettingsReader();
            try
            {

                try
                {
                    // get the connection string
                    ConnectionString = (string)configurationAppSettings.GetValue(KEY_CONNECTION_STRING, typeof(System.String));
                }
                catch (Exception e)
                {
                    ConnectionString = null;
                }

                try
                {
                    SmtpServer = (string)configurationAppSettings.GetValue(KEY_SMTP_SERVER, typeof(System.String));
                }
                catch (Exception e)
                {
                    SmtpServer = null;
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
                    }
                }
                catch (Exception e)
                {
                    SmtpServerPort = 25;
                }

                try
                {
                    SmtpServerUsername = (string)configurationAppSettings.GetValue(KEY_SMTP_SERVER_USERNAME, typeof(System.String));
                }
                catch (Exception e)
                {
                    SmtpServerUsername = null;
                }

                try
                {
                    SmtpServerPassword = (string)configurationAppSettings.GetValue(KEY_SMTP_SERVER_PASSWORD, typeof(System.String));
                }
                catch (Exception e)
                {
                    SmtpServerPassword = null;
                }

                try
                {
                    MailFrom = (string)configurationAppSettings.GetValue(KEY_MAIL_FROM, typeof(System.String));
                }
                catch (Exception e)
                {
                    MailFrom = null;
                }

                try
                {
                    MailSubject = (string)configurationAppSettings.GetValue(KEY_MAIL_SUBJECT, typeof(System.String));
                }
                catch (Exception e)
                {
                    MailSubject = null;
                }

            }
            catch (Exception e)
            {
            }
        }

   
    }
}