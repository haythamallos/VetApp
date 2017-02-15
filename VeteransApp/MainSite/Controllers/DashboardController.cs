using iTextSharp.text.pdf;
using MainSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainSite.Utils;

namespace MainSite.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
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
        public ActionResult BackPDF(BackModel back)
        {
            try
            {
                string pdfTemplatePath = Server.MapPath(Url.Content("~/Content/pdf/back.pdf"));
                btnGeneratePDF_Back(pdfTemplatePath, back);
            }
            catch (Exception ex)
            {

            }
            return View(back);
        }

        public ActionResult LogOut()
        {
            try
            {
                HttpContext.Session.Clear();
            }
            catch { }
            return RedirectToAction("Index", "Home");
        }

        private void btnGeneratePDF_Back(string pdfTemplatePath, BackModel back)
        {
            // Get the form fields for this PDF and give them increasing values
            var formFieldMap = PDFHelper.GetFormFieldNames(pdfTemplatePath);

            //var counter = 1;
            formFieldMap["form1[0].#subform[0].NameOfVeteran[0]"] = back.NameOfPatient;
            formFieldMap["form1[0].#subform[0].SSN[0]"] = back.SocialSecurity;
            formFieldMap["form1[0].#subform[0].Records[1]"] = back.BackDiagnosis;

            //foreach (var key in new List<string>(formFieldMap.Keys))
            //{
            //    formFieldMap[key] = counter.ToString();               
            //    counter++;
            //}

            var pdfContents = PDFHelper.GeneratePDF(pdfTemplatePath, formFieldMap);

            PDFHelper.ReturnPDF(pdfContents,"Back-Sample.pdf");
        }
    }
}