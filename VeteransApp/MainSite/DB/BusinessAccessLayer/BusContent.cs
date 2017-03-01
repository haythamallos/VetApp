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
    /// File:  BusContent.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/1/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Business Class for Content objects.
    /// </summary>
    public class BusContent
    {
        private SqlConnection _conn = null;
        private bool _hasError = false;
        private bool _hasInvalid = false;

        private ArrayList _arrlstEntities = null;
        private ArrayList _arrlstColumnErrors = new ArrayList();

        private const String REGEXP_ISVALID_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_USER_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_CONTENT_TYPE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_DATE_CREATED = "";
        private const String REGEXP_ISVALID_DATE_MODIFIED = "";
        private const String REGEXP_ISVALID_CONTENT_URL = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_CONTENT_DATA = "";
        private const String REGEXP_ISVALID_CONTENT_META = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
        private const String REGEXP_ISVALID_IS_SUBMITTED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_IS_DISABLED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_IS_DRAFT = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_DATE_SUBMITTED = "";
        private const String REGEXP_ISVALID_NOTES = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;

        public string SP_ENUM_NAME = null;


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>BusContent constructor takes SqlConnection object</summary>
        public BusContent()
        {
        }
        /// <summary>BusContent constructor takes SqlConnection object</summary>
        public BusContent(SqlConnection conn)
        {
            _conn = conn;
        }

        /// <summary>
        ///     Gets all Content objects
        ///     <remarks>   
        ///         No parameters. Returns all Content objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all Content objects</retvalue>
        /// </summary>
        public ArrayList Get()
        {
            return (Get(0, 0, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, false, false, false, new DateTime(), new DateTime(), null));
        }

        /// <summary>
        ///     Gets all Content objects
        ///     <remarks>   
        ///         No parameters. Returns all Content objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all Content objects</retvalue>
        /// </summary>
        public ArrayList Get(long lContentID)
        {
            return (Get(lContentID, 0, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, false, false, false, new DateTime(), new DateTime(), null));
        }

        /// <summary>
        ///     Gets all Content objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Content to be returned</param>
        ///     <retvalue>ArrayList containing Content object</retvalue>
        /// </summary>
        public ArrayList Get(Content o)
        {
            return (Get(o.ContentID, o.UserID, o.ContentTypeID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.ContentUrl, o.ContentData, o.ContentMeta, o.IsSubmitted, o.IsDisabled, o.IsDraft, o.DateSubmitted, o.DateSubmitted, o.Notes));
        }

        /// <summary>
        ///     Gets all Content objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Content to be returned</param>
        ///     <retvalue>ArrayList containing Content object</retvalue>
        /// </summary>
        public ArrayList Get(EnumContent o)
        {
            return (Get(o.ContentID, o.UserID, o.ContentTypeID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.ContentUrl, o.ContentData, o.ContentMeta, o.IsSubmitted, o.IsDisabled, o.IsDraft, o.BeginDateSubmitted, o.EndDateSubmitted, o.Notes));
        }

        /// <summary>
        ///     Gets all Content objects
        ///     <remarks>   
        ///         Returns Content objects in an array list 
        ///         using the given criteria 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing Content object</retvalue>
        /// </summary>
        public ArrayList Get(long pLngContentID, long pLngUserID, long pLngContentTypeID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, string pStrContentUrl, byte[] pBytContentData, string pStrContentMeta, bool? pBolIsSubmitted, bool? pBolIsDisabled, bool? pBolIsDraft, DateTime pDtBeginDateSubmitted, DateTime pDtEndDateSubmitted, string pStrNotes)
        {
            Content data = null;
            _arrlstEntities = new ArrayList();
            EnumContent enumContent = new EnumContent(_conn);
            enumContent.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumContent.SP_ENUM_NAME;
            enumContent.ContentID = pLngContentID;
            enumContent.UserID = pLngUserID;
            enumContent.ContentTypeID = pLngContentTypeID;
            enumContent.BeginDateCreated = pDtBeginDateCreated;
            enumContent.EndDateCreated = pDtEndDateCreated;
            enumContent.BeginDateModified = pDtBeginDateModified;
            enumContent.EndDateModified = pDtEndDateModified;
            enumContent.ContentUrl = pStrContentUrl;
            enumContent.ContentData = pBytContentData;
            enumContent.ContentMeta = pStrContentMeta;
            enumContent.IsSubmitted = pBolIsSubmitted;
            enumContent.IsDisabled = pBolIsDisabled;
            enumContent.IsDraft = pBolIsDraft;
            enumContent.BeginDateSubmitted = pDtBeginDateSubmitted;
            enumContent.EndDateSubmitted = pDtEndDateSubmitted;
            enumContent.Notes = pStrNotes;
            enumContent.EnumData();
            while (enumContent.hasMoreElements())
            {
                data = (Content)enumContent.nextElement();
                _arrlstEntities.Add(data);
            }
            enumContent = null;
            ArrayList.ReadOnly(_arrlstEntities);
            return _arrlstEntities;
        }

        /// <summary>
        ///     Saves Content object to database
        ///     <param name="o">Content to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Content o)
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
        ///     Modify Content object to database
        ///     <param name="o">Content to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Update(Content o)
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
        ///     Modify Content object to database
        ///     <param name="o">Content to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Load(Content o)
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
        ///     Modify Content object to database
        ///     <param name="o">Content to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Delete(Content o)
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
        ///     Exist Content object to database
        ///     <param name="o">Content to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public bool Exist(Content o)
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
        /// <summary>Property returns an ArrayList containing Content objects</summary>
        public ArrayList Contents
        {
            get
            {
                if (_arrlstEntities == null)
                {
                    Content data = null;
                    _arrlstEntities = new ArrayList();
                    EnumContent enumContent = new EnumContent(_conn);
                    enumContent.EnumData();
                    while (enumContent.hasMoreElements())
                    {
                        data = (Content)enumContent.nextElement();
                        _arrlstEntities.Add(data);
                    }
                    enumContent = null;
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
        public bool IsValid(Content pRefContent)
        {
            bool isValid = true;
            bool isValidTmp = true;

            _arrlstColumnErrors = null;
            _arrlstColumnErrors = new ArrayList();

            isValidTmp = IsValidContentID(pRefContent.ContentID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidUserID(pRefContent.UserID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidContentTypeID(pRefContent.ContentTypeID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateCreated(pRefContent.DateCreated);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateModified(pRefContent.DateModified);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidContentUrl(pRefContent.ContentUrl);
            if (!isValidTmp && pRefContent.ContentUrl != null)
            {
                isValid = false;
            }
            //Cannot validate type byte[].
            isValidTmp = IsValidContentMeta(pRefContent.ContentMeta);
            if (!isValidTmp && pRefContent.ContentMeta != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidIsSubmitted(pRefContent.IsSubmitted);
            if (!isValidTmp && pRefContent.IsSubmitted != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidIsDisabled(pRefContent.IsDisabled);
            if (!isValidTmp && pRefContent.IsDisabled != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidIsDraft(pRefContent.IsDraft);
            if (!isValidTmp && pRefContent.IsDraft != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateSubmitted(pRefContent.DateSubmitted);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidNotes(pRefContent.Notes);
            if (!isValidTmp && pRefContent.Notes != null)
            {
                isValid = false;
            }

            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidContentID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_ID;
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
                clm.ColumnName = Content.DB_FIELD_USER_ID;
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
                clm.ColumnName = Content.DB_FIELD_CONTENT_TYPE_ID;
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
                clm.ColumnName = Content.DB_FIELD_DATE_CREATED;
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
                clm.ColumnName = Content.DB_FIELD_DATE_MODIFIED;
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
        public bool IsValidContentUrl(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_CONTENT_URL)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_CONTENT_URL;
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
        public bool IsValidContentData(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_CONTENT_DATA)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_CONTENT_DATA;
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
        public bool IsValidContentMeta(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_CONTENT_META)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_CONTENT_META;
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
        public bool IsValidIsSubmitted(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_IS_SUBMITTED)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_IS_SUBMITTED;
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
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_IS_DISABLED;
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
        public bool IsValidIsDraft(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_IS_DRAFT)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_IS_DRAFT;
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
        public bool IsValidDateSubmitted(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_DATE_SUBMITTED)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_DATE_SUBMITTED;
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
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Content.DB_FIELD_NOTES;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
    }
}
// END OF CLASS FILE