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
    /// File:  BusMember.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	10/21/2016	Created
    /// 
    /// ----------------------------------------------------
    /// Business Class for Member objects.
    /// </summary>
    public class BusMember
    {
        private SqlConnection _conn = null;
        private bool _hasError = false;
        private bool _hasInvalid = false;

        private ArrayList _arrlstEntities = null;
        private ArrayList _arrlstColumnErrors = new ArrayList();

        private const String REGEXP_ISVALID_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_AUTHTOKEN = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_DATE_CREATED = "";
        private const String REGEXP_ISVALID_DATE_MODIFIED = "";
        private const String REGEXP_ISVALID_FIRSTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_MIDDLENAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_LASTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PROFILEIMAGEURL = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_IS_DISABLED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;

        public string SP_ENUM_NAME = null;


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>BusMember constructor takes SqlConnection object</summary>
        public BusMember()
        {
        }
        /// <summary>BusMember constructor takes SqlConnection object</summary>
        public BusMember(SqlConnection conn)
        {
            _conn = conn;
        }

        /// <summary>
        ///     Gets all Member objects
        ///     <remarks>   
        ///         No parameters. Returns all Member objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all Member objects</retvalue>
        /// </summary>
        public ArrayList Get()
        {
            return (Get(0, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, false));
        }

        /// <summary>
        ///     Gets all Member objects
        ///     <remarks>   
        ///         No parameters. Returns all Member objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all Member objects</retvalue>
        /// </summary>
        public ArrayList Get(long lMemberID)
        {
            return (Get(lMemberID, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, false));
        }

        /// <summary>
        ///     Gets all Member objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Member to be returned</param>
        ///     <retvalue>ArrayList containing Member object</retvalue>
        /// </summary>
        public ArrayList Get(Member o)
        {
            return (Get(o.MemberID, o.Authtoken, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.Firstname, o.Middlename, o.Lastname, o.Profileimageurl, o.IsDisabled));
        }

        /// <summary>
        ///     Gets all Member objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Member to be returned</param>
        ///     <retvalue>ArrayList containing Member object</retvalue>
        /// </summary>
        public ArrayList Get(EnumMember o)
        {
            return (Get(o.MemberID, o.Authtoken, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.Firstname, o.Middlename, o.Lastname, o.Profileimageurl, o.IsDisabled));
        }

        /// <summary>
        ///     Gets all Member objects
        ///     <remarks>   
        ///         Returns Member objects in an array list 
        ///         using the given criteria 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing Member object</retvalue>
        /// </summary>
        public ArrayList Get(long pLngMemberID, string pStrAuthtoken, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, string pStrFirstname, string pStrMiddlename, string pStrLastname, string pStrProfileimageurl, bool? pBolIsDisabled)
        {
            Member data = null;
            _arrlstEntities = new ArrayList();
            EnumMember enumMember = new EnumMember(_conn);
            enumMember.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumMember.SP_ENUM_NAME;
            enumMember.MemberID = pLngMemberID;
            enumMember.Authtoken = pStrAuthtoken;
            enumMember.BeginDateCreated = pDtBeginDateCreated;
            enumMember.EndDateCreated = pDtEndDateCreated;
            enumMember.BeginDateModified = pDtBeginDateModified;
            enumMember.EndDateModified = pDtEndDateModified;
            enumMember.Firstname = pStrFirstname;
            enumMember.Middlename = pStrMiddlename;
            enumMember.Lastname = pStrLastname;
            enumMember.Profileimageurl = pStrProfileimageurl;
            enumMember.IsDisabled = pBolIsDisabled;
            enumMember.EnumData();
            while (enumMember.hasMoreElements())
            {
                data = (Member)enumMember.nextElement();
                _arrlstEntities.Add(data);
            }
            enumMember = null;
            ArrayList.ReadOnly(_arrlstEntities);
            return _arrlstEntities;
        }

        /// <summary>
        ///     Saves Member object to database
        ///     <param name="o">Member to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Member o)
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
        ///     Modify Member object to database
        ///     <param name="o">Member to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Update(Member o)
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
        ///     Modify Member object to database
        ///     <param name="o">Member to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Load(Member o)
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
        ///     Modify Member object to database
        ///     <param name="o">Member to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Delete(Member o)
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
        ///     Exist Member object to database
        ///     <param name="o">Member to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public bool Exist(Member o)
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
        /// <summary>Property returns an ArrayList containing Member objects</summary>
        public ArrayList Members
        {
            get
            {
                if (_arrlstEntities == null)
                {
                    Member data = null;
                    _arrlstEntities = new ArrayList();
                    EnumMember enumMember = new EnumMember(_conn);
                    enumMember.EnumData();
                    while (enumMember.hasMoreElements())
                    {
                        data = (Member)enumMember.nextElement();
                        _arrlstEntities.Add(data);
                    }
                    enumMember = null;
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
        public bool IsValid(Member pRefMember)
        {
            bool isValid = true;
            bool isValidTmp = true;

            _arrlstColumnErrors = null;
            _arrlstColumnErrors = new ArrayList();

            isValidTmp = IsValidMemberID(pRefMember.MemberID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidAuthtoken(pRefMember.Authtoken);
            if (!isValidTmp && pRefMember.Authtoken != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateCreated(pRefMember.DateCreated);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateModified(pRefMember.DateModified);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidFirstname(pRefMember.Firstname);
            if (!isValidTmp && pRefMember.Firstname != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidMiddlename(pRefMember.Middlename);
            if (!isValidTmp && pRefMember.Middlename != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidLastname(pRefMember.Lastname);
            if (!isValidTmp && pRefMember.Lastname != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidProfileimageurl(pRefMember.Profileimageurl);
            if (!isValidTmp && pRefMember.Profileimageurl != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidIsDisabled(pRefMember.IsDisabled);
            if (!isValidTmp && pRefMember.IsDisabled != null)
            {
                isValid = false;
            }

            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidMemberID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Member.DB_FIELD_ID;
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
        public bool IsValidAuthtoken(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_AUTHTOKEN)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Member.DB_FIELD_AUTHTOKEN;
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
                clm.ColumnName = Member.DB_FIELD_DATE_CREATED;
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
                clm.ColumnName = Member.DB_FIELD_DATE_MODIFIED;
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
        public bool IsValidFirstname(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_FIRSTNAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Member.DB_FIELD_FIRSTNAME;
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
        public bool IsValidMiddlename(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_MIDDLENAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Member.DB_FIELD_MIDDLENAME;
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
        public bool IsValidLastname(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_LASTNAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Member.DB_FIELD_LASTNAME;
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
        public bool IsValidProfileimageurl(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_PROFILEIMAGEURL)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = Member.DB_FIELD_PROFILEIMAGEURL;
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
                clm.ColumnName = Member.DB_FIELD_IS_DISABLED;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
    }
}
// END OF CLASS FILE