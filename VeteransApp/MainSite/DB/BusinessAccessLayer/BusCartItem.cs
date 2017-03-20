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
    /// File:  BusCartItem.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/20/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Business Class for CartItem objects.
    /// </summary>
    public class BusCartItem
    {
        private SqlConnection _conn = null;
        private bool _hasError = false;
        private bool _hasInvalid = false;

        private ArrayList _arrlstEntities = null;
        private ArrayList _arrlstColumnErrors = new ArrayList();

        private const String REGEXP_ISVALID_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_DATE_CREATED = "";
        private const String REGEXP_ISVALID_DATE_MODIFIED = "";
        private const String REGEXP_ISVALID_PURCHASE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_USER_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_CONTENT_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_CONTENT_TYPE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;

        public string SP_ENUM_NAME = null;


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>BusCartItem constructor takes SqlConnection object</summary>
        public BusCartItem()
        {
        }
        /// <summary>BusCartItem constructor takes SqlConnection object</summary>
        public BusCartItem(SqlConnection conn)
        {
            _conn = conn;
        }

        /// <summary>
        ///     Gets all CartItem objects
        ///     <remarks>   
        ///         No parameters. Returns all CartItem objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all CartItem objects</retvalue>
        /// </summary>
        public ArrayList Get()
        {
            return (Get(0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), 0, 0, 0, 0));
        }

        /// <summary>
        ///     Gets all CartItem objects
        ///     <remarks>   
        ///         No parameters. Returns all CartItem objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all CartItem objects</retvalue>
        /// </summary>
        public ArrayList Get(long lCartItemID)
        {
            return (Get(lCartItemID, new DateTime(), new DateTime(), new DateTime(), new DateTime(), 0, 0, 0, 0));
        }

        /// <summary>
        ///     Gets all CartItem objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">CartItem to be returned</param>
        ///     <retvalue>ArrayList containing CartItem object</retvalue>
        /// </summary>
        public ArrayList Get(CartItem o)
        {
            return (Get(o.CartItemID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.PurchaseID, o.UserID, o.ContentID, o.ContentTypeID));
        }

        /// <summary>
        ///     Gets all CartItem objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">CartItem to be returned</param>
        ///     <retvalue>ArrayList containing CartItem object</retvalue>
        /// </summary>
        public ArrayList Get(EnumCartItem o)
        {
            return (Get(o.CartItemID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.PurchaseID, o.UserID, o.ContentID, o.ContentTypeID));
        }

        /// <summary>
        ///     Gets all CartItem objects
        ///     <remarks>   
        ///         Returns CartItem objects in an array list 
        ///         using the given criteria 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing CartItem object</retvalue>
        /// </summary>
        public ArrayList Get(long pLngCartItemID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, long pLngPurchaseID, long pLngUserID, long pLngContentID, long pLngContentTypeID)
        {
            CartItem data = null;
            _arrlstEntities = new ArrayList();
            EnumCartItem enumCartItem = new EnumCartItem(_conn);
            enumCartItem.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumCartItem.SP_ENUM_NAME;
            enumCartItem.CartItemID = pLngCartItemID;
            enumCartItem.BeginDateCreated = pDtBeginDateCreated;
            enumCartItem.EndDateCreated = pDtEndDateCreated;
            enumCartItem.BeginDateModified = pDtBeginDateModified;
            enumCartItem.EndDateModified = pDtEndDateModified;
            enumCartItem.PurchaseID = pLngPurchaseID;
            enumCartItem.UserID = pLngUserID;
            enumCartItem.ContentID = pLngContentID;
            enumCartItem.ContentTypeID = pLngContentTypeID;
            enumCartItem.EnumData();
            while (enumCartItem.hasMoreElements())
            {
                data = (CartItem)enumCartItem.nextElement();
                _arrlstEntities.Add(data);
            }
            enumCartItem = null;
            ArrayList.ReadOnly(_arrlstEntities);
            return _arrlstEntities;
        }

        /// <summary>
        ///     Saves CartItem object to database
        ///     <param name="o">CartItem to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(CartItem o)
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
        ///     Modify CartItem object to database
        ///     <param name="o">CartItem to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Update(CartItem o)
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
        ///     Modify CartItem object to database
        ///     <param name="o">CartItem to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Load(CartItem o)
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
        ///     Modify CartItem object to database
        ///     <param name="o">CartItem to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Delete(CartItem o)
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
        ///     Exist CartItem object to database
        ///     <param name="o">CartItem to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public bool Exist(CartItem o)
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
        /// <summary>Property returns an ArrayList containing CartItem objects</summary>
        public ArrayList CartItems
        {
            get
            {
                if (_arrlstEntities == null)
                {
                    CartItem data = null;
                    _arrlstEntities = new ArrayList();
                    EnumCartItem enumCartItem = new EnumCartItem(_conn);
                    enumCartItem.EnumData();
                    while (enumCartItem.hasMoreElements())
                    {
                        data = (CartItem)enumCartItem.nextElement();
                        _arrlstEntities.Add(data);
                    }
                    enumCartItem = null;
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
        public bool IsValid(CartItem pRefCartItem)
        {
            bool isValid = true;
            bool isValidTmp = true;

            _arrlstColumnErrors = null;
            _arrlstColumnErrors = new ArrayList();

            isValidTmp = IsValidCartItemID(pRefCartItem.CartItemID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateCreated(pRefCartItem.DateCreated);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateModified(pRefCartItem.DateModified);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidPurchaseID(pRefCartItem.PurchaseID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidUserID(pRefCartItem.UserID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidContentID(pRefCartItem.ContentID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidContentTypeID(pRefCartItem.ContentTypeID);
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
        public bool IsValidCartItemID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = CartItem.DB_FIELD_ID;
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
                clm.ColumnName = CartItem.DB_FIELD_DATE_CREATED;
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
                clm.ColumnName = CartItem.DB_FIELD_DATE_MODIFIED;
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
        public bool IsValidPurchaseID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_PURCHASE_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = CartItem.DB_FIELD_PURCHASE_ID;
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
                clm.ColumnName = CartItem.DB_FIELD_USER_ID;
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
        public bool IsValidContentID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_CONTENT_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = CartItem.DB_FIELD_CONTENT_ID;
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
                clm.ColumnName = CartItem.DB_FIELD_CONTENT_TYPE_ID;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
    }
}
// END OF CLASS FILE