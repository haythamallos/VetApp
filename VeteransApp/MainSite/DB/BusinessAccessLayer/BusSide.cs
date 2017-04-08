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
	/// File:  BusSide.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	3/23/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for Side objects.
	/// </summary>
	public class BusSide
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


		/// <summary>BusSide constructor takes SqlConnection object</summary>
		public BusSide()
		{
		}
		/// <summary>BusSide constructor takes SqlConnection object</summary>
		public BusSide(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all Side objects
	///     <remarks>   
	///         No parameters. Returns all Side objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Side objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), null, null, null));
	}

	 /// <summary>
	///     Gets all Side objects
	///     <remarks>   
	///         No parameters. Returns all Side objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Side objects</retvalue>
	/// </summary>
	public ArrayList Get(long lSideID)
	{
		return (Get(lSideID , new DateTime(), new DateTime(), null, null, null));
	}

        /// <summary>
        ///     Gets all Side objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Side to be returned</param>
        ///     <retvalue>ArrayList containing Side object</retvalue>
        /// </summary>
	public ArrayList Get(Side o)
	{	
		return (Get( o.SideID, o.DateCreated, o.DateCreated, o.Code, o.Description, o.VisibleCode	));
	}

        /// <summary>
        ///     Gets all Side objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Side to be returned</param>
        ///     <retvalue>ArrayList containing Side object</retvalue>
        /// </summary>
	public ArrayList Get(EnumSide o)
	{	
		return (Get( o.SideID, o.BeginDateCreated, o.EndDateCreated, o.Code, o.Description, o.VisibleCode	));
	}

		/// <summary>
		///     Gets all Side objects
		///     <remarks>   
		///         Returns Side objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing Side object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngSideID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, string pStrCode, string pStrDescription, string pStrVisibleCode)
		{
			Side data = null;
			_arrlstEntities = new ArrayList();
			EnumSide enumSide = new EnumSide(_conn);
			 enumSide.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumSide.SP_ENUM_NAME;
			enumSide.SideID = pLngSideID;
			enumSide.BeginDateCreated = pDtBeginDateCreated;
			enumSide.EndDateCreated = pDtEndDateCreated;
			enumSide.Code = pStrCode;
			enumSide.Description = pStrDescription;
			enumSide.VisibleCode = pStrVisibleCode;
			enumSide.EnumData();
			while (enumSide.hasMoreElements())
			{
				data = (Side) enumSide.nextElement();
				_arrlstEntities.Add(data);
			}
			enumSide = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves Side object to database
        ///     <param name="o">Side to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Side o)
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
		///     Modify Side object to database
		///     <param name="o">Side to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(Side o)
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
		///     Modify Side object to database
		///     <param name="o">Side to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(Side o)
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
		///     Modify Side object to database
		///     <param name="o">Side to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(Side o)
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
		///     Exist Side object to database
		///     <param name="o">Side to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(Side o)
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
		/// <summary>Property returns an ArrayList containing Side objects</summary>
		public ArrayList Sides 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					Side data = null;
					_arrlstEntities = new ArrayList();
					EnumSide enumSide = new EnumSide(_conn);
					enumSide.EnumData();
					while (enumSide.hasMoreElements())
					{
						data = (Side) enumSide.nextElement();
						_arrlstEntities.Add(data);
					}
					enumSide = null;
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
		public bool IsValid(Side pRefSide)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidSideID(pRefSide.SideID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefSide.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidCode(pRefSide.Code);
			if (!isValidTmp && pRefSide.Code != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidDescription(pRefSide.Description);
			if (!isValidTmp && pRefSide.Description != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidVisibleCode(pRefSide.VisibleCode);
			if (!isValidTmp && pRefSide.VisibleCode != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidSideID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Side.DB_FIELD_ID;
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
				clm.ColumnName = Side.DB_FIELD_DATE_CREATED;
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
				clm.ColumnName = Side.DB_FIELD_CODE;
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
				clm.ColumnName = Side.DB_FIELD_DESCRIPTION;
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
				clm.ColumnName = Side.DB_FIELD_VISIBLE_CODE;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE