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
        /// MemberCreateOrModify
        /// </summary>
        /// <param name="">pMember</param>
        /// <returns>long</returns>
        /// 
        public long MemberCreateOrModify(Member pMember)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusMember busMember = null;
                busMember = new BusMember(conn);
                busMember.Save(pMember);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pMember.MemberID;
                _hasError = busMember.HasError;
                if (busMember.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// MemberGetList
        /// </summary>
        /// <param name="">pEnumMember</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList MemberGetList(EnumMember pEnumMember)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusMember busMember = null;
                busMember = new BusMember(conn);
                items = busMember.Get(pEnumMember);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busMember.HasError;
                if (busMember.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// MemberGet
        /// </summary>
        /// <param name="">pLngMemberID</param>
        /// <returns>Member</returns>
        /// 
        public Member MemberGet(long pLngMemberID)
        {
            Member member = new Member() { MemberID = pLngMemberID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusMember busMember = null;
                busMember = new BusMember(conn);
                busMember.Load(member);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busMember.HasError;
                if (busMember.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return member;
        }

        /// <summary>
        /// MemberRemove
        /// </summary>
        /// <param name="">pMemberID</param>
        /// <returns>void</returns>
        /// 
        public void MemberRemove(long pMemberID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Member member = new Member();
                member.MemberID = pMemberID;
                BusMember bus = null;
                bus = new BusMember(conn);
                bus.Delete(member);
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
