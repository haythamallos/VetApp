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
                Evaluation evaluation = busFacCore.EvaluationGet(user);
                if (evaluation != null)
                {
                    dashboardModel.evaluationResults = new EvaluationResults();
                    dashboardModel.evaluationModel = new EvaluationModel();

                    int currentRating = 0;
                    if (user.CurrentRating > 0)
                    {
                        currentRating = (int) user.CurrentRating;
                    }
                    else
                    {
                        currentRating = (int)evaluation.CurrentRating;
                    }
                    dashboardModel.evaluationResults.CurrentRating = currentRating;

                    int projectionFactor = 3;
                    int delta = (100 - currentRating) /10;
                    if ( delta < projectionFactor)
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

                    dashboardModel.evaluationResults.AmountIncreasePerMonth = amountIncreasePerMonth;
                    dashboardModel.evaluationResults.AmountIncreasePerYear = amountIncreasePerMonth * 12;
                    dashboardModel.evaluationResults.TotalPerMonthAfterIncrease = RatingProjections.RatingTable_1[dashboardModel.evaluationResults.PotentialVARating].TotalPerMonth;
                }
            }


            return View(dashboardModel);

        }

        public ActionResult MainMenu()
        {
            return View();
        }
        public ActionResult Preliminary()
        {
            return View();
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
                SSN = user.Ssn
            };
            int currentRating = 0;
            if (user.CurrentRating > 0)
            {
                currentRating = (int) user.CurrentRating;
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
                user.Ssn = userModel.SSN;
                user.PhoneNumber = userModel.PhoneNumber;
                if (!user.Passwd.Equals(userModel.Password))
                {
                    user.Passwd = UtilsSecurity.encrypt(userModel.Password);
                }
                user.UserMessage = userModel.Message;
                user.CurrentRating = userModel.CurrentRating;
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
        public ActionResult BackForm(BackModel backModel, string submitButton)
        {
            try
            {
                BusFacPDF busFacPDF = new BusFacPDF();
                string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
                User user = Auth();
                switch (submitButton)
                {
                    case "Save":
                        backModel.TemplatePath = pdfTemplatePath;
                        backModel.UserID = user.UserID;
                        long ContentID = busFacPDF.Save(backModel);
                        break;
                    case "Submit":
                        break;
                    case "PDF":
                        byte[] form = busFacPDF.Back(pdfTemplatePath, backModel);
                        PDFHelper.ReturnPDF(form, "back-dbq.pdf");
                        break;
                }

            }
            catch (Exception ex)
            {

            }
            return View(backModel);
        }

        public ActionResult LogOut()
        {
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


