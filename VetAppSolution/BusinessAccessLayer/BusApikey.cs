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
	/// File:  BusApikey.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	10/20/2016	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for Apikey objects.
	/// </summary>
	public class BusApikey
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_DATE_EXPIRATION = "";
		private const String REGEXP_ISVALID_IS_DISABLED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_TOKEN = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_NOTES = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusApikey constructor takes SqlConnection object</summary>
		public BusApikey()
		{
		}
		/// <summary>BusApikey constructor takes SqlConnection object</summary>
		public BusApikey(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all Apikey objects
	///     <remarks>   
	///         No parameters. Returns all Apikey objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Apikey objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), false, null, null));
	}

	 /// <summary>
	///     Gets all Apikey objects
	///     <remarks>   
	///         No parameters. Returns all Apikey objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Apikey objects</retvalue>
	/// </summary>
	public ArrayList Get(long lApikeyID)
	{
		return (Get(lApikeyID , new DateTime(), new DateTime(), new DateTime(), new DateTime(), false, null, null));
	}

        /// <summary>
        ///     Gets all Apikey objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Apikey to be returned</param>
        ///     <retvalue>ArrayList containing Apikey object</retvalue>
        /// </summary>
	public ArrayList Get(Apikey o)
	{	
		return (Get( o.ApikeyID, o.DateCreated, o.DateCreated, o.DateExpiration, o.DateExpiration, o.IsDisabled, o.Token, o.Notes	));
	}

        /// <summary>
        ///     Gets all Apikey objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Apikey to be returned</param>
        ///     <retvalue>ArrayList containing Apikey object</retvalue>
        /// </summary>
	public ArrayList Get(EnumApikey o)
	{	
		return (Get( o.ApikeyID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateExpiration, o.EndDateExpiration, o.IsDisabled, o.Token, o.Notes	));
	}

		/// <summary>
		///     Gets all Apikey objects
		///     <remarks>   
		///         Returns Apikey objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing Apikey object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngApikeyID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateExpiration, DateTime pDtEndDateExpiration, bool? pBolIsDisabled, string pStrToken, string pStrNotes)
		{
			Apikey data = null;
			_arrlstEntities = new ArrayList();
			EnumApikey enumApikey = new EnumApikey(_conn);
			 enumApikey.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumApikey.SP_ENUM_NAME;
			enumApikey.ApikeyID = pLngApikeyID;
			enumApikey.BeginDateCreated = pDtBeginDateCreated;
			enumApikey.EndDateCreated = pDtEndDateCreated;
			enumApikey.BeginDateExpiration = pDtBeginDateExpiration;
			enumApikey.EndDateExpiration = pDtEndDateExpiration;
			enumApikey.IsDisabled = pBolIsDisabled;
			enumApikey.Token = pStrToken;
			enumApikey.Notes = pStrNotes;
			enumApikey.EnumData();
			while (enumApikey.hasMoreElements())
			{
				data = (Apikey) enumApikey.nextElement();
				_arrlstEntities.Add(data);
			}
			enumApikey = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves Apikey object to database
        ///     <param name="o">Apikey to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Apikey o)
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
		///     Modify Apikey object to database
		///     <param name="o">Apikey to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(Apikey o)
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
		///     Modify Apikey object to database
		///     <param name="o">Apikey to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(Apikey o)
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
		///     Modify Apikey object to database
		///     <param name="o">Apikey to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(Apikey o)
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
		///     Exist Apikey object to database
		///     <param name="o">Apikey to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(Apikey o)
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
		/// <summary>Property returns an ArrayList containing Apikey objects</summary>
		public ArrayList Apikeys 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					Apikey data = null;
					_arrlstEntities = new ArrayList();
					EnumApikey enumApikey = new EnumApikey(_conn);
					enumApikey.EnumData();
					while (enumApikey.hasMoreElements())
					{
						data = (Apikey) enumApikey.nextElement();
						_arrlstEntities.Add(data);
					}
					enumApikey = null;
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
		public bool IsValid(Apikey pRefApikey)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidApikeyID(pRefApikey.ApikeyID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefApikey.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateExpiration(pRefApikey.DateExpiration);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidIsDisabled(pRefApikey.IsDisabled);
			if (!isValidTmp && pRefApikey.IsDisabled != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidToken(pRefApikey.Token);
			if (!isValidTmp && pRefApikey.Token != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidNotes(pRefApikey.Notes);
			if (!isValidTmp && pRefApikey.Notes != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidApikeyID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apikey.DB_FIELD_ID;
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
				clm.ColumnName = Apikey.DB_FIELD_DATE_CREATED;
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
		public bool IsValidDateExpiration(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_DATE_EXPIRATION)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apikey.DB_FIELD_DATE_EXPIRATION;
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
		public bool IsValidIsDisabled(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_IS_DISABLED)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apikey.DB_FIELD_IS_DISABLED;
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
		public bool IsValidToken(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_TOKEN)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apikey.DB_FIELD_TOKEN;
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
				clm.ColumnName = Apikey.DB_FIELD_NOTES;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}



	}
}
 // END OF CLASS FILE
