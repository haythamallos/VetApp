using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Vetapp.Engine.BusinessAccessLayer;
using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.DataAccessLayer.Enumeration;

using MainSite.Models;

namespace Vetapp.Engine.BusinessFacadeLayer
{
    public class BusFacCore
    {
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        private Config _config = null;

        public bool HasError
        {
            get { return _hasError; }
        }
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
        public BusFacCore()
        {
            _config = new Config();
        }

        public SqlConnection getDBConnection()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(_config.ConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                ErrorCode error = new ErrorCode();
                _hasError = true;
            }

            return conn;
        }
        public bool CloseConnection(SqlConnection pRefConn)
        {
            bool bReturn = true;
            try
            {
                if (pRefConn != null)
                {
                    if (pRefConn.State == ConnectionState.Open)
                    {
                        pRefConn.Close();
                    }
                }
                bReturn = true;
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
                bReturn = false;
            }
            return bReturn;
        }

        /*********************** CUSTOM BEGIN *********************/
        public bool Exist(string username)
        {
            bool bExist = true;
            ArrayList arUser = Find(username);
            if ((!HasError) && (arUser.Count == 0))
            {
                bExist = false;
            }
            return bExist;
        }
        public ArrayList Find(string username)
        {
            ArrayList arUser = null;
            try
            {
                if ((!string.IsNullOrEmpty(username)))
                {
                    EnumUser enumUser = new EnumUser() { Username = username };
                    arUser = UserGetList(enumUser);
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }
            return arUser;
        }

        public User UserCreate(string username, string password, long user_source_id = 0)
        {
            User user = null;
            try
            {
                string passwordEncrypted = UtilsSecurity.encrypt(password);
                User userTmp = new User() { Username = username, Passwd = passwordEncrypted, UserRoleID = 1, CookieID = Guid.NewGuid().ToString(), NumberOfVisits = 1, UserSourceID = user_source_id };
                long lID = UserCreateOrModify(userTmp);
                if (lID > 0)
                {
                    user = UserGet(lID);
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return user;
        }

        public User UserAuthenticate(string username, string password)
        {
            User user = null;
            try
            {
                ArrayList arUsers = Find(username);
                if ((arUsers != null) && (arUsers.Count == 1))
                {
                    User userTmp = (User) arUsers[0];

                    if (userTmp.Impersonate == null)
                    {
                        userTmp.Impersonate = false;
                    } 
                    if (!((bool) userTmp.Impersonate))
                    {
                        string passwordEncrypted = UtilsSecurity.encrypt(password);
                        if (userTmp.Passwd == passwordEncrypted)
                        {
                            user = userTmp;
                        }
                    }
                    else
                    {
                        user = userTmp;
                    }
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return user;
        }

        public long EvaluationCreate(EvaluationModel evaluationModel, long userid)
        {
            long lID = 0;
            try
            {
                Evaluation evaluation = new Evaluation()
                {
                    HasAClaim = evaluationModel.HasAClaim,
                    HasActiveAppeal = evaluationModel.HasActiveAppeal,
                    IsFirsttimeFiling = evaluationModel.IsFirstTimeFiling,
                    CurrentRating = evaluationModel.CurrentRating,
                    UserID = userid
                };
                lID = EvaluationCreateOrModify(evaluation);
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return lID;
        }

        public Evaluation EvaluationGet(User user)
        {
            Evaluation evaluation = null;
            if ((user != null) && (user.UserID > 0))
            {
                EnumEvaluation enumEvaluation = new EnumEvaluation() { UserID = user.UserID };
                ArrayList arEvaluation = EvaluationGetList(enumEvaluation);
                if ((arEvaluation != null) && (arEvaluation.Count > 0))
                {
                    evaluation = (Evaluation)arEvaluation[arEvaluation.Count - 1];
                }
            }
            if (evaluation == null)
            {
                evaluation = new Evaluation() { UserID = user.UserID, CurrentRating = 0 };
            }
            return evaluation;
        }

        public User UserGet(string cookieid)
        {
            User user = null;
            if (!string.IsNullOrEmpty(cookieid))
            {
                EnumUser enumUser = new EnumUser() { CookieID = cookieid };
                ArrayList arUser = UserGetList(enumUser);
                if ((arUser != null) && (arUser.Count == 1))
                {
                    user = (User)arUser[0];
                }
            }
            return user;
        }

        public LayoutData GetLayoutData(string userguid, string sourceUserguid)
        {
            LayoutData layoutData = new LayoutData();
            if (!string.IsNullOrEmpty(userguid))
            {
                User user = UserGet(userguid);
                if (user != null)
                {
                    layoutData.user = user;
                    EnumContent enumContent = enumContent = new EnumContent() { UserID = user.UserID, IsDisabled = false };
                    enumContent.SP_ENUM_NAME = "spContentEnum1";
                    ArrayList arContent = ContentGetList(enumContent);
                    if (arContent != null)
                    {
                        int cntSaved = 0;
                        int cntPurchased = 0;
                        foreach (Content c in arContent)
                        {
                            if (c.ContentStateID <= 6)
                            {
                                cntSaved++;
                            }
                            if ((c.ContentStateID == 7) || (c.ContentStateID == 8))
                            {
                                cntPurchased++;
                            }
                        }
                        layoutData.NumSavedForms = cntSaved;
                        layoutData.NumPurchasedForms = cntPurchased;
                    }
                    enumContent = null;

                    layoutData.UserCart = GetUserCart(user);

                    if (!string.IsNullOrEmpty(sourceUserguid))
                    {
                        User sourceUser = UserGet(sourceUserguid);
                        if (sourceUser != null)
                        {
                            layoutData.sourceUser = sourceUser;
                        }
                    }

                }
            }
            return layoutData;
        }

        public Dictionary<long, ContentDashboard> GetContentDashboard(string userguid)
        {
            Dictionary<long, ContentDashboard> contentDashboardDictionary = new Dictionary<long, ContentDashboard>();
            if (!string.IsNullOrEmpty(userguid))
            {
                User user = UserGet(userguid);
                if (user != null)
                {
                    BusFacCore busFacCore = new BusFacCore();
                    EnumContentType enumContentType = enumContentType = new EnumContentType();
                    ArrayList arContentType = busFacCore.ContentTypeGetList(enumContentType);
                    if (arContentType != null)
                    {
                        ContentDashboard contentDashboard = null;
                        foreach (ContentType ct in arContentType)
                        {
                            if (!contentDashboardDictionary.ContainsKey(ct.ContentTypeID))
                            {
                                contentDashboard = new ContentDashboard() { contentType = ct };
                                contentDashboardDictionary.Add(ct.ContentTypeID, contentDashboard);
                            }
                        }

                        EnumContent enumContent = enumContent = new EnumContent() { UserID = user.UserID, IsDisabled = false };
                        enumContent.SP_ENUM_NAME = "spContentEnum1";
                        ArrayList arContent = busFacCore.ContentGetList(enumContent);
                        if (arContent != null)
                        {
                            Content content = null;
                            for (int i = 0; i < arContent.Count; i++)
                            {
                                content = (Content)arContent[i];
                                if (contentDashboardDictionary.ContainsKey(content.ContentTypeID))
                                {
                                    contentDashboardDictionary[content.ContentTypeID].content = content;
                                    switch (content.ContentStateID)
                                    {
                                        case 0:
                                            contentDashboardDictionary[content.ContentTypeID].ActionText = "Apply";
                                            break;
                                        case 1:
                                            contentDashboardDictionary[content.ContentTypeID].ActionText = "Start";
                                            break;
                                        case 2:
                                        case 3:
                                        case 4:
                                        case 5:
                                            contentDashboardDictionary[content.ContentTypeID].ActionText = "Finish";
                                            break;
                                        case 6:
                                            contentDashboardDictionary[content.ContentTypeID].ActionText = "Buy";
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        enumContent = null;
                    }
                }
            }
            return contentDashboardDictionary;
        }

        public Content ContentGetLatest(long UserID, long ContentTypeID)
        {
            Content content = null;
            EnumContent enumContent = new EnumContent() { UserID = UserID, ContentTypeID = ContentTypeID };
            BusFacCore busFacCore = new BusFacCore();
            ArrayList arContent = busFacCore.ContentGetList(enumContent);
            if ((arContent != null) && (arContent.Count > 0))
            {
                content = (Content)arContent[arContent.Count - 1];
            }
            return content;
        }

        private bool GetIsServiceConnected(List<JctUserContentType> lstJctUserContentType, long pContentTypeID)
        {
            bool b = false;
            JctUserContentType result = lstJctUserContentType.LastOrDefault(x => x.ContentTypeID == pContentTypeID);
            if (result != null)
            {
                if (result.IsConnected == null)
                {
                    b = false;
                }
                else
                {
                    b = (bool)result.IsConnected;
                }
            }
            return b;
        }
        public Dictionary<long, BenefitStatus> GetBenefitStatuses(long UserID)
        {
            Dictionary<long, BenefitStatus> dictBenefitStatuses = new Dictionary<long, BenefitStatus>();
            Dictionary<long, ContentState> dictContentStates = new Dictionary<long, ContentState>();
            List<JctUserContentType> lstJctUserContentType = new List<JctUserContentType>();

            EnumContentType enumContentType = new EnumContentType();
            ArrayList arContentType = ContentTypeGetList(enumContentType);

            EnumContentState enumContentState = new EnumContentState();
            ArrayList arContentState = ContentStateGetList(enumContentState);

            foreach (ContentState cs in arContentState)
            {
                dictContentStates.Add(cs.ContentStateID, cs);
            }

            EnumJctUserContentType enumJctUserContentType = new EnumJctUserContentType();
            enumJctUserContentType.UserID = UserID;
            ArrayList arJctUserContentType = JctUserContentTypeGetList(enumJctUserContentType);
            if (arJctUserContentType != null)
            {
                lstJctUserContentType = arJctUserContentType.Cast<JctUserContentType>().ToList();
            }

            BenefitStatus benefitStatus = null;
            Content content = null;

            foreach (ContentType ct in arContentType)
            {
                bool IsServiceConnected = GetIsServiceConnected(lstJctUserContentType, ct.ContentTypeID);
                benefitStatus = new BenefitStatus() { Key = ct.ContentTypeID, ActionText = "Start", Progress = "0", TooltipText = "Start Application", BenefitName = ct.VisibleCode, BenefitCode = ct.Code, IsConnected = IsServiceConnected, MaxRating = ct.MaxRating };
                JctUserContentType jct = lstJctUserContentType.LastOrDefault(x => x.ContentTypeID == ct.ContentTypeID);
                if (jct != null)
                {
                    benefitStatus.CurrentRating = jct.Rating;
                    benefitStatus.DeltaRating = ct.MaxRating - jct.Rating;
                    if (benefitStatus.DeltaRating < 0)
                    {
                        benefitStatus.DeltaRating = 0;
                    }
                }

                content = ContentGetLatest(UserID, ct.ContentTypeID);
                if (content != null)
                {
                    benefitStatus.ContentStateID = content.ContentStateID;
                    if ((content.ContentStateID > 0) && (content.ContentStateID <= 5))
                    {
                        //benefitStatus.ActionText = "Finish";
                        //benefitStatus.TooltipText = "Finish Application";
                        benefitStatus.ActionText = "Continue";
                        benefitStatus.TooltipText = "Continue Application";
                        benefitStatus.Progress = dictContentStates[content.ContentStateID].Code;
                    }
                    else if (content.ContentStateID == 6)
                    {
                        //benefitStatus.ActionText = "Purchase";
                        //benefitStatus.TooltipText = "Purchase Form";
                        benefitStatus.ActionText = "Continue";
                        benefitStatus.TooltipText = "Continue Form";
                        benefitStatus.Progress = "100";
                    }
                    else if (content.ContentStateID == 7)
                    {
                        benefitStatus.ActionText = "Done";
                        benefitStatus.TooltipText = "Form Submitted";
                        benefitStatus.Progress = "100";
                    }
                }
                dictBenefitStatuses.Add(ct.ContentTypeID, benefitStatus);
            }
            dictBenefitStatuses = dictBenefitStatuses.OrderByDescending(x => x.Value.DeltaRating).ToDictionary(x => x.Key, x => x.Value);
            return dictBenefitStatuses;
        }

        public CartItem CartItemGet(long UserID, long ContentID)
        {
            CartItem cartItem = null;
            EnumCartItem enumCartItem = new EnumCartItem() { UserID = UserID, ContentID = ContentID, PurchaseID = 0 };
            BusFacCore busFacCore = new BusFacCore();
            ArrayList arCartItem = busFacCore.CartItemGetList(enumCartItem);
            if ((arCartItem != null) && (arCartItem.Count > 0))
            {
                cartItem = (CartItem)arCartItem[arCartItem.Count - 1];
            }
            return cartItem;
        }

        public List<CartItem> CartItemPendingUserList(long UserID)
        {
            List<CartItem> lstCartItem = new List<CartItem>();
            EnumCartItem enumCartItem = new EnumCartItem() { UserID = UserID, PurchaseID = 0 };
            BusFacCore busFacCore = new BusFacCore();
            ArrayList arCartItem = busFacCore.CartItemGetList(enumCartItem);
            if ((arCartItem != null) && (arCartItem.Count > 0))
            {
                foreach (CartItem cartItem in arCartItem)
                {
                    lstCartItem.Add(cartItem);
                }
            }
            return lstCartItem;
        }

        public ProductCartModel GetUserCart(User user)
        {
            ProductCartModel productCartModel = new ProductCartModel();
            List<CartItem> lstCartItem = CartItemPendingUserList(user.UserID);
            ProductModel productModel = null;
            productCartModel.TotalPriceInPennies = 0;
            Content content = null;
            ContentType contentType = null;
            foreach (CartItem cartItem in lstCartItem)
            {
                productModel = new ProductModel();
                productModel.CartItemID = cartItem.CartItemID;
                content = ContentGet(cartItem.ContentID);
                contentType = ContentTypeGet(cartItem.ContentTypeID);
                SetProductModel(productModel, content, user, contentType);
                productCartModel.TotalPriceInPennies += (int)contentType.PriceInPennies;
                productCartModel.lstProductModel.Add(productModel);
            }
            productCartModel.TotalPriceText = String.Format("{0:c2}", (productCartModel.TotalPriceInPennies / 100));
            return productCartModel;
        }
        public void SetProductModel(ProductModel productModel, Content content, User user, ContentType contentType)
        {
            productModel.ContentTypeID = content.ContentTypeID;
            productModel.ContentID = content.ContentTypeID;
            content.UserID = user.UserID;
            productModel.ProductName = contentType.VisibleCode;
            productModel.Price = String.Format("{0:c2}", (contentType.PriceInPennies / 100));
            productModel.ImagePath = "../Images/" + contentType.Code + "/Png/0.png";
            productModel.ProductRefName = contentType.ProductRefName;
            productModel.ProductRefDescription = contentType.ProductRefDescription;
            productModel.NumberOfPages = (int)contentType.NumberOfPages;
        }

        public PurchasesModel GetPurchasedItems(string userguid)
        {
            PurchasesModel purchasesModel = new PurchasesModel();

            LayoutData layoutData = new LayoutData();
            if (!string.IsNullOrEmpty(userguid))
            {
                User user = UserGet(userguid);
                if (user != null)
                {
                    EnumContent enumContent = enumContent = new EnumContent() { UserID = user.UserID, IsDisabled = false };
                    enumContent.SP_ENUM_NAME = "spContentEnum1";
                    ArrayList arContent = ContentGetList(enumContent);
                    if (arContent != null)
                    {
                        foreach (Content c in arContent)
                        {
                            if ((c.ContentStateID == 7) || (c.ContentStateID == 8))
                            {
                            }
                        }
                    }
                    enumContent = null;

                }
            }
            return purchasesModel;
        }

        public List<ContentType> ContentTypeGetList()
        {
            List<ContentType> lstContentType = null;
            EnumContentType enumContentType = new EnumContentType();
            ArrayList arContentType = ContentTypeGetList(enumContentType);
            if ((arContentType != null) && (arContentType.Count > 0))
            {
                lstContentType = arContentType.Cast<ContentType>().ToList();
            }
            else
            {
                lstContentType = new List<ContentType>();
            }
            return lstContentType;
        }

        public List<JctUserContentType> JctUserContentTypeGetList(User user)
        {
            List<JctUserContentType> lstJctUserContentType = new List<JctUserContentType>();
            EnumJctUserContentType enumJctUserContentType = new EnumJctUserContentType() { UserID = user.UserID };
            ArrayList arJctUserContentType = JctUserContentTypeGetList(enumJctUserContentType);
            if ((arJctUserContentType != null) && (arJctUserContentType.Count > 0))
            {
                lstJctUserContentType = arJctUserContentType.Cast<JctUserContentType>().ToList();
            }
            return lstJctUserContentType;
        }

        public List<User> Search(string pattern, long lSourceUserID)
        {
            List<User> lstUser = new List<User>();
            EnumUser enumUser = new EnumUser();
            enumUser.UserSourceID = lSourceUserID;
            enumUser.UserRoleID = 1;
            enumUser.Firstname = "%" + pattern + "%";
            enumUser.Fullname = "%" + pattern + "%";
            enumUser.Lastname = "%" + pattern + "%";
            enumUser.PhoneNumber = "%" + pattern + "%";
            enumUser.Username = "%" + pattern + "%";
            ArrayList arUser = UserGetList(enumUser, "spUserEnum1");
            if (arUser != null)
            {
                lstUser = arUser.Cast<User>().ToList();
            }
            return lstUser;
        }

        public ArrayList UserGetList(EnumUser pEnumUser, string spname)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.SP_ENUM_NAME = spname;
                items = busUser.Get(pEnumUser);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        public List<User> SearchAdmin(string pattern)
        {
            List<User> lstUser = new List<User>();
            EnumUser enumUser = new EnumUser();
            enumUser.Firstname = "%" + pattern + "%";
            enumUser.Fullname = "%" + pattern + "%";
            enumUser.Lastname = "%" + pattern + "%";
            enumUser.PhoneNumber = "%" + pattern + "%";
            enumUser.Username = "%" + pattern + "%";
            ArrayList arUser = UserGetList(enumUser, "spUserEnum2");
            if (arUser != null)
            {
                lstUser = arUser.Cast<User>().ToList();
            }
            return lstUser;
        }
        /*********************** CUSTOM END *********************/



        //------------------------------------------
        /// <summary>
        /// DbversionCreateOrModify
        /// </summary>
        /// <param name="">pDbversion</param>
        /// <returns>long</returns>
        /// 
        public long DbversionCreateOrModify(Dbversion pDbversion)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                busDbversion.Save(pDbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pDbversion.DbversionID;
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// DbversionGetList
        /// </summary>
        /// <param name="">pEnumDbversion</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList DbversionGetList(EnumDbversion pEnumDbversion)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                items = busDbversion.Get(pEnumDbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// DbversionGet
        /// </summary>
        /// <param name="">pLngDbversionID</param>
        /// <returns>Dbversion</returns>
        /// 
        public Dbversion DbversionGet(long pLngDbversionID)
        {
            Dbversion dbversion = new Dbversion() { DbversionID = pLngDbversionID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                busDbversion.Load(dbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return dbversion;
        }

        /// <summary>
        /// DbversionRemove
        /// </summary>
        /// <param name="">pDbversionID</param>
        /// <returns>void</returns>
        /// 
        public void DbversionRemove(long pDbversionID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Dbversion dbversion = new Dbversion();
                dbversion.DbversionID = pDbversionID;
                BusDbversion bus = null;
                bus = new BusDbversion(conn);
                bus.Delete(dbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// EvaluationCreateOrModify
        /// </summary>
        /// <param name="">pEvaluation</param>
        /// <returns>long</returns>
        /// 
        public long EvaluationCreateOrModify(Evaluation pEvaluation)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusEvaluation busEvaluation = null;
                busEvaluation = new BusEvaluation(conn);
                busEvaluation.Save(pEvaluation);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pEvaluation.EvaluationID;
                _hasError = busEvaluation.HasError;
                if (busEvaluation.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// EvaluationGetList
        /// </summary>
        /// <param name="">pEnumEvaluation</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList EvaluationGetList(EnumEvaluation pEnumEvaluation)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusEvaluation busEvaluation = null;
                busEvaluation = new BusEvaluation(conn);
                items = busEvaluation.Get(pEnumEvaluation);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busEvaluation.HasError;
                if (busEvaluation.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// EvaluationGet
        /// </summary>
        /// <param name="">pLngEvaluationID</param>
        /// <returns>Evaluation</returns>
        /// 
        public Evaluation EvaluationGet(long pLngEvaluationID)
        {
            Evaluation evaluation = new Evaluation() { EvaluationID = pLngEvaluationID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusEvaluation busEvaluation = null;
                busEvaluation = new BusEvaluation(conn);
                busEvaluation.Load(evaluation);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busEvaluation.HasError;
                if (busEvaluation.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return evaluation;
        }

        /// <summary>
        /// EvaluationRemove
        /// </summary>
        /// <param name="">pEvaluationID</param>
        /// <returns>void</returns>
        /// 
        public void EvaluationRemove(long pEvaluationID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Evaluation evaluation = new Evaluation();
                evaluation.EvaluationID = pEvaluationID;
                BusEvaluation bus = null;
                bus = new BusEvaluation(conn);
                bus.Delete(evaluation);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// SyslogCreateOrModify
        /// </summary>
        /// <param name="">pSyslog</param>
        /// <returns>long</returns>
        /// 
        public long SyslogCreateOrModify(Syslog pSyslog)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                busSyslog.Save(pSyslog);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pSyslog.SyslogID;
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// SyslogGetList
        /// </summary>
        /// <param name="">pEnumSyslog</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList SyslogGetList(EnumSyslog pEnumSyslog)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                items = busSyslog.Get(pEnumSyslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// SyslogGet
        /// </summary>
        /// <param name="">pLngSyslogID</param>
        /// <returns>Syslog</returns>
        /// 
        public Syslog SyslogGet(long pLngSyslogID)
        {
            Syslog syslog = new Syslog() { SyslogID = pLngSyslogID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                busSyslog.Load(syslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return syslog;
        }

        /// <summary>
        /// SyslogRemove
        /// </summary>
        /// <param name="">pSyslogID</param>
        /// <returns>void</returns>
        /// 
        public void SyslogRemove(long pSyslogID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Syslog syslog = new Syslog();
                syslog.SyslogID = pSyslogID;
                BusSyslog bus = null;
                bus = new BusSyslog(conn);
                bus.Delete(syslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// UserCreateOrModify
        /// </summary>
        /// <param name="">pUser</param>
        /// <returns>long</returns>
        /// 
        public long UserCreateOrModify(User pUser)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.Save(pUser);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pUser.UserID;
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// UserGetList
        /// </summary>
        /// <param name="">pEnumUser</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList UserGetList(EnumUser pEnumUser)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                items = busUser.Get(pEnumUser);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// UserGet
        /// </summary>
        /// <param name="">pLngUserID</param>
        /// <returns>User</returns>
        /// 
        public User UserGet(long pLngUserID)
        {
            User user = new User() { UserID = pLngUserID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.Load(user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return user;
        }

        /// <summary>
        /// UserRemove
        /// </summary>
        /// <param name="">pUserID</param>
        /// <returns>void</returns>
        /// 
        public void UserRemove(long pUserID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                User user = new User();
                user.UserID = pUserID;
                BusUser bus = null;
                bus = new BusUser(conn);
                bus.Delete(user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// UserRoleCreateOrModify
        /// </summary>
        /// <param name="">pUserRole</param>
        /// <returns>long</returns>
        /// 
        public long UserRoleCreateOrModify(UserRole pUserRole)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                busUserRole.Save(pUserRole);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pUserRole.UserRoleID;
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// UserRoleGetList
        /// </summary>
        /// <param name="">pEnumUserRole</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList UserRoleGetList(EnumUserRole pEnumUserRole)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                items = busUserRole.Get(pEnumUserRole);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// UserRoleGet
        /// </summary>
        /// <param name="">pLngUserRoleID</param>
        /// <returns>UserRole</returns>
        /// 
        public UserRole UserRoleGet(long pLngUserRoleID)
        {
            UserRole user_role = new UserRole() { UserRoleID = pLngUserRoleID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                busUserRole.Load(user_role);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return user_role;
        }

        /// <summary>
        /// UserRoleRemove
        /// </summary>
        /// <param name="">pUserRoleID</param>
        /// <returns>void</returns>
        /// 
        public void UserRoleRemove(long pUserRoleID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                UserRole user_role = new UserRole();
                user_role.UserRoleID = pUserRoleID;
                BusUserRole bus = null;
                bus = new BusUserRole(conn);
                bus.Delete(user_role);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }

        //------------------------------------------
        /// <summary>
        /// ContentCreateOrModify
        /// </summary>
        /// <param name="">pContent</param>
        /// <returns>long</returns>
        /// 
        public long ContentCreateOrModify(Content pContent)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContent busContent = null;
                busContent = new BusContent(conn);
                busContent.Save(pContent);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pContent.ContentID;
                _hasError = busContent.HasError;
                if (busContent.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// ContentGetList
        /// </summary>
        /// <param name="">pEnumContent</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList ContentGetList(EnumContent pEnumContent)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContent busContent = null;
                busContent = new BusContent(conn);
                items = busContent.Get(pEnumContent);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busContent.HasError;
                if (busContent.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// ContentGet
        /// </summary>
        /// <param name="">pLngContentID</param>
        /// <returns>Content</returns>
        /// 
        public Content ContentGet(long pLngContentID)
        {
            Content content = new Content() { ContentID = pLngContentID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContent busContent = null;
                busContent = new BusContent(conn);
                busContent.Load(content);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busContent.HasError;
                if (busContent.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return content;
        }

        /// <summary>
        /// ContentRemove
        /// </summary>
        /// <param name="">pContentID</param>
        /// <returns>void</returns>
        /// 
        public void ContentRemove(long pContentID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Content content = new Content();
                content.ContentID = pContentID;
                BusContent bus = null;
                bus = new BusContent(conn);
                bus.Delete(content);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }


        //------------------------------------------
        /// <summary>
        /// ContentTypeCreateOrModify
        /// </summary>
        /// <param name="">pContentType</param>
        /// <returns>long</returns>
        /// 
        public long ContentTypeCreateOrModify(ContentType pContentType)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContentType busContentType = null;
                busContentType = new BusContentType(conn);
                busContentType.Save(pContentType);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pContentType.ContentTypeID;
                _hasError = busContentType.HasError;
                if (busContentType.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// ContentTypeGetList
        /// </summary>
        /// <param name="">pEnumContentType</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList ContentTypeGetList(EnumContentType pEnumContentType)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContentType busContentType = null;
                busContentType = new BusContentType(conn);
                items = busContentType.Get(pEnumContentType);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busContentType.HasError;
                if (busContentType.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// ContentTypeGet
        /// </summary>
        /// <param name="">pLngContentTypeID</param>
        /// <returns>ContentType</returns>
        /// 
        public ContentType ContentTypeGet(long pLngContentTypeID)
        {
            ContentType content_type = new ContentType() { ContentTypeID = pLngContentTypeID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContentType busContentType = null;
                busContentType = new BusContentType(conn);
                busContentType.Load(content_type);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busContentType.HasError;
                if (busContentType.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return content_type;
        }

        /// <summary>
        /// ContentTypeRemove
        /// </summary>
        /// <param name="">pContentTypeID</param>
        /// <returns>void</returns>
        /// 
        public void ContentTypeRemove(long pContentTypeID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                ContentType content_type = new ContentType();
                content_type.ContentTypeID = pContentTypeID;
                BusContentType bus = null;
                bus = new BusContentType(conn);
                bus.Delete(content_type);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }

        //------------------------------------------
        /// <summary>
        /// ContentStateCreateOrModify
        /// </summary>
        /// <param name="">pContentState</param>
        /// <returns>long</returns>
        /// 
        public long ContentStateCreateOrModify(ContentState pContentState)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContentState busContentState = null;
                busContentState = new BusContentState(conn);
                busContentState.Save(pContentState);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pContentState.ContentStateID;
                _hasError = busContentState.HasError;
                if (busContentState.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// ContentStateGetList
        /// </summary>
        /// <param name="">pEnumContentState</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList ContentStateGetList(EnumContentState pEnumContentState)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContentState busContentState = null;
                busContentState = new BusContentState(conn);
                items = busContentState.Get(pEnumContentState);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busContentState.HasError;
                if (busContentState.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// ContentStateGet
        /// </summary>
        /// <param name="">pLngContentStateID</param>
        /// <returns>ContentState</returns>
        /// 
        public ContentState ContentStateGet(long pLngContentStateID)
        {
            ContentState content_state = new ContentState() { ContentStateID = pLngContentStateID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusContentState busContentState = null;
                busContentState = new BusContentState(conn);
                busContentState.Load(content_state);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busContentState.HasError;
                if (busContentState.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return content_state;
        }

        /// <summary>
        /// ContentStateRemove
        /// </summary>
        /// <param name="">pContentStateID</param>
        /// <returns>void</returns>
        /// 
        public void ContentStateRemove(long pContentStateID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                ContentState content_state = new ContentState();
                content_state.ContentStateID = pContentStateID;
                BusContentState bus = null;
                bus = new BusContentState(conn);
                bus.Delete(content_state);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }

        //------------------------------------------
        /// <summary>
        /// CartItemCreateOrModify
        /// </summary>
        /// <param name="">pCartItem</param>
        /// <returns>long</returns>
        /// 
        public long CartItemCreateOrModify(CartItem pCartItem)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusCartItem busCartItem = null;
                busCartItem = new BusCartItem(conn);
                busCartItem.Save(pCartItem);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pCartItem.CartItemID;
                _hasError = busCartItem.HasError;
                if (busCartItem.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// CartItemGetList
        /// </summary>
        /// <param name="">pEnumCartItem</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList CartItemGetList(EnumCartItem pEnumCartItem)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusCartItem busCartItem = null;
                busCartItem = new BusCartItem(conn);
                items = busCartItem.Get(pEnumCartItem);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busCartItem.HasError;
                if (busCartItem.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// CartItemGet
        /// </summary>
        /// <param name="">pLngCartItemID</param>
        /// <returns>CartItem</returns>
        /// 
        public CartItem CartItemGet(long pLngCartItemID)
        {
            CartItem cart_item = new CartItem() { CartItemID = pLngCartItemID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusCartItem busCartItem = null;
                busCartItem = new BusCartItem(conn);
                busCartItem.Load(cart_item);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busCartItem.HasError;
                if (busCartItem.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return cart_item;
        }

        /// <summary>
        /// CartItemRemove
        /// </summary>
        /// <param name="">pCartItemID</param>
        /// <returns>void</returns>
        /// 
        public void CartItemRemove(long pCartItemID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                CartItem cart_item = new CartItem();
                cart_item.CartItemID = pCartItemID;
                BusCartItem bus = null;
                bus = new BusCartItem(conn);
                bus.Delete(cart_item);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }

        //------------------------------------------
        /// <summary>
        /// PurchaseCreateOrModify
        /// </summary>
        /// <param name="">pPurchase</param>
        /// <returns>long</returns>
        /// 
        public long PurchaseCreateOrModify(Purchase pPurchase)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusPurchase busPurchase = null;
                busPurchase = new BusPurchase(conn);
                busPurchase.Save(pPurchase);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pPurchase.PurchaseID;
                _hasError = busPurchase.HasError;
                if (busPurchase.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// PurchaseGetList
        /// </summary>
        /// <param name="">pEnumPurchase</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList PurchaseGetList(EnumPurchase pEnumPurchase)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusPurchase busPurchase = null;
                busPurchase = new BusPurchase(conn);
                items = busPurchase.Get(pEnumPurchase);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busPurchase.HasError;
                if (busPurchase.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// PurchaseGet
        /// </summary>
        /// <param name="">pLngPurchaseID</param>
        /// <returns>Purchase</returns>
        /// 
        public Purchase PurchaseGet(long pLngPurchaseID)
        {
            Purchase purchase = new Purchase() { PurchaseID = pLngPurchaseID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusPurchase busPurchase = null;
                busPurchase = new BusPurchase(conn);
                busPurchase.Load(purchase);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busPurchase.HasError;
                if (busPurchase.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return purchase;
        }

        /// <summary>
        /// PurchaseRemove
        /// </summary>
        /// <param name="">pPurchaseID</param>
        /// <returns>void</returns>
        /// 
        public void PurchaseRemove(long pPurchaseID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Purchase purchase = new Purchase();
                purchase.PurchaseID = pPurchaseID;
                BusPurchase bus = null;
                bus = new BusPurchase(conn);
                bus.Delete(purchase);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }

        //------------------------------------------
        /// <summary>
        /// JctUserContentTypeCreateOrModify
        /// </summary>
        /// <param name="">pJctUserContentType</param>
        /// <returns>long</returns>
        /// 
        public long JctUserContentTypeCreateOrModify(JctUserContentType pJctUserContentType)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusJctUserContentType busJctUserContentType = null;
                busJctUserContentType = new BusJctUserContentType(conn);
                busJctUserContentType.Save(pJctUserContentType);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pJctUserContentType.JctUserContentTypeID;
                _hasError = busJctUserContentType.HasError;
                if (busJctUserContentType.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// JctUserContentTypeGetList
        /// </summary>
        /// <param name="">pEnumJctUserContentType</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList JctUserContentTypeGetList(EnumJctUserContentType pEnumJctUserContentType)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusJctUserContentType busJctUserContentType = null;
                busJctUserContentType = new BusJctUserContentType(conn);
                items = busJctUserContentType.Get(pEnumJctUserContentType);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busJctUserContentType.HasError;
                if (busJctUserContentType.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// JctUserContentTypeGet
        /// </summary>
        /// <param name="">pLngJctUserContentTypeID</param>
        /// <returns>JctUserContentType</returns>
        /// 
        public JctUserContentType JctUserContentTypeGet(long pLngJctUserContentTypeID)
        {
            JctUserContentType jct_user_content_type = new JctUserContentType() { JctUserContentTypeID = pLngJctUserContentTypeID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusJctUserContentType busJctUserContentType = null;
                busJctUserContentType = new BusJctUserContentType(conn);
                busJctUserContentType.Load(jct_user_content_type);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busJctUserContentType.HasError;
                if (busJctUserContentType.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return jct_user_content_type;
        }

        /// <summary>
        /// JctUserContentTypeRemove
        /// </summary>
        /// <param name="">pJctUserContentTypeID</param>
        /// <returns>void</returns>
        /// 
        public void JctUserContentTypeRemove(long pJctUserContentTypeID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                JctUserContentType jct_user_content_type = new JctUserContentType();
                jct_user_content_type.JctUserContentTypeID = pJctUserContentTypeID;
                BusJctUserContentType bus = null;
                bus = new BusJctUserContentType(conn);
                bus.Delete(jct_user_content_type);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }

        //------------------------------------------
        /// <summary>
        /// SideCreateOrModify
        /// </summary>
        /// <param name="">pSide</param>
        /// <returns>long</returns>
        /// 
        public long SideCreateOrModify(Side pSide)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSide busSide = null;
                busSide = new BusSide(conn);
                busSide.Save(pSide);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pSide.SideID;
                _hasError = busSide.HasError;
                if (busSide.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// SideGetList
        /// </summary>
        /// <param name="">pEnumSide</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList SideGetList(EnumSide pEnumSide)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSide busSide = null;
                busSide = new BusSide(conn);
                items = busSide.Get(pEnumSide);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSide.HasError;
                if (busSide.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// SideGet
        /// </summary>
        /// <param name="">pLngSideID</param>
        /// <returns>Side</returns>
        /// 
        public Side SideGet(long pLngSideID)
        {
            Side side = new Side() { SideID = pLngSideID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSide busSide = null;
                busSide = new BusSide(conn);
                busSide.Load(side);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSide.HasError;
                if (busSide.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return side;
        }

        /// <summary>
        /// SideRemove
        /// </summary>
        /// <param name="">pSideID</param>
        /// <returns>void</returns>
        /// 
        public void SideRemove(long pSideID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Side side = new Side();
                side.SideID = pSideID;
                BusSide bus = null;
                bus = new BusSide(conn);
                bus.Delete(side);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }

        //------------------------------------------
        /// <summary>
        /// JctUserUserCreateOrModify
        /// </summary>
        /// <param name="">pJctUserUser</param>
        /// <returns>long</returns>
        /// 
        public long JctUserUserCreateOrModify(JctUserUser pJctUserUser)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusJctUserUser busJctUserUser = null;
                busJctUserUser = new BusJctUserUser(conn);
                busJctUserUser.Save(pJctUserUser);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pJctUserUser.JctUserUserID;
                _hasError = busJctUserUser.HasError;
                if (busJctUserUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// JctUserUserGetList
        /// </summary>
        /// <param name="">pEnumJctUserUser</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList JctUserUserGetList(EnumJctUserUser pEnumJctUserUser)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusJctUserUser busJctUserUser = null;
                busJctUserUser = new BusJctUserUser(conn);
                items = busJctUserUser.Get(pEnumJctUserUser);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busJctUserUser.HasError;
                if (busJctUserUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// JctUserUserGet
        /// </summary>
        /// <param name="">pLngJctUserUserID</param>
        /// <returns>JctUserUser</returns>
        /// 
        public JctUserUser JctUserUserGet(long pLngJctUserUserID)
        {
            JctUserUser jct_user_user = new JctUserUser() { JctUserUserID = pLngJctUserUserID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusJctUserUser busJctUserUser = null;
                busJctUserUser = new BusJctUserUser(conn);
                busJctUserUser.Load(jct_user_user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busJctUserUser.HasError;
                if (busJctUserUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return jct_user_user;
        }

        /// <summary>
        /// JctUserUserRemove
        /// </summary>
        /// <param name="">pJctUserUserID</param>
        /// <returns>void</returns>
        /// 
        public void JctUserUserRemove(long pJctUserUserID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                JctUserUser jct_user_user = new JctUserUser();
                jct_user_user.JctUserUserID = pJctUserUserID;
                BusJctUserUser bus = null;
                bus = new BusJctUserUser(conn);
                bus.Delete(jct_user_user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
    }

    public class LayoutData
    {
        public int NumSavedForms { get; set; }
        public int NumPurchasedForms { get; set; }
        public bool IsProfileComplete { get; set; }
        public ProductCartModel UserCart { get; set; }
        public User user { get; set; }
        public User sourceUser { get; set; }
    }

    public class ContentDashboard
    {
        public Content content { get; set; }
        public ContentType contentType { get; set; }
        public string ActionText { get; set; }
    }

}
