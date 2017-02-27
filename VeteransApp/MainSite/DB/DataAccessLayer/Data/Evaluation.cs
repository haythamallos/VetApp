using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Vetapp.Engine.Common;

namespace Vetapp.Engine.DataAccessLayer.Data
{
	/// <summary>
	/// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  Evaluation.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	2/27/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the Evaluation database table.
	/// </summary>
	public class Evaluation
	{
		//Attributes
		/// <summary>EvaluationID Attribute type String</summary>
		private long _lEvaluationID = 0;
		/// <summary>UserID Attribute type String</summary>
		private long _lUserID = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>DateModified Attribute type String</summary>
		private DateTime _dtDateModified = dtNull;
		/// <summary>IsFirsttimeFiling Attribute type String</summary>
		private bool? _bIsFirsttimeFiling = null;
		/// <summary>HasAClaim Attribute type String</summary>
		private bool? _bHasAClaim = null;
		/// <summary>HasActiveAppeal Attribute type String</summary>
		private bool? _bHasActiveAppeal = null;
		/// <summary>CurrentRating Attribute type String</summary>
		private long _lCurrentRating = 0;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class Evaluation and is of type bool</summary>
		public static readonly string ENTITY_NAME = "Evaluation"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "evaluation_id"; //Table id field name
		/// <summary>user_id Database field </summary>
		public static readonly string DB_FIELD_USER_ID = "user_id"; //Table UserID field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>date_modified Database field </summary>
		public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
		/// <summary>is_firsttime_filing Database field </summary>
		public static readonly string DB_FIELD_IS_FIRSTTIME_FILING = "is_firsttime_filing"; //Table IsFirsttimeFiling field name
		/// <summary>has_a_claim Database field </summary>
		public static readonly string DB_FIELD_HAS_A_CLAIM = "has_a_claim"; //Table HasAClaim field name
		/// <summary>has_active_appeal Database field </summary>
		public static readonly string DB_FIELD_HAS_ACTIVE_APPEAL = "has_active_appeal"; //Table HasActiveAppeal field name
		/// <summary>current_rating Database field </summary>
		public static readonly string DB_FIELD_CURRENT_RATING = "current_rating"; //Table CurrentRating field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "EvaluationID"; //Attribute id  name
		/// <summary>UserID Attribute type string</summary>
		public static readonly string TAG_USER_ID = "UserID"; //Table UserID field name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
		/// <summary>IsFirsttimeFiling Attribute type string</summary>
		public static readonly string TAG_IS_FIRSTTIME_FILING = "IsFirsttimeFiling"; //Table IsFirsttimeFiling field name
		/// <summary>HasAClaim Attribute type string</summary>
		public static readonly string TAG_HAS_A_CLAIM = "HasAClaim"; //Table HasAClaim field name
		/// <summary>HasActiveAppeal Attribute type string</summary>
		public static readonly string TAG_HAS_ACTIVE_APPEAL = "HasActiveAppeal"; //Table HasActiveAppeal field name
		/// <summary>CurrentRating Attribute type string</summary>
		public static readonly string TAG_CURRENT_RATING = "CurrentRating"; //Table CurrentRating field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spEvaluationInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spEvaluationUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spEvaluationDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spEvaluationLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spEvaluationExist"; //Exist sp name

