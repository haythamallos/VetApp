/// <summary>
/// Copyright (c) 2014 Vetapp Inc.  San Diego, California, USA
/// All Rights Reserved
///
/// File:  Logger.cs
/// History
/// ----------------------------------------------------
/// 001	HA	9/3/2014	Created
///
/// </summary>
///
using System;
using System.IO;

namespace Vetapp.Engine.Common
{
    public class LoggerFile
    {
        private string _strFileFullname = null;
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

        public string FileFullname
        {
            get { return _strFileFullname; }
            set { _strFileFullname = value; }
        }

        public LoggerFile(string pStrFileFullname)
        {
            _strFileFullname = pStrFileFullname;
        }

        public void Log(string pStrAction, string pStrMsgText)
        {
            // will append to file specified by filename property.
            // if that file is invalid, will write to a default file
            StreamWriter objWriter = null;

            if (!File.Exists(_strFileFullname))
            {
                objWriter = File.CreateText(FileFullname);
            }
            else
            {
                objWriter = File.AppendText(FileFullname);
            }

            try
            {
                if (pStrMsgText != null)
                {
                    objWriter.WriteLine(ToString(pStrAction, pStrMsgText));
                }

                objWriter.Close();
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
        }

        public string ToString(string pStrAction, string pStrMsg)
        {
            string strReturn = "";

            strReturn = DateTime.UtcNow.ToLongDateString() + " " + DateTime.UtcNow.ToLongTimeString() + ":  " + pStrAction + ":  " + pStrMsg;
            return strReturn;
        }
    }
}