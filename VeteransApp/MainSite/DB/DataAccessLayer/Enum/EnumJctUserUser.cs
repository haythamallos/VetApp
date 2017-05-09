using System;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Data;

using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Data;

namespace Vetapp.Engine.DataAccessLayer.Enumeration
{

	/// <summary>
	/// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  EnumJctUserUser.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	5/9/2017	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumJctUserUser
	{
		private bool _hasAny = false;
		private bool _hasMore = false;
		private bool _bSetup = false;

		private SqlCommand _cmd = null;
		private SqlDataReader _rdr = null;
		private SqlConnection _conn = null;
		
		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private int _nCount = 0;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>Attribute of type string</summary>
		public static readonly string ENTITY_NAME = "EnumJctUserUser"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lJctUserUserID = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private DateTime _dtBeginDateModified = new DateTime();
		private DateTime _dtEndDateModified = new DateTime();
		private long _lUserSourceID = 0;
		private long _lUserMemberID = 0;
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "jct_user_user_id"; //Table id field name
		/// <summary>JctUserUserID Attribute type string</summary>
		public static readonly string TAG_JCT_USER_USER_ID = "JctUserUserID"; //Attribute JctUserUserID  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
		/// <summary>EndDateModified Attribute type string</summary>
		public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
		/// <summary>UserSourceID Attribute type string</summary>
		public static readonly string TAG_USER_SOURCE_ID = "UserSourceID"; //Attribute UserSourceID  name
		/// <summary>UserMemberID Attribute type string</summary>
		public static readonly string TAG_USER_MEMBER_ID = "UserMemberID"; //Attribute UserMemberID  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spJctUserUserEnum"; //Enum sp name

		/// <summary>HasError is a Property in the JctUserUser Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
		/// <summary>JctUserUserID is a Property in the JctUserUser Class of type long</summary>
		public long JctUserUserID 
		{
			get{return _lJctUserUserID;}
			set{_lJctUserUserID = value;}
		}
		/// <summary>Property DateCreated. Type: DateTime</summary>
		public DateTime BeginDateCreated
		{
			get{return _dtBeginDateCreated;}
			set{_dtBeginDateCreated = value;}
		}
		/// <summary>Property DateCreated. Type: DateTime</summary>
		public DateTime EndDateCreated
		{
			get{return _dtEndDateCreated;}
			set{_dtEndDateCreated = value;}
		}
		/// <summary>Property DateModified. Type: DateTime</summary>
		public DateTime BeginDateModified
		{
			get{return _dtBeginDateModified;}
			set{_dtBeginDateModified = value;}
		}
		/// <summary>Property DateModified. Type: DateTime</summary>
		public DateTime EndDateModified
		{
			get{return _dtEndDateModified;}
			set{_dtEndDateModified = value;}
		}
		/// <summary>UserSourceID is a Property in the JctUserUser Class of type long</summary>
		public long UserSourceID 
		{
			get{return _lUserSourceID;}
			set{_lUserSourceID = value;}
		}
		/// <summary>UserMemberID is a Property in the JctUserUser Class of type long</summary>
		public long UserMemberID 
		{
			get{return _lUserMemberID;}
			set{_lUserMemberID = value;}
		}

		/// <summary>Count Property. Type: int</summary>
		public int Count 
		{
			get
			{
				_bSetup = true;
				// if necessary, close the old reader
				if ( (_cmd != null) || (_rdr != null) )
				{
					Close();
				}
				_cmd = new SqlCommand(SP_ENUM_NAME, _conn);
				_cmd.CommandType = CommandType.StoredProcedure;
				_setupEnumParams();
				_setupCountParams();
				_cmd.Connection = _conn;
				_cmd.ExecuteNonQuery();
				try
				{
					string strTmp;
					strTmp = _cmd.Parameters[PARAM_COUNT].Value.ToString();
					_nCount = int.Parse(strTmp);
				}
				catch 
				{
					_nCount = 0;
				}
				return _nCount;			}
		}

		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumJctUserUser()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumJctUserUser(SqlConnection conn)
		{
			_conn = conn;
		}


		// Implementation of IEnumerator
		/// <summary>Property of type JctUserUser. Returns the next JctUserUser in the list</summary>
		private JctUserUser _nextTransaction
		{
			get
			{
				JctUserUser o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new JctUserUser(_rdr);
					_hasMore = _rdr.Read();
					if (!_hasMore)
					{
						Close();
					}
				}
				return o;
			}
		}

		/// <summary>Enumerates the Data</summary>
		public void EnumData()
		{
			if (!_bSetup)
			{
				_bSetup = true;
				// if necessary, close the old reader
				if ( (_cmd != null) || (_rdr != null) )
				{
					Close();
				}
				_cmd = new SqlCommand(SP_ENUM_NAME, _conn);
				_cmd.CommandType = CommandType.StoredProcedure;
				_setupEnumParams();
				_cmd.Connection = _conn;
				_rdr = _cmd.ExecuteReader();
				_hasAny = _rdr.Read();
				_hasMore = _hasAny;
			}
		}


