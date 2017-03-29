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
    /// File:  BusJctUserContentType.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/24/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Business Class for JctUserContentType objects.
    /// </summary>
    public class BusJctUserContentType
    {
        private SqlConnection _conn = null;
        private bool _hasError = false;
        private bool _hasInvalid = false;

        private ArrayList _arrlstEntities = null;
        private ArrayList _arrlstColumnErrors = new ArrayList();

        private const String REGEXP_ISVALID_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_DATE_CREATED = "";
        private const String REGEXP_ISVALID_DATE_MODIFIED = "";
        private const String REGEXP_ISVALID_USER_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_SIDE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_CONTENT_TYPE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_RATING = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
        private const String REGEXP_ISVALID_RATINGLEFT = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
        private const String REGEXP_ISVALID_RATINGRIGHT = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;

        public string SP_ENUM_NAME = null;


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>BusJctUserContentType constructor takes SqlConnection object</summary>
        public BusJctUserContentType()
        {
        }
        /// <summary>BusJctUserContentType constructor takes SqlConnection object</summary>
        public BusJctUserContentType(SqlConnection conn)
        {
            _conn = conn;
        }

        /// <summary>
        ///     Gets all JctUserContentType objects
        ///     <remarks>   
        ///         No parameters. Returns all JctUserContentType objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all JctUserContentType objects</retvalue>
        /// </summary>
        public ArrayList Get()
        {
            return (Get(0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), 0, 0, 0, 0, 0, 0));
        }

        /// <summary>
        ///     Gets all JctUserContentType objects
        ///     <remarks>   
        ///         No parameters. Returns all JctUserContentType objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all JctUserContentType objects</retvalue>
        /// </summary>
        public ArrayList Get(long lJctUserContentTypeID)
        {
            return (Get(lJctUserContentTypeID, new DateTime(), new DateTime(), new DateTime(), new DateTime(), 0, 0, 0, 0, 0, 0));
        }

        /// <summary>
        ///     Gets all JctUserContentType objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">JctUserContentType to be returned</param>
        ///     <retvalue>ArrayList containing JctUserContentType object</retvalue>
        /// </summary>
        public ArrayList Get(JctUserContentType o)
        {
            return (Get(o.JctUserContentTypeID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.UserID, o.SideID, o.ContentTypeID, o.Rating, o.RatingLeft, o.RatingRight));
        }

        /// <summary>
        ///     Gets all JctUserContentType objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">JctUserContentType to be returned</param>
        ///     <retvalue>ArrayList containing JctUserContentType object</retvalue>
        /// </summary>
        public ArrayList Get(EnumJctUserContentType o)
        {
            return (Get(o.JctUserContentTypeID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.UserID, o.SideID, o.ContentTypeID, o.Rating, o.RatingLeft, o.RatingRight));
        }

        /// <summary>
        ///     Gets all JctUserContentType objects
        ///     <remarks>   
        ///         Returns JctUserContentType objects in an array list 
        ///         using the given criteria 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing JctUserContentType object</retvalue>
        /// </summary>
        public ArrayList Get(long pLngJctUserContentTypeID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, long pLngUserID, long pLngSideID, long pLngContentTypeID, long pLngRating, long pLngRatingLeft, long pLngRatingRight)
        {
            JctUserContentType data = null;
            _arrlstEntities = new ArrayList();
            EnumJctUserContentType enumJctUserContentType = new EnumJctUserContentType(_conn);
            enumJctUserContentType.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumJctUserContentType.SP_ENUM_NAME;
            enumJctUserContentType.JctUserContentTypeID = pLngJctUserContentTypeID;
            enumJctUserContentType.BeginDateCreated = pDtBeginDateCreated;
            enumJctUserContentType.EndDateCreated = pDtEndDateCreated;
            enumJctUserContentType.BeginDateModified = pDtBeginDateModified;
            enumJctUserContentType.EndDateModified = pDtEndDateModified;
            enumJctUserContentType.UserID = pLngUserID;
            enumJctUserContentType.SideID = pLngSideID;
            enumJctUserContentType.ContentTypeID = pLngContentTypeID;
            enumJctUserContentType.Rating = pLngRating;
            enumJctUserContentType.RatingLeft = pLngRatingLeft;
            enumJctUserContentType.RatingRight = pLngRatingRight;
            enumJctUserContentType.EnumData();
            while (enumJctUserContentType.hasMoreElements())
            {
                data = (JctUserContentType)enumJctUserContentType.nextElement();
                _arrlstEntities.Add(data);
            }
            enumJctUserContentType = null;
            ArrayList.ReadOnly(_arrlstEntities);
            return _arrlstEntities;
        }

        /// <summary>
        ///     Saves JctUserContentType object to database
        ///     <param name="o">JctUserContentType to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(JctUserContentType o)
        {
            if (o != null)
            {
                o.Save(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Modify JctUserContentType object to database
        ///     <param name="o">JctUserContentType to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Update(JctUserContentType o)
        {
            if (o != null)
            {
                o.Update(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Modify JctUserContentType object to database
        ///     <param name="o">JctUserContentType to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Load(JctUserContentType o)
        {
            if (o != null)
            {
                o.Load(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Modify JctUserContentType object to database
        ///     <param name="o">JctUserContentType to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Delete(JctUserContentType o)
        {
            if (o != null)
            {
                o.Delete(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Exist JctUserContentType object to database
        ///     <param name="o">JctUserContentType to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public bool Exist(JctUserContentType o)
        {
            bool bExist = false;
            if (o != null)
            {
                bExist = o.Exist(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }

            return bExist;
        }
        /// <summary>Property as to whether or not the object has an Error</summary>
        public bool HasError
        {
            get { return _hasError; }
        }
        /// <summary>Property as to whether or not the object has invalid columns</summary>
        public bool HasInvalid
        {
            get { return _hasInvalid; }
        }
        /// <summary>Property which returns all column error in an ArrayList</summary>
        public ArrayList ColumnErrors
        {
            get { return _arrlstColumnErrors; }
        }
        /// <summary>Property returns an ArrayList containing JctUserContentType objects</summary>
        public ArrayList JctUserContentTypes
        {
            get
            {
                if (_arrlstEntities == null)
                {
                    JctUserContentType data = null;
                    _arrlstEntities = new ArrayList();
                    EnumJctUserContentType enumJctUserContentType = new EnumJctUserContentType(_conn);
                    enumJctUserContentType.EnumData();
                    while (enumJctUserContentType.hasMoreElements())
                    {
                        data = (JctUserContentType)enumJctUserContentType.nextElement();
                        _arrlstEntities.Add(data);
                    }
                    enumJctUserContentType = null;
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
        public bool IsValid(JctUserContentType pRefJctUserContentType)
        {
            bool isValid = true;
            bool isValidTmp = true;

            _arrlstColumnErrors = null;
            _arrlstColumnErrors = new ArrayList();

            isValidTmp = IsValidJctUserContentTypeID(pRefJctUserContentType.JctUserContentTypeID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateCreated(pRefJctUserContentType.DateCreated);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateModified(pRefJctUserContentType.DateModified);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidUserID(pRefJctUserContentType.UserID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidSideID(pRefJctUserContentType.SideID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidContentTypeID(pRefJctUserContentType.ContentTypeID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidRating(pRefJctUserContentType.Rating);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidRatingLeft(pRefJctUserContentType.RatingLeft);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidRatingRight(pRefJctUserContentType.RatingRight);
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
        public bool IsValidJctUserContentTypeID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_ID;
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
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_DATE_CREATED;
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
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_DATE_MODIFIED;
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
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_USER_ID;
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
        public bool IsValidSideID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_SIDE_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_SIDE_ID;
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
        public bool IsValidContentTypeID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_CONTENT_TYPE_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_CONTENT_TYPE_ID;
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
        public bool IsValidRating(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_RATING)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_RATING;
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
        public bool IsValidRatingLeft(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_RATINGLEFT)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_RATINGLEFT;
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
        public bool IsValidRatingRight(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_RATINGRIGHT)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = JctUserContentType.DB_FIELD_RATINGRIGHT;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
    }
}
// END OF CLASS FILE