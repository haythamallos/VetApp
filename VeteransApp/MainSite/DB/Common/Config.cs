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

        private string _strStripeApiKey = null;
        private string _strStripeSecretKey = null;

        private static readonly string KEY_CONNECTION_STRING = "ConnectionString";
        private static readonly string KEY_SMTP_SERVER = "CredentialsEmailSmtpServer";
        private static readonly string KEY_SMTP_SERVER_PORT = "CredentialsEmailSmtpServerPort";
        private static readonly string KEY_SMTP_SERVER_USERNAME = "CredentialsEmailSmtpServerUsername";
        private static readonly string KEY_SMTP_SERVER_PASSWORD = "CredentialsEmailSmtpServerPassword";
        private static readonly string KEY_MAIL_FROM = "CredentialsEmailFrom";
        private static readonly string KEY_MAIL_SUBJECT = "CredentialsEmailSubject";
        private static readonly string KEY_STRIPE_API_KEY = "StripeApiKey";
        private static readonly string KEY_STRIPE_SECRET_KEY = "StripeSecretKey";

        public string StripeSecretKey
        {
            get { return _strStripeSecretKey; }
            set { _strStripeSecretKey = value; }
        }

        public string StripeApiKey
        {
            get { return _strStripeApiKey; }
            set { _strStripeApiKey = value; }
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

                try
                {
                    StripeApiKey = (string)configurationAppSettings.GetValue(KEY_STRIPE_API_KEY, typeof(System.String));
                }
                catch (Exception e)
                {
                    StripeApiKey = null;
                }

                try
                {
                    StripeSecretKey = (string)configurationAppSettings.GetValue(KEY_STRIPE_SECRET_KEY, typeof(System.String));
                }
                catch (Exception e)
                {
                    StripeSecretKey = null;
                }

            }
            catch (Exception e)
            {
            }
        }

   
    }
}