		/// <summary>returns the next element in the enumeration</summary>
		public object nextElement()
		{
			try
			{
				return _nextTransaction;
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
				return null;
			}
		}

		/// <summary>Returns whether or not more elements exist</summary>
		public bool hasMoreElements()
		{
			try
			{
				if (_bSetup)
				{
					EnumData();
				}
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

			return _hasMore;
		}

		/// <summary>Closes the datareader</summary>
		public void Close()
		{
			try
			{
				if ( _rdr != null )
				{
					_rdr.Dispose();
				}
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
			_rdr = null;
			_cmd = null;
		}

		/// <summary>ToString is overridden to display all properties of the JctUserUser Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_JCT_USER_USER_ID + ":  " + JctUserUserID.ToString() + "\n");
			if (!dtNull.Equals(BeginDateCreated))
			{
				sbReturn.Append(TAG_BEGIN_DATE_CREATED + ":  " + BeginDateCreated.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_BEGIN_DATE_CREATED + ":\n");
			}
			if (!dtNull.Equals(EndDateCreated))
			{
				sbReturn.Append(TAG_END_DATE_CREATED + ":  " + EndDateCreated.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_END_DATE_CREATED + ":\n");
			}
			if (!dtNull.Equals(BeginDateModified))
			{
				sbReturn.Append(TAG_BEGIN_DATE_MODIFIED + ":  " + BeginDateModified.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_BEGIN_DATE_MODIFIED + ":\n");
			}
			if (!dtNull.Equals(EndDateModified))
			{
				sbReturn.Append(TAG_END_DATE_MODIFIED + ":  " + EndDateModified.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_END_DATE_MODIFIED + ":\n");
			}
			sbReturn.Append(TAG_USER_SOURCE_ID + ":  " + UserSourceID + "\n");
			sbReturn.Append(TAG_USER_MEMBER_ID + ":  " + UserMemberID + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of JctUserUser</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_JCT_USER_USER_ID + ">" + JctUserUserID + "</" + TAG_JCT_USER_USER_ID + ">\n");
			if (!dtNull.Equals(BeginDateCreated))
			{
				sbReturn.Append("<" + TAG_BEGIN_DATE_CREATED + ">" + BeginDateCreated.ToString() + "</" + TAG_BEGIN_DATE_CREATED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_BEGIN_DATE_CREATED + "></" + TAG_BEGIN_DATE_CREATED + ">\n");
			}
			if (!dtNull.Equals(EndDateCreated))
			{
				sbReturn.Append("<" + TAG_END_DATE_CREATED + ">" + EndDateCreated.ToString() + "</" + TAG_END_DATE_CREATED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_END_DATE_CREATED + "></" + TAG_END_DATE_CREATED + ">\n");
			}
			if (!dtNull.Equals(BeginDateModified))
			{
				sbReturn.Append("<" + TAG_BEGIN_DATE_MODIFIED + ">" + BeginDateModified.ToString() + "</" + TAG_BEGIN_DATE_MODIFIED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_BEGIN_DATE_MODIFIED + "></" + TAG_BEGIN_DATE_MODIFIED + ">\n");
			}
			if (!dtNull.Equals(EndDateModified))
			{
				sbReturn.Append("<" + TAG_END_DATE_MODIFIED + ">" + EndDateModified.ToString() + "</" + TAG_END_DATE_MODIFIED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_END_DATE_MODIFIED + "></" + TAG_END_DATE_MODIFIED + ">\n");
			}
			sbReturn.Append("<" + TAG_USER_SOURCE_ID + ">" + UserSourceID + "</" + TAG_USER_SOURCE_ID + ">\n");
			sbReturn.Append("<" + TAG_USER_MEMBER_ID + ">" + UserMemberID + "</" + TAG_USER_MEMBER_ID + ">\n");
			sbReturn.Append("</" + ENTITY_NAME + ">" + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Parse XML string and assign values to object</summary>
		public void Parse(string pStrXml)
		{
			try
			{
				XmlDocument xmlDoc = null;
				string strXPath = null;
				XmlNodeList xNodes = null;

				xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(pStrXml);

				// get the element
				strXPath = "//" + ENTITY_NAME;
				xNodes = xmlDoc.SelectNodes(strXPath);
				if ( xNodes.Count > 0 )
				{
					Parse(xNodes.Item(0));
				}
			}
			catch 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}		
		/// <summary>Parse accepts an XmlNode and parses values</summary>
		public void Parse(XmlNode xNode)
		{
			XmlNode xResultNode = null;
			string strTmp = null;

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_JCT_USER_USER_ID);
				strTmp = xResultNode.InnerText;
				JctUserUserID = (long) Convert.ToInt32(strTmp);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_DATE_CREATED);
				BeginDateCreated = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_END_DATE_CREATED);
				EndDateCreated = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_DATE_MODIFIED);
				BeginDateModified = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_END_DATE_MODIFIED);
				EndDateModified = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_USER_SOURCE_ID);
				UserSourceID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			UserSourceID = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_USER_MEMBER_ID);
				UserMemberID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			UserMemberID = 0;
			}
		}
		/// <summary>Prompt for values</summary>
		public void Prompt()
		{
			try 
			{
				Console.WriteLine(TAG_BEGIN_DATE_CREATED + ":  ");
				try
				{
					string s = Console.ReadLine();
					BeginDateCreated = DateTime.Parse(s);
				}
				catch 
				{
					BeginDateCreated = new DateTime();
				}

				Console.WriteLine(TAG_END_DATE_CREATED + ":  ");
				try
				{
					string s = Console.ReadLine();
					EndDateCreated = DateTime.Parse(s);
				}
				catch  
				{
					EndDateCreated = new DateTime();
				}

				Console.WriteLine(TAG_BEGIN_DATE_MODIFIED + ":  ");
				try
				{
					string s = Console.ReadLine();
					BeginDateModified = DateTime.Parse(s);
				}
				catch 
				{
					BeginDateModified = new DateTime();
				}

				Console.WriteLine(TAG_END_DATE_MODIFIED + ":  ");
				try
				{
					string s = Console.ReadLine();
					EndDateModified = DateTime.Parse(s);
				}
				catch  
				{
					EndDateModified = new DateTime();
				}

				Console.WriteLine(TAG_USER_SOURCE_ID + ":  ");
				try
				{
					UserSourceID = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					UserSourceID = 0;
				}

				Console.WriteLine(TAG_USER_MEMBER_ID + ":  ");
				try
				{
					UserMemberID = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					UserMemberID = 0;
				}


			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
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
			if (! disposing)
				return; // we're being collected, so let the GC take care of this object
		}
		private void _setupCountParams()
		{
			SqlParameter paramCount = null;
			paramCount = new SqlParameter();
			paramCount.ParameterName = PARAM_COUNT;
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			_cmd.Parameters.Add(paramCount);
		}
		private void _setupEnumParams()
		{
			System.Text.StringBuilder sbLog = null;
			SqlParameter paramJctUserUserID = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramBeginDateModified = null;
			SqlParameter paramEndDateModified = null;
			SqlParameter paramUserSourceID = null;
			SqlParameter paramUserMemberID = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramJctUserUserID = new SqlParameter("@" + TAG_JCT_USER_USER_ID, JctUserUserID);
				sbLog.Append(TAG_JCT_USER_USER_ID + "=" + JctUserUserID + "\n");
				paramJctUserUserID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramJctUserUserID);

			// Setup the date created param
			if (!dtNull.Equals(BeginDateCreated))
			{
				paramBeginDateCreated = new SqlParameter("@" + TAG_BEGIN_DATE_CREATED, BeginDateCreated);
			}
			else
			{
				paramBeginDateCreated = new SqlParameter("@" + TAG_BEGIN_DATE_CREATED, DBNull.Value);
			}
			paramBeginDateCreated.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramBeginDateCreated);

			if (!dtNull.Equals(EndDateCreated))
			{
				paramEndDateCreated = new SqlParameter("@" + TAG_END_DATE_CREATED, EndDateCreated);
			}
			else
			{
				paramEndDateCreated = new SqlParameter("@" + TAG_END_DATE_CREATED, DBNull.Value);
			}
			paramEndDateCreated.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramEndDateCreated);

			// Setup the date modified param
			if (!dtNull.Equals(BeginDateModified))
			{
				paramBeginDateModified = new SqlParameter("@" + TAG_BEGIN_DATE_MODIFIED, BeginDateModified);
			}
			else
			{
				paramBeginDateModified = new SqlParameter("@" + TAG_BEGIN_DATE_MODIFIED, DBNull.Value);
			}
			paramBeginDateModified.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramBeginDateModified);

			if (!dtNull.Equals(EndDateModified))
			{
				paramEndDateModified = new SqlParameter("@" + TAG_END_DATE_MODIFIED, EndDateModified);
			}
			else
			{
				paramEndDateModified = new SqlParameter("@" + TAG_END_DATE_MODIFIED, DBNull.Value);
			}
			paramEndDateModified.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramEndDateModified);

				paramUserSourceID = new SqlParameter("@" + TAG_USER_SOURCE_ID, UserSourceID);
				sbLog.Append(TAG_USER_SOURCE_ID + "=" + UserSourceID + "\n");
				paramUserSourceID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramUserSourceID);
				paramUserMemberID = new SqlParameter("@" + TAG_USER_MEMBER_ID, UserMemberID);
				sbLog.Append(TAG_USER_MEMBER_ID + "=" + UserMemberID + "\n");
				paramUserMemberID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramUserMemberID);
		}

	}
}

