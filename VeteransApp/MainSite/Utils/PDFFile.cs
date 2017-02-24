using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace MainSite.Utils
{
    public class PDFFile
    {
        public string pdfTemplatePath { get; set; }
        public List<PDFFileField> FieldInfoList = new List<PDFFileField>();
        public AcroFields formFields = null;
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public Dictionary<string, string> formFieldMap = null;
        public PDFFile(string pdfPath)
        {
            pdfTemplatePath = pdfPath;
            formFieldMap = PDFHelper.GetFormFieldNames(pdfTemplatePath);

            var reader = new PdfReader(pdfTemplatePath);
            formFields = reader.AcroFields;

            Resolve();
        }

        private void Resolve()
        {
            try
            {
                var reader = new PdfReader(pdfTemplatePath);
                formFields = reader.AcroFields;
                PDFFileField pdffilefield = null;
                FieldInfoList.Clear();
                foreach (var entry in formFields.Fields)
                {
                    pdffilefield = new PDFFileField();
                    pdffilefield.FieldEntry = entry;
                    pdffilefield.Key = entry.Key;
                    var formFieldType = PDFFieldType.GetPDFFieldType(formFields.GetFieldType(entry.Key.ToString()));
                    pdffilefield.FormFieldType = formFieldType;
                    if (formFieldType is PDFCheckBoxFieldType)
                    {
                        pdffilefield.ExportValue = PDFHelper.GetExportValue(entry.Value as AcroFields.Item);
                        pdffilefield.IsCheckBox = true;
                    }
                    else if (formFieldType is PDFTextFieldType)
                    {
                        pdffilefield.IsTextField = true;
                    }
                    else
                    {
                        pdffilefield.IsOtherField = true;
                    }
                    FieldInfoList.Add(pdffilefield);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ex.Message;
                StackTrace = ex.StackTrace;
            }
        }

        private void Resolve2()
        {
            try
            {
                // Use iTextSharp PDF Reader, to get the fields and send to the 
                //Stamper to set the fields in the document
                PdfReader pdfReader = new PdfReader(pdfTemplatePath);


                using (MemoryStream ms = new MemoryStream())
                {
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, ms, '\0', true))
                    {
                        // Get Reference to PDF Document Fields
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        //create a bold font
                        iTextSharp.text.Font bold = FontFactory.GetFont(FontFactory.COURIER, 8f, iTextSharp.text.Font.BOLD);

                    }
                    //return ms.ToArray();
                }



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
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ex.Message;
                StackTrace = ex.StackTrace;
            }
        }
    }

    public class PDFFileField
    {
        public string Key { get; set; }
        public bool IsCheckBox { get; set; }
        public bool IsTextField { get; set; }
        public bool IsOtherField { get; set; }
        public PDFFieldType FormFieldType { get; set; }
        public string ExportValue { get; set; }
        public bool IsFieldValueSet { get; set; }
        public string FieldValue { get; set; }
        public KeyValuePair<string, AcroFields.Item> FieldEntry { get; set; }

        public string fieldinfo()
        {
            string s = null;
            if (FormFieldType is PDFCheckBoxFieldType)
            {
                s = (string.Format("{0} - {1} - Export Value: {2}",
                                                    Key,
                                                    FormFieldType,
                                                    ExportValue));
            }
            else
            {
                s = (string.Format("{0} - {1}", Key, FormFieldType));
            }
            return s;
        }

    }
}