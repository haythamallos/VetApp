using System;
using System.Linq;
using iTextSharp.text.pdf;
using MainSite.Models;
using System.IO;
using iTextSharp.text;
using MainSite.Classes;

using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.Common;

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

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper pdfStamper = null;

                    using (pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;
                        back.S60 = string.Empty;
                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;

                        // defaults
                        //form1[0].#subform[9].YesNo22[1]
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].SeverityAnkylosis[0]", "4");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo12[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13A[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo13[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo20[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo25[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo22[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");

                        pdfFormFields.SetField("form1[0].#subform[0].NameOfVeteran[0]", back.NameOfPatient);
                        pdfFormFields.SetField("form1[0].#subform[0].SSN[0]", back.SocialSecurity);

                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1");

                        if (!back.S47)
                        {
                            if (back.S48)
                            {
                                diagnosis = "Mechanical back pain syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses1[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis1[0]", dt);
                                back.S60 += diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode1[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S49)
                            {
                                diagnosis = "Lumbosacral sprain/strain";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses2[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis2[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode2[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S50)
                            {
                                diagnosis = "Facet joint arthropathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses3[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis3[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode3[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S51)
                            {
                                diagnosis = "Degenerative disc disease";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses4[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis4[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode4[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S1)
                            {
                                diagnosis = "Degenerative scoliosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses16[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis16[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode16[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S52)
                            {
                                diagnosis = "Foraminal/lateral recess/central stenosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses5[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis5[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode5[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S53)
                            {
                                diagnosis = "Degenerative spondylolisthesis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses6[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis6[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode6[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S54)
                            {
                                diagnosis = "Spondylolysis/isthmic spondylolisthesis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses7[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis7[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode7[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S55)
                            {
                                diagnosis = "Intervertebral disc syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses8[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis8[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode8[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S13)
                            {
                                diagnosis = "Radiculopathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses9[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis9[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode9[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S12)
                            {
                                diagnosis = "Ankylosis of thoracolumbar spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses10[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis10[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode10[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S7)
                            {
                                diagnosis = "Ankylosing spondylitis of the thoracolumbar spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses11[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis11[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode11[0]", icdcode.RefNumber);
                                }
                            }
                            if (back.S6)
                            {
                                diagnosis = "Vertebral fracture";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses15[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis15[0]", dt);
                                back.S60 += ", " + diagnosis;
                                if (ICDCodes.backICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode15[0]", icdcode.RefNumber);
                                }
                            }
                            if (!string.IsNullOrEmpty(back.S62))
                            {
                                diagnosis = "Other";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses12[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis12[0]", dt);
                                pdfFormFields.SetField("form1[0].#subform[0].OtherDiagnosis1[0]", back.S62);
                                back.S60 += ", " + diagnosis;
                            }

                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[0].NoDiagnoses[0]", "1");
                        }

                        if (back.S60.Count() > 1)
                        {
                            if (back.S60.IndexOf(", ") == 0)
                            {
                                back.S60 = back.S60.Remove(0, 2);
                            }
                        }
                        iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 6f, iTextSharp.text.Font.NORMAL);
                        normal = FontFactory.GetFont(FontFactory.COURIER, 4f, iTextSharp.text.Font.NORMAL);

                        //if (back.S60.Length > 255)
                        //{
                        //    normal = FontFactory.GetFont(FontFactory.COURIER, 4f, iTextSharp.text.Font.NORMAL);
                        //}
                        //set the field to bold
                        pdfFormFields.SetFieldProperty("form1[0].#subform[0].Records[1]", "textfont", normal.BaseFont, null);
                        pdfFormFields.SetField("form1[0].#subform[0].Records[1]", back.S60);

                        // Section 1D
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        // Section 2A
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", Defaults.BACK_SECION_2A);
                        // Section 2B
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", Defaults.BACK_SECION_2B);
                        // Section 2C
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", Defaults.BACK_SECION_2C);

                        if (back.S95)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].Forward_Flexion[1]", "2");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM1[0]", back.S96);
                        }

                        if (back.S93)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].Extension[0]", "2");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM2[0]", back.S86);
                        }

                        if (back.S92)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].RightLateral_Flexion[1]", "2");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM4[0]", back.S90);
                        }

                        if (back.S89)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].LeftLateral_Flexion[1]", "2");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM3[0]", back.S87);
                        }

                        if (back.S81)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].RightLateral_Rotation[1]", "2");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM6[0]", back.S82);
                        }

                        if (back.S79)
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].LeftLateral_Rotation[0]", "2");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[1].ROM5[0]", back.S78);
                        }

                        bool isAllSame = true;
                        if (!string.IsNullOrEmpty(back.S112))
                        {
                            if (back.S112 != back.S96)
                            {
                                isAllSame = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(back.S111))
                        {
                            if (back.S111 != back.S86)
                            {
                                isAllSame = false;
                            }

                        }
                        if (!string.IsNullOrEmpty(back.S113))
                        {
                            if (back.S113 != back.S90)
                            {
                                isAllSame = false;
                            }

                        }
                        if (!string.IsNullOrEmpty(back.S114))
                        {
                            if (back.S114 != back.S87)
                            {
                                isAllSame = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(back.S120))
                        {
                            if (back.S120 != back.S82)
                            {
                                isAllSame = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(back.S121))
                        {
                            if (back.S121 != back.S78)
                            {
                                isAllSame = false;
                            }
                        }

                        //if (!string.IsNullOrEmpty(back.S120))
                        //{
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM11[0]", back.S120);
                        //}
                        //if (!string.IsNullOrEmpty(back.S121))
                        //{
                        //    pdfFormFields.SetField("form1[0].#subform[2].PostROM12[0]", back.S121);
                        //}
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Perform[0]", "1");


                        if (isAllSame)
                        {
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Limitation[0]", "2");
                        }
                        else
                        {
                            // insert values
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM7[0]", back.S112);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM8[0]", back.S111);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM9[0]", back.S113);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM10[0]", back.S114);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM10[0]", back.S120);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM10[0]", back.S121);
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Limitation[1]", "1");
                        }


                        pdfFormFields.SetField("form1[0].#subform[2].Neck_Painful[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Neck_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss1[1]", "1");

                        if (back.S135Tenderness)
                        {
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Tender[0]", "1");
                            pdfFormFields.SetField("form1[0].#subform[2].Explain[2]", "Mild TTP Lumbar Paraspinal.");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Tender[1]", "2");
                        }


                        if ((back.S145MuscleSpasm) || (back.S145Guarding))
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].Guarding_Spasms[1]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].Guarding_Spasms[0]", "2");
                        }

                        if ((!back.S145MuscleSpasmQuestion) && (!back.S145GuardingQuestion))
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].Gait[0]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].Gait[1]", "2");
                            if ((back.S145MuscleSpasm) && (back.S145MuscleSpasmQuestion))
                            {
                                pdfFormFields.SetField("form1[0].#subform[3].DueTo[0]", "1");
                            }
                            if ((back.S145Guarding) && (back.S145GuardingQuestion))
                            {
                                pdfFormFields.SetField("form1[0].#subform[3].DueTo[1]", "2");
                            }
                        }

                        pdfFormFields.SetField("form1[0].#subform[3].SpinalContour[1]", "1");

                        if (back.S159)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor1[0]", "1");
                        }
                        if (back.S164)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor2[0]", "1");
                        }
                        if (back.S158)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor3[0]", "1");
                        }
                        if (back.S157)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor4[0]", "1");
                        }
                        if (back.S171)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor5[0]", "1");
                        }
                        if (back.S170)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor6[0]", "1");
                        }
                        if (back.S169)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor7[0]", "1");
                        }
                        if (back.S168)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor8[0]", "1");
                        }
                        if (back.S167)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor9[0]", "1");
                        }
                        if (back.S160)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor10[0]", "1");
                        }
                        if (back.S163)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor11[0]", "1");
                        }
                        if (back.S161)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor12[0]", "1");
                        }
                        if (back.S162)
                        {
                            pdfFormFields.SetField("form1[0].#subform[3].ContributingFactor13[0]", "1");
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
                            pdfFormFields.SetField("form1[0].#subform[4].ROM19[0]", back.S184);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM20[0]", back.S185);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM21[0]", back.S174);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM22[0]", back.S180);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM23[0]", back.S175);
                            pdfFormFields.SetField("form1[0].#subform[4].ROM24[0]", back.S177);

                            pdfFormFields.SetField("form1[0].#subform[4].YesNo6[1]", "1");
                            pdfFormFields.SetField("form1[0].#subform[4].Right_Limit[0]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[4].YesNo6[0]", "2");
                            pdfFormFields.SetField("form1[0].#subform[4].Right_Limit[1]", "2");
                        }

                        if (back.S316)
                        {
                            pdfFormFields.SetField("form1[0].#subform[6].YesNo11[0]", "1");

                            SetField_SRadiculopathyConstantPainLevelAnswer(back, pdfFormFields);
                            SetField_SRadiculopathyIntermittentPainLevelAnswer(back, pdfFormFields);
                            SetField_SRadiculopathyDullPainLevelAnswer(back, pdfFormFields);
                            SetField_SRadiculopathyTinglingPainLevelAnswer(back, pdfFormFields);
                            SetField_SRadiculopathyNumbnessPainLevelAnswer(back, pdfFormFields);
                            SetField_SRadiculopathySeverityLevel(back, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[6].YesNo11[1]", "2");
                        }

                        if (back.S55)
                        {
                            pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[0]", "3");
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[7].YesNo14A[1]", "2");
                        }

                        if (!string.IsNullOrEmpty(back.S15C))
                        {
                            SetField_S15C(back, pdfFormFields);
                        }

                        if (back.S414)
                        {
                            pdfFormFields.SetField("form1[0].#subform[8].YesNo18[0]", "1");
                            SetField_S17ABrace(back, pdfFormFields);
                            SetField_S17ACrutches(back, pdfFormFields);
                            SetField_S17ACane(back, pdfFormFields);
                            SetField_S17AWalker(back, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[8].YesNo18[1]", "2");
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
                pdfFormFields.SetField("form1[0].#subform[6].ConstantPain[0]", "1");
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
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity1[2]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity1[3]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity1[0]", "4");
                    break;
            }
        }

        private void SetField_SRadiculopathyConstantPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyConstantPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity1[1]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity1[0]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity1[3]", "4");
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
                pdfFormFields.SetField("form1[0].#subform[6].IntermittentPain[2]", "1");
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
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity2[1]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity2[0]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity2[3]", "4");
                    break;
            }
        }

        private void SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyIntermittentPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity2[2]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity2[3]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity2[0]", "4");
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
                pdfFormFields.SetField("form1[0].#subform[6].DullPain[0]", "1");
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
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity3[2]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity3[3]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Left_Severity3[0]", "4");
                    break;
            }
        }

        private void SetField_SRadiculopathyDullPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyDullPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity3[1]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity3[0]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[6].Right_Severity3[3]", "4");
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
                pdfFormFields.SetField("form1[0].#subform[7].Paresthesias[0]", "1");
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
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity4[2]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity4[3]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity4[0]", "4");
                    break;
            }
        }

        private void SetField_SRadiculopathyTinglingPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyTinglingPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity4[1]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity4[0]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity4[3]", "4");
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
                pdfFormFields.SetField("form1[0].#subform[7].Numbness[2]", "1");
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
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity5[1]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity5[0]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity5[3]", "4");
                    break;
            }
        }

        private void SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathyNumbnessPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity5[2]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity5[3]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity5[0]", "4");
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
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity7[2]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity7[3]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Left_Severity7[0]", "4");
                    break;
            }
        }

        private void SetField_SRadiculopathySeverityLevel_Right(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.SRadiculopathySeverityLevel)
            {
                case "MILD":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[1]", "2");
                    break;
                case "MODERATE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[0]", "3");
                    break;
                case "SEVERE":
                    pdfFormFields.SetField("form1[0].#subform[7].Right_Severity6[3]", "4");
                    break;
            }
        }

        private void SetField_S15C(BackModel back, AcroFields pdfFormFields)
        {
            switch (back.S15C)
            {
                case "ONEWEEK":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[0]", "1");
                    break;
                case "TWOWEEKS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[4]", "2");
                    break;
                case "FOURWEEKS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[1]", "3");
                    break;
                case "SIXWEEKS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[2]", "4");
                    break;
                case "SIXWEEKSPLUS":
                    pdfFormFields.SetField("form1[0].#subform[7].Duration[3]", "5");
                    break;
            }
        }

        private void SetField_S17ABrace(BackModel back, AcroFields pdfFormFields)
        {
            if (back.S416)
            {
                switch (back.S416Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse2[0]", "1");
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse2[1]", "2");
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse2[2]", "3");
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
                switch (back.S428Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse3[2]", "1");
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse3[1]", "2");
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse3[0]", "3");
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
                switch (back.S417Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse4[0]", "1");
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse4[1]", "2");
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse4[2]", "3");
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
                switch (back.S421Choice)
                {
                    case "OCCASIONAL":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse5[2]", "1");
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse5[1]", "2");
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField("form1[0].#subform[8].FrequencyofUse5[0]", "3");
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
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Neck_Painful[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Neck_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss1[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].SpinalContour[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].SeverityAnkylosis[1]", "4");
                        pdfFormFields.SetField("form1[0].#subform[6].AllNormal_Right1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].AllNormal_Left1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].Right_Biceps[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].Left_Biceps[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].Right_Triceps[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].Left_Triceps[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].Right_Brach[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].Left_Brach[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].ConstantPain[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13A[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo14B[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[7].Duration[1]", "3");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo13[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo14[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo15[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].Comments[9]", "None");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo18[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo20[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo25[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo22[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[9].YesNo24[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].Left_Reduction[1]", "2");
                        //
                        // 14A: Yes, if IVDS is check off on section 1B.

                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1");

                        pdfFormFields.SetField("form1[0].#subform[0].NameOfVeteran[0]", m.NameOfPatient);
                        pdfFormFields.SetField("form1[0].#subform[0].SSN[0]", m.SocialSecurity);

                        m.S58 = string.Empty;
                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;

                        if (!m.S45)
                        {
                            if (m.S46)
                            {
                                diagnosis = "Mechanical Cervical Pain Syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses1[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis1[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode1[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S47)
                            {
                                diagnosis = "Cervical Sprain/Strain";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses2[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis2[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode2[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S48)
                            {
                                diagnosis = "Cervical Spondylosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses3[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis3[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode3[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S49)
                            {
                                diagnosis = "Degenerative Disc Disease";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses4[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis4[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode4[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S50)
                            {
                                diagnosis = "Foraminal Stenosis/Central Stenosis";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses5[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis5[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode5[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S51)
                            {
                                diagnosis = "Intervertebral Disc Syndrome";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses6[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis6[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode6[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S52)
                            {
                                diagnosis = "Radiculopathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses7[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis7[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode7[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S53)
                            {
                                diagnosis = "Myelopathy";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses8[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis8[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode8[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S7)
                            {
                                diagnosis = "Ankylosis of Cervical Spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses9[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis9[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode9[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S6)
                            {
                                diagnosis = "Ankylosing Spondylitis of The Cervical Spine";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses10[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis10[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode10[0]", icdcode.RefNumber);
                                }
                            }
                            if (m.S1)
                            {
                                diagnosis = "Vertebral Fracture";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses11[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis11[0]", dt);
                                m.S58 += diagnosis;
                                if (ICDCodes.neckICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                                {
                                    pdfFormFields.SetField("form1[0].#subform[0].ICDCode11[0]", icdcode.RefNumber);
                                }
                            }

                            if (!string.IsNullOrEmpty(m.S54))
                            {
                                diagnosis = "Other";
                                pdfFormFields.SetField("form1[0].#subform[0].Diagnoses12[0]", "1");
                                pdfFormFields.SetField("form1[0].#subform[0].DateOfDiagnosis12[0]", dt);
                                pdfFormFields.SetField("form1[0].#subform[0].OtherDiagnosis1[0]", m.S54);
                                m.S58 += ", " + diagnosis;
                            }
                        }
                        else
                        {
                            pdfFormFields.SetField("form1[0].#subform[0].NoDiagnoses[0]", "1");
                        }

                        if (m.S58.Count() > 1)
                        {
                            if (m.S58.IndexOf(", ") == 0)
                            {
                                m.S58 = m.S58.Remove(0, 2);
                            }
                        }
                        iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 6f, iTextSharp.text.Font.NORMAL);
                        if (m.S58.Length > 255)
                        {
                            normal = FontFactory.GetFont(FontFactory.COURIER, 2f, iTextSharp.text.Font.NORMAL);
                        }
                        //set the field to bold
                        pdfFormFields.SetFieldProperty("form1[0].#subform[0].Records[1]", "textfont", normal.BaseFont, null);
                        pdfFormFields.SetField("form1[0].#subform[0].Records[1]", m.S58);

                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.");

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, lose of strength, soreness, and pain.");

                        SetField_Neck_2B(m, pdfFormFields);

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, lose of strength, soreness, and pain.");

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Severe restriction of range of motion.");

                        pdfFormFields.SetField("form1[0].#subform[1].ROM1[0]", m.S96);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM2[0]", m.S86);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM3[0]", m.S87);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM4[0]", m.S90);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM5[0]", m.S78);
                        pdfFormFields.SetField("form1[0].#subform[1].ROM6[0]", m.S82);

                        pdfFormFields.SetField("form1[0].#subform[2].PostROM7[0]", m.S109);
                        pdfFormFields.SetField("form1[0].#subform[2].PostROM8[0]", m.S108);
                        pdfFormFields.SetField("form1[0].#subform[2].PostROM9[0]", m.S110);
                        pdfFormFields.SetField("form1[0].#subform[2].PostROM10[0]", m.S111);
                        pdfFormFields.SetField("form1[0].#subform[2].PostROM11[0]", m.S117);
                        pdfFormFields.SetField("form1[0].#subform[2].PostROM12[0]", m.S118);

                        bool isAllSame = true;
                        if (m.S109 != m.S96)
                        {
                            isAllSame = false;
                        }
                        if (m.S108 != m.S86)
                        {
                            isAllSame = false;
                        }
                        if (m.S110 != m.S87)
                        {
                            isAllSame = false;
                        }
                        if (m.S111 != m.S90)
                        {
                            isAllSame = false;
                        }
                        if (m.S117 != m.S78)
                        {
                            isAllSame = false;
                        }
                        if (m.S118 != m.S82)
                        {
                            isAllSame = false;
                        }

                        pdfFormFields.SetField("form1[0].#subform[2].Right_Perform[0]", "1");

                        if (isAllSame)
                        {
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Limitation[0]", "2");
                        }
                        else
                        {
                            // insert values
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM7[0]", m.S109);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM8[0]", m.S108);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM9[0]", m.S110);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM10[0]", m.S111);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM11[0]", m.S117);
                            pdfFormFields.SetField("form1[0].#subform[2].PostROM12[0]", m.S118);
                            pdfFormFields.SetField("form1[0].#subform[2].Right_Limitation[1]", "1");
                        }




                        pdfFormFields.SetField(PDFItems.neckPDFItems[122].Code, PDFItems.neckPDFItems[122].ExportValue); //122
                        pdfFormFields.SetField(PDFItems.neckPDFItems[124].Code, PDFItems.neckPDFItems[124].ExportValue); //124
                        pdfFormFields.SetField(PDFItems.neckPDFItems[131].Code, PDFItems.neckPDFItems[131].ExportValue); //131
                        pdfFormFields.SetField(PDFItems.neckPDFItems[129].Code, PDFItems.neckPDFItems[129].ExportValue); //129

                        if (m.S135Tenderness)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[132].Code, PDFItems.neckPDFItems[132].ExportValue); //132
                            pdfFormFields.SetField(PDFItems.neckPDFItems[134].Code, "Mild TTP Lumbar Paraspinal."); //134
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[133].Code, PDFItems.neckPDFItems[133].ExportValue); //133
                        }


                        if ((m.S145MuscleSpasm) || (m.S145Guarding))
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[149].Code, PDFItems.neckPDFItems[149].ExportValue); //149
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[148].Code, PDFItems.neckPDFItems[148].ExportValue); //148
                        }

                        if ((!m.S145MuscleSpasmQuestion) && (!m.S145GuardingQuestion))
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[142].Code, PDFItems.neckPDFItems[142].ExportValue); //142
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[143].Code, PDFItems.neckPDFItems[143].ExportValue); //143
                            if ((m.S145MuscleSpasm) && (m.S145MuscleSpasmQuestion))
                            {
                                pdfFormFields.SetField(PDFItems.neckPDFItems[144].Code, PDFItems.neckPDFItems[144].ExportValue); //144
                            }
                            if ((m.S145Guarding) && (m.S145GuardingQuestion))
                            {
                                pdfFormFields.SetField(PDFItems.neckPDFItems[145].Code, PDFItems.neckPDFItems[145].ExportValue); //145
                            }
                        }

                        pdfFormFields.SetField(PDFItems.neckPDFItems[141].Code, PDFItems.neckPDFItems[141].ExportValue); //141

                        if (m.S159)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[156].Code, PDFItems.neckPDFItems[156].ExportValue); //156
                        }
                        if (m.S164)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[161].Code, PDFItems.neckPDFItems[161].ExportValue); //161
                        }
                        if (m.S158)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[155].Code, PDFItems.neckPDFItems[155].ExportValue); //155
                        }
                        if (m.S157)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[154].Code, PDFItems.neckPDFItems[154].ExportValue); //154
                        }
                        if (m.S171)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[168].Code, PDFItems.neckPDFItems[168].ExportValue); //168
                        }
                        if (m.S170)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[167].Code, PDFItems.neckPDFItems[167].ExportValue); //167
                        }
                        if (m.S169)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[166].Code, PDFItems.neckPDFItems[166].ExportValue); //166
                        }
                        if (m.S168)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[165].Code, PDFItems.neckPDFItems[165].ExportValue); //165
                        }
                        if (m.S167)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[164].Code, PDFItems.neckPDFItems[164].ExportValue); //164
                        }
                        if (m.S160)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[157].Code, PDFItems.neckPDFItems[157].ExportValue); //157
                        }
                        if (m.S163)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[160].Code, PDFItems.neckPDFItems[160].ExportValue); //160
                        }
                        if (m.S161)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[158].Code, PDFItems.neckPDFItems[1658].ExportValue);//158
                        }
                        if (m.S162)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[159].Code, PDFItems.neckPDFItems[159].ExportValue); //159
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
                            pdfFormFields.SetField(PDFItems.neckPDFItems[181].Code, m.S184); //181
                            pdfFormFields.SetField(PDFItems.neckPDFItems[182].Code, m.S185); //182
                            pdfFormFields.SetField(PDFItems.neckPDFItems[171].Code, m.S174); //171
                            pdfFormFields.SetField(PDFItems.neckPDFItems[177].Code, m.S180); //177
                            pdfFormFields.SetField(PDFItems.neckPDFItems[172].Code, m.S175); //172
                            pdfFormFields.SetField(PDFItems.neckPDFItems[174].Code, m.S177); //174

                            pdfFormFields.SetField(PDFItems.neckPDFItems[170].Code, PDFItems.neckPDFItems[170].ExportValue); //170
                            pdfFormFields.SetField(PDFItems.neckPDFItems[183].Code, PDFItems.neckPDFItems[183].ExportValue); //183
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[169].Code, PDFItems.neckPDFItems[169].ExportValue); //169
                            pdfFormFields.SetField(PDFItems.neckPDFItems[184].Code, PDFItems.neckPDFItems[184].ExportValue); //184
                        }

                        if (m.S316)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[305].Code, PDFItems.neckPDFItems[305].ExportValue); //305

                            SetField_SRadiculopathyConstantPainLevelAnswer(m, pdfFormFields);
                            SetField_SRadiculopathyIntermittentPainLevelAnswer(m, pdfFormFields);
                            SetField_SRadiculopathyDullPainLevelAnswer(m, pdfFormFields);
                            SetField_SRadiculopathyTinglingPainLevelAnswer(m, pdfFormFields);
                            SetField_SRadiculopathyNumbnessPainLevelAnswer(m, pdfFormFields);
                            SetField_SRadiculopathySeverityLevel(m, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[317].Code, PDFItems.neckPDFItems[317].ExportValue); //317
                        }

                        if (m.S51)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[330].Code, PDFItems.neckPDFItems[330].ExportValue); //330
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[377].Code, PDFItems.neckPDFItems[377].ExportValue); //377
                        }

                        if (!string.IsNullOrEmpty(m.S15C))
                        {
                            SetField_S15C(m, pdfFormFields);
                        }

                        if (m.S414)
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[401].Code, PDFItems.neckPDFItems[401].ExportValue); //401
                            SetField_S17ABrace(m, pdfFormFields);
                            SetField_S17ACrutches(m, pdfFormFields);
                            SetField_S17ACane(m, pdfFormFields);
                            SetField_S17AWalker(m, pdfFormFields);

                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.neckPDFItems[402].Code, PDFItems.neckPDFItems[402].ExportValue); //402
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
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }
        private void SetField_Neck_2B(NeckModel model, AcroFields pdfFormFields)
        {
            if (!string.IsNullOrEmpty(model.SDominantHand))
            {
                switch (model.SDominantHand)
                {
                    case "RIGHT":
                        pdfFormFields.SetField("form1[0].#subform[1].DominantHand[2]", "2");
                        break;
                    case "LEFT":
                        pdfFormFields.SetField("form1[0].#subform[1].DominantHand[1]", "1");
                        break;
                    case "AMBIDEXTROUS":
                        pdfFormFields.SetField("form1[0].#subform[1].DominantHand[0]", "3");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Constant pain radiculopath
        /// </summary>
        private void SetField_SRadiculopathyConstantPainLevelAnswer(NeckModel m, AcroFields pdfFormFields)
        {
            if (m.SRadiculopathyConstantPainLevelAnswer)
            {
                pdfFormFields.SetField(PDFItems.neckPDFItems[306].Code, PDFItems.neckPDFItems[306].ExportValue); //306
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
                    pdfFormFields.SetField(PDFItems.neckPDFItems[315].Code, PDFItems.neckPDFItems[315].ExportValue); //315
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[316].Code, PDFItems.neckPDFItems[316].ExportValue); //316
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[311].Code, PDFItems.neckPDFItems[311].ExportValue); //311
                    break;
            }
        }

        private void SetField_SRadiculopathyConstantPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyConstantPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[308].Code, PDFItems.neckPDFItems[308].ExportValue); //308
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[307].Code, PDFItems.neckPDFItems[307].ExportValue); //307
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[310].Code, PDFItems.neckPDFItems[310].ExportValue); //310
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
                pdfFormFields.SetField(PDFItems.neckPDFItems[263].Code, PDFItems.neckPDFItems[263].ExportValue); //263
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
                    pdfFormFields.SetField(PDFItems.neckPDFItems[254].Code, PDFItems.neckPDFItems[254].ExportValue); //254
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[253].Code, PDFItems.neckPDFItems[253].ExportValue); //253
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[258].Code, PDFItems.neckPDFItems[258].ExportValue); //258
                    break;
            }
        }

        private void SetField_SRadiculopathyIntermittentPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyIntermittentPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[261].Code, PDFItems.neckPDFItems[261].ExportValue); //261
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[262].Code, PDFItems.neckPDFItems[262].ExportValue); //262
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[259].Code, PDFItems.neckPDFItems[259].ExportValue); //259
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
                pdfFormFields.SetField(PDFItems.neckPDFItems[240].Code, PDFItems.neckPDFItems[240].ExportValue); //240
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
                    pdfFormFields.SetField(PDFItems.neckPDFItems[254].Code, PDFItems.neckPDFItems[254].ExportValue); //254
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[255].Code, PDFItems.neckPDFItems[255].ExportValue); //255
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[250].Code, PDFItems.neckPDFItems[250].ExportValue); //250
                    break;
            }
        }

        private void SetField_SRadiculopathyDullPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyDullPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[247].Code, PDFItems.neckPDFItems[247].ExportValue); //247
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[246].Code, PDFItems.neckPDFItems[246].ExportValue); //246
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[249].Code, PDFItems.neckPDFItems[249].ExportValue); //249
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
                pdfFormFields.SetField(PDFItems.neckPDFItems[348].Code, PDFItems.neckPDFItems[348].ExportValue); //348
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
                    pdfFormFields.SetField(PDFItems.neckPDFItems[357].Code, PDFItems.neckPDFItems[357].ExportValue); //357
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[358].Code, PDFItems.neckPDFItems[358].ExportValue); //358
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[353].Code, PDFItems.neckPDFItems[353].ExportValue); //353
                    break;
            }
        }

        private void SetField_SRadiculopathyTinglingPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyTinglingPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[350].Code, PDFItems.neckPDFItems[350].ExportValue); //350
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[349].Code, PDFItems.neckPDFItems[349].ExportValue); //349
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[352].Code, PDFItems.neckPDFItems[352].ExportValue); //352
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
                pdfFormFields.SetField(PDFItems.neckPDFItems[347].Code, PDFItems.neckPDFItems[347].ExportValue); //347
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
                    pdfFormFields.SetField(PDFItems.neckPDFItems[338].Code, PDFItems.neckPDFItems[338].ExportValue); //338
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[329].Code, PDFItems.neckPDFItems[329].ExportValue); //329
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[342].Code, PDFItems.neckPDFItems[342].ExportValue); //342
                    break;
            }
        }

        private void SetField_SRadiculopathyNumbnessPainLevelAnswer_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathyNumbnessPainLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[345].Code, PDFItems.neckPDFItems[345].ExportValue); //345
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[346].Code, PDFItems.neckPDFItems[346].ExportValue); //346
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[343].Code, PDFItems.neckPDFItems[343].ExportValue); //343
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
                    pdfFormFields.SetField(PDFItems.neckPDFItems[336].Code, PDFItems.neckPDFItems[336].ExportValue); //336
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[337].Code, PDFItems.neckPDFItems[337].ExportValue); //337
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[334].Code, PDFItems.neckPDFItems[334].ExportValue); //334
                    break;
            }
        }

        private void SetField_SRadiculopathySeverityLevel_Right(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.SRadiculopathySeverityLevel)
            {
                case "MILD":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[331].Code, PDFItems.neckPDFItems[331].ExportValue); //331
                    break;
                case "MODERATE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[330].Code, PDFItems.neckPDFItems[330].ExportValue); //330
                    break;
                case "SEVERE":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[333].Code, PDFItems.neckPDFItems[333].ExportValue); //333
                    break;
            }
        }

        private void SetField_S15C(NeckModel m, AcroFields pdfFormFields)
        {
            switch (m.S15C)
            {
                case "ONEWEEK":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[379].Code, PDFItems.neckPDFItems[379].ExportValue); //379
                    break;
                case "TWOWEEKS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[383].Code, PDFItems.neckPDFItems[383].ExportValue); //383
                    break;
                case "FOURWEEKS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[380].Code, PDFItems.neckPDFItems[380].ExportValue); //380
                    break;
                case "SIXWEEKS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[381].Code, PDFItems.neckPDFItems[381].ExportValue); //381
                    break;
                case "SIXWEEKSPLUS":
                    pdfFormFields.SetField(PDFItems.neckPDFItems[382].Code, PDFItems.neckPDFItems[382].ExportValue); //382
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
                        pdfFormFields.SetField(PDFItems.neckPDFItems[421].Code, PDFItems.neckPDFItems[421].ExportValue); //421
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[425].Code, PDFItems.neckPDFItems[425].ExportValue); //425
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[426].Code, PDFItems.neckPDFItems[426].ExportValue); //426
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
                        pdfFormFields.SetField(PDFItems.neckPDFItems[414].Code, PDFItems.neckPDFItems[414].ExportValue); //414
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[413].Code, PDFItems.neckPDFItems[413].ExportValue); //413
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[412].Code, PDFItems.neckPDFItems[412].ExportValue); //412
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
                        pdfFormFields.SetField(PDFItems.neckPDFItems[409].Code, PDFItems.neckPDFItems[409].ExportValue); //409
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[410].Code, PDFItems.neckPDFItems[410].ExportValue); //410
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[411].Code, PDFItems.neckPDFItems[411].ExportValue); //411
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
                        pdfFormFields.SetField(PDFItems.neckPDFItems[407].Code, PDFItems.neckPDFItems[407].ExportValue); //407
                        break;
                    case "REGULAR":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[406].Code, PDFItems.neckPDFItems[406].ExportValue); //406
                        break;
                    case "CONSTANT":
                        pdfFormFields.SetField(PDFItems.neckPDFItems[405].Code, PDFItems.neckPDFItems[405].ExportValue); //405
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
                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2A[0]", "On-set of injury incurred during active duty service.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2B[0]", "Physical limitations, lose of strength, soreness, and pain.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2C[0]", "Severe restriction of range of motion.");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Painful[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Painful[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Pain1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Pain1[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Tender[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Tender[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Loss[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength3[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength3[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis4[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis4[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].YesNoRC[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].YesNoRC_L[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R1[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L1[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R2[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L2[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R3[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L3[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_R4[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RotCuff_L4[2]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].No242[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].No24[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].No24[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].No20[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].One60[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].No12A[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].No12B[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo14A[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo16A[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo20[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo17B[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo17C[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo18[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[8].Comments18[0]", "Difficultly with overhead task.");

                        //
                        //
                        //
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
                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo4[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[1]", "Physical limitations, loss of strength, soreness, and pain.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "See section 13A");
                        pdfFormFields.SetField("form1[0].#subform[3].No7A[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].No6B[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_None[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_None[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].No11A[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo19[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo20[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo24[0]", "2");
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
                        pdfFormFields.SetField("F[0].Page_1[0].Yes1[0]", "1");
                        pdfFormFields.SetField("F[0].Page_1[0].B1[0]", "1");
                        pdfFormFields.SetField("F[0].Page_1[0].No4A[0]", "1");
                        pdfFormFields.SetField("F[0].Page_1[0].No4B[0]", "1");
                        pdfFormFields.SetField("F[0].Page_2[0].Yes5A[0]", "1");
                        pdfFormFields.SetField("F[0].Page_2[0].No5B[0]", "1");
                        pdfFormFields.SetField("F[0].Page_2[0].No10[0]", "1");

                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[44].Code, m.FirstName);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[46].Code, m.MiddleInitial);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[45].Code, m.LastName);
                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[41].Code, ssn.LeftPart);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[42].Code, ssn.MiddlePart);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[43].Code, ssn.RightPart);

                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[68].Code, ssn.LeftPart);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[69].Code, ssn.MiddlePart);
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[70].Code, ssn.RightPart);

                        }

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string S17 = "The onset of veterans sleep apnea was during active duty service";

                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[6].Code, PDFItems.sleepapneaPDFItems[6].ExportValue);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[7].Code, dt);
                        if (ICDCodes.sleepapneaICDCodes.TryGetValue("obstructive", out icdcode))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[16].Code, icdcode.RefNumber);
                        }

                        iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 6f, iTextSharp.text.Font.NORMAL);
                        normal = FontFactory.GetFont(FontFactory.COURIER, 4f, iTextSharp.text.Font.NORMAL);
                        //set the field to bold
                        pdfFormFields.SetFieldProperty(PDFItems.sleepapneaPDFItems[17].Code, "textfont", normal.BaseFont, null);
                        pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[17].Code, S17);

                        if (m.S20)
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[20].Code, PDFItems.sleepapneaPDFItems[20].ExportValue);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[21].Code, PDFItems.sleepapneaPDFItems[21].ExportValue);
                        }

                        if (!string.IsNullOrEmpty(m.lastSleepStudyDate))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[55].Code, m.lastSleepStudyDate);
                        }

                        if (!string.IsNullOrEmpty(m.FacilityName))
                        {
                            pdfFormFields.SetField(PDFItems.sleepapneaPDFItems[53].Code, m.FacilityName);
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
                        pdfFormFields.SetField("F[0].Page_1[0].Yes1[0]", "1");
                        pdfFormFields.SetField("F[0].Page_1[0].DescribeHistory[0]", "The onset of this disability was during military service.");
                        pdfFormFields.SetField("F[0].Page_2[0].No12[0]", "1");
                        pdfFormFields.SetField("F[0].Page_3[0].No11[0]", "1");
                        pdfFormFields.SetField("F[0].Page_3[0].No52[0]", "1");

                        pdfFormFields.SetField(PDFItems.headachePDFItems[39].Code, m.FirstName);
                        pdfFormFields.SetField(PDFItems.headachePDFItems[41].Code, m.MiddleInitial);
                        pdfFormFields.SetField(PDFItems.headachePDFItems[40].Code, m.LastName);
                        SSN ssn = UtilsString.ParseSSN(m.SocialSecurity);
                        if (ssn != null)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[36].Code, ssn.LeftPart);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[37].Code, ssn.MiddlePart);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[38].Code, ssn.RightPart);

                            pdfFormFields.SetField(PDFItems.headachePDFItems[85].Code, ssn.LeftPart);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[86].Code, ssn.MiddlePart);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[87].Code, ssn.RightPart);

                            pdfFormFields.SetField(PDFItems.headachePDFItems[105].Code, ssn.LeftPart);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[106].Code, ssn.MiddlePart);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[107].Code, ssn.RightPart);

                        }

                        string dt = System.DateTime.Today.ToShortDateString();
                        ICDCode icdcode = null;
                        string diagnosis = null;
                        if (m.S35)
                        {
                            diagnosis = "Migrane";
                            pdfFormFields.SetField(PDFItems.headachePDFItems[35].Code, PDFItems.headachePDFItems[35].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[19].Code, dt);
                            if (ICDCodes.headacheICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[22].Code, icdcode.RefNumber);
                            }
                        }
                        if (m.S23)
                        {
                            diagnosis = "Tension";
                            pdfFormFields.SetField(PDFItems.headachePDFItems[23].Code, PDFItems.headachePDFItems[23].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[18].Code, dt);
                            if (ICDCodes.headacheICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[21].Code, icdcode.RefNumber);
                            }
                        }
                        if (m.S24)
                        {
                            diagnosis = "Cluster";
                            pdfFormFields.SetField(PDFItems.headachePDFItems[24].Code, PDFItems.headachePDFItems[24].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[8].Code, dt);
                            if (ICDCodes.headacheICDCodes.TryGetValue(diagnosis.ToLower(), out icdcode))
                            {
                                pdfFormFields.SetField(PDFItems.headachePDFItems[20].Code, icdcode.RefNumber);
                            }
                        }

                        if (!string.IsNullOrEmpty(m.MedicationPlan))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[32].Code, PDFItems.headachePDFItems[32].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[30].Code, m.MedicationPlan);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[33].Code, PDFItems.headachePDFItems[33].ExportValue);
                        }

                        bool b3A = false;
                        if (m.S26)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[26].Code, PDFItems.headachePDFItems[26].ExportValue);
                            b3A = true;
                        }
                        if (m.S2)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[2].Code, PDFItems.headachePDFItems[2].ExportValue);
                            b3A = true;
                        }
                        if (m.S3)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[3].Code, PDFItems.headachePDFItems[3].ExportValue);
                            b3A = true;
                        }
                        if (m.S4)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[4].Code, PDFItems.headachePDFItems[4].ExportValue);
                            b3A = true;
                        }
                        if (m.S5)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[5].Code, PDFItems.headachePDFItems[5].ExportValue);
                            b3A = true;
                        }
                        if (!string.IsNullOrEmpty(m.S3AOther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[6].Code, PDFItems.headachePDFItems[6].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[15].Code, m.S3AOther);
                            b3A = true;
                        }
                        if (b3A)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[1].Code, PDFItems.headachePDFItems[1].ExportValue);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[7].Code, PDFItems.headachePDFItems[7].ExportValue);
                        }
                        m.S3AYes = b3A;


                        bool b3B = false;
                        if (m.S75)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[75].Code, PDFItems.headachePDFItems[75].ExportValue);
                            b3B = true;
                        }
                        if (m.S68)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[68].Code, PDFItems.headachePDFItems[68].ExportValue);
                            b3B = true;
                        }
                        if (m.S69)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[69].Code, PDFItems.headachePDFItems[69].ExportValue);
                            b3B = true;
                        }
                        if (m.S70)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[70].Code, PDFItems.headachePDFItems[70].ExportValue);
                            b3B = true;
                        }
                        if (m.S71)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[71].Code, PDFItems.headachePDFItems[71].ExportValue);
                            b3B = true;
                        }
                        if (m.S72)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[72].Code, PDFItems.headachePDFItems[72].ExportValue);
                            b3B = true;
                        }
                        if (!string.IsNullOrEmpty(m.S3BOther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[73].Code, PDFItems.headachePDFItems[73].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[74].Code, m.S3AOther);
                            b3B = true;
                        }
                        if (b3B)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[77].Code, PDFItems.headachePDFItems[77].ExportValue);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[76].Code, PDFItems.headachePDFItems[76].ExportValue);
                        }
                        m.S3BYes = b3B;

                        if (m.S54)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[54].Code, PDFItems.headachePDFItems[54].ExportValue);
                        }
                        if (m.S55)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[55].Code, PDFItems.headachePDFItems[55].ExportValue);
                        }
                        if (m.S60)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[60].Code, PDFItems.headachePDFItems[60].ExportValue);
                        }
                        if (!string.IsNullOrEmpty(m.S3COther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[61].Code, PDFItems.headachePDFItems[61].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[62].Code, m.S3COther);
                        }

                        if (m.S66)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[66].Code, PDFItems.headachePDFItems[66].ExportValue);
                        }
                        if (m.S65)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[65].Code, PDFItems.headachePDFItems[65].ExportValue);
                        }
                        if (m.S64)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[64].Code, PDFItems.headachePDFItems[64].ExportValue);
                        }
                        if (!string.IsNullOrEmpty(m.S3DOther))
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[63].Code, PDFItems.headachePDFItems[63].ExportValue);
                            pdfFormFields.SetField(PDFItems.headachePDFItems[67].Code, m.S3DOther);
                        }

                        m.S4AYes = false;
                        if (m.S56)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[56].Code, PDFItems.headachePDFItems[56].ExportValue);
                            m.S4AYes = true;
                        }
                        if (m.S57)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[57].Code, PDFItems.headachePDFItems[57].ExportValue);
                            m.S4AYes = true;
                        }
                        if (m.S58)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[58].Code, PDFItems.headachePDFItems[58].ExportValue);
                            m.S4AYes = true;
                        }
                        if (m.S59)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[59].Code, PDFItems.headachePDFItems[59].ExportValue);
                            m.S4AYes = true;
                        }
                        if (m.S4AYes)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[46].Code, PDFItems.headachePDFItems[46].ExportValue);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[47].Code, PDFItems.headachePDFItems[47].ExportValue);
                        }

                        m.S4CYes = false;
                        if (m.S45)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[45].Code, PDFItems.headachePDFItems[45].ExportValue);
                            m.S4CYes = true;
                        }
                        if (m.S44)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[44].Code, PDFItems.headachePDFItems[44].ExportValue);
                            m.S4CYes = true;
                        }
                        if (m.S43)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[43].Code, PDFItems.headachePDFItems[43].ExportValue);
                            m.S4CYes = true;
                        }
                        if (m.S42)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[42].Code, PDFItems.headachePDFItems[42].ExportValue);
                            m.S4CYes = true;
                        }
                        if (m.S4CYes)
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[50].Code, PDFItems.headachePDFItems[50].ExportValue);
                        }
                        else
                        {
                            pdfFormFields.SetField(PDFItems.headachePDFItems[51].Code, PDFItems.headachePDFItems[51].ExportValue);
                        }

                        switch(m.WorkCondition)
                        {
                            case "A":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7ARemark);
                                break;
                            case "B":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7BRemark);
                                break;
                            case "C":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7CRemark);
                                break;
                            case "D":
                                pdfFormFields.SetField(PDFItems.headachePDFItems[97].Code, PDFItems.headachePDFItems[97].ExportValue);
                                pdfFormFields.SetField(PDFItems.headachePDFItems[96].Code, Defaults.HEADACHE_SECION_7DRemark);
                                break;
                            default:
                                pdfFormFields.SetField(PDFItems.headachePDFItems[98].Code, PDFItems.headachePDFItems[98].ExportValue);
                                break;
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
                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("", "1");

                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, lose of strength, soreness, and pain.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Severe restriction of range of motion.");

                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[0]", "On-set of injury incurred during active duty service.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo4[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Painful[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Painful[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Left_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Pain1[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Tender[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Tender[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Loss[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Loss[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength2[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength2[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].RAnkylosis8[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].LAnkylosis8[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo9Right[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].Right_TalarTest[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].Right_AnteriorTest[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].Left_AnteriorTest[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].Right_TalarTest[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].Left_TalarTest[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo13[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo14[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo18[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo19[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo20[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo22[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo23[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo24[0]", "2");
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
                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");

                        // Section 2C
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2B[0]", Defaults.BACK_SECION_2B);
                        // Section 2D
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2C[0]", Defaults.BACK_SECION_2C);

                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2A[0]", "On-set of injury incurred during active duty service.");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Painful[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Painful[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Pain1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_FunctionalLoss1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Pain1[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_FunctionalLoss1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Tender[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Tender[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Loss[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Loss[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength3[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength3[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis6[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis6[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].Residuals[4]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].Residuals[3]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo15A[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo16A[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo20[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo17B[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo17C[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo18[0]", "2");

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
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2A[0]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2B[0]", "Physical limitations, loss of strength, soreness, and pain.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2C[0]", "Physical limitations, loss of strength, soreness, and pain. See section 6A.");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[2]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength2[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength2[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis5[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis5[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].No9C[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo10A[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo20[0]", "2");


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
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

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
                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[1]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Physical limitations, loss of strength, soreness, and pain. See section 6A.");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo4[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo6[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].Right_Loss[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].Left_Loss[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].RAnkylosis4[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].Left_Reduction[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo10[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo13[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo19[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo20[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[8].YesNo23[0]", "2");

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
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

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
                        pdfFormFields.SetField("form1[0].#subform[0].No1[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords7[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[0].AllRecords8[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Opinion[0]", "3");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe[1]", "The onset of this disability was during military service, continues to deal with symptoms, and flare ups.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo2[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe1[0]", "Physical limitations, loss of strength, soreness, and pain.");
                        pdfFormFields.SetField("form1[0].#subform[1].YesNo3[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[1].Describe2[0]", "Physical limitations, loss of strength, soreness, and pain. See Section 6A.");
                        pdfFormFields.SetField("form1[0].#subform[2].YesNo5[1]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Pain1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[2].Left_Pain1[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Right_Tender[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[3].Left_Tender[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Strength2[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength1[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Strength2[0]", "5");
                        pdfFormFields.SetField("form1[0].#subform[4].Right_Reduction[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].Left_Reduction[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[4].YesNo7[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[5].RAnkylosis4[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].LAnkylosis4[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[5].YesNo10[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo13[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].YesNo18[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo19[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[6].Residuals[6]", "1");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo22[1]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo23[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[7].YesNo24[0]", "2");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_Painful[0]", "1");
                        pdfFormFields.SetField("form1[0].#subform[2].Right_FunctionalLoss[0]", "1");
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
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return form;

        }
    }
}
