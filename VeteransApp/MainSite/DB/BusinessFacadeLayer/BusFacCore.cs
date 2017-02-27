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
        //public User UserEnumByAuthUserid(string pStrUserid)
        //{
        //    User user = null;
        //    bool bConn = false;
        //    SqlConnection conn = getDBConnection();
        //    if (conn != null)
        //    {
        //        ArrayList items = null;
        //        BusUser busUser = null;
        //        busUser = new BusUser(conn);
        //        busUser.SP_ENUM_NAME = "spUserEnumByAuthUserid";
        //        EnumUser enumUser = new EnumUser() { AuthUserid = pStrUserid };
        //        items = busUser.Get(enumUser);
        //        // close the db connection
        //        bConn = CloseConnection(conn);
        //        _hasError = busUser.HasError;
        //        if ((items != null) && (items.Count > 0))
        //        {
        //            user = (User) items[items.Count - 1];
        //        }
        //    }
        //    return user;
        //}
        /*********************** CUSTOM END *********************/



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
        //------------------------------------------
        /// <summary>
        /// UserRoleCreateOrModify
        /// </summary>
        /// <param name="">pUserRole</param>
        /// <returns>long</returns>
        /// 
        public long UserRoleCreateOrModify(UserRole pUserRole)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                busUserRole.Save(pUserRole);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pUserRole.UserRoleID;
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// UserRoleGetList
        /// </summary>
        /// <param name="">pEnumUserRole</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList UserRoleGetList(EnumUserRole pEnumUserRole)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                items = busUserRole.Get(pEnumUserRole);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// UserRoleGet
        /// </summary>
        /// <param name="">pLngUserRoleID</param>
        /// <returns>UserRole</returns>
        /// 
        public UserRole UserRoleGet(long pLngUserRoleID)
        {
            UserRole user_role = new UserRole() { UserRoleID = pLngUserRoleID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                busUserRole.Load(user_role);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return user_role;
        }

        /// <summary>
        /// UserRoleRemove
        /// </summary>
        /// <param name="">pUserRoleID</param>
        /// <returns>void</returns>
        /// 
        public void UserRoleRemove(long pUserRoleID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                UserRole user_role = new UserRole();
                user_role.UserRoleID = pUserRoleID;
                BusUserRole bus = null;
                bus = new BusUserRole(conn);
                bus.Delete(user_role);
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
