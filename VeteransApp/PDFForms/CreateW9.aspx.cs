﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class CreateW9 : System.Web.UI.Page
{
    protected void btnGeneratePDF_Click(object sender, EventArgs e)
    {
        var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/fw9.pdf"));

        // Get the form fields for this PDF and fill them in!
        var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
        formFieldMap["topmostSubform[0].Page1[0].f1_01_0_[0]"] = txtName.Text;
        formFieldMap["topmostSubform[0].Page1[0].f1_02_0_[0]"] = txtBusinessName.Text;

        if (rblTaxClassification.SelectedValue != null)
        {
            var formFieldName = string.Format("topmostSubform[0].Page1[0].c1_01[{0}]", rblTaxClassification.SelectedIndex);
            formFieldMap[formFieldName] = (rblTaxClassification.SelectedIndex + 1).ToString();
        }

        if (chkExemptPayee.Checked)
            formFieldMap["topmostSubform[0].Page1[0].c1_01[7]"] = "8";
            

        formFieldMap["topmostSubform[0].Page1[0].f1_04_0_[0]"] = txtAddress.Text;
        formFieldMap["topmostSubform[0].Page1[0].f1_05_0_[0]"] = txtCityStateZIP.Text;
        formFieldMap["topmostSubform[0].Page1[0].f1_07_0_[0]"] = txtAccountNumbers.Text;

        // Requester's name and address (hard-coded)
        formFieldMap["topmostSubform[0].Page1[0].f1_06_0_[0]"] = "Acme Website\n123 Anywhere Lane\nSpringfield, USA";

        // SSN
        if (!string.IsNullOrEmpty(txtSSN1.Text))
        {
            formFieldMap["topmostSubform[0].Page1[0].social[0].TextField1[0]"] = txtSSN1.Text;
            formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[0]"] = txtSSN2.Text;
            formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[1]"] = txtSSN3.Text;
        }
        else if (!string.IsNullOrEmpty(txtEIN1.Text))
        {
            formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[2]"] = txtEIN1.Text;
            formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[3]"] = txtEIN2.Text;
        }

        var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);

        PDFHelper.ReturnPDF(pdfContents, "Completed-W9.pdf");
    }
}