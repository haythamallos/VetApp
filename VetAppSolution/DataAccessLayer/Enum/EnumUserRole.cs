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
	/// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  EnumUserRole.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	11/28/2016	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumUserRole
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
		public static readonly string ENTITY_NAME = "EnumUserRole"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lUserRoleID = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private string _strCode = null;
		private string _strDescription = null;
		private string _strVisibleCode = null;
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "user_role_id"; //Table id field name
		/// <summary>UserRoleID Attribute type string</summary>
		public static readonly string TAG_USER_ROLE_ID = "UserRoleID"; //Attribute UserRoleID  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>Code Attribute type string</summary>
		public static readonly string TAG_CODE = "Code"; //Attribute Code  name
		/// <summary>Description Attribute type string</summary>
		public static readonly string TAG_DESCRIPTION = "Description"; //Attribute Description  name
		/// <summary>VisibleCode Attribute type string</summary>
		public static readonly string TAG_VISIBLE_CODE = "VisibleCode"; //Attribute VisibleCode  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spUserRoleEnum"; //Enum sp name

		/// <summary>HasError is a Property in the UserRole Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
		/// <summary>UserRoleID is a Property in the UserRole Class of type long</summary>
		public long UserRoleID 
		{
			get{return _lUserRoleID;}
			set{_lUserRoleID = value;}
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
		/// <summary>Code is a Property in the UserRole Class of type String</summary>
		public string Code 
		{
			get{return _strCode;}
			set{_strCode = value;}
		}
		/// <summary>Description is a Property in the UserRole Class of type String</summary>
		public string Description 
		{
			get{return _strDescription;}
			set{_strDescription = value;}
		}
		/// <summary>VisibleCode is a Property in the UserRole Class of type String</summary>
		public string VisibleCode 
		{
			get{return _strVisibleCode;}
			set{_strVisibleCode = value;}
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
		public EnumUserRole()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumUserRole(SqlConnection conn)
		{
			_conn = conn;
		}


		// Implementation of IEnumerator
		/// <summary>Property of type UserRole. Returns the next UserRole in the list</summary>
		private UserRole _nextTransaction
		{
			get
			{
				UserRole o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new UserRole(_rdr);
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

		/// <summary>ToString is overridden to display all properties of the UserRole Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_USER_ROLE_ID + ":  " + UserRoleID.ToString() + "\n");
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
			sbReturn.Append(TAG_CODE + ":  " + Code + "\n");
			sbReturn.Append(TAG_DESCRIPTION + ":  " + Description + "\n");
			sbReturn.Append(TAG_VISIBLE_CODE + ":  " + VisibleCode + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of UserRole</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_USER_ROLE_ID + ">" + UserRoleID + "</" + TAG_USER_ROLE_ID + ">\n");
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
			sbReturn.Append("<" + TAG_CODE + ">" + Code + "</" + TAG_CODE + ">\n");
			sbReturn.Append("<" + TAG_DESCRIPTION + ">" + Description + "</" + TAG_DESCRIPTION + ">\n");
			sbReturn.Append("<" + TAG_VISIBLE_CODE + ">" + VisibleCode + "</" + TAG_VISIBLE_CODE + ">\n");
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
				xResultNode = xNode.SelectSingleNode(TAG_USER_ROLE_ID);
				strTmp = xResultNode.InnerText;
				UserRoleID = (long) Convert.ToInt32(strTmp);
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
				xResultNode = xNode.SelectSingleNode(TAG_CODE);
				Code = xResultNode.InnerText;
				if (Code.Trim().Length == 0)
					Code = null;
			}
			catch  
			{
				Code = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_DESCRIPTION);
				Description = xResultNode.InnerText;
				if (Description.Trim().Length == 0)
					Description = null;
			}
			catch  
			{
				Description = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_VISIBLE_CODE);
				VisibleCode = xResultNode.InnerText;
				if (VisibleCode.Trim().Length == 0)
					VisibleCode = null;
			}
			catch  
			{
				VisibleCode = null;
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


				Console.WriteLine(TAG_CODE + ":  ");
				Code = Console.ReadLine();
				if (Code.Length == 0)
				{
					Code = null;
				}

				Console.WriteLine(TAG_DESCRIPTION + ":  ");
				Description = Console.ReadLine();
				if (Description.Length == 0)
				{
					Description = null;
				}

				Console.WriteLine(TAG_VISIBLE_CODE + ":  ");
				VisibleCode = Console.ReadLine();
				if (VisibleCode.Length == 0)
				{
					VisibleCode = null;
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
			SqlParameter paramUserRoleID = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramCode = null;
			SqlParameter paramDescription = null;
			SqlParameter paramVisibleCode = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramUserRoleID = new SqlParameter("@" + TAG_USER_ROLE_ID, UserRoleID);
				sbLog.Append(TAG_USER_ROLE_ID + "=" + UserRoleID + "\n");
				paramUserRoleID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramUserRoleID);

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

			// Setup the code text param
			if ( Code != null )
			{
				paramCode = new SqlParameter("@" + TAG_CODE, Code);
				sbLog.Append(TAG_CODE + "=" + Code + "\n");
			}
			else
			{
				paramCode = new SqlParameter("@" + TAG_CODE, DBNull.Value);
			}
			paramCode.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramCode);

			// Setup the description text param
			if ( Description != null )
			{
				paramDescription = new SqlParameter("@" + TAG_DESCRIPTION, Description);
				sbLog.Append(TAG_DESCRIPTION + "=" + Description + "\n");
			}
			else
			{
				paramDescription = new SqlParameter("@" + TAG_DESCRIPTION, DBNull.Value);
			}
			paramDescription.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramDescription);

			// Setup the visible code text param
			if ( VisibleCode != null )
			{
				paramVisibleCode = new SqlParameter("@" + TAG_VISIBLE_CODE, VisibleCode);
				sbLog.Append(TAG_VISIBLE_CODE + "=" + VisibleCode + "\n");
			}
			else
			{
				paramVisibleCode = new SqlParameter("@" + TAG_VISIBLE_CODE, DBNull.Value);
			}
			paramVisibleCode.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramVisibleCode);

		}

	}
}

