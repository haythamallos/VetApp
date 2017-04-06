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

            var MovementList90Deg = new System.Web.Mvc.SelectList(new[] { 90, 55, 25 });
            ViewBag.MovementList90Deg = MovementList90Deg;

            var MovementList30Deg = new System.Web.Mvc.SelectList(new[] { 30, 20, 10 });
            ViewBag.MovementList30Deg = MovementList30Deg;

            var CurrentRatingsList = new System.Web.Mvc.SelectList(new[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });
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
                int currentRating = (int)user.CurrentRating;
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
                dashboardModel.evaluationResults.PotentialDelta = 100 - dashboardModel.evaluationResults.IncreaseRating - dashboardModel.evaluationResults.CurrentRating;
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
            UserModel userModel = new UserModel()
            {
                Username = user.Username,
                Password = user.Passwd,
                FullName = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Message = user.UserMessage,
                SSN = user.Ssn,
                InternalCalculatedRating = user.InternalCalculatedRating,
                CurrentRating = (int)user.CurrentRating,
                HasCurrentRating = (bool)user.HasCurrentRating,
                IsRatingProfileFinished = (bool)user.IsRatingProfileFinished
            };
            return userModel;
        }
        public ActionResult ProfileUpdate()
        {
            ProfileModel profileModel = new ProfileModel();
            User user = Auth();
            UserModel userModel = UserToModel(user);
            profileModel.userModel = userModel;
            return View(profileModel);
        }
        public ActionResult ProfileUpdateAction(ProfileModel profileModel)
        {
            User user = Auth();

            try
            {
                user.Fullname = profileModel.userModel.FullName;
                user.Username = profileModel.userModel.Username;
                if (!string.IsNullOrEmpty(profileModel.userModel.SSN))
                {
                    if (!user.Ssn.Equals(profileModel.userModel.SSN))
                    {
                        user.Ssn = UtilsSecurity.encrypt(profileModel.userModel.SSN);
                    }
                }
                else
                {
                    user.Ssn = null;
                }

                user.PhoneNumber = profileModel.userModel.PhoneNumber;
                if (!user.Passwd.Equals(profileModel.userModel.Password))
                {
                    user.Passwd = UtilsSecurity.encrypt(profileModel.userModel.Password);
                }
                user.UserMessage = profileModel.userModel.Message;
                user.CurrentRating = profileModel.userModel.CurrentRating;
                BusFacCore busFacCore = new BusFacCore();
                long lID = busFacCore.UserCreateOrModify(user);

                if (lID > 0)
                {
                    ViewData["IsSaved"] = true;
                    user = busFacCore.UserGet(lID);
                    profileModel.userModel = UserToModel(user);
                }
                else
                {
                    ViewData["IsSaved"] = false;
                }
            }
            catch (Exception ex) { }

            return View("ProfileUpdate", profileModel);
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
                    user.CurrentRating = evaluationModel.CurrentRating;
                    //user.HasCurrentRating = true;
                    lID = busFacCore.UserCreateOrModify(user);
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
            ContentType returnContentType = new ContentType(); ;
            try
            {
                User user = Auth();
                if ((bool)user.HasCurrentRating)
                {
                    BusFacCore busFacCore = new BusFacCore();
                    List<ContentType> lstContentType = busFacCore.ContentTypeGetList();
                    List<JctUserContentType> lstJctUserContentTypeOfUser = busFacCore.JctUserContentTypeGetList(user);
                    bool bFound = false;
                    foreach (ContentType ct in lstContentType)
                    {
                        // does user have an entry?
                        if (!(lstJctUserContentTypeOfUser.Exists(x => x.ContentTypeID == ct.ContentTypeID)))
                        {
                            returnContentType = ct;
                            model.AskSide = (bool)ct.HasSides;
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        user.IsRatingProfileFinished = true;
                        long lUserID = busFacCore.UserCreateOrModify(user);
                        return RedirectToAction("Index");
                    }
                }
                model.contentType = returnContentType;
                model.ContentTypeID = returnContentType.ContentTypeID;
                string imageUrl = "../Images/";
                model.Rating = 0;
                if (returnContentType.ContentTypeID == 0)
                {
                    imageUrl += "ebenefits.jpg";
                    model.Rating = user.CurrentRating;
                }
                else
                {
                    imageUrl += returnContentType.Code + ".jpg";
                }
                model.imageURL = imageUrl;

            }
            catch (Exception ex)
            {

            }

            model.RatingLeftSide = 0;
            model.RatingRightSide = 0;
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
                model.Message = null;
                ContentType contentType = busFacCore.ContentTypeGet(model.ContentTypeID);
                JctUserContentType jctUserContentType = null;
                long lJctUserContentTypeID = 0;

                if (submitID == "OVERALLRATING_SUBMIT")
                {
                    user.HasCurrentRating = true;
                    user.CurrentRating = model.Rating;
                    lID = busFacCore.UserCreateOrModify(user);
                }
                else if (submitID == "OVERALLRATING_NONE")
                {
                    user.HasCurrentRating = true;
                    user.CurrentRating = 0;
                    lID = busFacCore.UserCreateOrModify(user);
                }
                else if (submitID == "NOT_CONNECTED")
                {
                    jctUserContentType = new JctUserContentType() { UserID = user.UserID, ContentTypeID = model.ContentTypeID, IsConnected = false };
                    lJctUserContentTypeID = busFacCore.JctUserContentTypeCreateOrModify(jctUserContentType);
                }
                else if (long.TryParse(submitID, out lParsedContentTypeID))
                {
                    if (!(bool)contentType.HasSides)
                    {
                        jctUserContentType = new JctUserContentType() { UserID = user.UserID, ContentTypeID = model.ContentTypeID, IsConnected = true, Rating = model.Rating };
                        lJctUserContentTypeID = busFacCore.JctUserContentTypeCreateOrModify(jctUserContentType);
                    }
                    else
                    {
                        jctUserContentType = new JctUserContentType() { UserID = user.UserID, ContentTypeID = model.ContentTypeID, IsConnected = true, Rating = model.RatingBothSide, SideID = 4 };
                        lJctUserContentTypeID = busFacCore.JctUserContentTypeCreateOrModify(jctUserContentType);

                        jctUserContentType = new JctUserContentType() { UserID = user.UserID, ContentTypeID = model.ContentTypeID, IsConnected = true, Rating = model.RatingLeftSide, SideID = 2 };
                        lJctUserContentTypeID = busFacCore.JctUserContentTypeCreateOrModify(jctUserContentType);

                        jctUserContentType = new JctUserContentType() { UserID = user.UserID, ContentTypeID = model.ContentTypeID, IsConnected = true, Rating = model.RatingRightSide, SideID = 3 };
                        lJctUserContentTypeID = busFacCore.JctUserContentTypeCreateOrModify(jctUserContentType);

                    }                  
                }

            }
            catch (Exception ex)
            {

            }
            model.Rating = 0;
            model.RatingLeftSide = 0;
            model.RatingRightSide = 0;
            model.RatingBothSide = 0;

            return RedirectToAction("RatingsCapture", model);
        }

        public ActionResult eBenefits(EBenefitsModel model)
        {

            return View("eBenefitsCapture", model);
        }

        /**************************************************************
         * Back Form
         * 
         *************************************************************/
        public ActionResult Back()
        {
            string viewName = "dbqBack";
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
        public ActionResult BackPost(BackModel model, long contentStateID)
        {
            string viewName = "dbqBack";
            long contenttypeid = 1;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
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
            return View(viewName, model);
        }

        /**************************************************************
        * Shoulder Form
        * 
        *************************************************************/
        public ActionResult Shoulder()
        {
            string viewName = "dbqShoulder";
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

            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult ShoulderPost(ShoulderModel model, long contentStateID)
        {
            string viewName = "dbqShoulder";
            long contenttypeid = 2;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/shoulder.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Shoulder(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
        * Neck Form
        * 
        *************************************************************/
        public ActionResult Neck()
        {
            string viewName = "dbqNeck";
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
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult NeckPost(NeckModel model, long contentStateID)
        {
            string viewName = "dbqNeck";
            long contenttypeid = 3;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/neck.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Neck(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
         * Foot Form
         * 
         *************************************************************/
        public ActionResult Foot()
        {
            string viewName = "dbqFoot";
            FootModel model = new FootModel();
            long contenttypeid = 4;
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
                    model = JSONHelper.Deserialize<FootModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult FootPost(FootModel model, long contentStateID)
        {
            string viewName = "dbqFoot";
            long contenttypeid = 4;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/foot.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Foot(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
          * Sleepapnea Form
          * 
          *************************************************************/
        public ActionResult Sleepapnea()
        {
            string viewName = "dbqSleepapnea";
            SleepapneaModel model = new SleepapneaModel();
            long contenttypeid = 5;
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
                    model = JSONHelper.Deserialize<SleepapneaModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult SleepapneaPost(SleepapneaModel model, long contentStateID)
        {
            string viewName = "dbqSleepapnea";
            long contenttypeid = 5;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/sleepapnea.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Sleepapnea(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
          * Headache Form
          * 
          *************************************************************/
        public ActionResult Headache()
        {
            string viewName = "dbqHeadache";
            HeadacheModel model = new HeadacheModel();
            long contenttypeid = 6;
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
                    model = JSONHelper.Deserialize<HeadacheModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult HeadachePost(HeadacheModel model, long contentStateID)
        {
            string viewName = "dbqHeadache";
            long contenttypeid = 6;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/headache.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Headache(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
        * Ankle Form
        * 
        *************************************************************/
        public ActionResult Ankle()
        {
            string viewName = "dbqAnkle";
            AnkleModel model = new AnkleModel();
            long contenttypeid = 7;
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
                    model = JSONHelper.Deserialize<AnkleModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult AnklePost(AnkleModel model, long contentStateID)
        {
            string viewName = "dbqAnkle";
            long contenttypeid = 7;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/ankle.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Ankle(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
        * Wrist Form
        * 
        *************************************************************/
        public ActionResult Wrist()
        {
            string viewName = "dbqWrist";
            WristModel model = new WristModel();
            long contenttypeid = 8;
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
                    model = JSONHelper.Deserialize<WristModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult WristPost(WristModel model, long contentStateID)
        {
            string viewName = "dbqWrist";
            long contenttypeid = 8;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/wrist.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Wrist(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
        * Knee Form
        * 
        *************************************************************/
        public ActionResult Knee()
        {
            string viewName = "dbqKnee";
            KneeModel model = new KneeModel();
            long contenttypeid = 9;
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
                    model = JSONHelper.Deserialize<KneeModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult KneePost(KneeModel model, long contentStateID)
        {
            string viewName = "dbqKnee";
            long contenttypeid = 9;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/knee.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Knee(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
        * Hip Form
        * 
        *************************************************************/
        public ActionResult Hip()
        {
            string viewName = "dbqHip";
            HipModel model = new HipModel();
            long contenttypeid = 10;
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
                    model = JSONHelper.Deserialize<HipModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult HipPost(HipModel model, long contentStateID)
        {
            string viewName = "dbqHip";
            long contenttypeid = 10;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/hip.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Hip(pdfTemplatePath, model);
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
            return View(viewName, model);
        }

        /**************************************************************
        * Elbow Form
        * 
        *************************************************************/
        public ActionResult Elbow()
        {
            string viewName = "dbqElbow";
            ElbowModel model = new ElbowModel();
            long contenttypeid = 11;
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
                    model = JSONHelper.Deserialize<ElbowModel>(content.ContentMeta);
                }

                if (string.IsNullOrEmpty(model.NameOfPatient))
                {
                    model.NameOfPatient = user.Fullname;
                }
                if (string.IsNullOrEmpty(model.SocialSecurity))
                {
                    model.SocialSecurity = user.Ssn;
                }
            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpPost]
        public ActionResult ElbowPost(ElbowModel model, long contentStateID)
        {
            string viewName = "dbqElbow";
            long contenttypeid = 11;
            try
            {
                long ContentID = FormSave(model, contentStateID, contenttypeid);
                if (contentStateID == 6)
                {
                    // submit application
                    BusFacPDF busFacPDF = new BusFacPDF();
                    string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/elbow.pdf"));
                    User user = Auth();
                    byte[] form = busFacPDF.Elbow(pdfTemplatePath, model);
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
            return View(viewName, model);
        }
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

//[HttpPost]
//public ActionResult eBenefitsCapture(EBenefitsModel model, string submitID)
//{
//    try
//    {
//        //string loginPage = "https://myaccess.dmdc.osd.mil/identitymanagement/authenticate.do?execution=e1s1";
//        string disabilitiesFramePage = "https://eauth.va.gov/wssweb/wss-common-webparts/mvc/ebn/ratedDisabilities";


//        try
//        {
//            using (var client = GetAuthenticatedClient())
//            {
//                //var html = client.DownloadString("http://veteransapp.azurewebsites.net/Calculator");
//                var html = client.DownloadString(disabilitiesFramePage);
//                //do your stuff with received HTML here
//            }

//            //WebClient client = new WebClient();

//            // Add a user agent header in case the 
//            // requested URI contains a query.

//            //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

//            //Stream data = client.OpenRead(disabilitiesFramePage);
//            //StreamReader reader = new StreamReader(data);
//            //string s = reader.ReadToEnd();
//            //Console.WriteLine(s);
//            //data.Close();
//            //reader.Close();

//            //IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver();
//            //driver.Url = disabilitiesFramePage;

//            // Download the data to a buffer.
//            //WebClient client = new WebClient();

//            //Byte[] pageData = client.DownloadData(disabilitiesFramePage);
//            //string pageHtml = Encoding.ASCII.GetString(pageData);

//            // Download the data to a file.
//            //client.DownloadFile("http://www.contoso.com", "page.htm");


//        }
//        catch (WebException webEx)
//        {
//        }
//        //HtmlWeb web = new HtmlWeb();
//        //HtmlDocument document = web.Load(disabilitiesFramePage);



//        //WebRequest req = HttpWebRequest.Create(disabilitiesFramePage);
//        //req.Method = "GET";

//        //string source;
//        //using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
//        //{
//        //    source = reader.ReadToEnd();
//        //}

//    }
//    catch (Exception ex)
//    {

//    }
//    return View("eBenefitsCapture", model);
//}
//protected CookieAwareWebClient GetAuthenticatedClient()
//{
//    var client = new CookieAwareWebClient();

//    var loginData = new NameValueCollection
//        {
//              { "userName", "joshua.smith3" },
//              { "password-clear", "zaq1@WSXC" }
//        };
//    //var loginData = new NameValueCollection
//    //    {
//    //          { "Username", "h1@gmail.com" },
//    //          { "Password", "123" }
//    //    };

//    //client.Login("http://veteransapp.azurewebsites.net/Home/Login2", loginData);
//    client.Login("https://myaccess.dmdc.osd.mil/identitymanagement/authenticate.do?gotoUrl=https://myaccess.dmdc.osd.mil/opensso/SAMLAwareServlet?TARGET=https://eauth.va.gov/ebenefits-portal", loginData);

//    return client;
//}
//private string GetContentTypeImageUrl(long contentTypeID)
//{
//    string imageUrl = "../Images/";
//    switch (contentTypeID)
//    {
//        case 0:
//            imageUrl += "ebenefits.jpg";
//            break;
//        case 1:
//            imageUrl += "back.jpg";
//            break;
//        case 2:
//            imageUrl += "shoulder.jpg";
//            break;
//        case 3:
//            imageUrl += "neck.jpg";
//            break;
//        case 4:
//            imageUrl += "foot.jpg";
//            break;
//        case 5:
//            imageUrl += "sleepapnea.jpg";
//            break;
//        case 6:
//            imageUrl += "headache.jpg";
//            break;
//        case 7:
//            imageUrl += "ankle.jpg";
//            break;
//        default:
//            break;
//    }
//    return imageUrl;
//}

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
