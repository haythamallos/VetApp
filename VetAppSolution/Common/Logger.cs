using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Vetapp.Engine.Common
{
    public class Logger
    {
        private string _strMsgSource = null;
        private static PoolConnection _pool = null;
        private static Config _config = null;
        private long _lInteractionID = 0;
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        private const string CONNECTION_POOL_NAME = "Main";

        //Here is the once-per-class call to initialize the log object
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        public string MsgSource
        {
            get { return _strMsgSource; }
            set { _strMsgSource = value; }
        }

        public Logger(string pStrSource)
        {
            //log.Debug("Application Started");
            try
            {
                //_strFileFullname = pStrFileFullname;
                _config = new Config();
                _pool = PoolConnection.GetInstance(_config, CONNECTION_POOL_NAME);
                _strMsgSource = pStrSource;

                //You should try to call the logger as soon as possible in your application
                //log.Debug("Application started");
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorStacktrace = ex.StackTrace.ToString();
                _errorMessage = ex.Message;
            }
        }

        public void Log(string pStrAction, string pStrMsgText, long pLngInteractionID)
        {
            _lInteractionID = pLngInteractionID;
            Log(pStrAction, pStrMsgText);
        }

        public void Log(string pStrAction, string pStrMsgText)
        {
            try
            {
                SqlConnection conn = _pool.GetConnection();
                bool bConn = false;

                bConn = _pool.OpenConnection(conn);
                if (bConn)
                {
                    Vetapp.Engine.Common.Syslog syslog = null;
                    syslog = new Vetapp.Engine.Common.Syslog(_config);
                    syslog.Msgtxt = pStrMsgText;
                    syslog.Msgaction = pStrAction;
                    syslog.Msgsource = _strMsgSource;
                    syslog.InteractionID = _lInteractionID;
                    syslog.Save(conn);
                    // close the db connection
                    bConn = _pool.CloseConnection(conn);
                    conn = null;
                }
                else
                {
                    // resort to writing to a file instead of db
                    writeToFile(pStrAction, pStrMsgText);
                }
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
                // resort to writing to a file instead of db
                writeToFile(pStrAction, pStrMsgText);
            }
        }

        private void writeToFile(string pStrAction, string pStrMsgText)
        {
            try
            {
                // will append to file specified by filename property.
                // if that file is invalid, will write to a default file
                StreamWriter objWriter = null;
                string strFileFullname = null;
                strFileFullname = _config.LogDir + System.IO.Path.DirectorySeparatorChar + "VetappSyslog.txt";
                if (!Directory.Exists(_config.LogDir))
                {
                    Directory.CreateDirectory(_config.LogDir);
                }
                if (!File.Exists(strFileFullname))
                {
                    objWriter = File.CreateText(strFileFullname);
                }
                else
                {
                    objWriter = File.AppendText(strFileFullname);
                }
                if (pStrMsgText != null)
                {
                    objWriter.WriteLine(ToString(pStrAction, pStrMsgText));
                }

                objWriter.Close();
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorStacktrace = ex.StackTrace.ToString();
                _errorMessage = ex.Message;
            }
        }

        public string ToString(string pStrAction, string pStrMsg)
        {
            string strReturn = "";

            strReturn = DateTime.UtcNow.ToLongDateString() + " " + DateTime.UtcNow.ToLongTimeString() + ":  " + pStrAction + "\n" + pStrMsg;
            return strReturn;
        }
    }

    internal class Syslog
    {
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        //Attributes
        /// <summary>SyslogID Attribute type String</summary>
        private long _lSyslogID = 0;

        /// <summary>DateCreated Attribute type String</summary>
        private DateTime _dtDateCreated = dtNull;

        /// <summary>RefNum Attribute type String</summary>
        private long _lInteractionID = 0;

        /// <summary>Msgsource Attribute type String</summary>
        private string _strMsgsource = null;

        /// <summary>Msgaction Attribute type String</summary>
        private string _strMsgaction = null;

        /// <summary>Msgtxt Attribute type String</summary>
        private string _strMsgtxt = null;

        private Config _config = null;
        private LoggerFile _oLog = null;

        private string _strLognameText = "Syslog";
        private bool _hasError = false;
        private static DateTime dtNull = new DateTime();

        /// <summary>HasError Property in class Syslog and is of type bool</summary>
        public static readonly string ENTITY_NAME = "Syslog"; //Table name to abstract

        // DB Field names
        /// <summary>ID Database field</summary>
        public static readonly string DB_FIELD_ID = "syslog_id"; //Table id field name

        /// <summary>date_created Database field </summary>
        public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name

        /// <summary>ref_num Database field </summary>
        public static readonly string DB_FIELD_INTERACTION_ID = "interaction_id"; //Table InteractionID field name

        /// <summary>msgsource Database field </summary>
        public static readonly string DB_FIELD_MSGSOURCE = "msgsource"; //Table Msgsource field name

        /// <summary>msgaction Database field </summary>
        public static readonly string DB_FIELD_MSGACTION = "msgaction"; //Table Msgaction field name

        /// <summary>msgtxt Database field </summary>
        public static readonly string DB_FIELD_MSGTXT = "msgtxt"; //Table Msgtxt field name

        // Attribute variables
        /// <summary>TAG_ID Attribute type string</summary>
        public static readonly string TAG_ID = "SyslogID"; //Attribute id  name

        /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name

        /// <summary>RefNum Attribute type string</summary>
        public static readonly string TAG_INTERACTION_ID = "InteractionID"; //Table InteractionID field name

        /// <summary>Msgsource Attribute type string</summary>
        public static readonly string TAG_MSGSOURCE = "Msgsource"; //Table Msgsource field name

        /// <summary>Msgaction Attribute type string</summary>
        public static readonly string TAG_MSGACTION = "Msgaction"; //Table Msgaction field name

        /// <summary>Msgtxt Attribute type string</summary>
        public static readonly string TAG_MSGTXT = "Msgtxt"; //Table Msgtxt field name

        // Stored procedure names
        private static readonly string SP_INSERT_NAME = "spSyslogInsert"; //Insert sp name

        //properties
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

        /// <summary>SyslogID is a Property in the Syslog Class of type long</summary>
        public long SyslogID
        {
            get { return _lSyslogID; }
            set { _lSyslogID = value; }
        }

        /// <summary>DateCreated is a Property in the Syslog Class of type DateTime</summary>
        public DateTime DateCreated
        {
            get { return _dtDateCreated; }
            set { _dtDateCreated = value; }
        }

        /// <summary>RefNum is a Property in the Syslog Class of type long</summary>
        public long InteractionID
        {
            get { return _lInteractionID; }
            set { _lInteractionID = value; }
        }

        /// <summary>Msgsource is a Property in the Syslog Class of type String</summary>
        public string Msgsource
        {
            get { return _strMsgsource; }
            set { _strMsgsource = value; }
        }

        /// <summary>Msgaction is a Property in the Syslog Class of type String</summary>
        public string Msgaction
        {
            get { return _strMsgaction; }
            set { _strMsgaction = value; }
        }

        /// <summary>Msgtxt is a Property in the Syslog Class of type String</summary>
        public string Msgtxt
        {
            get { return _strMsgtxt; }
            set { _strMsgtxt = value; }
        }

        /// <summary>HasError Property in class Syslog and is of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
        }

        //Constructors
        /// <summary>Syslog empty constructor</summary>
        //public Syslog()
        //{
        //}
        /// <summary>Syslog constructor takes a Config</summary>
        public Syslog(Config pConfig)
        {
            _config = pConfig;
            _oLog = new LoggerFile(_config.LogDir + System.IO.Path.AltDirectorySeparatorChar + _strLognameText + ".txt");
        }

        /// <summary>
        ///     Dispose of this object's resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }

        /// <summary>
        ///		Free the instance variables of this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return; // we're being collected, so let the GC take care of this object
        }

        // public methods
        /// <summary>ToString is overridden to display all properties of the Syslog Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_ID + ":  " + SyslogID.ToString() + "\n");
            if (!dtNull.Equals(DateCreated))
            {
                sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_DATE_CREATED + ":\n");
            }
            sbReturn.Append(TAG_INTERACTION_ID + ":  " + InteractionID + "\n");
            sbReturn.Append(TAG_MSGSOURCE + ":  " + Msgsource + "\n");
            sbReturn.Append(TAG_MSGACTION + ":  " + Msgaction + "\n");
            sbReturn.Append(TAG_MSGTXT + ":  " + Msgtxt + "\n");

            return sbReturn.ToString();
        }

        /// <summary>Calls sqlInsert() method which inserts a record into the database with current object values</summary>
        public void Save(SqlConnection conn)
        {
            try
            {
                sqlInsert(conn);
            }
            catch (Exception e)
            {
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
                _hasError = true;
            }
        }

        //protected
        /// <summary>Inserts row of data into the database</summary>
        protected void sqlInsert(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramDateCreated = null;
            SqlParameter paramInteractionID = null;
            SqlParameter paramMsgsource = null;
            SqlParameter paramMsgaction = null;
            SqlParameter paramMsgtxt = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure
            cmd = new SqlCommand(SP_INSERT_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
            paramDateCreated.DbType = DbType.DateTime;
            paramDateCreated.Direction = ParameterDirection.Input;

            paramInteractionID = new SqlParameter("@" + TAG_INTERACTION_ID, InteractionID);
            paramInteractionID.DbType = DbType.Int32;
            paramInteractionID.Direction = ParameterDirection.Input;

            paramMsgsource = new SqlParameter("@" + TAG_MSGSOURCE, Msgsource);
            paramMsgsource.DbType = DbType.String;
            paramMsgsource.Size = 255;
            paramMsgsource.Direction = ParameterDirection.Input;

            paramMsgaction = new SqlParameter("@" + TAG_MSGACTION, Msgaction);
            paramMsgaction.DbType = DbType.String;
            paramMsgaction.Size = 255;
            paramMsgaction.Direction = ParameterDirection.Input;

            paramMsgtxt = new SqlParameter("@" + TAG_MSGTXT, Msgtxt);
            paramMsgtxt.DbType = DbType.String;
            paramMsgtxt.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramDateCreated);
            cmd.Parameters.Add(paramInteractionID);
            cmd.Parameters.Add(paramMsgsource);
            cmd.Parameters.Add(paramMsgaction);
            cmd.Parameters.Add(paramMsgtxt);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            // assign the primary kiey
            string strTmp;
            strTmp = cmd.Parameters["@PKID"].Value.ToString();
            SyslogID = long.Parse(strTmp);

            // cleanup to help GC
            paramDateCreated = null;
            paramInteractionID = null;
            paramMsgsource = null;
            paramMsgaction = null;
            paramMsgtxt = null;
            paramPKID = null;
            cmd = null;
        }

        //private
        private void _log(string pStrAction, string pStrMsgText)
        {
            if (_config.DoLogInfo)
            {
                _oLog.Log(pStrAction, pStrMsgText);
            }
        }
    }
}