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
using System.Web.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace MainSite.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public static readonly string VARIANCE_DEFAULT_CHOICE = "-- Choose Phrase or Write Your Own Above --";

        private Config _config = null;

        private string[] arVarianceHistory = new string[] { "Onset of injury began during active duty service. Please see active duty records.",
                                                            "Dx and Hx well-established by VA and military.",
                                                            "Original injury incurred during active duty service, condition has continued.",
                                                            "Pt describes original diagnosis during the military, confirmed by VA and service-connected." };


        private string[] arVarianceFlareUps = new string[]  { "Pt states during a flare-up they are unable to have FROM with great pain and stiffness.",
                                                            "Pt describes pain, tenderness, and very restricted LROM.",
                                                            "Pt describes occasional swelling during a flare with highly restricted range of motion.",
                                                            "During a flare pt is very restricted with physical activities."};

        private string[] arVarianceFunctionLoss = new string[]  { "Pt reports weakened movement and tiring easily.",
                                                            "Pt describes constant pain with movement and unable to perform some physical tasks.",
                                                            "Pt states there is intermittent pain and swelling with daily activities.",
                                                            "Pt reports occasional loss of movement and extreme tenderness when exerting."};
        private User Auth()
        {
            bool bIsAuth = User.Identity.IsAuthenticated;
            User user = null;
            if ((!IsCookieEnabled()) || (!bIsAuth))
            {
                LogOut();
            }
            else
            {
                string userguid = GetCookieFieldValue(CookieManager.COOKIE_FIELD_USER_GUID);
                string activeuserguid = GetCookieFieldValue(CookieManager.COOKIE_FIELD_ACTIVEUSER_GUID);
                if (!string.IsNullOrEmpty(activeuserguid))
                {
                    userguid = activeuserguid;
                }
                if (!string.IsNullOrEmpty(userguid))
                {
                    BusFacCore busFacCore = new BusFacCore();
                    user = busFacCore.UserGet(userguid);
                }
            }
            return user;
        }
        private User AuthSourceUser()
        {
            User user = null;
            try
            {
                bool bIsAuth = User.Identity.IsAuthenticated;
                string userguid = GetCookieFieldValue(CookieManager.COOKIE_FIELD_USER_GUID);
                BusFacCore busFacCore = new BusFacCore();
                user = busFacCore.UserGet(userguid);
            }
            catch (Exception ex)
            {

            }
            return user;
        }
        public DashboardController()
        {
            _config = new Config();

            var MovementList0to110Deg = new System.Web.Mvc.SelectList(new[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110 });
            ViewBag.MovementList0To110Deg = MovementList0to110Deg;

            var MovementList0to45Deg = new System.Web.Mvc.SelectList(new[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45 });
            ViewBag.MovementList0To45Deg = MovementList0to45Deg;

            var MovementList20Deg = new System.Web.Mvc.SelectList(new[] { 20, 15, 10, 5, 0 });
            ViewBag.MovementList20Deg = MovementList20Deg;

            var MovementList25Deg = new System.Web.Mvc.SelectList(new[] { 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList25Deg = MovementList25Deg;

            var MovementList30Deg = new System.Web.Mvc.SelectList(new[] { 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList30Deg = MovementList30Deg;

            var MovementList40Deg = new System.Web.Mvc.SelectList(new[] { 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList40Deg = MovementList40Deg;

            var MovementList45Deg = new System.Web.Mvc.SelectList(new[] { 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList45Deg = MovementList45Deg;

            var MovementList60Deg = new System.Web.Mvc.SelectList(new[] { 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList60Deg = MovementList60Deg;

            var MovementList70Deg = new System.Web.Mvc.SelectList(new[] { 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList70Deg = MovementList70Deg;

            var MovementList80Deg = new System.Web.Mvc.SelectList(new[] { 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList80Deg = MovementList80Deg;

            var MovementList85Deg = new System.Web.Mvc.SelectList(new[] { 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList85Deg = MovementList85Deg;

            var MovementList90Deg = new System.Web.Mvc.SelectList(new[] { 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList90Deg = MovementList90Deg;

            var MovementList125Deg = new System.Web.Mvc.SelectList(new[] { 125, 120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList125Deg = MovementList125Deg;

            var MovementList140Deg = new System.Web.Mvc.SelectList(new[] { 140, 135, 130, 125, 120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList140Deg = MovementList140Deg;

            var MovementList145Deg = new System.Web.Mvc.SelectList(new[] { 145, 140, 135, 130, 125, 120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList145Deg = MovementList145Deg;

            var MovementList180Deg = new System.Web.Mvc.SelectList(new[] { 180, 175, 170, 165, 160, 155, 150, 145, 140, 135, 130, 125, 120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0 });
            ViewBag.MovementList180Deg = MovementList180Deg;

            var CurrentRatingsList = new System.Web.Mvc.SelectList(new[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });
            ViewBag.CurrentRatingsList = CurrentRatingsList;

            var VarianceHistory = new System.Web.Mvc.SelectList(new[] { VARIANCE_DEFAULT_CHOICE,
                                                                        "Onset of injury began during active duty service. Please see active duty records.",
                                                                        "Dx and Hx well-established by VA and military.",
                                                                        "Original injury incurred during active duty service, condition has continued.",
                                                                        "Pt describes original diagnosis during the military, confirmed by VA and service-connected."});
            ViewBag.VarianceHistory = VarianceHistory;

            var VarianceFlareUps = new System.Web.Mvc.SelectList(new[] { VARIANCE_DEFAULT_CHOICE,
                                                                        "Pt states during a flare-up they are unable to have FROM with great pain and stiffness.",
                                                                        "Pt describes pain, tenderness, and very restricted LROM.",
                                                                        "Pt describes occasional swelling during a flare with highly restricted range of motion.",
                                                                        "During a flare pt is very restricted with physical activities."});
            ViewBag.VarianceFlareUps = VarianceFlareUps;

            var VarianceFunctionLoss = new System.Web.Mvc.SelectList(new[] { VARIANCE_DEFAULT_CHOICE,
                                                                        "Pt reports weakened movement and tiring easily.",
                                                                        "Pt describes constant pain with movement and unable to perform some physical tasks.",
                                                                        "Pt states there is intermittent pain and swelling with daily activities.",
                                                                        "Pt reports occasional loss of movement and extreme tenderness when exerting."});
            ViewBag.VarianceFunctionLoss = VarianceFunctionLoss;

            var MuscleStrength = new System.Web.Mvc.SelectList((new[] { "5", "4" }));
            ViewBag.MuscleStrength = MuscleStrength;

            var ReflexExam = new System.Web.Mvc.SelectList(new[] { "3", "2", "1" });
            ViewBag.ReflexExam = ReflexExam;

        }
        public ActionResult Index()
        {
            User user = Auth();
            DashboardModel dashboardModel = new DashboardModel();
            if (user != null)
            {
                User userSource = AuthSourceUser();
                if ((bool)userSource.IsDisabled)
                {
                    LogOut();
                }

                BusFacCore busFacCore = new BusFacCore();
                Dictionary<long, BenefitStatus> dictBenefitStatuses = new Dictionary<long, BenefitStatus>();
                Dictionary<long, BenefitStatus> dictBenefitStatusesQualify = new Dictionary<long, BenefitStatus>();
                Dictionary<long, BenefitStatus> dictBenefitStatusesNonQualify = new Dictionary<long, BenefitStatus>();

                busFacCore.GetBenefitStatuses(user.UserID, ref dictBenefitStatuses, ref dictBenefitStatusesQualify, ref dictBenefitStatusesNonQualify);
                dashboardModel.BenefitStatuses = dictBenefitStatuses;
                dashboardModel.BenefitStatusesQualify = dictBenefitStatusesQualify;
                dashboardModel.BenefitStatusesNonQualify = dictBenefitStatusesNonQualify;

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
            if (user.IsDisabled == null)
            {
                user.IsDisabled = false;
            }
            UserModel userModel = new UserModel()
            {
                Username = user.Username,
                Password = user.Passwd,
                FullName = user.Fullname,
                PhoneNumber = UtilsString.OnlyDigits(user.PhoneNumber),
                Message = user.UserMessage,
                SSN = user.Ssn,
                InternalCalculatedRating = user.InternalCalculatedRating,
                CurrentRating = (int)user.CurrentRating,
                HasCurrentRating = (bool)user.HasCurrentRating,
                IsRatingProfileFinished = (bool)user.IsRatingProfileFinished,
                UserRoleID = user.UserRoleID,
                IsDisabled = (bool)user.IsDisabled,
                DateCreated = user.DateCreated,
                CookieID = user.CookieID
            };

            switch (user.UserRoleID)
            {
                case 1:
                    userModel.UserRoleText = "Client";
                    break;
                case 2:
                    userModel.UserRoleText = "Staff";
                    break;
                case 3:
                    userModel.UserRoleText = "Power User";
                    break;
                case 4:
                    userModel.UserRoleText = "Admin";
                    break;
                default:
                    break;
            }
            return userModel;
        }
        public ActionResult ProfileUpdate()
        {
            ProfileModel profileModel = new ProfileModel();
            User user = Auth();
            UserModel userModel = UserToModel(user);
            profileModel.userModel = userModel;
            BusFacCore busFacCore = new BusFacCore();
            profileModel.lstUserDisability = busFacCore.GetLatestUserDisability(user.UserID);
            User userSource = AuthSourceUser();
            if ((userSource != null) && (userSource.UserRoleID == 4))
            {
                profileModel.IsAdmin = true;
                profileModel.RoleChoice = Convert.ToString(user.UserRoleID);
            }
            return View(profileModel);
        }
        [HttpPost]
        public ActionResult ProfileUpdatePost(ProfileModel profileModel)
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
                        user.Ssn = UtilsSecurity.encrypt(UtilsString.OnlyDigits(profileModel.userModel.SSN));
                    }
                }
                else
                {
                    user.Ssn = null;
                }

                user.PhoneNumber = UtilsString.OnlyDigits(profileModel.userModel.PhoneNumber);
                if (!user.Passwd.Equals(profileModel.userModel.Password))
                {
                    user.Passwd = UtilsSecurity.encrypt(profileModel.userModel.Password);
                }
                user.UserMessage = profileModel.userModel.Message;
                if (user.CurrentRating != profileModel.userModel.CurrentRating)
                {
                    user.IsRatingProfileFinished = false;
                }
                user.CurrentRating = profileModel.userModel.CurrentRating;
                User userSource = AuthSourceUser();
                if ((userSource != null) && (userSource.UserRoleID == 4) && (!string.IsNullOrEmpty(profileModel.RoleChoice)))
                {

                    long tmpRoleID = (long)Convert.ToInt32(profileModel.RoleChoice);
                    if ((tmpRoleID >= 1) && (tmpRoleID <= 3))
                    {
                        user.UserRoleID = tmpRoleID;
                        user.IsDisabled = profileModel.userModel.IsDisabled;
                    }
                }

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
            return RedirectToAction("Index", "Dashboard");

            //return View("ProfileUpdate", profileModel);
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserModel userModel)
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
                            //FormsAuthentication.SetAuthCookie(userModel.Username, false);

                            var claims = new List<Claim>();
                            claims.Add(new Claim(ClaimTypes.Name, userModel.Username));
                            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                            System.Web.HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(identity);

                            AssociateEvaluationWithUser(user);
                            string userguid = GetCookieFieldValue(CookieManager.COOKIE_FIELD_USER_GUID);
                            if ((!string.IsNullOrEmpty(userguid)) && (user.CookieID != userguid))
                            {
                                bool b2 = SetCookieField(CookieManager.COOKIE_FIELD_ACTIVEUSER_GUID, string.Empty);
                            }
                            bool b = SetCookieField(CookieManager.COOKIE_FIELD_USER_GUID, user.CookieID);
                            return RedirectToAction("Index", "Dashboard");
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
                EvaluationModel evaluationModel = new EvaluationModel();
                long lID = 0;
                BusFacCore busFacCore = new BusFacCore();
                if (!string.IsNullOrEmpty(IsNewEval) && (IsNewEval == "true"))
                {
                    evaluationModel.CurrentRating = Convert.ToInt32(GetCookieFieldValue(CookieManager.COOKIE_FIELD_CURRENT_RATING));
                    evaluationModel.HasAClaim = Convert.ToBoolean(GetCookieFieldValue(CookieManager.COOKIE_FIELD_HAS_A_CLAIM));
                    evaluationModel.HasActiveAppeal = Convert.ToBoolean(GetCookieFieldValue(CookieManager.COOKIE_FIELD_HAS_ACTIVE_APPEAL));
                    evaluationModel.IsFirstTimeFiling = Convert.ToBoolean(GetCookieFieldValue(CookieManager.COOKIE_FIELD_IS_FIRST_TIME_FILING));
                    lID = busFacCore.EvaluationCreate(evaluationModel, user.UserID);
                    if (lID > 0)
                    {
                        bool b = SetCookieField(CookieManager.COOKIE_FIELD_ISNEW_EVAL, "false");
                    }
                    user.CurrentRating = evaluationModel.CurrentRating;
                    lID = busFacCore.UserCreateOrModify(user);
                }
            }
            catch { }
        }
        private void AssociateEvaluationWithUserInternal(User user, UserNewModel userNew)
        {
            try
            {
                EvaluationModel evaluationModel = new EvaluationModel();
                long lID = 0;
                BusFacCore busFacCore = new BusFacCore();
                evaluationModel.CurrentRating = userNew.CurrentRating;
                lID = busFacCore.EvaluationCreate(evaluationModel, user.UserID);
                user.CurrentRating = evaluationModel.CurrentRating;
                lID = busFacCore.UserCreateOrModify(user);
            }
            catch { }
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            try
            {
                BusFacCore busFacCore = new BusFacCore();
                UtilsValidation utilsValidation = new UtilsValidation();
                BusUser busUser = new BusUser();
                if ((utilsValidation.IsValidEmail(userModel.Username)) && (busUser.IsValidPasswd(userModel.Password)))
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
                                var claims = new List<Claim>();
                                claims.Add(new Claim(ClaimTypes.Name, userModel.Username));
                                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                                System.Web.HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(identity);

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
        //private string GetTemplatePath(IBaseModel model)
        //{
        //    string path = null;
        //    switch (model.GetType().Name)
        //    {
        //        case "BackModel":
        //            path = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
        //            break;
        //        default:
        //            break;
        //    }
        //    return path;
        //}


        private long FormSave(IBaseModel model, long contentStateID, long contentTypeID, bool isNew = false)
        {
            long ContentID = 0;
            BusFacPDF busFacPDF = new BusFacPDF();
            //model.TemplatePath = GetTemplatePath(model);
            ContentID = busFacPDF.Save(model, contentStateID, contentTypeID, isNew);
            return ContentID;
        }

        //[HttpPost]
        //public ActionResult BackFormToPdf(BackModel backModel, string submitButton)
        //{
        //    try
        //    {
        //        BusFacPDF busFacPDF = new BusFacPDF();
        //        string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
        //        User user = Auth();
        //        backModel.NameOfPatient = user.Fullname;
        //        backModel.SocialSecurity = user.Ssn;

        //        byte[] form = busFacPDF.Back(pdfTemplatePath, backModel);
        //        //PDFHelper.ReturnPDF(form, "back-dbq.pdf");
        //        string attachmentFilename = "back-hay.pdf";
        //        string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";
        //        string fullPath = path + attachmentFilename;
        //        System.IO.File.WriteAllBytes(fullPath, form);

        //        // Force the pdf document to be displayed in the browser
        //        Response.AppendHeader("Content-Disposition", "inline;");

        //        //return System.IO.File(fullPath, System.Net.Mime.MediaTypeNames.Application.Pdf, attachmentFilename);
        //        return File(fullPath, "application/pdf");

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View(backModel);
        //}

        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult LoginAccessDenied()
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
                    //List<JctUserContentType> lstJctUserContentTypeOfUser = busFacCore.JctUserContentTypeGetList(user);
                    bool bFound = false;
                    if (model.ContentTypeID < lstContentType.Count)
                    {
                        returnContentType = lstContentType[(int)model.ContentTypeID];
                        model.AskSide = (bool)returnContentType.HasSides;
                        bFound = true;

                    }
                    else { bFound = false; }

                    //foreach (ContentType ct in lstContentType)
                    //{
                    //    // does user have an entry?
                    //    if (!(lstJctUserContentTypeOfUser.Exists(x => x.ContentTypeID == ct.ContentTypeID)))
                    //    {
                    //        returnContentType = ct;
                    //        model.AskSide = (bool)ct.HasSides;
                    //        bFound = true;
                    //        break;
                    //    }
                    //}
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
                    NameSet nameSet = UtilsString.ParseFullname(model.Fullname);
                    user.Fullname = model.Fullname;
                    user.Firstname = nameSet.FirstName;
                    user.Middlename = nameSet.MiddleInitial;
                    user.Lastname = nameSet.LastName;
                    lID = busFacCore.UserCreateOrModify(user);
                }
                else if (submitID == "NOT_CONNECTED")
                {
                    jctUserContentType = new JctUserContentType() { UserID = user.UserID, ContentTypeID = model.ContentTypeID, IsConnected = false };
                    lJctUserContentTypeID = busFacCore.JctUserContentTypeCreateOrModify(jctUserContentType);
                }
                //else if (submitID == "OVERALLRATING_NONE")
                //{
                //    user.HasCurrentRating = true;
                //    user.CurrentRating = 0;
                //    lID = busFacCore.UserCreateOrModify(user);
                //}
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
        public ActionResult Back(string isnew)
        {
            string viewName = "dbqBack";
            BackModel model = new BackModel();
            long contenttypeid = 1;
            try
            {
                //string templatePath = GetTemplatePath(model);
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);

                }
                else
                {
                    model = JSONHelper.Deserialize<BackModel>(content.ContentMeta);

                }

                // Right leg advanced feature defaults
                var MuscleStrength199 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice199);
                ViewBag.MuscleStrength199 = MuscleStrength199;

                var MuscleStrength192 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice192);
                ViewBag.MuscleStrength192 = MuscleStrength192;

                var MuscleStrength193 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice193);
                ViewBag.MuscleStrength193 = MuscleStrength193;

                var MuscleStrength198 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice198);
                ViewBag.MuscleStrength198 = MuscleStrength198;

                var MuscleStrength197 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice197);
                ViewBag.MuscleStrength197 = MuscleStrength197;

                var MuscleStrength194 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice194);
                ViewBag.MuscleStrength194 = MuscleStrength194;

                var MuscleStrength195 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice195);
                ViewBag.MuscleStrength195 = MuscleStrength195;

                var MuscleStrength196 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice196);
                ViewBag.MuscleStrength196 = MuscleStrength196;

                var ReflexExam244 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S13AChoice244);
                ViewBag.ReflexExam244 = ReflexExam244;

                var ReflexExam241 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S13AChoice241);
                ViewBag.ReflexExam241 = ReflexExam241;


                // left leg advanced options
                var MuscleStrength226 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice226);
                ViewBag.MuscleStrength226 = MuscleStrength226;

                var MuscleStrength219 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice219);
                ViewBag.MuscleStrength219 = MuscleStrength219;

                var MuscleStrength220 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice220);
                ViewBag.MuscleStrength220 = MuscleStrength220;

                var MuscleStrength225 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice225);
                ViewBag.MuscleStrength225 = MuscleStrength225;

                var MuscleStrength224 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice224);
                ViewBag.MuscleStrength224 = MuscleStrength224;

                var MuscleStrength221 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice221);
                ViewBag.MuscleStrength221 = MuscleStrength221;

                var MuscleStrength222 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice222);
                ViewBag.MuscleStrength222 = MuscleStrength222;

                var MuscleStrength223 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S13AChoice223);
                ViewBag.MuscleStrength223 = MuscleStrength223;

                var ReflexExam242 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S13AChoice242);
                ViewBag.ReflexExam242 = ReflexExam242;

                var ReflexExam243 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S13AChoice241);
                ViewBag.ReflexExam243 = ReflexExam243;


            }
            catch (Exception ex)
            {

            }

            return View(viewName, model);
        }

        [HttpGet]
        public ActionResult dbqView(long id, string filename)
        {

            long ContentID = id;
            BusFacCore busFacCore = new BusFacCore();
            Content content = busFacCore.ContentGet(ContentID);
            byte[] form = content.ContentData;

            string attachmentFilename = filename;
            string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";
            string fullPath = path + attachmentFilename;
            System.IO.File.WriteAllBytes(fullPath, form);

            // Force the pdf document to be displayed in the browser
            Response.AppendHeader("Content-Disposition", "inline;");

            //return System.IO.File(fullPath, System.Net.Mime.MediaTypeNames.Application.Pdf, attachmentFilename);
            return File(fullPath, "application/pdf");
        }

        [HttpPost]
        public ActionResult BackPost(BackModel model, long contentStateID)
        {
            string viewName = "Back";
            string filename = viewName;
            long contenttypeid = 1;
            try
            {
                model.S13AChoice226 = Request.Form["S13AChoice226"].ToString();
                model.S13AChoice219 = Request.Form["S13AChoice219"].ToString();
                model.S13AChoice220 = Request.Form["S13AChoice220"].ToString();
                model.S13AChoice225 = Request.Form["S13AChoice225"].ToString();
                model.S13AChoice224 = Request.Form["S13AChoice224"].ToString();
                model.S13AChoice221 = Request.Form["S13AChoice221"].ToString();
                model.S13AChoice222 = Request.Form["S13AChoice222"].ToString();
                model.S13AChoice223 = Request.Form["S13AChoice223"].ToString();
                model.S13AChoice242 = Request.Form["S13AChoice242"].ToString();
                model.S13AChoice243 = Request.Form["S13AChoice243"].ToString();

                model.S13AChoice199 = Request.Form["S13AChoice199"].ToString();
                model.S13AChoice192 = Request.Form["S13AChoice192"].ToString();
                model.S13AChoice193 = Request.Form["S13AChoice193"].ToString();
                model.S13AChoice198 = Request.Form["S13AChoice198"].ToString();
                model.S13AChoice197 = Request.Form["S13AChoice197"].ToString();
                model.S13AChoice194 = Request.Form["S13AChoice194"].ToString();
                model.S13AChoice195 = Request.Form["S13AChoice195"].ToString();
                model.S13AChoice196 = Request.Form["S13AChoice196"].ToString();
                model.S13AChoice244 = Request.Form["S13AChoice244"].ToString();
                model.S13AChoice241 = Request.Form["S13AChoice241"].ToString();

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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);


                        //PdfModel pdfModel = new PdfModel() { ContentID = lID, Filename = filename };
                        //return View("dbqView", pdfModel);

                        //string attachmentFilename = "back-hay.pdf";
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";
                        //string fullPath = path + attachmentFilename;
                        //System.IO.File.WriteAllBytes(fullPath, form);

                        //// Force the pdf document to be displayed in the browser
                        //Response.AppendHeader("Content-Disposition", "inline;");

                        ////return System.IO.File(fullPath, System.Net.Mime.MediaTypeNames.Application.Pdf, attachmentFilename);
                        //return File(fullPath, "application/pdf");

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
        public ActionResult Shoulder(string isnew)
        {
            string viewName = "dbqShoulder";
            ShoulderModel model = new ShoulderModel();
            long contenttypeid = 2;
            try
            {
                //string templatePath = GetTemplatePath(model);
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);
                }
                else
                {
                    model = JSONHelper.Deserialize<ShoulderModel>(content.ContentMeta);
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Neck(string isnew)
        {
            string viewName = "dbqNeck";
            NeckModel model = new NeckModel();
            long contenttypeid = 3;
            try
            {
                //string templatePath = GetTemplatePath(model);
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);
                }
                else
                {
                    model = JSONHelper.Deserialize<NeckModel>(content.ContentMeta);
                }


                // Right leg advanced feature defaults
                var MuscleStrength198 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice198);
                ViewBag.MuscleStrength198 = MuscleStrength198;

                var MuscleStrength189 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice189);
                ViewBag.MuscleStrength189 = MuscleStrength189;

                var MuscleStrength190 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice190);
                ViewBag.MuscleStrength190 = MuscleStrength190;

                var MuscleStrength197 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice197);
                ViewBag.MuscleStrength197 = MuscleStrength197;

                var MuscleStrength196 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice196);
                ViewBag.MuscleStrength196 = MuscleStrength196;

                var MuscleStrength191 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice191);
                ViewBag.MuscleStrength191 = MuscleStrength191;

                var MuscleStrength192 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice192);
                ViewBag.MuscleStrength192 = MuscleStrength192;

                var MuscleStrength193 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice193);
                ViewBag.MuscleStrength193 = MuscleStrength193;

                var MuscleStrength195 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice195);
                ViewBag.MuscleStrength195 = MuscleStrength195;

                var MuscleStrength194 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice194);
                ViewBag.MuscleStrength194 = MuscleStrength194;

                var ReflexExam327 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S12AChoice327);
                ViewBag.ReflexExam327 = ReflexExam327;

                var ReflexExam322 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S12AChoice322);
                ViewBag.ReflexExam322 = ReflexExam322;

                var ReflexExam326 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S12AChoice326);
                ViewBag.ReflexExam326 = ReflexExam326;

                // left leg advanced options
                var MuscleStrength227 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice227);
                ViewBag.MuscleStrength227 = MuscleStrength227;

                var MuscleStrength218 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice218);
                ViewBag.MuscleStrength218 = MuscleStrength218;

                var MuscleStrength219 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice219);
                ViewBag.MuscleStrength219 = MuscleStrength219;

                var MuscleStrength226 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice226);
                ViewBag.MuscleStrength226 = MuscleStrength226;

                var MuscleStrength225 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice225);
                ViewBag.MuscleStrength225 = MuscleStrength225;

                var MuscleStrength220 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice220);
                ViewBag.MuscleStrength220 = MuscleStrength220;

                var MuscleStrength221 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice221);
                ViewBag.MuscleStrength221 = MuscleStrength221;

                var MuscleStrength222 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice222);
                ViewBag.MuscleStrength222 = MuscleStrength222;

                var MuscleStrength224 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice224);
                ViewBag.MuscleStrength224 = MuscleStrength224;

                var MuscleStrength223 = new System.Web.Mvc.SelectList((new[] { "5", "4" }), model.S12AChoice223);
                ViewBag.MuscleStrength223 = MuscleStrength223;

                var ReflexExam323 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S12AChoice323);
                ViewBag.ReflexExam323 = ReflexExam323;

                var ReflexExam324 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S12AChoice324);
                ViewBag.ReflexExam324 = ReflexExam324;

                var ReflexExam325 = new System.Web.Mvc.SelectList((new[] { "3", "2", "1" }), model.S12AChoice325);
                ViewBag.ReflexExam325 = ReflexExam325;

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
            string filename = viewName;
            long contenttypeid = 3;
            try
            {

                model.S12AChoice189 = Request.Form["S12AChoice189"].ToString();
                model.S12AChoice190 = Request.Form["S12AChoice190"].ToString();
                model.S12AChoice191 = Request.Form["S12AChoice191"].ToString();
                model.S12AChoice192 = Request.Form["S12AChoice192"].ToString();
                model.S12AChoice193 = Request.Form["S12AChoice193"].ToString();
                model.S12AChoice198 = Request.Form["S12AChoice198"].ToString();
                model.S12AChoice197 = Request.Form["S12AChoice197"].ToString();
                model.S12AChoice196 = Request.Form["S12AChoice196"].ToString();
                model.S12AChoice195 = Request.Form["S12AChoice195"].ToString();
                model.S12AChoice194 = Request.Form["S12AChoice194"].ToString();
                model.S12AChoice327 = Request.Form["S12AChoice327"].ToString();
                model.S12AChoice322 = Request.Form["S12AChoice322"].ToString();
                model.S12AChoice326 = Request.Form["S12AChoice326"].ToString();


                model.S12AChoice218 = Request.Form["S12AChoice218"].ToString();
                model.S12AChoice219 = Request.Form["S12AChoice219"].ToString();
                model.S12AChoice220 = Request.Form["S12AChoice220"].ToString();
                model.S12AChoice221 = Request.Form["S12AChoice221"].ToString();
                model.S12AChoice222 = Request.Form["S12AChoice222"].ToString();
                model.S12AChoice227 = Request.Form["S12AChoice227"].ToString();
                model.S12AChoice226 = Request.Form["S12AChoice226"].ToString();
                model.S12AChoice225 = Request.Form["S12AChoice225"].ToString();
                model.S12AChoice224 = Request.Form["S12AChoice224"].ToString();
                model.S12AChoice223 = Request.Form["S12AChoice223"].ToString();
                model.S12AChoice323 = Request.Form["S12AChoice323"].ToString();
                model.S12AChoice324 = Request.Form["S12AChoice324"].ToString();
                model.S12AChoice325 = Request.Form["S12AChoice325"].ToString();

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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Foot(string isnew)
        {
            string viewName = "dbqFoot";
            FootModel model = new FootModel();
            long contenttypeid = 4;
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);
                }
                else
                {
                    model = JSONHelper.Deserialize<FootModel>(content.ContentMeta);
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Sleepapnea(string isnew)
        {
            string viewName = "dbqSleepapnea";
            SleepapneaModel model = new SleepapneaModel();
            long contenttypeid = 5;
            try
            {
                //string templatePath = GetTemplatePath(model);
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);
                }
                else
                {
                    model = JSONHelper.Deserialize<SleepapneaModel>(content.ContentMeta);
                }
                DateTime dtNull = new DateTime();
                if (dtNull.Equals(model.lastSleepStudyDate))
                {
                    model.lastSleepStudyDate = DateTime.Now.ToShortDateString();
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.FirstName + " " + model.LastName, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Headache(string isnew)
        {
            string viewName = "dbqHeadache";
            HeadacheModel model = new HeadacheModel();
            long contenttypeid = 6;
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);
                }
                else
                {
                    model = JSONHelper.Deserialize<HeadacheModel>(content.ContentMeta);
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
            string filename = viewName;
            long contenttypeid = 6;
            try
            {
                if ((model.S26) || (model.S2) || (model.S3) || (model.S4) || (model.S5) || (model.S6) || (!string.IsNullOrEmpty(model.S3AOther)))
                {
                    model.S3AYes = true;
                }
                if ((model.S75) || (model.S68) || (model.S69) || (model.S70) || (model.S71) || (model.S72) || (!string.IsNullOrEmpty(model.S3BOther)))
                {
                    model.S3BYes = true;
                }

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
                        filename = UtilsString.createFilename(model.FirstName + " " + model.LastName, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Ankle(string isnew)
        {
            string viewName = "dbqAnkle";
            AnkleModel model = new AnkleModel();
            long contenttypeid = 7;
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);
                }
                else
                {
                    model = JSONHelper.Deserialize<AnkleModel>(content.ContentMeta);
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Wrist(string isnew)
        {
            string viewName = "dbqWrist";
            WristModel model = new WristModel();
            long contenttypeid = 8;
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);

                }
                else
                {
                    model = JSONHelper.Deserialize<WristModel>(content.ContentMeta);
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Knee(string isnew)
        {
            string viewName = "dbqKnee";
            KneeModel model = new KneeModel();
            long contenttypeid = 9;
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);

                }
                else
                {
                    model = JSONHelper.Deserialize<KneeModel>(content.ContentMeta);
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Hip(string isnew)
        {
            string viewName = "dbqHip";
            HipModel model = new HipModel();
            long contenttypeid = 10;
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);

                }
                else
                {
                    model = JSONHelper.Deserialize<HipModel>(content.ContentMeta);
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        public ActionResult Elbow(string isnew)
        {
            string viewName = "dbqElbow";
            ElbowModel model = new ElbowModel();
            long contenttypeid = 11;
            try
            {
                User user = Auth();
                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(user.UserID, contenttypeid);
                long ContentID = 0;
                model.UserID = user.UserID;
                if ((content == null) || (!string.IsNullOrEmpty(isnew)))
                {
                    ContentID = FormSave(model, 0, contenttypeid, true);
                    SetModelVariance(model);

                }
                else
                {
                    model = JSONHelper.Deserialize<ElbowModel>(content.ContentMeta);
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
            string filename = viewName;
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
                        filename = UtilsString.createFilename(model.NameOfPatient, viewName);
                        if (filename == null)
                        {
                            filename = viewName + ".pdf";
                        }
                        PDFHelper.ReturnPDF(form, filename);

                        //ProductModel productModel = new ProductModel() { ContentTypeID = model.ContentTypeID };
                        //return RedirectToAction("Product", productModel);
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
        //[HttpGet]
        //public ActionResult ClientNew(UserModel model)
        //{
        //    try
        //    {


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View(model);
        //}
        [HttpGet]
        public ActionResult ClientCreate(UserNewModel model)
        {
            return View(model);
        }
        [HttpGet]
        public ActionResult SwitchHome(UserNewModel model)
        {
            string userguid = GetCookieFieldValue(CookieManager.COOKIE_FIELD_USER_GUID);
            if (!string.IsNullOrEmpty(userguid))
            {
                bool b = SetCookieField(CookieManager.COOKIE_FIELD_ACTIVEUSER_GUID, userguid);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ClientCreatePost(UserNewModel model, string submit)
        {
            string viewName = "ClientCreate";
            try
            {
                if (submit == "submit")
                {
                    BusFacCore busFacCore = new BusFacCore();
                    UtilsValidation utilsValidation = new UtilsValidation();
                    if ((utilsValidation.IsValidEmail(model.Username)))
                    {
                        bool UserExist = busFacCore.Exist(model.Username);
                        if (!UserExist)
                        {
                            string password = model.Username + "001";
                            User userSource = Auth();
                            User user = busFacCore.UserCreate(model.Username, password, userSource.UserID);
                            if ((user != null) && (user.UserID > 0))
                            {
                                if (model.CurrentRating > 0)
                                {
                                    user.HasCurrentRating = true;
                                    user.CurrentRating = model.CurrentRating;
                                }
                                user.Fullname = model.FullName;
                                user.PhoneNumber = model.PhoneNumber;
                                user.InternalNotes = model.Note;
                                user.IsRatingProfileFinished = (!model.ShowUnderwritingWizard);
                                long lID = busFacCore.UserCreateOrModify(user);
                                AssociateEvaluationWithUserInternal(user, model);
                                bool b = SetCookieField(CookieManager.COOKIE_FIELD_ACTIVEUSER_GUID, user.CookieID);
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
                    else
                    {
                        ViewData["InvalidCredentials"] = true;
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }

            }
            catch (Exception ex)
            {
                ViewData["HasError"] = true;
            }

            return View(viewName, model);
        }
        [HttpPost]
        public ActionResult SearchResults(string pattern)
        {
            string viewName = "SearchResults";
            SearchResultModel model = new SearchResultModel();

            if (!string.IsNullOrEmpty(pattern))
            {
                User userSource = AuthSourceUser();
                BusFacCore busFacCore = new BusFacCore();
                if ((userSource != null) && (userSource.UserRoleID > 1))
                {
                    if (pattern == "---")
                    {
                        pattern = string.Empty;
                    }
                    List<User> lstUser = null;
                    if (userSource.UserRoleID == 4)
                    {
                        lstUser = busFacCore.SearchAdmin(pattern);
                    }
                    else
                    {
                        lstUser = busFacCore.Search(pattern, userSource.UserID);
                    }
                    if (lstUser.Count > 0)
                    {
                        List<UserModel> lstUserModel = new List<UserModel>();
                        UserModel userModel = null;
                        foreach (User user in lstUser)
                        {
                            userModel = UserToModel(user);
                            lstUserModel.Add(userModel);
                        }
                        model.lstUserModel = lstUserModel;
                    }
                    model.numresults = lstUser.Count;
                    model.searchText = pattern;
                }
            }
            return View(viewName, model);
        }

        [HttpGet]
        public ActionResult SetUser(string id)
        {
            bool b = SetCookieField(CookieManager.COOKIE_FIELD_ACTIVEUSER_GUID, id);
            return RedirectToAction("Index");
        }

        private void SetModelVariance(IBaseModel m)
        {
            int ind = 0;
            Random rnd = new Random();

            ind = rnd.Next(0, arVarianceHistory.Length);
            m.VarianceHistoryWriteIn = arVarianceHistory[ind];

            ind = rnd.Next(0, arVarianceFlareUps.Length);
            m.VarianceFlareUpsWriteIn = arVarianceFlareUps[ind];

            ind = rnd.Next(0, arVarianceFunctionLoss.Length);
            m.VarianceFunctionLossWriteIn = arVarianceFlareUps[ind];
        }
    }

}
