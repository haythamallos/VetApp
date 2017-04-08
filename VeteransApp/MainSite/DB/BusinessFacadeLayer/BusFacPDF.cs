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

        public long Save(IBaseModel model, long contentStateID, long contentTypeID)
        {
            long lID = 0;
            try
            {
                bool bAddID = false;


                BusFacCore busFacCore = new BusFacCore();
                Content content = busFacCore.ContentGetLatest(model.UserID, contentTypeID);
                if (content == null)
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

                        if (back.S60.Count() > 1)
                        {
                            if (back.S60.IndexOf(", ") == 0)
                            {
                                back.S60 = back.S60.Remove(0, 2);
                            }
                        }
                        iTextSharp.text.Font normal = FontFactory.GetFont(FontFactory.COURIER, 6f, iTextSharp.text.Font.NORMAL);
                        if (back.S60.Length > 255)
                        {
                            normal = FontFactory.GetFont(FontFactory.COURIER, 4f, iTextSharp.text.Font.NORMAL);
                        }
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