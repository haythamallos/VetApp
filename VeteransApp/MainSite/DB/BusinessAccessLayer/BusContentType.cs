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
	/// File:  BusContentType.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	3/1/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for ContentType objects.
	/// </summary>
	public class BusContentType
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


		/// <summary>BusContentType constructor takes SqlConnection object</summary>
		public BusContentType()
		{
		}
		/// <summary>BusContentType constructor takes SqlConnection object</summary>
		public BusContentType(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all ContentType objects
	///     <remarks>   
	///         No parameters. Returns all ContentType objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all ContentType objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), null, null, null));
	}

	 /// <summary>
	///     Gets all ContentType objects
	///     <remarks>   
	///         No parameters. Returns all ContentType objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all ContentType objects</retvalue>
	/// </summary>
	public ArrayList Get(long lContentTypeID)
	{
		return (Get(lContentTypeID , new DateTime(), new DateTime(), null, null, null));
	}

        /// <summary>
        ///     Gets all ContentType objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">ContentType to be returned</param>
        ///     <retvalue>ArrayList containing ContentType object</retvalue>
        /// </summary>
	public ArrayList Get(ContentType o)
	{	
		return (Get( o.ContentTypeID, o.DateCreated, o.DateCreated, o.Code, o.Description, o.VisibleCode	));
	}

        /// <summary>
        ///     Gets all ContentType objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">ContentType to be returned</param>
        ///     <retvalue>ArrayList containing ContentType object</retvalue>
        /// </summary>
	public ArrayList Get(EnumContentType o)
	{	
		return (Get( o.ContentTypeID, o.BeginDateCreated, o.EndDateCreated, o.Code, o.Description, o.VisibleCode	));
	}

		/// <summary>
		///     Gets all ContentType objects
		///     <remarks>   
		///         Returns ContentType objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing ContentType object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngContentTypeID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, string pStrCode, string pStrDescription, string pStrVisibleCode)
		{
			ContentType data = null;
			_arrlstEntities = new ArrayList();
			EnumContentType enumContentType = new EnumContentType(_conn);
			 enumContentType.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumContentType.SP_ENUM_NAME;
			enumContentType.ContentTypeID = pLngContentTypeID;
			enumContentType.BeginDateCreated = pDtBeginDateCreated;
			enumContentType.EndDateCreated = pDtEndDateCreated;
			enumContentType.Code = pStrCode;
			enumContentType.Description = pStrDescription;
			enumContentType.VisibleCode = pStrVisibleCode;
			enumContentType.EnumData();
			while (enumContentType.hasMoreElements())
			{
				data = (ContentType) enumContentType.nextElement();
				_arrlstEntities.Add(data);
			}
			enumContentType = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves ContentType object to database
        ///     <param name="o">ContentType to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(ContentType o)
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
		///     Modify ContentType object to database
		///     <param name="o">ContentType to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(ContentType o)
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
		///     Modify ContentType object to database
		///     <param name="o">ContentType to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(ContentType o)
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
		///     Modify ContentType object to database
		///     <param name="o">ContentType to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(ContentType o)
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
		///     Exist ContentType object to database
		///     <param name="o">ContentType to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(ContentType o)
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
		/// <summary>Property returns an ArrayList containing ContentType objects</summary>
		public ArrayList ContentTypes 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					ContentType data = null;
					_arrlstEntities = new ArrayList();
					EnumContentType enumContentType = new EnumContentType(_conn);
					enumContentType.EnumData();
					while (enumContentType.hasMoreElements())
					{
						data = (ContentType) enumContentType.nextElement();
						_arrlstEntities.Add(data);
					}
					enumContentType = null;
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
		public bool IsValid(ContentType pRefContentType)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidContentTypeID(pRefContentType.ContentTypeID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefContentType.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidCode(pRefContentType.Code);
			if (!isValidTmp && pRefContentType.Code != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidDescription(pRefContentType.Description);
			if (!isValidTmp && pRefContentType.Description != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidVisibleCode(pRefContentType.VisibleCode);
			if (!isValidTmp && pRefContentType.VisibleCode != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidContentTypeID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = ContentType.DB_FIELD_ID;
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
				clm.ColumnName = ContentType.DB_FIELD_DATE_CREATED;
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
				clm.ColumnName = ContentType.DB_FIELD_CODE;
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
				clm.ColumnName = ContentType.DB_FIELD_DESCRIPTION;
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
				clm.ColumnName = ContentType.DB_FIELD_VISIBLE_CODE;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
