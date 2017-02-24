using iTextSharp.text.pdf;
using MainSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainSite.Utils;
using System.IO;
using iTextSharp.text;
using System.Text;

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
                byte[] form = generateDBQBack(pdfTemplatePath, back);
                PDFHelper.ReturnPDF(form, "back-dbq.pdf");
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

        private byte[] generateDBQBack(string pdfTemplatePath, BackModel back)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(pdfTemplatePath);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;
                        back.S60 = string.Empty;
                        string dt = System.DateTime.Today.ToShortDateString();
                        if (!back.S47)
                        {
                            if (back.S48)
                            {
                                back.S60 += "Mechanical back pain syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses1[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis1[0]", dt);
                            }
                            if (back.S49)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses2[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis2[0]", dt);
                                back.S60 += ", Lumbosacral sprain/strain";
                            }
                            if (back.S50)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses3[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis3[0]", dt);
                                back.S60 += ", Facet joint arthropathy (degenerative joint disease of lumbosacral spine)";
                            }
                            if (back.S51)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses4[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Degenerative disc disease";
                            }
                            if (back.S1)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses16[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Degenerative scoliosis";
                            }
                            if (back.S52)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses5[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Foraminal/lateral recess/central stenosis";
                            }
                            if (back.S53)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses6[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Degenerative spondylolisthesis";
                            }
                            if (back.S54)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses7[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Spondylolysis/isthmic spondylolisthesis";
                            }
                            if (back.S55)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses8[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Intervertebral disc syndrome";
                            }
                            if (back.S13)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses9[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Radiculopathy";
                            }
                            if (back.S12)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses10[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Ankylosis of thoracolumbar spine";
                            }
                            if (back.S7)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses11[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Ankylosing spondylitis of the thoracolumbar spine (back)";
                            }
                            if (back.S6)
                            {
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses15[0]", "1");
                                pdfFormFields.SetField("", dt);
                                back.S60 += ", Vertebral fracture (vertebrae of the back)";
                            }
                        
                        }

                        iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 6f, iTextSharp.text.Font.NORMAL);
                        //set the field to bold
                        pdfFormFields.SetFieldProperty("form1[0].#subform[0].Records[1]", "textfont", normal.BaseFont, null);
                        pdfFormFields.SetField("form1[0].#subform[0].Records[1]", back.S60);
                    }

                    // Set the flattening flag to true, so the document is not editable
                    pdfStamper.FormFlattening = false;

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
            }

            return form;


        }


        private byte[] GeneratePDF_2(string pdfTemplatePath, StringBuilder sb)
        {
            byte[] form = null;

            try
            {
                // Use iTextSharp PDF Reader, to get the fields and send to the 
                //Stamper to set the fields in the document
                PdfReader pdfReader = new PdfReader(pdfTemplatePath);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        var counter = 1;
                        PdfContentByte content = null;
                        var fieldInfo = new List<string>();
                        foreach (var entry in pdfFormFields.Fields)
                        {
                            var formFieldType = PDFFieldType.GetPDFFieldType(pdfFormFields.GetFieldType(entry.Key.ToString()));

                            if (formFieldType is PDFCheckBoxFieldType)
                                fieldInfo.Add(string.Format("{0} - {1} - Export Value: {2}",
                                                            entry.Key,
                                                            formFieldType,
                                                            PDFHelper.GetExportValue(entry.Value as AcroFields.Item)));
                            else
                                fieldInfo.Add(string.Format("{0} - {1}", entry.Key, formFieldType));

                            Rectangle rectangle = pdfFormFields.GetFieldPositions(entry.Key)[0].position;
                            int page = pdfFormFields.GetFieldPositions(entry.Key)[0].page;
                            //put content over
                            content = pdfStamper.GetOverContent(page);
                            //Text over the existing page
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA,
                                    BaseFont.WINANSI, BaseFont.EMBEDDED);
                            content.BeginText();
                            content.SetFontAndSize(bf, 8);
                            //content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Page No: " + i, 200, 15, 0);
                            content.ShowTextAligned(PdfContentByte.ALIGN_LEFT, counter.ToString(), rectangle.Left, rectangle.Bottom, 0);
                            content.EndText();
                            counter++;
                        }

                        for (int i = 0; i < fieldInfo.Count; i++)
                        {
                            sb.Append((i + 1) + ".  " + fieldInfo[i] + Environment.NewLine);
                        }


                    }

                    // Set the flattening flag to true, so the document is not editable
                    pdfStamper.FormFlattening = false;

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
            }

            return form;
        }
        private void GeneratePDF_1(string pdfTemplatePath, BackModel back)
        {
            PDFFile pdfile = new PDFFile(pdfTemplatePath);


            if ((!pdfile.HasError) && (pdfile.FieldInfoList.Count > 0))
            {
                List<byte[]> fileList = new List<byte[]>();

                var counter = 1;
                var counterChk = 1;
                string oldval = null;
                foreach (PDFFileField f in pdfile.FieldInfoList)
                {
                    if (f.IsCheckBox)
                    {
                        oldval = pdfile.formFieldMap[f.Key];
                        pdfile.formFieldMap[f.Key] = f.ExportValue;
                        pdfile.formFieldMap["form1[0].#subform[0].NameOfVeteran[0]"] = counter.ToString() + ":" + f.fieldinfo();
                        var pdfContents = PDFHelper.GeneratePDF(pdfTemplatePath, pdfile.formFieldMap);
                        fileList.Add(pdfContents);
                        pdfile.formFieldMap[f.Key] = oldval;
                        counterChk++;
                    }
                    counter++;
                }

                using (MemoryStream msOutput = new MemoryStream())
                {
                    PdfReader pdfFile = new PdfReader(fileList[0]);
                    Document doc = new Document();
                    PdfWriter pCopy = new PdfSmartCopy(doc, msOutput);

                    doc.Open();

                    for (int k = 0; k < fileList.Count; k++)
                    {
                        for (int i = 1; i < pdfFile.NumberOfPages + 1; i++)
                        {
                            pdfFile = new PdfReader(fileList[k]);
                            ((PdfSmartCopy)pCopy).AddPage(pCopy.GetImportedPage(pdfFile, i));
                            pCopy.FreeReader(pdfFile);
                        }
                    }

                    pdfFile.Close();
                    pCopy.Close();
                    doc.Close();
                    fileList.Clear();

                    byte[] form = msOutput.ToArray();
                    PDFHelper.ReturnPDF(form, "back-merged.pdf");
                    //using (FileStream fileSteam = new FileStream(@"C:\Temp\Merged.pdf", FileMode.Create))
                    //{
                    //    fileStream.Write(form, 0, form.Length);
                    //}
                }

            }

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


