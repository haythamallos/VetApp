/// <summary>
/// Copyright (c) 2014 Vetapp Inc.  San Diego, California, USA
/// All Rights Reserved
///
/// File:  Common.cs
/// History
/// ----------------------------------------------------
/// 001	HA	9/3/2014	Created
///
/// </summary>
///

using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Vetapp.Engine.Common
{
    public class Column
    {
        private string _strColumnName = null;
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        public static readonly string ENTITY_NAME = "ColumnError";
        public static readonly string TAG_COLUMN_NAME = "ColumnName";
        public static readonly string TAG_HAS_ERROR = "HasError";

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

        /// <summary>ColumnName is a Property in the Column Class of type String</summary>
        public string ColumnName
        {
            get { return _strColumnName; }
            set { _strColumnName = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
		public Column()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="pStrColumn">The p STR column.</param>
		public Column(string pStrColumn)
        {
            ColumnName = pStrColumn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="pStrColumn">The p STR column.</param>
        /// <param name="pBlnHasError">if set to <c>true</c> [p BLN has error].</param>
		public Column(string pStrColumn, bool pBlnHasError)
        {
            ColumnName = pStrColumn;
            HasError = pBlnHasError;
        }

        /// <summary>ToString is overridden to display all properties of the Column Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_COLUMN_NAME + ":  " + ColumnName + "\n");
            sbReturn.Append(TAG_HAS_ERROR + ":  " + HasError + "\n");

            return sbReturn.ToString();
        }

        /// <summary>
        /// To the XML.
        /// </summary>
        /// <returns>Column description in XML format.</returns>
		public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<" + ENTITY_NAME + ">\n");
            sbReturn.Append("<" + TAG_COLUMN_NAME + ">" + ColumnName + "</" + TAG_COLUMN_NAME + ">\n");
            sbReturn.Append("<" + TAG_HAS_ERROR + ">" + HasError + "</" + TAG_HAS_ERROR + ">\n");
            sbReturn.Append("</" + ENTITY_NAME + ">\n");

            return sbReturn.ToString();
        }


    }
}