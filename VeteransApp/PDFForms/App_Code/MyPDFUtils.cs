using System.Collections;
using System.Collections.Generic;

public class MyPDFField
{
    public int index { get; set; }
    public float left = 0;
    public float right = 0;
    public float top = 0;
    public float bottom = 0;
    public PDFFieldType fieldType = null;
    public DictionaryEntry entry;
    public float[] fieldPosition;
    public string exportValue { get; set; }
    public bool isCheckBox { get; set; }
    public bool isTextBox { get; set; }

    public int leftInt = 0;
    public int rightInt = 0;
    public int topInt = 0;
    public int bottomInt = 0;

}

public class AssociatedField
{
    public MyPDFField OldPDFField { get; set; }
    public MyPDFField ConvertedPDFField { get; set; }
}

