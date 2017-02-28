using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Vetapp.Engine.BusinessAccessLayer;
using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.DataAccessLayer.Enumeration;

using MainSite.Models;

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
        public bool Exist(string username)
        {
            bool bExist = true;
            ArrayList arUser = Find(username);
            if ((!HasError) && (arUser.Count == 0))
            {
                bExist = false;
            }
            return bExist;
        }
        public ArrayList Find(string username)
        {
            ArrayList arUser = null;
            try
            {
                if ((!string.IsNullOrEmpty(username)))
                {
                    EnumUser enumUser = new EnumUser() { Username = username };
                    arUser = UserGetList(enumUser);
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }
            return arUser;
        }

        public User UserCreate(string username, string password)
        {
            User user = null;
            try
            {
                string passwordEncrypted = UtilsSecurity.encrypt(password);
                User userTmp = new User() { Username = username, Passwd = passwordEncrypted, UserRoleID = 1, CookieID = Guid.NewGuid().ToString(), NumberOfVisits = 1 };
                long lID = UserCreateOrModify(userTmp);
                if (lID > 0)
                {
                    user = UserGet(lID);
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return user;
        }

        public User UserAuthenticate(string username, string password, long userroleid = 1)
        {
            User user = null;
            try
            {
                string passwordEncrypted = UtilsSecurity.encrypt(password);
                EnumUser enumUser = new EnumUser() { Username = username, Passwd = passwordEncrypted, UserRoleID = userroleid };
                ArrayList arUser = UserGetList(enumUser);
                if ((arUser != null) && (arUser.Count == 1))
                {
                    user = (User)arUser[0];
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return user;
        }

        public long EvaluationCreate(EvaluationModel evaluationModel, long userid)
        {
            long lID = 0;
            try
            {
                Evaluation evaluation = new Evaluation()
                {
                    HasAClaim = evaluationModel.HasAClaim,
                    HasActiveAppeal = evaluationModel.HasActiveAppeal,
                    IsFirsttimeFiling = evaluationModel.IsFirstTimeFiling,
                    CurrentRating = evaluationModel.CurrentRating,
                    UserID = userid
                };
                lID = EvaluationCreateOrModify(evaluation);
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return lID;
        }

        public Evaluation EvaluationGet(User user)
        {
            Evaluation evaluation = null;
            if ((user != null) && (user.UserID > 0))
            {
                EnumEvaluation enumEvaluation = new EnumEvaluation() { UserID = user.UserID };
                ArrayList arEvaluation = EvaluationGetList(enumEvaluation);
                if ((arEvaluation != null) && (arEvaluation.Count > 0))
                {
                    evaluation = (Evaluation)arEvaluation[arEvaluation.Count - 1];
                }
            }
            return evaluation;
        }

        public User UserGet(string cookieid)
        {
            User user = null;
            if (!string.IsNullOrEmpty(cookieid))
            {
                EnumUser enumUser = new EnumUser() { CookieID = cookieid };
                ArrayList arUser = UserGetList(enumUser);
                if ((arUser != null) && (arUser.Count == 1))
                {
                    user = (User)arUser[0];
                }
            }
            return user;
        }
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
        /// EvaluationCreateOrModify
        /// </summary>
        /// <param name="">pEvaluation</param>
        /// <returns>long</returns>
        /// 
        public long EvaluationCreateOrModify(Evaluation pEvaluation)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusEvaluation busEvaluation = null;
                busEvaluation = new BusEvaluation(conn);
                busEvaluation.Save(pEvaluation);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pEvaluation.EvaluationID;
                _hasError = busEvaluation.HasError;
                if (busEvaluation.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// EvaluationGetList
        /// </summary>
        /// <param name="">pEnumEvaluation</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList EvaluationGetList(EnumEvaluation pEnumEvaluation)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusEvaluation busEvaluation = null;
                busEvaluation = new BusEvaluation(conn);
                items = busEvaluation.Get(pEnumEvaluation);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busEvaluation.HasError;
                if (busEvaluation.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// EvaluationGet
        /// </summary>
        /// <param name="">pLngEvaluationID</param>
        /// <returns>Evaluation</returns>
        /// 
        public Evaluation EvaluationGet(long pLngEvaluationID)
        {
            Evaluation evaluation = new Evaluation() { EvaluationID = pLngEvaluationID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusEvaluation busEvaluation = null;
                busEvaluation = new BusEvaluation(conn);
                busEvaluation.Load(evaluation);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busEvaluation.HasError;
                if (busEvaluation.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return evaluation;
        }

        /// <summary>
        /// EvaluationRemove
        /// </summary>
        /// <param name="">pEvaluationID</param>
        /// <returns>void</returns>
        /// 
        public void EvaluationRemove(long pEvaluationID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Evaluation evaluation = new Evaluation();
                evaluation.EvaluationID = pEvaluationID;
                BusEvaluation bus = null;
                bus = new BusEvaluation(conn);
                bus.Delete(evaluation);
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
