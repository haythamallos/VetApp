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
	/// File:  BusEvaluation.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	2/27/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for Evaluation objects.
	/// </summary>
	public class BusEvaluation
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_USER_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_DATE_MODIFIED = "";
		private const String REGEXP_ISVALID_IS_FIRSTTIME_FILING = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_HAS_A_CLAIM = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_HAS_ACTIVE_APPEAL = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_CURRENT_RATING = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusEvaluation constructor takes SqlConnection object</summary>
		public BusEvaluation()
		{
		}
		/// <summary>BusEvaluation constructor takes SqlConnection object</summary>
		public BusEvaluation(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all Evaluation objects
	///     <remarks>   
	///         No parameters. Returns all Evaluation objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Evaluation objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), false, false, false, 0));
	}

	 /// <summary>
	///     Gets all Evaluation objects
	///     <remarks>   
	///         No parameters. Returns all Evaluation objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Evaluation objects</retvalue>
	/// </summary>
	public ArrayList Get(long lEvaluationID)
	{
		return (Get(lEvaluationID , 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), false, false, false, 0));
	}

        /// <summary>
        ///     Gets all Evaluation objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Evaluation to be returned</param>
        ///     <retvalue>ArrayList containing Evaluation object</retvalue>
        /// </summary>
	public ArrayList Get(Evaluation o)
	{	
		return (Get( o.EvaluationID, o.UserID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.IsFirsttimeFiling, o.HasAClaim, o.HasActiveAppeal, o.CurrentRating	));
	}

        /// <summary>
        ///     Gets all Evaluation objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Evaluation to be returned</param>
        ///     <retvalue>ArrayList containing Evaluation object</retvalue>
        /// </summary>
	public ArrayList Get(EnumEvaluation o)
	{	
		return (Get( o.EvaluationID, o.UserID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.IsFirsttimeFiling, o.HasAClaim, o.HasActiveAppeal, o.CurrentRating	));
	}

		/// <summary>
		///     Gets all Evaluation objects
		///     <remarks>   
		///         Returns Evaluation objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing Evaluation object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngEvaluationID, long pLngUserID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, bool? pBolIsFirsttimeFiling, bool? pBolHasAClaim, bool? pBolHasActiveAppeal, long pLngCurrentRating)
		{
			Evaluation data = null;
			_arrlstEntities = new ArrayList();
			EnumEvaluation enumEvaluation = new EnumEvaluation(_conn);
			 enumEvaluation.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumEvaluation.SP_ENUM_NAME;
			enumEvaluation.EvaluationID = pLngEvaluationID;
			enumEvaluation.UserID = pLngUserID;
			enumEvaluation.BeginDateCreated = pDtBeginDateCreated;
			enumEvaluation.EndDateCreated = pDtEndDateCreated;
			enumEvaluation.BeginDateModified = pDtBeginDateModified;
			enumEvaluation.EndDateModified = pDtEndDateModified;
			enumEvaluation.IsFirsttimeFiling = pBolIsFirsttimeFiling;
			enumEvaluation.HasAClaim = pBolHasAClaim;
			enumEvaluation.HasActiveAppeal = pBolHasActiveAppeal;
			enumEvaluation.CurrentRating = pLngCurrentRating;
			enumEvaluation.EnumData();
			while (enumEvaluation.hasMoreElements())
			{
				data = (Evaluation) enumEvaluation.nextElement();
				_arrlstEntities.Add(data);
			}
			enumEvaluation = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves Evaluation object to database
        ///     <param name="o">Evaluation to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Evaluation o)
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
		///     Modify Evaluation object to database
		///     <param name="o">Evaluation to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(Evaluation o)
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
		///     Modify Evaluation object to database
		///     <param name="o">Evaluation to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(Evaluation o)
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
		///     Modify Evaluation object to database
		///     <param name="o">Evaluation to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(Evaluation o)
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
		///     Exist Evaluation object to database
		///     <param name="o">Evaluation to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(Evaluation o)
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
		/// <summary>Property returns an ArrayList containing Evaluation objects</summary>
		public ArrayList Evaluations 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					Evaluation data = null;
					_arrlstEntities = new ArrayList();
					EnumEvaluation enumEvaluation = new EnumEvaluation(_conn);
					enumEvaluation.EnumData();
					while (enumEvaluation.hasMoreElements())
					{
						data = (Evaluation) enumEvaluation.nextElement();
						_arrlstEntities.Add(data);
					}
					enumEvaluation = null;
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
		public bool IsValid(Evaluation pRefEvaluation)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidEvaluationID(pRefEvaluation.EvaluationID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidUserID(pRefEvaluation.UserID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefEvaluation.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateModified(pRefEvaluation.DateModified);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidIsFirsttimeFiling(pRefEvaluation.IsFirsttimeFiling);
			if (!isValidTmp && pRefEvaluation.IsFirsttimeFiling != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidHasAClaim(pRefEvaluation.HasAClaim);
			if (!isValidTmp && pRefEvaluation.HasAClaim != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidHasActiveAppeal(pRefEvaluation.HasActiveAppeal);
			if (!isValidTmp && pRefEvaluation.HasActiveAppeal != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidCurrentRating(pRefEvaluation.CurrentRating);
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
		public bool IsValidEvaluationID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Evaluation.DB_FIELD_ID;
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
		public bool IsValidUserID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_USER_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Evaluation.DB_FIELD_USER_ID;
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
				clm.ColumnName = Evaluation.DB_FIELD_DATE_CREATED;
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
				clm.ColumnName = Evaluation.DB_FIELD_DATE_MODIFIED;
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
		public bool IsValidIsFirsttimeFiling(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_IS_FIRSTTIME_FILING)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Evaluation.DB_FIELD_IS_FIRSTTIME_FILING;
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
		public bool IsValidHasAClaim(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_HAS_A_CLAIM)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Evaluation.DB_FIELD_HAS_A_CLAIM;
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
		public bool IsValidHasActiveAppeal(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_HAS_ACTIVE_APPEAL)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Evaluation.DB_FIELD_HAS_ACTIVE_APPEAL;
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
		public bool IsValidCurrentRating(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_CURRENT_RATING)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Evaluation.DB_FIELD_CURRENT_RATING;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
