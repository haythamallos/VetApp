using MainSite.Models;
using System;
using System.Web.Mvc;
using MainSite.Utils;
using MainSite.Classes;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.BusinessAccessLayer;
using System.Web;
using Stripe;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MainSite.Controllers
{
    public class DashboardController : Controller
    {
        private Config _config = null;

        private User Auth()
        {
            User user = null;
            if (!IsCookieEnabled())
            {
                LogOut();
            }
            else
            {
                string userguid = GetCookieFieldValue(CookieManager.COOKIE_FIELD_USER_GUID);
                if (!string.IsNullOrEmpty(userguid))
                {
                    BusFacCore busFacCore = new BusFacCore();
                    user = busFacCore.UserGet(userguid);
                }
            }
            return user;
        }
        public DashboardController()
        {
            _config = new Config();

            var MovementList90Deg = new SelectList(new[] { 90, 55, 25 });
            ViewBag.MovementList90Deg = MovementList90Deg;

            var MovementList30Deg = new SelectList(new[] { 30, 20, 10 });
            ViewBag.MovementList30Deg = MovementList30Deg;

            var CurrentRatingsList = new SelectList(new[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });
            ViewBag.CurrentRatingsList = CurrentRatingsList;

        }
        // GET: Dashboard
        public ActionResult Index()
        {
            User user = Auth();
            DashboardModel dashboardModel = new DashboardModel();
            if (user != null)
            {
                BusFacCore busFacCore = new BusFacCore();
                dashboardModel.BenefitStatuses = busFacCore.GetBenefitStatuses(user.UserID);

                Evaluation evaluation = busFacCore.EvaluationGet(user);
                dashboardModel.evaluationResults = new EvaluationResults();
                dashboardModel.evaluationModel = new EvaluationModel();
                UserModel userModel = UserToModel(user);
                dashboardModel.userModel = userModel;
                int currentRating = 0;
                if (user.CurrentRating > 0)
                {
                    currentRating = (int)user.CurrentRating;
                }
                else
                {
                    currentRating = (int)evaluation.CurrentRating;
                }
                dashboardModel.evaluationResults.CurrentRating = currentRating;

                int projectionFactor = 3;
                int delta = (100 - currentRating) / 10;
                if (delta < projectionFactor)
                {
                    projectionFactor = delta;
                }

                dashboardModel.evaluationResults.PotentialVARating = currentRating + (10 * projectionFactor);
                int amountIncreasePerMonth = 0;
                int cnt = 0;
                for (int i = currentRating + 10; i <= 100; i += 10)
                {
                    cnt++;
                    if (cnt <= projectionFactor)
                    {
                        amountIncreasePerMonth += RatingProjections.RatingTable_1[i].DeltaFromPrevious;
                    }
                    else
                    {
                        break;
                    }
                }
                dashboardModel.evaluationResults.IncreaseRating = dashboardModel.evaluationResults.PotentialVARating - dashboardModel.evaluationResults.CurrentRating;
                dashboardModel.evaluationResults.AmountIncreasePerMonth = amountIncreasePerMonth;
                dashboardModel.evaluationResults.AmountIncreasePerYear = amountIncreasePerMonth * 12;
                dashboardModel.evaluationResults.TotalPerMonthAfterIncrease = RatingProjections.RatingTable_1[dashboardModel.evaluationResults.PotentialVARating].TotalPerMonth;

                //ViewData["FormsSaved"] = "1";
            }


            return View(dashboardModel);

        }

        public ActionResult MainMenu()
        {
            return View();
        }
        public ActionResult PreForm(PreliminaryModel model)
        {
            try
            {
                if (model.ContentTypeID > 0)
                {
                    BusFacCore busFacCore = new BusFacCore();
                    model.contentType = busFacCore.ContentTypeGet(model.ContentTypeID);
                    switch (model.ContentTypeID)
                    {
                        case 1:
                            model.imageURL = "../Images/back-pain.jpg";
                            break;
                        case 2:
                            model.imageURL = "../Images/shoulder-pain.jpg";
                            break;
                        case 3:
                            model.imageURL = "../Images/neck-pain.jpg";
                            break;
                        default:
                            break;
                    }
                    model.AskSide = (bool)model.contentType.HasSides;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        //[HttpPost]
        //public ActionResult PreFormPost(PreliminaryModel model)
        //{
        //    try
        //    {
        //        BusFacCore busFacCore = new BusFacCore();
        //        model.contentType = busFacCore.ContentTypeGet(model.ContentTypeID);
        //        switch (model.ContentTypeID)
        //        {
        //            case 1:
        //                model.imageURL = "../Images/back-pain.jpg";
        //                break;
        //            case 2:
        //                model.imageURL = "../Images/shoulder-pain.jpg";
        //                break;
        //            case 3:
        //                model.imageURL = "../Images/neck-pain.jpg";
        //                break;
        //            default:
        //                break;
        //        }
        //        if (model.Rating >= model.contentType.MaxRating)
        //        {
        //            model.HasError = true;
        //            model.ErrorTitle = "Max Rating Reached";
        //            model.ErrorMsg = "You are currently at maximum rating for this benefit.  Try other body areas.";
        //        }
        //        else if (((bool)model.contentType.HasSides) && (string.IsNullOrEmpty(model.Side)))
        //        {
        //            model.HasError = true;
        //            model.ErrorTitle = "Info Needed";
        //            model.ErrorMsg = "Please choose the side of your disability.";
        //        }

        //        if ((!model.HasError) && (model.ContentTypeID > 0))
        //        {
        //            User user = Auth();
        //            string actionName = null;
        //            switch (model.ContentTypeID)
        //            {
        //                case 1:
        //                    user.HasRatingBack = true;
        //                    user.CurrentRatingBack = model.Rating;
        //                    actionName = "FormGetBack";
        //                    break;
        //                case 2:
        //                    user.HasRatingShoulder = true;
        //                    user.CurrentRatingShoulder = model.Rating;
        //                    actionName = "FormGetShoulder";
        //                    TempData["Side"] = model.Side;
        //                    break;
        //                case 3:
        //                    user.HasRatingNeck = true;
        //                    user.CurrentRatingNeck = model.Rating;
        //                    actionName = "FormGetNeck";
        //                    break;
        //                default:
        //                    break;
        //            }
        //            long lID = busFacCore.UserCreateOrModify(user);
        //            return RedirectToAction(actionName);
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View("PreForm", model);
        //}
        public ActionResult Preliminary(PreliminaryModel model)
        {
            return View(model);
        }

        public ActionResult FormQBack()
        {
            return View();
        }
        private UserModel UserToModel(User user)
        {
            UserModel userModel = null;
            userModel = new UserModel()
            {
                Username = user.Username,
                Password = user.Passwd,
                FullName = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Message = user.UserMessage,
                SSN = user.Ssn,
                //CurrentRatingBack = user.CurrentRatingBack,
                //CurrentRatingNeck = user.CurrentRatingNeck,
                //CurrentRatingShoulder = user.CurrentRatingShoulder,
                //HasRatingBack = (user.HasRatingBack == null) ? false : (bool)user.HasRatingBack,
                //HasRatingNeck = (user.HasRatingNeck == null) ? false : (bool)user.HasRatingNeck,
                //HasRatingShoulder = (user.HasRatingShoulder == null) ? false : (bool)user.HasRatingShoulder
            };
            int currentRating = 0;
            if (user.CurrentRating > 0)
            {
                currentRating = (int)user.CurrentRating;
            }
            else
            {
                BusFacCore busFacCore = new BusFacCore();
                Evaluation evaluation = busFacCore.EvaluationGet(user);
                if (evaluation != null)
                {
                    currentRating = (int)evaluation.CurrentRating;
                }
            }
            userModel.CurrentRating = currentRating;
            return userModel;
        }
        public ActionResult ProfileUpdate()
        {
            User user = Auth();
            UserModel userModel = UserToModel(user);
            return View(userModel);
        }
        public ActionResult ProfileUpdateAction(UserModel userModel)
        {
            User user = Auth();

            try
            {
                user.Fullname = userModel.FullName;
                user.Username = userModel.Username;
                if (!string.IsNullOrEmpty(userModel.SSN))
                {
                    if (!user.Ssn.Equals(userModel.SSN))
                    {
                        user.Ssn = UtilsSecurity.encrypt(userModel.SSN);
                    }
                }
                else
                {
                    user.Ssn = null;
                }

                user.PhoneNumber = userModel.PhoneNumber;
                if (!user.Passwd.Equals(userModel.Password))
                {
                    user.Passwd = UtilsSecurity.encrypt(userModel.Password);
                }
                user.UserMessage = userModel.Message;
                user.CurrentRating = userModel.CurrentRating;
                //user.CurrentRatingBack = userModel.CurrentRatingBack;
                //user.CurrentRatingShoulder = userModel.CurrentRatingShoulder;
                //user.CurrentRatingNeck = userModel.CurrentRatingNeck;
                BusFacCore busFacCore = new BusFacCore();
                long lID = busFacCore.UserCreateOrModify(user);

                if (lID > 0)
                {
                    ViewData["IsSaved"] = true;
                    user = busFacCore.UserGet(lID);
                    userModel = UserToModel(user);
                }
                else
                {
                    ViewData["IsSaved"] = false;
                }
            }
            catch (Exception ex) { }

            return View("ProfileUpdate", userModel);
        }
        public ActionResult Evaluation(EvaluationModel evaluationModel)
        {
            bool b = SetCookieField(CookieManager.COOKIE_FIELD_CURRENT_RATING, evaluationModel.CurrentRating.ToString());
            b = SetCookieField(CookieManager.COOKIE_FIELD_HAS_A_CLAIM, evaluationModel.HasAClaim.ToString());
            b = SetCookieField(CookieManager.COOKIE_FIELD_HAS_ACTIVE_APPEAL, evaluationModel.HasActiveAppeal.ToString());
            b = SetCookieField(CookieManager.COOKIE_FIELD_IS_FIRST_TIME_FILING, evaluationModel.IsFirstTimeFiling.ToString());
            b = SetCookieField(CookieManager.COOKIE_FIELD_ISNEW_EVAL, "true");
            return View("Register2");
        }
        public ActionResult Authenticate(UserModel userModel)
        {
            try
            {
                BusFacCore busFacCore = new BusFacCore();
                BusUser busUser = new BusUser();
                if ((busUser.IsValidUsername(userModel.Username)) && (busUser.IsValidPasswd(userModel.Password)))
                {
                    bool UserExist = busFacCore.Exist(userModel.Username);
                    if (UserExist)
                    {
                        User user = busFacCore.UserAuthenticate(userModel.Username, userModel.Password);
                        if ((user != null) && (user.UserID > 0))
                        {
                            AssociateEvaluationWithUser(user);
                            bool b = SetCookieField(CookieManager.COOKIE_FIELD_USER_GUID, user.CookieID);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["InvalidCredentials"] = true;
                        }
                    }
                    else
                    {
                        ViewData["UserExist"] = false;
                    }
                }
                else
                {
                    ViewData["InvalidCredentials"] = true;
                }

            }
            catch (Exception ex)
            {
                ViewData["HasError"] = true;
            }
            return View("Login2");
        }
        private void AssociateEvaluationWithUser(User user)
        {
            try
            {
                string IsNewEval = GetCookieFieldValue(CookieManager.COOKIE_FIELD_ISNEW_EVAL);
                if (!string.IsNullOrEmpty(IsNewEval) && (IsNewEval == "true"))
                {
                    EvaluationModel evaluationModel = new EvaluationModel();
                    evaluationModel.CurrentRating = Convert.ToInt32(GetCookieFieldValue(CookieManager.COOKIE_FIELD_CURRENT_RATING));
                    evaluationModel.HasAClaim = Convert.ToBoolean(GetCookieFieldValue(CookieManager.COOKIE_FIELD_HAS_A_CLAIM));
                    evaluationModel.HasActiveAppeal = Convert.ToBoolean(GetCookieFieldValue(CookieManager.COOKIE_FIELD_HAS_ACTIVE_APPEAL));
                    evaluationModel.IsFirstTimeFiling = Convert.ToBoolean(GetCookieFieldValue(CookieManager.COOKIE_FIELD_IS_FIRST_TIME_FILING));
                    BusFacCore busFacCore = new BusFacCore();
                    long lID = busFacCore.EvaluationCreate(evaluationModel, user.UserID);
                    if (lID > 0)
                    {
                        bool b = SetCookieField(CookieManager.COOKIE_FIELD_ISNEW_EVAL, "false");
                    }
                }
            }
            catch { }
        }
        public ActionResult Register(UserModel userModel)
        {
            try
            {
                BusFacCore busFacCore = new BusFacCore();
                BusUser busUser = new BusUser();
                if ((busUser.IsValidUsername(userModel.Username)) && (busUser.IsValidPasswd(userModel.Password)))
                {
                    if (!(userModel.Password.Equals(userModel.ConfirmPassword, StringComparison.Ordinal)))
                    {
                        ViewData["PasswordMatch"] = true;
                    }
                    else
                    {
                        bool UserExist = busFacCore.Exist(userModel.Username);
                        if (!UserExist)
                        {

                            User user = busFacCore.UserCreate(userModel.Username, userModel.Password);
                            if ((user != null) && (user.UserID > 0))
                            {
                                AssociateEvaluationWithUser(user);
                                bool b = SetCookieField(CookieManager.COOKIE_FIELD_USER_GUID, user.CookieID);
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewData["HasError"] = true;
                            }
                        }
                        else
                        {
                            ViewData["UserExist"] = true;
                        }
                    }
                }
                else
                {
                    ViewData["InvalidCredentials"] = true;
                }


            }
            catch (Exception ex)
            {
                ViewData["HasError"] = true;
            }
            return View("Register2");
        }
        private string GetTemplatePath(IBaseModel model)
        {
            string path = null;
            switch (model.GetType().Name)
            {
                case "BackModel":
                    path = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
                    break;
                default:
                    break;
            }
            return path;
        }
        public ActionResult FormGetBack()
        {
            string viewName = "BackForm";
            BackModel model = new BackModel();
            long contenttypeid = 1;
            try
            {
                string templatePath = GetTemplatePath(model);
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if (content == null)
                {
                    ContentID = FormSave(model, 0, contenttypeid);
                }
                else
                {
                    model = JSONHelper.Deserialize<BackModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }

                //if (!((bool)user.HasRatingBack))
                //{
                //    PreliminaryModel preliminaryModel = new PreliminaryModel() { ContentTypeID = 1 };
                //    return RedirectToAction("PreForm", preliminaryModel);
                //}
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult BackFormSave(BackModel model, long contentStateID)
        {
            try
            {
                long ContentID = FormSave(model, contentStateID, 1);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Back(pdfTemplatePath, model);
                    BusFacCore busFacCore = new BusFacCore();
                    Content content = busFacCore.ContentGet(ContentID);
                    content.ContentData = form;
                    long lID = busFacCore.ContentCreateOrModify(content);
                    if ((!busFacCore.HasError) && (lID > 0))
                    {
                        ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        return RedirectToAction("Product", productModel);
                    }
                    else
                    {
                        // encountered an error
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View("BackForm", model);
        }

        public ActionResult FormGetShoulder()
        {
            string viewName = "ShoulderForm";
            ShoulderModel model = new ShoulderModel();
            long contenttypeid = 2;
            try
            {
                string templatePath = GetTemplatePath(model);
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;

                if (content == null)
                {
                    ContentID = FormSave(model, 0, contenttypeid);
                }
                else
                {
                    model = JSONHelper.Deserialize<ShoulderModel>(content.ContentMeta);
                }

                if (TempData["Side"] != null)
                {
                    model.Side = (string)TempData["Side"];
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
                //if (!((bool)user.HasRatingShoulder))
                //{
                //    PreliminaryModel preliminaryModel = new PreliminaryModel() { ContentTypeID = 2 };
                //    return RedirectToAction("PreForm", preliminaryModel);
                //}

            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult ShoulderFormSave(ShoulderModel model, long contentStateID)
        {
            string viewName = "ShoulderForm";

            try
            {
                long ContentID = FormSave(model, contentStateID, 2);
                if (contentStateID == 6)
                {
                    // submit application
                }
            }
            catch (Exception ex)
            {

            }
            return View(viewName, model);
        }

        public ActionResult FormGetNeck()
        {
            string viewName = "NeckForm";
            NeckModel model = new NeckModel();
            long contenttypeid = 3;
            try
            {
                string templatePath = GetTemplatePath(model);
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if (content == null)
                {
                    ContentID = FormSave(model, 0, contenttypeid);
                }
                else
                {
                    model = JSONHelper.Deserialize<NeckModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
                //if (!((bool)user.HasRatingNeck))
                //{
                //    PreliminaryModel preliminaryModel = new PreliminaryModel() { ContentTypeID = 3 };
                //    return RedirectToAction("PreForm", preliminaryModel);
                //}
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult NeckFormSave(NeckModel model, long contentStateID)
        {
            string viewName = "NeckForm";

            try
            {
                long ContentID = FormSave(model, contentStateID, 3);
                if (contentStateID == 6)
                {
                    // submit application
                }
            }
            catch (Exception ex)
            {

            }
            return View(viewName, model);
        }

        private long FormSave(IBaseModel model, long contentStateID, long contentTypeID)
        {
            long ContentID = 0;
            BusFacPDF busFacPDF = new BusFacPDF();
            model.TemplatePath = GetTemplatePath(model);
            ContentID = busFacPDF.Save(model, contentStateID, contentTypeID);
            return ContentID;
        }

        [HttpPost]
        public ActionResult BackFormToPdf(BackModel backModel, string submitButton)
        {
            try
            {
                BusFacPDF busFacPDF = new BusFacPDF();
                string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
                User user = Auth();
                backModel.NameOfPatient = user.Fullname;
                backModel.SocialSecurity = user.Ssn;

                byte[] form = busFacPDF.Back(pdfTemplatePath, backModel);
                PDFHelper.ReturnPDF(form, "back-dbq.pdf");

            }
            catch (Exception ex)
            {

            }
            return View(backModel);
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }

        public string GetCookieFieldValue(string fieldName)
        {
            string value = null;
            try
            {
                HttpCookie cookie = Request.Cookies[CookieManager.COOKIENAME];
                value = cookie[fieldName];
            }
            catch { }
            return value;
        }

        public bool IsCookieEnabled()
        {
            bool isSuccess = false;
            try
            {
                HttpCookie cookie = Request.Cookies[CookieManager.COOKIENAME];
                if (cookie != null)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex) { }

            return isSuccess;
        }

        public bool SetCookieField(string fieldName, string fieldValue)
        {
            bool isSuccess = false;
            try
            {
                HttpCookie cookie = Request.Cookies[CookieManager.COOKIENAME];
                if (cookie != null)
                {
                    cookie[fieldName] = fieldValue;
                    isSuccess = true;
                    Response.Cookies.Add(cookie);
                }
            }
            catch { }

            return isSuccess;
        }

        public ActionResult SavedForm()
        {
            return View();
        }

        public ActionResult Product(ProductModel model, string submitButton)
        {
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                ContentType contentType = busFacCore.ContentTypeGet(model.ContentTypeID);
                Content content = busFacCore.ContentGetLatest(user.UserID, model.ContentTypeID);
                if (content != null)
                {
                    if ((submitButton == "ADDTOCART") || (submitButton == "CHECKOUT"))
                    {
                        CartItem cartItem = null;
                        cartItem = busFacCore.CartItemGet(user.UserID, content.ContentID);
                        if (cartItem == null)
                        {
                            cartItem = new CartItem() { UserID = user.UserID, ContentID = content.ContentID, ContentTypeID = content.ContentTypeID };
                            model.CartItemID = busFacCore.CartItemCreateOrModify(cartItem);
                            model.AlertMessageTitle = "Item Added to Cart!";
                            model.AlertMessageTitle = "Successfully added item to shopping cart.";
                        }
                        else
                        {
                            model.CartItemID = cartItem.CartItemID;
                            model.AlertMessageTitle = "Item Already Added";
                            model.AlertMessageTitle = "This item has already been added to the cart.";
                        }

                        if (submitButton == "CHECKOUT")
                        {
                            return RedirectToAction("ProductCart");
                        }
                    }
                    busFacCore.SetProductModel(model, content, user, contentType);
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        //private ProductCartModel GetUserCart(User user)
        //{
        //    BusFacCore busFacCore = new BusFacCore();
        //    ProductCartModel productCartModel = new ProductCartModel();
        //    List<CartItem> lstCartItem = busFacCore.CartItemPendingUserList(user.UserID);
        //    ProductModel productModel = null;
        //    productCartModel.TotalPrice = 0;
        //    Content content = null;
        //    ContentType contentType = null;
        //    foreach (CartItem cartItem in lstCartItem)
        //    {
        //        productModel = new ProductModel();
        //        productModel.CartItemID = cartItem.CartItemID;
        //        content = busFacCore.ContentGet(cartItem.ContentID);
        //        contentType = busFacCore.ContentTypeGet(cartItem.ContentTypeID);
        //        SetProductModel(productModel, content, user, contentType);
        //        productCartModel.TotalPrice += contentType.Price;
        //        productCartModel.lstProductModel.Add(productModel);
        //    }
        //    productCartModel.TotalPriceText = String.Format("{0:c2}", productCartModel.TotalPrice);
        //    return productCartModel;
        //}
        //private void SetProductModel(ProductModel productModel, Content content, User user, ContentType contentType)
        //{
        //    BusFacCore busFacCore = new BusFacCore();
        //    productModel.ContentTypeID = content.ContentTypeID;
        //    productModel.ContentID = content.ContentTypeID;
        //    content.UserID = user.UserID;
        //    productModel.ProductName = contentType.VisibleCode;
        //    productModel.Price = String.Format("{0:c2}", contentType.Price);
        //    productModel.ImagePath = "../Images/" + contentType.Code + "/Png/0.png";
        //    productModel.ProductRefName = contentType.ProductRefName;
        //    productModel.ProductRefDescription = contentType.ProductRefDescription;
        //    productModel.NumberOfPages = (int)contentType.NumberOfPages;
        //}

        //[HttpPost]
        //public ActionResult ProductCheckout(ProductCheckoutModel model)
        //{
        //    try
        //    {
        //        User user = Auth();
        //        BusFacCore busFacCore = new BusFacCore();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View(model);
        //}

        public ActionResult ProductCart(string id)
        {
            ProductCartModel model = null;
            BusFacCore busFacCore = new BusFacCore();
            try
            {
                User user = Auth();
                if (!string.IsNullOrEmpty(id))
                {
                    if (id == "CONTINUE")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        long CartItemID = 0;
                        if (long.TryParse(id, out CartItemID))
                        {
                            CartItem cartItem = busFacCore.CartItemGet(CartItemID);
                            if ((cartItem != null) && (cartItem.CartItemID > 0))
                            {
                                if ((cartItem.UserID == user.UserID))
                                {
                                    busFacCore.CartItemRemove(cartItem.CartItemID);
                                }
                            }
                        }

                    }
                }
                model = busFacCore.GetUserCart(user);
                model.StripeApiKey = _config.StripeApiKey;
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Charge(string stripeToken)
        {
            // charge
            ChargeModel chargeModel = new ChargeModel() { Authtoken = stripeToken };
            BusFacCore busFacCore = new BusFacCore();
            Purchase purchase = new Purchase() { IsError = true, IsSuccess = false };
            StringBuilder sbError = new StringBuilder();
            ProductCartModel cart = null;
            chargeModel.HasError = true;
            try
            {
                User user = Auth();
                cart = busFacCore.GetUserCart(user);

                StripeConfiguration.SetApiKey(_config.StripeSecretKey);
                var myCharge = new StripeChargeCreateOptions();

                // always set these properties
                // convert the amount to pennies
                myCharge.Amount = cart.TotalPriceInPennies;
                myCharge.Currency = "usd";

                // set this if you want to
                myCharge.Description = "Veteransapp test charge for " + user.Username;

                myCharge.SourceTokenOrExistingSourceId = chargeModel.Authtoken;

                var chargeService = new StripeChargeService();
                StripeCharge stripeCharge = chargeService.Create(myCharge);
                purchase.AmountInPennies = cart.TotalPriceInPennies;
                purchase.Authtoken = chargeModel.Authtoken;
                purchase.NumItemsInCart = cart.lstProductModel.Count;
                purchase.ResponseJson = stripeCharge.StripeResponse.ResponseJson;

                chargeModel.Amount = String.Format("{0:c2}", (cart.TotalPriceInPennies / 100));

                if (stripeCharge.Status == "succeeded")
                {
                    purchase.IsSuccess = true;
                    purchase.IsError = false;
                    chargeModel.HasError = false;
                }
                else
                {
                    // error in charge
                    purchase.IsSuccess = false;
                    purchase.IsError = true;
                    sbError.Append("STATUS_ERROR:  " + stripeCharge.Status + Environment.NewLine);
                }

            }
            catch (StripeException exception)
            {
                purchase.IsError = true;
                switch (exception.StripeError.ErrorType)
                {
                    case "card_error":
                        //do some stuff, set your lblError or something like this
                        ModelState.AddModelError(exception.StripeError.Code, exception.StripeError.Message);
                        sbError.Append("CARD_ERROR:  " + exception.StripeError.Code + Environment.NewLine + exception.StripeError.Message + Environment.NewLine);
                        // or better yet, handle based on error code: exception.StripeError.Code
                        break;
                    case "api_error":
                        sbError.Append("API_ERROR" + Environment.NewLine);
                        break;
                    case "invalid_request_error":
                        sbError.Append("INVALID_REQUEST_ERROR" + Environment.NewLine);
                        break;
                    default:
                        throw;
                }
            }
            catch (Exception ex)
            {
                sbError.Append("EXCEPTION:  " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
            }
            finally
            {
                if (sbError.Length > 0)
                {
                    purchase.ErrorTrace = sbError.ToString();
                }
                long lPurchaseID = busFacCore.PurchaseCreateOrModify(purchase);
                CartItem cartItem = null;
                Content content = null;
                long lID = 0;
                foreach (ProductModel p in cart.lstProductModel)
                {
                    cartItem = busFacCore.CartItemGet(p.CartItemID);
                    cartItem.PurchaseID = lPurchaseID;
                    lID = busFacCore.CartItemCreateOrModify(cartItem);

                    content = busFacCore.ContentGet(p.ContentID);
                    if ((bool)purchase.IsSuccess)
                    {
                        content.PurchaseID = lPurchaseID;
                        content.ContentStateID = 7;
                        content.Authtoken = stripeToken;
                        content.IsDisabled = false;
                    }
                    else
                    {
                        content.ErrorPurchaseID = lPurchaseID;
                    }
                    lID = busFacCore.ContentCreateOrModify(content);
                }

            }

            return View(chargeModel); ;
        }

        public ActionResult PurchasedForm(PurchasesModel model)
        {
            BusFacCore busFacCore = new BusFacCore();
            try
            {
                User user = Auth();

            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult RatingsCapture(PreliminaryModel model)
        {
            ContentType returnContentType = null;
            try
            {
                User user = Auth();
                if (!((bool)user.HasCurrentRating))
                {
                    returnContentType = new ContentType();
                }
                else
                {
                    BusFacCore busFacCore = new BusFacCore();
                    List<ContentType> lstContentType = busFacCore.ContentTypeGetList();
                    List<ContentType> lstContentTypeOfUser = busFacCore.ContentTypeGetList(user);
                    foreach (ContentType ct in lstContentType)
                    {
                        // does user have an entry?
                        if (lstContentTypeOfUser.Any(x => x.ContentTypeID != ct.ContentTypeID))
                        {
                            returnContentType = ct;
                            model.contentType = returnContentType;
                            model.AskSide = (bool)model.contentType.HasSides;
                            break;
                        }
                    }
                    if (returnContentType == null)
                    {
                        user.IsRatingProfileFinished = true;
                        long lUserID = busFacCore.UserCreateOrModify(user);
                        model.IsProfileFinished = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            model.ContentTypeID = returnContentType.ContentTypeID;
            model.imageURL = GetContentTypeImageUrl(model.ContentTypeID);

            return View(model);
        }

        [HttpPost]
        public ActionResult RatingsCapturePost(PreliminaryModel model, string submitID)
        {
            try
            {
                long lParsedContentTypeID = 0;
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                long lID = 0;
                if (submitID == "SUBMITRATING")
                {
                    user.HasCurrentRating = true;
                    user.CurrentRating = model.Rating;
                    lID = busFacCore.UserCreateOrModify(user);
                }
                else if (submitID == "NORATING")
                {
                    user.HasCurrentRating = true;
                    user.CurrentRating = 0;
                    lID = busFacCore.UserCreateOrModify(user);
                }
                else if (long.TryParse(submitID, out lParsedContentTypeID))
                {
                    // Update user with content type
                    ContentType contentType = busFacCore.ContentTypeGet(lParsedContentTypeID);
                    List<ContentType> lstContentTypeOfUser = busFacCore.ContentTypeGetList(user);
                    JctUserContentType jctUserContentType = null;
                    if (lstContentTypeOfUser.Any(x => x.ContentTypeID != contentType.ContentTypeID))
                    {
                        jctUserContentType = new JctUserContentType() { UserID = user.UserID, ContentTypeID = lParsedContentTypeID, Rating = model.Rating, SideID = model.Side };
                        long lJctUserContentTypeID = busFacCore.JctUserContentTypeCreateOrModify(jctUserContentType);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("RatingsCapture", model);
        }

        private string GetContentTypeImageUrl(long contentTypeID)
        {
            string imageUrl = "../Images/";
            switch (contentTypeID)
            {
                case 0:
                    imageUrl += "ebenefits.jpg";
                    break;
                case 1:
                    imageUrl += "back-pain.jpg";
                    break;
                case 2:
                    imageUrl += "shoulder-pain.jpg";
                    break;
                case 3:
                    imageUrl += "neck-pain.jpg";
                    break;
                default:
                    break;
            }
            return imageUrl;
        }

        //[HttpPost]
        //public ActionResult Charge(string stripeEmail, string stripeToken)
        //{
        //    //StripeConfiguration.SetApiKey()
        //    var customers = new StripeCustomerService();
        //    var charges = new StripeChargeService();

        //    var customer = customers.Create(new StripeCustomerCreateOptions
        //    {
        //        Email = stripeEmail,
        //        SourceToken = stripeToken
        //    });

        //    var charge = charges.Create(new StripeChargeCreateOptions
        //    {
        //        Amount = 500,
        //        Description = "Sample Charge",
        //        Currency = "usd",
        //        CustomerId = customer.Id
        //    });

        //    return View();
        //}

    }


}

//GeneratePDF_1(pdfTemplatePath, back);

//string sourceDir = System.IO.Path.GetDirectoryName(pdfTemplatePath);
//string[] fileEntries = Directory.GetFiles(sourceDir);
//string filenamenoext = null;
//foreach (string fileName in fileEntries)
//{
//    StringBuilder sb = new StringBuilder();
//    byte[] form = GeneratePDF_2(fileName, sb);
//    filenamenoext = Path.GetFileNameWithoutExtension(fileName);

//    System.IO.File.WriteAllBytes(sourceDir + Path.DirectorySeparatorChar + filenamenoext + "-mappings.pdf", form);
//    System.IO.File.WriteAllText(sourceDir + Path.DirectorySeparatorChar + filenamenoext + "-mappings.txt", sb.ToString());
//}

//if ((form != null) && (form.Length > 0))
//{
//    string s = sb.ToString();
//    PDFHelper.ReturnPDF(form, "back-test.pdf");

//}

// Get the form fields for this PDF and give them increasing values
//var formFieldMap = PDFHelper.GetFormFieldNames(pdfTemplatePath);

//var counter = 1;
//formFieldMap["form1[0].#subform[0].NameOfVeteran[0]"] = back.NameOfPatient;
//formFieldMap["form1[0].#subform[0].SSN[0]"] = back.SocialSecurity;
//formFieldMap["form1[0].#subform[0].Records[1]"] = back.BackDiagnosis;

//foreach (var key in new List<string>(formFieldMap.Keys))
//{
//    if ((key != null) && (key.IndexOf("CheckBox") != -1))
//    {
//        formFieldMap[key] = "True";
//    }
//}

//foreach (var key in new List<string>(formFieldMap.Keys))
//{
//    formFieldMap[key] = counter.ToString();
//    counter++;
//}

//var pdfContents = PDFHelper.GeneratePDF(pdfTemplatePath, formFieldMap);

//PDFHelper.ReturnPDF(pdfContents, "Back-Sample.pdf");

//Rectangle rectangle = pdfFormFields.GetFieldPositions(nameOfFieldCheckbox)[0].position;
//int page = pdfFormFields.GetFieldPositions(nameOfFieldCheckbox)[0].page;
////create a bold font
//iTextSharp.text.Font bold = FontFactory.GetFont(FontFactory.COURIER, 8f, iTextSharp.text.Font.BOLD);

////set the field to bold
//pdfFormFields.SetFieldProperty(nameOfField, "textfont", bold.BaseFont, null);

////set the text of the form field
//pdfFormFields.SetField(nameOfField, "This:  Will Be Displayed In The Field");

//string nameOfField = "form1[0].#subform[0].NameOfVeteran[0]";
//string nameOfFieldCheckbox = "form1[0].#subform[1].YesNo3[0]";

//AcroFields formAcroFields = pdfStamper.AcroFields;
//var fieldKeys = formAcroFields.Fields.Keys;
//String[] displayOptions = formAcroFields.GetListOptionDisplay(nameOfFieldCheckbox);
//String[] valueOptions = formAcroFields.GetListSelection(nameOfFieldCheckbox);
//foreach (string fieldKey in fieldKeys)
//{
//    //Change some data
//    if (fieldKey.Contains("Address"))
//    {
//        //formAcroFields.SetField(fieldKey, _data);
//    }
//}


//// Use iTextSharp PDF Reader, to get the fields and send to the 
////Stamper to set the fields in the document
//PdfReader pdfReader = new PdfReader(pdfTemplatePath);

//// Initialize Stamper (ms is a MemoryStream object)
//MemoryStream ms = new MemoryStream();
//PdfStamper pdfStamper = new PdfStamper(pdfReader, ms);

//// Get Reference to PDF Document Fields
//AcroFields pdfFormFields = pdfStamper.AcroFields;

////create a bold font
//iTextSharp.text.Font bold = FontFactory.GetFont(FontFactory.COURIER, 8f, iTextSharp.text.Font.BOLD);

////set the field to bold
//pdfFormFields.SetFieldProperty(nameOfField, "textfont", bold.BaseFont, null);

////set the text of the form field
//pdfFormFields.SetField(nameOfField, "This:  Will Be Displayed In The Field");

//// Set the flattening flag to false, so the document can continue to be edited
//pdfStamper.FormFlattening = true;

//// close the pdf stamper
//pdfStamper.Close();

//switch (submitButton)
//{
//    case "Save":
//        backModel.TemplatePath = pdfTemplatePath;
//        backModel.UserID = user.UserID;
//        long ContentID = busFacPDF.Save(backModel);
//        break;
//    case "Submit":
//        break;
//    case "PDF":
//        byte[] form = busFacPDF.Back(pdfTemplatePath, backModel);
//        PDFHelper.ReturnPDF(form, "back-dbq.pdf");
//        break;
//}

//[HttpPost]
//public ActionResult BackFormSubmit(BackModel backModel)
//{
//    try
//    {
//        //BusFacPDF busFacPDF = new BusFacPDF();
//        //backModel.TemplatePath = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
//        //long ContentID = busFacPDF.Save(backModel, 0);

//        //switch (submitButton)
//        //{
//        //    case "Save":
//        //        backModel.TemplatePath = pdfTemplatePath;
//        //        backModel.UserID = user.UserID;
//        //        long ContentID = busFacPDF.Save(backModel);
//        //        break;
//        //    case "Submit":
//        //        break;
//        //    case "PDF":
//        //        byte[] form = busFacPDF.Back(pdfTemplatePath, backModel);
//        //        PDFHelper.ReturnPDF(form, "back-dbq.pdf");
//        //        break;
//        //}

//    }
//    catch (Exception ex)
//    {

//    }
//    return View("BackForm", backModel);
//}

//switch (submitButton)
//{
//    case "Save":
//        backModel.TemplatePath = pdfTemplatePath;
//        backModel.UserID = user.UserID;
//        long ContentID = busFacPDF.Save(backModel);
//        break;
//    case "Submit":
//        break;
//    case "PDF":
//        byte[] form = busFacPDF.Back(pdfTemplatePath, backModel);
//        PDFHelper.ReturnPDF(form, "back-dbq.pdf");
//        break;
//}


//try
//{
//    HttpCookie cookie = new HttpCookie(CookieManager.COOKIENAME);
//    if (cookie != null)
//    {
//        cookie.Expires = DateTime.Now.AddDays(-1);
//        Response.Cookies.Add(cookie);
//    }
//}
//catch { }

//private byte[] GeneratePDF_2(string pdfTemplatePath, StringBuilder sb)
//{
//    byte[] form = null;

//    try
//    {
//        // Use iTextSharp PDF Reader, to get the fields and send to the 
//        //Stamper to set the fields in the document
//        PdfReader pdfReader = new PdfReader(pdfTemplatePath);

//        using (MemoryStream ms = new MemoryStream())
//        {
//            PdfStamper pdfStamper = null;

//            using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
//            {
//                // Get Reference to PDF Document Fields
//                AcroFields pdfFormFields = pdfStamper.AcroFields;

//                var counter = 1;
//                PdfContentByte content = null;
//                var fieldInfo = new List<string>();
//                foreach (var entry in pdfFormFields.Fields)
//                {
//                    var formFieldType = PDFFieldType.GetPDFFieldType(pdfFormFields.GetFieldType(entry.Key.ToString()));

//                    if (formFieldType is PDFCheckBoxFieldType)
//                        fieldInfo.Add(string.Format("{0} - {1} - Export Value: {2}",
//                                                    entry.Key,
//                                                    formFieldType,
//                                                    PDFHelper.GetExportValue(entry.Value as AcroFields.Item)));
//                    else
//                        fieldInfo.Add(string.Format("{0} - {1}", entry.Key, formFieldType));

//                    Rectangle rectangle = pdfFormFields.GetFieldPositions(entry.Key)[0].position;
//                    int page = pdfFormFields.GetFieldPositions(entry.Key)[0].page;
//                    //put content over
//                    content = pdfStamper.GetOverContent(page);
//                    //Text over the existing page
//                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA,
//                            BaseFont.WINANSI, BaseFont.EMBEDDED);
//                    content.BeginText();
//                    content.SetFontAndSize(bf, 8);
//                    //content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Page No: " + i, 200, 15, 0);
//                    content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, counter.ToString(), rectangle.Left, rectangle.Bottom, 0);
//                    content.EndText();
//                    counter++;
//                }

//                for (int i = 0; i < fieldInfo.Count; i++)
//                {
//                    sb.Append((i + 1) + ".  " + fieldInfo[i] + Environment.NewLine);
//                }


//            }

//            // Set the flattening flag to true, so the document is not editable
//            pdfStamper.FormFlattening = false;

//            // close the pdf stamper
//            pdfStamper.Close();

//            form = ms.ToArray();

//        }

//    }
//    catch (Exception ex)
//    {
//    }

//    return form;
//}
//private void GeneratePDF_1(string pdfTemplatePath, BackModel back)
//{
//    PDFFile pdfile = new PDFFile(pdfTemplatePath);


//    if ((!pdfile.HasError) && (pdfile.FieldInfoList.Count > 0))
//    {
//        List<byte[]> fileList = new List<byte[]>();

//        var counter = 1;
//        var counterChk = 1;
//        string oldval = null;
//        foreach (PDFFileField f in pdfile.FieldInfoList)
//        {
//            if (f.IsCheckBox)
//            {
//                oldval = pdfile.formFieldMap[f.Key];
//                pdfile.formFieldMap[f.Key] = f.ExportValue;
//                pdfile.formFieldMap["form1[0].#subform[0].NameOfVeteran[0]"] = counter.ToString() + ":" + f.fieldinfo();
//                var pdfContents = PDFHelper.GeneratePDF(pdfTemplatePath, pdfile.formFieldMap);
//                fileList.Add(pdfContents);
//                pdfile.formFieldMap[f.Key] = oldval;
//                counterChk++;
//            }
//            counter++;
//        }

//        using (MemoryStream msOutput = new MemoryStream())
//        {
//            PdfReader pdfFile = new PdfReader(fileList[0]);
//            Document doc = new Document();
//            PdfWriter pCopy = new PdfSmartCopy(doc, msOutput);

//            doc.Open();

//            for (int k = 0; k < fileList.Count; k++)
//            {
//                for (int i = 1; i < pdfFile.NumberOfPages + 1; i++)
//                {
//                    pdfFile = new PdfReader(fileList[k]);
//                    ((PdfSmartCopy)pCopy).AddPage(pCopy.GetImportedPage(pdfFile, i));
//                    pCopy.FreeReader(pdfFile);
//                }
//            }

//            pdfFile.Close();
//            pCopy.Close();
//            doc.Close();
//            fileList.Clear();

//            byte[] form = msOutput.ToArray();
//            PDFHelper.ReturnPDF(form, "back-merged.pdf");
//            //using (FileStream fileSteam = new FileStream(@"C:\Temp\Merged.pdf", FileMode.Create))
//            //{
//            //    fileStream.Write(form, 0, form.Length);
//            //}
//        }

//    }

//}

//public ActionResult BackFormGet(BackModel backModel)
//{
//    try
//    {
//        // check if content exists
//        User user = Auth();
//        BusFacCore busFacCore = new BusFacCore();
//        Content content = busFacCore.ContentGetLatest(user.UserID, 1);
//        long ContentID = 0;
//        backModel.UserID = user.UserID;
//        if (content == null)
//        {
//            ContentID = BackSave(backModel, 0);                    
//        }
//        else
//        {
//            backModel = JSONHelper.Deserialize<BackModel>(content.ContentMeta);
//        }

//        if (string.IsNullOrEmpty(backModel.NameOfPatient))
//        {
//            backModel.NameOfPatient = user.Fullname;
//        }
//        if (string.IsNullOrEmpty(backModel.SocialSecurity))
//        {
//            backModel.SocialSecurity = user.Ssn;
//        }
//    }
//    catch (Exception ex)
//    {

//    }
//    return View("BackForm", backModel);
//}

//private long BackSave(BackModel backModel, long contentStateID)
//{
//    long ContentID = 0;
//    BusFacPDF busFacPDF = new BusFacPDF();
//    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
//    backModel.TemplatePath = pdfTemplatePath;
//    ContentID = busFacPDF.Save(backModel, contentStateID, 1);
//    return ContentID;
//}