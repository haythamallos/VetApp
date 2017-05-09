using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Enumeration;
using Vetapp.Engine.DataAccessLayer.Data;

namespace Vetapp.Engine.BusinessAccessLayer
{

	/// <summary>
	/// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  BusJctUserUser.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	5/9/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for JctUserUser objects.
	/// </summary>
	public class BusJctUserUser
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_DATE_MODIFIED = "";
		private const String REGEXP_ISVALID_USER_SOURCE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_USER_MEMBER_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusJctUserUser constructor takes SqlConnection object</summary>
		public BusJctUserUser()
		{
		}
		/// <summary>BusJctUserUser constructor takes SqlConnection object</summary>
		public BusJctUserUser(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all JctUserUser objects
	///     <remarks>   
	///         No parameters. Returns all JctUserUser objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all JctUserUser objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), 0, 0));
	}

	 /// <summary>
	///     Gets all JctUserUser objects
	///     <remarks>   
	///         No parameters. Returns all JctUserUser objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all JctUserUser objects</retvalue>
	/// </summary>
	public ArrayList Get(long lJctUserUserID)
	{
		return (Get(lJctUserUserID , new DateTime(), new DateTime(), new DateTime(), new DateTime(), 0, 0));
	}

        /// <summary>
        ///     Gets all JctUserUser objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">JctUserUser to be returned</param>
        ///     <retvalue>ArrayList containing JctUserUser object</retvalue>
        /// </summary>
	public ArrayList Get(JctUserUser o)
	{	
		return (Get( o.JctUserUserID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.UserSourceID, o.UserMemberID	));
	}

        /// <summary>
        ///     Gets all JctUserUser objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">JctUserUser to be returned</param>
        ///     <retvalue>ArrayList containing JctUserUser object</retvalue>
        /// </summary>
	public ArrayList Get(EnumJctUserUser o)
	{	
		return (Get( o.JctUserUserID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.UserSourceID, o.UserMemberID	));
	}

		/// <summary>
		///     Gets all JctUserUser objects
		///     <remarks>   
		///         Returns JctUserUser objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing JctUserUser object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngJctUserUserID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, long pLngUserSourceID, long pLngUserMemberID)
		{
			JctUserUser data = null;
			_arrlstEntities = new ArrayList();
			EnumJctUserUser enumJctUserUser = new EnumJctUserUser(_conn);
			 enumJctUserUser.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumJctUserUser.SP_ENUM_NAME;
			enumJctUserUser.JctUserUserID = pLngJctUserUserID;
			enumJctUserUser.BeginDateCreated = pDtBeginDateCreated;
			enumJctUserUser.EndDateCreated = pDtEndDateCreated;
			enumJctUserUser.BeginDateModified = pDtBeginDateModified;
			enumJctUserUser.EndDateModified = pDtEndDateModified;
			enumJctUserUser.UserSourceID = pLngUserSourceID;
			enumJctUserUser.UserMemberID = pLngUserMemberID;
			enumJctUserUser.EnumData();
			while (enumJctUserUser.hasMoreElements())
			{
				data = (JctUserUser) enumJctUserUser.nextElement();
				_arrlstEntities.Add(data);
			}
			enumJctUserUser = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves JctUserUser object to database
        ///     <param name="o">JctUserUser to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(JctUserUser o)
		{
			if ( o != null )
			{
				o.Save(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Modify JctUserUser object to database
		///     <param name="o">JctUserUser to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(JctUserUser o)
		{
			if ( o != null )
			{
				o.Update(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Modify JctUserUser object to database
		///     <param name="o">JctUserUser to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(JctUserUser o)
		{
			if ( o != null )
			{
				o.Load(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Modify JctUserUser object to database
		///     <param name="o">JctUserUser to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(JctUserUser o)
		{
			if ( o != null )
			{
				o.Delete(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Exist JctUserUser object to database
		///     <param name="o">JctUserUser to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(JctUserUser o)
		{
			bool bExist = false;
			if ( o != null )
			{
				bExist = o.Exist(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}

			return bExist;
		}
		/// <summary>Property as to whether or not the object has an Error</summary>
		public bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Property as to whether or not the object has invalid columns</summary>
		public bool HasInvalid 
		{
			get{return _hasInvalid;}
		}
		/// <summary>Property which returns all column error in an ArrayList</summary>
		public ArrayList ColumnErrors
		{
			get{return _arrlstColumnErrors;}
		}
		/// <summary>Property returns an ArrayList containing JctUserUser objects</summary>
		public ArrayList JctUserUsers 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					JctUserUser data = null;
					_arrlstEntities = new ArrayList();
					EnumJctUserUser enumJctUserUser = new EnumJctUserUser(_conn);
					enumJctUserUser.EnumData();
					while (enumJctUserUser.hasMoreElements())
					{
						data = (JctUserUser) enumJctUserUser.nextElement();
						_arrlstEntities.Add(data);
					}
					enumJctUserUser = null;
					ArrayList.ReadOnly(_arrlstEntities);
				}
				return _arrlstEntities;
			}
		}

		/// <summary>
		///     Checks to make sure all values are valid
		///     <remarks>   
		///         Calls "IsValid" method for each property in ocject
		///     </remarks>   
		///     <retvalue>true if object has valid entries, false otherwise</retvalue>
		/// </summary>
		public bool IsValid(JctUserUser pRefJctUserUser)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidJctUserUserID(pRefJctUserUser.JctUserUserID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefJctUserUser.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateModified(pRefJctUserUser.DateModified);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidUserSourceID(pRefJctUserUser.UserSourceID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidUserMemberID(pRefJctUserUser.UserMemberID);
			if (!isValidTmp)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidJctUserUserID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = JctUserUser.DB_FIELD_ID;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidDateCreated(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_DATE_CREATED)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = JctUserUser.DB_FIELD_DATE_CREATED;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidDateModified(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_DATE_MODIFIED)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = JctUserUser.DB_FIELD_DATE_MODIFIED;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidUserSourceID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_USER_SOURCE_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = JctUserUser.DB_FIELD_USER_SOURCE_ID;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidUserMemberID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_USER_MEMBER_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = JctUserUser.DB_FIELD_USER_MEMBER_ID;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
