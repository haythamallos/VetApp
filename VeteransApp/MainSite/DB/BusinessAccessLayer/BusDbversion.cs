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
	/// File:  BusDbversion.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	2/27/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for Dbversion objects.
	/// </summary>
	public class BusDbversion
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_MAJOR_NUM = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
		private const String REGEXP_ISVALID_MINOR_NUM = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
		private const String REGEXP_ISVALID_NOTES = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusDbversion constructor takes SqlConnection object</summary>
		public BusDbversion()
		{
		}
		/// <summary>BusDbversion constructor takes SqlConnection object</summary>
		public BusDbversion(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all Dbversion objects
	///     <remarks>   
	///         No parameters. Returns all Dbversion objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Dbversion objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), 0, 0, null));
	}

	 /// <summary>
	///     Gets all Dbversion objects
	///     <remarks>   
	///         No parameters. Returns all Dbversion objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Dbversion objects</retvalue>
	/// </summary>
	public ArrayList Get(long lDbversionID)
	{
		return (Get(lDbversionID , new DateTime(), new DateTime(), 0, 0, null));
	}

        /// <summary>
        ///     Gets all Dbversion objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Dbversion to be returned</param>
        ///     <retvalue>ArrayList containing Dbversion object</retvalue>
        /// </summary>
	public ArrayList Get(Dbversion o)
	{	
		return (Get( o.DbversionID, o.DateCreated, o.DateCreated, o.MajorNum, o.MinorNum, o.Notes	));
	}

        /// <summary>
        ///     Gets all Dbversion objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Dbversion to be returned</param>
        ///     <retvalue>ArrayList containing Dbversion object</retvalue>
        /// </summary>
	public ArrayList Get(EnumDbversion o)
	{	
		return (Get( o.DbversionID, o.BeginDateCreated, o.EndDateCreated, o.MajorNum, o.MinorNum, o.Notes	));
	}

		/// <summary>
		///     Gets all Dbversion objects
		///     <remarks>   
		///         Returns Dbversion objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing Dbversion object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngDbversionID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, long pLngMajorNum, long pLngMinorNum, string pStrNotes)
		{
			Dbversion data = null;
			_arrlstEntities = new ArrayList();
			EnumDbversion enumDbversion = new EnumDbversion(_conn);
			 enumDbversion.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumDbversion.SP_ENUM_NAME;
			enumDbversion.DbversionID = pLngDbversionID;
			enumDbversion.BeginDateCreated = pDtBeginDateCreated;
			enumDbversion.EndDateCreated = pDtEndDateCreated;
			enumDbversion.MajorNum = pLngMajorNum;
			enumDbversion.MinorNum = pLngMinorNum;
			enumDbversion.Notes = pStrNotes;
			enumDbversion.EnumData();
			while (enumDbversion.hasMoreElements())
			{
				data = (Dbversion) enumDbversion.nextElement();
				_arrlstEntities.Add(data);
			}
			enumDbversion = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves Dbversion object to database
        ///     <param name="o">Dbversion to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Dbversion o)
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
		///     Modify Dbversion object to database
		///     <param name="o">Dbversion to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(Dbversion o)
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
		///     Modify Dbversion object to database
		///     <param name="o">Dbversion to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(Dbversion o)
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
		///     Modify Dbversion object to database
		///     <param name="o">Dbversion to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(Dbversion o)
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
		///     Exist Dbversion object to database
		///     <param name="o">Dbversion to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(Dbversion o)
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
		/// <summary>Property returns an ArrayList containing Dbversion objects</summary>
		public ArrayList Dbversions 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					Dbversion data = null;
					_arrlstEntities = new ArrayList();
					EnumDbversion enumDbversion = new EnumDbversion(_conn);
					enumDbversion.EnumData();
					while (enumDbversion.hasMoreElements())
					{
						data = (Dbversion) enumDbversion.nextElement();
						_arrlstEntities.Add(data);
					}
					enumDbversion = null;
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
		public bool IsValid(Dbversion pRefDbversion)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidDbversionID(pRefDbversion.DbversionID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefDbversion.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidMajorNum(pRefDbversion.MajorNum);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidMinorNum(pRefDbversion.MinorNum);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidNotes(pRefDbversion.Notes);
			if (!isValidTmp && pRefDbversion.Notes != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidDbversionID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Dbversion.DB_FIELD_ID;
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
				clm.ColumnName = Dbversion.DB_FIELD_DATE_CREATED;
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
		public bool IsValidMajorNum(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_MAJOR_NUM)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Dbversion.DB_FIELD_MAJOR_NUM;
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
		public bool IsValidMinorNum(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_MINOR_NUM)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Dbversion.DB_FIELD_MINOR_NUM;
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
		public bool IsValidNotes(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_NOTES)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Dbversion.DB_FIELD_NOTES;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
