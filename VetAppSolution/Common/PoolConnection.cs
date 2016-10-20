/// <summary>
/// Copyright (c) 2014 Vetapp Inc.  San Diego, California, USA
/// All Rights Reserved
///
/// File:  PoolConnection.cs
/// History
/// ----------------------------------------------------
/// 001	HA	9/3/2014	Created
///
/// </summary>
///
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Vetapp.Engine.Common
{
    public class PoolConnection
    {
        private static string _strPoolName = null;
        private static bool _bInstance = false;

        private static Config _config = null;
        private LoggerFile _oLog = null;
        private string _strLognameText = "PoolConnection";

        public static PoolConnection _poolConnection = null;
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

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

        //public enum DBPerformanceCounters
        //{
        //    NumberOfActiveConnectionPools,
        //    NumberOfActiveConnections,
        //    NumberOfFreeConnections,
        //    NumberOfNonPooledConnections,
        //    NumberOfPooledConnections,
        //    SoftDisconnectsPerSecond,
        //    SoftConnectsPerSecond,
        //    NumberOfReclaimedConnections,
        //    HardConnectsPerSecond,
        //    HardDisconnectsPerSecond,
        //    NumberOfActiveConnectionPoolGroups,
        //    NumberOfInactiveConnectionPoolGroups,
        //    NumberOfInactiveConnectionPools,
        //    NumberOfStasisConnections
        //}

        public static bool Instance
        {
            get { return _bInstance; }
            set { _bInstance = value; }
        }

        public static string PoolName
        {
            get { return _strPoolName; }
            set { _strPoolName = value; }
        }

        private PoolConnection()
        {
            //private constructor disallowing other to create object directly
            _oLog = new LoggerFile(_config.LogDir + System.IO.Path.AltDirectorySeparatorChar + _strLognameText + "-" + PoolName + ".txt");
        }

        public SqlConnection GetConnection()
        {
            SqlConnection conn = null;
            try
            {
                _log("CONNECTION POOL " + PoolName, "Request for connection received.");
                conn = new SqlConnection(_config.ConnectionString);
                _log("CONNECTION POOL " + PoolName, "Successful in creating connection.");
                //PerfMon();
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
            return conn;
        }

        public SqlConnection GetConnection(string pStrConnectionString)
        {
            SqlConnection conn = null;
            try
            {
                _log("CONNECTION POOL " + PoolName, "Request for connection received.");
                conn = new SqlConnection(pStrConnectionString);
                _log("CONNECTION POOL " + PoolName, "Successful in creating connection.");
                //PerfMon();
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
            return conn;
        }

        public bool OpenConnection(SqlConnection pRefConn)
        {
            bool bReturn = true;
            try
            {
                pRefConn.Open();
                _log("CONNECTION POOL " + PoolName, "Successful in opening connection.");
                //PerfMon();
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
                        _log("CONNECTION POOL " + PoolName, "Successful in closing connection.");
                        //PerfMon();
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

        // public methods
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("Connection string:  " + _config.ConnectionString + "\n");
            sbReturn.Append("Pool name:  " + PoolName + "\n");

            return sbReturn.ToString();
        }

        public static PoolConnection GetInstance(Config pConfig, string pStrPoolName)
        {
            if (!Instance)
            {
                PoolName = pStrPoolName;
                _config = pConfig;
                _poolConnection = new PoolConnection();
                Instance = true;
            }
            return _poolConnection;
        }

        //private void PerfMon()
        //{
        //    DotNetPerformanceCounters perfCounter = new DotNetPerformanceCounters();
        //    string InstanceName = Assembly.GetEntryAssembly().GetName().Name + "[" + Process.GetCurrentProcess().Id + "]";
        //    perfCounter.InitializeCounters(InstanceName, typeof(DBPerformanceCounters));
        //    _log("PoolConnection", perfCounter.PrintCounters());
        //   // perfCounter.PrintCounters();
        //}
        //private
        private void _log(string pStrAction, string pStrMsgText)
        {
            if (_config.DoLogInfo)
            {
                lock (_oLog)
                {
                    _oLog.Log(pStrAction, pStrMsgText);
                }
            }
        }
    }
}