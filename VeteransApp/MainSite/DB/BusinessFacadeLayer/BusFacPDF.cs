using System;
using iTextSharp.text.pdf;
using MainSite.Models;
using System.IO;
using iTextSharp.text;

using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.Common;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Vetapp.Engine.BusinessFacadeLayer
{
    public class BusFacPDF
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
        public BusFacPDF()
        {
            _config = new Config();
        }

        public long Save(IBaseModel model, long contentStateID, long contentTypeID, bool isNew = false)
        {
            long lID = 0;
            try
            {
                bool bAddID = false;


                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(model.UserID, contentTypeID);
                if ((content == null) || isNew)
                {
                    content = new Content() { UserID = model.UserID, ContentTypeID = contentTypeID };
                    model.ContentTypeID = contentTypeID;
                    bAddID = true;
                }
                else
                {
                    model.ContentID = content.ContentID;
                }

                model.ContentTypeID = contentTypeID;
                if ((contentStateID >= content.ContentStateID) && (contentStateID != 9999))
                {
                    content.ContentStateID = contentStateID;
                }
                model.ContentStateID = content.ContentStateID;
                content.ContentMeta = JSONHelper.Serialize<IBaseModel>(model);
                lID = busFacCore.ContentCreateOrModify(content);
                if (bAddID)
                {
                    model.ContentID = lID;
                    content.ContentMeta = JSONHelper.Serialize<IBaseModel>(model);
                    lID = busFacCore.ContentCreateOrModify(content);
                }
                _hasError = busFacCore.HasError;
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }
            return lID;
        }

        public byte[] Back(string pdfTemplatePath, BackModel back)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(pdfTemplatePath);

                //BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                //iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                //if (pdfReader.HasUsageRights())
                //{
                //    pdfReader.RemoveUsageRights();
                //}

                //if (pdfReader.HasUsageRights())
                //{
                //    pdfReader.RemoveUsageRights();
                //    PdfStamper pdfStamper = null;
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                //        {
                //            pdfStamper.FormFlattening = false;

                //        }
                //        // close the pdf stamper
                //        pdfStamper.Close();
                //        pdfReader.Close();
                //        form = ms.ToArray();
                //        return form;
                //    }
                //}
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;
                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {

                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;
                        //back.S60 = string.Empty;
                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;


                        pdfFormFields.GenerateAppearances = true; ;

                        // defaults
                        //form1[0].#subform[9].YesNo22[1]

                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].SeverityAnkylosis[0]", "4", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo12[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13A[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo13[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo20[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo25[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo22[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);


                        pdfFormFields.SetField("form1[0].#subform[0].NameOfVeteran[0]", back.NameOfPatient, true);

                        SSN ssn = UtilsString.ParseSSN(back.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.backPDFItems[57].Code, ssn.ToString(), true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.backPDFItems[57].Code, back.SocialSecurity, true);
                        }


                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1", true);

                        pdfFormFields.SetField(PDFItems.backPDFItems[329].Code, PDFItems.backPDFItems[329].ExportValue, true);

                        if (!back.S47)
                        {
                            if (back.S48)
                            {
                                diagnosis = "Mechanical back pain syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses1[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis1[0]", dt, true);
                                //back.S60 += diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode1[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S49)
                            {
                                diagnosis = "Lumbosacral sprain/strain";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses2[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis2[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode2[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S50)
                            {
                                diagnosis = "Facet joint arthropathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses3[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis3[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode3[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S51)
                            {
                                diagnosis = "Degenerative disc disease";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses4[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis4[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode4[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S1)
                            {
                                diagnosis = "Degenerative scoliosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses16[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis16[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode16[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S52)
                            {
                                diagnosis = "Foraminal/lateral recess/central stenosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses5[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis5[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode5[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S53)
                            {
                                diagnosis = "Degenerative spondylolisthesis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses6[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis6[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode6[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S54)
                            {
                                diagnosis = "Spondylolysis/isthmic spondylolisthesis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses7[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis7[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode7[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S55)
                            {
                                diagnosis = "Intervertebral disc syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses8[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis8[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode8[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S13)
                            {
                                diagnosis = "Radiculopathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses9[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis9[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode9[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S12)
                            {
                                diagnosis = "Ankylosis of thoracolumbar spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses10[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis10[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode10[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S7)
                            {
                                diagnosis = "Ankylosing spondylitis of the thoracolumbar spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses11[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis11[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode11[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (back.S6)
                            {
                                diagnosis = "Vertebral fracture";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses15[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis15[0]", dt, true);
                                //back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode15[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (!string.IsNullOrEmpty(back.S62))
                            {
                                diagnosis = "Other";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses12[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis12[0]", dt, true);
                                pdfFormFields.SetField("form1[0].#subform[0].OtherDiagnosis1[0]", back.S62, true);
                                //back.S60 += ", " + back.S62;
                            }
                            //if (back.S60.LastIndexOf(",") == 0)
                            //{
                            //    back.S60 = back.S60.Replace(",", string.Empty).Trim();
                            //}

                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[0].NoDiagnoses[0]", "1", true);
                        }

                        //iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 6f, iTextSharp.text.Font.NORMAL);
                        //normal = FontFactory.GetFont(FontFactory.COURIER, 4f, iTextSharp.text.Font.NORMAL);

                        //set the field to bold
                        //pdfFormFields.SetFieldProperty("form1[0].#subform[0].Records[1]", "textfont", normal.BaseFont, null);
                        pdfFormFields.SetField("form1[0].#subform[0].Records[1]", back.S60, true);

                        // Section 1D
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        // Section 2A
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", Defaults.BACK_SECION_2A, true);
                        // Section 2B
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", Defaults.BACK_SECION_2B, true);
                        // Section 2C
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", Defaults.BACK_SECION_2C, true);

                        if (back.S95)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].Forward_Flexion[1]", "2", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM1[0]", back.S96, true);
                        }

                        if (back.S93)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].Extension[0]", "2", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM2[0]", back.S86, true);
                        }

                        if (back.S92)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].RightLateral_Flexion[1]", "2", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM4[0]", back.S90, true);
                        }

                        if (back.S89)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].LeftLateral_Flexion[1]", "2", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM3[0]", back.S87, true);
                        }

                        if (back.S81)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].RightLateral_Rotation[1]", "2", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM6[0]", back.S82, true);
                        }

                        if (back.S79)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].LeftLateral_Rotation[0]", "2", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM5[0]", back.S78, true);
                        }

                        bool isAllSame = true;
                        //if (!string.IsNullOrEmpty(back.S112))
                        //{
                        //    if (back.S112 != back.S96)
                        //    {
                        //        isAllSame = false;
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(back.S111))
                        //{
                        //    if (back.S111 != back.S86)
                        //    {
                        //        isAllSame = false;
                        //    }

                        //}
                        //if (!string.IsNullOrEmpty(back.S113))
                        //{
                        //    if (back.S113 != back.S90)
                        //    {
                        //        isAllSame = false;
                        //    }

                        //}
                        //if (!string.IsNullOrEmpty(back.S114))
                        //{
                        //    if (back.S114 != back.S87)
                        //    {
                        //        isAllSame = false;
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(back.S120))
                        //{
                        //    if (back.S120 != back.S82)
                        //    {
                        //        isAllSame = false;
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(back.S121))
                        //{
                        //    if (back.S121 != back.S78)
                        //    {
                        //        isAllSame = false;
                        //    }
                        //}

                        //pdfFormFields.SetField("form1[0].#subform[2].Right_Perform[0]", "1", true);


                        //if (isAllSame)
                        //{                           
                        //    pdfFormFields.SetField("form1[0].#subform[2].Right_Limitation[0]", "2", true);
                        //}
                        //else
                        //{
                        //    // insert values
                        //    pdfFormFields.SetField(PDFItems.backPDFItems[112].Code, back.S112, true);
                        //    pdfFormFields.SetField(PDFItems.backPDFItems[111].Code, back.S111, true);
                        //    pdfFormFields.SetField(PDFItems.backPDFItems[113].Code, back.S113, true);
                        //    pdfFormFields.SetField(PDFItems.backPDFItems[114].Code, back.S114, true);
                        //    pdfFormFields.SetField(PDFItems.backPDFItems[120].Code, back.S120, true);
                        //    pdfFormFields.SetField(PDFItems.backPDFItems[121].Code, back.S121, true);
                        //    pdfFormFields.SetField(PDFItems.backPDFItems[118].Code, PDFItems.backPDFItems[118].ExportValue, true);

                        //}
                        pdfFormFields.SetField(PDFItems.backPDFItems[115].Code, PDFItems.backPDFItems[115].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.backPDFItems[117].Code, PDFItems.backPDFItems[117].ExportValue, true);

                        //pdfFormFields.SetField(PDFItems.backPDFItems[112].Code, back.S112, true);
                        //pdfFormFields.SetField(PDFItems.backPDFItems[111].Code, back.S111, true);
                        //pdfFormFields.SetField(PDFItems.backPDFItems[113].Code, back.S113, true);
                        //pdfFormFields.SetField(PDFItems.backPDFItems[114].Code, back.S114, true);
                        //pdfFormFields.SetField(PDFItems.backPDFItems[120].Code, back.S120, true);
                        //pdfFormFields.SetField(PDFItems.backPDFItems[121].Code, back.S121, true);


                        pdfFormFields.SetField("form1[0].#subform[2].Neck_Painful[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Neck_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss1[1]", "1", true);

                        if (back.S135Tenderness)
                        {
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Tender[0]", "1", true);
                            pdfFormFields.SetField("form1[0].#subform[2].Explain[2]", "Mild TTP Lumbar Paraspinal.", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Tender[1]", "2", true);
                        }

                        bool doGuarding = true;
                        if ((!back.S145MuscleSpasmQuestion) && (!back.S145GuardingQuestion))
                        {
                            //pdfFormFields.SetField("form1[0].#subform[3].Gait[0]", "1", true);
                            pdfFormFields.SetField(PDFItems.backPDFItems[145].Code, PDFItems.backPDFItems[145].ExportValue, true);
                            doGuarding = false;
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].Gait[1]", "2", true);
                            if ((back.S145MuscleSpasm) && (back.S145MuscleSpasmQuestion))
                            {
                                pdfFormFields.SetField("form1[0].#subform[3].DueTo[0]", "1", true);
                            }
                            if ((back.S145Guarding) && (back.S145GuardingQuestion))
                            {
                                pdfFormFields.SetField("form1[0].#subform[3].DueTo[1]", "2", true);
                            }
                        }

                        if (((back.S145MuscleSpasm) || (back.S145Guarding)) && doGuarding)
                        {
                            //pdfFormFields.SetField("form1[0].#subform[3].Guarding_Spasms[1]", "1", true);
                            pdfFormFields.SetField(PDFItems.backPDFItems[152].Code, PDFItems.backPDFItems[152].ExportValue, true);
                        }
                        else
                        {
                            //pdfFormFields.SetField("form1[0].#subform[3].Guarding_Spasms[0]", "2", true);
                            pdfFormFields.SetField(PDFItems.backPDFItems[151].Code, PDFItems.backPDFItems[151].ExportValue, true);
                        }


                        pdfFormFields.SetField("form1[0].#subform[3].SpinalContour[1]", "1", true);

                        if (back.S159)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor1[0]", "1", true);
                        }
                        if (back.S164)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor2[0]", "1", true);
                        }
                        if (back.S158)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor3[0]", "1", true);
                        }
                        if (back.S157)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor4[0]", "1", true);
                        }
                        if (back.S171)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor5[0]", "1", true);
                        }
                        if (back.S170)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor6[0]", "1", true);
                        }
                        if (back.S169)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor7[0]", "1", true);
                        }
                        if (back.S168)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor8[0]", "1", true);
                        }
                        if (back.S167)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor9[0]", "1", true);
                        }
                        if (back.S160)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor10[0]", "1", true);
                        }
                        if (back.S163)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor11[0]", "1", true);
                        }
                        if (back.S161)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor12[0]", "1", true);
                        }
                        if (back.S162)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor13[0]", "1", true);
                        }

                        if (back.S184 != back.S112)
                        {
                            isAllSame = false;
                        }

                        if (back.S185 != back.S111)
                        {
                            isAllSame = false;
                        }

                        if (back.S174 != back.S113)
                        {
                            isAllSame = false;
                        }

                        if (back.S180 != back.S114)
                        {
                            isAllSame = false;
                        }

                        if (back.S175 != back.S120)
                        {
                            isAllSame = false;
                        }

                        if (back.S177 != back.S121)
                        {
                            isAllSame = false;
                        }

                        if (!isAllSame)
                        {
                            pdfFormFields.SetField("form1[0].#subform[4].ROM19[0]", back.S184, true);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM20[0]", back.S185, true);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM21[0]", back.S174, true);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM22[0]", back.S180, true);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM23[0]", back.S175, true);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM24[0]", back.S177, true);

                            pdfFormFields.SetField("form1[0].#subform[4].YesNo6[1]", "1", true);
                            pdfFormFields.SetField("form1[0].#subform[4].Right_Limit[0]", "1", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[4].YesNo6[0]", "2", true);
                            pdfFormFields.SetField("form1[0].#subform[4].Right_Limit[1]", "2", true);
                        }

                        // Radiculopath public use.  Different for internal
                        //if (back.S316)
                        //{
                        //    pdfFormFields.SetField("form1[0].#subform[6].YesNo11[0]", "1", true);

                        //    SetField_SRadiculopathyConstantPainLevelAnswer(back, pdfFormFields);
                        //    SetField_SRadiculopathyIntermittentPainLevelAnswer(back, pdfFormFields);
                        //    SetField_SRadiculopathyDullPainLevelAnswer(back, pdfFormFields);
                        //    SetField_SRadiculopathyTinglingPainLevelAnswer(back, pdfFormFields);
                        //    SetField_SRadiculopathyNumbnessPainLevelAnswer(back, pdfFormFields);
                        //    SetField_SRadiculopathySeverityLevel(back, pdfFormFields);

                        //}
                        //else
                        //{
                        //    //pdfFormFields.SetField("form1[0].#subform[6].YesNo11[1]", "2", true);

                        //    pdfFormFields.SetField(PDFItems.backPDFItems[334].Code, PDFItems.backPDFItems[334].ExportValue, true);

                        //}

                        // internal use radiculopath rules
                        if (!string.IsNullOrEmpty(back.S13AChoice))
                        {
                            if (back.S13AChoice == "NO")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[199].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[192].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[193].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[198].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[197].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[194].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[195].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[196].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[226].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[219].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[220].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[225].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[224].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[221].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[222].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[223].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[244].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[241].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[242].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[243].Code, "2", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[310].Code, PDFItems.backPDFItems[310].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[288].Code, PDFItems.backPDFItems[288].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[287].Code, PDFItems.backPDFItems[287].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[275].Code, PDFItems.backPDFItems[275].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[291].Code, PDFItems.backPDFItems[291].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[306].Code, PDFItems.backPDFItems[306].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[301].Code, PDFItems.backPDFItems[301].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[317].Code, PDFItems.backPDFItems[317].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[270].Code, PDFItems.backPDFItems[270].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[274].Code, PDFItems.backPDFItems[274].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[281].Code, PDFItems.backPDFItems[281].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[280].Code, PDFItems.backPDFItems[280].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[300].Code, PDFItems.backPDFItems[300].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[294].Code, PDFItems.backPDFItems[294].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[299].Code, PDFItems.backPDFItems[299].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[320].Code, PDFItems.backPDFItems[320].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[331].Code, PDFItems.backPDFItems[331].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[334].Code, PDFItems.backPDFItems[334].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[329].Code, PDFItems.backPDFItems[329].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[262].Code, PDFItems.backPDFItems[262].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[251].Code, PDFItems.backPDFItems[251].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[363].Code, PDFItems.backPDFItems[363].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[350].Code, PDFItems.backPDFItems[350].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[344].Code, PDFItems.backPDFItems[344].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[203].Code, PDFItems.backPDFItems[203].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[230].Code, PDFItems.backPDFItems[230].ExportValue, true);


                            }
                            else if (back.S13AChoice == "YES")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[316].Code, PDFItems.backPDFItems[316].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[323].Code, PDFItems.backPDFItems[323].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[268].Code, PDFItems.backPDFItems[268].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[245].Code, PDFItems.backPDFItems[245].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[357].Code, PDFItems.backPDFItems[357].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[356].Code, PDFItems.backPDFItems[356].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[380].Code, PDFItems.backPDFItems[380].ExportValue, true);

                            }

                            if ((back.S13AChoiceLeftLeg != "NONE") && (back.S13AChoiceRightLeg != "NONE"))
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[377].Code, PDFItems.backPDFItems[377].ExportValue, true);
                            }

                            if (back.S13AChoiceRightLeg == "NONE")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[310].Code, PDFItems.backPDFItems[310].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[288].Code, PDFItems.backPDFItems[288].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[287].Code, PDFItems.backPDFItems[287].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[275].Code, PDFItems.backPDFItems[275].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[291].Code, PDFItems.backPDFItems[291].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[306].Code, PDFItems.backPDFItems[306].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[301].Code, PDFItems.backPDFItems[301].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[317].Code, PDFItems.backPDFItems[317].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[326].Code, PDFItems.backPDFItems[326].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[265].Code, PDFItems.backPDFItems[265].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[248].Code, PDFItems.backPDFItems[248].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[360].Code, PDFItems.backPDFItems[360].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[353].Code, PDFItems.backPDFItems[353].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[341].Code, PDFItems.backPDFItems[341].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[380].Code, PDFItems.backPDFItems[380].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[379].Code, PDFItems.backPDFItems[379].ExportValue, true);
                            }
                            else if (back.S13AChoiceRightLeg == "MILD")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[199].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[192].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[193].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[198].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[197].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[194].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[195].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[196].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[244].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[241].Code, "2", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[310].Code, PDFItems.backPDFItems[310].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[288].Code, PDFItems.backPDFItems[288].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[287].Code, PDFItems.backPDFItems[287].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[277].Code, PDFItems.backPDFItems[277].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[318].Code, PDFItems.backPDFItems[318].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[307].Code, PDFItems.backPDFItems[307].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[306].Code, PDFItems.backPDFItems[306].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[301].Code, PDFItems.backPDFItems[301].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[325].Code, PDFItems.backPDFItems[325].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[266].Code, PDFItems.backPDFItems[266].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[247].Code, PDFItems.backPDFItems[247].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[359].Code, PDFItems.backPDFItems[359].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[354].Code, PDFItems.backPDFItems[354].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[340].Code, PDFItems.backPDFItems[340].ExportValue, true);

                            }

                            if (back.S13AChoiceLeftLeg == "NONE")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[270].Code, PDFItems.backPDFItems[270].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[274].Code, PDFItems.backPDFItems[274].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[281].Code, PDFItems.backPDFItems[281].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[300].Code, PDFItems.backPDFItems[300].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[294].Code, PDFItems.backPDFItems[294].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[299].Code, PDFItems.backPDFItems[299].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[320].Code, PDFItems.backPDFItems[320].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[331].Code, PDFItems.backPDFItems[331].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[260].Code, PDFItems.backPDFItems[260].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[253].Code, PDFItems.backPDFItems[253].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[365].Code, PDFItems.backPDFItems[365].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[348].Code, PDFItems.backPDFItems[348].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[344].Code, PDFItems.backPDFItems[344].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[380].Code, PDFItems.backPDFItems[380].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[378].Code, PDFItems.backPDFItems[378].ExportValue, true);

                            }
                            else if (back.S13AChoiceLeftLeg == "MILD")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[226].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[219].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[220].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[225].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[224].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[221].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[222].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[223].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[242].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[243].Code, "2", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[270].Code, PDFItems.backPDFItems[270].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[274].Code, PDFItems.backPDFItems[274].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[281].Code, PDFItems.backPDFItems[281].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[278].Code, PDFItems.backPDFItems[278].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[293].Code, PDFItems.backPDFItems[293].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[294].Code, PDFItems.backPDFItems[294].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[299].Code, PDFItems.backPDFItems[299].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[321].Code, PDFItems.backPDFItems[321].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[332].Code, PDFItems.backPDFItems[332].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[259].Code, PDFItems.backPDFItems[259].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[254].Code, PDFItems.backPDFItems[254].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[366].Code, PDFItems.backPDFItems[366].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[347].Code, PDFItems.backPDFItems[347].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[345].Code, PDFItems.backPDFItems[345].ExportValue, true);

                            }

                            if (back.S13AChoiceRightLeg == "MODERATE")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[199].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[192].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[193].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[198].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[197].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[194].Code, "4", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[195].Code, "4", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[196].Code, "4", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[244].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[241].Code, "1", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[202].Code, PDFItems.backPDFItems[202].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[205].Code, PDFItems.backPDFItems[205].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[310].Code, PDFItems.backPDFItems[310].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[290].Code, PDFItems.backPDFItems[290].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[285].Code, PDFItems.backPDFItems[285].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[276].Code, PDFItems.backPDFItems[276].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[307].Code, PDFItems.backPDFItems[307].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[306].Code, PDFItems.backPDFItems[306].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[302].Code, PDFItems.backPDFItems[302].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[318].Code, PDFItems.backPDFItems[318].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[324].Code, PDFItems.backPDFItems[324].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[267].Code, PDFItems.backPDFItems[267].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[246].Code, PDFItems.backPDFItems[246].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[358].Code, PDFItems.backPDFItems[358].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[355].Code, PDFItems.backPDFItems[355].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[339].Code, PDFItems.backPDFItems[339].ExportValue, true);

                            }

                            if (back.S13AChoiceLeftLeg == "MODERATE")
                            {
                                pdfFormFields.SetField(PDFItems.backPDFItems[226].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[219].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[220].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[225].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[224].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[221].Code, "4", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[222].Code, "4", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[223].Code, "4", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[242].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[243].Code, "1", true);

                                pdfFormFields.SetField(PDFItems.backPDFItems[229].Code, PDFItems.backPDFItems[229].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[232].Code, PDFItems.backPDFItems[232].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[333].Code, PDFItems.backPDFItems[333].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[258].Code, PDFItems.backPDFItems[258].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[255].Code, PDFItems.backPDFItems[255].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[367].Code, PDFItems.backPDFItems[367].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[338].Code, PDFItems.backPDFItems[338].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[346].Code, PDFItems.backPDFItems[346].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[270].Code, PDFItems.backPDFItems[270].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[274].Code, PDFItems.backPDFItems[274].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[283].Code, PDFItems.backPDFItems[283].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[279].Code, PDFItems.backPDFItems[279].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[293].Code, PDFItems.backPDFItems[293].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[294].Code, PDFItems.backPDFItems[294].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.backPDFItems[299].Code, PDFItems.backPDFItems[299].ExportValue, true);

                            }

                            //if (back.S13AChoice == "NONE")
                            //{
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[334].Code, PDFItems.backPDFItems[334].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[317].Code, PDFItems.backPDFItems[317].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[320].Code, PDFItems.backPDFItems[320].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[310].Code, PDFItems.backPDFItems[310].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[270].Code, PDFItems.backPDFItems[270].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[288].Code, PDFItems.backPDFItems[288].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[274].Code, PDFItems.backPDFItems[274].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[287].Code, PDFItems.backPDFItems[287].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[281].Code, PDFItems.backPDFItems[281].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[275].Code, PDFItems.backPDFItems[275].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[280].Code, PDFItems.backPDFItems[280].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[314].Code, PDFItems.backPDFItems[314].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[315].Code, PDFItems.backPDFItems[315].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[312].Code, PDFItems.backPDFItems[312].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[311].Code, PDFItems.backPDFItems[311].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[368].Code, PDFItems.backPDFItems[368].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[341].Code, PDFItems.backPDFItems[341].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[344].Code, PDFItems.backPDFItems[344].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[244].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[241].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[242].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[243].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[199].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[192].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[193].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[198].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[197].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[194].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[195].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[196].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[203].Code, PDFItems.backPDFItems[203].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[226].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[219].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[220].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[225].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[224].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[221].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[222].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[223].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[230].Code, PDFItems.backPDFItems[230].ExportValue, true);
                            //}
                            //else if (back.S13AChoice == "MILD")
                            //{
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[316].Code, PDFItems.backPDFItems[316].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[380].Code, PDFItems.backPDFItems[380].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[377].Code, PDFItems.backPDFItems[377].ExportValue, true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[199].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[192].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[193].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[198].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[197].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[194].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[195].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[196].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[203].Code, PDFItems.backPDFItems[203].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[226].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[219].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[220].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[225].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[224].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[221].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[222].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[223].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[230].Code, PDFItems.backPDFItems[230].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[244].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[241].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[242].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[243].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[310].Code, PDFItems.backPDFItems[310].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[288].Code, PDFItems.backPDFItems[288].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[287].Code, PDFItems.backPDFItems[287].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[277].Code, PDFItems.backPDFItems[277].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[270].Code, PDFItems.backPDFItems[270].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[274].Code, PDFItems.backPDFItems[274].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[281].Code, PDFItems.backPDFItems[281].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[278].Code, PDFItems.backPDFItems[278].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[340].Code, PDFItems.backPDFItems[340].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[345].Code, PDFItems.backPDFItems[345].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[268].Code, PDFItems.backPDFItems[268].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[266].Code, PDFItems.backPDFItems[266].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[245].Code, PDFItems.backPDFItems[245].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[247].Code, PDFItems.backPDFItems[247].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[254].Code, PDFItems.backPDFItems[254].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[357].Code, PDFItems.backPDFItems[357].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[359].Code, PDFItems.backPDFItems[359].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[366].Code, PDFItems.backPDFItems[366].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[356].Code, PDFItems.backPDFItems[356].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[354].Code, PDFItems.backPDFItems[354].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[347].Code, PDFItems.backPDFItems[347].ExportValue, true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[307].Code, PDFItems.backPDFItems[307].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[293].Code, PDFItems.backPDFItems[293].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[306].Code, PDFItems.backPDFItems[306].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[294].Code, PDFItems.backPDFItems[294].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[301].Code, PDFItems.backPDFItems[301].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[299].Code, PDFItems.backPDFItems[299].ExportValue, true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[318].Code, PDFItems.backPDFItems[318].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[321].Code, PDFItems.backPDFItems[321].ExportValue, true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[259].Code, PDFItems.backPDFItems[259].ExportValue, true);

                            //}
                            //else if (back.S13AChoice == "MODERATE")
                            //{
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[316].Code, PDFItems.backPDFItems[316].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[380].Code, PDFItems.backPDFItems[380].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[377].Code, PDFItems.backPDFItems[377].ExportValue, true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[199].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[192].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[193].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[198].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[197].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[194].Code, "4", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[195].Code, "4", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[196].Code, "4", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[202].Code, PDFItems.backPDFItems[202].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[205].Code, PDFItems.backPDFItems[205].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[226].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[219].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[220].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[225].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[224].Code, "5", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[221].Code, "4", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[222].Code, "4", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[223].Code, "4", true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[229].Code, PDFItems.backPDFItems[229].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[232].Code, PDFItems.backPDFItems[232].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[244].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[242].Code, "2", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[241].Code, "1", true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[243].Code, "1", true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[310].Code, PDFItems.backPDFItems[310].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[288].Code, PDFItems.backPDFItems[288].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[270].Code, PDFItems.backPDFItems[270].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[274].Code, PDFItems.backPDFItems[274].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[285].Code, PDFItems.backPDFItems[285].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[283].Code, PDFItems.backPDFItems[283].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[276].Code, PDFItems.backPDFItems[276].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[279].Code, PDFItems.backPDFItems[279].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[308].Code, PDFItems.backPDFItems[308].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[292].Code, PDFItems.backPDFItems[292].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[305].Code, PDFItems.backPDFItems[305].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[295].Code, PDFItems.backPDFItems[295].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[302].Code, PDFItems.backPDFItems[302].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[298].Code, PDFItems.backPDFItems[298].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[318].Code, PDFItems.backPDFItems[318].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[321].Code, PDFItems.backPDFItems[321].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[268].Code, PDFItems.backPDFItems[268].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[267].Code, PDFItems.backPDFItems[267].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[258].Code, PDFItems.backPDFItems[258].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[245].Code, PDFItems.backPDFItems[245].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[246].Code, PDFItems.backPDFItems[246].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[357].Code, PDFItems.backPDFItems[357].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[358].Code, PDFItems.backPDFItems[358].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[367].Code, PDFItems.backPDFItems[367].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[356].Code, PDFItems.backPDFItems[356].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[355].Code, PDFItems.backPDFItems[355].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[338].Code, PDFItems.backPDFItems[338].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[339].Code, PDFItems.backPDFItems[339].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[346].Code, PDFItems.backPDFItems[346].ExportValue, true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[308].Code, PDFItems.backPDFItems[308].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[292].Code, PDFItems.backPDFItems[292].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[305].Code, PDFItems.backPDFItems[305].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[295].Code, PDFItems.backPDFItems[295].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[302].Code, PDFItems.backPDFItems[302].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[298].Code, PDFItems.backPDFItems[298].ExportValue, true);

                            //    pdfFormFields.SetField(PDFItems.backPDFItems[318].Code, PDFItems.backPDFItems[318].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[321].Code, PDFItems.backPDFItems[321].ExportValue, true);
                            //    pdfFormFields.SetField(PDFItems.backPDFItems[255].Code, PDFItems.backPDFItems[255].ExportValue, true);


                            //}
                        }

                        if (back.S55)
                        {
                            pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[0]", "3", true);
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[7].YesNo14A[1]", "2", true);
                        }

                        if (!string.IsNullOrEmpty(back.S15C))
                        {
                            SetField_S15C(back, pdfFormFields);
                        }

                        if (back.S414)
                        {
                            pdfFormFields.SetField("form1[0].#subform[8].YesNo18[0]", "1", true);
                            SetField_S17ABrace(back, pdfFormFields);
                            SetField_S17ACrutches(back, pdfFormFields);
                            SetField_S17ACane(back, pdfFormFields);
                            SetField_S17AWalker(back, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[8].YesNo18[1]", "2", true);
                        }

                        // corrections

                        switch (back.S13JChoice)
                        {
                            case "RIGHT":
                                pdfFormFields.SetField(PDFItems.backPDFItems[374].Code, PDFItems.backPDFItems[374].ExportValue, true);
                                break;
                            case "LEFT":
                                pdfFormFields.SetField(PDFItems.backPDFItems[375].Code, PDFItems.backPDFItems[375].ExportValue, true);
                                break;
                            default:
                                break;
                        }

                        pdfFormFields.SetField(PDFItems.backPDFItems[313].Code, PDFItems.backPDFItems[313].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.backPDFItems[405].Code, PDFItems.backPDFItems[405].ExportValue, true);

                        pdfFormFields.SetField(PDFItems.backPDFItems[65].Code, back.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.backPDFItems[66].Code, back.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.backPDFItems[67].Code, back.VarianceFunctionLossWriteIn, true);

                        //if (back.IsFormReadonly)
                        //{
                        //    IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.backPDFItems[118].Code);
                        //    Rectangle rect = lstPos[0].position;
                        //    PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                        //    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        //    cb.SetFontAndSize(bf, 12);
                        //    cb.BeginText();
                        //    cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                        //    cb.ShowText(string.Empty);
                        //    cb.EndText();


                        //}
                        pdfFormFields.SetField(PDFItems.backPDFItems[459].Code, PDFItems.backPDFItems[459].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.backPDFItems[457].Code, "Due to the flareups and severity of this condition, sedentary occupation is recommended.", true);


                    }


                    if (back.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();


                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        /// <summary>
        /// Constant pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyConstantPainLevelAnswer(BackModel back, AcroFields pdfFormFields)
        {
            if (back.SRadiculopathyConstantPainLevelAnswer)
            {
                pdfFormFields.SetField("form1[0].#subform[6].ConstantPain[0]", "1", true);
                switch (back.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyConstantPainLevelAnswer_Left(back, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyConstantPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyConstantPainLevelAnswer_Left(back, pdfFormFields);
                        SetField_SRadiculopathyConstantPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_SRadiculopathyConstantPainLevelAnswer_Left(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyConstantPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity1[2]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity1[3]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity1[0]", "4", true);
                    break;
            }
        }

        private void SetField_SRadiculopathyConstantPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyConstantPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity1[1]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity1[0]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity1[3]", "4", true);
                    break;
            }
        }

        /// <summary>
        /// Intermittent pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyIntermittentPainLevelAnswer(BackModel back, AcroFields pdfFormFields)
        {
            if (back.SRadiculopathyIntermittentPainLevelAnswer)
            {
                pdfFormFields.SetField("form1[0].#subform[6].IntermittentPain[2]", "1", true);
                switch (back.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Left(back, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Left(back, pdfFormFields);
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyIntermittentPainLevelAnswer_Left(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyIntermittentPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity2[1]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity2[0]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity2[3]", "4", true);
                    break;
            }
        }

        private void SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyIntermittentPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity2[2]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity2[3]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity2[0]", "4", true);
                    break;
            }
        }

        /// <summary>
        /// Dull pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyDullPainLevelAnswer(BackModel back, AcroFields pdfFormFields)
        {
            if (back.SRadiculopathyDullPainLevelAnswer)
            {
                pdfFormFields.SetField("form1[0].#subform[6].DullPain[0]", "1", true);
                switch (back.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyDullPainLevelAnswer_Left(back, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyDullPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyDullPainLevelAnswer_Left(back, pdfFormFields);
                        SetField_SRadiculopathyDullPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyDullPainLevelAnswer_Left(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyDullPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity3[2]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity3[3]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity3[0]", "4", true);
                    break;
            }
        }

        private void SetField_SRadiculopathyDullPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyDullPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity3[1]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity3[0]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity3[3]", "4", true);
                    break;
            }
        }

        /// <summary>
        /// Tingling pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyTinglingPainLevelAnswer(BackModel back, AcroFields pdfFormFields)
        {
            if (back.SRadiculopathyTinglingPainLevelAnswer)
            {
                pdfFormFields.SetField("form1[0].#subform[7].Paresthesias[0]", "1", true);
                switch (back.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Left(back, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Left(back, pdfFormFields);
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyTinglingPainLevelAnswer_Left(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyTinglingPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity4[2]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity4[3]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity4[0]", "4", true);
                    break;
            }
        }

        private void SetField_SRadiculopathyTinglingPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyTinglingPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity4[1]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity4[0]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity4[3]", "4", true);
                    break;
            }
        }


        /// <summary>
        /// Numbness pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyNumbnessPainLevelAnswer(BackModel back, AcroFields pdfFormFields)
        {
            if (back.SRadiculopathyNumbnessPainLevelAnswer)
            {
                pdfFormFields.SetField("form1[0].#subform[7].Numbness[2]", "1", true);
                switch (back.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Left(back, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Left(back, pdfFormFields);
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(back, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyNumbnessPainLevelAnswer_Left(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyNumbnessPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity5[1]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity5[0]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity5[3]", "4", true);
                    break;
            }
        }

        private void SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyNumbnessPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity5[2]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity5[3]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity5[0]", "4", true);
                    break;
            }
        }

        /// <summary>
        /// Numbness pain SRadiculopathySeverityLevel
        /// </summary>
        private void SetField_SRadiculopathySeverityLevel(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyLegSide)
            {
                case "LEFT":
                    SetField_SRadiculopathySeverityLevel_Left(back, pdfFormFields);
                    break;
                case "RIGHT":
                    SetField_SRadiculopathySeverityLevel_Right(back, pdfFormFields);
                    break;
                case "BOTH":
                    SetField_SRadiculopathySeverityLevel_Left(back, pdfFormFields);
                    SetField_SRadiculopathySeverityLevel_Right(back, pdfFormFields);
                    break;
                default:
                    break;
            }
        }
        private void SetField_SRadiculopathySeverityLevel_Left(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathySeverityLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity7[2]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity7[3]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity7[0]", "4", true);
                    break;
            }
        }

        private void SetField_SRadiculopathySeverityLevel_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathySeverityLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[1]", "2", true);
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[0]", "3", true);
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[3]", "4", true);
                    break;
            }
        }

        private void SetField_S15C(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.S15C)
            {
                case "ONEWEEK":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[0]", "1", true);
                    break;
                case "TWOWEEKS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[4]", "2", true);
                    break;
                case "FOURWEEKS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[1]", "3", true);
                    break;
                case "SIXWEEKS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[2]", "4", true);
                    break;
                case "SIXWEEKSPLUS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[3]", "5", true);
                    break;
            }
        }

        private void SetField_S17ABrace(BackModel back, AcroFields pdfFormFields)
        {
            if (back.S416)
            {
                pdfFormFields.SetField(PDFItems.backPDFItems[416].Code, PDFItems.backPDFItems[416].ExportValue, true);
                switch (back.S416Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse2[0]", "1", true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse2[1]", "2", true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse2[2]", "3", true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17ACrutches(BackModel back, AcroFields pdfFormFields)
        {
            if (back.S428)
            {
                pdfFormFields.SetField(PDFItems.backPDFItems[428].Code, PDFItems.backPDFItems[428].ExportValue, true);

                switch (back.S428Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse3[2]", "1", true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse3[1]", "2", true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse3[0]", "3", true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17ACane(BackModel back, AcroFields pdfFormFields)
        {
            if (back.S417)
            {
                pdfFormFields.SetField(PDFItems.backPDFItems[417].Code, PDFItems.backPDFItems[417].ExportValue, true);

                switch (back.S417Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse4[0]", "1", true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse4[1]", "2", true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse4[2]", "3", true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17AWalker(BackModel back, AcroFields pdfFormFields)
        {
            if (back.S421)
            {
                pdfFormFields.SetField(PDFItems.backPDFItems[421].Code, PDFItems.backPDFItems[421].ExportValue, true);

                switch (back.S421Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse5[2]", "1", true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse5[1]", "2", true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse5[0]", "3", true);
                        break;
                    default:
                        break;
                }
            }
        }

        /**************************************************************
          * Neck Form
          * 
          *************************************************************/
        public byte[] Neck(string template, NeckModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // defaults
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Neck_Painful[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Neck_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss1[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].SpinalContour[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].SeverityAnkylosis[1]", "4", true);
                        pdfFormFields.SetField("form1[0].#subform[6].AllNormal_Right1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].AllNormal_Left1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].Right_Triceps[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].Left_Triceps[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].Right_Brach[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].Left_Brach[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].ConstantPain[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13A[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].Duration[1]", "3", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo13[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo14[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo15[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].Comments[9]", "None", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo18[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo20[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo25[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo22[1]", "2", true);
                        pdfFormFields.SetField(PDFItems.neckPDFItems[446].Code, PDFItems.neckPDFItems[446].ExportValue, true);

                        //
                        // 14A: Yes, if IVDS is check off on section 1B.

                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1", true);

                        pdfFormFields.SetField(PDFItems.neckPDFItems[56].Code, m.NameOfPatient, true);
                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[55].Code, ssn.ToString(), true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[55].Code, m.SocialSecurity, true);
                        }


                        //m.S58 = string.Empty;
                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;

                        if (!m.S45)
                        {
                            if (m.S46)
                            {
                                diagnosis = "Mechanical Cervical Pain Syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses1[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis1[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode1[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S47)
                            {
                                diagnosis = "Cervical Sprain/Strain";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses2[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis2[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode2[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S48)
                            {
                                diagnosis = "Cervical Spondylosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses3[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis3[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode3[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S49)
                            {
                                diagnosis = "Degenerative Disc Disease";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses4[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis4[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode4[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S50)
                            {
                                diagnosis = "Foraminal Stenosis/Central Stenosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses5[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis5[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode5[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S51)
                            {
                                diagnosis = "Intervertebral Disc Syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses6[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis6[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode6[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S52)
                            {
                                diagnosis = "Radiculopathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses7[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis7[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode7[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S53)
                            {
                                diagnosis = "Myelopathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses8[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis8[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode8[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S7)
                            {
                                diagnosis = "Ankylosis of Cervical Spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses9[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis9[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode9[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S6)
                            {
                                diagnosis = "Ankylosing Spondylitis of The Cervical Spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses10[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis10[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode10[0]", icdcode.RefNumber, true);
                                }
                            }
                            if (m.S1)
                            {
                                diagnosis = "Vertebral Fracture";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses11[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis11[0]", dt, true);
                                //m.S58 += diagnosis + ". ";
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode11[0]", icdcode.RefNumber, true);
                                }
                            }

                            if (!string.IsNullOrEmpty(m.S54))
                            {
                                diagnosis = "Other";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses12[0]", "1", true);
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis12[0]", dt, true);
                                pdfFormFields.SetField("form1[0].#subform[0].OtherDiagnosis1[0]", m.S54, true);
                                //m.S58 += "." + m.S54;
                            }

                            //if (m.S58.LastIndexOf(",") == 0)
                            //{
                            //    m.S58 = m.S58.Replace(",", string.Empty).Trim();
                            //}

                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[0].NoDiagnoses[0]", "1", true);
                        }

                        iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 2f, iTextSharp.text.Font.NORMAL);
                        //normal = FontFactory.GetFont(FontFactory.COURIER, 2f, iTextSharp.text.Font.NORMAL);
                        normal = FontFactory.GetFont(FontFactory.COURIER, 2f, iTextSharp.text.Font.NORMAL);

                        //set the field to bold
                        pdfFormFields.SetFieldProperty(PDFItems.neckPDFItems[58].Code, "textfont", normal.BaseFont, null);
                        pdfFormFields.SetField(PDFItems.neckPDFItems[58].Code, m.S58, true);


                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.", true);

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.", true);

                        //SetField_Neck_2B(m, pdfFormFields);

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.", true);

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Severe restriction of range of motion.", true);

                        pdfFormFields.SetField("form1[0].#subform[1].ROM1[0]", m.S96, true);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM2[0]", m.S86, true);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM4[0]", m.S90, true);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM3[0]", m.S87, true);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM6[0]", m.S82, true);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM5[0]", m.S78, true);

                        //pdfFormFields.SetField("form1[0].#subform[2].PostROM7[0]", m.S109, true);
                        //pdfFormFields.SetField("form1[0].#subform[2].PostROM8[0]", m.S108, true);
                        //pdfFormFields.SetField("form1[0].#subform[2].PostROM9[0]", m.S110, true);
                        //pdfFormFields.SetField("form1[0].#subform[2].PostROM10[0]", m.S111, true);
                        //pdfFormFields.SetField("form1[0].#subform[2].PostROM11[0]", m.S117, true);
                        //pdfFormFields.SetField("form1[0].#subform[2].PostROM12[0]", m.S118, true);

                        pdfFormFields.SetField(PDFItems.neckPDFItems[112].Code, PDFItems.neckPDFItems[112].ExportValue, true); //112
                        pdfFormFields.SetField(PDFItems.neckPDFItems[114].Code, PDFItems.neckPDFItems[114].ExportValue, true); //114

                        bool isAllSame = true;
                        //if (m.S109 != m.S96)
                        //{
                        //    isAllSame = false;
                        //}
                        //if (m.S108 != m.S86)
                        //{
                        //    isAllSame = false;
                        //}
                        //if (m.S110 != m.S87)
                        //{
                        //    isAllSame = false;
                        //}
                        //if (m.S111 != m.S90)
                        //{
                        //    isAllSame = false;
                        //}
                        //if (m.S117 != m.S78)
                        //{
                        //    isAllSame = false;
                        //}
                        //if (m.S118 != m.S82)
                        //{
                        //    isAllSame = false;
                        //}

                        //pdfFormFields.SetField("form1[0].#subform[2].Right_Perform[0]", "1", true); // 112

                        //if (isAllSame)
                        //{
                        //    pdfFormFields.SetField("form1[0].#subform[2].Right_Limitation[0]", "2", true);//114
                        //}
                        //else
                        //{
                        //    // insert values
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM7[0]", m.S109, true);
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM8[0]", m.S108, true);
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM9[0]", m.S110, true);
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM10[0]", m.S111, true);
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM11[0]", m.S117, true);
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM12[0]", m.S118, true);
                        //    pdfFormFields.SetField("form1[0].#subform[2].Right_Limitation[1]", "1", true); //115

                        //}

                        pdfFormFields.SetField(PDFItems.neckPDFItems[122].Code, PDFItems.neckPDFItems[122].ExportValue, true); //122
                        pdfFormFields.SetField(PDFItems.neckPDFItems[124].Code, PDFItems.neckPDFItems[124].ExportValue, true); //124
                        pdfFormFields.SetField(PDFItems.neckPDFItems[131].Code, PDFItems.neckPDFItems[131].ExportValue, true); //131
                        pdfFormFields.SetField(PDFItems.neckPDFItems[129].Code, PDFItems.neckPDFItems[129].ExportValue, true); //129

                        if (m.S135Tenderness)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[132].Code, PDFItems.neckPDFItems[132].ExportValue, true); //132
                            //pdfFormFields.SetField(PDFItems.neckPDFItems[134].Code, "Mild TTP Lumbar Paraspinal.", true); //134
                            pdfFormFields.SetField(PDFItems.neckPDFItems[134].Code, "Mild TTP C4-7", true);

                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[133].Code, PDFItems.neckPDFItems[133].ExportValue, true); //133
                        }

                        if (!m.S145MuscleSpasmQuestion)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[148].Code, PDFItems.neckPDFItems[148].ExportValue, true); //148
                            pdfFormFields.SetField(PDFItems.neckPDFItems[142].Code, PDFItems.neckPDFItems[142].ExportValue, true); //142
                        }
                        else
                        {
                            if ((m.S145MuscleSpasm) || (m.S145Guarding))
                            {
                                pdfFormFields.SetField(PDFItems.neckPDFItems[149].Code, PDFItems.neckPDFItems[149].ExportValue, true); //149
                                pdfFormFields.SetField(PDFItems.neckPDFItems[143].Code, PDFItems.neckPDFItems[143].ExportValue, true); //143

                                if (m.S145MuscleSpasm)
                                {
                                    pdfFormFields.SetField(PDFItems.neckPDFItems[144].Code, PDFItems.neckPDFItems[144].ExportValue, true); //144                                
                                }
                                if (m.S145Guarding)
                                {
                                    pdfFormFields.SetField(PDFItems.neckPDFItems[145].Code, PDFItems.neckPDFItems[145].ExportValue, true); //145
                                }
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.neckPDFItems[148].Code, PDFItems.neckPDFItems[148].ExportValue, true); //148
                            }

                        }


                        pdfFormFields.SetField(PDFItems.neckPDFItems[141].Code, PDFItems.neckPDFItems[141].ExportValue, true); //141

                        if (m.S159)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[156].Code, PDFItems.neckPDFItems[156].ExportValue, true); //156
                        }
                        if (m.S164)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[161].Code, PDFItems.neckPDFItems[161].ExportValue, true); //161
                        }
                        if (m.S158)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[155].Code, PDFItems.neckPDFItems[155].ExportValue, true); //155
                        }
                        if (m.S157)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[154].Code, PDFItems.neckPDFItems[154].ExportValue, true); //154
                        }
                        if (m.S171)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[168].Code, PDFItems.neckPDFItems[168].ExportValue, true); //168
                        }
                        if (m.S170)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[167].Code, PDFItems.neckPDFItems[167].ExportValue, true); //167
                        }
                        if (m.S169)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[166].Code, PDFItems.neckPDFItems[166].ExportValue, true); //166
                        }
                        if (m.S168)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[165].Code, PDFItems.neckPDFItems[165].ExportValue, true); //165
                        }
                        if (m.S167)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[164].Code, PDFItems.neckPDFItems[164].ExportValue, true); //164
                        }
                        if (m.S160)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[157].Code, PDFItems.neckPDFItems[157].ExportValue, true); //157
                        }
                        if (m.S163)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[160].Code, PDFItems.neckPDFItems[160].ExportValue, true); //160
                        }
                        if (m.S161)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[158].Code, PDFItems.neckPDFItems[158].ExportValue, true);//158
                        }
                        if (m.S162)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[159].Code, PDFItems.neckPDFItems[159].ExportValue, true); //159
                        }

                        if (m.S184 != m.S109)
                        {
                            isAllSame = false;
                        }

                        if (m.S185 != m.S108)
                        {
                            isAllSame = false;
                        }

                        if (m.S174 != m.S110)
                        {
                            isAllSame = false;
                        }

                        if (m.S180 != m.S111)
                        {
                            isAllSame = false;
                        }

                        if (m.S175 != m.S117)
                        {
                            isAllSame = false;
                        }

                        if (m.S177 != m.S118)
                        {
                            isAllSame = false;
                        }

                        if (!isAllSame)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[181].Code, m.S184, true); //181
                            pdfFormFields.SetField(PDFItems.neckPDFItems[182].Code, m.S185, true); //182
                            pdfFormFields.SetField(PDFItems.neckPDFItems[171].Code, m.S174, true); //171
                            pdfFormFields.SetField(PDFItems.neckPDFItems[177].Code, m.S180, true); //177
                            pdfFormFields.SetField(PDFItems.neckPDFItems[172].Code, m.S175, true); //172
                            pdfFormFields.SetField(PDFItems.neckPDFItems[174].Code, m.S177, true); //174

                            pdfFormFields.SetField(PDFItems.neckPDFItems[170].Code, PDFItems.neckPDFItems[170].ExportValue, true); //170
                            pdfFormFields.SetField(PDFItems.neckPDFItems[183].Code, PDFItems.neckPDFItems[183].ExportValue, true); //183
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[169].Code, PDFItems.neckPDFItems[169].ExportValue, true); //169
                            pdfFormFields.SetField(PDFItems.neckPDFItems[184].Code, PDFItems.neckPDFItems[184].ExportValue, true); //184
                        }

                        //if (m.S316)
                        //{
                        //    pdfFormFields.SetField(PDFItems.neckPDFItems[305].Code, PDFItems.neckPDFItems[305].ExportValue, true); //305

                        //    SetField_SRadiculopathyConstantPainLevelAnswer(m, pdfFormFields);
                        //    SetField_SRadiculopathyIntermittentPainLevelAnswer(m, pdfFormFields);
                        //    SetField_SRadiculopathyDullPainLevelAnswer(m, pdfFormFields);
                        //    SetField_SRadiculopathyTinglingPainLevelAnswer(m, pdfFormFields);
                        //    SetField_SRadiculopathyNumbnessPainLevelAnswer(m, pdfFormFields);
                        //    SetField_SRadiculopathySeverityLevel(m, pdfFormFields);

                        //}
                        //else
                        //{
                        //    pdfFormFields.SetField(PDFItems.neckPDFItems[317].Code, PDFItems.neckPDFItems[317].ExportValue, true); //317
                        //}

                        if (m.S51)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[330].Code, PDFItems.neckPDFItems[330].ExportValue, true); //330
                            pdfFormFields.SetField(PDFItems.neckPDFItems[378].Code, PDFItems.neckPDFItems[378].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.neckPDFItems[380].Code, PDFItems.neckPDFItems[380].ExportValue, true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[377].Code, PDFItems.neckPDFItems[377].ExportValue, true); //377
                        }

                        if ((!string.IsNullOrEmpty(m.S14C)) && (m.S51))
                        {
                            SetField_S14C(m, pdfFormFields);
                        }

                        if (m.S414)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[401].Code, PDFItems.neckPDFItems[401].ExportValue, true); //401
                            SetField_S17ABrace(m, pdfFormFields);
                            SetField_S17ACrutches(m, pdfFormFields);
                            SetField_S17ACane(m, pdfFormFields);
                            SetField_S17AWalker(m, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[402].Code, PDFItems.neckPDFItems[402].ExportValue, true); //402
                        }

                        // corrections
                        pdfFormFields.SetField(PDFItems.neckPDFItems[107].Code, PDFItems.neckPDFItems[107].ExportValue, true);

                        switch (m.S2BChoice)
                        {
                            case "RIGHT":
                                pdfFormFields.SetField(PDFItems.neckPDFItems[103].Code, PDFItems.neckPDFItems[103].ExportValue, true);
                                break;
                            case "LEFT":
                                pdfFormFields.SetField(PDFItems.neckPDFItems[102].Code, PDFItems.neckPDFItems[102].ExportValue, true);
                                break;
                            default:
                                break;
                        }

                        pdfFormFields.SetField(PDFItems.neckPDFItems[359].Code, PDFItems.neckPDFItems[359].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.neckPDFItems[444].Code, "Sedentary occupations recommended due to flareups and restricted ROM.", true);

                        if (!string.IsNullOrEmpty(m.S12AChoice))
                        {
                            if (m.S12AChoice == "NONE")
                            {
                                pdfFormFields.SetField(PDFItems.neckPDFItems[317].Code, PDFItems.neckPDFItems[317].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[332].Code, PDFItems.neckPDFItems[332].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[335].Code, PDFItems.neckPDFItems[335].ExportValue, true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[189].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[190].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[191].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[192].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[193].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[194].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[195].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[196].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[197].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[198].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[218].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[219].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[220].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[221].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[222].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[223].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[224].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[225].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[226].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[227].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[231].Code, PDFItems.neckPDFItems[231].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[202].Code, PDFItems.neckPDFItems[202].ExportValue, true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[299].Code, PDFItems.neckPDFItems[299].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[265].Code, PDFItems.neckPDFItems[265].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[277].Code, PDFItems.neckPDFItems[277].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[269].Code, PDFItems.neckPDFItems[269].ExportValue, true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[276].Code, PDFItems.neckPDFItems[276].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[270].Code, PDFItems.neckPDFItems[270].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[304].Code, PDFItems.neckPDFItems[304].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[301].Code, PDFItems.neckPDFItems[301].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[300].Code, PDFItems.neckPDFItems[300].ExportValue, true);

                            }
                            else if (m.S12AChoice == "MILD")
                            {
                                // Common if Raduciculopath
                                pdfFormFields.SetField(PDFItems.neckPDFItems[305].Code, PDFItems.neckPDFItems[305].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[361].Code, PDFItems.neckPDFItems[361].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[364].Code, PDFItems.neckPDFItems[364].ExportValue, true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[331].Code, PDFItems.neckPDFItems[331].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[336].Code, PDFItems.neckPDFItems[336].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[189].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[190].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[191].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[192].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[193].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[194].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[195].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[196].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[197].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[198].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[218].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[219].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[220].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[221].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[222].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[223].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[224].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[225].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[226].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[227].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[201].Code, PDFItems.neckPDFItems[201].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[230].Code, PDFItems.neckPDFItems[230].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[233].Code, PDFItems.neckPDFItems[233].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[204].Code, PDFItems.neckPDFItems[204].ExportValue, true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[323].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[327].Code, "2", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[299].Code, PDFItems.neckPDFItems[299].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[265].Code, PDFItems.neckPDFItems[265].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[277].Code, PDFItems.neckPDFItems[277].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[269].Code, PDFItems.neckPDFItems[269].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[274].Code, PDFItems.neckPDFItems[274].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[272].Code, PDFItems.neckPDFItems[272].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[296].Code, PDFItems.neckPDFItems[296].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[282].Code, PDFItems.neckPDFItems[282].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[295].Code, PDFItems.neckPDFItems[295].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[283].Code, PDFItems.neckPDFItems[283].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[290].Code, PDFItems.neckPDFItems[290].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[288].Code, PDFItems.neckPDFItems[288].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[263].Code, PDFItems.neckPDFItems[263].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[261].Code, PDFItems.neckPDFItems[261].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[254].Code, PDFItems.neckPDFItems[254].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[240].Code, PDFItems.neckPDFItems[240].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[242].Code, PDFItems.neckPDFItems[242].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[249].Code, PDFItems.neckPDFItems[249].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[348].Code, PDFItems.neckPDFItems[348].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[350].Code, PDFItems.neckPDFItems[350].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[357].Code, PDFItems.neckPDFItems[357].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[347].Code, PDFItems.neckPDFItems[347].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[345].Code, PDFItems.neckPDFItems[345].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[338].Code, PDFItems.neckPDFItems[338].ExportValue, true);

                            }
                            else if (m.S12AChoice == "MODERATE")
                            {
                                // Common if Raduciculopath
                                pdfFormFields.SetField(PDFItems.neckPDFItems[305].Code, PDFItems.neckPDFItems[305].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[361].Code, PDFItems.neckPDFItems[361].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[364].Code, PDFItems.neckPDFItems[364].ExportValue, true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[330].Code, PDFItems.neckPDFItems[330].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[337].Code, PDFItems.neckPDFItems[337].ExportValue, true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[191].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[192].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[193].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[196].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[197].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[198].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[220].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[221].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[222].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[225].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[226].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[227].Code, "5", true);


                                pdfFormFields.SetField(PDFItems.neckPDFItems[189].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[190].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[218].Code, "5", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[219].Code, "5", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[194].Code, "4", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[195].Code, "4", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[223].Code, "4", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[224].Code, "4", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[327].Code, "2", true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[323].Code, "2", true);

                                pdfFormFields.SetField(PDFItems.neckPDFItems[230].Code, PDFItems.neckPDFItems[230].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[233].Code, PDFItems.neckPDFItems[233].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[201].Code, PDFItems.neckPDFItems[201].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[204].Code, PDFItems.neckPDFItems[204].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[299].Code, PDFItems.neckPDFItems[299].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[265].Code, PDFItems.neckPDFItems[265].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[279].Code, PDFItems.neckPDFItems[279].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[267].Code, PDFItems.neckPDFItems[267].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[275].Code, PDFItems.neckPDFItems[275].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[271].Code, PDFItems.neckPDFItems[271].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[296].Code, PDFItems.neckPDFItems[296].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[282].Code, PDFItems.neckPDFItems[282].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[295].Code, PDFItems.neckPDFItems[295].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[283].Code, PDFItems.neckPDFItems[283].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[290].Code, PDFItems.neckPDFItems[290].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[288].Code, PDFItems.neckPDFItems[288].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[263].Code, PDFItems.neckPDFItems[263].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[262].Code, PDFItems.neckPDFItems[262].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[253].Code, PDFItems.neckPDFItems[253].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[240].Code, PDFItems.neckPDFItems[240].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[241].Code, PDFItems.neckPDFItems[241].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[250].Code, PDFItems.neckPDFItems[250].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[348].Code, PDFItems.neckPDFItems[348].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[349].Code, PDFItems.neckPDFItems[349].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[358].Code, PDFItems.neckPDFItems[358].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[347].Code, PDFItems.neckPDFItems[347].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[346].Code, PDFItems.neckPDFItems[346].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.neckPDFItems[329].Code, PDFItems.neckPDFItems[329].ExportValue, true);

                            }
                        }

                        pdfFormFields.SetField(PDFItems.neckPDFItems[65].Code, m.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.neckPDFItems[66].Code, m.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.neckPDFItems[67].Code, m.VarianceFunctionLossWriteIn, true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.neckPDFItems[115].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }
                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }
        //private void SetField_Neck_2B(NeckModel model, AcroFields pdfFormFields)
        //{
        //    if (!string.IsNullOrEmpty(model.SDominantHand))
        //    {
        //        switch (model.SDominantHand)
        //        {
        //            case "RIGHT":
        //                pdfFormFields.SetField("form1[0].#subform[1].DominantHand[2]", "2", true);
        //                break;
        //            case "LEFT":
        //                pdfFormFields.SetField("form1[0].#subform[1].DominantHand[1]", "1", true);
        //                break;
        //            case "AMBIDEXTROUS":
        //                pdfFormFields.SetField("form1[0].#subform[1].DominantHand[0]", "3", true);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        /// <summary>
        /// Constant pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyConstantPainLevelAnswer(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.SRadiculopathyConstantPainLevelAnswer)
            {
                pdfFormFields.SetField(PDFItems.neckPDFItems[306].Code, PDFItems.neckPDFItems[306].ExportValue, true); //306
                switch (m.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyConstantPainLevelAnswer_Left(m, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyConstantPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyConstantPainLevelAnswer_Left(m, pdfFormFields);
                        SetField_SRadiculopathyConstantPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_SRadiculopathyConstantPainLevelAnswer_Left(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyConstantPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[315].Code, PDFItems.neckPDFItems[315].ExportValue, true); //315
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[316].Code, PDFItems.neckPDFItems[316].ExportValue, true); //316
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[311].Code, PDFItems.neckPDFItems[311].ExportValue, true); //311
                    break;
            }
        }

        private void SetField_SRadiculopathyConstantPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyConstantPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[308].Code, PDFItems.neckPDFItems[308].ExportValue, true); //308
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[307].Code, PDFItems.neckPDFItems[307].ExportValue, true); //307
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[310].Code, PDFItems.neckPDFItems[310].ExportValue, true); //310
                    break;
            }
        }

        /// <summary>
        /// Intermittent pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyIntermittentPainLevelAnswer(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.SRadiculopathyIntermittentPainLevelAnswer)
            {
                pdfFormFields.SetField(PDFItems.neckPDFItems[263].Code, PDFItems.neckPDFItems[263].ExportValue, true); //263
                switch (m.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Left(m, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Left(m, pdfFormFields);
                        SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyIntermittentPainLevelAnswer_Left(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyIntermittentPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[254].Code, PDFItems.neckPDFItems[254].ExportValue, true); //254
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[253].Code, PDFItems.neckPDFItems[253].ExportValue, true); //253
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[258].Code, PDFItems.neckPDFItems[258].ExportValue, true); //258
                    break;
            }
        }

        private void SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyIntermittentPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[261].Code, PDFItems.neckPDFItems[261].ExportValue, true); //261
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[262].Code, PDFItems.neckPDFItems[262].ExportValue, true); //262
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[259].Code, PDFItems.neckPDFItems[259].ExportValue, true); //259
                    break;
            }
        }

        /// <summary>
        /// Dull pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyDullPainLevelAnswer(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.SRadiculopathyDullPainLevelAnswer)
            {
                pdfFormFields.SetField(PDFItems.neckPDFItems[240].Code, PDFItems.neckPDFItems[240].ExportValue, true); //240
                switch (m.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyDullPainLevelAnswer_Left(m, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyDullPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyDullPainLevelAnswer_Left(m, pdfFormFields);
                        SetField_SRadiculopathyDullPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyDullPainLevelAnswer_Left(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyDullPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[254].Code, PDFItems.neckPDFItems[254].ExportValue, true); //254
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[255].Code, PDFItems.neckPDFItems[255].ExportValue, true); //255
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[250].Code, PDFItems.neckPDFItems[250].ExportValue, true); //250
                    break;
            }
        }

        private void SetField_SRadiculopathyDullPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyDullPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[247].Code, PDFItems.neckPDFItems[247].ExportValue, true); //247
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[246].Code, PDFItems.neckPDFItems[246].ExportValue, true); //246
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[249].Code, PDFItems.neckPDFItems[249].ExportValue, true); //249
                    break;
            }
        }

        /// <summary>
        /// Tingling pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyTinglingPainLevelAnswer(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.SRadiculopathyTinglingPainLevelAnswer)
            {
                pdfFormFields.SetField(PDFItems.neckPDFItems[348].Code, PDFItems.neckPDFItems[348].ExportValue, true); //348
                switch (m.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Left(m, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Left(m, pdfFormFields);
                        SetField_SRadiculopathyTinglingPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyTinglingPainLevelAnswer_Left(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyTinglingPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[357].Code, PDFItems.neckPDFItems[357].ExportValue, true); //357
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[358].Code, PDFItems.neckPDFItems[358].ExportValue, true); //358
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[353].Code, PDFItems.neckPDFItems[353].ExportValue, true); //353
                    break;
            }
        }

        private void SetField_SRadiculopathyTinglingPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyTinglingPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[350].Code, PDFItems.neckPDFItems[350].ExportValue, true); //350
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[349].Code, PDFItems.neckPDFItems[349].ExportValue, true); //349
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[352].Code, PDFItems.neckPDFItems[352].ExportValue, true); //352
                    break;
            }
        }


        /// <summary>
        /// Numbness pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyNumbnessPainLevelAnswer(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.SRadiculopathyNumbnessPainLevelAnswer)
            {
                pdfFormFields.SetField(PDFItems.neckPDFItems[347].Code, PDFItems.neckPDFItems[347].ExportValue, true); //347
                switch (m.SRadiculopathyLegSide)
                {
                    case "LEFT":
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Left(m, pdfFormFields);
                        break;
                    case "RIGHT":
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    case "BOTH":
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Left(m, pdfFormFields);
                        SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(m, pdfFormFields);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetField_SRadiculopathyNumbnessPainLevelAnswer_Left(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyNumbnessPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[338].Code, PDFItems.neckPDFItems[338].ExportValue, true); //338
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[329].Code, PDFItems.neckPDFItems[329].ExportValue, true); //329
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[342].Code, PDFItems.neckPDFItems[342].ExportValue, true); //342
                    break;
            }
        }

        private void SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyNumbnessPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[345].Code, PDFItems.neckPDFItems[345].ExportValue, true); //345
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[346].Code, PDFItems.neckPDFItems[346].ExportValue, true); //346
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[343].Code, PDFItems.neckPDFItems[343].ExportValue, true); //343
                    break;
            }
        }

        /// <summary>
        /// Numbness pain SRadiculopathySeverityLevel
        /// </summary>
        private void SetField_SRadiculopathySeverityLevel(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyLegSide)
            {
                case "LEFT":
                    SetField_SRadiculopathySeverityLevel_Left(m, pdfFormFields);
                    break;
                case "RIGHT":
                    SetField_SRadiculopathySeverityLevel_Right(m, pdfFormFields);
                    break;
                case "BOTH":
                    SetField_SRadiculopathySeverityLevel_Left(m, pdfFormFields);
                    SetField_SRadiculopathySeverityLevel_Right(m, pdfFormFields);
                    break;
                default:
                    break;
            }
        }
        private void SetField_SRadiculopathySeverityLevel_Left(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathySeverityLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[336].Code, PDFItems.neckPDFItems[336].ExportValue, true); //336
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[337].Code, PDFItems.neckPDFItems[337].ExportValue, true); //337
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[334].Code, PDFItems.neckPDFItems[334].ExportValue, true); //334
                    break;
            }
        }

        private void SetField_SRadiculopathySeverityLevel_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathySeverityLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[331].Code, PDFItems.neckPDFItems[331].ExportValue, true); //331
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[330].Code, PDFItems.neckPDFItems[330].ExportValue, true); //330
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[333].Code, PDFItems.neckPDFItems[333].ExportValue, true); //333
                    break;
            }
        }

        private void SetField_S14C(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.S14C)
            {
                case "ONEWEEK":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[379].Code, PDFItems.neckPDFItems[379].ExportValue, true); //379
                    break;
                case "TWOWEEKS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[383].Code, PDFItems.neckPDFItems[383].ExportValue, true); //383
                    break;
                case "FOURWEEKS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[380].Code, PDFItems.neckPDFItems[380].ExportValue, true); //380
                    break;
                case "SIXWEEKS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[381].Code, PDFItems.neckPDFItems[381].ExportValue, true); //381
                    break;
                case "SIXWEEKSPLUS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[382].Code, PDFItems.neckPDFItems[382].ExportValue, true); //382
                    break;
            }
        }

        private void SetField_S17ABrace(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.S416)
            {
                switch (m.S416Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[421].Code, PDFItems.neckPDFItems[421].ExportValue, true); //421
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[425].Code, PDFItems.neckPDFItems[425].ExportValue, true); //425
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[426].Code, PDFItems.neckPDFItems[426].ExportValue, true); //426
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17ACrutches(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.S428)
            {
                switch (m.S428Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[414].Code, PDFItems.neckPDFItems[414].ExportValue, true); //414
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[413].Code, PDFItems.neckPDFItems[413].ExportValue, true); //413
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[412].Code, PDFItems.neckPDFItems[412].ExportValue, true); //412
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17ACane(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.S417)
            {
                switch (m.S417Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[409].Code, PDFItems.neckPDFItems[409].ExportValue, true); //409
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[410].Code, PDFItems.neckPDFItems[410].ExportValue, true); //410
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[411].Code, PDFItems.neckPDFItems[411].ExportValue, true); //411
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17AWalker(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.S421)
            {
                switch (m.S421Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[407].Code, PDFItems.neckPDFItems[407].ExportValue, true); //407
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[406].Code, PDFItems.neckPDFItems[406].ExportValue, true); //406
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[405].Code, PDFItems.neckPDFItems[405].ExportValue, true); //405
                        break;
                    default:
                        break;
                }
            }
        }
        /**************************************************************
          * Shoulder Form
          * 
          *************************************************************/
        public byte[] Shoulder(string template, ShoulderModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // defaults
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[1].Code, PDFItems.shoulderPDFItems[1].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[9].Code, PDFItems.shoulderPDFItems[9].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[10].Code, PDFItems.shoulderPDFItems[10].ExportValue, true);
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe2A[0]", "On-set of injury incurred during active duty service.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe2B[0]", "Physical limitations, loss of strength, soreness, and pain.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe2C[0]", "Severe restriction of range of motion.", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Painful[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Painful[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Pain1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Pain1[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Tender[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Tender[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Loss[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength3[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength3[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis4[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis4[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].YesNoRC[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].YesNoRC_L[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R1[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L1[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R2[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L2[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R3[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L3[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R4[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L4[2]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].No242[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].No24[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].No24[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].No20[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].One60[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].No12A[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].No12B[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo14A[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo16A[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo20[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo17B[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo17C[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo18[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[8].Comments18[0]", "Difficultly with overhead task.", true);

                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[419].Code, PDFItems.shoulderPDFItems[419].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[512].Code, PDFItems.shoulderPDFItems[512].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[522].Code, PDFItems.shoulderPDFItems[522].ExportValue, true);

                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[15].Code, m.NameOfPatient, true);

                        // Corrections
                        // -	Box138, 199, 191, 162, 226, 205, 218, 310, 319, 331, 386, 388, 355, 371, 359, 375, 363, 379, 367, 383, 510, 512, 514, 520, 550, 555
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[199].Code, PDFItems.shoulderPDFItems[199].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[191].Code, PDFItems.shoulderPDFItems[191].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[162].Code, PDFItems.shoulderPDFItems[162].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[226].Code, PDFItems.shoulderPDFItems[226].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[205].Code, PDFItems.shoulderPDFItems[205].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[218].Code, PDFItems.shoulderPDFItems[218].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[310].Code, PDFItems.shoulderPDFItems[310].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[319].Code, PDFItems.shoulderPDFItems[319].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[331].Code, PDFItems.shoulderPDFItems[331].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[386].Code, PDFItems.shoulderPDFItems[386].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[388].Code, PDFItems.shoulderPDFItems[388].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[355].Code, PDFItems.shoulderPDFItems[355].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[371].Code, PDFItems.shoulderPDFItems[371].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[359].Code, PDFItems.shoulderPDFItems[359].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[375].Code, PDFItems.shoulderPDFItems[375].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[363].Code, PDFItems.shoulderPDFItems[363].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[379].Code, PDFItems.shoulderPDFItems[379].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[367].Code, PDFItems.shoulderPDFItems[367].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[383].Code, PDFItems.shoulderPDFItems[383].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[510].Code, PDFItems.shoulderPDFItems[510].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[512].Code, PDFItems.shoulderPDFItems[512].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[514].Code, PDFItems.shoulderPDFItems[514].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[520].Code, PDFItems.shoulderPDFItems[520].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[550].Code, PDFItems.shoulderPDFItems[550].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[555].Code, PDFItems.shoulderPDFItems[555].ExportValue, true);


                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[14].Code, ssn.ToString(), true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[14].Code, m.SocialSecurity, true);
                        }

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;
                        //string S17 = string.Empty;
                        if (!m.S63)
                        {
                            if (m.S64)
                            {
                                diagnosis = "Shoulder Strain";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[64].Code, PDFItems.shoulderPDFItems[64].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[61].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[62].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S64Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[75].Code, PDFItems.shoulderPDFItems[75].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[74].Code, PDFItems.shoulderPDFItems[74].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[73].Code, PDFItems.shoulderPDFItems[73].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S65)
                            {
                                diagnosis = "Shoulder Inpingement Syndrome";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[65].Code, PDFItems.shoulderPDFItems[65].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[60].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[59].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S65Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[21].Code, PDFItems.shoulderPDFItems[21].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[57].Code, PDFItems.shoulderPDFItems[57].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[58].Code, PDFItems.shoulderPDFItems[58].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S66)
                            {
                                diagnosis = "Bicipital Tendonitis";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[66].Code, PDFItems.shoulderPDFItems[66].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[52].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[53].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S66Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[56].Code, PDFItems.shoulderPDFItems[56].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[55].Code, PDFItems.shoulderPDFItems[55].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[54].Code, PDFItems.shoulderPDFItems[54].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S67)
                            {
                                diagnosis = "Bicipital Tendon Tear";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[67].Code, PDFItems.shoulderPDFItems[67].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[51].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[50].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S67Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[22].Code, PDFItems.shoulderPDFItems[22].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[48].Code, PDFItems.shoulderPDFItems[48].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[49].Code, PDFItems.shoulderPDFItems[49].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S68)
                            {
                                diagnosis = "Rotator Cuff Tendonitis";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[68].Code, PDFItems.shoulderPDFItems[68].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[43].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[44].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S68Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[47].Code, PDFItems.shoulderPDFItems[47].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[46].Code, PDFItems.shoulderPDFItems[46].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[45].Code, PDFItems.shoulderPDFItems[45].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S69)
                            {
                                diagnosis = "Rotator Cuff Tear";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[69].Code, PDFItems.shoulderPDFItems[69].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[42].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[41].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S69Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[23].Code, PDFItems.shoulderPDFItems[23].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[39].Code, PDFItems.shoulderPDFItems[39].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[40].Code, PDFItems.shoulderPDFItems[40].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S70)
                            {
                                diagnosis = "Labral Tear Including SLAP";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[70].Code, PDFItems.shoulderPDFItems[70].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[34].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[35].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S70Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[38].Code, PDFItems.shoulderPDFItems[38].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[37].Code, PDFItems.shoulderPDFItems[37].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[36].Code, PDFItems.shoulderPDFItems[36].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S71)
                            {
                                diagnosis = "Subacromial/Subdeloid Bursitis";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[71].Code, PDFItems.shoulderPDFItems[71].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[33].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[32].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S71Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[24].Code, PDFItems.shoulderPDFItems[24].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[30].Code, PDFItems.shoulderPDFItems[30].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[31].Code, PDFItems.shoulderPDFItems[31].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S72)
                            {
                                diagnosis = "Glenohumeral Joint Ostearthritis";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[72].Code, PDFItems.shoulderPDFItems[72].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[25].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[26].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S72Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[29].Code, PDFItems.shoulderPDFItems[29].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[28].Code, PDFItems.shoulderPDFItems[28].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[27].Code, PDFItems.shoulderPDFItems[27].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S81)
                            {
                                diagnosis = "Acromioclavicular Joint Osteoarthritis";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[81].Code, PDFItems.shoulderPDFItems[81].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[76].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[77].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S81Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[80].Code, PDFItems.shoulderPDFItems[80].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[79].Code, PDFItems.shoulderPDFItems[79].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[78].Code, PDFItems.shoulderPDFItems[78].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S111)
                            {
                                diagnosis = "Ankylosis Of Glenohumeral Articulations";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[111].Code, PDFItems.shoulderPDFItems[111].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[106].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[107].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S111Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[110].Code, PDFItems.shoulderPDFItems[110].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[109].Code, PDFItems.shoulderPDFItems[109].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[108].Code, PDFItems.shoulderPDFItems[108].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S105)
                            {
                                diagnosis = "Glenohumeral Joint Instability";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[105].Code, PDFItems.shoulderPDFItems[105].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[100].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[101].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S105Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[104].Code, PDFItems.shoulderPDFItems[104].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[103].Code, PDFItems.shoulderPDFItems[103].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[102].Code, PDFItems.shoulderPDFItems[102].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S99)
                            {
                                diagnosis = "Glenohumeral Joint Dislocation";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[99].Code, PDFItems.shoulderPDFItems[99].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[94].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[95].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S99Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[98].Code, PDFItems.shoulderPDFItems[98].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[97].Code, PDFItems.shoulderPDFItems[97].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[96].Code, PDFItems.shoulderPDFItems[96].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S93)
                            {
                                diagnosis = "Shoulder Joint Replacement";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[93].Code, PDFItems.shoulderPDFItems[93].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[88].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[89].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S93Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[92].Code, PDFItems.shoulderPDFItems[92].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[91].Code, PDFItems.shoulderPDFItems[91].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[90].Code, PDFItems.shoulderPDFItems[90].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }
                            if (m.S87)
                            {
                                diagnosis = "Acromioclavicular Joint Separation";
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[87].Code, PDFItems.shoulderPDFItems[87].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[82].Code, dt, true);
                                if (ICDCodes.shoulderICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[83].Code, icdcode.RefNumber, true);
                                }
                                //S17 += diagnosis;
                                switch (m.S87Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[86].Code, PDFItems.shoulderPDFItems[86].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[85].Code, PDFItems.shoulderPDFItems[85].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[84].Code, PDFItems.shoulderPDFItems[84].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }
                                //S17 += ".  ";
                            }

                            if (!string.IsNullOrEmpty(m.S128Other))
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[128].Code, PDFItems.shoulderPDFItems[128].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[131].Code, m.S128Other, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[127].Code, dt, true);
                                //S17 += m.S128Other;
                                switch (m.S128Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[113].Code, PDFItems.shoulderPDFItems[113].ExportValue, true);
                                        //S17 += ", Right";
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[124].Code, PDFItems.shoulderPDFItems[124].ExportValue, true);
                                        //S17 += ", Left";
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[125].Code, PDFItems.shoulderPDFItems[125].ExportValue, true);
                                        //S17 += ", Bilateral";
                                        break;
                                    default:
                                        break;
                                }

                                //S17 += ".  ";
                            }

                            //pdfFormFields.SetField(PDFItems.shoulderPDFItems[17].Code, S17, true);
                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[17].Code, m.S17, true);

                            bool DoInitialROMLeft = false;
                            bool DoInitialROMRight = false;

                            if ((m.S64Side == "BOTH")
                                || (m.S65Side == "BOTH")
                                || (m.S66Side == "BOTH")
                                || (m.S67Side == "BOTH")
                                || (m.S68Side == "BOTH")
                                || (m.S69Side == "BOTH")
                                || (m.S70Side == "BOTH")
                                || (m.S71Side == "BOTH")
                                || (m.S72Side == "BOTH")
                                || (m.S81Side == "BOTH")
                                || (m.S111Side == "BOTH")
                                || (m.S105Side == "BOTH")
                                || (m.S99Side == "BOTH")
                                || (m.S93Side == "BOTH")
                                || (m.S87Side == "BOTH")
                                )
                            {
                                DoInitialROMLeft = true;
                                DoInitialROMRight = true;
                            }
                            if ((m.S64Side == "LEFT")
                                || (m.S65Side == "LEFT")
                                || (m.S66Side == "LEFT")
                                || (m.S67Side == "LEFT")
                                || (m.S68Side == "LEFT")
                                || (m.S69Side == "LEFT")
                                || (m.S70Side == "LEFT")
                                || (m.S71Side == "LEFT")
                                || (m.S72Side == "LEFT")
                                || (m.S81Side == "LEFT")
                                || (m.S111Side == "LEFT")
                                || (m.S105Side == "LEFT")
                                || (m.S99Side == "LEFT")
                                || (m.S93Side == "LEFT")
                                || (m.S87Side == "LEFT")
                                )
                            {
                                DoInitialROMLeft = true;
                            }
                            if ((m.S64Side == "RIGHT")
                                || (m.S65Side == "RIGHT")
                                || (m.S66Side == "RIGHT")
                                || (m.S67Side == "RIGHT")
                                || (m.S68Side == "RIGHT")
                                || (m.S69Side == "RIGHT")
                                || (m.S70Side == "RIGHT")
                                || (m.S71Side == "RIGHT")
                                || (m.S72Side == "RIGHT")
                                || (m.S81Side == "RIGHT")
                                || (m.S111Side == "RIGHT")
                                || (m.S105Side == "RIGHT")
                                || (m.S99Side == "RIGHT")
                                || (m.S93Side == "RIGHT")
                                || (m.S87Side == "RIGHT")
                                )
                            {
                                DoInitialROMRight = true;
                            }

                            bool SameInitialROMLeft = false;
                            bool SameInitialROMRight = false;
                            bool SameFinalROMLeft = false;
                            bool SameFinalROMRight = false;


                            if ((DoInitialROMLeft) && (DoInitialROMRight))
                            {
                                // set the inital RIGHT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[156].Code, m.S156, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[152].Code, m.S152, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[144].Code, m.S144, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[148].Code, m.S148, true);

                                if ((m.S156 == m.S180)
                                    && (m.S152 == m.S181)
                                    && (m.S144 == m.S187)
                                    && (m.S148 == m.S188)
                                    )
                                {
                                    SameInitialROMRight = true;
                                }

                                // Set the initial LEFT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[177].Code, m.S177, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[173].Code, m.S173, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[165].Code, m.S165, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[169].Code, m.S169, true);

                                if ((m.S177 == m.S200)
                                    && (m.S173 == m.S194)
                                    && (m.S165 == m.S192)
                                    && (m.S169 == m.S193)
                                    )
                                {
                                    SameInitialROMLeft = true;
                                }


                                // Second test

                                // Left defaults
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[180].Code, m.S180, true);
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[181].Code, m.S181, true);
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[187].Code, m.S187, true);
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[188].Code, m.S188, true);

                                // Right defaults
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[200].Code, m.S200, true);
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[194].Code, m.S194, true);
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[192].Code, m.S192, true);
                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[193].Code, m.S193, true);

                                // default checkboxes
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[182].Code, PDFItems.shoulderPDFItems[182].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[199].Code, PDFItems.shoulderPDFItems[199].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[184].Code, PDFItems.shoulderPDFItems[184].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[197].Code, PDFItems.shoulderPDFItems[197].ExportValue, true);

                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[182].Code, PDFItems.shoulderPDFItems[182].ExportValue, true);
                                //if (SameInitialROMRight)
                                //{
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[184].Code, PDFItems.shoulderPDFItems[184].ExportValue, true);
                                //}
                                //else
                                //{
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[185].Code, PDFItems.shoulderPDFItems[185].ExportValue, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[180].Code, m.S180, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[181].Code, m.S181, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[187].Code, m.S187, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[188].Code, m.S188, true);
                                //}
                                //if ((m.S180 == m.S293)
                                //    && (m.S181 == m.S286)
                                //    && (m.S187 == m.S287)
                                //    && (m.S188 == m.S289)
                                //    )
                                //{
                                //    SameFinalROMRight = true;
                                //}



                                //pdfFormFields.SetField(PDFItems.shoulderPDFItems[199].Code, PDFItems.shoulderPDFItems[199].ExportValue, true);
                                //if (SameInitialROMLeft)
                                //{
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[197].Code, PDFItems.shoulderPDFItems[197].ExportValue, true);
                                //}
                                //else
                                //{
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[196].Code, PDFItems.shoulderPDFItems[196].ExportValue, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[200].Code, m.S200, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[194].Code, m.S194, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[192].Code, m.S192, true);
                                //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[193].Code, m.S193, true);
                                //}
                                //if ((m.S200 == m.S304)
                                //    && (m.S194 == m.S297)
                                //    && (m.S192 == m.S301)
                                //    && (m.S193 == m.S300)
                                //    )
                                //{
                                //    SameFinalROMLeft = true;
                                //}


                                if (SameFinalROMRight)
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[295].Code, PDFItems.shoulderPDFItems[295].ExportValue, true);
                                }
                                else
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[294].Code, PDFItems.shoulderPDFItems[294].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[293].Code, m.S293, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[286].Code, m.S286, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[287].Code, m.S287, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[289].Code, m.S289, true);
                                }

                                if (SameFinalROMLeft)
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[305].Code, PDFItems.shoulderPDFItems[305].ExportValue, true);
                                }
                                else
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[306].Code, PDFItems.shoulderPDFItems[306].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[304].Code, m.S304, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[297].Code, m.S297, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[301].Code, m.S301, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[300].Code, m.S300, true);
                                }

                            }
                            else if (DoInitialROMRight)
                            {
                                // the default for initial LEFT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[177].Code, "180", true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[173].Code, "180", true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[165].Code, "90", true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[169].Code, "90", true);

                                // set the values for initial RIGHT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[156].Code, m.S156, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[152].Code, m.S152, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[144].Code, m.S144, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[148].Code, m.S148, true);

                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[197].Code, PDFItems.shoulderPDFItems[197].ExportValue, true);

                                if ((m.S156 == m.S180)
                                    && (m.S152 == m.S181)
                                    && (m.S144 == m.S187)
                                    && (m.S148 == m.S188)
                                    )
                                {
                                    SameInitialROMRight = true;
                                }

                                // Second test for setting RIGHT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[182].Code, PDFItems.shoulderPDFItems[182].ExportValue, true);
                                if (SameInitialROMRight)
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[184].Code, PDFItems.shoulderPDFItems[184].ExportValue, true);
                                }
                                else
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[185].Code, PDFItems.shoulderPDFItems[185].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[180].Code, m.S180, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[181].Code, m.S181, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[187].Code, m.S187, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[188].Code, m.S188, true);
                                }
                                if ((m.S180 == m.S293)
                                    && (m.S181 == m.S286)
                                    && (m.S187 == m.S287)
                                    && (m.S188 == m.S289)
                                    )
                                {
                                    SameFinalROMRight = true;
                                }


                                // final RIGHT
                                if (SameFinalROMRight)
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[295].Code, PDFItems.shoulderPDFItems[295].ExportValue, true);
                                }
                                else
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[294].Code, PDFItems.shoulderPDFItems[294].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[293].Code, m.S293, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[286].Code, m.S286, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[287].Code, m.S287, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[289].Code, m.S289, true);
                                }

                                // set the LEFT repeat to no change
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[199].Code, PDFItems.shoulderPDFItems[199].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[197].Code, PDFItems.shoulderPDFItems[197].ExportValue, true);

                                // set the LEFT Flare-up to no change
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[305].Code, PDFItems.shoulderPDFItems[305].ExportValue, true);

                            }
                            else if (DoInitialROMLeft)
                            {
                                // the default for initial RIGHT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[156].Code, "180", true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[152].Code, "180", true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[144].Code, "90", true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[148].Code, "90", true);

                                // set the initial LEFT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[177].Code, m.S177, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[173].Code, m.S173, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[165].Code, m.S165, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[169].Code, m.S169, true);

                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[295].Code, PDFItems.shoulderPDFItems[295].ExportValue, true);

                                if ((m.S177 == m.S200)
                                    && (m.S173 == m.S194)
                                    && (m.S165 == m.S192)
                                    && (m.S169 == m.S193)
                                    )
                                {
                                    SameInitialROMLeft = true;
                                }

                                // Second test for setting LEFT
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[199].Code, PDFItems.shoulderPDFItems[199].ExportValue, true);
                                if (SameInitialROMLeft)
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[197].Code, PDFItems.shoulderPDFItems[197].ExportValue, true);
                                }
                                else
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[196].Code, PDFItems.shoulderPDFItems[196].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[200].Code, m.S200, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[194].Code, m.S194, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[192].Code, m.S192, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[193].Code, m.S193, true);
                                }
                                if ((m.S200 == m.S304)
                                    && (m.S194 == m.S297)
                                    && (m.S192 == m.S301)
                                    && (m.S193 == m.S300)
                                    )
                                {
                                    SameFinalROMLeft = true;
                                }


                                // final LEFT
                                if (SameFinalROMLeft)
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[305].Code, PDFItems.shoulderPDFItems[305].ExportValue, true);
                                }
                                else
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[306].Code, PDFItems.shoulderPDFItems[306].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[304].Code, m.S304, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[297].Code, m.S297, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[301].Code, m.S301, true);
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[300].Code, m.S300, true);

                                }

                                // set the RIGHT repeat to no change
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[182].Code, PDFItems.shoulderPDFItems[182].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[184].Code, PDFItems.shoulderPDFItems[184].ExportValue, true);

                                // set the RIGHT Flare-up to no change
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[295].Code, PDFItems.shoulderPDFItems[295].ExportValue, true);

                            }

                            if ((!SameFinalROMLeft) || (!SameFinalROMRight))
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[285].Code, PDFItems.shoulderPDFItems[285].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[284].Code, PDFItems.shoulderPDFItems[284].ExportValue, true);
                            }

                            //if ((SameFinalROMLeft) || (SameFinalROMRight))
                            //{
                            //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[285].Code, PDFItems.shoulderPDFItems[285].ExportValue, true);
                            //}
                            //else
                            //{
                            //    pdfFormFields.SetField(PDFItems.shoulderPDFItems[284].Code, PDFItems.shoulderPDFItems[284].ExportValue, true);
                            //}




                            if (m.S231)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[231].Code, PDFItems.shoulderPDFItems[231].ExportValue, true);
                                switch (m.S231Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[273].Code, PDFItems.shoulderPDFItems[273].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[232].Code, PDFItems.shoulderPDFItems[232].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[274].Code, PDFItems.shoulderPDFItems[274].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S275)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[275].Code, PDFItems.shoulderPDFItems[275].ExportValue, true);
                                switch (m.S275Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[271].Code, PDFItems.shoulderPDFItems[271].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[272].Code, PDFItems.shoulderPDFItems[272].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[270].Code, PDFItems.shoulderPDFItems[270].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S229)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[229].Code, PDFItems.shoulderPDFItems[229].ExportValue, true);
                                switch (m.S229Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[268].Code, PDFItems.shoulderPDFItems[268].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[233].Code, PDFItems.shoulderPDFItems[233].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[269].Code, PDFItems.shoulderPDFItems[269].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S228)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[228].Code, PDFItems.shoulderPDFItems[228].ExportValue, true);
                                switch (m.S228Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[266].Code, PDFItems.shoulderPDFItems[266].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[267].Code, PDFItems.shoulderPDFItems[267].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[265].Code, PDFItems.shoulderPDFItems[265].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S282)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[282].Code, PDFItems.shoulderPDFItems[282].ExportValue, true);
                                switch (m.S282Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[263].Code, PDFItems.shoulderPDFItems[263].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[234].Code, PDFItems.shoulderPDFItems[234].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[264].Code, PDFItems.shoulderPDFItems[264].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S281)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[281].Code, PDFItems.shoulderPDFItems[281].ExportValue, true);
                                switch (m.S281Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[261].Code, PDFItems.shoulderPDFItems[261].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[262].Code, PDFItems.shoulderPDFItems[262].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[260].Code, PDFItems.shoulderPDFItems[260].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S280)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[280].Code, PDFItems.shoulderPDFItems[280].ExportValue, true);
                                switch (m.S280Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[258].Code, PDFItems.shoulderPDFItems[258].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[235].Code, PDFItems.shoulderPDFItems[235].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[259].Code, PDFItems.shoulderPDFItems[259].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S279)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[279].Code, PDFItems.shoulderPDFItems[279].ExportValue, true);
                                switch (m.S279Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[256].Code, PDFItems.shoulderPDFItems[256].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[257].Code, PDFItems.shoulderPDFItems[257].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[255].Code, PDFItems.shoulderPDFItems[255].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S278)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[278].Code, PDFItems.shoulderPDFItems[278].ExportValue, true);
                                switch (m.S278Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[253].Code, PDFItems.shoulderPDFItems[253].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[236].Code, PDFItems.shoulderPDFItems[236].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[254].Code, PDFItems.shoulderPDFItems[254].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S237)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[237].Code, PDFItems.shoulderPDFItems[237].ExportValue, true);
                                switch (m.S237Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[251].Code, PDFItems.shoulderPDFItems[251].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[252].Code, PDFItems.shoulderPDFItems[252].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[250].Code, PDFItems.shoulderPDFItems[250].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S249)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[249].Code, PDFItems.shoulderPDFItems[249].ExportValue, true);
                                switch (m.S249Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[247].Code, PDFItems.shoulderPDFItems[247].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[238].Code, PDFItems.shoulderPDFItems[238].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[248].Code, PDFItems.shoulderPDFItems[248].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S239)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[239].Code, PDFItems.shoulderPDFItems[239].ExportValue, true);
                                switch (m.S239Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[245].Code, PDFItems.shoulderPDFItems[245].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[246].Code, PDFItems.shoulderPDFItems[246].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[244].Code, PDFItems.shoulderPDFItems[244].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (m.S243)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[243].Code, PDFItems.shoulderPDFItems[243].ExportValue, true);
                                switch (m.S243Side)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[241].Code, PDFItems.shoulderPDFItems[241].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[240].Code, PDFItems.shoulderPDFItems[240].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[242].Code, PDFItems.shoulderPDFItems[242].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }
                            }

                            if (!m.S10A)
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[406].Code, PDFItems.shoulderPDFItems[406].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[408].Code, PDFItems.shoulderPDFItems[408].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[407].Code, PDFItems.shoulderPDFItems[407].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[409].Code, PDFItems.shoulderPDFItems[409].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[404].Code, PDFItems.shoulderPDFItems[404].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.shoulderPDFItems[412].Code, PDFItems.shoulderPDFItems[412].ExportValue, true);

                                switch (m.S10ASide)
                                {
                                    case "RIGHT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[395].Code, PDFItems.shoulderPDFItems[395].ExportValue, true);
                                        break;
                                    case "LEFT":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[396].Code, PDFItems.shoulderPDFItems[396].ExportValue, true);
                                        break;
                                    case "BOTH":
                                        pdfFormFields.SetField(PDFItems.shoulderPDFItems[397].Code, PDFItems.shoulderPDFItems[397].ExportValue, true);
                                        break;
                                    default:
                                        break;
                                }


                                if (m.S10C == "FREQUENT")
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[410].Code, PDFItems.shoulderPDFItems[410].ExportValue, true);
                                    switch (m.S10ASide)
                                    {
                                        case "RIGHT":
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[410].Code, PDFItems.shoulderPDFItems[410].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[395].Code, PDFItems.shoulderPDFItems[395].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[398].Code, PDFItems.shoulderPDFItems[398].ExportValue, true);
                                            break;
                                        case "LEFT":
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[410].Code, PDFItems.shoulderPDFItems[410].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[396].Code, PDFItems.shoulderPDFItems[396].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[399].Code, PDFItems.shoulderPDFItems[399].ExportValue, true);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (m.S10C == "INFREQUENT")
                                {
                                    pdfFormFields.SetField(PDFItems.shoulderPDFItems[411].Code, PDFItems.shoulderPDFItems[411].ExportValue, true);
                                    switch (m.S10ASide)
                                    {
                                        case "RIGHT":
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[392].Code, PDFItems.shoulderPDFItems[392].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[411].Code, PDFItems.shoulderPDFItems[411].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[398].Code, PDFItems.shoulderPDFItems[398].ExportValue, true);
                                            break;
                                        case "LEFT":
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[393].Code, PDFItems.shoulderPDFItems[393].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[411].Code, PDFItems.shoulderPDFItems[411].ExportValue, true);
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[399].Code, PDFItems.shoulderPDFItems[399].ExportValue, true);
                                            break;
                                        case "BOTH":
                                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[394].Code, PDFItems.shoulderPDFItems[394].ExportValue, true);
                                            break;
                                        default:
                                            break;
                                    }

                                }
                            }

                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[132].Code, m.VarianceHistoryWriteIn, true);
                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[133].Code, m.VarianceFlareUpsWriteIn, true);
                            pdfFormFields.SetField(PDFItems.shoulderPDFItems[134].Code, m.VarianceFunctionLossWriteIn, true);

                            if (m.IsFormReadonly)
                            {
                                IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.shoulderPDFItems[2].Code);
                                Rectangle rect = lstPos[0].position;
                                PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                cb.SetFontAndSize(bf, 12);
                                cb.BeginText();
                                cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                                cb.ShowText(string.Empty);
                                cb.EndText();
                            }
                        }
                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        /**************************************************************
          * Foot Form
          * 
          *************************************************************/
        public byte[] Foot(string template, FootModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // defaults
                        //pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].YesNo4[1]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe1[1]", "Physical limitations, loss of strength, soreness, and pain.", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "See section 13A", true);
                        //pdfFormFields.SetField("form1[0].#subform[3].No7A[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[3].No6B[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[3].Right_None[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[3].Left_None[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[5].No11A[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[7].YesNo19[0]", "2", true);
                        //pdfFormFields.SetField("form1[0].#subform[8].YesNo20[0]", "2", true);
                        //pdfFormFields.SetField("form1[0].#subform[8].YesNo24[0]", "2", true);

                        pdfFormFields.SetField(PDFItems.footPDFItems[48].Code, PDFItems.footPDFItems[48].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[57].Code, PDFItems.footPDFItems[57].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[56].Code, PDFItems.footPDFItems[56].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[83].Code, "Original onset of injury was during active duty service.", true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[90].Code, PDFItems.footPDFItems[90].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[93].Code, PDFItems.footPDFItems[93].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[87].Code, PDFItems.footPDFItems[87].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[94].Code, PDFItems.footPDFItems[94].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[185].Code, PDFItems.footPDFItems[185].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[181].Code, PDFItems.footPDFItems[181].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[180].Code, PDFItems.footPDFItems[180].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[175].Code, PDFItems.footPDFItems[175].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[231].Code, PDFItems.footPDFItems[231].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[235].Code, PDFItems.footPDFItems[235].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[252].Code, PDFItems.footPDFItems[252].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[264].Code, PDFItems.footPDFItems[264].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[283].Code, PDFItems.footPDFItems[283].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[376].Code, PDFItems.footPDFItems[376].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[404].Code, PDFItems.footPDFItems[404].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[406].Code, PDFItems.footPDFItems[406].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[511].Code, PDFItems.footPDFItems[511].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[506].Code, PDFItems.footPDFItems[506].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[508].Code, PDFItems.footPDFItems[508].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[542].Code, PDFItems.footPDFItems[542].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[547].Code, PDFItems.footPDFItems[547].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[555].Code, PDFItems.footPDFItems[555].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[554].Code, PDFItems.footPDFItems[554].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[562].Code, PDFItems.footPDFItems[562].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[560].Code, "Sedentary position is highly recommended due to flareups and sensitivity.", true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[85].Code, "PT describes pain when walking or running for extended periods and constant soreness and limitation of physical activities.", true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[91].Code, "PT describes flareups with sensitivity and tenderness which can greatly restrict mobility.", true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[86].Code, "Please see above remarks", true);

                        pdfFormFields.SetField(PDFItems.footPDFItems[73].Code, m.NameOfPatient, true);

                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[72].Code, ssn.ToString(), true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[72].Code, m.SocialSecurity, true);
                        }

                        string dt = System.DateTime.Today.ToShortDateString();
                        StringBuilder sb = new StringBuilder();

                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S62, "Flat Foot", 62, 46, 47, sb, m.Side, 79, 78, 77, PDFItems.footPDFItems, ICDCodes.footICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S71, "Plantar Fasciitis", 71, 9, 8, sb, m.Side, 5, 6, 7, PDFItems.footPDFItems, ICDCodes.footICDCodes);


                        if (!string.IsNullOrEmpty(m.S112Other))
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[112].Code, PDFItems.footPDFItems[165].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[115].Code, m.S112Other, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[111].Code, dt, true);
                            //sb.Append(m.S112Other);
                            switch (m.Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.footPDFItems[97].Code, PDFItems.footPDFItems[97].ExportValue, true);
                                    //sb.Append(", Right");
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.footPDFItems[108].Code, PDFItems.footPDFItems[108].ExportValue, true);
                                    //sb.Append(", Left");
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.footPDFItems[109].Code, PDFItems.footPDFItems[109].ExportValue, true);
                                    //sb.Append(", Bilateral");
                                    break;
                                default:
                                    break;
                            }

                            //sb.Append(".");
                        }

                        //pdfFormFields.SetField(PDFItems.footPDFItems[75].Code, sb.ToString(), true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[75].Code, m.S75, true);

                        if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[498].Code, PDFItems.footPDFItems[498].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[500].Code, PDFItems.footPDFItems[500].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[493].Code, PDFItems.footPDFItems[493].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[494].Code, PDFItems.footPDFItems[494].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[497].Code, "Please see section 13a", true);
                        }
                        else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[199].Code, PDFItems.footPDFItems[199].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[501].Code, PDFItems.footPDFItems[501].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[493].Code, PDFItems.footPDFItems[493].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[494].Code, PDFItems.footPDFItems[494].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.footPDFItems[502].Code, "Please see section 13a", true);
                        }

                        if (m.S62)
                        {
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[79].Code, PDFItems.footPDFItems[79].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[78].Code, PDFItems.footPDFItems[78].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[77].Code, PDFItems.footPDFItems[77].ExportValue, true);
                            }
                        }

                        if (m.HasPainOnUse)
                        {
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[135].Code, PDFItems.footPDFItems[135].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[131].Code, PDFItems.footPDFItems[131].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[132].Code, PDFItems.footPDFItems[132].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[128].Code, PDFItems.footPDFItems[128].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[135].Code, PDFItems.footPDFItems[135].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[130].Code, PDFItems.footPDFItems[130].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[132].Code, PDFItems.footPDFItems[132].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[127].Code, PDFItems.footPDFItems[127].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[135].Code, PDFItems.footPDFItems[135].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[129].Code, PDFItems.footPDFItems[129].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[132].Code, PDFItems.footPDFItems[132].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[126].Code, PDFItems.footPDFItems[126].ExportValue, true);
                            }
                        }

                        if (m.HasSwelling)
                        {
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[172].Code, PDFItems.footPDFItems[172].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[142].Code, PDFItems.footPDFItems[142].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[172].Code, PDFItems.footPDFItems[172].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[143].Code, PDFItems.footPDFItems[143].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[172].Code, PDFItems.footPDFItems[172].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[144].Code, PDFItems.footPDFItems[144].ExportValue, true);
                            }

                        }

                        if (m.HasCalluses)
                        {
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[169].Code, PDFItems.footPDFItems[169].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[139].Code, PDFItems.footPDFItems[139].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[169].Code, PDFItems.footPDFItems[169].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[140].Code, PDFItems.footPDFItems[140].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[169].Code, PDFItems.footPDFItems[169].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[139].Code, PDFItems.footPDFItems[139].ExportValue, true);
                            }

                        }

                        if (m.HasTenderness)
                        {
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[168].Code, PDFItems.footPDFItems[168].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[136].Code, PDFItems.footPDFItems[136].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[164].Code, PDFItems.footPDFItems[164].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[168].Code, PDFItems.footPDFItems[168].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[137].Code, PDFItems.footPDFItems[137].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[215].Code, PDFItems.footPDFItems[215].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[168].Code, PDFItems.footPDFItems[168].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[138].Code, PDFItems.footPDFItems[138].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[164].Code, PDFItems.footPDFItems[164].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[215].Code, PDFItems.footPDFItems[215].ExportValue, true);
                            }

                        }

                        if (m.HasMarkedDeformity)
                        {
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[174].Code, PDFItems.footPDFItems[174].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[147].Code, PDFItems.footPDFItems[147].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[184].Code, PDFItems.footPDFItems[184].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[162].Code, PDFItems.footPDFItems[162].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[217].Code, PDFItems.footPDFItems[217].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[174].Code, PDFItems.footPDFItems[174].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[146].Code, PDFItems.footPDFItems[146].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[184].Code, PDFItems.footPDFItems[184].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[161].Code, PDFItems.footPDFItems[161].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[221].Code, PDFItems.footPDFItems[221].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[174].Code, PDFItems.footPDFItems[174].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[184].Code, PDFItems.footPDFItems[184].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[145].Code, PDFItems.footPDFItems[145].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[160].Code, PDFItems.footPDFItems[160].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[217].Code, PDFItems.footPDFItems[217].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.footPDFItems[221].Code, PDFItems.footPDFItems[221].ExportValue, true);
                            }

                        }

                        if (m.DeviceArchSupport)
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[202].Code, PDFItems.footPDFItems[202].ExportValue, true);
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[205].Code, PDFItems.footPDFItems[205].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[204].Code, PDFItems.footPDFItems[204].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[203].Code, PDFItems.footPDFItems[203].ExportValue, true);
                            }
                        }

                        if (m.DeviceBuiltupShoes)
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[210].Code, PDFItems.footPDFItems[210].ExportValue, true);
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[213].Code, PDFItems.footPDFItems[213].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[212].Code, PDFItems.footPDFItems[212].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[211].Code, PDFItems.footPDFItems[211].ExportValue, true);
                            }
                        }

                        if (m.DeviceOrthotics)
                        {
                            pdfFormFields.SetField(PDFItems.footPDFItems[206].Code, PDFItems.footPDFItems[206].ExportValue, true);
                            if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "RIGHT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[209].Code, PDFItems.footPDFItems[209].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "LEFT"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[208].Code, PDFItems.footPDFItems[208].ExportValue, true);
                            }
                            else if ((!string.IsNullOrEmpty(m.Side)) && (m.Side == "BOTH"))
                            {
                                pdfFormFields.SetField(PDFItems.footPDFItems[207].Code, PDFItems.footPDFItems[207].ExportValue, true);
                            }
                        }

                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S430, m.Side, 430, 431, 472, 473);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S474, m.Side, 474, 471, 470, 469);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S428, m.Side, 428, 432, 467, 468);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S427, m.Side, 427, 466, 465, 464);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S490, m.Side, 490, 433, 462, 463);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S481, m.Side, 481, 461, 460, 459);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S489, m.Side, 489, 488, 487, 486);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S485, m.Side, 485, 484, 483, 482);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S480, m.Side, 480, 434, 457, 458);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S479, m.Side, 479, 456, 455, 454);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S478, m.Side, 478, 435, 452, 453);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S436, m.Side, 436, 451, 450, 449);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S448, m.Side, 448, 437, 446, 447);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S438, m.Side, 438, 445, 444, 443);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.footPDFItems, m.S442, m.Side, 442, 439, 440, 441);

                        pdfFormFields.SetField(PDFItems.footPDFItems[83].Code, m.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[85].Code, m.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.footPDFItems[91].Code, m.VarianceFunctionLossWriteIn, true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.footPDFItems[49].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }


                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        /**************************************************************
          * Sleepapnea Form
          * 
          *************************************************************/
        public byte[] Sleepapnea(string template, SleepapneaModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // Defaults
                        pdfFormFields.SetField("F[0].Page_1[0].Yes1[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_1[0].B1[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_1[0].No4A[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_1[0].No4B[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_2[0].No5B[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_2[0].No10[0]", "1", true);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[22].Code, PDFItems.sleepapneaPDFItems[22].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[24].Code, PDFItems.sleepapneaPDFItems[24].ExportValue, true);

                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[18].Code, m.S18, true);

                        if (!string.IsNullOrEmpty(m.FirstName))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[44].Code, m.FirstName, true);
                        }

                        if (!string.IsNullOrEmpty(m.MiddleInitial))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[46].Code, m.MiddleInitial, true);
                        }

                        if (!string.IsNullOrEmpty(m.LastName))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[45].Code, m.LastName, true);
                        }

                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[41].Code, ssn.LeftPart, true);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[42].Code, ssn.MiddlePart, true);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[43].Code, ssn.RightPart, true);

                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[68].Code, ssn.LeftPart, true);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[69].Code, ssn.MiddlePart, true);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[70].Code, ssn.RightPart, true);

                        }

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string S17 = "The onset of veterans sleep apnea was during active duty service";

                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[6].Code, PDFItems.sleepapneaPDFItems[6].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[7].Code, dt, true);
                        if (ICDCodes.sleepapneaICDCodes.TryGetValue("obstructive", out icdcode))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[16].Code, icdcode.RefNumber, true);
                        }

                        iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 6f, iTextSharp.text.Font.NORMAL);
                        normal = FontFactory.GetFont(FontFactory.COURIER, 4f, iTextSharp.text.Font.NORMAL);
                        //set the field to bold
                        pdfFormFields.SetFieldProperty(PDFItems.sleepapneaPDFItems[17].Code, "textfont", normal.BaseFont, null);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[17].Code, S17, true);

                        if (m.S20)
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[20].Code, PDFItems.sleepapneaPDFItems[20].ExportValue, true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[21].Code, PDFItems.sleepapneaPDFItems[21].ExportValue, true);
                        }

                        if (!string.IsNullOrEmpty(m.lastSleepStudyDate))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[55].Code, m.lastSleepStudyDate, true);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[52].Code, PDFItems.sleepapneaPDFItems[52].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[49].Code, PDFItems.sleepapneaPDFItems[49].ExportValue, true);

                        }

                        if (!string.IsNullOrEmpty(m.FacilityName))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[53].Code, m.FacilityName, true);
                        }

                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[54].Code, "OSA dx", true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.sleepapneaPDFItems[23].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }


                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        /**************************************************************
          * Headache Form
          * 
          *************************************************************/
        public byte[] Headache(string template, HeadacheModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // Defaults
                        pdfFormFields.SetField("F[0].Page_1[0].Yes1[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_1[0].DescribeHistory[0]", "The onset of this disability was during military service.", true);
                        pdfFormFields.SetField("F[0].Page_2[0].No12[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_3[0].No11[0]", "1", true);
                        pdfFormFields.SetField("F[0].Page_3[0].No52[0]", "1", true);

                        pdfFormFields.SetField(PDFItems.headachePDFItems[28].Code, m.S28, true);

                        if (!string.IsNullOrEmpty(m.FirstName))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[39].Code, m.FirstName, true);
                        }

                        if (!string.IsNullOrEmpty(m.MiddleInitial))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[41].Code, m.MiddleInitial, true);
                        }

                        if (!string.IsNullOrEmpty(m.LastName))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[40].Code, m.LastName, true);
                        }

                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[36].Code, ssn.LeftPart, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[37].Code, ssn.MiddlePart, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[38].Code, ssn.RightPart, true);

                            pdfFormFields.SetField(PDFItems.headachePDFItems[85].Code, ssn.LeftPart, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[86].Code, ssn.MiddlePart, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[87].Code, ssn.RightPart, true);

                            pdfFormFields.SetField(PDFItems.headachePDFItems[105].Code, ssn.LeftPart, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[106].Code, ssn.MiddlePart, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[107].Code, ssn.RightPart, true);

                        }

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;
                        if (m.S35)
                        {
                            diagnosis = "Migrane";
                            pdfFormFields.SetField(PDFItems.headachePDFItems[35].Code, PDFItems.headachePDFItems[35].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[19].Code, dt, true);
                            if (ICDCodes.headacheICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[22].Code, icdcode.RefNumber, true);
                            }
                        }
                        if (m.S23)
                        {
                            diagnosis = "Tension";
                            pdfFormFields.SetField(PDFItems.headachePDFItems[23].Code, PDFItems.headachePDFItems[23].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[18].Code, dt, true);
                            if (ICDCodes.headacheICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[21].Code, icdcode.RefNumber, true);
                            }
                        }
                        if (m.S24)
                        {
                            diagnosis = "Cluster";
                            pdfFormFields.SetField(PDFItems.headachePDFItems[24].Code, PDFItems.headachePDFItems[24].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[8].Code, dt, true);
                            if (ICDCodes.headacheICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[20].Code, icdcode.RefNumber, true);
                            }
                        }
                        if (!string.IsNullOrEmpty(m.S25Other))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[25].Code, PDFItems.headachePDFItems[25].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[11].Code, dt, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[10].Code, m.S25Other, true);
                        }

                        if (!string.IsNullOrEmpty(m.MedicationPlan))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[32].Code, PDFItems.headachePDFItems[32].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[30].Code, m.MedicationPlan, true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[33].Code, PDFItems.headachePDFItems[33].ExportValue, true);
                        }

                        bool b3A = false;
                        if (m.S26)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[26].Code, PDFItems.headachePDFItems[26].ExportValue, true);
                            b3A = true;
                        }
                        if (m.S2)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[2].Code, PDFItems.headachePDFItems[2].ExportValue, true);
                            b3A = true;
                        }
                        if (m.S3)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[3].Code, PDFItems.headachePDFItems[3].ExportValue, true);
                            b3A = true;
                        }
                        if (m.S4)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[4].Code, PDFItems.headachePDFItems[4].ExportValue, true);
                            b3A = true;
                        }
                        if (m.S5)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[5].Code, PDFItems.headachePDFItems[5].ExportValue, true);
                            b3A = true;
                        }
                        if (!string.IsNullOrEmpty(m.S3AOther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[6].Code, PDFItems.headachePDFItems[6].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[15].Code, m.S3AOther, true);
                            b3A = true;
                        }
                        if (b3A)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[1].Code, PDFItems.headachePDFItems[1].ExportValue, true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[7].Code, PDFItems.headachePDFItems[7].ExportValue, true);
                        }
                        m.S3AYes = b3A;


                        bool b3B = false;
                        if (m.S75)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[75].Code, PDFItems.headachePDFItems[75].ExportValue, true);
                            b3B = true;
                        }
                        if (m.S68)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[68].Code, PDFItems.headachePDFItems[68].ExportValue, true);
                            b3B = true;
                        }
                        if (m.S69)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[69].Code, PDFItems.headachePDFItems[69].ExportValue, true);
                            b3B = true;
                        }
                        if (m.S70)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[70].Code, PDFItems.headachePDFItems[70].ExportValue, true);
                            b3B = true;
                        }
                        if (m.S71)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[71].Code, PDFItems.headachePDFItems[71].ExportValue, true);
                            b3B = true;
                        }
                        if (m.S72)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[72].Code, PDFItems.headachePDFItems[72].ExportValue, true);
                            b3B = true;
                        }
                        if (!string.IsNullOrEmpty(m.S3BOther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[73].Code, PDFItems.headachePDFItems[73].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[74].Code, m.S3AOther, true);
                            b3B = true;
                        }
                        if (b3B)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[77].Code, PDFItems.headachePDFItems[77].ExportValue, true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[76].Code, PDFItems.headachePDFItems[76].ExportValue, true);
                        }
                        m.S3BYes = b3B;

                        if (m.S54)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[54].Code, PDFItems.headachePDFItems[54].ExportValue, true);
                        }
                        if (m.S55)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[55].Code, PDFItems.headachePDFItems[55].ExportValue, true);
                        }
                        if (m.S60)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[60].Code, PDFItems.headachePDFItems[60].ExportValue, true);
                        }
                        if (!string.IsNullOrEmpty(m.S3COther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[61].Code, PDFItems.headachePDFItems[61].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[62].Code, m.S3COther, true);
                        }

                        if (m.S66)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[66].Code, PDFItems.headachePDFItems[66].ExportValue, true);
                        }
                        if (m.S65)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[65].Code, PDFItems.headachePDFItems[65].ExportValue, true);
                        }
                        if (m.S64)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[64].Code, PDFItems.headachePDFItems[64].ExportValue, true);
                        }
                        if (!string.IsNullOrEmpty(m.S3DOther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[63].Code, PDFItems.headachePDFItems[63].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[67].Code, m.S3DOther, true);
                        }


                        switch (m.WorkCondition)
                        {
                            case "A":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7ARemark, true);
                                break;
                            case "B":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7BRemark, true);
                                break;
                            case "C":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7CRemark, true);
                                break;
                            case "D":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7DRemark, true);
                                break;
                            default:
                                pdfFormFields.SetField(PDFItems.headachePDFItems[98].Code, PDFItems.headachePDFItems[98].ExportValue, true);
                                break;
                        }

                        if ((m.S35) && (((m.S23) || (m.S24) || (!string.IsNullOrEmpty(m.S25Other)))))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[46].Code, PDFItems.headachePDFItems[46].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[48].Code, PDFItems.headachePDFItems[48].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[51].Code, PDFItems.headachePDFItems[51].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[53].Code, PDFItems.headachePDFItems[53].ExportValue, true);

                            if (m.S56)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[56].Code, PDFItems.headachePDFItems[56].ExportValue, true);
                            }
                            if (m.S57)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[57].Code, PDFItems.headachePDFItems[57].ExportValue, true);
                            }
                            if (m.S58)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[58].Code, PDFItems.headachePDFItems[58].ExportValue, true);
                            }
                            if (m.S59)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[59].Code, PDFItems.headachePDFItems[59].ExportValue, true);
                            }

                        }
                        else if ((m.S23) || (m.S24) || (!string.IsNullOrEmpty(m.S25Other)))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[47].Code, PDFItems.headachePDFItems[47].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[49].Code, PDFItems.headachePDFItems[49].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[50].Code, PDFItems.headachePDFItems[50].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[52].Code, PDFItems.headachePDFItems[52].ExportValue, true);

                            if (m.S45)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[45].Code, PDFItems.headachePDFItems[45].ExportValue, true);
                            }
                            if (m.S44)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[44].Code, PDFItems.headachePDFItems[44].ExportValue, true);
                            }
                            if (m.S43)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[43].Code, PDFItems.headachePDFItems[43].ExportValue, true);
                            }
                            if (m.S42)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[42].Code, PDFItems.headachePDFItems[42].ExportValue, true);
                            }


                        }
                        else if (m.S35)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[46].Code, PDFItems.headachePDFItems[46].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[48].Code, PDFItems.headachePDFItems[48].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[51].Code, PDFItems.headachePDFItems[51].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[53].Code, PDFItems.headachePDFItems[53].ExportValue, true);

                            if (m.S56)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[56].Code, PDFItems.headachePDFItems[56].ExportValue, true);
                            }
                            if (m.S57)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[57].Code, PDFItems.headachePDFItems[57].ExportValue, true);
                            }
                            if (m.S58)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[58].Code, PDFItems.headachePDFItems[58].ExportValue, true);
                            }
                            if (m.S59)
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[59].Code, PDFItems.headachePDFItems[59].ExportValue, true);
                            }

                        }

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.headachePDFItems[34].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }


                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        /**************************************************************
          * Ankle Form
          * 
          *************************************************************/
        public byte[] Ankle(string template, AnkleModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // defaults

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, lose of strength, soreness, and pain.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Severe restriction of range of motion.", true);

                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", "On-set of injury incurred during active duty service.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo4[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Painful[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Painful[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Left_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Pain1[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Tender[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Tender[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Loss[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Loss[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength2[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength2[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].RAnkylosis8[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[4].LAnkylosis8[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo9Right[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Right_TalarTest[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Right_AnteriorTest[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Left_AnteriorTest[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Right_TalarTest[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Left_TalarTest[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo13[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo14[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo18[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo19[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo20[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo22[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo23[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo24[0]", "2", true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[342].Code, PDFItems.anklePDFItems[342].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[345].Code, PDFItems.anklePDFItems[345].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[367].Code, PDFItems.anklePDFItems[367].ExportValue, true);

                        pdfFormFields.SetField(PDFItems.anklePDFItems[130].Code, PDFItems.anklePDFItems[130].ExportValue, true);


                        pdfFormFields.SetField(PDFItems.anklePDFItems[95].Code, m.NameOfPatient, true);
                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[94].Code, ssn.ToString(), true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[94].Code, m.SocialSecurity, true);
                        }


                        pdfFormFields.SetField(PDFItems.anklePDFItems[69].Code, PDFItems.anklePDFItems[69].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[76].Code, PDFItems.anklePDFItems[76].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[77].Code, PDFItems.anklePDFItems[77].ExportValue, true);

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;
                        //string S97 = string.Empty;

                        if (m.S82)
                        {
                            diagnosis = "Lateral Collateral Ligament Sprain";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[82].Code, PDFItems.anklePDFItems[82].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[66].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[67].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S82Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[101].Code, PDFItems.anklePDFItems[101].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[100].Code, PDFItems.anklePDFItems[190].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[99].Code, PDFItems.anklePDFItems[99].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S83)
                        {
                            diagnosis = "Deltoid Ligamnet Sprain";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[83].Code, PDFItems.anklePDFItems[83].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[65].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[64].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S83Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[1].Code, PDFItems.anklePDFItems[1].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[62].Code, PDFItems.anklePDFItems[62].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[63].Code, PDFItems.anklePDFItems[63].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S84)
                        {
                            diagnosis = "Osteochondritis Dissecans to include Ostechondral Fracture";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[84].Code, PDFItems.anklePDFItems[84].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[57].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[58].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S84Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[61].Code, PDFItems.anklePDFItems[61].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[60].Code, PDFItems.anklePDFItems[60].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[59].Code, PDFItems.anklePDFItems[59].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S85)
                        {
                            diagnosis = "Impingement";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[85].Code, PDFItems.anklePDFItems[85].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[56].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[55].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S85Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[2].Code, PDFItems.anklePDFItems[2].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[53].Code, PDFItems.anklePDFItems[53].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[54].Code, PDFItems.anklePDFItems[54].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S86)
                        {
                            diagnosis = "Tendonitis";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[86].Code, PDFItems.anklePDFItems[86].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[48].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[49].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S86Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[52].Code, PDFItems.anklePDFItems[52].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[51].Code, PDFItems.anklePDFItems[51].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[50].Code, PDFItems.anklePDFItems[50].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S87)
                        {
                            diagnosis = "Retrocalcaneal Bursitis";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[87].Code, PDFItems.anklePDFItems[87].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[47].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[46].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S87Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[3].Code, PDFItems.anklePDFItems[3].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[44].Code, PDFItems.anklePDFItems[44].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[45].Code, PDFItems.anklePDFItems[45].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S88)
                        {
                            diagnosis = "Achilles Tendon Rupture";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[88].Code, PDFItems.anklePDFItems[88].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[39].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[40].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S88Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[43].Code, PDFItems.anklePDFItems[43].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[42].Code, PDFItems.anklePDFItems[42].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[41].Code, PDFItems.anklePDFItems[41].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S89)
                        {
                            diagnosis = "Osteoarthritis Of The Ankle";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[89].Code, PDFItems.anklePDFItems[89].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[38].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[37].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S89Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[4].Code, PDFItems.anklePDFItems[4].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[35].Code, PDFItems.anklePDFItems[35].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[36].Code, PDFItems.anklePDFItems[36].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S90)
                        {
                            diagnosis = "Avascular Necrosis, Talus";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[90].Code, PDFItems.anklePDFItems[90].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[30].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[31].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S90Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[34].Code, PDFItems.anklePDFItems[34].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[33].Code, PDFItems.anklePDFItems[33].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[32].Code, PDFItems.anklePDFItems[32].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S91)
                        {
                            diagnosis = "Ankle Joint Replacement";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[91].Code, PDFItems.anklePDFItems[91].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[29].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[28].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S91Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[5].Code, PDFItems.anklePDFItems[5].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[26].Code, PDFItems.anklePDFItems[26].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[27].Code, PDFItems.anklePDFItems[27].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }
                        if (m.S92)
                        {
                            diagnosis = "Ankylosis Of Ankle,Subtatalar Or Tarsal Joint";
                            pdfFormFields.SetField(PDFItems.anklePDFItems[92].Code, PDFItems.anklePDFItems[92].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[21].Code, dt, true);
                            if (ICDCodes.ankleICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[22].Code, icdcode.RefNumber, true);
                            }
                            //S97 += diagnosis;
                            switch (m.S92Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[25].Code, PDFItems.anklePDFItems[25].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[24].Code, PDFItems.anklePDFItems[24].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[23].Code, PDFItems.anklePDFItems[23].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S97 += ".  ";
                        }

                        if (!string.IsNullOrEmpty(m.S93Other))
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[93].Code, PDFItems.anklePDFItems[93].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[104].Code, m.S93Other, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[20].Code, dt, true);
                            //S97 += m.S93Other;
                            switch (m.S93Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[6].Code, PDFItems.anklePDFItems[6].ExportValue, true);
                                    //S97 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[17].Code, PDFItems.anklePDFItems[17].ExportValue, true);
                                    //S97 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[18].Code, PDFItems.anklePDFItems[18].ExportValue, true);
                                    //S97 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }

                            //S97 += ".";
                        }

                        //pdfFormFields.SetField(PDFItems.anklePDFItems[97].Code, S97, true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[97].Code, m.S97, true);


                        bool DoInitialROMLeft = false;
                        bool DoInitialROMRight = false;

                        if ((m.S82Side == "BOTH")
                            || (m.S83Side == "BOTH")
                            || (m.S84Side == "BOTH")
                            || (m.S85Side == "BOTH")
                            || (m.S86Side == "BOTH")
                            || (m.S87Side == "BOTH")
                            || (m.S88Side == "BOTH")
                            || (m.S89Side == "BOTH")
                            || (m.S90Side == "BOTH")
                            || (m.S91Side == "BOTH")
                            || (m.S92Side == "BOTH")
                            )
                        {
                            DoInitialROMLeft = true;
                            DoInitialROMRight = true;
                        }
                        if ((m.S82Side == "LEFT")
                            || (m.S83Side == "LEFT")
                            || (m.S84Side == "LEFT")
                            || (m.S85Side == "LEFT")
                            || (m.S86Side == "LEFT")
                            || (m.S87Side == "LEFT")
                            || (m.S88Side == "LEFT")
                            || (m.S89Side == "LEFT")
                            || (m.S90Side == "LEFT")
                            || (m.S91Side == "LEFT")
                            || (m.S92Side == "LEFT")
                            )
                        {
                            DoInitialROMLeft = true;
                        }
                        if ((m.S82Side == "RIGHT")
                            || (m.S83Side == "RIGHT")
                            || (m.S84Side == "RIGHT")
                            || (m.S85Side == "RIGHT")
                            || (m.S86Side == "RIGHT")
                            || (m.S87Side == "RIGHT")
                            || (m.S88Side == "RIGHT")
                            || (m.S89Side == "RIGHT")
                            || (m.S90Side == "RIGHT")
                            || (m.S91Side == "RIGHT")
                            || (m.S92Side == "RIGHT")
                            )
                        {
                            DoInitialROMRight = true;
                        }

                        bool SameInitialROMLeft = false;
                        bool SameInitialROMRight = false;
                        bool SameFinalROMLeft = false;
                        bool SameFinalROMRight = false;

                        if ((DoInitialROMLeft) && (DoInitialROMRight))
                        {
                            // set the RIGHT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[133].Code, m.S133, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[137].Code, m.S137, true);

                            if ((m.S133 == m.S165)
                                && (m.S137 == m.S164)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Set the LEFT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[121].Code, m.S121, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[117].Code, m.S117, true);

                            if ((m.S121 == m.S150)
                                && (m.S117 == m.S151)
                                )
                            {
                                SameInitialROMLeft = true;
                            }


                            // Second test RIGHT

                            pdfFormFields.SetField(PDFItems.anklePDFItems[166].Code, PDFItems.anklePDFItems[166].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[149].Code, PDFItems.anklePDFItems[149].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[168].Code, PDFItems.anklePDFItems[168].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[147].Code, PDFItems.anklePDFItems[147].ExportValue, true);

                            //                        pdfFormFields.SetField(PDFItems.anklePDFItems[166].Code, PDFItems.anklePDFItems[166].ExportValue, true);
                            //                        if (SameInitialROMRight)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[168].Code, PDFItems.anklePDFItems[168].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[169].Code, PDFItems.anklePDFItems[169].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[165].Code, m.S165, true);
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[164].Code, m.S164, true);
                            //                        }
                            //                        if ((m.S165 == m.S252)
                            //                            && (m.S164 == m.S255)
                            //                            )
                            //                        {
                            //                            SameFinalROMRight = true;
                            //                        }


                            //                        // Second test LEFT
                            //                        pdfFormFields.SetField(PDFItems.anklePDFItems[149].Code, PDFItems.anklePDFItems[149].ExportValue, true);
                            //                        if (SameInitialROMLeft)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[147].Code, PDFItems.anklePDFItems[147].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[146].Code, PDFItems.anklePDFItems[146].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[150].Code, m.S150, true);
                            //                            pdfFormFields.SetField(PDFItems.anklePDFItems[151].Code, m.S151, true);

                            //                        }
                            //                        if ((m.S150 == m.S237)
                            //&& (m.S151 == m.S234)
                            //)
                            //                        {
                            //                            SameFinalROMLeft = true;
                            //                        }



                            // Final test RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[257].Code, PDFItems.anklePDFItems[257].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[256].Code, PDFItems.anklePDFItems[256].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[252].Code, m.S252, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[255].Code, m.S255, true);
                            }

                            // Final test LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[238].Code, PDFItems.anklePDFItems[238].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[239].Code, PDFItems.anklePDFItems[239].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[237].Code, m.S237, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[234].Code, m.S234, true);
                            }

                        }
                        else if (DoInitialROMRight)
                        {
                            // the default for initial LEFT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[121].Code, "45", true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[117].Code, "20", true);

                            // set the values for initial RIGHT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[133].Code, m.S133, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[137].Code, m.S137, true);

                            if ((m.S133 == m.S165)
                                && (m.S137 == m.S164)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Second test for setting RIGHT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[166].Code, PDFItems.anklePDFItems[166].ExportValue, true);
                            if (SameInitialROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[168].Code, PDFItems.anklePDFItems[168].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[169].Code, PDFItems.anklePDFItems[169].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[165].Code, m.S165, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[164].Code, m.S164, true);

                            }
                            if ((m.S165 == m.S252)
    && (m.S164 == m.S255)
    )
                            {
                                SameFinalROMRight = true;
                            }


                            // final RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[257].Code, PDFItems.anklePDFItems[257].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[256].Code, PDFItems.anklePDFItems[256].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[252].Code, m.S252, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[255].Code, m.S255, true);
                            }

                            // set the LEFT repeat to no change
                            pdfFormFields.SetField(PDFItems.anklePDFItems[149].Code, PDFItems.anklePDFItems[149].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[147].Code, PDFItems.anklePDFItems[147].ExportValue, true);

                            // set the LEFT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.anklePDFItems[238].Code, PDFItems.anklePDFItems[238].ExportValue, true);

                        }
                        else if (DoInitialROMLeft)
                        {
                            // the default for initial RIGHT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[133].Code, "45", true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[137].Code, "20", true);

                            // set the initial LEFT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[121].Code, m.S121, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[117].Code, m.S117, true);

                            if ((m.S121 == m.S150)
                                && (m.S117 == m.S151)
                                )
                            {
                                SameInitialROMLeft = true;
                            }


                            // Second test LEFT
                            pdfFormFields.SetField(PDFItems.anklePDFItems[149].Code, PDFItems.anklePDFItems[149].ExportValue, true);
                            if (SameInitialROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[147].Code, PDFItems.anklePDFItems[147].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[146].Code, PDFItems.anklePDFItems[146].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[150].Code, m.S150, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[151].Code, m.S151, true);


                            }
                            if ((m.S150 == m.S237)
    && (m.S151 == m.S234)
    )
                            {
                                SameFinalROMLeft = true;
                            }

                            // final LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[238].Code, PDFItems.anklePDFItems[238].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.anklePDFItems[239].Code, PDFItems.anklePDFItems[239].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[237].Code, m.S237, true);
                                pdfFormFields.SetField(PDFItems.anklePDFItems[234].Code, m.S234, true);
                            }

                            // set the RIGHT repeat to no change
                            pdfFormFields.SetField(PDFItems.anklePDFItems[166].Code, PDFItems.anklePDFItems[166].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.anklePDFItems[168].Code, PDFItems.anklePDFItems[168].ExportValue, true);

                            // set the RIGHT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.anklePDFItems[257].Code, PDFItems.anklePDFItems[257].ExportValue, true);

                        }


                        if ((!SameFinalROMLeft) || (!SameFinalROMRight))
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[233].Code, PDFItems.anklePDFItems[233].ExportValue, true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[232].Code, PDFItems.anklePDFItems[232].ExportValue, true);
                        }

                        if (m.S187)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[187].Code, PDFItems.anklePDFItems[187].ExportValue, true);
                            switch (m.S187Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[221].Code, PDFItems.anklePDFItems[221].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[251].Code, PDFItems.anklePDFItems[251].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[222].Code, PDFItems.anklePDFItems[222].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S223)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[223].Code, PDFItems.anklePDFItems[223].ExportValue, true);
                            switch (m.S223Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[220].Code, PDFItems.anklePDFItems[220].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[250].Code, PDFItems.anklePDFItems[250].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[219].Code, PDFItems.anklePDFItems[219].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S185)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[185].Code, PDFItems.anklePDFItems[185].ExportValue, true);
                            switch (m.S185Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[217].Code, PDFItems.anklePDFItems[217].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[249].Code, PDFItems.anklePDFItems[249].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[218].Code, PDFItems.anklePDFItems[218].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S184)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[184].Code, PDFItems.anklePDFItems[184].ExportValue, true);
                            switch (m.S184Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[216].Code, PDFItems.anklePDFItems[216].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[248].Code, PDFItems.anklePDFItems[248].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[215].Code, PDFItems.anklePDFItems[215].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S231)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[231].Code, PDFItems.anklePDFItems[231].ExportValue, true);
                            switch (m.S231Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[213].Code, PDFItems.anklePDFItems[213].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[247].Code, PDFItems.anklePDFItems[247].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[214].Code, PDFItems.anklePDFItems[214].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S230)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[230].Code, PDFItems.anklePDFItems[230].ExportValue, true);
                            switch (m.S230Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[212].Code, PDFItems.anklePDFItems[212].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[246].Code, PDFItems.anklePDFItems[246].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[211].Code, PDFItems.anklePDFItems[211].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S229)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[229].Code, PDFItems.anklePDFItems[229].ExportValue, true);
                            switch (m.S229Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[209].Code, PDFItems.anklePDFItems[209].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[245].Code, PDFItems.anklePDFItems[245].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[210].Code, PDFItems.anklePDFItems[210].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S228)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[228].Code, PDFItems.anklePDFItems[228].ExportValue, true);
                            switch (m.S228Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[208].Code, PDFItems.anklePDFItems[208].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[244].Code, PDFItems.anklePDFItems[244].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[207].Code, PDFItems.anklePDFItems[207].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S227)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[227].Code, PDFItems.anklePDFItems[227].ExportValue, true);
                            switch (m.S227Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[205].Code, PDFItems.anklePDFItems[205].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[188].Code, PDFItems.anklePDFItems[188].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[206].Code, PDFItems.anklePDFItems[206].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S189)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[189].Code, PDFItems.anklePDFItems[189].ExportValue, true);
                            switch (m.S189Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[203].Code, PDFItems.anklePDFItems[203].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[204].Code, PDFItems.anklePDFItems[204].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[202].Code, PDFItems.anklePDFItems[202].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S201)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[201].Code, PDFItems.anklePDFItems[201].ExportValue, true);
                            switch (m.S201Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[199].Code, PDFItems.anklePDFItems[199].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[190].Code, PDFItems.anklePDFItems[190].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[200].Code, PDFItems.anklePDFItems[200].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S191)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[191].Code, PDFItems.anklePDFItems[191].ExportValue, true);
                            switch (m.S191Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[197].Code, PDFItems.anklePDFItems[197].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[198].Code, PDFItems.anklePDFItems[198].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[196].Code, PDFItems.anklePDFItems[196].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S195)
                        {
                            pdfFormFields.SetField(PDFItems.anklePDFItems[195].Code, PDFItems.anklePDFItems[195].ExportValue, true);
                            switch (m.S195Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[193].Code, PDFItems.anklePDFItems[193].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[192].Code, PDFItems.anklePDFItems[192].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.anklePDFItems[194].Code, PDFItems.anklePDFItems[194].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }

                        pdfFormFields.SetField(PDFItems.anklePDFItems[107].Code, m.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[109].Code, m.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.anklePDFItems[110].Code, m.VarianceFunctionLossWriteIn, true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.anklePDFItems[69].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();

                        }
                    }

                    // Set the flattening flag to true, so the document is not editable
                    if (m.IsFormReadonly)
                    {
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();
                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        /**************************************************************
          * Wrist Form
          * 
          *************************************************************/
        public byte[] Wrist(string template, WristModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // Defaults
                        pdfFormFields.SetField(PDFItems.wristPDFItems[2].Code, PDFItems.wristPDFItems[2].ExportValue, true);

                        // Section 2C
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2B[0]", Defaults.BACK_SECION_2B, true);
                        // Section 2D
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2C[0]", Defaults.BACK_SECION_2C, true);

                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2A[0]", "On-set of injury incurred during active duty service.", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Painful[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Painful[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Pain1[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss1[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Pain1[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss1[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Tender[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Tender[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Loss[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength3[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength3[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis6[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis6[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Residuals[4]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Residuals[3]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo15A[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo16A[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo20[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo17B[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo17C[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo18[0]", "2", true);

                        pdfFormFields.SetField(PDFItems.wristPDFItems[202].Code, PDFItems.wristPDFItems[202].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[184].Code, PDFItems.wristPDFItems[184].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[205].Code, PDFItems.wristPDFItems[205].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[190].Code, PDFItems.wristPDFItems[190].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[289].Code, PDFItems.wristPDFItems[289].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[290].Code, PDFItems.wristPDFItems[290].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[376].Code, PDFItems.wristPDFItems[376].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[371].Code, PDFItems.wristPDFItems[371].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[373].Code, PDFItems.wristPDFItems[373].ExportValue, true);

                        string dt = System.DateTime.Today.ToShortDateString();
                        StringBuilder sb = new StringBuilder();

                        // p1 - Main condition check
                        // p2 - The condition text
                        // p3 - Condition index
                        // p4 - date
                        // p5 - icd code index
                        // p6 - overall condition text
                        // p7 - condition side
                        // p8 - Right index
                        // p9 - Left index
                        // p10 - Both index
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S64, "Wrist Sprain", 64, 61, 62, sb, m.S64Side, 75, 74, 73, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S65, "Tendonitis, Wrist", 65, 60, 59, sb, m.S65Side, 21, 57, 58, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S66, "Ganglion Cyst", 66, 52, 53, sb, m.S66Side, 56, 55, 54, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S67, "Carpal Metacarpal, Arthritis", 67, 51, 50, sb, m.S67Side, 22, 48, 49, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S68, "Osteoarthritis, Arthtritis, Wrist", 68, 43, 44, sb, m.S68Side, 47, 46, 45, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S69, "DeQuervains Syndrome", 69, 42, 41, sb, m.S69Side, 23, 39, 40, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S70, "Triangular Fibrocartilaginous Complex Injury", 70, 34, 35, sb, m.S70Side, 38, 37, 36, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S71, "Carpal Instability", 71, 33, 32, sb, m.S71Side, 24, 30, 31, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S72, "Avascular Necrosis of Carpal Bones", 72, 25, 26, sb, m.S72Side, 29, 28, 27, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S81, "Wrist Anthroplasty", 81, 76, 77, sb, m.S81Side, 80, 79, 78, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S87, "Ankylosis Of Wrist", 87, 82, 83, sb, m.S87Side, 86, 85, 84, PDFItems.wristPDFItems, ICDCodes.wristICDCodes);
                        if (!string.IsNullOrEmpty(m.S103Other))
                        {
                            pdfFormFields.SetField(PDFItems.wristPDFItems[103].Code, PDFItems.wristPDFItems[103].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[106].Code, m.S103Other, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[102].Code, dt, true);
                            sb.Append(m.S103Other);
                            switch (m.S103Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.wristPDFItems[88].Code, PDFItems.wristPDFItems[88].ExportValue, true);
                                    sb.Append(", Right");
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.wristPDFItems[99].Code, PDFItems.wristPDFItems[99].ExportValue, true);
                                    sb.Append(", Left");
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.wristPDFItems[100].Code, PDFItems.wristPDFItems[100].ExportValue, true);
                                    sb.Append(", Bilateral");
                                    break;
                                default:
                                    break;
                            }

                            sb.Append(".");
                        }
                        //pdfFormFields.SetField(PDFItems.wristPDFItems[17].Code, sb.ToString(), true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[17].Code, m.S17, true);






                        bool DoInitialROMLeft = false;
                        bool DoInitialROMRight = false;

                        if ((m.S64Side == "BOTH")
                            || (m.S65Side == "BOTH")
                            || (m.S66Side == "BOTH")
                            || (m.S67Side == "BOTH")
                            || (m.S68Side == "BOTH")
                            || (m.S69Side == "BOTH")
                            || (m.S70Side == "BOTH")
                            || (m.S71Side == "BOTH")
                            || (m.S72Side == "BOTH")
                            || (m.S81Side == "BOTH")
                            || (m.S87Side == "BOTH")
                            || (m.S103Side == "BOTH")
                                        )
                        {
                            DoInitialROMLeft = true;
                            DoInitialROMRight = true;
                        }
                        if ((m.S64Side == "LEFT")
                            || (m.S65Side == "LEFT")
                            || (m.S66Side == "LEFT")
                            || (m.S67Side == "LEFT")
                            || (m.S68Side == "LEFT")
                            || (m.S69Side == "LEFT")
                            || (m.S70Side == "LEFT")
                            || (m.S71Side == "LEFT")
                            || (m.S72Side == "LEFT")
                            || (m.S81Side == "LEFT")
                            || (m.S87Side == "LEFT")
                            || (m.S103Side == "LEFT")
                            )
                        {
                            DoInitialROMLeft = true;
                        }
                        if ((m.S64Side == "RIGHT")
                            || (m.S65Side == "RIGHT")
                            || (m.S66Side == "RIGHT")
                            || (m.S67Side == "RIGHT")
                            || (m.S68Side == "RIGHT")
                            || (m.S69Side == "RIGHT")
                            || (m.S70Side == "RIGHT")
                            || (m.S71Side == "RIGHT")
                            || (m.S72Side == "RIGHT")
                            || (m.S81Side == "RIGHT")
                            || (m.S87Side == "RIGHT")
                            || (m.S103Side == "RIGHT")
                            )
                        {
                            DoInitialROMRight = true;
                        }

                        bool SameInitialROMLeft = false;
                        bool SameInitialROMRight = false;
                        bool SameFinalROMLeft = false;
                        bool SameFinalROMRight = false;

                        if ((DoInitialROMLeft) && (DoInitialROMRight))
                        {
                            // set the RIGHT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[135].Code, m.S135, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[131].Code, m.S131, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[123].Code, m.S123, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[127].Code, m.S127, true);

                            if ((m.S135 == m.S159)
                                && (m.S131 == m.S160)
                                && (m.S123 == m.S166)
                                && (m.S127 == m.S167)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Set the LEFT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[156].Code, m.S156, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[152].Code, m.S152, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[144].Code, m.S144, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[148].Code, m.S148, true);

                            if ((m.S156 == m.S179)
                                && (m.S152 == m.S173)
                                && (m.S144 == m.S171)
                                && (m.S148 == m.S172)
                                )
                            {
                                SameInitialROMLeft = true;
                            }


                            // Second test RIGHT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[161].Code, PDFItems.wristPDFItems[161].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[178].Code, PDFItems.wristPDFItems[178].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[163].Code, PDFItems.wristPDFItems[163].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[176].Code, PDFItems.wristPDFItems[176].ExportValue, true);

                            //                        pdfFormFields.SetField(PDFItems.wristPDFItems[161].Code, PDFItems.wristPDFItems[161].ExportValue, true);
                            //                        if (SameInitialROMRight)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[163].Code, PDFItems.wristPDFItems[163].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[164].Code, PDFItems.wristPDFItems[164].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[159].Code, m.S159, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[160].Code, m.S160, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[166].Code, m.S166, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[167].Code, m.S167, true);


                            //                        }
                            //                        if ((m.S159 == m.S272)
                            //&& (m.S160 == m.S265)
                            //&& (m.S166 == m.S266)
                            //&& (m.S167 == m.S268)
                            //)
                            //                        {
                            //                            SameFinalROMRight = true;
                            //                        }

                            //                        // Second test LEFT
                            //                        pdfFormFields.SetField(PDFItems.wristPDFItems[178].Code, PDFItems.wristPDFItems[178].ExportValue, true);
                            //                        if (SameInitialROMLeft)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[176].Code, PDFItems.wristPDFItems[176].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[175].Code, PDFItems.wristPDFItems[175].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[179].Code, m.S179, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[173].Code, m.S173, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[171].Code, m.S171, true);
                            //                            pdfFormFields.SetField(PDFItems.wristPDFItems[172].Code, m.S172, true);


                            //                        }
                            //                        if ((m.S179 == m.S283)
                            //&& (m.S173 == m.S276)
                            //&& (m.S171 == m.S280)
                            //&& (m.S172 == m.S279)
                            //)
                            //                        {
                            //                            SameFinalROMLeft = true;
                            //                        }

                            // Final test RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[274].Code, PDFItems.wristPDFItems[274].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[273].Code, PDFItems.wristPDFItems[273].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[272].Code, m.S272, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[265].Code, m.S265, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[266].Code, m.S266, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[268].Code, m.S268, true);
                            }

                            // Final test LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[284].Code, PDFItems.wristPDFItems[284].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[285].Code, PDFItems.wristPDFItems[285].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[283].Code, m.S283, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[276].Code, m.S276, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[280].Code, m.S280, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[279].Code, m.S279, true);
                            }

                        }
                        else if (DoInitialROMRight)
                        {
                            // the default for initial LEFT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[156].Code, "80", true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[152].Code, "70", true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[144].Code, "45", true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[148].Code, "20", true);

                            // set the values for initial RIGHT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[135].Code, m.S135, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[131].Code, m.S131, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[123].Code, m.S123, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[127].Code, m.S127, true);

                            if ((m.S135 == m.S159)
                                && (m.S131 == m.S160)
                                && (m.S123 == m.S166)
                                && (m.S127 == m.S167)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Second test RIGHT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[161].Code, PDFItems.wristPDFItems[161].ExportValue, true);
                            if (SameInitialROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[163].Code, PDFItems.wristPDFItems[163].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[164].Code, PDFItems.wristPDFItems[164].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[159].Code, m.S159, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[160].Code, m.S160, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[166].Code, m.S166, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[167].Code, m.S167, true);


                            }
                            if ((m.S159 == m.S272)
    && (m.S160 == m.S265)
    && (m.S166 == m.S266)
    && (m.S167 == m.S268)
    )
                            {
                                SameFinalROMRight = true;
                            }

                            // final RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[274].Code, PDFItems.wristPDFItems[274].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[273].Code, PDFItems.wristPDFItems[273].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[272].Code, m.S272, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[265].Code, m.S265, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[266].Code, m.S266, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[268].Code, m.S268, true);
                            }


                            // set the LEFT repeat to no change
                            pdfFormFields.SetField(PDFItems.wristPDFItems[178].Code, PDFItems.wristPDFItems[178].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[176].Code, PDFItems.wristPDFItems[176].ExportValue, true);

                            // set the LEFT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.wristPDFItems[284].Code, PDFItems.wristPDFItems[284].ExportValue, true);

                        }
                        else if (DoInitialROMLeft)
                        {
                            // the default for initial RIGHT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[135].Code, "80", true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[131].Code, "70", true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[123].Code, "45", true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[127].Code, "20", true);

                            // set the initial LEFT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[156].Code, m.S156, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[152].Code, m.S152, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[144].Code, m.S144, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[148].Code, m.S148, true);

                            if ((m.S156 == m.S179)
                                && (m.S152 == m.S173)
                                && (m.S144 == m.S171)
                                && (m.S148 == m.S172)
                                )
                            {
                                SameInitialROMLeft = true;
                            }

                            // Second test LEFT
                            pdfFormFields.SetField(PDFItems.wristPDFItems[178].Code, PDFItems.wristPDFItems[178].ExportValue, true);
                            if (SameInitialROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[176].Code, PDFItems.wristPDFItems[176].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[175].Code, PDFItems.wristPDFItems[175].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[179].Code, m.S179, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[173].Code, m.S173, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[171].Code, m.S171, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[172].Code, m.S172, true);


                            }
                            if ((m.S179 == m.S283)
    && (m.S173 == m.S276)
    && (m.S171 == m.S280)
    && (m.S172 == m.S279)
    )
                            {
                                SameFinalROMLeft = true;
                            }


                            // final LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[284].Code, PDFItems.wristPDFItems[284].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.wristPDFItems[285].Code, PDFItems.wristPDFItems[285].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[283].Code, m.S283, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[276].Code, m.S276, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[280].Code, m.S280, true);
                                pdfFormFields.SetField(PDFItems.wristPDFItems[279].Code, m.S279, true);
                            }

                            // set the RIGHT repeat to no change
                            pdfFormFields.SetField(PDFItems.wristPDFItems[161].Code, PDFItems.wristPDFItems[161].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.wristPDFItems[163].Code, PDFItems.wristPDFItems[163].ExportValue, true);

                            // set the RIGHT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.wristPDFItems[274].Code, PDFItems.wristPDFItems[274].ExportValue, true);

                        }

                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S210, m.S210Side, 210, 252, 211, 253);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S254, m.S254Side, 254, 250, 251, 249);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S208, m.S208Side, 208, 247, 212, 248);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S207, m.S207Side, 207, 245, 246, 244);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S261, m.S261Side, 261, 242, 213, 243);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S260, m.S260Side, 260, 240, 241, 239);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S259, m.S259Side, 259, 237, 214, 238);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S258, m.S258Side, 258, 235, 236, 234);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S257, m.S257Side, 257, 232, 215, 233);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S216, m.S216Side, 216, 230, 231, 229);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S228, m.S228Side, 228, 226, 217, 227);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S218, m.S218Side, 218, 224, 225, 223);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.wristPDFItems, m.S222, m.S222Side, 222, 220, 219, 221);

                        pdfFormFields.SetField(PDFItems.wristPDFItems[108].Code, m.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[109].Code, m.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.wristPDFItems[110].Code, m.VarianceFunctionLossWriteIn, true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.wristPDFItems[2].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }
                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        /**************************************************************
          * Knee Form
          * 
          *************************************************************/
        public byte[] Knee(string template, KneeModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // Defaults
                        PdfFill.SetMedicalRecordReviewSection(pdfFormFields, PDFItems.kneePDFItems, 44, 51, 52);

                        pdfFormFields.SetField(PDFItems.kneePDFItems[137].Code, PDFItems.kneePDFItems[137].ExportValue, true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2A[0]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2B[0]", "Physical limitations, loss of strength, soreness, and pain.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2C[0]", "Physical limitations, loss of strength, soreness, and pain. See section 6A.", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength2[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength2[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis5[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis5[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].No9C[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo10A[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo20[0]", "2", true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[320].Code, PDFItems.kneePDFItems[320].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[291].Code, PDFItems.kneePDFItems[291].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[550].Code, PDFItems.kneePDFItems[550].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[566].Code, PDFItems.kneePDFItems[566].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[559].Code, PDFItems.kneePDFItems[559].ExportValue, true);


                        string dt = System.DateTime.Today.ToShortDateString();
                        StringBuilder sb = new StringBuilder();

                        // SetConditionKnee(m.S57, "Knee Strain", 57, 41, 42, S168, m.S58Side, 73, 72, 71);
                        //SetConditionKnee(AcroFields pdfFormFields, bool p1, string p2, int p3, int p4, int p5, ref String p6, string p7, int p8, int p9, int p10)
                        // p1 - Main condition check
                        // p2 - The condition text
                        // p3 - Condition index
                        // p4 - date
                        // p5 - icd code index
                        // p6 - overall condition text
                        // p7 - condition side
                        // p8 - Right index
                        // p9 - Left index
                        // p10 - Both index
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S57, "Knee Strain", 57, 41, 42, sb, m.S57Side, 73, 72, 71, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S58, "Knee Tendonitits", 58, 40, 39, sb, m.S58Side, 1, 37, 38, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S59, "Knee Meniscal Tear", 59, 32, 33, sb, m.S59Side, 36, 35, 34, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S60, "Knee Anterior Cruciate Ligament Tear", 60, 31, 30, sb, m.S60Side, 2, 28, 29, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S61, "Knee Posterior Cruciate Ligament Tear", 61, 23, 24, sb, m.S61Side, 27, 26, 25, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S62, "Patellar or Quadriceps Tendon Rupture", 62, 22, 21, sb, m.S62Side, 3, 19, 20, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S63, "Knee Joint Osteoarthritis", 63, 14, 15, sb, m.S63Side, 18, 17, 16, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S64, "Knee Joint Ankylosis", 64, 13, 12, sb, m.S64Side, 4, 10, 11, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S65, "Knee fracture", 65, 13, 12, sb, m.S65Side, 5, 6, 7, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S79, "Stress Fracture Of Tibia", 79, 74, 75, sb, m.S79Side, 78, 77, 76, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S127, "Tibia and/or Fibula Fracture", 127, 122, 123, sb, m.S127Side, 126, 125, 124, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S121, "Recurrent Patellar Dislocation", 121, 116, 117, sb, m.S121Side, 120, 119, 118, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S115, "Recurrent Subluxation", 115, 110, 111, sb, m.S115Side, 114, 113, 112, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S109, "Knee Instability", 109, 104, 105, sb, m.S109Side, 108, 107, 106, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S103, "Patellar Dislocation", 103, 98, 99, sb, m.S103Side, 102, 101, 100, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S85, "Knee Cartilage Restoration Surgery", 85, 80, 81, sb, m.S85Side, 84, 83, 82, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S97, "Shin Splints", 97, 92, 93, sb, m.S97Side, 96, 95, 94, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);
                        PdfFill.SetClaimedConditionsSection(pdfFormFields, m.S91, "Patellofemoral Pain Syndrome", 91, 86, 87, sb, m.S91Side, 90, 89, 88, PDFItems.kneePDFItems, ICDCodes.kneeICDCodes);


                        if (!string.IsNullOrEmpty(m.S165Other))
                        {
                            pdfFormFields.SetField(PDFItems.kneePDFItems[165].Code, PDFItems.kneePDFItems[165].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[168].Code, m.S165Other, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[164].Code, dt, true);
                            sb.Append(m.S165Other);
                            switch (m.S165Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[150].Code, PDFItems.kneePDFItems[150].ExportValue, true);
                                    sb.Append(", Right");
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[161].Code, PDFItems.kneePDFItems[161].ExportValue, true);
                                    sb.Append(", Left");
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[162].Code, PDFItems.kneePDFItems[162].ExportValue, true);
                                    sb.Append(", Bilateral");
                                    break;
                                default:
                                    break;
                            }

                            sb.Append(".");
                        }

                        //pdfFormFields.SetField(PDFItems.kneePDFItems[69].Code, sb.ToString(), true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[69].Code, m.S69, true);


                        bool DoInitialROMLeft = false;
                        bool DoInitialROMRight = false;

                        if ((m.S57Side == "BOTH")
                            || (m.S58Side == "BOTH")
                            || (m.S59Side == "BOTH")
                            || (m.S60Side == "BOTH")
                            || (m.S61Side == "BOTH")
                            || (m.S62Side == "BOTH")
                            || (m.S63Side == "BOTH")
                            || (m.S64Side == "BOTH")
                            || (m.S65Side == "BOTH")
                            || (m.S79Side == "BOTH")
                            || (m.S127Side == "BOTH")
                            || (m.S121Side == "BOTH")
                            || (m.S115Side == "BOTH")
                            || (m.S109Side == "BOTH")
                            || (m.S103Side == "BOTH")
                            || (m.S85Side == "BOTH")
                            || (m.S97Side == "BOTH")
                            || (m.S91Side == "BOTH")
                            || (m.S165Side == "BOTH")
                            )
                        {
                            DoInitialROMLeft = true;
                            DoInitialROMRight = true;
                        }
                        if ((m.S57Side == "LEFT")
                            || (m.S58Side == "LEFT")
                            || (m.S59Side == "LEFT")
                            || (m.S60Side == "LEFT")
                            || (m.S61Side == "LEFT")
                            || (m.S62Side == "LEFT")
                            || (m.S63Side == "LEFT")
                            || (m.S64Side == "LEFT")
                            || (m.S65Side == "LEFT")
                            || (m.S79Side == "LEFT")
                            || (m.S127Side == "LEFT")
                            || (m.S121Side == "LEFT")
                            || (m.S115Side == "LEFT")
                            || (m.S109Side == "LEFT")
                            || (m.S103Side == "LEFT")
                            || (m.S85Side == "LEFT")
                            || (m.S97Side == "LEFT")
                            || (m.S91Side == "LEFT")
                            || (m.S165Side == "LEFT")
                            )
                        {
                            DoInitialROMLeft = true;
                        }
                        if ((m.S57Side == "RIGHT")
                            || (m.S58Side == "RIGHT")
                            || (m.S59Side == "RIGHT")
                            || (m.S60Side == "RIGHT")
                            || (m.S61Side == "RIGHT")
                            || (m.S62Side == "RIGHT")
                            || (m.S63Side == "RIGHT")
                            || (m.S64Side == "RIGHT")
                            || (m.S65Side == "RIGHT")
                            || (m.S79Side == "RIGHT")
                            || (m.S127Side == "RIGHT")
                            || (m.S121Side == "RIGHT")
                            || (m.S115Side == "RIGHT")
                            || (m.S109Side == "RIGHT")
                            || (m.S103Side == "RIGHT")
                            || (m.S85Side == "RIGHT")
                            || (m.S97Side == "RIGHT")
                            || (m.S91Side == "RIGHT")
                            || (m.S165Side == "RIGHT")
                            )
                        {
                            DoInitialROMRight = true;
                        }

                        bool SameInitialROMLeft = false;
                        bool SameInitialROMRight = false;
                        bool SameFinalROMLeft = false;
                        bool SameFinalROMRight = false;

                        if ((DoInitialROMLeft) && (DoInitialROMRight))
                        {
                            // set the RIGHT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[171].Code, m.S171, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[175].Code, m.S175, true);

                            if ((m.S171 == m.S203)
                                && (m.S175 == m.S202)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Set the LEFT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[146].Code, m.S146, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[142].Code, m.S142, true);

                            if ((m.S146 == m.S187)
                                && (m.S142 == m.S188)
                                )
                            {
                                SameInitialROMLeft = true;
                            }


                            // Second test RIGHT
                            // defaults
                            pdfFormFields.SetField(PDFItems.kneePDFItems[204].Code, PDFItems.kneePDFItems[204].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[186].Code, PDFItems.kneePDFItems[186].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[206].Code, PDFItems.kneePDFItems[206].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[184].Code, PDFItems.kneePDFItems[184].ExportValue, true);

                            //                        pdfFormFields.SetField(PDFItems.kneePDFItems[204].Code, PDFItems.kneePDFItems[204].ExportValue, true);
                            //                        if (SameInitialROMRight)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[206].Code, PDFItems.kneePDFItems[168].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[207].Code, PDFItems.kneePDFItems[207].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[203].Code, m.S203, true);
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[202].Code, m.S202, true);

                            //                        }
                            //                        if ((m.S203 == m.S313)
                            //&& (m.S202 == m.S314)
                            //)
                            //                        {
                            //                            SameFinalROMRight = true;
                            //                        }


                            // Second test LEFT
                            //                        pdfFormFields.SetField(PDFItems.kneePDFItems[186].Code, PDFItems.kneePDFItems[186].ExportValue, true);
                            //                        if (SameInitialROMLeft)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[184].Code, PDFItems.kneePDFItems[184].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[183].Code, PDFItems.kneePDFItems[183].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[187].Code, m.S187, true);
                            //                            pdfFormFields.SetField(PDFItems.kneePDFItems[188].Code, m.S188, true);


                            //                        }
                            //                        if ((m.S187 == m.S287)
                            //&& (m.S188 == m.S284)
                            //)
                            //                        {
                            //                            SameFinalROMLeft = true;
                            //                        }

                            // Final test RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[316].Code, PDFItems.kneePDFItems[316].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[315].Code, PDFItems.kneePDFItems[315].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[313].Code, m.S313, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[314].Code, m.S314, true);
                            }

                            // Final test LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[288].Code, PDFItems.kneePDFItems[288].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[289].Code, PDFItems.kneePDFItems[289].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[287].Code, m.S287, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[284].Code, m.S284, true);
                            }

                            // Defaults
                            pdfFormFields.SetField(PDFItems.kneePDFItems[186].Code, PDFItems.kneePDFItems[186].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[200].Code, PDFItems.kneePDFItems[200].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[198].Code, PDFItems.kneePDFItems[198].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[193].Code, PDFItems.kneePDFItems[193].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[195].Code, PDFItems.kneePDFItems[195].ExportValue, true);

                            pdfFormFields.SetField(PDFItems.kneePDFItems[204].Code, PDFItems.kneePDFItems[204].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[210].Code, PDFItems.kneePDFItems[210].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[218].Code, PDFItems.kneePDFItems[218].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[216].Code, PDFItems.kneePDFItems[216].ExportValue, true);

                        }
                        else if (DoInitialROMRight)
                        {
                            // the default for initial LEFT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[146].Code, "140", true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[142].Code, "140", true);

                            // set the values for initial RIGHT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[171].Code, m.S171, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[175].Code, m.S175, true);

                            if ((m.S171 == m.S203)
                                && (m.S175 == m.S202)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Second test for setting RIGHT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[204].Code, PDFItems.kneePDFItems[204].ExportValue, true);
                            if (SameInitialROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[206].Code, PDFItems.kneePDFItems[168].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[207].Code, PDFItems.kneePDFItems[207].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[203].Code, m.S203, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[202].Code, m.S202, true);


                            }
                            if ((m.S203 == m.S313)
    && (m.S202 == m.S314)
    )
                            {
                                SameFinalROMRight = true;
                            }

                            // final RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[316].Code, PDFItems.kneePDFItems[316].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[315].Code, PDFItems.kneePDFItems[315].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[313].Code, m.S313, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[314].Code, m.S314, true);
                            }

                            // set the LEFT repeat to no change
                            pdfFormFields.SetField(PDFItems.kneePDFItems[186].Code, PDFItems.kneePDFItems[186].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[184].Code, PDFItems.kneePDFItems[184].ExportValue, true);

                            // set the LEFT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.kneePDFItems[288].Code, PDFItems.kneePDFItems[288].ExportValue, true);

                            // Defaults
                            pdfFormFields.SetField(PDFItems.kneePDFItems[204].Code, PDFItems.kneePDFItems[204].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[210].Code, PDFItems.kneePDFItems[210].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[218].Code, PDFItems.kneePDFItems[218].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[216].Code, PDFItems.kneePDFItems[216].ExportValue, true);

                        }
                        else if (DoInitialROMLeft)
                        {
                            // the default for initial RIGHT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[130].Code, "140", true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[134].Code, "140", true);

                            // set the initial LEFT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[146].Code, m.S146, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[142].Code, m.S142, true);

                            if ((m.S146 == m.S187)
                                && (m.S142 == m.S188)
                                )
                            {
                                SameInitialROMLeft = true;
                            }

                            // Second test LEFT
                            pdfFormFields.SetField(PDFItems.kneePDFItems[186].Code, PDFItems.kneePDFItems[186].ExportValue, true);
                            if (SameInitialROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[184].Code, PDFItems.kneePDFItems[184].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[183].Code, PDFItems.kneePDFItems[183].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[187].Code, m.S187, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[188].Code, m.S188, true);


                            }
                            if ((m.S187 == m.S287)
    && (m.S188 == m.S284)
    )
                            {
                                SameFinalROMLeft = true;
                            }


                            // final LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[288].Code, PDFItems.kneePDFItems[288].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.kneePDFItems[289].Code, PDFItems.kneePDFItems[289].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[287].Code, m.S287, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[284].Code, m.S284, true);
                            }

                            // set the RIGHT repeat to no change
                            pdfFormFields.SetField(PDFItems.kneePDFItems[204].Code, PDFItems.kneePDFItems[204].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[206].Code, PDFItems.kneePDFItems[206].ExportValue, true);

                            // set the RIGHT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.kneePDFItems[288].Code, PDFItems.kneePDFItems[288].ExportValue, true);

                            // Defaults
                            pdfFormFields.SetField(PDFItems.kneePDFItems[186].Code, PDFItems.kneePDFItems[186].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[200].Code, PDFItems.kneePDFItems[200].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[198].Code, PDFItems.kneePDFItems[198].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[193].Code, PDFItems.kneePDFItems[193].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[195].Code, PDFItems.kneePDFItems[195].ExportValue, true);

                        }

                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S226, m.S226Side, 226, 268, 227, 269);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S270, m.S270Side, 270, 266, 267, 265);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S224, m.S224Side, 224, 263, 228, 264);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S223, m.S223Side, 223, 261, 262, 260);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S278, m.S278Side, 278, 258, 229, 359);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S277, m.S277Side, 277, 256, 257, 255);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S276, m.S276Side, 276, 253, 230, 354);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S275, m.S275Side, 275, 251, 252, 250);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S274, m.S274Side, 274, 248, 231, 249);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S232, m.S232Side, 232, 246, 247, 245);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S244, m.S244Side, 244, 242, 233, 243);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S234, m.S234Side, 234, 240, 241, 239);
                        PdfFill.SetContributingFactorsSection(pdfFormFields, PDFItems.kneePDFItems, m.S238, m.S238Side, 238, 236, 235, 237);


                        if (m.S505)
                        {
                            pdfFormFields.SetField(PDFItems.kneePDFItems[505].Code, PDFItems.kneePDFItems[505].ExportValue, true);
                            SetField_S17ABrace_Knee(m, pdfFormFields);
                            SetField_S17ACrutches_Knee(m, pdfFormFields);
                            SetField_S17ACane_Knee(m, pdfFormFields);
                            SetField_S17AWalker_Knee(m, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.kneePDFItems[506].Code, PDFItems.kneePDFItems[506].ExportValue, true);
                        }

                        bool bS496On = false;
                        if (!m.S462)
                        {
                            pdfFormFields.SetField(PDFItems.kneePDFItems[477].Code, PDFItems.kneePDFItems[477].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[496].Code, PDFItems.kneePDFItems[496].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.kneePDFItems[494].Code, PDFItems.kneePDFItems[494].ExportValue, true);
                            bS496On = true;
                        }
                        else
                        {
                            if (m.S11ASurgeryType == "BOTH")
                            {

                                pdfFormFields.SetField(PDFItems.kneePDFItems[485].Code, PDFItems.kneePDFItems[485].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[488].Code, PDFItems.kneePDFItems[488].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[487].Code, PDFItems.kneePDFItems[487].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[483].Code, m.surgeryDate, true);

                                pdfFormFields.SetField(PDFItems.kneePDFItems[512].Code, PDFItems.kneePDFItems[512].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[509].Code, PDFItems.kneePDFItems[509].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[510].Code, PDFItems.kneePDFItems[510].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[514].Code, m.surgeryDate, true);

                                pdfFormFields.SetField(PDFItems.kneePDFItems[462].Code, PDFItems.kneePDFItems[462].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[465].Code, PDFItems.kneePDFItems[465].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[466].Code, PDFItems.kneePDFItems[466].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[467].Code, PDFItems.kneePDFItems[467].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[468].Code, PDFItems.kneePDFItems[468].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[490].Code, PDFItems.kneePDFItems[490].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[479].Code, "meniscectomy", true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[482].Code, m.surgeryDate, true);

                                pdfFormFields.SetField(PDFItems.kneePDFItems[462].Code, PDFItems.kneePDFItems[462].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[472].Code, PDFItems.kneePDFItems[472].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[473].Code, PDFItems.kneePDFItems[473].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[474].Code, PDFItems.kneePDFItems[474].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[475].Code, PDFItems.kneePDFItems[475].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[570].Code, PDFItems.kneePDFItems[570].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[518].Code, "meniscectomy", true);
                                pdfFormFields.SetField(PDFItems.kneePDFItems[515].Code, m.surgeryDate, true);

                                if (!string.IsNullOrEmpty(m.surgeryDate))
                                {
                                    if (!bS496On)
                                    {
                                        pdfFormFields.SetField(PDFItems.kneePDFItems[495].Code, PDFItems.kneePDFItems[495].ExportValue, true);
                                    }
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[498].Code, PDFItems.kneePDFItems[498].ExportValue, true);
                                }

                            }
                            else if (m.S11ASurgeryType == "KNEE")
                            {
                                if ((m.S11ASide == "RIGHT") || (m.S11ASide == "BOTH"))
                                {
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[485].Code, PDFItems.kneePDFItems[485].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[488].Code, PDFItems.kneePDFItems[488].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[487].Code, PDFItems.kneePDFItems[487].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[483].Code, m.surgeryDate, true);

                                }
                                if ((m.S11ASide == "LEFT") || (m.S11ASide == "BOTH"))
                                {
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[512].Code, PDFItems.kneePDFItems[512].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[509].Code, PDFItems.kneePDFItems[509].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[510].Code, PDFItems.kneePDFItems[510].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[514].Code, m.surgeryDate, true);

                                }
                            }
                            else if (m.S11ASurgeryType == "MENISCECTOMY")
                            {
                                if ((m.S11ASide == "RIGHT") || (m.S11ASide == "BOTH"))
                                {
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[462].Code, PDFItems.kneePDFItems[462].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[465].Code, PDFItems.kneePDFItems[465].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[466].Code, PDFItems.kneePDFItems[466].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[467].Code, PDFItems.kneePDFItems[467].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[468].Code, PDFItems.kneePDFItems[468].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[490].Code, PDFItems.kneePDFItems[490].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[479].Code, "meniscectomy", true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[482].Code, m.surgeryDate, true);
                                }
                                if ((m.S11ASide == "LEFT") || (m.S11ASide == "BOTH"))
                                {
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[462].Code, PDFItems.kneePDFItems[462].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[472].Code, PDFItems.kneePDFItems[472].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[473].Code, PDFItems.kneePDFItems[473].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[474].Code, PDFItems.kneePDFItems[474].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[475].Code, PDFItems.kneePDFItems[475].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[570].Code, PDFItems.kneePDFItems[570].ExportValue, true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[518].Code, "meniscectomy", true);
                                    pdfFormFields.SetField(PDFItems.kneePDFItems[515].Code, m.surgeryDate, true);
                                }
                            }
                        }

                        pdfFormFields.SetField(PDFItems.kneePDFItems[130].Code, m.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[131].Code, m.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.kneePDFItems[132].Code, m.VarianceFunctionLossWriteIn, true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.kneePDFItems[51].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }



                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        private void SetField_S17ABrace_Knee(KneeModel m, AcroFields pdfFormFields)
        {
            if (m.S519)
            {
                pdfFormFields.SetField(PDFItems.kneePDFItems[519].Code, PDFItems.kneePDFItems[519].ExportValue, true);
                switch (m.S519Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[537].Code, PDFItems.kneePDFItems[537].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[541].Code, PDFItems.kneePDFItems[541].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[542].Code, PDFItems.kneePDFItems[542].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17ACrutches_Knee(KneeModel m, AcroFields pdfFormFields)
        {
            if (m.S531)
            {
                pdfFormFields.SetField(PDFItems.kneePDFItems[531].Code, PDFItems.kneePDFItems[531].ExportValue, true);
                switch (m.S519Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[530].Code, PDFItems.kneePDFItems[530].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[529].Code, PDFItems.kneePDFItems[529].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[528].Code, PDFItems.kneePDFItems[528].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17ACane_Knee(KneeModel m, AcroFields pdfFormFields)
        {
            if (m.S520)
            {
                pdfFormFields.SetField(PDFItems.kneePDFItems[520].Code, PDFItems.kneePDFItems[520].ExportValue, true);
                switch (m.S519Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[525].Code, PDFItems.kneePDFItems[525].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[526].Code, PDFItems.kneePDFItems[526].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[527].Code, PDFItems.kneePDFItems[527].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S17AWalker_Knee(KneeModel m, AcroFields pdfFormFields)
        {
            if (m.S524)
            {
                pdfFormFields.SetField(PDFItems.kneePDFItems[524].Code, PDFItems.kneePDFItems[524].ExportValue, true);
                switch (m.S519Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[523].Code, PDFItems.kneePDFItems[523].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[522].Code, PDFItems.kneePDFItems[522].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.kneePDFItems[521].Code, PDFItems.kneePDFItems[521].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }




        /**************************************************************
          * Hip Form
          * 
          *************************************************************/
        public byte[] Hip(string template, HipModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // Defaults
                        //pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[1]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Physical limitations, loss of strength, soreness, and pain. See section 6A.", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo6[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Right_Loss[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].Left_Loss[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].RAnkylosis4[0]", "1", true);
                        //pdfFormFields.SetField("form1[0].#subform[5].Left_Reduction[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo10[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo20[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo23[0]", "2", true);


                        pdfFormFields.SetField(PDFItems.hipPDFItems[77].Code, m.NameOfPatient, true);
                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[76].Code, ssn.ToString(), true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[76].Code, m.SocialSecurity, true);
                        }

                        pdfFormFields.SetField(PDFItems.hipPDFItems[54].Code, PDFItems.hipPDFItems[54].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[61].Code, PDFItems.hipPDFItems[61].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[62].Code, PDFItems.hipPDFItems[62].ExportValue, true);

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;
                        //string S79 = string.Empty;

                        if (m.S67)
                        {
                            diagnosis = "Osteoarthritis, Hip";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[67].Code, PDFItems.hipPDFItems[67].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[51].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[52].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S67Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[83].Code, PDFItems.hipPDFItems[83].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[82].Code, PDFItems.hipPDFItems[82].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[81].Code, PDFItems.hipPDFItems[81].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }
                        if (m.S68)
                        {
                            diagnosis = "Hip Joint Replacement";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[68].Code, PDFItems.hipPDFItems[68].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[50].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[49].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S68Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[1].Code, PDFItems.hipPDFItems[1].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[47].Code, PDFItems.hipPDFItems[47].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[48].Code, PDFItems.hipPDFItems[48].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }
                        if (m.S69)
                        {
                            diagnosis = "Trochanteris Pain Syndrome";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[69].Code, PDFItems.hipPDFItems[69].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[42].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[43].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S69Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[46].Code, PDFItems.hipPDFItems[46].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[45].Code, PDFItems.hipPDFItems[45].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[44].Code, PDFItems.hipPDFItems[44].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }
                        if (m.S70)
                        {
                            diagnosis = "Femoral Acetabular Impingement Syndrome";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[70].Code, PDFItems.hipPDFItems[70].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[41].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[40].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S70Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[2].Code, PDFItems.hipPDFItems[2].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[38].Code, PDFItems.hipPDFItems[38].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[39].Code, PDFItems.hipPDFItems[39].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }
                        if (m.S71)
                        {
                            diagnosis = "Liopsoas Tendinitis";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[71].Code, PDFItems.hipPDFItems[71].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[33].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[34].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S71Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[37].Code, PDFItems.hipPDFItems[37].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[36].Code, PDFItems.hipPDFItems[36].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[36].Code, PDFItems.hipPDFItems[35].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }
                        if (m.S72)
                        {
                            diagnosis = "Femoral Neck Stress Fracture";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[72].Code, PDFItems.hipPDFItems[72].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[32].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[31].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S72Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[3].Code, PDFItems.hipPDFItems[3].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[29].Code, PDFItems.hipPDFItems[29].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[30].Code, PDFItems.hipPDFItems[30].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }
                        if (m.S73)
                        {
                            diagnosis = "Avascular Necrosis, Hip";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[73].Code, PDFItems.hipPDFItems[73].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[24].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[25].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S73Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[28].Code, PDFItems.hipPDFItems[28].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[27].Code, PDFItems.hipPDFItems[27].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[26].Code, PDFItems.hipPDFItems[26].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }
                        if (m.S74)
                        {
                            diagnosis = "Ankylosis Of Hip Joint";
                            pdfFormFields.SetField(PDFItems.hipPDFItems[74].Code, PDFItems.hipPDFItems[74].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[23].Code, dt, true);
                            if (ICDCodes.hipICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[22].Code, icdcode.RefNumber, true);
                            }
                            //S79 += diagnosis;
                            switch (m.S74Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[4].Code, PDFItems.hipPDFItems[4].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[20].Code, PDFItems.hipPDFItems[20].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[21].Code, PDFItems.hipPDFItems[21].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S79 += ".  ";
                        }

                        if (!string.IsNullOrEmpty(m.S75Other))
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[75].Code, PDFItems.hipPDFItems[75].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[86].Code, m.S75Other, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[19].Code, dt, true);
                            //S79 += m.S75Other;
                            switch (m.S75Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[5].Code, PDFItems.hipPDFItems[5].ExportValue, true);
                                    //S79 += ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[16].Code, PDFItems.hipPDFItems[16].ExportValue, true);
                                    //S79 += ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.hipPDFItems[17].Code, PDFItems.hipPDFItems[17].ExportValue, true);
                                    //S79 += ", Bilateral";
                                    break;
                                default:
                                    break;
                            }

                            //S79 += ".  ";
                        }

                        //pdfFormFields.SetField(PDFItems.hipPDFItems[79].Code, S79, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[79].Code, m.S79, true);

                        bool DoInitialROMLeft = false;
                        bool DoInitialROMRight = false;

                        if ((m.S67Side == "BOTH")
                            || (m.S68Side == "BOTH")
                            || (m.S69Side == "BOTH")
                            || (m.S70Side == "BOTH")
                            || (m.S71Side == "BOTH")
                            || (m.S72Side == "BOTH")
                            || (m.S73Side == "BOTH")
                            || (m.S74Side == "BOTH")
                            )
                        {
                            DoInitialROMLeft = true;
                            DoInitialROMRight = true;
                        }
                        if ((m.S67Side == "LEFT")
                            || (m.S68Side == "LEFT")
                            || (m.S69Side == "LEFT")
                            || (m.S70Side == "LEFT")
                            || (m.S71Side == "LEFT")
                            || (m.S72Side == "LEFT")
                            || (m.S73Side == "LEFT")
                            || (m.S74Side == "LEFT")
                            )
                        {
                            DoInitialROMLeft = true;
                        }
                        if ((m.S67Side == "RIGHT")
                            || (m.S68Side == "RIGHT")
                            || (m.S69Side == "RIGHT")
                            || (m.S70Side == "RIGHT")
                            || (m.S71Side == "RIGHT")
                            || (m.S72Side == "RIGHT")
                            || (m.S73Side == "RIGHT")
                            || (m.S74Side == "RIGHT")
                            )
                        {
                            DoInitialROMRight = true;
                        }

                        bool SameInitialROMLeft = false;
                        bool SameInitialROMRight = false;
                        bool SameFinalROMLeft = false;
                        bool SameFinalROMRight = false;

                        if ((DoInitialROMLeft) && (DoInitialROMRight))
                        {
                            // set the RIGHT
                            pdfFormFields.SetField(PDFItems.hipPDFItems[123].Code, m.S123, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[113].Code, m.S113, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[114].Code, m.S114, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[117].Code, m.S117, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[103].Code, m.S103, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[109].Code, m.S109, true);

                            if ((m.S123 == m.S159)
                                && (m.S113 == m.S158)
                                && (m.S114 == m.S160)
                                && (m.S117 == m.S161)
                                && (m.S103 == m.S167)
                                && (m.S109 == m.S168)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Set the LEFT
                            pdfFormFields.SetField(PDFItems.hipPDFItems[204].Code, PDFItems.hipPDFItems[204].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[186].Code, PDFItems.hipPDFItems[186].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[206].Code, PDFItems.hipPDFItems[206].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[184].Code, PDFItems.hipPDFItems[184].ExportValue, true);


                            //                        pdfFormFields.SetField(PDFItems.hipPDFItems[154].Code, m.S154, true);
                            //                        pdfFormFields.SetField(PDFItems.hipPDFItems[144].Code, m.S144, true);
                            //                        pdfFormFields.SetField(PDFItems.hipPDFItems[145].Code, m.S145, true);
                            //                        pdfFormFields.SetField(PDFItems.hipPDFItems[148].Code, m.S148, true);
                            //                        pdfFormFields.SetField(PDFItems.hipPDFItems[134].Code, m.S134, true);
                            //                        pdfFormFields.SetField(PDFItems.hipPDFItems[140].Code, m.S140, true);

                            //                        if ((m.S154 == m.S197)
                            //                            && (m.S144 == m.S198)
                            //                            && (m.S145 == m.S191)
                            //                            && (m.S148 == m.S188)
                            //                            && (m.S134 == m.S189)
                            //                            && (m.S140 == m.S190)
                            //                            )
                            //                        {
                            //                            SameInitialROMLeft = true;
                            //                        }


                            //                        // Second test
                            //                        pdfFormFields.SetField(PDFItems.hipPDFItems[162].Code, PDFItems.hipPDFItems[162].ExportValue, true);
                            //                        if (SameInitialROMRight)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[164].Code, PDFItems.hipPDFItems[164].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[165].Code, PDFItems.hipPDFItems[165].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[159].Code, m.S159, true);
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[158].Code, m.S158, true);
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[160].Code, m.S160, true);
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[161].Code, m.S161, true);
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[167].Code, m.S167, true);
                            //                            pdfFormFields.SetField(PDFItems.hipPDFItems[168].Code, m.S168, true);


                            //                        }
                            //                        if ((m.S159 == m.S282)
                            //&& (m.S158 == m.S283)
                            //&& (m.S160 == m.S272)
                            //&& (m.S161 == m.S278)
                            //&& (m.S167 == m.S273)
                            //&& (m.S168 == m.S275)
                            //)
                            //                        {
                            //                            SameFinalROMRight = true;
                            //                        }

                            pdfFormFields.SetField(PDFItems.hipPDFItems[196].Code, PDFItems.hipPDFItems[196].ExportValue, true);
                            if (SameInitialROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[194].Code, PDFItems.hipPDFItems[194].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[193].Code, PDFItems.hipPDFItems[193].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[197].Code, m.S197, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[198].Code, m.S198, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[191].Code, m.S191, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[188].Code, m.S188, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[189].Code, m.S189, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[190].Code, m.S190, true);


                            }
                            if ((m.S197 == m.S322)
    && (m.S198 == m.S313)
    && (m.S191 == m.S311)
    && (m.S188 == m.S312)
    && (m.S189 == m.S318)
    && (m.S190 == m.S317)
    )
                            {
                                SameFinalROMLeft = true;
                            }

                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[285].Code, PDFItems.hipPDFItems[285].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[284].Code, PDFItems.hipPDFItems[284].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[282].Code, m.S282, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[283].Code, m.S283, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[272].Code, m.S272, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[278].Code, m.S278, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[273].Code, m.S273, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[275].Code, m.S275, true);
                            }

                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[323].Code, PDFItems.hipPDFItems[323].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[324].Code, PDFItems.hipPDFItems[324].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[322].Code, m.S322, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[313].Code, m.S313, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[311].Code, m.S311, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[312].Code, m.S312, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[318].Code, m.S318, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[317].Code, m.S317, true);
                            }

                        }
                        else if (DoInitialROMRight)
                        {
                            // the default for initial LEFT
                            pdfFormFields.SetField(PDFItems.hipPDFItems[154].Code, "125", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[144].Code, "30", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[145].Code, "45", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[148].Code, "25", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[134].Code, "60", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[140].Code, "40", true);

                            pdfFormFields.SetField(PDFItems.hipPDFItems[123].Code, m.S123, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[113].Code, m.S113, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[114].Code, m.S114, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[117].Code, m.S117, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[103].Code, m.S103, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[109].Code, m.S109, true);

                            if ((m.S123 == m.S159)
                                && (m.S113 == m.S158)
                                && (m.S114 == m.S160)
                                && (m.S117 == m.S161)
                                && (m.S103 == m.S167)
                                && (m.S109 == m.S168)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            pdfFormFields.SetField(PDFItems.hipPDFItems[162].Code, PDFItems.hipPDFItems[162].ExportValue, true);
                            if (SameInitialROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[164].Code, PDFItems.hipPDFItems[164].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[165].Code, PDFItems.hipPDFItems[165].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[159].Code, m.S159, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[158].Code, m.S158, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[160].Code, m.S160, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[161].Code, m.S161, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[167].Code, m.S167, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[168].Code, m.S168, true);


                            }
                            if ((m.S159 == m.S282)
    && (m.S158 == m.S283)
    && (m.S160 == m.S272)
    && (m.S161 == m.S278)
    && (m.S167 == m.S273)
    && (m.S168 == m.S275)
    )
                            {
                                SameFinalROMRight = true;
                            }

                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[285].Code, PDFItems.hipPDFItems[285].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[284].Code, PDFItems.hipPDFItems[284].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[282].Code, m.S282, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[283].Code, m.S283, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[272].Code, m.S272, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[278].Code, m.S278, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[273].Code, m.S273, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[275].Code, m.S275, true);
                            }

                            // set the LEFT repeat to no change
                            pdfFormFields.SetField(PDFItems.hipPDFItems[196].Code, PDFItems.hipPDFItems[196].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[194].Code, PDFItems.hipPDFItems[194].ExportValue, true);

                            // set the LEFT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.hipPDFItems[323].Code, PDFItems.hipPDFItems[323].ExportValue, true);

                        }
                        else if (DoInitialROMLeft)
                        {
                            // set the default for initial Right
                            pdfFormFields.SetField(PDFItems.hipPDFItems[123].Code, "125", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[113].Code, "30", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[114].Code, "45", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[117].Code, "25", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[103].Code, "60", true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[109].Code, "40", true);

                            pdfFormFields.SetField(PDFItems.hipPDFItems[154].Code, m.S154, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[144].Code, m.S144, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[145].Code, m.S145, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[148].Code, m.S148, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[134].Code, m.S134, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[140].Code, m.S140, true);

                            if ((m.S154 == m.S197)
                                && (m.S144 == m.S198)
                                && (m.S145 == m.S191)
                                && (m.S148 == m.S188)
                                && (m.S134 == m.S189)
                                && (m.S140 == m.S190)
                                )
                            {
                                SameInitialROMLeft = true;
                            }

                            pdfFormFields.SetField(PDFItems.hipPDFItems[196].Code, PDFItems.hipPDFItems[196].ExportValue, true);
                            if (SameInitialROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[194].Code, PDFItems.hipPDFItems[194].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[193].Code, PDFItems.hipPDFItems[193].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[197].Code, m.S197, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[198].Code, m.S198, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[191].Code, m.S191, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[188].Code, m.S188, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[189].Code, m.S189, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[190].Code, m.S190, true);


                            }
                            if ((m.S197 == m.S322)
    && (m.S198 == m.S313)
    && (m.S191 == m.S311)
    && (m.S188 == m.S312)
    && (m.S189 == m.S318)
    && (m.S190 == m.S317)
    )
                            {
                                SameFinalROMLeft = true;
                            }

                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[323].Code, PDFItems.hipPDFItems[323].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.hipPDFItems[324].Code, PDFItems.hipPDFItems[324].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[322].Code, m.S322, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[313].Code, m.S313, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[311].Code, m.S311, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[312].Code, m.S312, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[318].Code, m.S318, true);
                                pdfFormFields.SetField(PDFItems.hipPDFItems[317].Code, m.S317, true);
                            }

                            // set the RIGHT repeat to no change
                            pdfFormFields.SetField(PDFItems.hipPDFItems[162].Code, PDFItems.hipPDFItems[162].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[164].Code, PDFItems.hipPDFItems[164].ExportValue, true);

                            // set the RIGHT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.hipPDFItems[285].Code, PDFItems.hipPDFItems[285].ExportValue, true);

                        }


                        if (m.S262)
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[262].Code, PDFItems.hipPDFItems[262].ExportValue, true);
                        }
                        if (m.S216)
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[216].Code, PDFItems.hipPDFItems[216].ExportValue, true);
                        }
                        SetContributingFactorsHip(pdfFormFields, m.S217, m.S217Side, 217, 259, 218, 260);
                        SetContributingFactorsHip(pdfFormFields, m.S261, m.S261Side, 261, 257, 258, 256);
                        SetContributingFactorsHip(pdfFormFields, m.S215, m.S215Side, 215, 254, 219, 255);
                        SetContributingFactorsHip(pdfFormFields, m.S214, m.S214Side, 214, 252, 253, 251);
                        SetContributingFactorsHip(pdfFormFields, m.S269, m.S269Side, 269, 249, 220, 250);
                        SetContributingFactorsHip(pdfFormFields, m.S268, m.S268Side, 268, 247, 248, 246);
                        SetContributingFactorsHip(pdfFormFields, m.S267, m.S267Side, 267, 244, 221, 245);
                        SetContributingFactorsHip(pdfFormFields, m.S266, m.S266Side, 266, 242, 243, 241);
                        SetContributingFactorsHip(pdfFormFields, m.S265, m.S265Side, 265, 239, 222, 240);
                        SetContributingFactorsHip(pdfFormFields, m.S223, m.S223Side, 223, 237, 238, 236);
                        SetContributingFactorsHip(pdfFormFields, m.S235, m.S235Side, 235, 233, 224, 234);
                        SetContributingFactorsHip(pdfFormFields, m.S225, m.S225Side, 225, 231, 232, 230);
                        SetContributingFactorsHip(pdfFormFields, m.S229, m.S229Side, 229, 227, 226, 228);
                        if (m.S264)
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[263].Code, m.S264Other, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[264].Code, PDFItems.hipPDFItems[264].ExportValue, true);
                        }

                        if (m.S416)
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[416].Code, PDFItems.hipPDFItems[416].ExportValue, true);
                            SetField_S12ABrace_Hip(m, pdfFormFields);
                            SetField_S12ACrutches_Hip(m, pdfFormFields);
                            SetField_S12ACane_Hip(m, pdfFormFields);
                            SetField_S12AWalker_Hip(m, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[417].Code, PDFItems.hipPDFItems[417].ExportValue, true);
                        }

                        pdfFormFields.SetField(PDFItems.hipPDFItems[295].Code, "5", true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[293].Code, "5", true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[294].Code, "5", true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[334].Code, "5", true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[326].Code, "5", true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[327].Code, "5", true);

                        pdfFormFields.SetField(PDFItems.hipPDFItems[299].Code, PDFItems.hipPDFItems[299].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[332].Code, PDFItems.hipPDFItems[332].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[343].Code, PDFItems.hipPDFItems[343].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[405].Code, PDFItems.hipPDFItems[405].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[407].Code, PDFItems.hipPDFItems[407].ExportValue, true);

                        pdfFormFields.SetField(PDFItems.hipPDFItems[479].Code, PDFItems.hipPDFItems[479].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[477].Code, "Sedentary occupation highly suggested due to flareups and severity of symptoms.", true);

                        pdfFormFields.SetField(PDFItems.hipPDFItems[106].Code, PDFItems.hipPDFItems[106].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[137].Code, PDFItems.hipPDFItems[137].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[162].Code, PDFItems.hipPDFItems[162].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[169].Code, PDFItems.hipPDFItems[169].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[173].Code, PDFItems.hipPDFItems[173].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[199].Code, PDFItems.hipPDFItems[199].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[196].Code, PDFItems.hipPDFItems[196].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[212].Code, PDFItems.hipPDFItems[212].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[184].Code, PDFItems.hipPDFItems[184].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[209].Code, PDFItems.hipPDFItems[209].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[178].Code, PDFItems.hipPDFItems[178].ExportValue, true);

                        if ((DoInitialROMLeft) && (DoInitialROMRight))
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[179].Code, PDFItems.hipPDFItems[179].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[181].Code, PDFItems.hipPDFItems[181].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[204].Code, PDFItems.hipPDFItems[204].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[202].Code, PDFItems.hipPDFItems[202].ExportValue, true);
                        }
                        else if (DoInitialROMRight)
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[194].Code, PDFItems.hipPDFItems[194].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[201].Code, PDFItems.hipPDFItems[201].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[323].Code, PDFItems.hipPDFItems[323].ExportValue, true);

                            pdfFormFields.SetField(PDFItems.hipPDFItems[179].Code, PDFItems.hipPDFItems[179].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[181].Code, PDFItems.hipPDFItems[181].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[201].Code, PDFItems.hipPDFItems[201].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[203].Code, PDFItems.hipPDFItems[203].ExportValue, true);


                        }
                        else if (DoInitialROMLeft)
                        {
                            pdfFormFields.SetField(PDFItems.hipPDFItems[164].Code, PDFItems.hipPDFItems[164].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[180].Code, PDFItems.hipPDFItems[180].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[285].Code, PDFItems.hipPDFItems[285].ExportValue, true);

                            pdfFormFields.SetField(PDFItems.hipPDFItems[180].Code, PDFItems.hipPDFItems[180].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[182].Code, PDFItems.hipPDFItems[182].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[204].Code, PDFItems.hipPDFItems[204].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.hipPDFItems[202].Code, PDFItems.hipPDFItems[202].ExportValue, true);

                        }

                        pdfFormFields.SetField(PDFItems.hipPDFItems[90].Code, m.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[91].Code, m.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.hipPDFItems[92].Code, m.VarianceFunctionLossWriteIn, true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.hipPDFItems[54].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }


                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }

        private void SetContributingFactorsHip(AcroFields pdfFormFields, bool f, string side, int indCheck, int indRight, int indLeft, int indBoth)
        {
            if (f)
            {
                pdfFormFields.SetField(PDFItems.hipPDFItems[indCheck].Code, PDFItems.hipPDFItems[indCheck].ExportValue, true);
                switch (side)
                {
                    case "RIGHT":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[indRight].Code, PDFItems.hipPDFItems[indRight].ExportValue, true);
                        break;
                    case "LEFT":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[indLeft].Code, PDFItems.hipPDFItems[indLeft].ExportValue, true);
                        break;
                    case "BOTH":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[indBoth].Code, PDFItems.hipPDFItems[indBoth].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }

        }

        private void SetField_S12ABrace_Hip(HipModel m, AcroFields pdfFormFields)
        {
            if (m.S418)
            {
                pdfFormFields.SetField(PDFItems.hipPDFItems[418].Code, PDFItems.hipPDFItems[418].ExportValue, true);
                switch (m.S418Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[436].Code, PDFItems.hipPDFItems[436].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[440].Code, PDFItems.hipPDFItems[440].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[441].Code, PDFItems.hipPDFItems[441].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S12ACrutches_Hip(HipModel m, AcroFields pdfFormFields)
        {
            if (m.S430)
            {
                pdfFormFields.SetField(PDFItems.hipPDFItems[430].Code, PDFItems.hipPDFItems[430].ExportValue, true);

                switch (m.S430Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[429].Code, PDFItems.hipPDFItems[429].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[428].Code, PDFItems.hipPDFItems[428].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[427].Code, PDFItems.hipPDFItems[427].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S12ACane_Hip(HipModel m, AcroFields pdfFormFields)
        {
            if (m.S419)
            {
                pdfFormFields.SetField(PDFItems.hipPDFItems[419].Code, PDFItems.hipPDFItems[419].ExportValue, true);

                switch (m.S419Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[424].Code, PDFItems.hipPDFItems[424].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[425].Code, PDFItems.hipPDFItems[425].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[426].Code, PDFItems.hipPDFItems[426].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetField_S12AWalker_Hip(HipModel m, AcroFields pdfFormFields)
        {
            if (m.S423)
            {
                pdfFormFields.SetField(PDFItems.hipPDFItems[423].Code, PDFItems.hipPDFItems[423].ExportValue, true);

                switch (m.S423Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[422].Code, PDFItems.hipPDFItems[422].ExportValue, true);
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[421].Code, PDFItems.hipPDFItems[421].ExportValue, true);
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.hipPDFItems[420].Code, PDFItems.hipPDFItems[420].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }
        /**************************************************************
          * Elbow Form
          * 
          *************************************************************/
        public byte[] Elbow(string template, ElbowModel m)
        {
            byte[] form = null;

            try
            {
                PdfReader pdfReader = new PdfReader(template);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        // Defaults
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[1]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.", true);
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Physical limitations, loss of strength, soreness, and pain. See Section 6A.", true);
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Pain1[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Tender[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Tender[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength2[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength2[0]", "5", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis4[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis4[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo10[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo13[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo18[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo19[0]", "2", true);
                        //pdfFormFields.SetField("form1[0].#subform[6].Residuals[6]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo22[1]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo23[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo24[0]", "2", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Painful[0]", "1", true);
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss[0]", "1", true);

                        // correction defaults
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[146].Code, PDFItems.elbowPDFItems[146].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[297].Code, PDFItems.elbowPDFItems[297].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[264].Code, PDFItems.elbowPDFItems[264].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[421].Code, PDFItems.elbowPDFItems[421].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[423].Code, PDFItems.elbowPDFItems[423].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[458].Code, PDFItems.elbowPDFItems[458].ExportValue, true);


                        pdfFormFields.SetField(PDFItems.elbowPDFItems[83].Code, m.NameOfPatient, true);
                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[82].Code, ssn.ToString(), true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[82].Code, m.SocialSecurity, true);
                        }

                        pdfFormFields.SetField(PDFItems.elbowPDFItems[59].Code, PDFItems.elbowPDFItems[59].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[66].Code, PDFItems.elbowPDFItems[66].ExportValue, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[67].Code, PDFItems.elbowPDFItems[67].ExportValue, true);

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;
                        //string S85 = string.Empty;

                        if (m.S72)
                        {
                            diagnosis = "Olecranon Bursitis";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[72].Code, PDFItems.elbowPDFItems[72].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[56].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[57].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S72Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[89].Code, PDFItems.elbowPDFItems[89].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[88].Code, PDFItems.elbowPDFItems[88].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[87].Code, PDFItems.elbowPDFItems[87].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S73)
                        {
                            diagnosis = "Tricep Tendinitis";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[73].Code, PDFItems.elbowPDFItems[73].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[55].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[54].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S73Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[1].Code, PDFItems.elbowPDFItems[1].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[52].Code, PDFItems.elbowPDFItems[52].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[53].Code, PDFItems.elbowPDFItems[53].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S74)
                        {
                            diagnosis = "Lateral Epicondylitis";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[74].Code, PDFItems.elbowPDFItems[74].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[47].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[48].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S74Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[51].Code, PDFItems.elbowPDFItems[51].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[50].Code, PDFItems.elbowPDFItems[50].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[49].Code, PDFItems.elbowPDFItems[49].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S75)
                        {
                            diagnosis = "Medial Epicondylitis";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[75].Code, PDFItems.elbowPDFItems[75].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[46].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[45].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S75Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[2].Code, PDFItems.elbowPDFItems[2].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[43].Code, PDFItems.elbowPDFItems[43].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[44].Code, PDFItems.elbowPDFItems[44].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S76)
                        {
                            diagnosis = "Instability";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[76].Code, PDFItems.elbowPDFItems[76].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[38].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[39].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S76Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[42].Code, PDFItems.elbowPDFItems[42].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[41].Code, PDFItems.elbowPDFItems[41].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[40].Code, PDFItems.elbowPDFItems[40].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S77)
                        {
                            diagnosis = "Dislocation, Elbow";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[77].Code, PDFItems.elbowPDFItems[77].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[37].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[36].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S77Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[3].Code, PDFItems.elbowPDFItems[3].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[34].Code, PDFItems.elbowPDFItems[34].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    //S85+= ", Bilateral";
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[35].Code, PDFItems.elbowPDFItems[35].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S78)
                        {
                            diagnosis = "Osteoarthritis, Elbow";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[78].Code, PDFItems.elbowPDFItems[78].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[29].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[30].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S78Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[33].Code, PDFItems.elbowPDFItems[33].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[32].Code, PDFItems.elbowPDFItems[32].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[31].Code, PDFItems.elbowPDFItems[31].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S79)
                        {
                            diagnosis = "Total Elbow Arthroplasty";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[79].Code, PDFItems.elbowPDFItems[79].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[28].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[27].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S79Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[4].Code, PDFItems.elbowPDFItems[4].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[25].Code, PDFItems.elbowPDFItems[25].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[26].Code, PDFItems.elbowPDFItems[26].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }
                        if (m.S80)
                        {
                            diagnosis = "Ankylosis Of Elbow Joint";
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[80].Code, PDFItems.elbowPDFItems[80].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[20].Code, dt, true);
                            if (ICDCodes.elbowICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[21].Code, icdcode.RefNumber, true);
                            }
                            //S85+= diagnosis;
                            switch (m.S80Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[24].Code, PDFItems.elbowPDFItems[24].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[23].Code, PDFItems.elbowPDFItems[23].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[22].Code, PDFItems.elbowPDFItems[22].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }
                            //S85+= ".  ";
                        }

                        if (!string.IsNullOrEmpty(m.S81Other))
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[81].Code, PDFItems.elbowPDFItems[81].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[92].Code, m.S81Other, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[19].Code, dt, true);
                            //S85+= m.S81Other;
                            switch (m.S81Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[5].Code, PDFItems.elbowPDFItems[5].ExportValue, true);
                                    //S85+= ", Right";
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[16].Code, PDFItems.elbowPDFItems[16].ExportValue, true);
                                    //S85+= ", Left";
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[17].Code, PDFItems.elbowPDFItems[17].ExportValue, true);
                                    //S85+= ", Bilateral";
                                    break;
                                default:
                                    break;
                            }

                            //S85+= ".";
                        }

                        //pdfFormFields.SetField(PDFItems.elbowPDFItems[85].Code, S85, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[85].Code, m.S85, true);

                        bool DoInitialROMLeft = false;
                        bool DoInitialROMRight = false;

                        if ((m.S72Side == "BOTH")
                            || (m.S73Side == "BOTH")
                            || (m.S74Side == "BOTH")
                            || (m.S75Side == "BOTH")
                            || (m.S76Side == "BOTH")
                            || (m.S77Side == "BOTH")
                            || (m.S78Side == "BOTH")
                            || (m.S79Side == "BOTH")
                            || (m.S80Side == "BOTH")
                            )
                        {
                            DoInitialROMLeft = true;
                            DoInitialROMRight = true;
                        }

                        if ((m.S72Side == "LEFT")
                            || (m.S73Side == "LEFT")
                            || (m.S74Side == "LEFT")
                            || (m.S75Side == "LEFT")
                            || (m.S76Side == "LEFT")
                            || (m.S77Side == "LEFT")
                            || (m.S78Side == "LEFT")
                            || (m.S79Side == "LEFT")
                            || (m.S80Side == "LEFT")
                            )
                        {
                            DoInitialROMLeft = true;
                        }
                        if ((m.S72Side == "RIGHT")
                            || (m.S73Side == "RIGHT")
                            || (m.S74Side == "RIGHT")
                            || (m.S75Side == "RIGHT")
                            || (m.S76Side == "RIGHT")
                            || (m.S77Side == "RIGHT")
                            || (m.S78Side == "RIGHT")
                            || (m.S79Side == "RIGHT")
                            || (m.S80Side == "RIGHT")
                            )
                        {
                            DoInitialROMRight = true;
                        }

                        bool SameInitialROMLeft = false;
                        bool SameInitialROMRight = false;
                        bool SameFinalROMLeft = false;
                        bool SameFinalROMRight = false;

                        if ((DoInitialROMLeft) && (DoInitialROMRight))
                        {
                            // set the RIGHT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[130].Code, m.S130, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[134].Code, m.S134, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[137].Code, m.S137, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[140].Code, m.S140, true);

                            if ((m.S130 == m.S170)
                                && (m.S134 == m.S169)
                                && (m.S137 == m.S171)
                                && (m.S140 == m.S172)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Set the LEFT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[122].Code, m.S122, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[112].Code, m.S112, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[113].Code, m.S113, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[116].Code, m.S116, true);

                            if ((m.S122 == m.S154)
                                && (m.S112 == m.S155)
                                && (m.S113 == m.S148)
                                && (m.S116 == m.S147)
                                )
                            {
                                SameInitialROMLeft = true;
                            }


                            // Second test RIGHT
                            // default checkboxes
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[173].Code, PDFItems.elbowPDFItems[173].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[153].Code, PDFItems.elbowPDFItems[153].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[175].Code, PDFItems.elbowPDFItems[175].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[151].Code, PDFItems.elbowPDFItems[151].ExportValue, true);

                            //                        pdfFormFields.SetField(PDFItems.elbowPDFItems[173].Code, PDFItems.elbowPDFItems[173].ExportValue, true);
                            //                        if (SameInitialROMRight)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[175].Code, PDFItems.elbowPDFItems[175].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[176].Code, PDFItems.elbowPDFItems[176].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[170].Code, m.S170, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[169].Code, m.S169, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[171].Code, m.S171, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[172].Code, m.S172, true);

                            //                        }
                            //                        if ((m.S170 == m.S292)
                            //                            && (m.S169 == m.S290)
                            //                            && (m.S171 == m.S288)
                            //                            && (m.S172 == m.S286)
                            //                            )
                            //                        {
                            //                            SameFinalROMRight = true;
                            //                        }



                            //                        // Second test LEFT
                            //                        pdfFormFields.SetField(PDFItems.elbowPDFItems[153].Code, PDFItems.elbowPDFItems[153].ExportValue, true);
                            //                        if (SameInitialROMLeft)
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[151].Code, PDFItems.elbowPDFItems[151].ExportValue, true);
                            //                        }
                            //                        else
                            //                        {
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[150].Code, PDFItems.elbowPDFItems[150].ExportValue, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[154].Code, m.S154, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[155].Code, m.S155, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[148].Code, m.S148, true);
                            //                            pdfFormFields.SetField(PDFItems.elbowPDFItems[147].Code, m.S147, true);


                            //                        }
                            //                        if ((m.S154 == m.S260)
                            //&& (m.S155 == m.S255)
                            //&& (m.S148 == m.S253)
                            //&& (m.S147 == m.S254)
                            //)
                            //                        {
                            //                            SameFinalROMLeft = true;
                            //                        }

                            // Final test RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[293].Code, PDFItems.elbowPDFItems[293].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[294].Code, PDFItems.elbowPDFItems[294].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[292].Code, m.S292, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[290].Code, m.S290, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[288].Code, m.S288, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[286].Code, m.S286, true);
                            }

                            // Final test LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[261].Code, PDFItems.elbowPDFItems[261].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[262].Code, PDFItems.elbowPDFItems[262].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[260].Code, m.S260, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[255].Code, m.S255, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[253].Code, m.S253, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[254].Code, m.S254, true);
                            }

                        }
                        else if (DoInitialROMRight)
                        {
                            // the default for initial LEFT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[122].Code, "145", true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[112].Code, "180", true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[113].Code, "85", true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[116].Code, "80", true);

                            // set the values for initial RIGHT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[130].Code, m.S130, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[134].Code, m.S134, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[137].Code, m.S137, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[140].Code, m.S140, true);

                            if ((m.S130 == m.S170)
                                && (m.S134 == m.S169)
                                && (m.S137 == m.S171)
                                && (m.S140 == m.S172)
                                )
                            {
                                SameInitialROMRight = true;
                            }

                            // Second test for setting RIGHT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[173].Code, PDFItems.elbowPDFItems[173].ExportValue, true);
                            if (SameInitialROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[175].Code, PDFItems.elbowPDFItems[175].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[176].Code, PDFItems.elbowPDFItems[176].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[170].Code, m.S170, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[169].Code, m.S169, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[171].Code, m.S171, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[172].Code, m.S172, true);


                            }
                            if ((m.S170 == m.S292)
    && (m.S169 == m.S290)
    && (m.S171 == m.S288)
    && (m.S172 == m.S286)
    )
                            {
                                SameFinalROMRight = true;
                            }

                            // final RIGHT
                            if (SameFinalROMRight)
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[293].Code, PDFItems.elbowPDFItems[293].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[294].Code, PDFItems.elbowPDFItems[294].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[292].Code, m.S292, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[290].Code, m.S290, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[288].Code, m.S288, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[286].Code, m.S286, true);
                            }

                            // set the LEFT repeat to no change
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[153].Code, PDFItems.elbowPDFItems[153].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[151].Code, PDFItems.elbowPDFItems[151].ExportValue, true);

                            // set the LEFT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[261].Code, PDFItems.elbowPDFItems[261].ExportValue, true);

                        }
                        else if (DoInitialROMLeft)
                        {
                            // the default for initial RIGHT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[130].Code, "145", true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[134].Code, "180", true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[137].Code, "85", true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[140].Code, "80", true);

                            // set the initial LEFT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[122].Code, m.S122, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[112].Code, m.S112, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[113].Code, m.S113, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[116].Code, m.S116, true);

                            if ((m.S122 == m.S154)
                                && (m.S112 == m.S155)
                                && (m.S113 == m.S148)
                                && (m.S116 == m.S147)
                                )
                            {
                                SameInitialROMLeft = true;
                            }


                            // Second test for setting LEFT
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[153].Code, PDFItems.elbowPDFItems[153].ExportValue, true);
                            if (SameInitialROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[151].Code, PDFItems.elbowPDFItems[151].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[150].Code, PDFItems.elbowPDFItems[150].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[154].Code, m.S154, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[155].Code, m.S155, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[148].Code, m.S148, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[147].Code, m.S147, true);


                            }
                            if ((m.S154 == m.S260)
    && (m.S155 == m.S255)
    && (m.S148 == m.S253)
    && (m.S147 == m.S254)
    )
                            {
                                SameFinalROMLeft = true;
                            }

                            // final LEFT
                            if (SameFinalROMLeft)
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[261].Code, PDFItems.elbowPDFItems[261].ExportValue, true);
                            }
                            else
                            {
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[262].Code, PDFItems.elbowPDFItems[262].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[260].Code, m.S260, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[255].Code, m.S255, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[253].Code, m.S253, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[254].Code, m.S254, true);
                            }

                            // set the RIGHT repeat to no change
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[173].Code, PDFItems.elbowPDFItems[173].ExportValue, true);
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[175].Code, PDFItems.elbowPDFItems[175].ExportValue, true);

                            // set the RIGHT Flare-up to no change
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[293].Code, PDFItems.elbowPDFItems[293].ExportValue, true);

                        }

                        if ((!SameFinalROMLeft) || (!SameFinalROMRight))
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[249].Code, PDFItems.elbowPDFItems[249].ExportValue, true);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[248].Code, PDFItems.elbowPDFItems[248].ExportValue, true);
                        }



                        if (m.S195)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[195].Code, PDFItems.elbowPDFItems[195].ExportValue, true);
                            switch (m.S195Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[237].Code, PDFItems.elbowPDFItems[237].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[196].Code, PDFItems.elbowPDFItems[196].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[238].Code, PDFItems.elbowPDFItems[238].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S239)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[239].Code, PDFItems.elbowPDFItems[239].ExportValue, true);
                            switch (m.S239Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[235].Code, PDFItems.elbowPDFItems[235].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[236].Code, PDFItems.elbowPDFItems[236].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[234].Code, PDFItems.elbowPDFItems[234].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S193)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[193].Code, PDFItems.elbowPDFItems[193].ExportValue, true);
                            switch (m.S193Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[232].Code, PDFItems.elbowPDFItems[232].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[197].Code, PDFItems.elbowPDFItems[197].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[233].Code, PDFItems.elbowPDFItems[233].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S192)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[192].Code, PDFItems.elbowPDFItems[192].ExportValue, true);
                            switch (m.S192Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[230].Code, PDFItems.elbowPDFItems[230].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[231].Code, PDFItems.elbowPDFItems[231].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[229].Code, PDFItems.elbowPDFItems[229].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S247)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[282].Code, PDFItems.elbowPDFItems[247].ExportValue, true);
                            switch (m.S247Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[227].Code, PDFItems.elbowPDFItems[227].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[198].Code, PDFItems.elbowPDFItems[198].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[228].Code, PDFItems.elbowPDFItems[228].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S246)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[246].Code, PDFItems.elbowPDFItems[246].ExportValue, true);
                            switch (m.S246Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[225].Code, PDFItems.elbowPDFItems[225].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[226].Code, PDFItems.elbowPDFItems[226].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[224].Code, PDFItems.elbowPDFItems[224].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S245)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[245].Code, PDFItems.elbowPDFItems[245].ExportValue, true);
                            switch (m.S245Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[222].Code, PDFItems.elbowPDFItems[222].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[199].Code, PDFItems.elbowPDFItems[199].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[223].Code, PDFItems.elbowPDFItems[223].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S244)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[244].Code, PDFItems.elbowPDFItems[244].ExportValue, true);
                            switch (m.S244Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[220].Code, PDFItems.elbowPDFItems[220].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[221].Code, PDFItems.elbowPDFItems[221].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[219].Code, PDFItems.elbowPDFItems[219].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S243)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[243].Code, PDFItems.elbowPDFItems[243].ExportValue, true);
                            switch (m.S243Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[217].Code, PDFItems.elbowPDFItems[217].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[200].Code, PDFItems.elbowPDFItems[200].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[218].Code, PDFItems.elbowPDFItems[218].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S201)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[201].Code, PDFItems.elbowPDFItems[201].ExportValue, true);
                            switch (m.S201Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[215].Code, PDFItems.elbowPDFItems[215].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[216].Code, PDFItems.elbowPDFItems[216].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[214].Code, PDFItems.elbowPDFItems[214].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S213)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[213].Code, PDFItems.elbowPDFItems[213].ExportValue, true);
                            switch (m.S213Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[211].Code, PDFItems.elbowPDFItems[211].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[202].Code, PDFItems.elbowPDFItems[202].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[212].Code, PDFItems.elbowPDFItems[212].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S203)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[203].Code, PDFItems.elbowPDFItems[203].ExportValue, true);
                            switch (m.S203Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[209].Code, PDFItems.elbowPDFItems[209].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[210].Code, PDFItems.elbowPDFItems[210].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[208].Code, PDFItems.elbowPDFItems[208].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (m.S207)
                        {
                            pdfFormFields.SetField(PDFItems.elbowPDFItems[207].Code, PDFItems.elbowPDFItems[207].ExportValue, true);
                            switch (m.S207Side)
                            {
                                case "RIGHT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[209].Code, PDFItems.elbowPDFItems[209].ExportValue, true);
                                    break;
                                case "LEFT":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[210].Code, PDFItems.elbowPDFItems[210].ExportValue, true);
                                    break;
                                case "BOTH":
                                    pdfFormFields.SetField(PDFItems.elbowPDFItems[208].Code, PDFItems.elbowPDFItems[208].ExportValue, true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        switch (m.SideHand)
                        {
                            case "RIGHT":
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[102].Code, PDFItems.elbowPDFItems[102].ExportValue, true);
                                break;
                            case "LEFT":
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[105].Code, PDFItems.elbowPDFItems[105].ExportValue, true);
                                break;
                            default:
                                break;
                        }

                        switch (m.Side)
                        {
                            case "BOTH":
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[182].Code, PDFItems.elbowPDFItems[182].ExportValue, true);
                                pdfFormFields.SetField(PDFItems.elbowPDFItems[180].Code, PDFItems.elbowPDFItems[180].ExportValue, true);
                                break;
                            default:
                                break;
                        }

                        pdfFormFields.SetField(PDFItems.elbowPDFItems[96].Code, m.VarianceHistoryWriteIn, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[97].Code, m.VarianceFlareUpsWriteIn, true);
                        pdfFormFields.SetField(PDFItems.elbowPDFItems[98].Code, m.VarianceFunctionLossWriteIn, true);

                        if (m.IsFormReadonly)
                        {
                            IList<AcroFields.FieldPosition> lstPos = pdfFormFields.GetFieldPositions(PDFItems.elbowPDFItems[59].Code);
                            Rectangle rect = lstPos[0].position;
                            PdfContentByte cb = pdfStamper.GetOverContent(lstPos[0].page);
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetFontAndSize(bf, 12);
                            cb.BeginText();
                            cb.SetTextMatrix(rect.Left + 1, rect.Bottom + 2);
                            cb.ShowText(string.Empty);
                            cb.EndText();
                        }


                    }

                    if (m.IsFormReadonly)
                    {
                        // Set the flattening flag to true, so the document is not editable
                        pdfStamper.FormFlattening = true;
                    }

                    // close the pdf stamper
                    pdfStamper.Close();

                    form = ms.ToArray();

                }

            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }
    }

    public class PdfFill
    {
        // SetClaimedConditionsSection
        // p1 - Main condition check
        // p2 - The condition text
        // p3 - Condition index
        // p4 - date
        // p5 - icd code index
        // p6 - overall condition text
        // p7 - condition side
        // p8 - Right index
        // p9 - Left index
        // p10 - Both index
        public static void SetClaimedConditionsSection(AcroFields pdfFormFields, bool p1, string p2, int p3, int p4, int p5, StringBuilder p6, string p7, int p8, int p9, int p10, Dictionary<int, PDFItem> items, Dictionary<string, ICDCode> icdcodes)
        {
            string dt = System.DateTime.Today.ToShortDateString();
            ICDCode icdcode = null;
            string diagnosis = null;

            if (p1)
            {
                diagnosis = p2;
                pdfFormFields.SetField(items[p3].Code, items[p3].ExportValue, true);
                pdfFormFields.SetField(items[p4].Code, dt, true);
                if ((icdcodes != null) && (p5 > 0))
                {
                    if (icdcodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                    {
                        pdfFormFields.SetField(items[p5].Code, icdcode.RefNumber, true);
                    }
                }
                p6.Append(diagnosis);
                switch (p7)
                {
                    case "RIGHT":
                        pdfFormFields.SetField(items[p8].Code, items[p8].ExportValue, true);
                        p6.Append(", Right");
                        break;
                    case "LEFT":
                        pdfFormFields.SetField(items[p9].Code, items[p9].ExportValue, true);
                        p6.Append(", Left");
                        break;
                    case "BOTH":
                        pdfFormFields.SetField(items[p10].Code, items[p10].ExportValue, true);
                        p6.Append(", Bilateral");
                        break;
                    default:
                        break;
                }
                p6.Append(".  ");
            }
        }

        public static void SetMedicalRecordReviewSection(AcroFields pdfFormFields, Dictionary<int, PDFItem> items, int p1, int p2, int p3)
        {
            pdfFormFields.SetField(items[p1].Code, items[p1].ExportValue, true);
            pdfFormFields.SetField(items[p2].Code, items[p2].ExportValue, true);
            pdfFormFields.SetField(items[p3].Code, items[p3].ExportValue, true);
        }

        public static void SetContributingFactorsSection(AcroFields pdfFormFields, Dictionary<int, PDFItem> items, bool pCheck, string pSide, int pInd, int indRight, int indLeft, int indBoth)
        {
            if (pCheck)
            {
                pdfFormFields.SetField(items[pInd].Code, items[pInd].ExportValue, true);
                switch (pSide)
                {
                    case "RIGHT":
                        pdfFormFields.SetField(items[indRight].Code, items[indRight].ExportValue, true);
                        break;
                    case "LEFT":
                        pdfFormFields.SetField(items[indLeft].Code, items[indLeft].ExportValue, true);
                        break;
                    case "BOTH":
                        pdfFormFields.SetField(items[indBoth].Code, items[indBoth].ExportValue, true);
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
