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
	/// File:  BusContentState.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	3/11/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for ContentState objects.
	/// </summary>
	public class BusContentState
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


		/// <summary>BusContentState constructor takes SqlConnection object</summary>
		public BusContentState()
		{
		}
		/// <summary>BusContentState constructor takes SqlConnection object</summary>
		public BusContentState(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all ContentState objects
	///     <remarks>   
	///         No parameters. Returns all ContentState objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all ContentState objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), null, null, null));
	}

	 /// <summary>
	///     Gets all ContentState objects
	///     <remarks>   
	///         No parameters. Returns all ContentState objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all ContentState objects</retvalue>
	/// </summary>
	public ArrayList Get(long lContentStateID)
	{
		return (Get(lContentStateID , new DateTime(), new DateTime(), null, null, null));
	}

        /// <summary>
        ///     Gets all ContentState objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">ContentState to be returned</param>
        ///     <retvalue>ArrayList containing ContentState object</retvalue>
        /// </summary>
	public ArrayList Get(ContentState o)
	{	
		return (Get( o.ContentStateID, o.DateCreated, o.DateCreated, o.Code, o.Description, o.VisibleCode	));
	}

        /// <summary>
        ///     Gets all ContentState objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">ContentState to be returned</param>
        ///     <retvalue>ArrayList containing ContentState object</retvalue>
        /// </summary>
	public ArrayList Get(EnumContentState o)
	{	
		return (Get( o.ContentStateID, o.BeginDateCreated, o.EndDateCreated, o.Code, o.Description, o.VisibleCode	));
	}

		/// <summary>
		///     Gets all ContentState objects
		///     <remarks>   
		///         Returns ContentState objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing ContentState object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngContentStateID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, string pStrCode, string pStrDescription, string pStrVisibleCode)
		{
			ContentState data = null;
			_arrlstEntities = new ArrayList();
			EnumContentState enumContentState = new EnumContentState(_conn);
			 enumContentState.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumContentState.SP_ENUM_NAME;
			enumContentState.ContentStateID = pLngContentStateID;
			enumContentState.BeginDateCreated = pDtBeginDateCreated;
			enumContentState.EndDateCreated = pDtEndDateCreated;
			enumContentState.Code = pStrCode;
			enumContentState.Description = pStrDescription;
			enumContentState.VisibleCode = pStrVisibleCode;
			enumContentState.EnumData();
			while (enumContentState.hasMoreElements())
			{
				data = (ContentState) enumContentState.nextElement();
				_arrlstEntities.Add(data);
			}
			enumContentState = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves ContentState object to database
        ///     <param name="o">ContentState to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(ContentState o)
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
		///     Modify ContentState object to database
		///     <param name="o">ContentState to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(ContentState o)
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
		///     Modify ContentState object to database
		///     <param name="o">ContentState to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(ContentState o)
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
		///     Modify ContentState object to database
		///     <param name="o">ContentState to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(ContentState o)
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
		///     Exist ContentState object to database
		///     <param name="o">ContentState to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(ContentState o)
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
		/// <summary>Property returns an ArrayList containing ContentState objects</summary>
		public ArrayList ContentStates 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					ContentState data = null;
					_arrlstEntities = new ArrayList();
					EnumContentState enumContentState = new EnumContentState(_conn);
					enumContentState.EnumData();
					while (enumContentState.hasMoreElements())
					{
						data = (ContentState) enumContentState.nextElement();
						_arrlstEntities.Add(data);
					}
					enumContentState = null;
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
		public bool IsValid(ContentState pRefContentState)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidContentStateID(pRefContentState.ContentStateID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefContentState.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidCode(pRefContentState.Code);
			if (!isValidTmp && pRefContentState.Code != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidDescription(pRefContentState.Description);
			if (!isValidTmp && pRefContentState.Description != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidVisibleCode(pRefContentState.VisibleCode);
			if (!isValidTmp && pRefContentState.VisibleCode != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidContentStateID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = ContentState.DB_FIELD_ID;
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
				clm.ColumnName = ContentState.DB_FIELD_DATE_CREATED;
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
				clm.ColumnName = ContentState.DB_FIELD_CODE;
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
				clm.ColumnName = ContentState.DB_FIELD_DESCRIPTION;
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
				clm.ColumnName = ContentState.DB_FIELD_VISIBLE_CODE;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
