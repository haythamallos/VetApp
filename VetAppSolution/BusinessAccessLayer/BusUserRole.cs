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
	/// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  BusUserRole.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	11/28/2016	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for UserRole objects.
	/// </summary>
	public class BusUserRole
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_CODE = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_DESCRIPTION = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_VISIBLE_CODE = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusUserRole constructor takes SqlConnection object</summary>
		public BusUserRole()
		{
		}
		/// <summary>BusUserRole constructor takes SqlConnection object</summary>
		public BusUserRole(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all UserRole objects
	///     <remarks>   
	///         No parameters. Returns all UserRole objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all UserRole objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), null, null, null));
	}

	 /// <summary>
	///     Gets all UserRole objects
	///     <remarks>   
	///         No parameters. Returns all UserRole objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all UserRole objects</retvalue>
	/// </summary>
	public ArrayList Get(long lUserRoleID)
	{
		return (Get(lUserRoleID , new DateTime(), new DateTime(), null, null, null));
	}

        /// <summary>
        ///     Gets all UserRole objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">UserRole to be returned</param>
        ///     <retvalue>ArrayList containing UserRole object</retvalue>
        /// </summary>
	public ArrayList Get(UserRole o)
	{	
		return (Get( o.UserRoleID, o.DateCreated, o.DateCreated, o.Code, o.Description, o.VisibleCode	));
	}

        /// <summary>
        ///     Gets all UserRole objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">UserRole to be returned</param>
        ///     <retvalue>ArrayList containing UserRole object</retvalue>
        /// </summary>
	public ArrayList Get(EnumUserRole o)
	{	
		return (Get( o.UserRoleID, o.BeginDateCreated, o.EndDateCreated, o.Code, o.Description, o.VisibleCode	));
	}

		/// <summary>
		///     Gets all UserRole objects
		///     <remarks>   
		///         Returns UserRole objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing UserRole object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngUserRoleID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, string pStrCode, string pStrDescription, string pStrVisibleCode)
		{
			UserRole data = null;
			_arrlstEntities = new ArrayList();
			EnumUserRole enumUserRole = new EnumUserRole(_conn);
			 enumUserRole.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumUserRole.SP_ENUM_NAME;
			enumUserRole.UserRoleID = pLngUserRoleID;
			enumUserRole.BeginDateCreated = pDtBeginDateCreated;
			enumUserRole.EndDateCreated = pDtEndDateCreated;
			enumUserRole.Code = pStrCode;
			enumUserRole.Description = pStrDescription;
			enumUserRole.VisibleCode = pStrVisibleCode;
			enumUserRole.EnumData();
			while (enumUserRole.hasMoreElements())
			{
				data = (UserRole) enumUserRole.nextElement();
				_arrlstEntities.Add(data);
			}
			enumUserRole = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves UserRole object to database
        ///     <param name="o">UserRole to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(UserRole o)
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
		///     Modify UserRole object to database
		///     <param name="o">UserRole to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(UserRole o)
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
		///     Modify UserRole object to database
		///     <param name="o">UserRole to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(UserRole o)
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
		///     Modify UserRole object to database
		///     <param name="o">UserRole to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(UserRole o)
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
		///     Exist UserRole object to database
		///     <param name="o">UserRole to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(UserRole o)
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
		/// <summary>Property returns an ArrayList containing UserRole objects</summary>
		public ArrayList UserRoles 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					UserRole data = null;
					_arrlstEntities = new ArrayList();
					EnumUserRole enumUserRole = new EnumUserRole(_conn);
					enumUserRole.EnumData();
					while (enumUserRole.hasMoreElements())
					{
						data = (UserRole) enumUserRole.nextElement();
						_arrlstEntities.Add(data);
					}
					enumUserRole = null;
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
		public bool IsValid(UserRole pRefUserRole)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidUserRoleID(pRefUserRole.UserRoleID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefUserRole.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidCode(pRefUserRole.Code);
			if (!isValidTmp && pRefUserRole.Code != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidDescription(pRefUserRole.Description);
			if (!isValidTmp && pRefUserRole.Description != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidVisibleCode(pRefUserRole.VisibleCode);
			if (!isValidTmp && pRefUserRole.VisibleCode != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidUserRoleID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = UserRole.DB_FIELD_ID;
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
				clm.ColumnName = UserRole.DB_FIELD_DATE_CREATED;
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
		public bool IsValidCode(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_CODE)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = UserRole.DB_FIELD_CODE;
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
		public bool IsValidDescription(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_DESCRIPTION)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = UserRole.DB_FIELD_DESCRIPTION;
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
		public bool IsValidVisibleCode(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_VISIBLE_CODE)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = UserRole.DB_FIELD_VISIBLE_CODE;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
