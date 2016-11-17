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
	/// File:  BusSyslog.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	11/16/2016	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for Syslog objects.
	/// </summary>
	public class BusSyslog
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_INTERACTION_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_DATE_MODIFIED = "";
		private const String REGEXP_ISVALID_MSGSOURCE = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_MSGACTION = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_MSGTXT = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusSyslog constructor takes SqlConnection object</summary>
		public BusSyslog()
		{
		}
		/// <summary>BusSyslog constructor takes SqlConnection object</summary>
		public BusSyslog(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all Syslog objects
	///     <remarks>   
	///         No parameters. Returns all Syslog objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Syslog objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null));
	}

	 /// <summary>
	///     Gets all Syslog objects
	///     <remarks>   
	///         No parameters. Returns all Syslog objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Syslog objects</retvalue>
	/// </summary>
	public ArrayList Get(long lSyslogID)
	{
		return (Get(lSyslogID , 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null));
	}

        /// <summary>
        ///     Gets all Syslog objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Syslog to be returned</param>
        ///     <retvalue>ArrayList containing Syslog object</retvalue>
        /// </summary>
	public ArrayList Get(Syslog o)
	{	
		return (Get( o.SyslogID, o.InteractionID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.Msgsource, o.Msgaction, o.Msgtxt	));
	}

        /// <summary>
        ///     Gets all Syslog objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Syslog to be returned</param>
        ///     <retvalue>ArrayList containing Syslog object</retvalue>
        /// </summary>
	public ArrayList Get(EnumSyslog o)
	{	
		return (Get( o.SyslogID, o.InteractionID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.Msgsource, o.Msgaction, o.Msgtxt	));
	}

		/// <summary>
		///     Gets all Syslog objects
		///     <remarks>   
		///         Returns Syslog objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing Syslog object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngSyslogID, long pLngInteractionID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, string pStrMsgsource, string pStrMsgaction, string pStrMsgtxt)
		{
			Syslog data = null;
			_arrlstEntities = new ArrayList();
			EnumSyslog enumSyslog = new EnumSyslog(_conn);
			 enumSyslog.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumSyslog.SP_ENUM_NAME;
			enumSyslog.SyslogID = pLngSyslogID;
			enumSyslog.InteractionID = pLngInteractionID;
			enumSyslog.BeginDateCreated = pDtBeginDateCreated;
			enumSyslog.EndDateCreated = pDtEndDateCreated;
			enumSyslog.BeginDateModified = pDtBeginDateModified;
			enumSyslog.EndDateModified = pDtEndDateModified;
			enumSyslog.Msgsource = pStrMsgsource;
			enumSyslog.Msgaction = pStrMsgaction;
			enumSyslog.Msgtxt = pStrMsgtxt;
			enumSyslog.EnumData();
			while (enumSyslog.hasMoreElements())
			{
				data = (Syslog) enumSyslog.nextElement();
				_arrlstEntities.Add(data);
			}
			enumSyslog = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves Syslog object to database
        ///     <param name="o">Syslog to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Syslog o)
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
		///     Modify Syslog object to database
		///     <param name="o">Syslog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(Syslog o)
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
		///     Modify Syslog object to database
		///     <param name="o">Syslog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(Syslog o)
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
		///     Modify Syslog object to database
		///     <param name="o">Syslog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(Syslog o)
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
		///     Exist Syslog object to database
		///     <param name="o">Syslog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(Syslog o)
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
		/// <summary>Property returns an ArrayList containing Syslog objects</summary>
		public ArrayList Syslogs 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					Syslog data = null;
					_arrlstEntities = new ArrayList();
					EnumSyslog enumSyslog = new EnumSyslog(_conn);
					enumSyslog.EnumData();
					while (enumSyslog.hasMoreElements())
					{
						data = (Syslog) enumSyslog.nextElement();
						_arrlstEntities.Add(data);
					}
					enumSyslog = null;
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
		public bool IsValid(Syslog pRefSyslog)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidSyslogID(pRefSyslog.SyslogID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidInteractionID(pRefSyslog.InteractionID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefSyslog.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateModified(pRefSyslog.DateModified);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidMsgsource(pRefSyslog.Msgsource);
			if (!isValidTmp && pRefSyslog.Msgsource != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidMsgaction(pRefSyslog.Msgaction);
			if (!isValidTmp && pRefSyslog.Msgaction != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidMsgtxt(pRefSyslog.Msgtxt);
			if (!isValidTmp && pRefSyslog.Msgtxt != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidSyslogID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Syslog.DB_FIELD_ID;
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
		public bool IsValidInteractionID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_INTERACTION_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Syslog.DB_FIELD_INTERACTION_ID;
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
				clm.ColumnName = Syslog.DB_FIELD_DATE_CREATED;
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
				clm.ColumnName = Syslog.DB_FIELD_DATE_MODIFIED;
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
		public bool IsValidMsgsource(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_MSGSOURCE)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Syslog.DB_FIELD_MSGSOURCE;
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
		public bool IsValidMsgaction(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_MSGACTION)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Syslog.DB_FIELD_MSGACTION;
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
		public bool IsValidMsgtxt(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_MSGTXT)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Syslog.DB_FIELD_MSGTXT;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
