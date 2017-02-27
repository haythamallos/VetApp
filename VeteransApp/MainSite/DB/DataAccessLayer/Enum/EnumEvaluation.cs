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
	/// File:  EnumEvaluation.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	2/27/2017	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumEvaluation
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
		public static readonly string ENTITY_NAME = "EnumEvaluation"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lEvaluationID = 0;
		private long _lUserID = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private DateTime _dtBeginDateModified = new DateTime();
		private DateTime _dtEndDateModified = new DateTime();
		private bool? _bIsFirsttimeFiling = null;
		private bool? _bHasAClaim = null;
		private bool? _bHasActiveAppeal = null;
		private long _lCurrentRating = 0;
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "evaluation_id"; //Table id field name
		/// <summary>EvaluationID Attribute type string</summary>
		public static readonly string TAG_EVALUATION_ID = "EvaluationID"; //Attribute EvaluationID  name
		/// <summary>UserID Attribute type string</summary>
		public static readonly string TAG_USER_ID = "UserID"; //Attribute UserID  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
		/// <summary>EndDateModified Attribute type string</summary>
		public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
		/// <summary>IsFirsttimeFiling Attribute type string</summary>
		public static readonly string TAG_IS_FIRSTTIME_FILING = "IsFirsttimeFiling"; //Attribute IsFirsttimeFiling  name
		/// <summary>HasAClaim Attribute type string</summary>
		public static readonly string TAG_HAS_A_CLAIM = "HasAClaim"; //Attribute HasAClaim  name
		/// <summary>HasActiveAppeal Attribute type string</summary>
		public static readonly string TAG_HAS_ACTIVE_APPEAL = "HasActiveAppeal"; //Attribute HasActiveAppeal  name
		/// <summary>CurrentRating Attribute type string</summary>
		public static readonly string TAG_CURRENT_RATING = "CurrentRating"; //Attribute CurrentRating  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spEvaluationEnum"; //Enum sp name

		/// <summary>HasError is a Property in the Evaluation Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
		/// <summary>EvaluationID is a Property in the Evaluation Class of type long</summary>
		public long EvaluationID 
		{
			get{return _lEvaluationID;}
			set{_lEvaluationID = value;}
		}
		/// <summary>UserID is a Property in the Evaluation Class of type long</summary>
		public long UserID 
		{
			get{return _lUserID;}
			set{_lUserID = value;}
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
		/// <summary>IsFirsttimeFiling is a Property in the Evaluation Class of type bool</summary>
		public bool? IsFirsttimeFiling 
		{
			get{return _bIsFirsttimeFiling;}
			set{_bIsFirsttimeFiling = value;}
		}
		/// <summary>HasAClaim is a Property in the Evaluation Class of type bool</summary>
		public bool? HasAClaim 
		{
			get{return _bHasAClaim;}
			set{_bHasAClaim = value;}
		}
		/// <summary>HasActiveAppeal is a Property in the Evaluation Class of type bool</summary>
		public bool? HasActiveAppeal 
		{
			get{return _bHasActiveAppeal;}
			set{_bHasActiveAppeal = value;}
		}
		/// <summary>CurrentRating is a Property in the Evaluation Class of type long</summary>
		public long CurrentRating 
		{
			get{return _lCurrentRating;}
			set{_lCurrentRating = value;}
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
		public EnumEvaluation()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumEvaluation(SqlConnection conn)
		{
			_conn = conn;
		}


		// Implementation of IEnumerator
		/// <summary>Property of type Evaluation. Returns the next Evaluation in the list</summary>
		private Evaluation _nextTransaction
		{
			get
			{
				Evaluation o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new Evaluation(_rdr);
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

		/// <summary>ToString is overridden to display all properties of the Evaluation Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_EVALUATION_ID + ":  " + EvaluationID.ToString() + "\n");
			sbReturn.Append(TAG_USER_ID + ":  " + UserID + "\n");
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
			sbReturn.Append(TAG_IS_FIRSTTIME_FILING + ":  " + IsFirsttimeFiling + "\n");
			sbReturn.Append(TAG_HAS_A_CLAIM + ":  " + HasAClaim + "\n");
			sbReturn.Append(TAG_HAS_ACTIVE_APPEAL + ":  " + HasActiveAppeal + "\n");
			sbReturn.Append(TAG_CURRENT_RATING + ":  " + CurrentRating + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Evaluation</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_EVALUATION_ID + ">" + EvaluationID + "</" + TAG_EVALUATION_ID + ">\n");
			sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
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
			sbReturn.Append("<" + TAG_IS_FIRSTTIME_FILING + ">" + IsFirsttimeFiling + "</" + TAG_IS_FIRSTTIME_FILING + ">\n");
			sbReturn.Append("<" + TAG_HAS_A_CLAIM + ">" + HasAClaim + "</" + TAG_HAS_A_CLAIM + ">\n");
			sbReturn.Append("<" + TAG_HAS_ACTIVE_APPEAL + ">" + HasActiveAppeal + "</" + TAG_HAS_ACTIVE_APPEAL + ">\n");
			sbReturn.Append("<" + TAG_CURRENT_RATING + ">" + CurrentRating + "</" + TAG_CURRENT_RATING + ">\n");
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
				xResultNode = xNode.SelectSingleNode(TAG_EVALUATION_ID);
				strTmp = xResultNode.InnerText;
				EvaluationID = (long) Convert.ToInt32(strTmp);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_USER_ID);
				UserID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			UserID = 0;
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
				xResultNode = xNode.SelectSingleNode(TAG_IS_FIRSTTIME_FILING);
				IsFirsttimeFiling = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			IsFirsttimeFiling = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_HAS_A_CLAIM);
				HasAClaim = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			HasAClaim = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_HAS_ACTIVE_APPEAL);
				HasActiveAppeal = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			HasActiveAppeal = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_CURRENT_RATING);
				CurrentRating = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			CurrentRating = 0;
			}
		}
		/// <summary>Prompt for values</summary>
		public void Prompt()
		{
			try 
			{
				Console.WriteLine(TAG_USER_ID + ":  ");
				try
				{
					UserID = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					UserID = 0;
				}

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

				Console.WriteLine(TAG_IS_FIRSTTIME_FILING + ":  ");
				try
				{
					IsFirsttimeFiling = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					IsFirsttimeFiling = false;
				}

				Console.WriteLine(TAG_HAS_A_CLAIM + ":  ");
				try
				{
					HasAClaim = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					HasAClaim = false;
				}

				Console.WriteLine(TAG_HAS_ACTIVE_APPEAL + ":  ");
				try
				{
					HasActiveAppeal = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					HasActiveAppeal = false;
				}

				Console.WriteLine(TAG_CURRENT_RATING + ":  ");
				try
				{
					CurrentRating = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					CurrentRating = 0;
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
			SqlParameter paramEvaluationID = null;
			SqlParameter paramUserID = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramBeginDateModified = null;
			SqlParameter paramEndDateModified = null;
			SqlParameter paramIsFirsttimeFiling = null;
			SqlParameter paramHasAClaim = null;
			SqlParameter paramHasActiveAppeal = null;
			SqlParameter paramCurrentRating = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramEvaluationID = new SqlParameter("@" + TAG_EVALUATION_ID, EvaluationID);
				sbLog.Append(TAG_EVALUATION_ID + "=" + EvaluationID + "\n");
				paramEvaluationID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramEvaluationID);

				paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
				sbLog.Append(TAG_USER_ID + "=" + UserID + "\n");
				paramUserID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramUserID);
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

				paramIsFirsttimeFiling = new SqlParameter("@" + TAG_IS_FIRSTTIME_FILING, IsFirsttimeFiling);
				sbLog.Append(TAG_IS_FIRSTTIME_FILING + "=" + IsFirsttimeFiling + "\n");
				paramIsFirsttimeFiling.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramIsFirsttimeFiling);
				paramHasAClaim = new SqlParameter("@" + TAG_HAS_A_CLAIM, HasAClaim);
				sbLog.Append(TAG_HAS_A_CLAIM + "=" + HasAClaim + "\n");
				paramHasAClaim.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramHasAClaim);
				paramHasActiveAppeal = new SqlParameter("@" + TAG_HAS_ACTIVE_APPEAL, HasActiveAppeal);
				sbLog.Append(TAG_HAS_ACTIVE_APPEAL + "=" + HasActiveAppeal + "\n");
				paramHasActiveAppeal.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramHasActiveAppeal);
				paramCurrentRating = new SqlParameter("@" + TAG_CURRENT_RATING, CurrentRating);
				sbLog.Append(TAG_CURRENT_RATING + "=" + CurrentRating + "\n");
				paramCurrentRating.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramCurrentRating);

		}

	}
}