		//properties
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
		/// <summary>DateCreated is a Property in the Evaluation Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>DateModified is a Property in the Evaluation Class of type DateTime</summary>
		public DateTime DateModified 
		{
			get{return _dtDateModified;}
			set{_dtDateModified = value;}
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


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class Evaluation and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class Evaluation and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>Evaluation empty constructor</summary>
		public Evaluation()
		{
		}
		/// <summary>Evaluation constructor takes EvaluationID and a SqlConnection</summary>
		public Evaluation(long l, SqlConnection conn) 
		{
			EvaluationID = l;
			try
			{
				sqlLoad(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Evaluation Constructor takes pStrData and Config</summary>
		public Evaluation(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>Evaluation Constructor takes SqlDataReader</summary>
		public Evaluation(SqlDataReader rd)
		{
			sqlParseResultSet(rd);
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

		// public methods
		/// <summary>ToString is overridden to display all properties of the Evaluation Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + EvaluationID.ToString() + "\n");
			sbReturn.Append(TAG_USER_ID + ":  " + UserID + "\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_CREATED + ":\n");
			}
			if (!dtNull.Equals(DateModified))
			{
				sbReturn.Append(TAG_DATE_MODIFIED + ":  " + DateModified.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_MODIFIED + ":\n");
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
			sbReturn.Append("<Evaluation>\n");
			sbReturn.Append("<" + TAG_ID + ">" + EvaluationID + "</" + TAG_ID + ">\n");
			sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + ">" + DateCreated.ToString() + "</" + TAG_DATE_CREATED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + "></" + TAG_DATE_CREATED + ">\n");
			}
			if (!dtNull.Equals(DateModified))
			{
				sbReturn.Append("<" + TAG_DATE_MODIFIED + ">" + DateModified.ToString() + "</" + TAG_DATE_MODIFIED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_MODIFIED + "></" + TAG_DATE_MODIFIED + ">\n");
			}
			sbReturn.Append("<" + TAG_IS_FIRSTTIME_FILING + ">" + IsFirsttimeFiling + "</" + TAG_IS_FIRSTTIME_FILING + ">\n");
			sbReturn.Append("<" + TAG_HAS_A_CLAIM + ">" + HasAClaim + "</" + TAG_HAS_A_CLAIM + ">\n");
			sbReturn.Append("<" + TAG_HAS_ACTIVE_APPEAL + ">" + HasActiveAppeal + "</" + TAG_HAS_ACTIVE_APPEAL + ">\n");
			sbReturn.Append("<" + TAG_CURRENT_RATING + ">" + CurrentRating + "</" + TAG_CURRENT_RATING + ">\n");
			sbReturn.Append("</Evaluation>" + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Parse accepts a string in XML format and parses values</summary>
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
				foreach (XmlNode xNode in xNodes)
				{
					Parse(xNode);
				}
			}
			catch (Exception e) 
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
				xResultNode = xNode.SelectSingleNode(TAG_ID);
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
				xResultNode = xNode.SelectSingleNode(TAG_DATE_CREATED);
				DateCreated = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_DATE_MODIFIED);
				DateModified = DateTime.Parse(xResultNode.InnerText);
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
		/// <summary>Calls sqlLoad() method which gets record from database with evaluation_id equal to the current object's EvaluationID </summary>
		public void Load(SqlConnection conn)
		{
			try
			{
				sqlLoad(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where evaluation_id equal to the current object's EvaluationID </summary>
		public void Update(SqlConnection conn)
		{
			bool bExist = false;
			try
			{
				bExist = Exist(conn);
				if (bExist)
				{
					sqlUpdate(conn);
				}
				else
				{
				}
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}
		/// <summary>Calls sqlInsert() method which inserts a record into the database with current object values</summary>
		public void Save(SqlConnection conn)
		{
			try
			{
				bool bExist = false;

				bExist = Exist(conn);
				if (!bExist)
				{
					sqlInsert(conn);
				}
				else
				{
					sqlUpdate(conn);
				}
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Calls sqlDelete() method which delete's the record from database where where evaluation_id equal to the current object's EvaluationID </summary>
		public void Delete(SqlConnection conn)
		{
			try
			{
				sqlDelete(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}
		/// <summary>Calls sqlExists() returns true if the record exists, false if not </summary>
		public bool Exist(SqlConnection conn)
		{
			bool bReturn = false;
			try
			{
				bReturn = sqlExist(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

			return bReturn;
		}
		/// <summary>Prompt user to enter Property values</summary>
		public void Prompt()
		{
			try 
			{

				Console.WriteLine(Evaluation.TAG_USER_ID + ":  ");
				UserID = (long)Convert.ToInt32(Console.ReadLine());
				try
				{
					Console.WriteLine(Evaluation.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}
				try
				{
					Console.WriteLine(Evaluation.TAG_DATE_MODIFIED + ":  ");
					DateModified = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateModified = new DateTime();
				}

				Console.WriteLine(Evaluation.TAG_IS_FIRSTTIME_FILING + ":  ");
				IsFirsttimeFiling = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Evaluation.TAG_HAS_A_CLAIM + ":  ");
				HasAClaim = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Evaluation.TAG_HAS_ACTIVE_APPEAL + ":  ");
				HasActiveAppeal = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Evaluation.TAG_CURRENT_RATING + ":  ");
				CurrentRating = (long)Convert.ToInt32(Console.ReadLine());

			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}
		
		//protected
		/// <summary>Inserts row of data into the database</summary>
		protected void sqlInsert(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramUserID = null;
			SqlParameter paramDateCreated = null;
			SqlParameter paramIsFirsttimeFiling = null;
			SqlParameter paramHasAClaim = null;
			SqlParameter paramHasActiveAppeal = null;
			SqlParameter paramCurrentRating = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_INSERT_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
			paramUserID.DbType = DbType.Int32;
			paramUserID.Direction = ParameterDirection.Input;

				paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
			paramDateCreated.DbType = DbType.DateTime;
			paramDateCreated.Direction = ParameterDirection.Input;


			paramIsFirsttimeFiling = new SqlParameter("@" + TAG_IS_FIRSTTIME_FILING, IsFirsttimeFiling);
			paramIsFirsttimeFiling.DbType = DbType.Boolean;
			paramIsFirsttimeFiling.Direction = ParameterDirection.Input;

			paramHasAClaim = new SqlParameter("@" + TAG_HAS_A_CLAIM, HasAClaim);
			paramHasAClaim.DbType = DbType.Boolean;
			paramHasAClaim.Direction = ParameterDirection.Input;

			paramHasActiveAppeal = new SqlParameter("@" + TAG_HAS_ACTIVE_APPEAL, HasActiveAppeal);
			paramHasActiveAppeal.DbType = DbType.Boolean;
			paramHasActiveAppeal.Direction = ParameterDirection.Input;

			paramCurrentRating = new SqlParameter("@" + TAG_CURRENT_RATING, CurrentRating);
			paramCurrentRating.DbType = DbType.Int32;
			paramCurrentRating.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramUserID);
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramIsFirsttimeFiling);
			cmd.Parameters.Add(paramHasAClaim);
			cmd.Parameters.Add(paramHasActiveAppeal);
			cmd.Parameters.Add(paramCurrentRating);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			EvaluationID = long.Parse(strTmp);

			// cleanup to help GC
			paramUserID = null;
			paramDateCreated = null;
			paramIsFirsttimeFiling = null;
			paramHasAClaim = null;
			paramHasActiveAppeal = null;
			paramCurrentRating = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramEvaluationID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramEvaluationID = new SqlParameter("@" + TAG_ID, EvaluationID);
			paramEvaluationID.Direction = ParameterDirection.Input;
			paramEvaluationID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramEvaluationID);
			cmd.Parameters.Add(paramCount);
			cmd.ExecuteNonQuery();

			string strTmp;
			int nCount = 0;
			strTmp = cmd.Parameters["@COUNT"].Value.ToString();
			nCount = int.Parse(strTmp);
			if (nCount > 0)
			{
				bExist = true;
			}

			// cleanup
			paramEvaluationID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramEvaluationID = null;
			SqlParameter paramUserID = null;
			SqlParameter paramDateModified = null;
			SqlParameter paramIsFirsttimeFiling = null;
			SqlParameter paramHasAClaim = null;
			SqlParameter paramHasActiveAppeal = null;
			SqlParameter paramCurrentRating = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramEvaluationID = new SqlParameter("@" + TAG_ID, EvaluationID);
			paramEvaluationID.DbType = DbType.Int32;
			paramEvaluationID.Direction = ParameterDirection.Input;


			paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
			paramUserID.DbType = DbType.Int32;
			paramUserID.Direction = ParameterDirection.Input;


				paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
			paramDateModified.DbType = DbType.DateTime;
			paramDateModified.Direction = ParameterDirection.Input;

			paramIsFirsttimeFiling = new SqlParameter("@" + TAG_IS_FIRSTTIME_FILING, IsFirsttimeFiling);
			paramIsFirsttimeFiling.DbType = DbType.Boolean;
			paramIsFirsttimeFiling.Direction = ParameterDirection.Input;

			paramHasAClaim = new SqlParameter("@" + TAG_HAS_A_CLAIM, HasAClaim);
			paramHasAClaim.DbType = DbType.Boolean;
			paramHasAClaim.Direction = ParameterDirection.Input;

			paramHasActiveAppeal = new SqlParameter("@" + TAG_HAS_ACTIVE_APPEAL, HasActiveAppeal);
			paramHasActiveAppeal.DbType = DbType.Boolean;
			paramHasActiveAppeal.Direction = ParameterDirection.Input;

			paramCurrentRating = new SqlParameter("@" + TAG_CURRENT_RATING, CurrentRating);
			paramCurrentRating.DbType = DbType.Int32;
			paramCurrentRating.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramEvaluationID);
			cmd.Parameters.Add(paramUserID);
			cmd.Parameters.Add(paramDateModified);
			cmd.Parameters.Add(paramIsFirsttimeFiling);
			cmd.Parameters.Add(paramHasAClaim);
			cmd.Parameters.Add(paramHasActiveAppeal);
			cmd.Parameters.Add(paramCurrentRating);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			EvaluationID = long.Parse(s);

			// cleanup
			paramEvaluationID = null;
			paramUserID = null;
			paramDateModified = null;
			paramIsFirsttimeFiling = null;
			paramHasAClaim = null;
			paramHasActiveAppeal = null;
			paramCurrentRating = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramEvaluationID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramEvaluationID = new SqlParameter("@" + TAG_ID, EvaluationID);
			paramEvaluationID.DbType = DbType.Int32;
			paramEvaluationID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramEvaluationID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramEvaluationID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramEvaluationID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramEvaluationID = new SqlParameter("@" + TAG_ID, EvaluationID);
			paramEvaluationID.DbType = DbType.Int32;
			paramEvaluationID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramEvaluationID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramEvaluationID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.EvaluationID = long.Parse(rdr[DB_FIELD_ID].ToString());
			try
			{
			this.UserID = Convert.ToInt32(rdr[DB_FIELD_USER_ID].ToString().Trim());
			}
			catch{}
         try
			{
				this.DateCreated = DateTime.Parse(rdr[DB_FIELD_DATE_CREATED].ToString());
			}
			catch 
			{
			}
         try
			{
				this.DateModified = DateTime.Parse(rdr[DB_FIELD_DATE_MODIFIED].ToString());
			}
			catch 
			{
			}
			try
			{
			this.IsFirsttimeFiling = Convert.ToBoolean(rdr[DB_FIELD_IS_FIRSTTIME_FILING].ToString().Trim());
			}
			catch{}
			try
			{
			this.HasAClaim = Convert.ToBoolean(rdr[DB_FIELD_HAS_A_CLAIM].ToString().Trim());
			}
			catch{}
			try
			{
			this.HasActiveAppeal = Convert.ToBoolean(rdr[DB_FIELD_HAS_ACTIVE_APPEAL].ToString().Trim());
			}
			catch{}
			try
			{
			this.CurrentRating = Convert.ToInt32(rdr[DB_FIELD_CURRENT_RATING].ToString().Trim());
			}
			catch{}
		}

	}
}

//END OF Evaluation CLASS FILE
