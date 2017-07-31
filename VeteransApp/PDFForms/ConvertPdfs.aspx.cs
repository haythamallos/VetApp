using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConvertPdfs : System.Web.UI.Page
{
    public static readonly string TEMPLATE_PATH = "~/PDFTemplates";
    public static readonly string TEMPLATE_PATH_CONVERTED = "~/PDFTemplatesConverted";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var pdfs = Directory.GetFiles(Server.MapPath(TEMPLATE_PATH), "*.pdf");

            foreach (var pdf in pdfs)
                ddlPDFs.Items.Add(Path.GetFileName(pdf));

            pdfs = Directory.GetFiles(Server.MapPath(TEMPLATE_PATH_CONVERTED), "*.pdf");
            foreach (var pdf in pdfs)
                ddlPDFsConverted.Items.Add(Path.GetFileName(pdf));

        }
    }
   
    //protected void btnShowFields_Click(object sender, EventArgs e)
    //{
    //    var pdfPath = Path.Combine(Server.MapPath(TEMPLATE_PATH), ddlPDFs.SelectedValue);
    //    var fieldInfo = GenerateFormFields(pdfPath);
    //    // Get the form fields for this PDF and bind them to the BulletedList control
    //    blFields.DataSource = fieldInfo;
    //    blFields.DataBind();

    //}
    //protected void btnShowFieldsConverted_Click(object sender, EventArgs e)
    //{
    //    var pdfPath = Path.Combine(Server.MapPath(TEMPLATE_PATH_CONVERTED), ddlPDFs.SelectedValue);
    //    var fieldInfo = GenerateFormFields(pdfPath);
    //    // Get the form fields for this PDF and bind them to the BulletedList control
    //    blFields.DataSource = fieldInfo;
    //    blFields.DataBind();

    //}
    //private List<string> GenerateFormFields(string pdfPath)
    //{
    //    var fieldInfo = new List<string>();

    //    var reader = new PdfReader(pdfPath);
    //    var formFields = reader.AcroFields;

    //    float[] fieldPosition = null;
    //    foreach (DictionaryEntry entry in formFields.Fields)
    //    {
    //        var formFieldType = PDFFieldType.GetPDFFieldType(formFields.GetFieldType(entry.Key.ToString()));

    //        fieldPosition = formFields.GetFieldPositions(entry.Key.ToString());
    //        float left = fieldPosition[1];
    //        float right = fieldPosition[3];
    //        float top = fieldPosition[4];
    //        float bottom = fieldPosition[2];

    //        if (formFieldType is PDFCheckBoxFieldType)
    //        {
    //            fieldInfo.Add(string.Format("{0} - {1} - Export Value: {2}",
    //                                        entry.Key,
    //                                        formFieldType,
    //                                        PDFHelper.GetExportValue(entry.Value as AcroFields.Item)));
    //        }
    //        else
    //        {
    //            fieldInfo.Add(string.Format("{0} - {1}", entry.Key, formFieldType));
    //        }
    //    }
    //    reader.Close();
    //    return fieldInfo;
    //}



    //protected void btnGenerateSampleConverted_Click(object sender, EventArgs e)
    //{
    //    var pdfPath = Path.Combine(Server.MapPath(TEMPLATE_PATH_CONVERTED), ddlPDFs.SelectedValue);

    //    // Get the form fields for this PDF and give them increasing values
    //    var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);

    //    var counter = 1;
    //    foreach (var key in new List<string>(formFieldMap.Keys))
    //    {
    //        formFieldMap[key] = counter.ToString();
    //        counter++;
    //    }


    //    var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);

    //    PDFHelper.ReturnPDF(pdfContents, ddlPDFs.SelectedValue + "-Sample.pdf");
    //}

    private List<MyPDFField> GenerateFMyFields(string pdfPath)
    {
        List<MyPDFField> myPDFFields = new List<MyPDFField>();

        var fieldInfo = new List<string>();
        var reader = new PdfReader(pdfPath);
        var formFields = reader.AcroFields;
        float[] fieldPosition = null;
        MyPDFField myfield = null;
        int counter = 0;
        foreach (DictionaryEntry entry in formFields.Fields)
        {
            counter++;
            fieldPosition = formFields.GetFieldPositions(entry.Key.ToString());
            float left = fieldPosition[1];
            float right = fieldPosition[3];
            float top = fieldPosition[4];
            float bottom = fieldPosition[2];
            var formFieldType = PDFFieldType.GetPDFFieldType(formFields.GetFieldType(entry.Key.ToString()));

            myfield = new MyPDFField();
            myfield.fieldType = formFieldType;
            myfield.fieldPosition = fieldPosition;

            myfield.left = left;
            myfield.right = right;
            myfield.top = top;
            myfield.bottom = bottom;

            myfield.leftInt = (int) left;
            myfield.rightInt = (int)right;
            myfield.topInt = (int)top;
            myfield.bottomInt = (int)bottom;

            if (formFieldType is PDFCheckBoxFieldType)
            {
                myfield.isCheckBox = true;
                myfield.exportValue = PDFHelper.GetExportValue(entry.Value as AcroFields.Item);
            }
            else if (formFieldType is PDFTextFieldType)
            {
                myfield.isTextBox = true;
            }
            myfield.index = counter;
            myfield.entry = entry;
            myPDFFields.Add(myfield);
        }
        reader.Close();
        return myPDFFields;
    }

    protected void btnGenerateSampleConverted_Click(object sender, EventArgs e)
    {
        try
        {
            var pdfPathOld = Path.Combine(Server.MapPath(TEMPLATE_PATH), ddlPDFs.SelectedValue);
            var pdfPathConverted = Path.Combine(Server.MapPath(TEMPLATE_PATH_CONVERTED), ddlPDFs.SelectedValue);

            List<MyPDFField> lstMyPDFFieldOld = GenerateFMyFields(pdfPathOld);
            List<MyPDFField> lstMyPDFFieldConverted = GenerateFMyFields(pdfPathConverted);

            var pdfContents = GeneratePDF(pdfPathOld, lstMyPDFFieldOld);
            PDFHelper.ReturnPDF(pdfContents, ddlPDFs.SelectedValue + "-Sample.pdf");

            //MyPDFField foundField = null;
            //foreach(MyPDFField myfield in lstMyPDFFieldOld)
            //{
            //    foundField = lstMyPDFFieldConverted.FirstOrDefault(x =>( (x.leftInt == myfield.leftInt)
            //    && (x.rightInt == myfield.rightInt)
            //    && (x.topInt == myfield.topInt)
            //    && (x.bottomInt == myfield.bottomInt)
            //    ));
            //}
            //List<AssociatedField> lstAssociatedField = new List<AssociatedField>();

        }
        catch (Exception ex)
        {

        }
    }

    public static byte[] GeneratePDF(string pdfPath, List<MyPDFField> lstMyPDFField)
    {
        var output = new MemoryStream();
        var reader = new PdfReader(pdfPath);
        var stamper = new PdfStamper(reader, output);
        var formFields = stamper.AcroFields;

        int counter = 0;
        foreach(MyPDFField f in lstMyPDFField)
        {
            counter++;
            if (f.isCheckBox)
            {
                formFields.SetField(f.entry.Key.ToString(), f.exportValue);
            }
            else if (f.isTextBox)
            {
                formFields.SetField(f.entry.Key.ToString(), counter.ToString());
            }
        }
        //foreach (var fieldName in formFieldMap.Keys)
        //    formFields.SetField(fieldName, formFieldMap[fieldName]);

        //stamper.FormFlattening = true;
        stamper.Close();
        reader.Close();

        return output.ToArray();
    }
}