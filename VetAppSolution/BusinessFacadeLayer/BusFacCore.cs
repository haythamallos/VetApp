using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Vetapp.Engine.BusinessAccessLayer;
using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.DataAccessLayer.Enumeration;



namespace Vetapp.Engine.BusinessFacadeLayer
{
    public class BusFacCore
    {
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        private string _strConnectionString = null;

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
        public BusFacCore(string pStrConnectionString)
        {
            _strConnectionString = pStrConnectionString;
        }
 
        public SqlConnection getDBConnection()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(_strConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                ErrorCode error = new ErrorCode();
                _hasError = true;
            }

            return conn;
        }
        public bool CloseConnection(SqlConnection pRefConn)
        {
            bool bReturn = true;
            try
            {
                if (pRefConn != null)
                {
                    if (pRefConn.State == ConnectionState.Open)
                    {
                        pRefConn.Close();
                    }
                }
                bReturn = true;
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
                bReturn = false;
            }
            return bReturn;
        }

        /*********************** CUSTOM BEGIN *********************/
        public User UserEnumByAuthUserid(string pStrUserid)
        {
            User user = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                ArrayList items = null;
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.SP_ENUM_NAME = "spUserEnumByAuthUserid";
                EnumUser enumUser = new EnumUser() { AuthUserid = pStrUserid };
                items = busUser.Get(enumUser);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if ((items != null) && (items.Count > 0))
                {
                    user = (User) items[items.Count - 1];
                }
            }
            return user;
        }
        /*********************** CUSTOM END *********************/


        //------------------------------------------
        /// <summary>
        /// ApikeyCreateOrModify
        /// </summary>
        /// <param name="">pApikey</param>
        /// <returns>long</returns>
        /// 
        public long ApikeyCreateOrModify(Apikey pApikey)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusApikey busApikey = null;
                busApikey = new BusApikey(conn);
                busApikey.Save(pApikey);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pApikey.ApikeyID;
                _hasError = busApikey.HasError;
                if (busApikey.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// ApikeyGetList
        /// </summary>
        /// <param name="">pEnumApikey</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList ApikeyGetList(EnumApikey pEnumApikey)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusApikey busApikey = null;
                busApikey = new BusApikey(conn);
                items = busApikey.Get(pEnumApikey);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busApikey.HasError;
                if (busApikey.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// ApikeyGet
        /// </summary>
        /// <param name="">pLngApikeyID</param>
        /// <returns>Apikey</returns>
        /// 
        public Apikey ApikeyGet(long pLngApikeyID)
        {
            Apikey apikey = new Apikey() { ApikeyID = pLngApikeyID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusApikey busApikey = null;
                busApikey = new BusApikey(conn);
                busApikey.Load(apikey);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busApikey.HasError;
                if (busApikey.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return apikey;
        }

        /// <summary>
        /// ApikeyRemove
        /// </summary>
        /// <param name="">pApikeyID</param>
        /// <returns>void</returns>
        /// 
        public void ApikeyRemove(long pApikeyID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Apikey apikey = new Apikey();
                apikey.ApikeyID = pApikeyID;
                BusApikey bus = null;
                bus = new BusApikey(conn);
                bus.Delete(apikey);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// ApilogCreateOrModify
        /// </summary>
        /// <param name="">pApilog</param>
        /// <returns>long</returns>
        /// 
        public long ApilogCreateOrModify(Apilog pApilog)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusApilog busApilog = null;
                busApilog = new BusApilog(conn);
                busApilog.Save(pApilog);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pApilog.ApilogID;
                _hasError = busApilog.HasError;
                if (busApilog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// ApilogGetList
        /// </summary>
        /// <param name="">pEnumApilog</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList ApilogGetList(EnumApilog pEnumApilog)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusApilog busApilog = null;
                busApilog = new BusApilog(conn);
                items = busApilog.Get(pEnumApilog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busApilog.HasError;
                if (busApilog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// ApilogGet
        /// </summary>
        /// <param name="">pLngApilogID</param>
        /// <returns>Apilog</returns>
        /// 
        public Apilog ApilogGet(long pLngApilogID)
        {
            Apilog apilog = new Apilog() { ApilogID = pLngApilogID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusApilog busApilog = null;
                busApilog = new BusApilog(conn);
                busApilog.Load(apilog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busApilog.HasError;
                if (busApilog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return apilog;
        }

        /// <summary>
        /// ApilogRemove
        /// </summary>
        /// <param name="">pApilogID</param>
        /// <returns>void</returns>
        /// 
        public void ApilogRemove(long pApilogID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Apilog apilog = new Apilog();
                apilog.ApilogID = pApilogID;
                BusApilog bus = null;
                bus = new BusApilog(conn);
                bus.Delete(apilog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// DbversionCreateOrModify
        /// </summary>
        /// <param name="">pDbversion</param>
        /// <returns>long</returns>
        /// 
        public long DbversionCreateOrModify(Dbversion pDbversion)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                busDbversion.Save(pDbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pDbversion.DbversionID;
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// DbversionGetList
        /// </summary>
        /// <param name="">pEnumDbversion</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList DbversionGetList(EnumDbversion pEnumDbversion)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                items = busDbversion.Get(pEnumDbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// DbversionGet
        /// </summary>
        /// <param name="">pLngDbversionID</param>
        /// <returns>Dbversion</returns>
        /// 
        public Dbversion DbversionGet(long pLngDbversionID)
        {
            Dbversion dbversion = new Dbversion() { DbversionID = pLngDbversionID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                busDbversion.Load(dbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return dbversion;
        }

        /// <summary>
        /// DbversionRemove
        /// </summary>
        /// <param name="">pDbversionID</param>
        /// <returns>void</returns>
        /// 
        public void DbversionRemove(long pDbversionID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Dbversion dbversion = new Dbversion();
                dbversion.DbversionID = pDbversionID;
                BusDbversion bus = null;
                bus = new BusDbversion(conn);
                bus.Delete(dbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// SyslogCreateOrModify
        /// </summary>
        /// <param name="">pSyslog</param>
        /// <returns>long</returns>
        /// 
        public long SyslogCreateOrModify(Syslog pSyslog)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                busSyslog.Save(pSyslog);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pSyslog.SyslogID;
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// SyslogGetList
        /// </summary>
        /// <param name="">pEnumSyslog</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList SyslogGetList(EnumSyslog pEnumSyslog)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                items = busSyslog.Get(pEnumSyslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// SyslogGet
        /// </summary>
        /// <param name="">pLngSyslogID</param>
        /// <returns>Syslog</returns>
        /// 
        public Syslog SyslogGet(long pLngSyslogID)
        {
            Syslog syslog = new Syslog() { SyslogID = pLngSyslogID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                busSyslog.Load(syslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return syslog;
        }

        /// <summary>
        /// SyslogRemove
        /// </summary>
        /// <param name="">pSyslogID</param>
        /// <returns>void</returns>
        /// 
        public void SyslogRemove(long pSyslogID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Syslog syslog = new Syslog();
                syslog.SyslogID = pSyslogID;
                BusSyslog bus = null;
                bus = new BusSyslog(conn);
                bus.Delete(syslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// UserCreateOrModify
        /// </summary>
        /// <param name="">pUser</param>
        /// <returns>long</returns>
        /// 
        public long UserCreateOrModify(User pUser)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.Save(pUser);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pUser.UserID;
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// UserGetList
        /// </summary>
        /// <param name="">pEnumUser</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList UserGetList(EnumUser pEnumUser)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                items = busUser.Get(pEnumUser);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// UserGet
        /// </summary>
        /// <param name="">pLngUserID</param>
        /// <returns>User</returns>
        /// 
        public User UserGet(long pLngUserID)
        {
            User user = new User() { UserID = pLngUserID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.Load(user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return user;
        }

        /// <summary>
        /// UserRemove
        /// </summary>
        /// <param name="">pUserID</param>
        /// <returns>void</returns>
        /// 
        public void UserRemove(long pUserID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                User user = new User();
                user.UserID = pUserID;
                BusUser bus = null;
                bus = new BusUser(conn);
                bus.Delete(user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }






    }
}
