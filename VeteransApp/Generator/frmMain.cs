using FileHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

//pooling=true;min pool size=3;max pool size=5;Initial Catalog=MDNDBExport;Data Source=devsql1;User ID=mdndbuser;Password=mdndbuser;
//pooling=true;min pool size=3;max pool size=5;Initial Catalog=MdnDBDev;Data Source=devsql\devsql;User ID=mdndbuser;Password=mdndbuser;
//pooling=true;min pool size=3;max pool size=5;Data Source=devsql\devsql;Initial Catalog=BenMDN;Persist Security Info=True;User ID=benuser;Password=benuser
//pooling=true;min pool size=3;max pool size=5;Initial Catalog=mdndb2;Data Source=DWHALLOS2\MYSQLEXP2008R2;User ID=mdndbuser;Password=mdndbuser;
//pooling=true;min pool size=3;max pool size=5;Initial Catalog=Dev DB Apr 3 2012;Data Source=devsql\devsql;User ID=DWPE;Password=dwpe;
//pooling=true;min pool size=3;max pool size=5;Initial Catalog=Mini Trace;Data Source=devsql1;User ID=DWPE;Password=dwpe;
//pooling=true;min pool size=3;max pool size=5;Initial Catalog=ReportThisDB-Snap-08-12-2013;Data Source=devsql1;User ID=mdndbuser;Password=mdndbuser;

namespace CodeGenerator
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class frmMain : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ListBox lstTables;
        private System.Data.SqlClient.SqlConnection con2;
        private System.Windows.Forms.RadioButton rbSelectedTables;
        private System.Windows.Forms.RadioButton rbAllTables;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.TextBox txtCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSQLDates;
        private System.Windows.Forms.TextBox txtSQLNumbers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSQLStrings;
        private System.Windows.Forms.Label lblbla;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSB;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cbAuto;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.GroupBox grpGenerate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnExists;
        private System.Windows.Forms.Button btnClass;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox chkWrite;
        private System.Windows.Forms.TextBox txtSQLTexts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEnum;
        private System.Windows.Forms.Button btnSpEnum;
        private System.Windows.Forms.DataGrid dg;
        private System.Windows.Forms.Button btnBiz;

        private static string DATA_NAMESPACE = "Vetapp.Engine.DataAccessLayer.Data.";
        private System.Windows.Forms.Button btnProxy;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnBizFac;
        private System.Windows.Forms.Button btnWebAccess;
        private Button btnGrant;
        private TextBox grantTextBox;
        private Button clearDBButton;
        private Button allSPButton;
        private GroupBox groupBox1;
        private Button grantSelectedButton;
        private Label label5;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label6;
        private TextBox solutionDirPathTextBox;
        private FileSystemWatcher mainFileSystemWatcher;

        private string currentSolDirName = null;
        private CheckBox nullableBoolCheckBox;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private Label label7;
        private TextBox defaultNullableBoolValueTextBox;

        private string _strBoolText = "bool";
        private Button btnBizValExpClass;
        private ComboBox txtCS;
        private Button button3;
        private Button button4;
        private Button btnFacCrud;
        private GroupBox groupBox4;
        private Button btnKeysSqlGenerate;
        private Label label8;
        private TextBox numApiKeysTextBox;
        private Button btnKeysGnerate;
        private GroupBox miscGroup;
        private Button genPDFArraysButton;
        private Button btnGo;
        private ComboBox cboDictionary;
        private Button realtorInfoButton;
        private Button btnAddAllBusFacCorGet;
        private Button getSQLProdDBButton;
        private string _strBoolDefaultValue = "false";

        public frmMain()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lstTables = new System.Windows.Forms.ListBox();
            this.btnSB = new System.Windows.Forms.Button();
            this.con2 = new System.Data.SqlClient.SqlConnection();
            this.rbSelectedTables = new System.Windows.Forms.RadioButton();
            this.rbAllTables = new System.Windows.Forms.RadioButton();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.miscGroup = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.cboDictionary = new System.Windows.Forms.ComboBox();
            this.genPDFArraysButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.getSQLProdDBButton = new System.Windows.Forms.Button();
            this.btnKeysSqlGenerate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numApiKeysTextBox = new System.Windows.Forms.TextBox();
            this.btnKeysGnerate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.defaultNullableBoolValueTextBox = new System.Windows.Forms.TextBox();
            this.nullableBoolCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSQLTexts = new System.Windows.Forms.TextBox();
            this.txtSQLStrings = new System.Windows.Forms.TextBox();
            this.lblbla = new System.Windows.Forms.Label();
            this.txtSQLNumbers = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSQLDates = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCB = new System.Windows.Forms.TextBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.cbAuto = new System.Windows.Forms.ComboBox();
            this.lblAuto = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpGenerate = new System.Windows.Forms.GroupBox();
            this.realtorInfoButton = new System.Windows.Forms.Button();
            this.btnBizValExpClass = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFacCrud = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.solutionDirPathTextBox = new System.Windows.Forms.TextBox();
            this.btnClass = new System.Windows.Forms.Button();
            this.btnBiz = new System.Windows.Forms.Button();
            this.btnEnum = new System.Windows.Forms.Button();
            this.btnProxy = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.clearDBButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grantSelectedButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.grantTextBox = new System.Windows.Forms.TextBox();
            this.btnGrant = new System.Windows.Forms.Button();
            this.allSPButton = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExists = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSpEnum = new System.Windows.Forms.Button();
            this.btnWebAccess = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnBizFac = new System.Windows.Forms.Button();
            this.chkWrite = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGrid();
            this.mainFileSystemWatcher = new System.IO.FileSystemWatcher();
            this.txtCS = new System.Windows.Forms.ComboBox();
            this.btnAddAllBusFacCorGet = new System.Windows.Forms.Button();
            this.grpOptions.SuspendLayout();
            this.miscGroup.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpGenerate.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(747, 68);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(689, 450);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.TextChanged += new System.EventHandler(this.txtOutput_TextChanged);
            // 
            // lstTables
            // 
            this.lstTables.Location = new System.Drawing.Point(12, 72);
            this.lstTables.Name = "lstTables";
            this.lstTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstTables.Size = new System.Drawing.Size(125, 186);
            this.lstTables.TabIndex = 2;
            this.lstTables.SelectedIndexChanged += new System.EventHandler(this.lstTables_SelectedIndexChanged);
            // 
            // btnSB
            // 
            this.btnSB.Location = new System.Drawing.Point(420, 522);
            this.btnSB.Name = "btnSB";
            this.btnSB.Size = new System.Drawing.Size(112, 20);
            this.btnSB.TabIndex = 6;
            this.btnSB.Text = "Add StringBuilder";
            this.btnSB.Visible = false;
            this.btnSB.Click += new System.EventHandler(this.button3_Click);
            // 
            // con2
            // 
            this.con2.FireInfoMessageEventOnUserErrors = false;
            // 
            // rbSelectedTables
            // 
            this.rbSelectedTables.Checked = true;
            this.rbSelectedTables.Location = new System.Drawing.Point(12, 158);
            this.rbSelectedTables.Name = "rbSelectedTables";
            this.rbSelectedTables.Size = new System.Drawing.Size(100, 16);
            this.rbSelectedTables.TabIndex = 7;
            this.rbSelectedTables.TabStop = true;
            this.rbSelectedTables.Text = "Selected Table";
            // 
            // rbAllTables
            // 
            this.rbAllTables.Location = new System.Drawing.Point(117, 157);
            this.rbAllTables.Name = "rbAllTables";
            this.rbAllTables.Size = new System.Drawing.Size(75, 16);
            this.rbAllTables.TabIndex = 8;
            this.rbAllTables.Text = "All Tables";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.miscGroup);
            this.grpOptions.Controls.Add(this.groupBox4);
            this.grpOptions.Controls.Add(this.label7);
            this.grpOptions.Controls.Add(this.defaultNullableBoolValueTextBox);
            this.grpOptions.Controls.Add(this.nullableBoolCheckBox);
            this.grpOptions.Controls.Add(this.label4);
            this.grpOptions.Controls.Add(this.txtSQLTexts);
            this.grpOptions.Controls.Add(this.txtSQLStrings);
            this.grpOptions.Controls.Add(this.lblbla);
            this.grpOptions.Controls.Add(this.txtSQLNumbers);
            this.grpOptions.Controls.Add(this.label3);
            this.grpOptions.Controls.Add(this.txtSQLDates);
            this.grpOptions.Controls.Add(this.label2);
            this.grpOptions.Controls.Add(this.txtCB);
            this.grpOptions.Controls.Add(this.lblCreatedBy);
            this.grpOptions.Controls.Add(this.rbSelectedTables);
            this.grpOptions.Controls.Add(this.cbAuto);
            this.grpOptions.Controls.Add(this.lblAuto);
            this.grpOptions.Controls.Add(this.rbAllTables);
            this.grpOptions.Location = new System.Drawing.Point(143, 48);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(589, 176);
            this.grpOptions.TabIndex = 9;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // miscGroup
            // 
            this.miscGroup.Controls.Add(this.btnGo);
            this.miscGroup.Controls.Add(this.cboDictionary);
            this.miscGroup.Controls.Add(this.genPDFArraysButton);
            this.miscGroup.Location = new System.Drawing.Point(225, 67);
            this.miscGroup.Name = "miscGroup";
            this.miscGroup.Size = new System.Drawing.Size(357, 38);
            this.miscGroup.TabIndex = 32;
            this.miscGroup.TabStop = false;
            this.miscGroup.Text = "Misc Util";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(313, 11);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(35, 20);
            this.btnGo.TabIndex = 31;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cboDictionary
            // 
            this.cboDictionary.FormattingEnabled = true;
            this.cboDictionary.Items.AddRange(new object[] {
            "timeWords0.txt",
            "badShortCuts0.txt",
            "realEstateTerms0.txt",
            "realEstateAdjectives0.txt",
            "prepositions0.txt",
            "generalAdjectives0.txt",
            "conjunctions0.txt",
            "badDictionary0.txt",
            "numbersWords0.txt"});
            this.cboDictionary.Location = new System.Drawing.Point(138, 11);
            this.cboDictionary.Name = "cboDictionary";
            this.cboDictionary.Size = new System.Drawing.Size(159, 21);
            this.cboDictionary.TabIndex = 30;
            this.cboDictionary.Text = "timeWords0.txt";
            // 
            // genPDFArraysButton
            // 
            this.genPDFArraysButton.Location = new System.Drawing.Point(8, 11);
            this.genPDFArraysButton.Name = "genPDFArraysButton";
            this.genPDFArraysButton.Size = new System.Drawing.Size(115, 24);
            this.genPDFArraysButton.TabIndex = 29;
            this.genPDFArraysButton.Tag = "8";
            this.genPDFArraysButton.Text = "Gen PDF Arrays";
            this.genPDFArraysButton.Click += new System.EventHandler(this.genPDFArraysButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.getSQLProdDBButton);
            this.groupBox4.Controls.Add(this.btnKeysSqlGenerate);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.numApiKeysTextBox);
            this.groupBox4.Controls.Add(this.btnKeysGnerate);
            this.groupBox4.Location = new System.Drawing.Point(203, 118);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(385, 55);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "API Auth Keys";
            // 
            // getSQLProdDBButton
            // 
            this.getSQLProdDBButton.Location = new System.Drawing.Point(292, 16);
            this.getSQLProdDBButton.Name = "getSQLProdDBButton";
            this.getSQLProdDBButton.Size = new System.Drawing.Size(84, 35);
            this.getSQLProdDBButton.TabIndex = 30;
            this.getSQLProdDBButton.Tag = "0";
            this.getSQLProdDBButton.Text = "Gen SQL ProdDB";
            this.getSQLProdDBButton.Click += new System.EventHandler(this.getSQLProdDBButton_Click);
            // 
            // btnKeysSqlGenerate
            // 
            this.btnKeysSqlGenerate.Location = new System.Drawing.Point(227, 16);
            this.btnKeysSqlGenerate.Name = "btnKeysSqlGenerate";
            this.btnKeysSqlGenerate.Size = new System.Drawing.Size(62, 24);
            this.btnKeysSqlGenerate.TabIndex = 28;
            this.btnKeysSqlGenerate.Tag = "8";
            this.btnKeysSqlGenerate.Text = "Gen SQL";
            this.btnKeysSqlGenerate.Click += new System.EventHandler(this.btnKeysSqlGenerate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "#";
            // 
            // numApiKeysTextBox
            // 
            this.numApiKeysTextBox.Location = new System.Drawing.Point(30, 19);
            this.numApiKeysTextBox.Name = "numApiKeysTextBox";
            this.numApiKeysTextBox.Size = new System.Drawing.Size(102, 20);
            this.numApiKeysTextBox.TabIndex = 27;
            this.numApiKeysTextBox.Text = "1";
            this.numApiKeysTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnKeysGnerate
            // 
            this.btnKeysGnerate.Location = new System.Drawing.Point(137, 16);
            this.btnKeysGnerate.Name = "btnKeysGnerate";
            this.btnKeysGnerate.Size = new System.Drawing.Size(85, 24);
            this.btnKeysGnerate.TabIndex = 26;
            this.btnKeysGnerate.Tag = "8";
            this.btnKeysGnerate.Text = "Gen Keys";
            this.btnKeysGnerate.Click += new System.EventHandler(this.btnKeysGnerate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Default Nullable Bool Value";
            // 
            // defaultNullableBoolValueTextBox
            // 
            this.defaultNullableBoolValueTextBox.Location = new System.Drawing.Point(223, 37);
            this.defaultNullableBoolValueTextBox.Name = "defaultNullableBoolValueTextBox";
            this.defaultNullableBoolValueTextBox.Size = new System.Drawing.Size(109, 20);
            this.defaultNullableBoolValueTextBox.TabIndex = 22;
            this.defaultNullableBoolValueTextBox.Text = "null";
            // 
            // nullableBoolCheckBox
            // 
            this.nullableBoolCheckBox.AutoSize = true;
            this.nullableBoolCheckBox.Checked = true;
            this.nullableBoolCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nullableBoolCheckBox.Location = new System.Drawing.Point(337, 40);
            this.nullableBoolCheckBox.Name = "nullableBoolCheckBox";
            this.nullableBoolCheckBox.Size = new System.Drawing.Size(88, 17);
            this.nullableBoolCheckBox.TabIndex = 21;
            this.nullableBoolCheckBox.Text = "Nullable Bool";
            this.nullableBoolCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "SQL Text";
            // 
            // txtSQLTexts
            // 
            this.txtSQLTexts.Location = new System.Drawing.Point(83, 110);
            this.txtSQLTexts.Name = "txtSQLTexts";
            this.txtSQLTexts.Size = new System.Drawing.Size(109, 20);
            this.txtSQLTexts.TabIndex = 19;
            this.txtSQLTexts.Text = "TEXT";
            // 
            // txtSQLStrings
            // 
            this.txtSQLStrings.Location = new System.Drawing.Point(83, 85);
            this.txtSQLStrings.Name = "txtSQLStrings";
            this.txtSQLStrings.Size = new System.Drawing.Size(120, 20);
            this.txtSQLStrings.TabIndex = 16;
            this.txtSQLStrings.Text = "VARCHAR(SIZE)";
            // 
            // lblbla
            // 
            this.lblbla.AutoSize = true;
            this.lblbla.Location = new System.Drawing.Point(18, 89);
            this.lblbla.Name = "lblbla";
            this.lblbla.Size = new System.Drawing.Size(63, 13);
            this.lblbla.TabIndex = 15;
            this.lblbla.Text = "SQL Strings";
            // 
            // txtSQLNumbers
            // 
            this.txtSQLNumbers.Location = new System.Drawing.Point(83, 61);
            this.txtSQLNumbers.Name = "txtSQLNumbers";
            this.txtSQLNumbers.Size = new System.Drawing.Size(120, 20);
            this.txtSQLNumbers.TabIndex = 14;
            this.txtSQLNumbers.Text = "NUMERIC(SIZE,PREC)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "SQL Numbers";
            // 
            // txtSQLDates
            // 
            this.txtSQLDates.Location = new System.Drawing.Point(83, 40);
            this.txtSQLDates.Name = "txtSQLDates";
            this.txtSQLDates.Size = new System.Drawing.Size(120, 20);
            this.txtSQLDates.TabIndex = 12;
            this.txtSQLDates.Text = "SMALLDATETIME";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "SQL Dates:";
            // 
            // txtCB
            // 
            this.txtCB.Location = new System.Drawing.Point(83, 17);
            this.txtCB.Name = "txtCB";
            this.txtCB.Size = new System.Drawing.Size(29, 20);
            this.txtCB.TabIndex = 10;
            this.txtCB.Text = "HA";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(20, 21);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(62, 13);
            this.lblCreatedBy.TabIndex = 9;
            this.lblCreatedBy.Text = "Created By:";
            // 
            // cbAuto
            // 
            this.cbAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuto.Items.AddRange(new object[] {
            "Ask",
            "Yes",
            "No"});
            this.cbAuto.Location = new System.Drawing.Point(97, 131);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(71, 21);
            this.cbAuto.TabIndex = 17;
            // 
            // lblAuto
            // 
            this.lblAuto.AutoSize = true;
            this.lblAuto.Location = new System.Drawing.Point(17, 135);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(82, 13);
            this.lblAuto.TabIndex = 18;
            this.lblAuto.Text = "Auto-Increment:";
            // 
            // lblOutput
            // 
            this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutput.BackColor = System.Drawing.Color.Transparent;
            this.lblOutput.Location = new System.Drawing.Point(743, 50);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(868, 16);
            this.lblOutput.TabIndex = 10;
            this.lblOutput.Text = "Output";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Connection String";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(13, 50);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 20);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "Refresh Tables";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(990, 524);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(123, 20);
            this.btnCopy.TabIndex = 14;
            this.btnCopy.Text = "Copy All to Clipboard";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(903, 524);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 20);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAll
            // 
            this.btnAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll.Location = new System.Drawing.Point(153, 522);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(29, 20);
            this.btnAll.TabIndex = 17;
            this.btnAll.Text = "All";
            this.btnAll.Visible = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNone.Location = new System.Drawing.Point(183, 522);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(40, 20);
            this.btnNone.TabIndex = 18;
            this.btnNone.Text = "None";
            this.btnNone.Visible = false;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(312, 522);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 20);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit                ";
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpGenerate
            // 
            this.grpGenerate.Controls.Add(this.realtorInfoButton);
            this.grpGenerate.Controls.Add(this.btnBizValExpClass);
            this.grpGenerate.Controls.Add(this.groupBox3);
            this.grpGenerate.Controls.Add(this.clearDBButton);
            this.grpGenerate.Controls.Add(this.groupBox2);
            this.grpGenerate.Controls.Add(this.btnWebAccess);
            this.grpGenerate.Controls.Add(this.btnCreate);
            this.grpGenerate.Controls.Add(this.btnBizFac);
            this.grpGenerate.Location = new System.Drawing.Point(143, 224);
            this.grpGenerate.Name = "grpGenerate";
            this.grpGenerate.Size = new System.Drawing.Size(597, 292);
            this.grpGenerate.TabIndex = 20;
            this.grpGenerate.TabStop = false;
            this.grpGenerate.Text = "Generate";
            // 
            // realtorInfoButton
            // 
            this.realtorInfoButton.Location = new System.Drawing.Point(188, 256);
            this.realtorInfoButton.Name = "realtorInfoButton";
            this.realtorInfoButton.Size = new System.Drawing.Size(84, 24);
            this.realtorInfoButton.TabIndex = 24;
            this.realtorInfoButton.Tag = "0";
            this.realtorInfoButton.Text = "Get Realtor Info";
            // 
            // btnBizValExpClass
            // 
            this.btnBizValExpClass.Location = new System.Drawing.Point(467, 207);
            this.btnBizValExpClass.Name = "btnBizValExpClass";
            this.btnBizValExpClass.Size = new System.Drawing.Size(95, 23);
            this.btnBizValExpClass.TabIndex = 29;
            this.btnBizValExpClass.Tag = "21";
            this.btnBizValExpClass.Text = "BizValExp Class";
            this.btnBizValExpClass.UseVisualStyleBackColor = true;
            this.btnBizValExpClass.Click += new System.EventHandler(this.btn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnFacCrud);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.solutionDirPathTextBox);
            this.groupBox3.Controls.Add(this.btnClass);
            this.groupBox3.Controls.Add(this.btnBiz);
            this.groupBox3.Controls.Add(this.btnEnum);
            this.groupBox3.Controls.Add(this.btnProxy);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(12, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(413, 115);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Classes";
            // 
            // btnFacCrud
            // 
            this.btnFacCrud.Location = new System.Drawing.Point(307, 41);
            this.btnFacCrud.Name = "btnFacCrud";
            this.btnFacCrud.Size = new System.Drawing.Size(93, 24);
            this.btnFacCrud.TabIndex = 30;
            this.btnFacCrud.Tag = "9";
            this.btnFacCrud.Text = "FacCRUD";
            this.btnFacCrud.Click += new System.EventHandler(this.btnFacCrud_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Solution Dir Path";
            this.label6.Visible = false;
            // 
            // solutionDirPathTextBox
            // 
            this.solutionDirPathTextBox.Location = new System.Drawing.Point(107, 13);
            this.solutionDirPathTextBox.Name = "solutionDirPathTextBox";
            this.solutionDirPathTextBox.Size = new System.Drawing.Size(295, 20);
            this.solutionDirPathTextBox.TabIndex = 29;
            this.solutionDirPathTextBox.Visible = false;
            // 
            // btnClass
            // 
            this.btnClass.Location = new System.Drawing.Point(7, 41);
            this.btnClass.Name = "btnClass";
            this.btnClass.Size = new System.Drawing.Size(91, 24);
            this.btnClass.TabIndex = 5;
            this.btnClass.Tag = "6";
            this.btnClass.Text = ".cs Data Class";
            this.btnClass.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnBiz
            // 
            this.btnBiz.Location = new System.Drawing.Point(207, 41);
            this.btnBiz.Name = "btnBiz";
            this.btnBiz.Size = new System.Drawing.Size(95, 24);
            this.btnBiz.TabIndex = 12;
            this.btnBiz.Tag = "9";
            this.btnBiz.Text = ".cs Biz Class";
            this.btnBiz.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnEnum
            // 
            this.btnEnum.Location = new System.Drawing.Point(107, 41);
            this.btnEnum.Name = "btnEnum";
            this.btnEnum.Size = new System.Drawing.Size(95, 24);
            this.btnEnum.TabIndex = 10;
            this.btnEnum.Tag = "7";
            this.btnEnum.Text = ".cs Enum Class";
            this.btnEnum.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnProxy
            // 
            this.btnProxy.Location = new System.Drawing.Point(8, 72);
            this.btnProxy.Name = "btnProxy";
            this.btnProxy.Size = new System.Drawing.Size(94, 24);
            this.btnProxy.TabIndex = 13;
            this.btnProxy.Tag = "10";
            this.btnProxy.Text = ".cs Proxy Class";
            this.btnProxy.Click += new System.EventHandler(this.btn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(108, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(194, 24);
            this.button2.TabIndex = 23;
            this.button2.Tag = "11";
            this.button2.Text = ".cs Enum Proxy Class";
            this.button2.Click += new System.EventHandler(this.btn_Click);
            // 
            // clearDBButton
            // 
            this.clearDBButton.Location = new System.Drawing.Point(102, 256);
            this.clearDBButton.Name = "clearDBButton";
            this.clearDBButton.Size = new System.Drawing.Size(83, 24);
            this.clearDBButton.TabIndex = 28;
            this.clearDBButton.Tag = "8";
            this.clearDBButton.Text = "Clear DB";
            this.clearDBButton.Visible = false;
            this.clearDBButton.Click += new System.EventHandler(this.clearDBButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.allSPButton);
            this.groupBox2.Controls.Add(this.btnInsert);
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.btnExists);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnSpEnum);
            this.groupBox2.Location = new System.Drawing.Point(7, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 110);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stored Procedures";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(98, 49);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 55);
            this.button4.TabIndex = 32;
            this.button4.Tag = "23";
            this.button4.Text = "All SPs Enhanced";
            this.button4.Click += new System.EventHandler(this.btn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(312, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 24);
            this.button3.TabIndex = 31;
            this.button3.Tag = "22";
            this.button3.Text = "SP Enum Enhanced";
            this.button3.Click += new System.EventHandler(this.btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grantSelectedButton);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.grantTextBox);
            this.groupBox1.Controls.Add(this.btnGrant);
            this.groupBox1.Location = new System.Drawing.Point(195, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 55);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Permission (Grant)";
            // 
            // grantSelectedButton
            // 
            this.grantSelectedButton.Location = new System.Drawing.Point(268, 16);
            this.grantSelectedButton.Name = "grantSelectedButton";
            this.grantSelectedButton.Size = new System.Drawing.Size(104, 24);
            this.grantSelectedButton.TabIndex = 28;
            this.grantSelectedButton.Tag = "8";
            this.grantSelectedButton.Text = "Grant Selected";
            this.grantSelectedButton.Click += new System.EventHandler(this.grantSelectedButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Username";
            // 
            // grantTextBox
            // 
            this.grantTextBox.Location = new System.Drawing.Point(72, 19);
            this.grantTextBox.Name = "grantTextBox";
            this.grantTextBox.Size = new System.Drawing.Size(100, 20);
            this.grantTextBox.TabIndex = 27;
            // 
            // btnGrant
            // 
            this.btnGrant.Location = new System.Drawing.Point(178, 16);
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(85, 24);
            this.btnGrant.TabIndex = 26;
            this.btnGrant.Tag = "8";
            this.btnGrant.Text = "Grant All";
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            // 
            // allSPButton
            // 
            this.allSPButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allSPButton.Location = new System.Drawing.Point(8, 49);
            this.allSPButton.Name = "allSPButton";
            this.allSPButton.Size = new System.Drawing.Size(85, 55);
            this.allSPButton.TabIndex = 29;
            this.allSPButton.Tag = "20";
            this.allSPButton.Text = "All SPs";
            this.allSPButton.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(513, 19);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(65, 24);
            this.btnInsert.TabIndex = 7;
            this.btnInsert.Tag = "5";
            this.btnInsert.Text = "SP Insert";
            this.btnInsert.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(8, 19);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(69, 24);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Tag = "2";
            this.btnLoad.Text = "SP Load";
            this.btnLoad.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(85, 19);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(73, 24);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Tag = "3";
            this.btnUpdate.Text = "SP Update";
            this.btnUpdate.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnExists
            // 
            this.btnExists.Location = new System.Drawing.Point(163, 19);
            this.btnExists.Name = "btnExists";
            this.btnExists.Size = new System.Drawing.Size(74, 24);
            this.btnExists.TabIndex = 0;
            this.btnExists.Tag = "1";
            this.btnExists.Text = "SP Exists";
            this.btnExists.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(435, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 24);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Tag = "4";
            this.btnDelete.Text = "SP Delete";
            this.btnDelete.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSpEnum
            // 
            this.btnSpEnum.Location = new System.Drawing.Point(243, 19);
            this.btnSpEnum.Name = "btnSpEnum";
            this.btnSpEnum.Size = new System.Drawing.Size(62, 24);
            this.btnSpEnum.TabIndex = 11;
            this.btnSpEnum.Tag = "8";
            this.btnSpEnum.Text = "SP Enum";
            this.btnSpEnum.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnWebAccess
            // 
            this.btnWebAccess.Location = new System.Drawing.Point(467, 176);
            this.btnWebAccess.Name = "btnWebAccess";
            this.btnWebAccess.Size = new System.Drawing.Size(95, 24);
            this.btnWebAccess.TabIndex = 25;
            this.btnWebAccess.Tag = "13";
            this.btnWebAccess.Text = "Web Acc Class";
            this.btnWebAccess.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(12, 256);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(83, 24);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Tag = "0";
            this.btnCreate.Text = "Create Table";
            this.btnCreate.Visible = false;
            this.btnCreate.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnBizFac
            // 
            this.btnBizFac.Location = new System.Drawing.Point(467, 144);
            this.btnBizFac.Name = "btnBizFac";
            this.btnBizFac.Size = new System.Drawing.Size(95, 24);
            this.btnBizFac.TabIndex = 24;
            this.btnBizFac.Tag = "12";
            this.btnBizFac.Text = "Biz Fac Class";
            this.btnBizFac.Click += new System.EventHandler(this.btn_Click);
            // 
            // chkWrite
            // 
            this.chkWrite.Location = new System.Drawing.Point(1027, 50);
            this.chkWrite.Name = "chkWrite";
            this.chkWrite.Size = new System.Drawing.Size(86, 16);
            this.chkWrite.TabIndex = 9;
            this.chkWrite.Text = "Write To File";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(230, 522);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 21;
            this.button1.Text = "Test Center";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dg
            // 
            this.dg.DataMember = "";
            this.dg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dg.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dg.Location = new System.Drawing.Point(0, 773);
            this.dg.Name = "dg";
            this.dg.Size = new System.Drawing.Size(1436, 100);
            this.dg.TabIndex = 22;
            // 
            // mainFileSystemWatcher
            // 
            this.mainFileSystemWatcher.EnableRaisingEvents = true;
            this.mainFileSystemWatcher.Filter = "*.cs";
            this.mainFileSystemWatcher.SynchronizingObject = this;
            this.mainFileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.mainFileSystemWatcher_Changed);
            this.mainFileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.mainFileSystemWatcher_Created);
            // 
            // txtCS
            // 
            this.txtCS.FormattingEnabled = true;
            this.txtCS.Items.AddRange(new object[] {
            "Data Source=HAYTHAMALLO38E3\\SQLEXPRESS;Initial Catalog=localvetdb;Persist Securit" +
                "y Info=True;User ID=vetdbuser;Password=vetdbuser;",
            "Data Source=52.8.194.212;Initial Catalog=vetappmaindbprod;Persist Security Info=T" +
                "rue;User ID=vetappproddbuser;Password=wRax57uswENADrAc;"});
            this.txtCS.Location = new System.Drawing.Point(13, 23);
            this.txtCS.Name = "txtCS";
            this.txtCS.Size = new System.Drawing.Size(1100, 21);
            this.txtCS.TabIndex = 23;
            this.txtCS.SelectedIndexChanged += new System.EventHandler(this.txtCS_SelectedIndexChanged);
            // 
            // btnAddAllBusFacCorGet
            // 
            this.btnAddAllBusFacCorGet.Location = new System.Drawing.Point(545, 522);
            this.btnAddAllBusFacCorGet.Name = "btnAddAllBusFacCorGet";
            this.btnAddAllBusFacCorGet.Size = new System.Drawing.Size(129, 20);
            this.btnAddAllBusFacCorGet.TabIndex = 24;
            this.btnAddAllBusFacCorGet.Tag = "24";
            this.btnAddAllBusFacCorGet.Text = "Add All BusCoreGet";
            this.btnAddAllBusFacCorGet.Click += new System.EventHandler(this.btn_Click);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1436, 873);
            this.Controls.Add(this.btnAddAllBusFacCorGet);
            this.Controls.Add(this.txtCS);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.chkWrite);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpGenerate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.btnSB);
            this.Controls.Add(this.lstTables);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "T-SQL / CLASS CREATION WIZARD";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.miscGroup.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpGenerate.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.Run(new frmMain());
        }

        /// <summary>
        /// Handles the Click event of the btn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btn_Click(object sender, System.EventArgs e)
        {
            if (this.nullableBoolCheckBox.Checked)
            {
                _strBoolText = "bool?";
                _strBoolDefaultValue = this.defaultNullableBoolValueTextBox.Text.Trim();
            }

            string solDirPath = this.solutionDirPathTextBox.Text.Trim();
            this.mainFileSystemWatcher.EnableRaisingEvents = false;
            if (solDirPath != String.Empty)
            {
                if (Directory.Exists(solDirPath))
                {
                    this.chkWrite.Checked = true;
                    this.mainFileSystemWatcher.Path = Application.StartupPath;
                    this.mainFileSystemWatcher.EnableRaisingEvents = true;
                }
            }
            bool Auto = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (rbAllTables.Checked)
            {
                foreach (string li in lstTables.Items)
                {
                    SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + li + "]", con2);
                    con2.Close();
                    if (con2.State == ConnectionState.Closed) con2.Open();
                    DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
                    try
                    {
                        Auto = dt.Select("ColumnName = '" + li + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";
                    }
                    catch { }
                    if (!li.ToLower().StartsWith("dtp"))
                    {
                        switch (((Button)sender).Tag.ToString())
                        {
                            case "0":
                                sb.Append(BuildCreate(li, Auto));
                                break;

                            case "1":
                                sb.Append(BuildExists(li));
                                break;

                            case "2":
                                sb.Append(BuildLoads(li));
                                break;

                            case "3":
                                sb.Append(BuildUpdates(li));
                                break;

                            case "4":
                                sb.Append(BuildDeletes(li));
                                break;

                            case "5":
                                sb.Append(BuildInserts(li));
                                break;

                            case "6":
                                currentSolDirName = "DataAccessLayer\\Data";
                                sb.Append(BuildClassFile(li));
                                break;

                            case "7":
                                currentSolDirName = "DataAccessLayer\\Enum";
                                sb.Append(BuildEnumClass(li));
                                break;

                            case "8":
                                sb.Append(BuildSpEnum(li));
                                break;

                            case "9":
                                currentSolDirName = "BusinessAccessLayer";
                                sb.Append(BuildBizClass(li));
                                break;

                            case "10":
                                currentSolDirName = "ProxyWin";
                                sb.Append(BuildProxyClass(li));
                                break;

                            case "11":
                                currentSolDirName = "ProxyWin";
                                sb.Append(BuildEnumProxyClass(li));
                                break;

                            case "12":
                                sb.Append(BuildBizFacade(li));
                                break;

                            case "13":
                                sb.Append(BuildWebServices(li));
                                break;

                            case "20":
                                sb.Append(BuildExists(li));
                                sb.Append(BuildLoads(li));
                                sb.Append(BuildUpdates(li));
                                sb.Append(BuildDeletes(li));
                                sb.Append(BuildInserts(li));
                                sb.Append(BuildSpEnum(li));
                                break;

                            case "21":
                                sb.Append(BuildBizValExpClass());
                                break;

                            case "22":
                                sb.Append(BuildSpEnumEnhanced(li));
                                break;

                            case "23":
                                MessageBox.Show(
                                    "This feature will work exactly like the All SP's button with the exception of creating an SP Enum Enhanced in place of SP Enum.",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                sb.Append(BuildExists(li));
                                sb.Append(BuildLoads(li));
                                sb.Append(BuildUpdates(li));
                                sb.Append(BuildDeletes(li));
                                sb.Append(BuildInserts(li));
                                sb.Append(BuildSpEnumEnhanced(li));
                                break;
                        }

                        if (chkWrite.Checked && ((Button)sender).Tag.ToString() == "6" || ((Button)sender).Tag.ToString() == "10")
                        {
                            // create a writer and open the file
                            TextWriter tw = new StreamWriter(CamCase(li) + ".cs");

                            // write a line of text to the file
                            tw.WriteLine(sb.ToString());

                            // close the stream
                            tw.Close();

                            sb = new System.Text.StringBuilder();
                        }
                        if (chkWrite.Checked && (((Button)sender).Tag.ToString() == "7" || ((Button)sender).Tag.ToString() == "11"))
                        {
                            // create a writer and open the file
                            TextWriter tw = new StreamWriter("Enum" + CamCase(li) + ".cs");

                            // write a line of text to the file
                            tw.WriteLine(sb.ToString());

                            // close the stream
                            tw.Close();

                            sb = new System.Text.StringBuilder();
                        }
                        if (chkWrite.Checked && ((Button)sender).Tag.ToString() == "9")
                        {
                            // create a writer and open the file
                            TextWriter tw = new StreamWriter("Bus" + CamCase(li) + ".cs");

                            // write a line of text to the file
                            tw.WriteLine(sb.ToString());

                            // close the stream
                            tw.Close();

                            sb = new System.Text.StringBuilder();
                        }
                    }
                }
            }
            else
            {
                if (lstTables.SelectedItems.Count == 0)
                    MessageBox.Show("You have not selected any tables!");

                foreach (string li in lstTables.SelectedItems)
                {
                    SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + li + "]", con2);
                    con2.Close();
                    if (con2.State == ConnectionState.Closed) con2.Open();
                    DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
                    Auto = dt.Select("ColumnName = '" + li + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";

                    switch (((Button)sender).Tag.ToString())
                    {
                        case "0":
                            sb.Append(BuildCreate(li, Auto));
                            break;

                        case "1":
                            sb.Append(BuildExists(li));
                            break;

                        case "2":
                            sb.Append(BuildLoads(li));
                            break;

                        case "3":
                            sb.Append(BuildUpdates(li));
                            break;

                        case "4":
                            sb.Append(BuildDeletes(li));
                            break;

                        case "5":
                            sb.Append(BuildInserts(li));
                            break;

                        case "6":
                            currentSolDirName = "DataAccessLayer\\Data";
                            sb.Append(BuildClassFile(li));
                            break;

                        case "7":
                            currentSolDirName = "DataAccessLayer\\Enum";
                            sb.Append(BuildEnumClass(li));
                            break;

                        case "8":
                            sb.Append(BuildSpEnum(li));
                            break;

                        case "9":
                            currentSolDirName = "BusinessAccessLayer";
                            sb.Append(BuildBizClass(li));
                            break;

                        case "10":
                            currentSolDirName = "ProxyWin";
                            sb.Append(BuildProxyClass(li));
                            break;

                        case "11":
                            currentSolDirName = "ProxyWin";
                            sb.Append(BuildEnumProxyClass(li));
                            break;

                        case "12":
                            sb.Append(BuildBizFacade(li));
                            break;

                        case "13":
                            sb.Append(BuildWebServices(li));
                            break;

                        case "20":
                            sb.Append(BuildExists(li));
                            sb.Append(BuildLoads(li));
                            sb.Append(BuildUpdates(li));
                            sb.Append(BuildDeletes(li));
                            sb.Append(BuildInserts(li));
                            sb.Append(BuildSpEnum(li));
                            break;

                        case "21":
                            sb.Append(BuildBizValExpClass());
                            break;

                        case "22":
                            sb.Append(BuildSpEnumEnhanced(li));
                            break;

                        case "23":
                            MessageBox.Show(
                                "This feature will work exactly like the All SP's button with the exception of creating an SP Enum Enhanced in place of SP Enum.",
                                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sb.Append(BuildExists(li));
                            sb.Append(BuildLoads(li));
                            sb.Append(BuildUpdates(li));
                            sb.Append(BuildDeletes(li));
                            sb.Append(BuildInserts(li));
                            sb.Append(BuildSpEnumEnhanced(li));
                            break;

                        case "24":
                            sb.Append(BuildAllBusFacGets());
                            break;
                    }
                    if (chkWrite.Checked && (((Button)sender).Tag.ToString() == "6" || ((Button)sender).Tag.ToString() == "10"))
                    {
                        // create a writer and open the file
                        TextWriter tw = new StreamWriter(CamCase(li) + ".cs");

                        // write a line of text to the file
                        tw.WriteLine(sb.ToString());

                        // close the stream
                        tw.Close();

                        sb = new System.Text.StringBuilder();
                    }
                    if (chkWrite.Checked && (((Button)sender).Tag.ToString() == "7" || ((Button)sender).Tag.ToString() == "11"))
                    {
                        // create a writer and open the file
                        TextWriter tw = new StreamWriter("Enum" + CamCase(li) + ".cs");

                        // write a line of text to the file
                        tw.WriteLine(sb.ToString());

                        // close the stream
                        tw.Close();

                        sb = new System.Text.StringBuilder();
                    }
                    if (chkWrite.Checked && ((Button)sender).Tag.ToString() == "9")
                    {
                        // create a writer and open the file
                        TextWriter tw = new StreamWriter("Bus" + CamCase(li) + ".cs");

                        // write a line of text to the file
                        tw.WriteLine(sb.ToString());

                        // close the stream
                        tw.Close();

                        sb = new System.Text.StringBuilder();
                    }
                }
            }
            txtOutput.Text = sb.ToString();
        }

        /// <summary>
        /// Builds all bus fac gets.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private string BuildAllBusFacGets()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string li_orig in lstTables.Items)
            {
                string li = CamCase(li_orig);
                string liPas = PascalCase(li_orig);

                sb.Append("        /// <summary>");
                sb.Append("\n");
                sb.Append("        /// " + li + "Get");
                sb.Append("\n");
                sb.Append("        /// </summary>");
                sb.Append("\n");
                sb.Append("        /// <param name=\"\">pLng" + li + "ID</param>");
                sb.Append("\n");
                sb.Append("        /// <returns>" + li + "</returns>");
                sb.Append("\n");
                sb.Append("        /// ");
                sb.Append("\n");
                sb.Append("        public " + li + " " + li + "Get(long pLng" + li + "ID)");
                sb.Append("\n");
                sb.Append("        {");
                sb.Append("\n");
                sb.Append("            " + li + " " + liPas + " = new " + li + "() { " + li + "ID = pLng" + li + "ID };");
                sb.Append("\n");
                //sb.Append("            _log(\"GET\", \"Received Get Request " + li + "\");");
                sb.Append("\n");
                sb.Append("            bool bConn = false;");
                sb.Append("\n");
                sb.Append("            SqlConnection conn = getDBConnection();");
                sb.Append("\n");
                sb.Append("            if (conn != null)");
                sb.Append("\n");
                sb.Append("            {");
                sb.Append("\n");
                sb.Append("                Bus" + li + " bus" + li + " = null;");
                sb.Append("\n");
                sb.Append("                bus" + li + " = new Bus" + li + "(conn);");
                sb.Append("\n");
                sb.Append("                bus" + li + ".Load(" + liPas + ");");
                sb.Append("\n");
                sb.Append("                // close the db connection");
                sb.Append("\n");
                sb.Append("                bConn = CloseConnection(conn);");
                sb.Append("\n");
                sb.Append("                _hasError = bus" + li + ".HasError;");
                sb.Append("\n");
                sb.Append("                if (bus" + li + ".HasError)");
                sb.Append("\n");
                sb.Append("                {");
                sb.Append("\n");
                sb.Append("                    // error");
                sb.Append("\n");
                sb.Append("                    ErrorCode error = new ErrorCode();");
                sb.Append("\n");
                //sb.Append("                    _log(\"ERROR\", error.ToString());");
                sb.Append("\n");
                sb.Append("                }");
                sb.Append("\n");
                sb.Append("            }");
                sb.Append("\n");
                sb.Append("            return " + liPas + ";");
                sb.Append("\n");
                sb.Append("        }");
                sb.Append("\n");
                sb.Append("\n");
                sb.Append("\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Handles the Load event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            cbAuto.SelectedIndex = 0;
            try
            {
                con2.ConnectionString = txtCS.Text;
                SqlCommand cmdSP = new SqlCommand("select [name] from sysobjects where type = 'U' ORDER BY [name]", con2);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmdSP;
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lstTables.Items.Add(dr["name"].ToString());
                }
            }
            catch { }
        }

        /// <summary>
        /// Cams the case.
        /// </summary>
        /// <param name="li">The li.</param>
        /// <returns></returns>
        private string CamCase(string li)
        {
            if (li.EndsWith("_id"))
                li = li.Substring(0, li.Length - 3) + "ID";

            li = li[0].ToString().ToUpper() + li.Substring(1);
            while (li.IndexOf("_") > 0)
            {
                li = li.Substring(0, li.IndexOf("_")) + li.Substring(li.IndexOf("_") + 1, 1).ToUpper() + li.Substring(li.IndexOf("_") + 2);
            }
            return li;
        }

        /// <summary>
        /// Pascals the case.
        /// </summary>
        /// <param name="li">The li.</param>
        /// <returns></returns>
        private string PascalCase(string li)
        {
            li = CamCase(li);
            li = li[0].ToString().ToLower()[0] + li.Substring(1);
            return li;
        }

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, System.EventArgs e)
        {
            txtOutput.Text = txtOutput.Text.Replace("\"", "\\\"");
            for (int i = 0; i < txtOutput.Text.Length - 1; i++)
            {
                if (txtOutput.Text.Substring(i, 1) == "\n")
                {
                    string s = "s += \"";
                    s = txtOutput.Text.Substring(0, i - 1);
                    s += "\\n\");\n sb.Append(\"";
                    try
                    {
                        s += txtOutput.Text.Substring(i + 1);
                    }
                    catch { }
                    txtOutput.Text = s;
                    i = i + 10;
                }
            }
            txtOutput.Text = "sb.Append(\"" + txtOutput.Text + "\\n\");";
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            lstTables.Items.Clear();
            try
            {
                con2.Close();
                con2.ConnectionString = txtCS.Text;
                con2.Open();
                SqlCommand cmdSP = new SqlCommand("select [name] from sysobjects where type = 'U' ORDER BY [name]", con2);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmdSP;
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lstTables.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ee)
            {
                lstTables.Items.Add("!! ERROR !!");
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCopy_Click(object sender, System.EventArgs e)
        {
            txtOutput.SelectAll();
            txtOutput.Copy();
            txtOutput.SelectionLength = 0;
        }

        /// <summary>
        /// Builds the loads.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildLoads(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Load]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("BEGIN");
            sb.Append("\n");
            sb.Append("	PRINT '<<<< Dropping Stored Procedure sp" + csn + "Load >>>>'");
            sb.Append("\n");
            sb.Append("	DROP PROCEDURE [sp" + csn + "Load]");
            sb.Append("\n");
            sb.Append("END");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("/*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		PROCEDURE NAME: sp" + csn + "Load");
            sb.Append("\n");
            sb.Append("**		Change History");
            sb.Append("\n");
            sb.Append("*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		Date:		Author:		Description:");
            sb.Append("\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created");
            sb.Append("\n");
            sb.Append("*******************************************************************************/");
            sb.Append("\n");
            sb.Append("CREATE PROCEDURE sp" + csn + "Load");
            sb.Append("\n");
            sb.Append("(");
            sb.Append("\n");
            sb.Append("@" + csn + "ID        NUMERIC(10) = 0");
            sb.Append("\n");
            sb.Append(")");
            sb.Append("\n");
            sb.Append("AS");
            sb.Append("\n");
            sb.Append("SET NOCOUNT ON");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("-- select record(s) with specified id");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("SELECT ");

            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
                con2.Close();
                if (con2.State == ConnectionState.Closed) con2.Open();
                DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }
                    sb.Append(" [" + dr["ColumnName"].ToString() + "],");
                }

                sb.Remove(sb.ToString().Length - 1, 1);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append(" \nFROM [" + dbn + "] \nWHERE " + dbn + "_id = @" + csn + "ID");
            sb.Append("\n");
            sb.Append("RETURN 0");
            sb.Append("\n");
            sb.Append("------------------------------------------------");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Load]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("PRINT '<<<< Created Stored Procedure sp" + csn + "Load >>>>'");
            sb.Append("\n");
            sb.Append("ELSE");
            sb.Append("\n");
            sb.Append("PRINT '<<< Failed Creating Stored Procedure sp" + csn + "Load >>>'");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");

            return (sb.ToString());
        }

        /// <summary>
        /// Builds the biz class.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildBizClass(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);
            bool Auto = false;

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
            Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";

            sb = new System.Text.StringBuilder();

            sb.Append("using System;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Collections;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Text.RegularExpressions;\n");
            sb.Append("\n");
            sb.Append("using Vetapp.Engine.Common;\n");
            //sb.Append("using Vetapp.BusinessFacadeLayer;\n");
            sb.Append("using Vetapp.Engine.DataAccessLayer.Enumeration;\n");
            sb.Append("using Vetapp.Engine.DataAccessLayer.Data;\n");
            sb.Append("\n");
            sb.Append("namespace Vetapp.Engine.BusinessAccessLayer\n");
            sb.Append("{\n");
            sb.Append("\n");
            sb.Append("	/// <summary>\n");
            sb.Append("	/// Copyright (c) " + DateTime.UtcNow.Year.ToString() + " Haytham Allos.  San Diego, California, USA\n");
            sb.Append("	/// All Rights Reserved\n");
            sb.Append("	/// \n");
            sb.Append("	/// File:  Bus" + csn + ".cs\n");
            sb.Append("	/// History\n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// 001	" + txtCB.Text + "	" + DateTime.UtcNow.ToShortDateString() + "	Created\n");
            sb.Append("	/// \n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// Business Class for " + csn + " objects.\n");
            sb.Append("	/// </summary>\n");
            sb.Append("	public class Bus" + csn + "\n");
            sb.Append("	{\n");
            sb.Append("		private SqlConnection _conn = null;\n");
            //sb.Append("		private Config _config = null;\n");
            //sb.Append("		private Logger _oLog = null;\n");
            //sb.Append("		private string _strLognameText = \"BusinessAccessLayer-Bus-" + csn + "\";\n");
            sb.Append("		private bool _hasError = false;\n");
            sb.Append("		private bool _hasInvalid = false;\n");
            sb.Append("\n");
            sb.Append("		private ArrayList _arrlstEntities = null;\n");
            sb.Append("		private ArrayList _arrlstColumnErrors = new ArrayList();\n");
            sb.Append("\n");
            sb.Append("		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    sb.Append("		private const String REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() +
                        " = " + GetTypeRegExp(dr["ProviderType"].ToString(), dr["NumericPrecision"].ToString(),
                        dr["NumericScale"].ToString(), dr["ColumnName"].ToString()) + ";\n");
            }

            sb.Append("\n		public string SP_ENUM_NAME = null;\n");

            sb.Append("\n\n");
            sb.Append("/*********************** CUSTOM NON-META BEGIN *********************/\n");
            sb.Append("\n");
            sb.Append("/*********************** CUSTOM NON-META END *********************/\n");
            sb.Append("\n\n");
            sb.Append("		/// <summary>Bus" + csn + " constructor takes SqlConnection object</summary>\n");
            sb.Append("		public Bus" + csn + "()\n");
            sb.Append("		{\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Bus" + csn + " constructor takes SqlConnection object</summary>\n");
            sb.Append("		public Bus" + csn + "(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			_conn = conn;\n");
            sb.Append("		}\n");
            //sb.Append("		/// <summary>Bus" + csn + " constructor takes SqlConnection object and Config Object</summary>\n");
            //sb.Append("		public Bus" + csn + "(SqlConnection conn, Config pConfig)\n");
            //sb.Append("		{\n");
            //sb.Append("			_conn = conn;\n");
            ////sb.Append("			_config = pConfig;\n");
            ////sb.Append("			_oLog = new Logger(_strLognameText);\n");
            //sb.Append("		}\n");
            sb.Append("\n");

            sb.Append("	 /// <summary>\n");
            sb.Append("	///     Gets all " + csn + " objects\n");
            sb.Append("	///     <remarks>   \n");
            sb.Append("	///         No parameters. Returns all " + csn + " objects \n");
            sb.Append("	///     </remarks>   \n");
            sb.Append("	///     <retvalue>ArrayList containing all " + csn + " objects</retvalue>\n");
            sb.Append("	/// </summary>\n");
            sb.Append("	public ArrayList Get()\n");
            sb.Append("	{\n");
            sb.Append("		return (Get(0");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                        case "12": // NVARCHAR
                        case "21": // VARBINARY
                        case "18": // TEXT
                        case "11": // NTEXT
                            sb.Append(", null");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    //sb.Append(", 0");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append(", 0");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append(", false");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append(", 0");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append(", 0");
                            }

                            if (dr["NumericScale"].ToString() == "6")
                            {
                                sb.Append(", 0");
                                sb.Append(", 0");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append(", new DateTime()");
                            sb.Append(", new DateTime()");
                            break;

                        case "4": //DATETETIME
                            sb.Append(", new DateTime()");
                            sb.Append(", new DateTime()");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            break;
                    }
                }

                con2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            //, null, null, null
            sb.Append("));\n");
            sb.Append("	}\n\n");

            sb.Append("	 /// <summary>\n");
            sb.Append("	///     Gets all " + csn + " objects\n");
            sb.Append("	///     <remarks>   \n");
            sb.Append("	///         No parameters. Returns all " + csn + " objects \n");
            sb.Append("	///     </remarks>   \n");
            sb.Append("	///     <retvalue>ArrayList containing all " + csn + " objects</retvalue>\n");
            sb.Append("	/// </summary>\n");
            sb.Append("	public ArrayList Get(long l" + csn + "ID)\n");
            sb.Append("	{\n");
            sb.Append("		return (Get(l" + csn + "ID ");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                        case "12": // NVARCHAR
                        case "21": // VARBINARY
                        case "18": // TEXT
                        case "11": // NTEXT
                            sb.Append(", null");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    //sb.Append(", 0");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append(", 0");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append(", false");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append(", 0");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append(", 0");
                            }

                            if (dr["NumericScale"].ToString() == "6")
                            {
                                sb.Append(", 0");
                                sb.Append(", 0");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append(", new DateTime()");
                            sb.Append(", new DateTime()");
                            break;

                        case "4": //DATETETIME
                            sb.Append(", new DateTime()");
                            sb.Append(", new DateTime()");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            break;
                    }
                }

                con2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            //, null, null, null
            sb.Append("));\n");
            sb.Append("	}\n\n");

            sb.Append("        /// <summary>\n");
            sb.Append("        ///     Gets all " + csn + " objects\n");
            sb.Append("        ///     <remarks>   \n");
            sb.Append("        ///         Returns ArrayList containing object passed in \n");
            sb.Append("        ///     </remarks>   \n");
            sb.Append("        ///     <param name=\"o\">" + csn + " to be returned</param>\n");
            sb.Append("        ///     <retvalue>ArrayList containing " + csn + " object</retvalue>\n");
            sb.Append("        /// </summary>\n");

            sb.Append("	public ArrayList Get(" + NS(csn) + " o)\n");
            sb.Append("	{	\n");
            sb.Append("		return (Get(");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }

                if (dr["ProviderType"].ToString() == "15" || dr["ProviderType"].ToString() == "4")
                {
                    sb.Append(" o." + CamCase(dr["ColumnName"].ToString()) + ",");
                    sb.Append(" o." + CamCase(dr["ColumnName"].ToString()) + ",");
                }
                else
                {
                    sb.Append(" o." + CamCase(dr["ColumnName"].ToString()) + ",");
                }

                if (dr["ProviderType"].ToString() == "5")
                {
                    if (dr["NumericScale"].ToString() == "6")
                    {
                        sb.Append(" o." + CamCase(dr["ColumnName"].ToString()) + ",");
                        sb.Append(" o." + CamCase(dr["ColumnName"].ToString()) + ",");
                    }
                }
            }
            sb.Remove(sb.ToString().Length - 1, 1);
            sb.Append("	));\n");
            sb.Append("	}\n\n");

            sb.Append("        /// <summary>\n");
            sb.Append("        ///     Gets all " + csn + " objects\n");
            sb.Append("        ///     <remarks>   \n");
            sb.Append("        ///         Returns ArrayList containing object passed in \n");
            sb.Append("        ///     </remarks>   \n");
            sb.Append("        ///     <param name=\"o\">" + csn + " to be returned</param>\n");
            sb.Append("        ///     <retvalue>ArrayList containing " + csn + " object</retvalue>\n");
            sb.Append("        /// </summary>\n");

            sb.Append("	public ArrayList Get(Enum" + csn + " o)\n");
            sb.Append("	{	\n");
            sb.Append("		return (Get(");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }

                if (dr["ProviderType"].ToString() == "15" || dr["ProviderType"].ToString() == "4")
                {
                    sb.Append(" o.Begin" + CamCase(dr["ColumnName"].ToString()) + ",");
                    sb.Append(" o.End" + CamCase(dr["ColumnName"].ToString()) + ",");
                }
                else
                {
                    sb.Append(" o." + CamCase(dr["ColumnName"].ToString()) + ",");
                }

                if (dr["ProviderType"].ToString() == "5")
                {
                    if (dr["NumericScale"].ToString() == "6")
                    {
                        sb.Append(" o.Begin" + CamCase(dr["ColumnName"].ToString()) + ",");
                        sb.Append(" o.End" + CamCase(dr["ColumnName"].ToString()) + ",");
                    }
                }
            }
            sb.Remove(sb.ToString().Length - 1, 1);
            sb.Append("	));\n");
            sb.Append("	}\n\n");

            sb.Append("		/// <summary>\n");
            sb.Append("		///     Gets all " + csn + " objects\n");
            sb.Append("		///     <remarks>   \n");
            sb.Append("		///         Returns " + csn + " objects in an array list \n");
            sb.Append("		///         using the given criteria \n");
            sb.Append("		///     </remarks>   \n");
            sb.Append("		///     <retvalue>ArrayList containing " + csn + " object</retvalue>\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public ArrayList Get(");
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "21": // VARBINARY
                            sb.Append(" byte[] pByt" + CamCase(dr["ColumnName"].ToString()) + ",");
                            break;

                        case "22": // VARCHAR
                        case "12": // NVARCHAR
                        case "18": // TEXT
                        case "11": // NTEXT
                            sb.Append(" string pStr" + CamCase(dr["ColumnName"].ToString()) + ",");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append(" long pLng" + CamCase(dr["ColumnName"].ToString()) + ",");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append(" long pLng" + CamCase(dr["ColumnName"].ToString()) + ",");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append(" " + _strBoolText + " pBol" + CamCase(dr["ColumnName"].ToString()) + ",");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append(" long pLng" + CamCase(dr["ColumnName"].ToString()) + ",");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append(" double pDbl" + CamCase(dr["ColumnName"].ToString()) + ",");
                            }

                            if (dr["NumericScale"].ToString() == "6")
                            {
                                sb.Append(" double pDblBegin" + CamCase(dr["ColumnName"].ToString()) + ",");
                                sb.Append(" double pDblEnd" + CamCase(dr["ColumnName"].ToString()) + ",");
                            }
                            break;

                        case "15": //SMALLDATETIME
                        case "4": //DATETETIME
                            sb.Append(" DateTime pDtBegin" + CamCase(dr["ColumnName"].ToString()) + ",");
                            sb.Append(" DateTime pDtEnd" + CamCase(dr["ColumnName"].ToString()) + ",");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append(" object pObj" + CamCase(dr["ColumnName"].ToString()) + ",");
                            break;
                    }
                }

                con2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Remove(sb.ToString().Length - 1, 1);
            sb.Append(")\n");

            sb.Append("		{\n");
            sb.Append("			" + NS(csn) + " data = null;\n");
            sb.Append("			_arrlstEntities = new ArrayList();\n");
            sb.Append("			Enum" + csn + " enum" + csn + " = new Enum" + csn + "(_conn);\n");

            sb.Append("			 enum" + csn + ".SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enum" + csn + ".SP_ENUM_NAME;\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "21": // VARBINARY
                            sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pByt" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            break;

                        case "22": // VARCHAR
                        case "12": // NVARCHAR
                        case "18": // TEXT
                        case "11": // NTEXT
                            sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pStr" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pLng" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pLng" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pBol" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pLng" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pDbl" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            }

                            if (dr["NumericScale"].ToString() == "6")
                            {
                                sb.Append("			enum" + csn + ".Begin" + CamCase(dr["ColumnName"].ToString()) + " = pDblBegin" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                                sb.Append("			enum" + csn + ".End" + CamCase(dr["ColumnName"].ToString()) + " = pDblEnd" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("			enum" + csn + ".Begin" + CamCase(dr["ColumnName"].ToString()) + " = pDtBegin" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            sb.Append("			enum" + csn + ".End" + CamCase(dr["ColumnName"].ToString()) + " = pDtEnd" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("			enum" + csn + ".Begin" + CamCase(dr["ColumnName"].ToString()) + " = pDtBegin" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            sb.Append("			enum" + csn + ".End" + CamCase(dr["ColumnName"].ToString()) + " = pDtEnd" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("			enum" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = pObj" + CamCase(dr["ColumnName"].ToString()) + ";\n");
                            break;
                    }
                }

                con2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("			enum" + csn + ".EnumData();\n");

            //sb.Append("			_log(\"Get\", enum" + csn + ".ToString());\n");
            sb.Append("			while (enum" + csn + ".hasMoreElements())\n");
            sb.Append("			{\n");
            sb.Append("				data = (" + NS(csn) + ") enum" + csn + ".nextElement();\n");
            sb.Append("				_arrlstEntities.Add(data);\n");
            sb.Append("			}\n");
            sb.Append("			enum" + csn + " = null;\n");
            sb.Append("			ArrayList.ReadOnly(_arrlstEntities);\n");
            sb.Append("			return _arrlstEntities;\n");
            sb.Append("		}\n\n");

            sb.Append("        /// <summary>\n");
            sb.Append("        ///     Saves " + csn + " object to database\n");
            sb.Append("        ///     <param name=\"o\">" + csn + " to be saved.</param>\n");
            sb.Append("        ///     <retvalue>void</retvalue>\n");
            sb.Append("        /// </summary>\n");

            sb.Append("		public void Save(" + NS(csn) + " o)\n");
            sb.Append("		{\n");
            sb.Append("			if ( o != null )\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"SAVING\", o.ToString());\n");
            sb.Append("				o.Save(_conn);\n");
            sb.Append("				if ( o.HasError )\n");
            sb.Append("				{\n");
            //sb.Append("     _log(\"ERROR SAVING\", o.ToString());\n");
            sb.Append("					_hasError = true;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");

            sb.Append("		/// <summary>\n");
            sb.Append("		///     Modify " + csn + " object to database\n");
            sb.Append("		///     <param name=\"o\">" + csn + " to be modified.</param>\n");
            sb.Append("		///     <retvalue>void</retvalue>\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public void Update(" + NS(csn) + " o)\n");
            sb.Append("		{\n");
            sb.Append("			if ( o != null )\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"UPDATING\", o.ToString());\n");
            sb.Append("				o.Update(_conn);\n");
            sb.Append("				if ( o.HasError )\n");
            sb.Append("				{\n");
            //sb.Append("     _log(\"ERROR UPDATING\", o.ToString());\n");
            sb.Append("					_hasError = true;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///     Modify " + csn + " object to database\n");
            sb.Append("		///     <param name=\"o\">" + csn + " to be modified.</param>\n");
            sb.Append("		///     <retvalue>void</retvalue>\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public void Load(" + NS(csn) + " o)\n");
            sb.Append("		{\n");
            sb.Append("			if ( o != null )\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"LOADING\", o.ToString());\n");
            sb.Append("				o.Load(_conn);\n");
            sb.Append("				if ( o.HasError )\n");
            sb.Append("				{\n");
            //sb.Append("     _log(\"ERROR LOADING\", o.ToString());\n");
            sb.Append("					_hasError = true;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///     Modify " + csn + " object to database\n");
            sb.Append("		///     <param name=\"o\">" + csn + " to be modified.</param>\n");
            sb.Append("		///     <retvalue>void</retvalue>\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public void Delete(" + NS(csn) + " o)\n");
            sb.Append("		{\n");
            sb.Append("			if ( o != null )\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"DELETING\", o.ToString());\n");
            sb.Append("				o.Delete(_conn);\n");
            sb.Append("				if ( o.HasError )\n");
            sb.Append("				{\n");
            //sb.Append("     _log(\"ERROR DELETING\", o.ToString());\n");
            sb.Append("					_hasError = true;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///     Exist " + csn + " object to database\n");
            sb.Append("		///     <param name=\"o\">" + csn + " to be modified.</param>\n");
            sb.Append("		///     <retvalue>void</retvalue>\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public bool Exist(" + NS(csn) + " o)\n");
            sb.Append("		{\n");
            sb.Append("			bool bExist = false;\n");
            sb.Append("			if ( o != null )\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"EXIST\", o.ToString());\n");
            sb.Append("				bExist = o.Exist(_conn);\n");
            sb.Append("				if ( o.HasError )\n");
            sb.Append("				{\n");
            //sb.Append("     _log(\"ERROR EXIST\", o.ToString());\n");
            sb.Append("					_hasError = true;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("			return bExist;\n");
            sb.Append("		}\n");

            sb.Append("		/// <summary>Property as to whether or not the object has an Error</summary>\n");
            sb.Append("		public bool HasError \n");
            sb.Append("		{\n");
            sb.Append("			get{return _hasError;}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Property as to whether or not the object has invalid columns</summary>\n");
            sb.Append("		public bool HasInvalid \n");
            sb.Append("		{\n");
            sb.Append("			get{return _hasInvalid;}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Property which returns all column error in an ArrayList</summary>\n");
            sb.Append("		public ArrayList ColumnErrors\n");
            sb.Append("		{\n");
            sb.Append("			get{return _arrlstColumnErrors;}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Property returns an ArrayList containing " + csn + " objects</summary>\n");
            sb.Append("		public ArrayList " + csn + "s \n");
            sb.Append("		{\n");
            sb.Append("			get\n");
            sb.Append("			{\n");
            sb.Append("				if ( _arrlstEntities == null )\n");
            sb.Append("				{\n");
            sb.Append("					" + NS(csn) + " data = null;\n");
            sb.Append("					_arrlstEntities = new ArrayList();\n");
            sb.Append("					Enum" + csn + " enum" + csn + " = new Enum" + csn + "(_conn);\n");
            sb.Append("					enum" + csn + ".EnumData();\n");
            sb.Append("					while (enum" + csn + ".hasMoreElements())\n");
            sb.Append("					{\n");
            sb.Append("						data = (" + NS(csn) + ") enum" + csn + ".nextElement();\n");
            sb.Append("						_arrlstEntities.Add(data);\n");
            sb.Append("					}\n");
            sb.Append("					enum" + csn + " = null;\n");
            sb.Append("					ArrayList.ReadOnly(_arrlstEntities);\n");
            sb.Append("				}\n");
            sb.Append("				return _arrlstEntities;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///     Checks to make sure all values are valid\n");
            sb.Append("		///     <remarks>   \n");
            sb.Append("		///         Calls \"IsValid\" method for each property in ocject\n");
            sb.Append("		///     </remarks>   \n");
            sb.Append("		///     <retvalue>true if object has valid entries, false otherwise</retvalue>\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public bool IsValid(" + NS(csn) + " pRef" + csn + ")\n");
            sb.Append("		{\n");
            sb.Append("			bool isValid = true;\n");
            sb.Append("			bool isValidTmp = true;\n");
            sb.Append("            \n");
            sb.Append("			_arrlstColumnErrors = null;\n");
            sb.Append("			_arrlstColumnErrors = new ArrayList();\n");
            sb.Append("\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }

                if (dr["ProviderType"].ToString() != "21")
                {
                    sb.Append("			isValidTmp = IsValid" + CamCase(dr["ColumnName"].ToString()) + "(pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + ");\n");
                }

                if ((dr["ProviderType"].ToString() == "5") || (dr["ProviderType"].ToString() == "4") || (dr["ProviderType"].ToString() == "8"))
                {
                    sb.Append("			if (!isValidTmp)\n");
                }
                else if (dr["ProviderType"].ToString() == "21")
                {
                    sb.Append("            //Cannot validate type byte[].\n");
                }
                else
                {
                    sb.Append("			if (!isValidTmp && pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " != null)\n");
                }
                if (dr["ProviderType"].ToString() != "21")
                {
                    sb.Append("			{\n");
                    sb.Append("				isValid = false;\n");
                    sb.Append("			}\n");
                }
            }

            sb.Append("\n");
            sb.Append("			return isValid;\n");
            sb.Append("		}\n");
            //here
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    sb.Append("		/// <summary>\n");
                    sb.Append("		///     Checks to make sure value is valid\n");
                    sb.Append("		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>\n");
                    sb.Append("		/// </summary>\n");

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(string pStrData)\n");
                            sb.Append("		{\n");
                            sb.Append("			bool isValid = true;\n");
                            sb.Append("            \n");
                            sb.Append("			// do some validation\n");
                            sb.Append("			isValid = !(new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pStrData);\n");
                            sb.Append("			if ( !isValid )\n");
                            sb.Append("			{\n");
                            sb.Append("				Column clm = null;\n");
                            sb.Append("				clm = new Column();\n");
                            sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                            sb.Append("				clm.HasError = true;\n");
                            sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                            sb.Append("				_hasInvalid = true;\n");
                            sb.Append("			}\n");
                            sb.Append("			return isValid;\n");
                            sb.Append("		}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(string pStrData)\n");
                            sb.Append("		{\n");
                            sb.Append("			bool isValid = true;\n");
                            sb.Append("            \n");
                            sb.Append("			// do some validation\n");
                            sb.Append("			isValid = !(new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pStrData);\n");
                            sb.Append("			if ( !isValid )\n");
                            sb.Append("			{\n");
                            sb.Append("				Column clm = null;\n");
                            sb.Append("				clm = new Column();\n");
                            sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                            sb.Append("				clm.HasError = true;\n");
                            sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                            sb.Append("				_hasInvalid = true;\n");
                            sb.Append("			}\n");
                            sb.Append("			return isValid;\n");
                            sb.Append("		}\n");
                            break;

                        case "21": // VARBINARY
                        case "18": // TEXT
                            sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(string pStrData)\n");
                            sb.Append("		{\n");
                            sb.Append("			bool isValid = true;\n");
                            sb.Append("            \n");
                            sb.Append("			// do some validation\n");
                            sb.Append("			isValid = !(new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pStrData);\n");
                            sb.Append("			if ( !isValid )\n");
                            sb.Append("			{\n");
                            sb.Append("				Column clm = null;\n");
                            sb.Append("				clm = new Column();\n");
                            sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                            sb.Append("				clm.HasError = true;\n");
                            sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                            sb.Append("				_hasInvalid = true;\n");
                            sb.Append("			}\n");
                            sb.Append("			return isValid;\n");
                            sb.Append("		}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(string pStrData)\n");
                            sb.Append("		{\n");
                            sb.Append("			bool isValid = true;\n");
                            sb.Append("            \n");
                            sb.Append("			// do some validation\n");
                            sb.Append("			isValid = !(new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pStrData);\n");
                            sb.Append("			if ( !isValid )\n");
                            sb.Append("			{\n");
                            sb.Append("				Column clm = null;\n");
                            sb.Append("				clm = new Column();\n");
                            sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                            sb.Append("				clm.HasError = true;\n");
                            sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                            sb.Append("				_hasInvalid = true;\n");
                            sb.Append("			}\n");
                            sb.Append("			return isValid;\n");
                            sb.Append("		}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(long pLngData)\n");
                                    sb.Append("		{\n");
                                    sb.Append("			bool isValid = true;\n");
                                    sb.Append("            \n");
                                    sb.Append("			// do some validation\n");
                                    sb.Append("			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());\n");
                                    sb.Append("			if ( !isValid )\n");
                                    sb.Append("			{\n");
                                    sb.Append("				Column clm = null;\n");
                                    sb.Append("				clm = new Column();\n");
                                    sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_ID;\n");
                                    sb.Append("				clm.HasError = true;\n");
                                    sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                                    sb.Append("				_hasInvalid = true;\n");
                                    sb.Append("			}\n");
                                    sb.Append("			return isValid;\n");
                                    sb.Append("		}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(long pLngData)\n");
                                    sb.Append("		{\n");
                                    sb.Append("			bool isValid = true;\n");
                                    sb.Append("            \n");
                                    sb.Append("			// do some validation\n");
                                    sb.Append("			isValid = (new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pLngData.ToString());\n");
                                    sb.Append("			if ( !isValid )\n");
                                    sb.Append("			{\n");
                                    sb.Append("				Column clm = null;\n");
                                    sb.Append("				clm = new Column();\n");
                                    sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                                    sb.Append("				clm.HasError = true;\n");
                                    sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                                    sb.Append("				_hasInvalid = true;\n");
                                    sb.Append("			}\n");
                                    sb.Append("			return isValid;\n");
                                    sb.Append("		}\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(" + _strBoolText + " pBolData)\n");
                                sb.Append("		{\n");
                                sb.Append("			bool isValid = true;\n");
                                sb.Append("            \n");
                                sb.Append("			// do some validation\n");
                                sb.Append("			isValid = (new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pBolData.ToString());\n");
                                sb.Append("			if ( !isValid )\n");
                                sb.Append("			{\n");
                                sb.Append("				Column clm = null;\n");
                                sb.Append("				clm = new Column();\n");
                                sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                                sb.Append("				clm.HasError = true;\n");
                                sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                                sb.Append("				_hasInvalid = true;\n");
                                sb.Append("			}\n");
                                sb.Append("			return isValid;\n");
                                sb.Append("		}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(long pLngData)\n");
                                sb.Append("		{\n");
                                sb.Append("			bool isValid = true;\n");
                                sb.Append("            \n");
                                sb.Append("			// do some validation\n");
                                sb.Append("			isValid = (new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pLngData.ToString());\n");
                                sb.Append("			if ( !isValid )\n");
                                sb.Append("			{\n");
                                sb.Append("				Column clm = null;\n");
                                sb.Append("				clm = new Column();\n");
                                sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                                sb.Append("				clm.HasError = true;\n");
                                sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                                sb.Append("				_hasInvalid = true;\n");
                                sb.Append("			}\n");
                                sb.Append("			return isValid;\n");
                                sb.Append("		}\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(double pDblData)\n");
                                sb.Append("		{\n");
                                sb.Append("			bool isValid = true;\n");
                                sb.Append("            \n");
                                sb.Append("			// do some validation\n");
                                sb.Append("			isValid = (new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pDblData.ToString());\n");
                                sb.Append("			if ( !isValid )\n");
                                sb.Append("			{\n");
                                sb.Append("				Column clm = null;\n");
                                sb.Append("				clm = new Column();\n");
                                sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                                sb.Append("				clm.HasError = true;\n");
                                sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                                sb.Append("				_hasInvalid = true;\n");
                                sb.Append("			}\n");
                                sb.Append("			return isValid;\n");
                                sb.Append("		}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(DateTime pDtData)\n");
                            sb.Append("		{\n");
                            sb.Append("			bool isValid = true;\n");
                            sb.Append("            \n");
                            sb.Append("			// do some validation\n");
                            sb.Append("			isValid = (new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pDtData.ToString());\n");
                            sb.Append("			if ( !isValid )\n");
                            sb.Append("			{\n");
                            sb.Append("				Column clm = null;\n");
                            sb.Append("				clm = new Column();\n");
                            sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                            sb.Append("				clm.HasError = true;\n");
                            sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                            sb.Append("				_hasInvalid = true;\n");
                            sb.Append("			}\n");
                            sb.Append("			return isValid;\n");
                            sb.Append("		}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("		public bool IsValid" + CamCase(dr["ColumnName"].ToString()) + "(DateTime pDtData)\n");
                            sb.Append("		{\n");
                            sb.Append("			bool isValid = true;\n");
                            sb.Append("            \n");
                            sb.Append("			// do some validation\n");
                            sb.Append("			isValid = (new Regex(REGEXP_ISVALID_" + dr["ColumnName"].ToString().ToUpper() + ")).IsMatch(pDtData.ToString());\n");
                            sb.Append("			if ( !isValid )\n");
                            sb.Append("			{\n");
                            sb.Append("				Column clm = null;\n");
                            sb.Append("				clm = new Column();\n");
                            sb.Append("				clm.ColumnName = " + NS(csn) + ".DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ";\n");
                            sb.Append("				clm.HasError = true;\n");
                            sb.Append("				_arrlstColumnErrors.Add(clm);\n");
                            sb.Append("				_hasInvalid = true;\n");
                            sb.Append("			}\n");
                            sb.Append("			return isValid;\n");
                            sb.Append("		}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            break;
                    }
                }

                con2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            //sb.Append("        /// <summary>\n");
            //sb.Append("        ///     Standard Error Logging\n");
            //sb.Append("        ///     <retvalue>void</retvalue>\n");
            //sb.Append("        /// </summary>\n");
            //sb.Append("		private void _log(string pStrAction, string pStrMsgText) \n");
            //sb.Append("		{\n");
            //sb.Append("			if (_config != null )\n");
            //sb.Append("			{\n");
            //sb.Append("				if (_config.DoLogInfo)\n");
            //sb.Append("				{\n");
            //sb.Append("						_oLog.Log(pStrAction, pStrMsgText);\n");
            //sb.Append("				}\n");
            //sb.Append("			}\n");
            //sb.Append("\n");
            //sb.Append("		}\n");
            //sb.Append("\n");
            //sb.Append("        /// <summary>\n");
            //sb.Append("        ///     Command Line Prompts to get values\n");
            //sb.Append("        ///     <retvalue>void</retvalue>\n");
            //sb.Append("        /// </summary>\n");
            //sb.Append("		public void Prompt(bool GetIdendity, " + NS(csn) + " pRef" + csn + ")\n");
            //sb.Append("		{\n");
            //sb.Append("			try \n");
            //sb.Append("			{\n");
            //sb.Append("				GetIdendity = true;				\n");
            //sb.Append("				if (GetIdendity)\n");
            //sb.Append("				{\n");
            //sb.Append("					Console.WriteLine(" + NS(csn) + ".TAG_ID + \":  \");\n");
            //sb.Append("					try\n");
            //sb.Append("					{\n");
            //sb.Append("						pRef" + csn + "." + csn + "ID = long.Parse(Console.ReadLine());\n");
            //sb.Append("					}\n");
            //sb.Append("					catch \n");
            //sb.Append("					{\n");
            //sb.Append("						pRef" + csn + "." + csn + "ID = 0;\n");
            //sb.Append("					}\n");
            //sb.Append("				}\n");
            //sb.Append("\n");
            ////here
            //try
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (dr["ProviderType"].ToString() == "29")
            //        {
            //            continue;
            //        }

            //        if (dr["ColumnName"].ToString() == dbn + "_id") continue;
            //        switch (dr["ProviderType"].ToString())
            //        {
            //            case "22": // VARCHAR
            //                sb.Append("\n");
            //                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
            //                break;

            //            case "12": // NVARCHAR
            //                sb.Append("\n");
            //                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
            //                break;

            //            case "21": // VARBINARY
            //                sb.Append("             // Cannot reliably convert byte[] to string.\n");
            //                //    sb.Append("\n");
            //                //    sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                //    sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
            //                break;

            //            case "18": // TEXT
            //                sb.Append("\n");
            //                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
            //                break;

            //            case "11": // NTEXT
            //                sb.Append("\n");
            //                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
            //                break;

            //            case "20":
            //            case "2":
            //            case "16":
            //            case "8":
            //            case "5": // NUMERIC
            //                if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
            //                {
            //                    if (dr["ColumnName"].ToString() == (dbn + "_id"))
            //                    {
            //                        // NUMERIC(10)
            //                        sb.Append("\n");
            //                        sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                        sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
            //                    }
            //                    else
            //                    {
            //                        // NUMERIC(10)
            //                        sb.Append("\n");
            //                        sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                        sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
            //                    }
            //                }
            //                else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
            //                {
            //                    // BOOLEAN
            //                    sb.Append("\n");
            //                    sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                    sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(Console.ReadLine());\n");
            //                }
            //                else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
            //                {
            //                    // INT / LONG
            //                    sb.Append("\n");
            //                    sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                    sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
            //                }
            //                else
            //                {
            //                    // DECIMAL
            //                    sb.Append("\n");
            //                    sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                    sb.Append("				pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(Console.ReadLine());\n");
            //                }
            //                break;

            //            case "15": //SMALLDATETIME
            //                sb.Append("				try\n");
            //                sb.Append("				{\n");
            //                sb.Append("					Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                sb.Append("					pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(Console.ReadLine());\n");
            //                sb.Append("				}\n");
            //                sb.Append("				catch \n");
            //                sb.Append("				{\n");
            //                sb.Append("					pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
            //                sb.Append("				}\n");
            //                break;

            //            case "4": //DATETETIME
            //                sb.Append("				try\n");
            //                sb.Append("				{\n");
            //                sb.Append("					Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                sb.Append("					pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(Console.ReadLine());\n");
            //                sb.Append("				}\n");
            //                sb.Append("				catch \n");
            //                sb.Append("				{\n");
            //                sb.Append("					pRef" + csn + "." + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
            //                sb.Append("				}\n");
            //                break;

            //            default: // OTHER?
            //                MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
            //                sb.Append("\n");
            //                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
            //                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
            //                break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //sb.Append("\n");
            //sb.Append("			}\n");
            //sb.Append("			catch (Exception e) \n");
            //sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            //sb.Append("			}\n");
            //sb.Append("		}\n");
            //sb.Append("\n");
            //sb.Append("		/// <summary>Unit Testing: Save, Delete, Update, Exist, Load and ToXml</summary>\n");
            //sb.Append("		public void Test()\n");
            //sb.Append("		{\n");
            //sb.Append("			try \n");
            //sb.Append("			{\n");
            //sb.Append("				Console.WriteLine(\"What would you like to do?\");\n");
            //sb.Append("				Console.WriteLine(\"1.  Save.\");\n");
            //sb.Append("				Console.WriteLine(\"2.  Get All.\");\n");
            //sb.Append("				Console.WriteLine(\"q.  Quit.\");\n");
            //sb.Append("				\n");
            //sb.Append("				string strAns = \"\";\n");
            //sb.Append("\n");
            //sb.Append("				strAns = Console.ReadLine();\n");
            //sb.Append("				if (strAns != \"q\")\n");
            //sb.Append("				{	\n");
            //sb.Append("					int nAns = 0;\n");
            //sb.Append("					nAns = int.Parse(strAns);\n");
            //sb.Append("					switch(nAns)\n");
            //sb.Append("					{\n");
            //sb.Append("						case 1:\n");
            //sb.Append("							// save\n");
            //sb.Append("							" + NS(csn) + " o = null;\n");
            //sb.Append("							o = new " + NS(csn) + "(_config);\n");
            //sb.Append("							Console.WriteLine(\"Save:  \");\n");
            //sb.Append("							Prompt(true, o);\n");
            //sb.Append("							Save(o);\n");
            //sb.Append("							Console.WriteLine(\"Has error:  \" + HasError.ToString() );\n");
            //sb.Append("							Console.WriteLine(\"Has invalid:  \" + HasInvalid.ToString() );\n");
            //sb.Append("							Console.WriteLine(\"Column Errors Count:  \" + ColumnErrors.Count.ToString() );\n");
            //sb.Append("							if ( ColumnErrors.Count > 0 )\n");
            //sb.Append("							{\n");
            //sb.Append("								foreach (Column item in ColumnErrors)\n");
            //sb.Append("								{\n");
            //sb.Append("									Console.WriteLine(\"Column Errors Count:  \" + item.ToString() );\n");
            //sb.Append("								}\n");
            //sb.Append("							}\n");
            //sb.Append("							Console.WriteLine(\" \");\n");
            //sb.Append("							Console.WriteLine(\"Press ENTER to continue...\");\n");
            //sb.Append("							Console.ReadLine();\n");
            //sb.Append("							break;\n");
            //sb.Append("						case 2:\n");
            //sb.Append("							ArrayList " + csn + " = null;\n");
            //sb.Append("							" + csn + " = Get();\n");
            //sb.Append("							Console.WriteLine(\"" + csn + " count:  \" + " + csn + "s.Count.ToString() );\n");
            //sb.Append("							foreach (" + NS(csn) + " item in " + csn + "s)\n");
            //sb.Append("							{\n");
            //sb.Append("								Console.WriteLine(\"-------\\n\");\n");
            //sb.Append("								Console.WriteLine(item.ToString() );\n");
            //sb.Append("							}\n");
            //sb.Append("							break;\n");
            //sb.Append("						default:\n");
            //sb.Append("							Console.WriteLine(\"Undefined option.\");\n");
            //sb.Append("							break;\n");
            //sb.Append("					}\n");
            //sb.Append("				}\n");
            //sb.Append("			}\n");
            //sb.Append("			catch (Exception e) \n");
            //sb.Append("			{\n");
            //sb.Append("				Console.WriteLine(e.ToString());\n");
            //sb.Append("				Console.WriteLine(e.StackTrace);\n");
            //sb.Append("				Console.ReadLine();\n");
            //sb.Append("			}\n");
            //sb.Append("		}\n");
            //sb.Append("\n");
            sb.Append("	}\n");
            sb.Append("}\n");
            sb.Append(" // END OF CLASS FILE");

            return (sb.ToString());
        }

        /// <summary>
        /// Gets the type reg exp.
        /// </summary>
        /// <param name="providerType">Type of the provider.</param>
        /// <param name="numericPercision">The numeric percision.</param>
        /// <param name="numericScale">The numeric scale.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        private string GetTypeRegExp(string providerType, string numericPercision, string numericScale, string columnName)
        {
            switch (providerType)
            {
                case "2":
                    return "BusValidationExpressions.REGEX_TYPE_PATTERN_BIT";
                    break;

                case "5":
                    if (numericPercision == "10")
                    {
                        return "BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10";
                    }
                    else if (numericScale == "0" || numericScale == "255")
                    {
                        return "BusValidationExpressions.REGEX_TYPE_PATTERN_INT";
                    }
                    else
                    {
                        return "\"\"";
                    }
                    break;

                case "8":
                    if (numericScale == "0" || numericScale == "255")
                    {
                        return "BusValidationExpressions.REGEX_TYPE_PATTERN_INT";
                    }
                    else
                    {
                        return "\"\"";
                    }
                    break;

                case "12":
                    return "BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255";
                    break;

                case "18":
                    return "BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT";
                    break;

                default:
                    return "\"\"";
                    break;
            }
        }

        /// <summary>
        /// Builds the sp enum.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildSpEnum(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);
            bool Auto = false;

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
            Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";

            sb = new System.Text.StringBuilder();

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Enum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("BEGIN");
            sb.Append("\n");
            sb.Append("	PRINT '<<<< Dropping Stored Procedure sp" + csn + "Enum >>>>'");
            sb.Append("\n");
            sb.Append("	DROP PROCEDURE [sp" + csn + "Enum]");
            sb.Append("\n");
            sb.Append("END");
            sb.Append("\n");
            sb.Append("GO\n");
            sb.Append("\n");
            sb.Append("/*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		PROCEDURE NAME: sp" + csn + "Enum");
            sb.Append("\n");
            sb.Append("**		Change History");
            sb.Append("\n");
            sb.Append("*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		Date:		Author:		Description:");
            sb.Append("\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created");
            sb.Append("\n");
            sb.Append("*******************************************************************************/");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("CREATE PROCEDURE sp" + csn + "Enum\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    //if (dr["ColumnName"].ToString() == (dbn + "_id")) continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "21": // VARBINARY
                            //sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " VARBINARY(MAX)" + " = NULL,\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLTexts.Text + " = NULL,\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLTexts.Text + " = NULL,\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                                else
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "1").Replace("PREC", "0") + " = NULL,\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", "0") + " = 0,\n");
                            }
                            else
                            {
                                // DECIMAL
                                //sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", "2") + " = 0,\n");
                                string sc = dr["NumericScale"].ToString();
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", sc) + " = 0,\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("    	@Begin" + CamCase(dr["ColumnName"].ToString()).PadRight(20) + " DATETIME = NULL,\n");
                            sb.Append("    	@End" + CamCase(dr["ColumnName"].ToString()).PadRight(22) + " DATETIME = NULL,\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("    	@Begin" + CamCase(dr["ColumnName"].ToString()).PadRight(20) + " DATETIME = NULL,\n");
                            sb.Append("    	@End" + CamCase(dr["ColumnName"].ToString()).PadRight(22) + " DATETIME = NULL,\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " BINARY(10) = NULL,\n");
                            break;
                    }
                }

                con2.Close();

                Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";
                dt.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append(" 	@" + "COUNT".PadRight(25) + "NUMERIC(10,0) = 0 OUTPUT\n");
            sb.Append("\n");
            sb.Append("AS\n");
            sb.Append("    	SET NOCOUNT ON\n");
            sb.Append("\n");
            sb.Append("\n");

            bool first = true;

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    //if (dr["ColumnName"].ToString() == (dbn + "_id")) continue;
                    if (first)
                    {
                        sb.Append(" IF(1 = 1)\n");
                        GenerateEnhancedEnum(dt, sb, dbn);

                        first = false;
                    }

                    sb.Append("	ELSE ");

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("	IF   (@" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)\n");
                            sb.Append("		SELECT ");
                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n");

                            sb.Append("		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("	IF   (@" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)\n");
                            sb.Append("		SELECT ");
                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n");

                            sb.Append("		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("	IF   (@" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)\n");
                            sb.Append("		SELECT ");
                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n");

                            sb.Append("		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            break;

                        case "18": // TEXT
                            sb.Append("	IF   (@" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)\n");
                            sb.Append("		SELECT ");
                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n");

                            sb.Append("		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("	IF   (@" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)\n");
                            sb.Append("		SELECT ");
                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n");

                            sb.Append("		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("	IF (@" + CamCase(dr["ColumnName"].ToString()) + " > 0)\n");
                                    sb.Append("		SELECT ");
                                    try
                                    {
                                        foreach (DataRow drr in dt.Rows)
                                            sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                        sb.Remove(sb.ToString().Length - 1, 1);
                                    }
                                    catch { }
                                    sb.Append("\n");
                                    sb.Append("		FROM [" + dbn + "] \n");
                                    sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] = @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                                    break;
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("	IF (@" + CamCase(dr["ColumnName"].ToString()) + " > 0)\n");
                                    sb.Append("		SELECT ");
                                    try
                                    {
                                        foreach (DataRow drr in dt.Rows)
                                            sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                        sb.Remove(sb.ToString().Length - 1, 1);
                                    }
                                    catch { }
                                    sb.Append("\n");
                                    sb.Append("		FROM [" + dbn + "] \n");
                                    sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] = @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("	IF (@" + CamCase(dr["ColumnName"].ToString()) + " is not NULL )\n");
                                sb.Append("		SELECT ");
                                try
                                {
                                    foreach (DataRow drr in dt.Rows)
                                        sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                    sb.Remove(sb.ToString().Length - 1, 1);
                                }
                                catch { }
                                sb.Append("\n");
                                sb.Append("		FROM [" + dbn + "] \n");
                                sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] = @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("	IF (@" + CamCase(dr["ColumnName"].ToString()) + " > 0)\n");
                                sb.Append("		SELECT ");
                                try
                                {
                                    foreach (DataRow drr in dt.Rows)
                                        sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                    sb.Remove(sb.ToString().Length - 1, 1);
                                }
                                catch { }
                                sb.Append("\n");
                                sb.Append("		FROM [" + dbn + "] \n");
                                sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] = @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("	IF (@" + CamCase(dr["ColumnName"].ToString()) + " > 0)\n");
                                sb.Append("		SELECT ");
                                try
                                {
                                    foreach (DataRow drr in dt.Rows)
                                        sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                    sb.Remove(sb.ToString().Length - 1, 1);
                                }
                                catch { }
                                sb.Append("\n");
                                sb.Append("		FROM [" + dbn + "] \n");
                                sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] = @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("	IF  (@Begin" + CamCase(dr["ColumnName"].ToString()) + " is not NULL) and\n");
                            sb.Append("	      (@End" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)  \n");

                            sb.Append("		SELECT ");

                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] >= @Begin" + CamCase(dr["ColumnName"].ToString()) + " and\n");
                            sb.Append("			[" + dr["ColumnName"].ToString() + "] <= @End" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            sb.Append("\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("	IF  (@Begin" + CamCase(dr["ColumnName"].ToString()) + " is not NULL) and\n");
                            sb.Append("	      (@End" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)  \n");

                            sb.Append("		SELECT ");

                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] >= @Begin" + CamCase(dr["ColumnName"].ToString()) + " and\n");
                            sb.Append("			[" + dr["ColumnName"].ToString() + "] <= @End" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            sb.Append("\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("	IF   (@" + CamCase(dr["ColumnName"].ToString()) + " is not NULL)\n");
                            sb.Append("		SELECT ");
                            try
                            {
                                foreach (DataRow drr in dt.Rows)
                                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                                sb.Remove(sb.ToString().Length - 1, 1);
                            }
                            catch { }
                            sb.Append("\n");

                            sb.Append("		FROM [" + dbn + "] \n");
                            sb.Append("		WHERE [" + dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            break;
                    }
                }

                con2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("	ELSE\n");

            sb.Append("		SELECT ");
            try
            {
                foreach (DataRow drr in dt.Rows)
                    sb.Append(" [" + drr["ColumnName"].ToString() + "],");

                sb.Remove(sb.ToString().Length - 1, 1);
            }
            catch { }
            sb.Append("\n");
            sb.Append("		FROM [" + dbn + "] \n");
            sb.Append("\n");

            sb.Append("	SELECT @COUNT=@@rowcount \n");
            sb.Append("\n");
            sb.Append("    	RETURN 0\n");
            sb.Append("\n");
            sb.Append("GO\n");

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Enum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("PRINT '<<<< Created Stored Procedure sp" + csn + "Enum >>>>'");
            sb.Append("\n");
            sb.Append("ELSE");
            sb.Append("\n");
            sb.Append("PRINT '<<< Failed Creating Stored Procedure sp" + csn + "Enum >>>'");
            sb.Append("\n");
            sb.Append("GO\n\n");

            return (sb.ToString());
        }

        /// <summary>
        /// Builds the sp enum.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildSpEnumEnhanced(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);
            bool Auto = false;

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
            Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";

            sb = new System.Text.StringBuilder();

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Enum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("BEGIN");
            sb.Append("\n");
            sb.Append("	PRINT '<<<< Dropping Stored Procedure sp" + csn + "Enum >>>>'");
            sb.Append("\n");
            sb.Append("	DROP PROCEDURE [sp" + csn + "Enum]");
            sb.Append("\n");
            sb.Append("END");
            sb.Append("\n");
            sb.Append("GO\n");
            sb.Append("\n");
            sb.Append("/*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		PROCEDURE NAME: sp" + csn + "Enum");
            sb.Append("\n");
            sb.Append("**		Change History");
            sb.Append("\n");
            sb.Append("*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		Date:		Author:		Description:");
            sb.Append("\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created");
            sb.Append("\n");
            sb.Append("*******************************************************************************/");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("CREATE PROCEDURE sp" + csn + "Enum\n");
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    //if (dr["ColumnName"].ToString() == (dbn + "_id")) continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " +
                                      txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" +
                                      txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "21": // VARBINARY
                            //sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " VARBINARY(MAX)" +
                                      " = NULL,\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLTexts.Text +
                                      " = NULL,\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLTexts.Text +
                                      " = NULL,\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                                else
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" ||
                                     dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " +
                                          txtSQLNumbers.Text.Replace("SIZE", "1").Replace("PREC", "0") + " = NULL,\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " +
                                          txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", "0") + " = 0,\n");
                            }
                            else
                            {
                                // DECIMAL
                                //sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", "2") + " = 0,\n");
                                string sc = dr["NumericScale"].ToString();
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " +
                                          txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", sc) + " = 0,\n");
                                sb.Append("    	@Begin" + CamCase(dr["ColumnName"].ToString()).PadRight(20) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", sc) + " = 0,\n");
                                sb.Append("    	@End" + CamCase(dr["ColumnName"].ToString()).PadRight(22) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", sc) + " = 0,\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("    	@Begin" + CamCase(dr["ColumnName"].ToString()).PadRight(20) + " DATETIME = NULL,\n");
                            sb.Append("    	@End" + CamCase(dr["ColumnName"].ToString()).PadRight(22) + " DATETIME = NULL,\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("    	@Begin" + CamCase(dr["ColumnName"].ToString()).PadRight(20) + " DATETIME = NULL,\n");
                            sb.Append("    	@End" + CamCase(dr["ColumnName"].ToString()).PadRight(22) + " DATETIME = NULL,\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " BINARY(10) = NULL,\n");
                            break;
                    }
                }

                con2.Close();

                Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";
                dt.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append(" 	@" + "COUNT".PadRight(25) + "NUMERIC(10,0) = 0 OUTPUT\n");
            sb.Append("\n");
            sb.Append("AS\n");
            sb.Append("    	SET NOCOUNT ON\n");
            sb.Append("\n");
            sb.Append("\n");

            GenerateEnhancedEnum(dt, sb, dbn);

            sb.Append("\n\n      SELECT @COUNT=@@rowcount \n");
            sb.Append("\n");
            sb.Append("    	RETURN 0\n");
            sb.Append("\n");
            sb.Append("GO\n");

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Enum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("PRINT '<<<< Created Stored Procedure sp" + csn + "Enum >>>>'");
            sb.Append("\n");
            sb.Append("ELSE");
            sb.Append("\n");
            sb.Append("PRINT '<<< Failed Creating Stored Procedure sp" + csn + "Enum >>>'");
            sb.Append("\n");
            sb.Append("GO\n\n");

            return (sb.ToString());
        }

        private void GenerateEnhancedEnum(DataTable dt, StringBuilder sb, string dbn)
        {
            bool first = true;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    //if (dr["ColumnName"].ToString() == (dbn + "_id")) continue;
                    if (first)
                    {
                        sb.Append("      SELECT ");

                        try
                        {
                            foreach (DataRow drr in dt.Rows)
                            {
                                if (drr["ProviderType"].ToString() == "29")
                                {
                                    continue;
                                }
                                sb.Append(" [" + drr["ColumnName"].ToString() + "],");
                            }

                            sb.Remove(sb.ToString().Length - 1, 1);
                        }
                        catch
                        {
                        }
                        sb.Append("\n");

                        sb.Append("      FROM [" + dbn + "] \n      WHERE ");
                        first = false;
                    }
                    else
                    {
                        sb.Append("      AND ");
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR

                            sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;

                        case "18": // TEXT
                            sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                              dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) +
                                              "))\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                              dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) +
                                              "))\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" ||
                                     dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                          dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) +
                                          "))\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                          dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) +
                                          "))\n");
                            }
                            else if (dr["NumericScale"].ToString() == "6")
                            {
                                sb.Append("((@Begin" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                       dr["ColumnName"].ToString() + "] >= @Begin" + CamCase(dr["ColumnName"].ToString()) +
                                       "))\n");
                                sb.Append("      AND ((@End" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                          dr["ColumnName"].ToString() + "] <= @End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            }
                            else
                            {
                                // DECIMAL
                                //sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                //          dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) +
                                //          "))\n");
                                sb.Append("((@Begin" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                          dr["ColumnName"].ToString() + "] >= @Begin" + CamCase(dr["ColumnName"].ToString()) +
                                          "))\n");
                                sb.Append("      AND ((@End" + CamCase(dr["ColumnName"].ToString()) + " = 0) OR ([" +
                                          dr["ColumnName"].ToString() + "] <= @End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("((@Begin" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] >= @Begin" + CamCase(dr["ColumnName"].ToString()) +
                                      "))\n");
                            sb.Append("      AND ((@End" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] <= @End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("((@Begin" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] >= @Begin" + CamCase(dr["ColumnName"].ToString()) +
                                      "))\n");
                            sb.Append("      AND ((@End" + CamCase(dr["ColumnName"].ToString()) + " IS NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] <= @End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("((@" + CamCase(dr["ColumnName"].ToString()) + " = NULL) OR ([" +
                                      dr["ColumnName"].ToString() + "] LIKE @" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            break;
                    }
                }
                sb.Append(" ORDER BY [" + dbn + "_id" + "] ASC\n");

                con2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// Builds the exists.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildExists(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Exist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("BEGIN");
            sb.Append("\n");
            sb.Append("	PRINT '<<<< Dropping Stored Procedure sp" + csn + "Exist >>>>'");
            sb.Append("\n");
            sb.Append("	DROP PROCEDURE [sp" + csn + "Exist]");
            sb.Append("\n");
            sb.Append("END");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("/*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		PROCEDURE NAME: sp" + csn + "Exist");
            sb.Append("\n");
            sb.Append("**		Change History");
            sb.Append("\n");
            sb.Append("*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		Date:		Author:		Description:");
            sb.Append("\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created");
            sb.Append("\n");
            sb.Append("*******************************************************************************/");
            sb.Append("\n");
            sb.Append("CREATE PROCEDURE sp" + csn + "Exist");
            sb.Append("\n");
            sb.Append("(");
            sb.Append("\n");
            sb.Append("@" + csn + "ID        NUMERIC(10) = 0,");
            sb.Append("\n");
            sb.Append("@COUNT          INT = 0 OUTPUT");
            sb.Append("\n");
            sb.Append(")");
            sb.Append("\n");
            sb.Append("AS");
            sb.Append("\n");
            sb.Append("SET NOCOUNT ON");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("-- check if a record with the specified id exists");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("SELECT @COUNT = COUNT(" + dbn + "_id) \nFROM [" + dbn + "] \nWHERE " + dbn + "_id = @" + csn + "ID");
            sb.Append("\n");
            sb.Append("RETURN 0");
            sb.Append("\n");
            sb.Append("------------------------------------------------");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Exist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("PRINT '<<<< Created Stored Procedure sp" + csn + "Exist >>>>'");
            sb.Append("\n");
            sb.Append("ELSE");
            sb.Append("\n");
            sb.Append("PRINT '<<< Failed Creating Stored Procedure sp" + csn + "Exist >>>'");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");

            return (sb.ToString());
        }

        /// <summary>
        /// Handles the Click event of the btnClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnClear_Click(object sender, System.EventArgs e)
        {
            txtOutput.Text = "";
        }

        /// <summary>
        /// Builds the class file.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildClassFile(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);
            bool Auto = false;

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
            Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";

            sb = new System.Text.StringBuilder();

            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Text;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("\n");
            sb.Append("using Vetapp.Engine.Common;\n");
            sb.Append("\n");
            sb.Append("namespace Vetapp.Engine.DataAccessLayer.Data\n");
            sb.Append("{\n");
            sb.Append("	/// <summary>\n");
            sb.Append("	/// Copyright (c) " + DateTime.UtcNow.Year.ToString() + " Haytham Allos.  San Diego, California, USA\n");
            sb.Append("	/// All Rights Reserved\n");
            sb.Append("	/// \n");
            sb.Append("	/// File:  " + csn + ".cs\n");
            sb.Append("	/// History\n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// 001	" + txtCB.Text + "	" + DateTime.UtcNow.ToShortDateString() + "	Created\n");
            sb.Append("	/// \n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// Abstracts the " + csn + " database table.\n");
            sb.Append("	/// </summary>\n");
            sb.Append("	public class " + csn + "\n");
            sb.Append("	{\n");
            sb.Append("		//Attributes\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type String</summary>\n");
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		private byte[] _byte" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                    // NUMERIC(10)
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                else
                                    // NUMERIC(10)
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		private " + _strBoolText + " _b" + CamCase(dr["ColumnName"].ToString()) + " = " + _strBoolDefaultValue + ";\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		private double _d" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("		private DateTime _dt" + CamCase(dr["ColumnName"].ToString()) + " = dtNull;\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("		private DateTime _dt" + CamCase(dr["ColumnName"].ToString()) + " = dtNull;\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " type: " + dr["ProviderType"].ToString());
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		private object _o" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("\n");

            //sb.Append("		private Config _config = null;\n");
            sb.Append("		private ErrorCode _errorCode = null;\n");
            //sb.Append("		private Logger _oLog = null;\n");
            //sb.Append("		private string _strLognameText = \"DataAccessLayer-Data-" + csn + "\";\n");
            sb.Append("		private bool _hasError = false;\n");
            sb.Append("		private static DateTime dtNull = new DateTime();\n");
            sb.Append("\n");

            sb.Append("		/// <summary>HasError Property in class " + csn + " and is of type bool</summary>\n");
            sb.Append("		public static readonly string ENTITY_NAME = \"" + csn + "\"; //Table name to abstract\n");
            sb.Append("\n");
            sb.Append("		// DB Field names\n");

            sb.Append("		/// <summary>ID Database field</summary>\n");
            sb.Append("		public static readonly string DB_FIELD_ID = \"" + dbn + "_id\"; //Table id field name\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                {
                    sb.Append("		/// <summary>" + dr["ColumnName"].ToString() + " Database field </summary>\n");
                    sb.Append("		public static readonly string DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + " = \"" + dr["ColumnName"].ToString() + "\"; //Table " + CamCase(dr["ColumnName"].ToString()) + " field name\n");
                }
            }

            sb.Append("\n");
            sb.Append("		// Attribute variables\n");

            sb.Append("		/// <summary>TAG_ID Attribute type string</summary>\n");
            sb.Append("		public static readonly string TAG_ID = \"" + csn + "ID\"; //Attribute id  name\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                {
                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                    sb.Append("		public static readonly string TAG_" + dr["ColumnName"].ToString().ToUpper() + " = \"" + CamCase(dr["ColumnName"].ToString()) + "\"; //Table " + CamCase(dr["ColumnName"].ToString()) + " field name\n");
                }
            }

            sb.Append("\n");
            sb.Append("		// Stored procedure names\n");
            sb.Append("		private static readonly string SP_INSERT_NAME = \"sp" + csn + "Insert\"; //Insert sp name\n");
            sb.Append("		private static readonly string SP_UPDATE_NAME = \"sp" + csn + "Update\"; //Update sp name\n");
            sb.Append("		private static readonly string SP_DELETE_NAME = \"sp" + csn + "Delete\"; //Delete sp name\n");
            sb.Append("		private static readonly string SP_LOAD_NAME = \"sp" + csn + "Load\"; //Load sp name\n");
            sb.Append("		private static readonly string SP_EXIST_NAME = \"sp" + csn + "Exist\"; //Exist sp name\n");
            sb.Append("\n");
            sb.Append("		//properties\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type byte[]</summary>\n");
                            sb.Append("		public byte[] " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _byte" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_byte" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type bool</summary>\n");
                                sb.Append("		public " + _strBoolText + " " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _b" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_b" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type double</summary>\n");
                                sb.Append("		public double " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _d" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_d" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type DateTime</summary>\n");
                            sb.Append("		public DateTime " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dt" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dt" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type DateTime</summary>\n");
                            sb.Append("		public DateTime " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dt" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dt" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("\n\n");
            sb.Append("/*********************** CUSTOM NON-META BEGIN *********************/\n");
            sb.Append("\n");
            sb.Append("/*********************** CUSTOM NON-META END *********************/\n");
            sb.Append("\n\n");
            sb.Append("		/// <summary>HasError Property in class " + csn + " and is of type bool</summary>\n");
            sb.Append("		public  bool HasError \n");
            sb.Append("		{\n");
            sb.Append("			get{return _hasError;}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Error Property in class " + csn + " and is of type ErrorCode</summary>\n");
            sb.Append("		public ErrorCode Error \n");
            sb.Append("		{\n");
            sb.Append("			get{return _errorCode;}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("//Constructors\n");
            sb.Append("		/// <summary>" + csn + " empty constructor</summary>\n");
            sb.Append("		public " + csn + "()\n");
            sb.Append("		{\n");
            sb.Append("		}\n");
            //sb.Append("		/// <summary>" + csn + " constructor takes a Config</summary>\n");
            //sb.Append("		public " + csn + "(Config pConfig)\n");
            //sb.Append("		{\n");
            ////sb.Append("			_config = pConfig;\n");
            ////sb.Append("			_oLog = new Logger(_strLognameText);\n");
            //sb.Append("		}\n");
            sb.Append("		/// <summary>" + csn + " constructor takes " + csn + "ID and a SqlConnection</summary>\n");
            sb.Append("		public " + csn + "(long l, SqlConnection conn) \n");
            sb.Append("		{\n");
            sb.Append("			" + csn + "ID = l;\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				sqlLoad(conn);\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>" + csn + " Constructor takes pStrData and Config</summary>\n");
            sb.Append("		public " + csn + "(string pStrData)\n");
            sb.Append("		{\n");
            //sb.Append("			_config = pConfig;\n");
            //sb.Append("			_oLog = new Logger(_strLognameText);\n");
            sb.Append("			Parse(pStrData);\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>" + csn + " Constructor takes SqlDataReader</summary>\n");
            sb.Append("		public " + csn + "(SqlDataReader rd)\n");
            sb.Append("		{\n");
            sb.Append("			sqlParseResultSet(rd);\n");
            sb.Append("		}\n");

            sb.Append("		/// <summary>\n");
            sb.Append("		///     Dispose of this object's resources.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public void Dispose()\n");
            sb.Append("		{\n");
            sb.Append("			Dispose(true);\n");
            sb.Append("			GC.SuppressFinalize(true); // as a service to those who might inherit from us\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///		Free the instance variables of this object.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		protected virtual void Dispose(bool disposing)\n");
            sb.Append("		{\n");
            sb.Append("			if (! disposing)\n");
            sb.Append("				return; // we're being collected, so let the GC take care of this object\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		// public methods\n");
            sb.Append("		/// <summary>ToString is overridden to display all properties of the " + csn + " Class</summary>\n");
            sb.Append("		public override string ToString() \n");
            sb.Append("		{\n");

            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");

            sb.Append("			sbReturn.Append(TAG_ID + \":  \" + " + csn + "ID.ToString() + \"\\n\");\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + " + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
            }
            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");

            sb.Append("		}\n");
            sb.Append("		/// <summary>Creates well formatted XML - includes all properties of " + csn + "</summary>\n");
            sb.Append("		public string ToXml() \n");
            sb.Append("		{\n");
            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");
            sb.Append("			sbReturn.Append(\"<" + csn + ">\\n\");\n");
            sb.Append("			sbReturn.Append(\"<\" + TAG_ID + \">\" + " + csn + "ID + \"</\" + TAG_ID + \">\\n\");\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + " + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"</\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"></\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"</\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
            }

            sb.Append("			sbReturn.Append(\"</" + csn + ">\" + \"\\n\");\n");
            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Parse accepts a string in XML format and parses values</summary>\n");
            sb.Append("		public void Parse(string pStrXml)\n");
            sb.Append("		{\n");

            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				XmlDocument xmlDoc = null;\n");
            sb.Append("				string strXPath = null;\n");
            sb.Append("				XmlNodeList xNodes = null;\n");
            sb.Append("\n");
            sb.Append("				xmlDoc = new XmlDocument();\n");
            sb.Append("				xmlDoc.LoadXml(pStrXml);\n");
            sb.Append("\n");
            sb.Append("				// get the element\n");
            sb.Append("				strXPath = \"//\" + ENTITY_NAME;\n");
            sb.Append("				xNodes = xmlDoc.SelectNodes(strXPath);\n");
            sb.Append("				foreach (XmlNode xNode in xNodes)\n");
            sb.Append("				{\n");
            sb.Append("					Parse(xNode);\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");

            sb.Append("		}		\n");
            sb.Append("		/// <summary>Parse accepts an XmlNode and parses values</summary>\n");
            sb.Append("		public void Parse(XmlNode xNode)\n");
            sb.Append("		{\n");

            sb.Append("			XmlNode xResultNode = null;\n");
            sb.Append("			string strTmp = null;\n");
            sb.Append("\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_ID);\n");
            sb.Append("				strTmp = xResultNode.InnerText;\n");
            sb.Append("				" + csn + "ID = (long) Convert.ToInt32(strTmp);\n");
            sb.Append("			}\n");
            sb.Append("			catch  \n");
            sb.Append("			{\n");
            sb.Append("			}\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("            //Cannot reliably convert byte[] to string.\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                    break;
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = false;\n");
                                sb.Append("			}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                                break;
                            }
                            else if (dr["NumericScale"].ToString() == "6")
                            {
                                // Double
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToDouble(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("		}\n");

            sb.Append("		/// <summary>Calls sqlLoad() method which gets record from database with " + dbn + "_id equal to the current object's " + csn + "ID </summary>\n");
            sb.Append("		public void Load(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"LOAD\", ToString());\n");
            sb.Append("				sqlLoad(conn);\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Calls sqlUpdate() method which record record from database with current object values where " + dbn + "_id equal to the current object's " + csn + "ID </summary>\n");
            sb.Append("		public void Update(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			bool bExist = false;\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"UPDATE\", ToString());\n");
            sb.Append("				bExist = Exist(conn);\n");
            sb.Append("				if (bExist)\n");
            sb.Append("				{\n");
            sb.Append("					sqlUpdate(conn);\n");
            sb.Append("				}\n");
            sb.Append("				else\n");
            sb.Append("				{\n");
            //sb.Append("				_log(\"NOT_EXIST\", ToString());\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Calls sqlInsert() method which inserts a record into the database with current object values</summary>\n");
            sb.Append("		public void Save(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				bool bExist = false;\n");
            sb.Append("\n");
            //sb.Append("				_log(\"INSERT\", ToString());\n");

            sb.Append("				bExist = Exist(conn);\n");
            sb.Append("				if (!bExist)\n");
            sb.Append("				{\n");
            sb.Append("					sqlInsert(conn);\n");
            sb.Append("				}\n");
            sb.Append("				else\n");
            sb.Append("				{\n");
            //sb.Append("				_log(\"ALREADY_EXISTS\", ToString());\n");
            sb.Append("					sqlUpdate(conn);\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Calls sqlDelete() method which delete's the record from database where where " + dbn + "_id equal to the current object's " + csn + "ID </summary>\n");
            sb.Append("		public void Delete(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"DELETE\", ToString());\n");
            sb.Append("				sqlDelete(conn);\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Calls sqlExists() returns true if the record exists, false if not </summary>\n");
            sb.Append("		public bool Exist(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			bool bReturn = false;\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				bReturn = sqlExist(conn);\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("			return bReturn;\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Prompt user to enter Property values</summary>\n");
            sb.Append("		public void Prompt()\n");
            sb.Append("		{\n");
            sb.Append("			try \n");
            sb.Append("			{\n");

            if (!Auto)
            {
                sb.Append("				{\n");
                sb.Append("					Console.WriteLine(TAG_ID + \":  \");\n");
                sb.Append("					try\n");
                sb.Append("					{\n");
                sb.Append("						" + csn + "ID = long.Parse(Console.ReadLine());\n");
                sb.Append("					}\n");
                sb.Append("					catch \n");
                sb.Append("					{\n");
                sb.Append("						" + csn + "ID = 0;\n");
                sb.Append("					}\n");
                sb.Append("				}\n");
            }
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("             //Cannot reliably convert byte[] to string.\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\n");
                                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(Console.ReadLine());\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\n");
                                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("\n");
                                sb.Append("				Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(Console.ReadLine());\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("				try\n");
                            sb.Append("				{\n");
                            sb.Append("					Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(Console.ReadLine());\n");
                            sb.Append("				}\n");
                            sb.Append("				catch \n");
                            sb.Append("				{\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("				}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("				try\n");
                            sb.Append("				{\n");
                            sb.Append("					Console.WriteLine(" + NS(csn) + ".TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(Console.ReadLine());\n");
                            sb.Append("				}\n");
                            sb.Append("				catch \n");
                            sb.Append("				{\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("				}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sb.Append("\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		\n");
            sb.Append("		//protected\n");
            sb.Append("		/// <summary>Inserts row of data into the database</summary>\n");
            sb.Append("		protected void sqlInsert(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			SqlCommand cmd = null;\n");

            if (!Auto)
                sb.Append("			SqlParameter param" + csn + "ID = null;\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id" && dr["ColumnName"].ToString() != "date_modified")
                    sb.Append("			SqlParameter param" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
            }

            sb.Append("			SqlParameter paramPKID = null;\n");
            sb.Append("\n");
            sb.Append("			//Create a command object identifying\n");
            sb.Append("			//the stored procedure	\n");
            sb.Append("			cmd = new SqlCommand(SP_INSERT_NAME, conn);\n");
            sb.Append("\n");
            sb.Append("			//Set the command object so it knows\n");
            sb.Append("			//to execute a stored procedure\n");
            sb.Append("			cmd.CommandType = CommandType.StoredProcedure;\n");
            sb.Append("			\n");
            sb.Append("			// parameters\n");

            if (!Auto)
            {
                sb.Append("			param" + csn + "ID = new SqlParameter(\"@\" + TAG_ID, " + csn + "ID);\n");
                sb.Append("			param" + csn + "ID.DbType = DbType.Int32;\n");
                sb.Append("			param" + csn + "ID.Direction = ParameterDirection.Input;\n");
            }
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;

                    sb.Append("\n");
                    if (dr["ProviderType"].ToString() == "15"
                        || dr["ProviderType"].ToString() == "4")
                    {
                        switch (dr["ColumnName"].ToString())
                        {
                            case "date_created":
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DateTime.UtcNow);\n");
                                break;

                            case "date_modified":
                                break;

                            default:
                                sb.Append("			if (!dtNull.Equals(" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                                sb.Append("			{\n");
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("			}\n");
                                sb.Append("			else\n");
                                sb.Append("			{\n");
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                                sb.Append("			}\n");
                                break;
                        }
                    }
                    else
                        sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Size = " + dr["ColumnSize"].ToString() + ";\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Size = " + dr["ColumnSize"].ToString() + ";\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Binary;\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Size = " + dr["ColumnSize"].ToString() + ";\n");
                            break;

                        case "18": // TEXT
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Int32;\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Int32;\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Boolean;\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Int32;\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Decimal;\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            if (dr["ColumnName"].ToString() != "date_modified")
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.DateTime;\n");
                            break;

                        case "4": //DATETETIME
                            if (dr["ColumnName"].ToString() != "date_modified")
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.DateTime;\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Object;\n");
                            break;
                    }
                    if (dr["ColumnName"].ToString() != "date_modified")
                        sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sb.Append("\n");
            sb.Append("			paramPKID = new SqlParameter();\n");
            sb.Append("			paramPKID.ParameterName = \"@PKID\";\n");
            sb.Append("			paramPKID.DbType = DbType.Int32;\n");
            sb.Append("			paramPKID.Direction = ParameterDirection.Output;\n");
            sb.Append("\n");
            sb.Append("			//Add parameters to command, which\n");
            sb.Append("			//will be passed to the stored procedure\n");

            if (!Auto)
                sb.Append("			cmd.Parameters.Add(param" + csn + "ID);\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id" && dr["ColumnName"].ToString() != "date_modified")
                    sb.Append("			cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
            }

            sb.Append("			cmd.Parameters.Add(paramPKID);\n");
            sb.Append("\n");
            sb.Append("			// execute the command\n");
            sb.Append("			cmd.ExecuteNonQuery();\n");
            sb.Append("			// assign the primary kiey\n");
            sb.Append("			string strTmp;\n");
            sb.Append("			strTmp = cmd.Parameters[\"@PKID\"].Value.ToString();\n");
            sb.Append("			" + csn + "ID = long.Parse(strTmp);\n");
            sb.Append("\n");
            sb.Append("			// cleanup to help GC\n");
            if (!Auto)
                sb.Append("			param" + csn + "ID = null;\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }

                if (dr["ColumnName"].ToString() != dbn + "_id" && dr["ColumnName"].ToString() != "date_modified")
                    sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
            }

            sb.Append("			paramPKID = null;\n");
            sb.Append("			cmd = null;\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Check to see if the row exists in database</summary>\n");
            sb.Append("		protected bool sqlExist(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			bool bExist = false;\n");
            sb.Append("\n");
            sb.Append("			SqlCommand cmd = null;\n");
            sb.Append("			SqlParameter param" + csn + "ID = null;\n");
            sb.Append("			SqlParameter paramCount = null;\n");
            sb.Append("\n");
            sb.Append("			cmd = new SqlCommand(SP_EXIST_NAME, conn);\n");
            sb.Append("			cmd.CommandType = CommandType.StoredProcedure;\n");
            sb.Append("\n");
            sb.Append("			param" + csn + "ID = new SqlParameter(\"@\" + TAG_ID, " + csn + "ID);\n");
            sb.Append("			param" + csn + "ID.Direction = ParameterDirection.Input;\n");
            sb.Append("			param" + csn + "ID.DbType = DbType.Int32;\n");
            sb.Append("\n");
            sb.Append("			paramCount = new SqlParameter();\n");
            sb.Append("			paramCount.ParameterName = \"@COUNT\";\n");
            sb.Append("			paramCount.DbType = DbType.Int32;\n");
            sb.Append("			paramCount.Direction = ParameterDirection.Output;\n");
            sb.Append("\n");
            sb.Append("			cmd.Parameters.Add(param" + csn + "ID);\n");
            sb.Append("			cmd.Parameters.Add(paramCount);\n");
            sb.Append("			cmd.ExecuteNonQuery();\n");
            sb.Append("\n");
            sb.Append("			string strTmp;\n");
            sb.Append("			int nCount = 0;\n");
            sb.Append("			strTmp = cmd.Parameters[\"@COUNT\"].Value.ToString();\n");
            sb.Append("			nCount = int.Parse(strTmp);\n");
            sb.Append("			if (nCount > 0)\n");
            sb.Append("			{\n");
            sb.Append("				bExist = true;\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("			// cleanup\n");
            sb.Append("			param" + csn + "ID = null;\n");
            sb.Append("			paramCount = null;\n");
            sb.Append("			cmd = null;\n");
            sb.Append("\n");
            sb.Append("			return bExist;\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Updates row of data in database</summary>\n");
            sb.Append("		protected void sqlUpdate(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			SqlCommand cmd = null;\n");
            sb.Append("			SqlParameter param" + csn + "ID = null;\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id" && dr["ColumnName"].ToString() != "date_created")
                    sb.Append("			SqlParameter param" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
            }

            sb.Append("			SqlParameter paramPKID = null;\n");
            sb.Append("\n");
            sb.Append("			//Create a command object identifying\n");
            sb.Append("			//the stored procedure	\n");
            sb.Append("			cmd = new SqlCommand(SP_UPDATE_NAME, conn);\n");
            sb.Append("\n");
            sb.Append("			//Set the command object so it knows\n");
            sb.Append("			//to execute a stored procedure\n");
            sb.Append("			cmd.CommandType = CommandType.StoredProcedure;\n");
            sb.Append("			\n");
            sb.Append("			// parameters\n");
            sb.Append("\n");
            sb.Append("			param" + csn + "ID = new SqlParameter(\"@\" + TAG_ID, " + csn + "ID);\n");
            sb.Append("			param" + csn + "ID.DbType = DbType.Int32;\n");
            sb.Append("			param" + csn + "ID.Direction = ParameterDirection.Input;\n");
            sb.Append("\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;

                    sb.Append("\n");
                    if (dr["ProviderType"].ToString() == "15"
                        || dr["ProviderType"].ToString() == "4")
                    {
                        switch (dr["ColumnName"].ToString())
                        {
                            case "date_created":
                                break;

                            case "date_modified":
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DateTime.UtcNow);\n");
                                break;

                            default:
                                sb.Append("			if (!dtNull.Equals(" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                                sb.Append("			{\n");
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("			}\n");
                                sb.Append("			else\n");
                                sb.Append("			{\n");
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                                sb.Append("			}\n");
                                break;
                        }
                    }
                    else
                        sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Size = " + dr["ColumnSize"].ToString() + ";\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Size = " + dr["ColumnSize"].ToString() + ";\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Binary;\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Size = " + dr["ColumnSize"].ToString() + ";\n");
                            break;

                        case "18": // TEXT
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.String;\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Int32;\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Int32;\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Boolean;\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Int32;\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Decimal;\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            if (dr["ColumnName"].ToString() != "date_created")
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.DateTime;\n");
                            break;

                        case "4": //DATETETIME
                            if (dr["ColumnName"].ToString() != "date_created")
                                sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.DateTime;\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".DbType = DbType.Object;\n");
                            break;
                    }
                    if (dr["ColumnName"].ToString() != "date_created")
                        sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sb.Append("\n");
            sb.Append("			paramPKID = new SqlParameter();\n");
            sb.Append("			paramPKID.ParameterName = \"@PKID\";\n");
            sb.Append("			paramPKID.DbType = DbType.Int32;\n");
            sb.Append("			paramPKID.Direction = ParameterDirection.Output;\n");
            sb.Append("\n");
            sb.Append("			//Add parameters to command, which\n");
            sb.Append("			//will be passed to the stored procedure\n");
            sb.Append("			cmd.Parameters.Add(param" + csn + "ID);\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id" && dr["ColumnName"].ToString() != "date_created")
                    sb.Append("			cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
            }

            sb.Append("			cmd.Parameters.Add(paramPKID);\n");
            sb.Append("\n");
            sb.Append("			// execute the command\n");
            sb.Append("			cmd.ExecuteNonQuery();\n");
            sb.Append("			string s;\n");
            sb.Append("			s = cmd.Parameters[\"@PKID\"].Value.ToString();\n");
            sb.Append("			" + csn + "ID = long.Parse(s);\n");
            sb.Append("\n");
            sb.Append("			// cleanup\n");
            sb.Append("			param" + csn + "ID = null;\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id" && dr["ColumnName"].ToString() != "date_created")
                    sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
            }

            sb.Append("			paramPKID = null;\n");
            sb.Append("			cmd = null;\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Deletes row of data in database</summary>\n");
            sb.Append("		protected void sqlDelete(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			SqlCommand cmd = null;\n");
            sb.Append("			SqlParameter param" + csn + "ID = null;\n");
            sb.Append("\n");
            sb.Append("			cmd = new SqlCommand(SP_DELETE_NAME, conn);\n");
            sb.Append("			cmd.CommandType = CommandType.StoredProcedure;\n");
            sb.Append("			param" + csn + "ID = new SqlParameter(\"@\" + TAG_ID, " + csn + "ID);\n");
            sb.Append("			param" + csn + "ID.DbType = DbType.Int32;\n");
            sb.Append("			param" + csn + "ID.Direction = ParameterDirection.Input;\n");
            sb.Append("			cmd.Parameters.Add(param" + csn + "ID);\n");
            sb.Append("			cmd.ExecuteNonQuery();\n");
            sb.Append("\n");
            sb.Append("			// cleanup to help GC\n");
            sb.Append("			param" + csn + "ID = null;\n");
            sb.Append("			cmd = null;\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Load row of data from database</summary>\n");
            sb.Append("		protected void sqlLoad(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			SqlCommand cmd = null;\n");
            sb.Append("			SqlParameter param" + csn + "ID = null;\n");
            sb.Append("			SqlDataReader rdr = null;\n");
            sb.Append("\n");
            sb.Append("			cmd = new SqlCommand(SP_LOAD_NAME, conn);\n");
            sb.Append("			cmd.CommandType = CommandType.StoredProcedure;\n");
            sb.Append("			param" + csn + "ID = new SqlParameter(\"@\" + TAG_ID, " + csn + "ID);\n");
            sb.Append("			param" + csn + "ID.DbType = DbType.Int32;\n");
            sb.Append("			param" + csn + "ID.Direction = ParameterDirection.Input;\n");
            sb.Append("			cmd.Parameters.Add(param" + csn + "ID);\n");
            sb.Append("			rdr = cmd.ExecuteReader();\n");
            sb.Append("			if (rdr.Read())\n");
            sb.Append("			{\n");
            sb.Append("				sqlParseResultSet(rdr);\n");
            sb.Append("			}\n");
            sb.Append("			// cleanup\n");
            sb.Append("			rdr.Dispose();\n");
            sb.Append("			rdr = null;\n");
            sb.Append("			param" + csn + "ID = null;\n");
            sb.Append("			cmd = null;\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Parse result set</summary>\n");
            sb.Append("		protected void sqlParseResultSet(SqlDataReader rdr)\n");
            sb.Append("		{\n");
            sb.Append("			this." + csn + "ID = long.Parse(rdr[DB_FIELD_ID].ToString());\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim();\n");
                            sb.Append("			}\n");
                            sb.Append("			catch{}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim();\n");
                            sb.Append("			}\n");
                            sb.Append("			catch{}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("         if(rdr[rdr.GetOrdinal(DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ")] != DBNull.Value)\n");
                            sb.Append("         {\n");
                            sb.Append("			       this." + CamCase(dr["ColumnName"].ToString()) + " = (byte[])rdr[rdr.GetOrdinal(DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + ")];\n");
                            sb.Append("         }\n");
                            sb.Append("			}\n");
                            sb.Append("			catch{}\n");
                            break;

                        case "18": // TEXT
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("			       this." + CamCase(dr["ColumnName"].ToString()) + " = rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim();\n");
                            sb.Append("			}\n");
                            sb.Append("			catch{}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("			       this." + CamCase(dr["ColumnName"].ToString()) + " = rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim();\n");
                            sb.Append("			}\n");
                            sb.Append("			catch{}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				this." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToInt32(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim());\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch{}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToInt32(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim());\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch{}\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim());\n");
                                sb.Append("			}\n");
                                sb.Append("			catch{}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToInt32(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim());\n");
                                sb.Append("			}\n");
                                sb.Append("			catch{}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "6")
                            {
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToDouble(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim());\n");
                                sb.Append("			}\n");
                                sb.Append("			catch{}\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim());\n");
                                sb.Append("			}\n");
                                sb.Append("			catch{}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("         try\n");
                            sb.Append("			{\n");
                            sb.Append("				this." + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString());\n");
                            sb.Append("			}\n");
                            sb.Append("			catch \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("         try\n");
                            sb.Append("			{\n");
                            sb.Append("				this." + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString());\n");
                            sb.Append("			}\n");
                            sb.Append("			catch \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("			this." + CamCase(dr["ColumnName"].ToString()) + " = rdr[DB_FIELD_" + dr["ColumnName"].ToString().ToUpper() + "].ToString().Trim();\n");
                            sb.Append("			}\n");
                            sb.Append("			catch{}\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sb.Append("		}\n");
            sb.Append("\n");
            //sb.Append("		//private\n");
            //sb.Append("		/// <summary>Log errors</summary>\n");
            //sb.Append("		private void _log(string pStrAction, string pStrMsgText) \n");
            //sb.Append("		{\n");
            //sb.Append("			if (_config != null )\n");
            //sb.Append("			{\n");
            //sb.Append("				if (_config.DoLogInfo)\n");
            //sb.Append("				{\n");
            //sb.Append("						_oLog.Log(pStrAction, pStrMsgText);\n");
            //sb.Append("				}\n");
            //sb.Append("			}\n");
            //sb.Append("\n");
            //sb.Append("		}\n");
            //sb.Append("\n");
            //sb.Append("		/// <summary>Unit Testing: Save, Delete, Update, Exist, Load and ToXml</summary>\n");
            //sb.Append("		public void Test(SqlConnection conn)\n");
            //sb.Append("		{\n");
            //sb.Append("			try \n");
            //sb.Append("			{\n");
            //sb.Append("				Console.WriteLine(\"What would you like to do?\");\n");
            //sb.Append("				Console.WriteLine(\"1.  Save.\");\n");
            //sb.Append("				Console.WriteLine(\"2.  Delete.\");\n");
            //sb.Append("				Console.WriteLine(\"3.  Update.\");\n");
            //sb.Append("				Console.WriteLine(\"4.  Exist.\");\n");
            //sb.Append("				Console.WriteLine(\"5.  Load.\");\n");
            //sb.Append("				Console.WriteLine(\"6.  ToXml.\");\n");
            //sb.Append("				Console.WriteLine(\"q.  Quit.\");\n");
            //sb.Append("				\n");
            //sb.Append("				string strAns = \"\";\n");
            //sb.Append("\n");
            //sb.Append("				strAns = Console.ReadLine();\n");
            //sb.Append("				if (strAns != \"q\")\n");
            //sb.Append("				{	\n");
            //sb.Append("					int nAns = 0;\n");
            //sb.Append("					nAns = int.Parse(strAns);\n");
            //sb.Append("					switch(nAns)\n");
            //sb.Append("					{\n");
            //sb.Append("						case 1:\n");
            //sb.Append("							// insert\n");
            //sb.Append("							Console.WriteLine(\"Save:  \");\n");
            //sb.Append("							Prompt();\n");
            //sb.Append("							Save(conn);\n");
            //sb.Append("							Console.WriteLine(ToString());\n");
            //sb.Append("							Console.WriteLine(\" \");\n");
            //sb.Append("							Console.WriteLine(\"Press ENTER to continue...\");\n");
            //sb.Append("							Console.ReadLine();\n");
            //sb.Append("							break;\n");
            //sb.Append("						case 2:\n");
            //sb.Append("							Console.WriteLine(\"Delete \" + TAG_ID + \":  \");\n");
            //sb.Append("							strAns = Console.ReadLine();\n");
            //sb.Append("							" + csn + "ID = long.Parse(strAns);\n");
            //sb.Append("							Delete(conn);\n");
            //sb.Append("							Console.WriteLine(\" \");\n");
            //sb.Append("							Console.WriteLine(\"Press ENTER to continue...\");\n");
            //sb.Append("							Console.ReadLine();\n");
            //sb.Append("							break;\n");
            //sb.Append("						case 3:\n");
            //sb.Append("							Console.WriteLine(\"Update:  \");\n");
            //sb.Append("							Prompt();\n");
            //sb.Append("							Update(conn);\n");
            //sb.Append("							Console.WriteLine(ToString());\n");
            //sb.Append("							Console.WriteLine(\" \");\n");
            //sb.Append("							Console.WriteLine(\"Press ENTER to continue...\");\n");
            //sb.Append("							Console.ReadLine();\n");
            //sb.Append("							break;\n");
            //sb.Append("						case 4:\n");
            //sb.Append("							Console.WriteLine(\"Exist \" + TAG_ID + \":  \");\n");
            //sb.Append("							strAns = Console.ReadLine();\n");
            //sb.Append("							" + csn + "ID = long.Parse(strAns);\n");
            //sb.Append("							bool bExist = false;\n");
            //sb.Append("							bExist = Exist(conn);\n");
            //sb.Append("							Console.WriteLine(\"Record id \" + " + csn + "ID + \" exist:  \" + bExist.ToString() );\n");
            //sb.Append("							Console.WriteLine(\" \");\n");
            //sb.Append("							Console.WriteLine(\"Press ENTER to continue...\");\n");
            //sb.Append("							Console.ReadLine();\n");
            //sb.Append("							break;\n");
            //sb.Append("						case 5:\n");
            //sb.Append("							Console.WriteLine(\"Load \" + TAG_ID + \":  \");\n");
            //sb.Append("							strAns = Console.ReadLine();\n");
            //sb.Append("							" + csn + "ID = long.Parse(strAns);\n");
            //sb.Append("							Load(conn);\n");
            //sb.Append("							Console.WriteLine(ToString());\n");
            //sb.Append("							Console.WriteLine(\" \");\n");
            //sb.Append("							Console.WriteLine(\"Press ENTER to continue...\");\n");
            //sb.Append("							Console.ReadLine();\n");
            //sb.Append("							break;\n");
            //sb.Append("						case 6:\n");
            //sb.Append("							Console.WriteLine(\"ToXml \" + TAG_ID + \":  \");\n");
            //sb.Append("							strAns = Console.ReadLine();\n");
            //sb.Append("							" + csn + "ID = long.Parse(strAns);\n");
            //sb.Append("							Load(conn);\n");
            //sb.Append("							Console.WriteLine(ToXml());\n");
            //sb.Append("							Console.WriteLine(\" \");\n");
            //sb.Append("							Console.WriteLine(\"Press ENTER to continue...\");\n");
            //sb.Append("							Console.ReadLine();\n");
            //sb.Append("							break;\n");
            //sb.Append("						default:\n");
            //sb.Append("							Console.WriteLine(\"Undefined option.\");\n");
            //sb.Append("							break;\n");
            //sb.Append("					}\n");
            //sb.Append("				}\n");
            //sb.Append("			}\n");
            //sb.Append("			catch (Exception e) \n");
            //sb.Append("			{\n");
            //sb.Append("				Console.WriteLine(e.ToString());\n");
            //sb.Append("				Console.WriteLine(e.StackTrace);\n");
            //sb.Append("				Console.ReadLine();\n");
            //sb.Append("			}\n");
            //sb.Append("\n");
            //sb.Append("		}		\n");
            sb.Append("	}\n");
            sb.Append("}\n");
            sb.Append("\n//END OF " + CamCase(csn) + " CLASS FILE");

            return (sb.ToString());
        }

        private string BuildUpdates(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("BEGIN");
            sb.Append("\n");
            sb.Append("	PRINT '<<<< Dropping Stored Procedure sp" + csn + "Update >>>>'");
            sb.Append("\n");
            sb.Append("	DROP PROCEDURE [sp" + csn + "Update]");
            sb.Append("\n");
            sb.Append("END");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("/*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		PROCEDURE NAME: sp" + csn + "Update");
            sb.Append("\n");
            sb.Append("**		Change History");
            sb.Append("\n");
            sb.Append("*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		Date:		Author:		Description:");
            sb.Append("\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created");
            sb.Append("\n");
            sb.Append("*******************************************************************************/");
            sb.Append("\n");
            sb.Append("CREATE PROCEDURE sp" + csn + "Update");
            sb.Append("\n");
            sb.Append("(");
            sb.Append("\n");

            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
                con2.Close();
                if (con2.State == ConnectionState.Closed) con2.Open();
                DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "21": // VARBINARY
                            //sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " VARBINARY(MAX)" + " = NULL,\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLTexts.Text + " = NULL,\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLTexts.Text + " = NULL,\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                                else
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "1").Replace("PREC", "0") + " = 0,\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", "0") + " = 0,\n");
                            }
                            else
                            {
                                // DECIMAL
                                string sc = dr["NumericScale"].ToString();
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", sc) + " = 0,\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            if (dr["ColumnName"].ToString() != "date_created")
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " SMALLDATETIME = NULL,\n");
                            break;

                        case "4": //DATETETIME
                            if (dr["ColumnName"].ToString() != "date_created")
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " DATETIME = NULL,\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " BINARY(10) = NULL,\n");
                            break;
                    }
                }

                sb.Append("\t@" + "PKID".PadRight(25) + " NUMERIC(10) OUTPUT");
                con2.Close();

                dt.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("\n");
            sb.Append(")");
            sb.Append("\n");
            sb.Append("AS");
            sb.Append("\n");
            sb.Append("SET NOCOUNT ON");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");

            sb.Append("   -- Update record wth NUMERIC(10) value\n\n");

            sb.Append("UPDATE [" + dbn + "] SET \n");

            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
                con2.Close();
                if (con2.State == ConnectionState.Closed) con2.Open();
                DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
                bool first;
                first = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (!first)
                        if (dr["ColumnName"].ToString() != "date_created")
                            sb.Append("\t[" + dr["ColumnName"].ToString() + "] = @" + CamCase(dr["ColumnName"].ToString()) + ",\n");
                    first = false;
                }

                sb.Remove(sb.ToString().Length - 2, 2);
                dt.Dispose();
            }
            catch { }

            sb.Append("\nWHERE " + dbn + "_id = @" + csn + "ID");
            sb.Append("\n\n");
            sb.Append("-- return ID for updated record\n");

            sb.Append("SELECT @PKID = @" + csn + "ID");

            sb.Append("\n");
            sb.Append("------------------------------------------------");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("PRINT '<<<< Created Stored Procedure sp" + csn + "Update >>>>'");
            sb.Append("\n");
            sb.Append("ELSE");
            sb.Append("\n");
            sb.Append("PRINT '<<< Failed Creating Stored Procedure sp" + csn + "Update >>>'");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");

            return (sb.ToString());
        }

        /// <summary>
        /// Builds the enum class.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildEnumClass(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);
            //bool Auto = false;

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
            //Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";

            sb.Append("using System;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Text;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Data;\n");
            sb.Append("\n");
            sb.Append("using Vetapp.Engine.Common;\n");
            sb.Append("using Vetapp.Engine.DataAccessLayer.Data;\n");
            sb.Append("\n");
            sb.Append("namespace Vetapp.Engine.DataAccessLayer.Enumeration\n");
            sb.Append("{\n");
            sb.Append("\n");
            sb.Append("	/// <summary>\n");
            sb.Append("	/// Copyright (c) " + DateTime.UtcNow.Year.ToString() + " Haytham Allos.  San Diego, California, USA\n");
            sb.Append("	/// All Rights Reserved\n");
            sb.Append("	/// \n");
            sb.Append("	/// File:  Enum" + csn + ".cs\n");
            sb.Append("	/// History\n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// 001	" + txtCB.Text + "	" + DateTime.UtcNow.ToShortDateString() + "	Created\n");
            sb.Append("	/// \n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// </summary>\n");
            sb.Append("	public class Enum" + csn + "\n");
            sb.Append("	{\n");
            sb.Append("		private bool _hasAny = false;\n");
            sb.Append("		private bool _hasMore = false;\n");
            sb.Append("		private bool _bSetup = false;\n");
            sb.Append("\n");
            sb.Append("		private SqlCommand _cmd = null;\n");
            sb.Append("		private SqlDataReader _rdr = null;\n");
            sb.Append("		private SqlConnection _conn = null;\n");
            sb.Append("		\n");
            //sb.Append("		private Config _config = null;\n");
            //sb.Append("		private Logger _oLog = null;\n");
            //sb.Append("		private string _strLognameText = \"DataAccessLayer-Enum-Enum" + csn + "\";\n");
            sb.Append("		private ErrorCode _errorCode = null;\n");
            sb.Append("		private bool _hasError = false;\n");
            sb.Append("		private int _nCount = 0;\n");
            sb.Append("\n\n");
            sb.Append("/*********************** CUSTOM NON-META BEGIN *********************/\n");
            sb.Append("\n");
            sb.Append("/*********************** CUSTOM NON-META END *********************/\n");
            sb.Append("\n\n");
            sb.Append("		/// <summary>Attribute of type string</summary>\n");
            sb.Append("		public static readonly string ENTITY_NAME = \"Enum" + csn + "\"; //Table name to abstract\n");
            sb.Append("		private static DateTime dtNull = new DateTime();\n");
            sb.Append("		private static readonly string PARAM_COUNT = \"@COUNT\"; //Sp count parameter\n");
            sb.Append("\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		private byte[] _byte" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                    // NUMERIC(10)
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                else
                                    // NUMERIC(10)
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		private " + _strBoolText + " _b" + CamCase(dr["ColumnName"].ToString()) + " = " + _strBoolDefaultValue + ";\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		private double _d" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("		private double _dBegin" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("		private double _dEnd" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("		private DateTime _dtBegin" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("		private DateTime _dtEnd" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("		private DateTime _dtBegin" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("		private DateTime _dtEnd" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		public object _o" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("//		private string _strOrderByEnum = \"ASC\";\n");
            sb.Append("		private string _strOrderByField = DB_FIELD_ID;\n");
            sb.Append("\n");
            sb.Append("		/// <summary>DB_FIELD_ID Attribute type string</summary>\n");
            sb.Append("		public static readonly string DB_FIELD_ID = \"" + dbn + "_id\"; //Table id field name\n");

            foreach (DataRow dr in dt.Rows)
            //if (dr["ColumnName"].ToString() != dbn + "_id")
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }

                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                if (dr["ProviderType"].ToString() == "4"
                    || dr["ProviderType"].ToString() == "15")
                {
                    sb.Append("		public static readonly string TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " = \"Begin" + CamCase(dr["ColumnName"].ToString()) + "\"; //Attribute " + CamCase(dr["ColumnName"].ToString()) + "  name\n");
                    sb.Append("		/// <summary>End" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                    sb.Append("		public static readonly string TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " = \"End" + CamCase(dr["ColumnName"].ToString()) + "\"; //Attribute " + CamCase(dr["ColumnName"].ToString()) + "  name\n");
                }
                else
                {
                    sb.Append("		public static readonly string TAG_" + dr["ColumnName"].ToString().ToUpper() + " = \"" + CamCase(dr["ColumnName"].ToString()) + "\"; //Attribute " + CamCase(dr["ColumnName"].ToString()) + "  name\n");
                }

                if (dr["ProviderType"].ToString() == "5")
                {
                    if (dr["NumericScale"].ToString() == "6")
                    {
                        sb.Append("		public static readonly string TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " = \"Begin" + CamCase(dr["ColumnName"].ToString()) + "\"; //Attribute " + CamCase(dr["ColumnName"].ToString()) + "  name\n");
                        sb.Append("		/// <summary>End" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                        sb.Append("		public static readonly string TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " = \"End" + CamCase(dr["ColumnName"].ToString()) + "\"; //Attribute " + CamCase(dr["ColumnName"].ToString()) + "  name\n");
                    }
                }
            }

            sb.Append("		// Stored procedure name\n");
            sb.Append("		public string SP_ENUM_NAME = \"sp" + csn + "Enum\"; //Enum sp name\n");
            sb.Append("\n");

            sb.Append("		/// <summary>HasError is a Property in the " + csn + " Class of type bool</summary>\n");
            sb.Append("		public bool HasError \n");
            sb.Append("		{\n");
            sb.Append("			get{return _hasError;}\n");
            sb.Append("			set{_hasError = value;}\n");
            sb.Append("		}\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type byte[]</summary>\n");
                            sb.Append("		public byte[] " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _byte" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_byte" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type bool</summary>\n");
                                sb.Append("		public " + _strBoolText + " " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _b" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_b" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type double</summary>\n");
                                sb.Append("		public double " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _d" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_d" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");

                                sb.Append("		/// <summary>Property " + CamCase(dr["ColumnName"].ToString()) + ". Type: double</summary>\n");
                                sb.Append("		public double Begin" + CamCase(dr["ColumnName"].ToString()) + "\n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _dBegin" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_dBegin" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                                sb.Append("		/// <summary>Property " + CamCase(dr["ColumnName"].ToString()) + ". Type: double</summary>\n");
                                sb.Append("		public double End" + CamCase(dr["ColumnName"].ToString()) + "\n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _dEnd" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_dEnd" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("		/// <summary>Property " + CamCase(dr["ColumnName"].ToString()) + ". Type: DateTime</summary>\n");
                            sb.Append("		public DateTime Begin" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dtBegin" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dtBegin" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            sb.Append("		/// <summary>Property " + CamCase(dr["ColumnName"].ToString()) + ". Type: DateTime</summary>\n");
                            sb.Append("		public DateTime End" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dtEnd" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dtEnd" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("		/// <summary>Property " + CamCase(dr["ColumnName"].ToString()) + ". Type: DateTime</summary>\n");
                            sb.Append("		public DateTime Begin" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dtBegin" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dtBegin" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            sb.Append("		/// <summary>Property " + CamCase(dr["ColumnName"].ToString()) + ". Type: DateTime</summary>\n");
                            sb.Append("		public DateTime End" + CamCase(dr["ColumnName"].ToString()) + "\n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dtEnd" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dtEnd" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("\n");
            sb.Append("		/// <summary>Count Property. Type: int</summary>\n");
            sb.Append("		public int Count \n");
            sb.Append("		{\n");
            sb.Append("			get\n");
            sb.Append("			{\n");
            sb.Append("				_bSetup = true;\n");
            //sb.Append("				_log(\"ENUM COUNT\", \"Calling sp \" + SP_ENUM_NAME);\n");
            sb.Append("				// if necessary, close the old reader\n");
            sb.Append("				if ( (_cmd != null) || (_rdr != null) )\n");
            sb.Append("				{\n");
            sb.Append("					Close();\n");
            sb.Append("				}\n");
            sb.Append("				_cmd = new SqlCommand(SP_ENUM_NAME, _conn);\n");
            sb.Append("				_cmd.CommandType = CommandType.StoredProcedure;\n");
            sb.Append("				_setupEnumParams();\n");
            sb.Append("				_setupCountParams();\n");
            sb.Append("				_cmd.Connection = _conn;\n");
            sb.Append("				_cmd.ExecuteNonQuery();\n");
            sb.Append("				try\n");
            sb.Append("				{\n");
            sb.Append("					string strTmp;\n");
            sb.Append("					strTmp = _cmd.Parameters[PARAM_COUNT].Value.ToString();\n");
            sb.Append("					_nCount = int.Parse(strTmp);\n");
            sb.Append("				}\n");
            sb.Append("				catch \n");
            sb.Append("				{\n");
            sb.Append("					_nCount = 0;\n");
            sb.Append("				}\n");
            sb.Append("				return _nCount;			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>\n");
            sb.Append("		public Enum" + csn + "()\n");
            sb.Append("		{\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>\n");
            sb.Append("		public Enum" + csn + "(SqlConnection conn)\n");
            sb.Append("		{\n");
            sb.Append("			_conn = conn;\n");
            sb.Append("		}\n");
            sb.Append("\n");
            //sb.Append("		/// <summary>Constructor takes 2 parameters: SqlConnection and Config</summary>\n");
            //sb.Append("		public Enum" + csn + "(SqlConnection conn)\n");
            //sb.Append("		{\n");
            //sb.Append("			_conn = conn;\n");
            ////sb.Append("			_config = pConfig;\n");
            ////sb.Append("			_oLog = new Logger(_strLognameText);\n");
            //sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		// Implementation of IEnumerator\n");
            sb.Append("		/// <summary>Property of type " + csn + ". Returns the next " + csn + " in the list</summary>\n");
            sb.Append("		private " + NS(csn) + " _nextTransaction\n");
            sb.Append("		{\n");
            sb.Append("			get\n");
            sb.Append("			{\n");
            sb.Append("				" + NS(csn) + " o = null;\n");
            sb.Append("				\n");
            sb.Append("				if (!_bSetup)\n");
            sb.Append("				{\n");
            sb.Append("					EnumData();\n");
            sb.Append("				}\n");
            sb.Append("				if (_hasMore)\n");
            sb.Append("				{\n");
            sb.Append("					o = new " + NS(csn) + "(_rdr);\n");
            sb.Append("					_hasMore = _rdr.Read();\n");
            sb.Append("					if (!_hasMore)\n");
            sb.Append("					{\n");
            sb.Append("						Close();\n");
            sb.Append("					}\n");
            sb.Append("				}\n");
            sb.Append("				return o;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>Enumerates the Data</summary>\n");
            sb.Append("		public void EnumData()\n");
            sb.Append("		{\n");
            sb.Append("			if (!_bSetup)\n");
            sb.Append("			{\n");
            sb.Append("				_bSetup = true;\n");
            //sb.Append("				_log(\"ENUM\", \"Calling sp \" + SP_ENUM_NAME);\n");
            sb.Append("				// if necessary, close the old reader\n");
            sb.Append("				if ( (_cmd != null) || (_rdr != null) )\n");
            sb.Append("				{\n");
            sb.Append("					Close();\n");
            sb.Append("				}\n");
            sb.Append("				_cmd = new SqlCommand(SP_ENUM_NAME, _conn);\n");
            sb.Append("				_cmd.CommandType = CommandType.StoredProcedure;\n");
            sb.Append("				_setupEnumParams();\n");
            sb.Append("				_cmd.Connection = _conn;\n");
            sb.Append("				_rdr = _cmd.ExecuteReader();\n");
            sb.Append("				_hasAny = _rdr.Read();\n");
            sb.Append("				_hasMore = _hasAny;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("		/// <summary>returns the next element in the enumeration</summary>\n");
            sb.Append("		public object nextElement()\n");
            sb.Append("		{\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				return _nextTransaction;\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("				return null;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>Returns whether or not more elements exist</summary>\n");
            sb.Append("		public bool hasMoreElements()\n");
            sb.Append("		{\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				if (_bSetup)\n");
            sb.Append("				{\n");
            sb.Append("					EnumData();\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("			return _hasMore;\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>Closes the datareader</summary>\n");
            sb.Append("		public void Close()\n");
            sb.Append("		{\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				if ( _rdr != null )\n");
            sb.Append("				{\n");
            sb.Append("					_rdr.Dispose();\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("			_rdr = null;\n");
            sb.Append("			_cmd = null;\n");
            sb.Append("		}\n");

            sb.Append("\n");

            sb.Append("		/// <summary>ToString is overridden to display all properties of the " + csn + " Class</summary>\n");
            sb.Append("		public override string ToString() \n");
            sb.Append("		{\n");
            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");
            sb.Append("			sbReturn.Append(TAG_" + dbn.ToUpper() + "_ID + \":  \" + " + csn + "ID.ToString() + \"\\n\");\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(Begin" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + Begin" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \":\\n\");\n");
                        sb.Append("			}\n");

                        sb.Append("			if (!dtNull.Equals(End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + End" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \":\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
            }
            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");
            sb.Append("		}\n");

            sb.Append("		/// <summary>Creates well formatted XML - includes all properties of " + csn + "</summary>\n");
            sb.Append("		public string ToXml() \n");
            sb.Append("		{\n");
            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");
            sb.Append("			sbReturn.Append(\"<\" + ENTITY_NAME + \">\\n\");\n");
            sb.Append("			sbReturn.Append(\"<\" + TAG_" + dbn.ToUpper() + "_ID + \">\" + " + csn + "ID + \"</\" + TAG_" + dbn.ToUpper() + "_ID + \">\\n\");\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(Begin" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + Begin" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"</\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \"></\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");

                        sb.Append("			if (!dtNull.Equals(End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + End" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"</\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \"></\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"</\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
            }

            sb.Append("			sbReturn.Append(\"</\" + ENTITY_NAME + \">\" + \"\\n\");\n");
            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");
            sb.Append("		}\n");

            sb.Append("		/// <summary>Parse XML string and assign values to object</summary>\n");
            sb.Append("		public void Parse(string pStrXml)\n");
            sb.Append("		{\n");

            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				XmlDocument xmlDoc = null;\n");
            sb.Append("				string strXPath = null;\n");
            sb.Append("				XmlNodeList xNodes = null;\n");
            sb.Append("\n");
            sb.Append("				xmlDoc = new XmlDocument();\n");
            sb.Append("				xmlDoc.LoadXml(pStrXml);\n");
            sb.Append("\n");
            sb.Append("				// get the element\n");
            sb.Append("				strXPath = \"//\" + ENTITY_NAME;\n");
            sb.Append("				xNodes = xmlDoc.SelectNodes(strXPath);\n");
            sb.Append("				if ( xNodes.Count > 0 )\n");
            sb.Append("				{\n");
            sb.Append("					Parse(xNodes.Item(0));\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch \n");
            sb.Append("			{\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("		}		\n");

            sb.Append("		/// <summary>Parse accepts an XmlNode and parses values</summary>\n");
            sb.Append("		public void Parse(XmlNode xNode)\n");
            sb.Append("		{\n");

            sb.Append("			XmlNode xResultNode = null;\n");
            sb.Append("			string strTmp = null;\n");
            sb.Append("\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dbn.ToUpper() + "_ID);\n");
            sb.Append("				strTmp = xResultNode.InnerText;\n");
            sb.Append("				" + csn + "ID = (long) Convert.ToInt32(strTmp);\n");
            sb.Append("			}\n");
            sb.Append("			catch  \n");
            sb.Append("			{\n");
            sb.Append("			}\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "21": // VARBINARY
                            sb.Append("          // Cannot reliably convert a byte[] to a string.\n");
                            break;

                        case "22": // VARCHAR
                        case "12": // NVARCHAR
                        case "18": // TEXT
                        case "11": // NTEXT
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("				if (" + CamCase(dr["ColumnName"].ToString()) + ".Trim().Length == 0)\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                    break;
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = false;\n");
                                sb.Append("			}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                                break;
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                            }
                            break;

                        case "4": //DATETETIME
                        case "15": //SMALLDATETIME
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				Begin" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");

                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				End" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("		}\n");

            sb.Append("		/// <summary>Prompt for values</summary>\n");
            sb.Append("		public void Prompt()\n");
            sb.Append("		{\n");
            sb.Append("			try \n");
            sb.Append("			{\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR

                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            sb.Append("				if (" + CamCase(dr["ColumnName"].ToString()) + ".Length == 0)\n");
                            sb.Append("				{\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("				}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            sb.Append("				if (" + CamCase(dr["ColumnName"].ToString()) + ".Length == 0)\n");
                            sb.Append("				{\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("				}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("              // Cannot reliably convert a byte[] to string.\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            sb.Append("				if (" + CamCase(dr["ColumnName"].ToString()) + ".Length == 0)\n");
                            sb.Append("				{\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("				}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            sb.Append("				if (" + CamCase(dr["ColumnName"].ToString()) + ".Length == 0)\n");
                            sb.Append("				{\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("				}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                    sb.Append("				try\n");
                                    sb.Append("				{\n");
                                    sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
                                    sb.Append("				}\n");
                                    sb.Append("				catch \n");
                                    sb.Append("				{\n");
                                    sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("				}\n");
                                    sb.Append("\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                    sb.Append("				try\n");
                                    sb.Append("				{\n");
                                    sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
                                    sb.Append("				}\n");
                                    sb.Append("				catch \n");
                                    sb.Append("				{\n");
                                    sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("				}\n");
                                    sb.Append("\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                sb.Append("				try\n");
                                sb.Append("				{\n");
                                sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(Console.ReadLine());\n");
                                sb.Append("				}\n");
                                sb.Append("				catch \n");
                                sb.Append("				{\n");
                                sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = false;\n");
                                sb.Append("				}\n");
                                sb.Append("\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                sb.Append("				try\n");
                                sb.Append("				{\n");
                                sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = (long)Convert.ToInt32(Console.ReadLine());\n");
                                sb.Append("				}\n");
                                sb.Append("				catch \n");
                                sb.Append("				{\n");
                                sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("				}\n");
                                sb.Append("\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                                sb.Append("				try\n");
                                sb.Append("				{\n");
                                sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(Console.ReadLine());\n");
                                sb.Append("				}\n");
                                sb.Append("				catch \n");
                                sb.Append("				{\n");
                                sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("				}\n");
                                sb.Append("\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("				Console.WriteLine(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				try\n");
                            sb.Append("				{\n");
                            sb.Append("					string s = Console.ReadLine();\n");
                            sb.Append("					Begin" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(s);\n");
                            sb.Append("				}\n");
                            sb.Append("				catch \n");
                            sb.Append("				{\n");
                            sb.Append("					Begin" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("				}\n");
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				try\n");
                            sb.Append("				{\n");
                            sb.Append("					string s = Console.ReadLine();\n");
                            sb.Append("					End" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(s);\n");
                            sb.Append("				}\n");
                            sb.Append("				catch \n");
                            sb.Append("				{\n");
                            sb.Append("					End" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("				}\n");
                            sb.Append("\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("				Console.WriteLine(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				try\n");
                            sb.Append("				{\n");
                            sb.Append("					string s = Console.ReadLine();\n");
                            sb.Append("					Begin" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(s);\n");
                            sb.Append("				}\n");
                            sb.Append("				catch \n");
                            sb.Append("				{\n");
                            sb.Append("					Begin" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("				}\n");
                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				try\n");
                            sb.Append("				{\n");
                            sb.Append("					string s = Console.ReadLine();\n");
                            sb.Append("					End" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(s);\n");
                            sb.Append("				}\n");
                            sb.Append("				catch  \n");
                            sb.Append("				{\n");
                            sb.Append("					End" + CamCase(dr["ColumnName"].ToString()) + " = new DateTime();\n");
                            sb.Append("				}\n");
                            sb.Append("\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");

                            sb.Append("\n");
                            sb.Append("				Console.WriteLine(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Console.ReadLine();\n");
                            sb.Append("				if (" + CamCase(dr["ColumnName"].ToString()) + ".Length == 0)\n");
                            sb.Append("				{\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("				}\n");
                            sb.Append("\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sb.Append("\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e) \n");
            sb.Append("			{\n");
            //sb.Append("				 _log(\"ERROR\", e.ToString() + e.StackTrace.ToString());\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///     Dispose of this object's resources.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public void Dispose()\n");
            sb.Append("		{\n");
            sb.Append("			Dispose(true);\n");
            sb.Append("			GC.SuppressFinalize(true); // as a service to those who might inherit from us\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///		Free the instance variables of this object.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		protected virtual void Dispose(bool disposing)\n");
            sb.Append("		{\n");
            sb.Append("			if (! disposing)\n");
            sb.Append("				return; // we're being collected, so let the GC take care of this object\n");
            sb.Append("		}\n");
            sb.Append("		private void _setupCountParams()\n");
            sb.Append("		{\n");
            sb.Append("			SqlParameter paramCount = null;\n");
            sb.Append("			paramCount = new SqlParameter();\n");
            sb.Append("			paramCount.ParameterName = PARAM_COUNT;\n");
            sb.Append("			paramCount.DbType = DbType.Int32;\n");
            sb.Append("			paramCount.Direction = ParameterDirection.Output;\n");
            sb.Append("\n");
            sb.Append("			_cmd.Parameters.Add(paramCount);\n");
            sb.Append("		}\n");
            sb.Append("		private void _setupEnumParams()\n");
            sb.Append("		{\n");
            sb.Append("			System.Text.StringBuilder sbLog = null;\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }

                if (dr["ProviderType"].ToString() == "4"
                    || dr["ProviderType"].ToString() == "15")
                {
                    sb.Append("			SqlParameter paramBegin" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                    sb.Append("			SqlParameter paramEnd" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                }
                else
                {
                    sb.Append("			SqlParameter param" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                }

                if (dr["ProviderType"].ToString() == "5")
                {
                    if (dr["NumericScale"].ToString() == "6")
                    {
                        sb.Append("			SqlParameter paramBegin" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                        sb.Append("			SqlParameter paramEnd" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                    }
                }
            }

            sb.Append("			DateTime dtNull = new DateTime();\n");
            sb.Append("\n");
            sb.Append("			sbLog = new System.Text.StringBuilder();\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " text param\n");
                            sb.Append("			if ( " + CamCase(dr["ColumnName"].ToString()) + " != null )\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                            sb.Append("			}\n");
                            sb.Append("			else\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			}\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " text param\n");
                            sb.Append("			if ( " + CamCase(dr["ColumnName"].ToString()) + " != null )\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                            sb.Append("			}\n");
                            sb.Append("			else\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			}\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " text param\n");
                            sb.Append("			//if ( " + CamCase(dr["ColumnName"].ToString()) + " != null )\n");
                            sb.Append("			//{\n");
                            sb.Append("			//	param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("			//	sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                            sb.Append("			//}\n");
                            sb.Append("			//else\n");
                            sb.Append("			//{\n");
                            sb.Append("			//	param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			//}\n");
                            sb.Append("			//param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			//_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            break;

                        case "18": // TEXT
                            sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " text param\n");
                            sb.Append("			if ( " + CamCase(dr["ColumnName"].ToString()) + " != null )\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                            sb.Append("			}\n");
                            sb.Append("			else\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			}\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " text param\n");
                            sb.Append("			if ( " + CamCase(dr["ColumnName"].ToString()) + " != null )\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                            sb.Append("			}\n");
                            sb.Append("			else\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			}\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                    sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                                    sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                                    sb.Append("				_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                    sb.Append("\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                    sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                                    sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                                    sb.Append("				_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                                sb.Append("				_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                                sb.Append("				_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
                                sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                                sb.Append("				_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            }

                            if (dr["NumericScale"].ToString() == "6")
                            {
                                sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " param\n");
                                sb.Append("			if (Begin" + CamCase(dr["ColumnName"].ToString()) + " != 0)\n");
                                sb.Append("			{\n");
                                sb.Append("				paramBegin" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + ", Begin" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("				sbLog.Append(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + Begin" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("			}\n");
                                sb.Append("			else\n");
                                sb.Append("			{\n");
                                sb.Append("				paramBegin" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + ", 0);\n");
                                sb.Append("			}\n");
                                sb.Append("			paramBegin" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                                sb.Append("			_cmd.Parameters.Add(paramBegin" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("\n");
                                sb.Append("			if (End" + CamCase(dr["ColumnName"].ToString()) + " != 0)\n");
                                sb.Append("			{\n");
                                sb.Append("				paramEnd" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + ", End" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("				sbLog.Append(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + End" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("			}\n");
                                sb.Append("			else\n");
                                sb.Append("			{\n");
                                sb.Append("				paramEnd" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + ", 0);\n");
                                sb.Append("			}\n");
                                sb.Append("			paramEnd" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                                sb.Append("			_cmd.Parameters.Add(paramEnd" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                                sb.Append("\n");
                            }
                            break;

                        case "4": //DATETETIME
                        case "15": //SMALLDATETIME
                            sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " param\n");
                            sb.Append("			if (!dtNull.Equals(Begin" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            sb.Append("			{\n");
                            sb.Append("				paramBegin" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + ", Begin" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            //sb.Append("				sbLog.Append(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + Begin" + CamCase(dr["ColumnName"].ToString()) + ".ToLongDateString());\n");
                            sb.Append("			}\n");
                            sb.Append("			else\n");
                            sb.Append("			{\n");
                            sb.Append("				paramBegin" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			}\n");
                            sb.Append("			paramBegin" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			_cmd.Parameters.Add(paramBegin" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            sb.Append("			if (!dtNull.Equals(End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                            sb.Append("			{\n");
                            sb.Append("				paramEnd" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + ", End" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            //sb.Append("				sbLog.Append(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + End" + CamCase(dr["ColumnName"].ToString()) + ".ToLongDateString());\n");
                            sb.Append("			}\n");
                            sb.Append("			else\n");
                            sb.Append("			{\n");
                            sb.Append("				paramEnd" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			}\n");
                            sb.Append("			paramEnd" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			_cmd.Parameters.Add(paramEnd" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("			// Setup the " + dr["ColumnName"].ToString().Replace("_", " ") + " text param\n");
                            sb.Append("			if ( " + CamCase(dr["ColumnName"].ToString()) + " != null )\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("				sbLog.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"=\" + " + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("			}\n");
                            sb.Append("			else\n");
                            sb.Append("			{\n");
                            sb.Append("				param" + CamCase(dr["ColumnName"].ToString()) + " = new SqlParameter(\"@\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + ", DBNull.Value);\n");
                            sb.Append("			}\n");
                            sb.Append("			param" + CamCase(dr["ColumnName"].ToString()) + ".Direction = ParameterDirection.Input;\n");
                            sb.Append("			_cmd.Parameters.Add(param" + CamCase(dr["ColumnName"].ToString()) + ");\n");
                            sb.Append("\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //sb.Append("				_log(\"ENUM PARAMS\", sbLog.ToString());\n");

            //sb.Append("			sbLog = null;\n");
            sb.Append("		}\n");
            sb.Append("\n");
            //sb.Append("		//private\n");
            //sb.Append("		private void _log(string pStrAction, string pStrMsgText) \n");
            //sb.Append("		{\n");
            //sb.Append("			if (_config != null )\n");
            //sb.Append("			{\n");
            //sb.Append("				if (_config.DoLogInfo)\n");
            //sb.Append("				{\n");
            //sb.Append("						_oLog.Log(pStrAction, pStrMsgText);\n");
            //sb.Append("				}\n");
            //sb.Append("			}\n");
            //sb.Append("\n");
            //sb.Append("		}\n");
            sb.Append("	}\n");
            sb.Append("}\n");

            return sb.ToString();
        }

        //		private string BuildStaticEnum(string TableName)
        //		{
        //			System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //
        //			string dbn = TableName;
        //			string csn = CamCase(TableName);
        //
        //			sb.Append("using System;\n");
        //			sb.Append("using System.Data.SqlClient;\n");
        //			sb.Append("using System.Data;\n");
        //			sb.Append("\n");
        //			sb.Append("using Mdn.Common;\n");
        //			sb.Append("using Mdn.DataAccessLayer.Data;\n");
        //			sb.Append("\n");
        //			sb.Append("namespace Mdn.DataAccessLayer.Enumeration\n");
        //			sb.Append("{\n");
        //			sb.Append("	/// <summary>\n");
        //			sb.Append("	/// Enumerates " + csn + " data.\n");
        //			sb.Append("	/// </summary>\n");
        //			sb.Append("	public class Enum" + csn + "\n");
        //			sb.Append("	{\n");
        //			sb.Append("		private bool _hasAny = false;\n");
        //			sb.Append("		private bool _hasMore = false;\n");
        //			sb.Append("		private bool _bSetup = false;\n");
        //			sb.Append("\n");
        //			sb.Append("		private SqlCommand _cmd = null;\n");
        //			sb.Append("		private SqlDataReader _rdr = null;\n");
        //			sb.Append("		private SqlConnection _conn = null;\n");
        //			sb.Append("		\n");
        //			sb.Append("		private Config _config = null;\n");
        //			sb.Append("		private Logger _oLogText = null;\n");
        //			sb.Append("		private Logger _oLogXml = null;\n");
        //			sb.Append("		private string _strLognameText = \"DataAccessLayer-Enum-Enum" + csn + ".txt\";\n");
        //			sb.Append("		private string _strLognameXml = \"DataAccessLayer-Enum-Enum" + csn + ".xml\";\n");
        //			sb.Append("		private ErrorCode _errorCode = null;\n");
        //			sb.Append("		private bool _hasError = false;\n");
        //			sb.Append("		private int _nCount = 0;\n");
        //			sb.Append("\n");
        //			sb.Append("		private static readonly string PARAM_COUNT = \"@COUNT\"; //Sp count parameter\n");
        //			sb.Append("\n");
        //			sb.Append("		// Stored procedure name\n");
        //			sb.Append("		private static readonly string SP_ENUM_NAME = \"sp" + csn + "Enum\"; //Enum sp name\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Counts number of rows in " + csn + " enumeration.</summary>\n");
        //			sb.Append("		public int Count \n");
        //			sb.Append("		{\n");
        //			sb.Append("			get\n");
        //			sb.Append("			{\n");
        //			sb.Append("				_bSetup = true;\n");
        //			sb.Append("				_log(\"ENUM COUNT\", \"Calling sp \" + SP_ENUM_NAME);\n");
        //			sb.Append("				// if necessary, close the old reader\n");
        //			sb.Append("				if ( (_cmd != null) || (_rdr != null) )\n");
        //			sb.Append("				{\n");
        //			sb.Append("					Close();\n");
        //			sb.Append("				}\n");
        //			sb.Append("				SqlCommand cmd = null;\n");
        //			sb.Append("				cmd = new SqlCommand(SP_ENUM_NAME, _conn);\n");
        //			sb.Append("				cmd.CommandType = CommandType.StoredProcedure;\n");
        //			sb.Append("				cmd.Connection = _conn;\n");
        //			sb.Append("				_setupCountParams(cmd);\n");
        //			sb.Append("				cmd.ExecuteNonQuery();\n");
        //			sb.Append("				try\n");
        //			sb.Append("				{\n");
        //			sb.Append("					string strTmp;\n");
        //			sb.Append("					strTmp = cmd.Parameters[PARAM_COUNT].Value.ToString();\n");
        //			sb.Append("					_nCount = int.Parse(strTmp);\n");
        //			sb.Append("				}\n");
        //			sb.Append("				catch \n");
        //			sb.Append("				{\n");
        //			sb.Append("					_nCount = 0;\n");
        //			sb.Append("				}\n");
        //			sb.Append("				return _nCount;\n");
        //			sb.Append("			}\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Constructor takes SqlConnection object</summary>\n");
        //			sb.Append("		public Enum" + csn + "(SqlConnection conn)\n");
        //			sb.Append("		{\n");
        //			sb.Append("			_conn = conn;\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Constructor takes SqlConnection object and Config object</summary>\n");
        //			sb.Append("		public Enum" + csn + "(SqlConnection conn, Config pConfig)\n");
        //			sb.Append("		{\n");
        //			sb.Append("			_conn = conn;\n");
        //			sb.Append("			_config = pConfig;\n");
        //			sb.Append("			_oLogText = new Logger(pConfig.LogDir + System.IO.Path.DirectorySeparatorChar.ToString() + _strLognameText);\n");
        //			sb.Append("			_oLogXml = new Logger(pConfig.LogDir + System.IO.Path.DirectorySeparatorChar.ToString() + _strLognameXml);\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Implementation of IEnumerator</summary>\n");
        //			sb.Append("		private " + csn + " _nextTransaction\n");
        //			sb.Append("		{\n");
        //			sb.Append("			get\n");
        //			sb.Append("			{\n");
        //			sb.Append("				" + csn + " o = null;\n");
        //			sb.Append("				\n");
        //			sb.Append("				if (!_bSetup)\n");
        //			sb.Append("				{\n");
        //			sb.Append("					EnumData();\n");
        //			sb.Append("				}\n");
        //			sb.Append("				if (_hasMore)\n");
        //			sb.Append("				{\n");
        //			sb.Append("					o = new " + csn + "(_rdr);\n");
        //			sb.Append("					_hasMore = _rdr.Read();\n");
        //			sb.Append("					if (!_hasMore)\n");
        //			sb.Append("					{\n");
        //			sb.Append("						Close();\n");
        //			sb.Append("					}\n");
        //			sb.Append("				}\n");
        //			sb.Append("				return o;\n");
        //			sb.Append("			}\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Enumerate Data</summary>\n");
        //			sb.Append("		public void EnumData()\n");
        //			sb.Append("		{\n");
        //			sb.Append("			if (!_bSetup)\n");
        //			sb.Append("			{\n");
        //			sb.Append("				_bSetup = true;\n");
        //			sb.Append("				_log(\"ENUM\", \"Calling sp \" + SP_ENUM_NAME);\n");
        //			sb.Append("				// if necessary, close the old reader\n");
        //			sb.Append("				if ( (_cmd != null) || (_rdr != null) )\n");
        //			sb.Append("				{\n");
        //			sb.Append("					Close();\n");
        //			sb.Append("				}\n");
        //			sb.Append("				_cmd = new SqlCommand(SP_ENUM_NAME, _conn);\n");
        //			sb.Append("				_cmd.CommandType = CommandType.StoredProcedure;\n");
        //			sb.Append("				_cmd.Connection = _conn;\n");
        //			sb.Append("				_setupEnumParams();\n");
        //			sb.Append("				_rdr = _cmd.ExecuteReader();\n");
        //			sb.Append("				_hasAny = _rdr.Read();\n");
        //			sb.Append("				_hasMore = _hasAny;\n");
        //			sb.Append("			}\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Gets next element in Enumeration</summary>\n");
        //			sb.Append("		public object nextElement()\n");
        //			sb.Append("		{\n");
        //			sb.Append("			try\n");
        //			sb.Append("			{\n");
        //			sb.Append("				return _nextTransaction;\n");
        //			sb.Append("			}\n");
        //			sb.Append("			catch (Exception e) \n");
        //			sb.Append("			{\n");
        //			sb.Append("				_log(\"ERROR\", e.ToString(), \"<Exception>\" + e.Message.ToString() + \"</Exception>\");\n");
        //			sb.Append("				_log(\"ERROR\", e.StackTrace.ToString(), \"<Exception>\" + e.Message.ToString() + \"</Exception>\");\n");
        //			sb.Append("				_hasError = true;\n");
        //			sb.Append("				_errorCode = new ErrorCode();\n");
        //			sb.Append("				return null;\n");
        //			sb.Append("			}\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Returns boolean value indicating whether or not there are more elements</summary>\n");
        //			sb.Append("		public bool hasMoreElements()\n");
        //			sb.Append("		{\n");
        //			sb.Append("			try\n");
        //			sb.Append("			{\n");
        //			sb.Append("				if (_bSetup)\n");
        //			sb.Append("				{\n");
        //			sb.Append("					EnumData();\n");
        //			sb.Append("				}\n");
        //			sb.Append("			}\n");
        //			sb.Append("			catch (Exception e) \n");
        //			sb.Append("			{\n");
        //			sb.Append("				_log(\"ERROR\", e.ToString(), \"<Exception>\" + e.Message.ToString() + \"</Exception>\");\n");
        //			sb.Append("				_log(\"ERROR\", e.StackTrace.ToString(), \"<Exception>\" + e.Message.ToString() + \"</Exception>\");\n");
        //			sb.Append("				_hasError = true;\n");
        //			sb.Append("				_errorCode = new ErrorCode();\n");
        //			sb.Append("			}\n");
        //			sb.Append("\n");
        //			sb.Append("			return _hasMore;\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>Closes enumeration</summary>\n");
        //			sb.Append("		public void Close()\n");
        //			sb.Append("		{\n");
        //			sb.Append("			try\n");
        //			sb.Append("			{\n");
        //			sb.Append("				if ( _rdr != null )\n");
        //			sb.Append("				{\n");
        //			sb.Append("					_rdr.Dispose();\n");
        //			sb.Append("				}\n");
        //			sb.Append("			}\n");
        //			sb.Append("			catch (Exception e) \n");
        //			sb.Append("			{\n");
        //			sb.Append("				_log(\"ERROR\", e.ToString(), \"<Exception>\" + e.Message.ToString() + \"</Exception>\");\n");
        //			sb.Append("				_log(\"ERROR\", e.StackTrace.ToString(), \"<Exception>\" + e.Message.ToString() + \"</Exception>\");\n");
        //			sb.Append("				_hasError = true;\n");
        //			sb.Append("				_errorCode = new ErrorCode();\n");
        //			sb.Append("			}\n");
        //			sb.Append("			_rdr = null;\n");
        //			sb.Append("			_cmd = null;\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		/// <summary>\n");
        //			sb.Append("		///     Dispose of this object's resources.\n");
        //			sb.Append("		/// </summary>\n");
        //			sb.Append("		public void Dispose()\n");
        //			sb.Append("		{\n");
        //			sb.Append("			Dispose(true);\n");
        //			sb.Append("			GC.SuppressFinalize(true); // as a service to those who might inherit from us\n");
        //			sb.Append("		}\n");
        //			sb.Append("		/// <summary>\n");
        //			sb.Append("		///		Free the instance variables of this object.\n");
        //			sb.Append("		/// </summary>\n");
        //			sb.Append("		protected virtual void Dispose(bool disposing)\n");
        //			sb.Append("		{\n");
        //			sb.Append("			if (! disposing)\n");
        //			sb.Append("				return; // we're being collected, so let the GC take care of this object\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("		private void _setupCountParams(SqlCommand pCmd)\n");
        //			sb.Append("		{\n");
        //			sb.Append("			SqlParameter paramCount = null;\n");
        //			sb.Append("			paramCount = new SqlParameter();\n");
        //			sb.Append("			paramCount.ParameterName = PARAM_COUNT;\n");
        //			sb.Append("			paramCount.DbType = DbType.Int32;\n");
        //			sb.Append("			paramCount.Direction = ParameterDirection.Output;\n");
        //			sb.Append("\n");
        //			sb.Append("			pCmd.Parameters.Add(paramCount);\n");
        //			sb.Append("		}\n");
        //			sb.Append("		/// <summary>Set up enumeration parameters</summary>\n");
        //			sb.Append("		private void _setupEnumParams()\n");
        //			sb.Append("		{\n");
        //			sb.Append("			// use _cmd object to insert stored procedures\n");
        //			sb.Append("			// paramaters\n");
        //			sb.Append("		}\n");
        //			sb.Append("		//private\n");
        //			sb.Append("		/// <summary>Log Error Information</summary>\n");
        //			sb.Append("		private void _log(string pStrAction, string pStrMsgText, string pStrMsgXml) \n");
        //			sb.Append("		{\n");
        //			sb.Append("			if (_config != null )\n");
        //			sb.Append("			{\n");
        //			sb.Append("				if (_config.DoLogError)\n");
        //			sb.Append("				{\n");
        //			sb.Append("					if (_oLogText != null )\n");
        //			sb.Append("					{\n");
        //			sb.Append("						_oLogText.Log(pStrAction, pStrMsgText);\n");
        //			sb.Append("					}\n");
        //			sb.Append("					if (_oLogXml != null )\n");
        //			sb.Append("					{\n");
        //			sb.Append("						_oLogXml.Log(pStrAction, null, pStrMsgXml);\n");
        //			sb.Append("					}\n");
        //			sb.Append("				}\n");
        //			sb.Append("			}\n");
        //			sb.Append("		}\n");
        //			sb.Append("		/// <summary>Log information</summary>\n");
        //			sb.Append("		private void _log(string pStrAction, string pStrMsgText, string pStrMsgXml) \n");
        //			sb.Append("		{\n");
        //			sb.Append("			if (_config != null )\n");
        //			sb.Append("			{\n");
        //			sb.Append("				if (_config.DoLogInfo)\n");
        //			sb.Append("				{\n");
        //			sb.Append("					if (_oLogText != null )\n");
        //			sb.Append("					{\n");
        //			sb.Append("						_oLogText.Log(pStrAction, pStrMsgText);\n");
        //			sb.Append("					}\n");
        //			sb.Append("					if (_oLogXml != null )\n");
        //			sb.Append("					{\n");
        //			sb.Append("						_oLogXml.Log(pStrAction, null, pStrMsgXml);\n");
        //			sb.Append("					}\n");
        //			sb.Append("				}\n");
        //			sb.Append("			}\n");
        //			sb.Append("\n");
        //			sb.Append("		}\n");
        //			sb.Append("\n");
        //			sb.Append("\n");
        //			sb.Append("	}\n");
        //			sb.Append("}\n");
        //
        //			return (sb.ToString());
        //		}

        /// <summary>
        /// Builds the deletes.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildDeletes(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("BEGIN");
            sb.Append("\n");
            sb.Append("	PRINT '<<<< Dropping Stored Procedure sp" + csn + "Delete >>>>'");
            sb.Append("\n");
            sb.Append("	DROP PROCEDURE [sp" + csn + "Delete]");
            sb.Append("\n");
            sb.Append("END");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("/*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		PROCEDURE NAME: sp" + csn + "Delete");
            sb.Append("\n");
            sb.Append("**		Change History");
            sb.Append("\n");
            sb.Append("*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		Date:		Author:		Description:");
            sb.Append("\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created");
            sb.Append("\n");
            sb.Append("*******************************************************************************/");
            sb.Append("\n");
            sb.Append("CREATE PROCEDURE sp" + csn + "Delete");
            sb.Append("\n");
            sb.Append("(");
            sb.Append("\n");
            sb.Append("@" + csn + "ID        NUMERIC(10) = 0");
            sb.Append("\n");
            sb.Append(")");
            sb.Append("\n");
            sb.Append("AS");
            sb.Append("\n");
            sb.Append("SET NOCOUNT ON");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("-- check if a record with the specified id exists");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("DELETE FROM [" + dbn + "] \nWHERE " + dbn + "_id = " + "@" + csn + "ID");
            sb.Append("\n");
            sb.Append("------------------------------------------------");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("PRINT '<<<< Created Stored Procedure sp" + csn + "Delete >>>>'");
            sb.Append("\n");
            sb.Append("ELSE");
            sb.Append("\n");
            sb.Append("PRINT '<<< Failed Creating Stored Procedure sp" + csn + "Delete >>>'");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");

            return (sb.ToString());
        }

        /// <summary>
        /// Handles the Click event of the btnAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnAll_Click(object sender, System.EventArgs e)
        {
            for (int i = lstTables.Items.Count - 1; i >= 0; i--)
                lstTables.SetSelected(i, true);
        }

        /// <summary>
        /// Handles the Click event of the btnNone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNone_Click(object sender, System.EventArgs e)
        {
            for (int i = lstTables.Items.Count - 1; i >= 0; i--)
                lstTables.SetSelected(i, false);
        }

        /// <summary>
        /// Builds the web services.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildWebServices(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            sb.Append("	[WebMethod(CacheDuration = CACHE_CREATE_" + dbn.ToUpper() + ",\n");
            sb.Append("		 Description = \"Creates a " + csn + " and returns a serialized response object.\")]\n");
            sb.Append("	public string " + csn + "Create(string pSerialized" + csn + "Object)\n");
            sb.Append("	{\n");
            //sb.Append("		_log(\"" + csn + "Create\", \"Received Create request:  \" + pSerialized" + csn + "Object);\n");
            sb.Append("		BusFacBudgeting customer = null;\n");
            sb.Append("		customer = new BusFacBudgeting();\n");
            sb.Append("		bool bValid = false;\n");
            sb.Append("		bValid = IsXmlValid(pSerialized" + csn + "Object);\n");
            //sb.Append("		_log(\"" + csn + "Create\", \"IsValid:  \" + bValid.ToString() + \"\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("		if (bValid)\n");
            sb.Append("		{\n");
            sb.Append("			customer.Response.RequestData = pSerialized" + csn + "Object;\n");
            sb.Append("			if (IsAuthenticated((int)EnumPermission." + csn + "Create))\n");
            sb.Append("			{\n");
            //sb.Append("				_log(\"" + csn + "Create\", \"IsAuthenticated:  True\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("				customer.Response.RequestData = \"<WorkingStatus>Incomplete</WorkingStatus>\";\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = 0;\n");
            sb.Append("				customer." + csn + "Create(pSerialized" + csn + "Object);\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// \n");
            sb.Append("				// does not have permission\n");
            sb.Append("				//\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = -1;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		else\n");
            sb.Append("		{\n");
            sb.Append("			// invalid XML\n");
            sb.Append("			customer.Response.IsXmlValid = false;\n");
            sb.Append("		}\n");
            //sb.Append("		_log(\"" + csn + "Create\", \"Returning response:  \\n\" + customer.Response.ToXml());\n");
            sb.Append("		return customer.Response.ToXml();\n");
            sb.Append("	}\n");
            sb.Append("\n");
            sb.Append("	[WebMethod(CacheDuration = CACHE_EDIT_" + dbn.ToUpper() + ",\n");
            sb.Append("		 Description = \"Modifies a " + csn + " and returns a serialized response object.\")]\n");
            sb.Append("	public string " + csn + "Edit(string pSerialized" + csn + "Object)\n");
            sb.Append("	{\n");
            //sb.Append("		_log(\"" + csn + "Edit\", \"Received edit request:  \" + pSerialized" + csn + "Object);\n");
            sb.Append("		BusFacBudgeting customer = null;\n");
            sb.Append("		customer = new BusFacBudgeting();\n");
            sb.Append("		bool bValid = false;\n");
            sb.Append("		bValid = IsXmlValid(pSerialized" + csn + "Object);\n");
            //sb.Append("		_log(\"" + csn + "Edit\", \"IsValid:  \" + bValid.ToString() + \"\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("		if (bValid)\n");
            sb.Append("		{\n");
            sb.Append("			customer.Response.RequestData = pSerialized" + csn + "Object;\n");
            sb.Append("			if (IsAuthenticated((int)EnumPermission." + csn + "Edit))\n");
            sb.Append("			{\n");
            sb.Append("				customer.Response.RequestData = \"<WorkingStatus>Incomplete</WorkingStatus>\";\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = 0;\n");
            sb.Append("				customer." + csn + "Edit(pSerialized" + csn + "Object);\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// \n");
            sb.Append("				// does not have permission\n");
            sb.Append("				//\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = -1;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		else\n");
            sb.Append("		{\n");
            sb.Append("			// invalid XML\n");
            //sb.Append("			_log(\"" + csn + "Edit\", \"XML is invalid\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("			customer.Response.IsXmlValid = false;\n");
            sb.Append("		}\n");
            sb.Append("\n");
            //sb.Append("		_log(\"" + csn + "Edit\", \"Returning response:  \\n\" + customer.Response.ToXml());\n");
            sb.Append("		return customer.Response.ToXml();\n");
            sb.Append("	}\n");
            sb.Append("\n");
            sb.Append("	[WebMethod(CacheDuration = CACHE_REMOVE_" + dbn.ToUpper() + ",\n");
            sb.Append("		 Description = \"Removes a " + csn + " and returns a serialized response object.\")]\n");
            sb.Append("	public string " + csn + "Remove(string pSerialized" + csn + "Object)\n");
            sb.Append("	{\n");
            //sb.Append("		_log(\"" + csn + "Remove\", \"Received remove request:  \" + pSerialized" + csn + "Object);\n");
            sb.Append("		BusFacBudgeting customer = null;\n");
            sb.Append("		customer = new BusFacBudgeting();\n");
            sb.Append("		bool bValid = false;\n");
            sb.Append("		bValid = IsXmlValid(pSerialized" + csn + "Object);\n");
            //sb.Append("		_log(\"" + csn + "Remove\", \"IsValid:  \" + bValid.ToString() + \"\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("		if (bValid)\n");
            sb.Append("		{\n");
            sb.Append("			customer.Response.RequestData = pSerialized" + csn + "Object;\n");
            sb.Append("			if (IsAuthenticated((int)EnumPermission." + csn + "Remove))\n");
            sb.Append("			{\n");
            sb.Append("				customer.Response.RequestData = \"<WorkingStatus>Incomplete</WorkingStatus>\";\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = 0;\n");
            sb.Append("				customer." + csn + "Remove(pSerialized" + csn + "Object);\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// \n");
            sb.Append("				// does not have permission\n");
            sb.Append("				//\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = -1;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		else\n");
            sb.Append("		{\n");
            sb.Append("			// invalid XML\n");
            //sb.Append("			_log(\"" + csn + "Remove\", \"XML is invalid\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("			customer.Response.IsXmlValid = false;\n");
            sb.Append("		}\n");
            sb.Append("\n");
            //sb.Append("		_log(\"" + csn + "Remove\", \"Returning response:  \\n\" + customer.Response.ToXml());\n");
            sb.Append("		return customer.Response.ToXml();\n");
            sb.Append("	}\n");
            sb.Append("\n");
            sb.Append("	[WebMethod(CacheDuration = CACHE_VIEW_" + dbn.ToUpper() + ",\n");
            sb.Append("		 Description = \"Gets a " + csn + " and returns a serialized response object.\")]\n");
            sb.Append("	public string " + csn + "Get(string pSerialized" + csn + "Object)\n");
            sb.Append("	{\n");
            //sb.Append("		_log(\"" + csn + "Get\", \"Received get request:  \" + pSerialized" + csn + "Object);\n");
            sb.Append("		BusFacBudgeting customer = null;\n");
            sb.Append("		customer = new BusFacBudgeting();\n");
            sb.Append("		bool bValid = false;\n");
            sb.Append("		bValid = IsXmlValid(pSerialized" + csn + "Object);\n");
            //sb.Append("		_log(\"" + csn + "Get\", \"IsValid:  \" + bValid.ToString() + \"\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("		if (bValid)\n");
            sb.Append("		{\n");
            sb.Append("			customer.Response.RequestData = pSerialized" + csn + "Object;\n");
            sb.Append("			if (IsAuthenticated((int)EnumPermission." + csn + "Remove))\n");
            sb.Append("			{\n");
            sb.Append("				customer.Response.RequestData = \"<WorkingStatus>Incomplete</WorkingStatus>\";\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = 0;\n");
            sb.Append("				customer." + csn + "Get(pSerialized" + csn + "Object);\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// \n");
            sb.Append("				// does not have permission\n");
            sb.Append("				//\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = -1;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		else\n");
            sb.Append("		{\n");
            sb.Append("			// invalid XML\n");
            //sb.Append("			_log(\"" + csn + "Get\", \"XML is invalid\\n\" + pSerialized" + csn + "Object);\n");
            sb.Append("			customer.Response.IsXmlValid = false;\n");
            sb.Append("		}\n");
            sb.Append("\n");
            //sb.Append("		_log(\"" + csn + "Get\", \"Returning response:  \\n\" + customer.Response.ToXml());\n");
            sb.Append("		return customer.Response.ToXml();\n");
            sb.Append("	}\n");
            sb.Append("\n");
            sb.Append("	[WebMethod(CacheDuration = CACHE_VIEW_" + dbn.ToUpper() + ",\n");
            sb.Append("		 Description = \"Gets a constrained list of " + csn + "s and returns a serialized response object.\")]\n");
            sb.Append("	public string " + csn + "GetList(string pSerializedEnum" + csn + "Object)\n");
            sb.Append("	{\n");
            //sb.Append("		_log(\"" + csn + "GetList\", \"Received get request:  \" + pSerializedEnum" + csn + "Object);\n");
            sb.Append("		BusFacBudgeting customer = null;\n");
            sb.Append("		customer = new BusFacBudgeting();\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("		bool bValid = false;\n");
            sb.Append("		bValid = IsXmlValid(pSerializedEnum" + csn + "Object);\n");
            //sb.Append("		_log(\"" + csn + "GetList\", \"IsValid:  \" + bValid.ToString() + \"\\n\" + pSerializedEnum" + csn + "Object);\n");
            sb.Append("		if (bValid)\n");
            sb.Append("		{\n");
            sb.Append("			customer.Response.RequestData = pSerializedEnum" + csn + "Object;\n");
            sb.Append("			if (IsAuthenticated((int)EnumPermission." + csn + "View))\n");
            sb.Append("			{\n");
            sb.Append("				customer.Response.RequestData = \"<WorkingStatus>Incomplete</WorkingStatus>\";\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = 0;\n");
            sb.Append("				customer." + csn + "GetList(pSerializedEnum" + csn + "Object);\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// \n");
            sb.Append("				// does not have permission\n");
            sb.Append("				//\n");
            sb.Append("				customer.Response.AuthenticationObject.ID = -1;\n");
            sb.Append("			}\n");
            sb.Append("		}\n");
            sb.Append("		else\n");
            sb.Append("		{\n");
            sb.Append("			// invalid XML\n");
            //sb.Append("			_log(\"" + csn + "GetList\", \"XML is invalid\\n\" + pSerializedEnum" + csn + "Object);\n");
            sb.Append("			customer.Response.IsXmlValid = false;\n");
            sb.Append("		}\n");
            sb.Append("\n");
            //sb.Append("		_log(\"" + csn + "GetList\", \"Returning response:  \\n\" + customer.Response.ToXml());\n");
            sb.Append("		return customer.Response.ToXml();\n");
            sb.Append("	}\n");
            sb.Append("\n");

            return sb.ToString();
        }

        /// <summary>
        /// Builds the create.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="Auto">if set to <c>true</c> [auto].</param>
        /// <returns></returns>
        private string BuildCreate(string TableName, bool Auto)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            sb.Append("/*******************************************************************************\n");
            sb.Append("**		Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**		Date:		Author:		Description:\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = '" + TableName + "')\n");
            sb.Append("BEGIN\n");
            sb.Append("		PRINT 'Dropping Table " + TableName + "'\n");
            sb.Append("		DROP  Table [" + TableName + "]\n");
            sb.Append("END\n");
            sb.Append("GO\n");
            sb.Append("CREATE TABLE [" + TableName + "]\n");
            sb.Append("(\n");

            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmdSP;
                DataTable dt = new DataTable();
                da.Fill(dt);
                int count;
                count = 0;

                foreach (DataColumn dc in dt.Columns)
                {
                    if (count++ == 0)
                    {
                        if (Auto)
                            sb.Append("\t" + Convert.ToString(TableName + "_id").PadRight(25) + " NUMERIC(10) ".PadRight(15) + "  NOT NULL PRIMARY KEY IDENTITY,\n");
                        else
                            sb.Append("\t" + Convert.ToString(TableName + "_id").PadRight(25) + " NUMERIC(10)".PadRight(15) + "  NOT NULL PRIMARY KEY,\n");
                    }
                    else
                    {
                        switch (dc.DataType.ToString())
                        {
                            case "System.String":
                                if (dc.ColumnName.ToLower().IndexOf("note") > -1)
                                    sb.Append("\t" + dc.ColumnName.PadRight(25) + " " + txtSQLTexts.Text.PadRight(15) + " DEFAULT NULL,\n");
                                else
                                    sb.Append("\t" + dc.ColumnName.PadRight(25) + " " + txtSQLStrings.Text.PadRight(15) + " DEFAULT NULL,\n");

                                break;

                            case "System.Decimal":
                                if (dc.ColumnName.ToLower().EndsWith("id"))
                                    sb.Append("\t" + dc.ColumnName.PadRight(25) + " NUMERIC(10)".PadRight(15) + "  DEFAULT 0,\n");
                                else
                                    sb.Append("\t" + dc.ColumnName.PadRight(25) + " " + txtSQLNumbers.Text.PadRight(15) + " DEFAULT 0,\n");
                                break;

                            case "System.DateTime":
                                sb.Append("\t" + dc.ColumnName.PadRight(25) + " " + txtSQLDates.Text.PadRight(15) + " DEFAULT NULL,\n");
                                break;

                            default:
                                sb.Append("\t" + dc.ColumnName.PadRight(25) + " " + txtSQLStrings.Text.PadRight(15) + " DEFAULT NULL,\n");
                                break;
                        }
                    }
                }
                sb.Remove(sb.ToString().Length - 2, 1);
                da.Dispose();
                dt.Dispose();
            }
            catch { }

            sb.Append(")\n");
            sb.Append("GO\n");
            sb.Append("\n");
            sb.Append("IF OBJECT_ID('" + TableName + "') IS NOT NULL\n");
            sb.Append("    PRINT '<<< CREATED TABLE " + TableName + " >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("    PRINT '<<< FAILED CREATING TABLE " + TableName + " >>>'\n");
            sb.Append("GO\n");

            return sb.ToString();
        }

        /// <summary>
        /// Builds the inserts.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildInserts(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);
            bool Auto = false;

            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("BEGIN");
            sb.Append("\n");
            sb.Append("	PRINT '<<<< Dropping Stored Procedure sp" + csn + "Insert >>>>'");
            sb.Append("\n");
            sb.Append("	DROP PROCEDURE [sp" + csn + "Insert]");
            sb.Append("\n");
            sb.Append("END");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("/*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		PROCEDURE NAME: sp" + csn + "Insert");
            sb.Append("\n");
            sb.Append("**		Change History");
            sb.Append("\n");
            sb.Append("*******************************************************************************");
            sb.Append("\n");
            sb.Append("**		Date:		Author:		Description:");
            sb.Append("\n");
            sb.Append("**		"); sb.Append(DateTime.UtcNow.ToShortDateString()); sb.Append("		" + txtCB.Text + "		Created");
            sb.Append("\n");
            sb.Append("*******************************************************************************/");
            sb.Append("\n");
            sb.Append("CREATE PROCEDURE sp" + csn + "Insert");
            sb.Append("\n");
            sb.Append("(");
            sb.Append("\n");

            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
                con2.Close();
                if (con2.State == ConnectionState.Closed) con2.Open();
                DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            break;

                        case "21": // VARBINARY
                            //sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLStrings.Text.Replace("SIZE", dr["ColumnSize"].ToString()) + " = NULL,\n");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " VARBINARY(MAX)" + " = NULL,\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLTexts.Text + " = NULL,\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " N" + txtSQLTexts.Text + " = NULL,\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                                else
                                    // NUMERIC(10)
                                    sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " NUMERIC(10) = 0,\n");
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "1").Replace("PREC", "0") + " = 0,\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", "0") + " = 0,\n");
                            }
                            else
                            {
                                // DECIMAL
                                string sc = dr["NumericScale"].ToString();
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " " + txtSQLNumbers.Text.Replace("SIZE", "10").Replace("PREC", sc) + " = 0,\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            if (dr["ColumnName"].ToString() != "date_modified")
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " SMALLDATETIME = NULL,\n");
                            break;

                        case "4": //DATETETIME
                            if (dr["ColumnName"].ToString() != "date_modified")
                                sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " DATETIME = NULL,\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()).PadRight(25) + " BINARY(10) = NULL,\n");
                            break;
                    }
                }

                sb.Append("\t@" + "PKID".PadRight(25) + " NUMERIC(10) OUTPUT");
                con2.Close();

                Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";
                dt.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("\n");
            sb.Append(")");
            sb.Append("\n");
            sb.Append("AS");
            sb.Append("\n");
            sb.Append("SET NOCOUNT ON");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");

            sb.Append("   -- Update record wth NUMERIC(10) value\n\n");

            sb.Append("INSERT INTO [" + dbn + "]\n");
            sb.Append("(\n");
            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
                con2.Close();
                if (con2.State == ConnectionState.Closed) con2.Open();
                DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
                bool first = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (!first || !Auto)
                        if (dr["ColumnName"].ToString() != "date_modified")
                            sb.Append("\t[" + dr["ColumnName"].ToString() + "],\n");
                    first = false;
                }

                sb.Remove(sb.ToString().Length - 2, 2);
                sb.Append("\n)\n VALUES \n(\n");
            }
            catch { }

            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
                con2.Close();
                if (con2.State == ConnectionState.Closed) con2.Open();
                DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
                bool first = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (!first || !Auto)
                        if (dr["ColumnName"].ToString() != "date_modified")
                            sb.Append("\t@" + CamCase(dr["ColumnName"].ToString()) + ",\n");
                    first = false;
                }

                sb.Remove(sb.ToString().Length - 2, 2);
                sb.Append("\n)\n");
            }
            catch { }

            sb.Append("\n\n");
            sb.Append("-- return ID for new record\n");

            if (Auto)
                sb.Append("SELECT @PKID = SCOPE_IDENTITY()\n");
            else
                sb.Append("SELECT @PKID = @" + csn + "ID\n");

            sb.Append("\n");
            sb.Append("------------------------------------------------");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");
            sb.Append("IF EXISTS (select * from sysobjects where id = object_id(N'[sp" + csn + "Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.Append("\n");
            sb.Append("PRINT '<<<< Created Stored Procedure sp" + csn + "Insert >>>>'");
            sb.Append("\n");
            sb.Append("ELSE");
            sb.Append("\n");
            sb.Append("PRINT '<<< Failed Creating Stored Procedure sp" + csn + "Insert >>>'");
            sb.Append("\n");
            sb.Append("GO");
            sb.Append("\n");
            sb.Append("");
            sb.Append("\n");

            return (sb.ToString());
        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            int j = 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string s in lstTables.Items)
            {
                j++;
                //txtOutput.Text += "Console.WriteLine(\"" + j + ".  Unit test " + CamCase(s) + " DAL object.\");\n";

                //				sb.Append("	private const int CACHE_CREATE_" + s.ToUpper() + " = 0;\n");
                //				sb.Append("	private const int CACHE_EDIT_" + s.ToUpper() + " = 0;\n");
                //				sb.Append("	private const int CACHE_REMOVE_" + s.ToUpper() + " = 0;\n");
                //				sb.Append("	private const int CACHE_VIEW_" + s.ToUpper() + " = 0;\n");

                sb.Append("		" + CamCase(s) + "Create = " + j.ToString() + ",\n");
                j++;
                sb.Append("		" + CamCase(s) + "Edit = " + j.ToString() + ",\n");
                j++;
                sb.Append("		" + CamCase(s) + "View = " + j.ToString() + ",\n");
                j++;
                sb.Append("		" + CamCase(s) + "Remove = " + j.ToString() + ",\n");

                //				txtOutput.Text += "	case " + j + ":\n";
                //				txtOutput.Text += "	" + CamCase(s) + " obj" + CamCase(s) + " = null;\n";
                //				txtOutput.Text += "	obj" + CamCase(s) + " = new " + CamCase(s) + "();\n";
                //				txtOutput.Text += "	conn.Open();\n";
                //				txtOutput.Text += "	obj" + CamCase(s) + ".Test(conn);\n";
                //				txtOutput.Text += "	conn.Close();\n";
                //				txtOutput.Text += "	Console.WriteLine(obj" + CamCase(s) + ".ToString());\n";
                //				txtOutput.Text += "	obj" + CamCase(s) + " = null;\n";
                //				txtOutput.Text += "	break;\n";

                //				sb.Append("		case " + j + ":\n");
                //				sb.Append("			 pool.OpenConnection(conn);\n");
                //				sb.Append("			 Bus" + CamCase(s) + " o" + CamCase(s) + " = new Bus" + CamCase(s) + "(conn, oConfig);\n");
                //				sb.Append("			 o" + CamCase(s) + ".Test();\n");
                //				sb.Append("			 pool.CloseConnection(conn);\n");
                //				sb.Append("			 break;\n\n");

                //sb.Append("			Console.WriteLine(\"" + j + ".  Unit test " + CamCase(s) + " ENUM object.\");\n");

                //sb.Append("			Console.WriteLine(\"" + j + ".  Unit test the Biz " + CamCase(s) + "\");\n");

                //				sb.Append("							case " + j + ":\n");
                //				sb.Append("								conn.Open();\n");
                //				sb.Append("								Enum" + CamCase(s) + " enum" + CamCase(s) + " = new Enum" + CamCase(s) + "(conn, oConfig);\n");
                //				sb.Append("								enum" + CamCase(s) + ".Prompt();\n");
                //				sb.Append("								enum" + CamCase(s) + ".EnumData();\n");
                //				sb.Append("								while (enum" + CamCase(s) + ".hasMoreElements())\n");
                //				sb.Append("								{\n");
                //				sb.Append("									data = (" + CamCase(s) + ") enum" + CamCase(s) + ".nextElement();\n");
                //				sb.Append("									Console.WriteLine(data.ToString());\n");
                //				sb.Append("									Console.WriteLine(\"----------\");\n");
                //				sb.Append("								}\n");
                //				sb.Append("								Console.WriteLine(\"Total Count:  \" + enum" + CamCase(s) + ".Count.ToString());\n");
                //				sb.Append("								Console.WriteLine(\"----------\");\n");
                //				sb.Append("								conn.Close();\n");
                //				sb.Append("								enum" + CamCase(s) + " = null;\n");
                //				sb.Append("								break;\n\n");
            }
            txtOutput.Text = sb.ToString();
        }

        /// <summary>
        /// Handles the TextChanged event of the txtOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtOutput_TextChanged(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lstTables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lstTables_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + lstTables.SelectedItems[0].ToString() + "]", con2);
                SqlDataReader dr;
                con2.Close();
                con2.Open();

                dr = cmdSP.ExecuteReader();
                DataTable x = dr.GetSchemaTable();
                dg.DataSource = x;
            }
            catch { }
        }

        /// <summary>
        /// Builds the proxy class.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildProxyClass(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();

            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Text;\n");
            sb.Append("\n");
            sb.Append("namespace Vetapp.Client.Proxy\n");
            sb.Append("{\n");
            sb.Append("	/// <summary>\n");
            sb.Append("	/// Copyright (c) " + DateTime.UtcNow.Year.ToString() + " Haytham Allos.  San Diego, California, USA\n");
            sb.Append("	/// All Rights Reserved\n");
            sb.Append("	/// \n");
            sb.Append("	/// File:  " + csn + ".cs\n");
            sb.Append("	/// History\n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// 001	" + this.txtCB.Text + "     " + DateTime.UtcNow.ToShortDateString() + "	Created\n");
            sb.Append("	/// \n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// Abstracts the " + csn + " database table.\n");
            sb.Append("	/// </summary>\n");
            sb.Append("	public class " + csn + "\n");
            sb.Append("	{\n");
            sb.Append("		//attributes\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type byte[]</summary>\n");
                            sb.Append("		private byte[] _byte" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {	// NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type long</summary>\n");
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                }
                                else
                                {	// NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type long</summary>\n");
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type bool</summary>\n");
                                sb.Append("		private " + _strBoolText + " _b" + CamCase(dr["ColumnName"].ToString()) + " = " + _strBoolDefaultValue + ";\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type long</summary>\n");
                                sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type double</summary>\n");
                                sb.Append("		private double _d" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type DateTime</summary>\n");
                            sb.Append("		private DateTime _dt" + CamCase(dr["ColumnName"].ToString()) + " = dtNull;\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type DateTime</summary>\n");
                            sb.Append("		private DateTime _dt" + CamCase(dr["ColumnName"].ToString()) + " = dtNull;\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " type: " + dr["ProviderType"].ToString());
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type unknown</summary>\n");
                            sb.Append("		private object _o" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("\n");
            sb.Append("		private static DateTime dtNull = new DateTime();\n");
            sb.Append("		private bool _hasError = false;\n");
            sb.Append("		private ErrorCode _errorCode = null;\n");
            sb.Append("\n");
            sb.Append("		/// <summary>Attribute of type string</summary>\n");
            sb.Append("		public static readonly string ENTITY_NAME = \"" + csn + "\"; //Table name to abstract\n");
            sb.Append("\n");
            sb.Append("		// Attribute variables\n");

            sb.Append("		/// <summary>TAG_ID Attribute</summary>\n");
            sb.Append("		public static readonly string TAG_ID = \"" + csn + "ID\"; //Attribute id  name\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                {
                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                    sb.Append("		public static readonly string TAG_" + dr["ColumnName"].ToString().ToUpper() + " = \"" + CamCase(dr["ColumnName"].ToString()) + "\"; //Table " + CamCase(dr["ColumnName"].ToString()) + " field name\n");
                }
            }

            sb.Append("\n");
            sb.Append("\n");

            sb.Append("/*********************** CUSTOM NON-META BEGIN *********************/\n");
            sb.Append("\n");
            sb.Append("/*********************** CUSTOM NON-META END *********************/\n");
            sb.Append("\n\n");
            sb.Append("		//properties\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type byte[]</summary>\n");
                            sb.Append("		public byte[] " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _byte" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_byte" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type bool</summary>\n");
                                sb.Append("		public " + _strBoolText + " " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _b" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_b" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type double</summary>\n");
                                sb.Append("		public double " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _d" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_d" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type DateTime</summary>\n");
                            sb.Append("		public DateTime " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dt" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dt" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type DateTime</summary>\n");
                            sb.Append("		public DateTime " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dt" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dt" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("		/// <summary>HasError Property in class " + csn + " and is of type bool</summary>\n");
            sb.Append("		public  bool HasError \n");
            sb.Append("		{\n");
            sb.Append("			get{return _hasError;}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Error Property in class " + csn + " and is of type ErrorCode</summary>\n");
            sb.Append("		public ErrorCode Error \n");
            sb.Append("		{\n");
            sb.Append("			get{return _errorCode;}\n");
            sb.Append("		}\n");
            sb.Append("//Constructors\n");
            sb.Append("		/// <summary>" + csn + " empty constructor</summary>\n");
            sb.Append("		public " + csn + "()\n");
            sb.Append("		{\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>" + csn + " Constructor takes string pStrData and Config</summary>\n");
            sb.Append("		public " + csn + "(string pStrData)\n");
            sb.Append("		{\n");
            sb.Append("			Parse(pStrData);\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///     Dispose of this object's resources.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public void Dispose()\n");
            sb.Append("		{\n");
            sb.Append("			Dispose(true);\n");
            sb.Append("			GC.SuppressFinalize(true); // as a service to those who might inherit from us\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///		Free the instance variables of this object.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		protected virtual void Dispose(bool disposing)\n");
            sb.Append("		{\n");
            sb.Append("			if (! disposing)\n");
            sb.Append("				return; // we're being collected, so let the GC take care of this object\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		// public methods\n");
            sb.Append("		/// <summary>ToString is overridden to display all properties of the " + csn + " Class</summary>\n");
            sb.Append("		public override string ToString() \n");
            sb.Append("		{\n");
            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");

            sb.Append("			sbReturn.Append(TAG_ID + \":  \" + " + csn + "ID.ToString() + \"\\n\");\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + " + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
            }

            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Creates well formatted XML - includes all properties of " + csn + "</summary>\n");
            sb.Append("		public string ToXml() \n");
            sb.Append("		{\n");
            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");
            sb.Append("			sbReturn.Append(\"<" + csn + ">\\n\");\n");
            sb.Append("			sbReturn.Append(\"<\" + TAG_ID + \">\" + " + csn + "ID + \"</\" + TAG_ID + \">\\n\");\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + " + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"</\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \"></\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"</\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
            }

            sb.Append("			sbReturn.Append(\"</" + csn + ">\" + \"\\n\");\n");
            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Parse accepts a string in XML format and parses values</summary>\n");
            sb.Append("		public void Parse(string pStrXml)\n");
            sb.Append("		{\n");

            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				XmlDocument xmlDoc = null;\n");
            sb.Append("				string strXPath = null;\n");
            sb.Append("				XmlNodeList xNodes = null;\n");
            sb.Append("\n");
            sb.Append("				xmlDoc = new XmlDocument();\n");
            sb.Append("				xmlDoc.LoadXml(pStrXml);\n");
            sb.Append("\n");
            sb.Append("				// get the element\n");
            sb.Append("				strXPath = \"//\" + ENTITY_NAME;\n");
            sb.Append("				xNodes = xmlDoc.SelectNodes(strXPath);\n");
            sb.Append("				foreach (XmlNode xNode in xNodes)\n");
            sb.Append("				{\n");
            sb.Append("					Parse(xNode);\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch \n");
            sb.Append("			{\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");

            sb.Append("		}		\n");
            sb.Append("		/// <summary>Parse accepts an XmlNode and parses values</summary>\n");
            sb.Append("		public void Parse(XmlNode xNode)\n");
            sb.Append("		{\n");

            sb.Append("			XmlNode xResultNode = null;\n");
            sb.Append("			string strTmp = null;\n");
            sb.Append("\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_ID);\n");
            sb.Append("				strTmp = xResultNode.InnerText;\n");
            sb.Append("				" + csn + "ID = (long) Convert.ToInt32(strTmp);\n");
            sb.Append("			}\n");
            sb.Append("			catch  \n");
            sb.Append("			{\n");
            sb.Append("			}\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("         // Cannot reliably convert a byte[] to a string().\n");
                            break;

                        case "18": // TEXT
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = null;\n");
                            sb.Append("			}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                    break;
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = false;\n");
                                sb.Append("			}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                                break;
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");
                            break;

                        case "4": //DATETETIME
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("		}\n");
            sb.Append("	}\n");
            sb.Append("}\n");
            sb.Append("\n");
            sb.Append("//END OF " + csn + " CLASS FILE\n");
            sb.Append("\n");

            return sb.ToString();
        }

        /// <summary>
        /// Builds the enum proxy class.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildEnumProxyClass(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();

            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Text;\n");
            sb.Append("using System.Collections;\n");
            sb.Append("\n");
            sb.Append("namespace Vetapp.Client.Proxy\n");
            sb.Append("{\n");
            sb.Append("	/// <summary>\n");
            sb.Append("	/// Copyright (c) " + DateTime.UtcNow.Year.ToString() + " Haytham Allos.  San Diego, California, USA\n");
            sb.Append("	/// All Rights Reserved\n");
            sb.Append("	/// \n");
            sb.Append("	/// File:  " + csn + ".cs\n");
            sb.Append("	/// History\n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// 001	" + this.txtCB.Text + "     " + DateTime.UtcNow.ToShortDateString() + "	Created\n");
            sb.Append("	/// \n");
            sb.Append("	/// ----------------------------------------------------\n");
            sb.Append("	/// </summary>\n");
            sb.Append("	public class Enum" + csn + "\n");
            sb.Append("	{\n");
            sb.Append("		//attributes\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type byte[]</summary>\n");
                            sb.Append("		private byte[] _byte" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                            sb.Append("		private string _str" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {	// NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type long</summary>\n");
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                }
                                else
                                {	// NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type long</summary>\n");
                                    sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type bool</summary>\n");
                                sb.Append("		private " + _strBoolText + " _b" + CamCase(dr["ColumnName"].ToString()) + " = false;\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type long</summary>\n");
                                sb.Append("		private long _l" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type double</summary>\n");
                                sb.Append("		private double _d" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");

                                sb.Append("		/// <summary>Begin" + CamCase(dr["ColumnName"].ToString()) + " Attribute type double</summary>\n");
                                sb.Append("		private double _dBegin" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("		/// <summary>End" + CamCase(dr["ColumnName"].ToString()) + " Attribute type double</summary>\n");
                                sb.Append("		private double _dEnd" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                            }
                            break;

                        case "4": // DATETIME
                        case "15": //SMALLDATETIME
                            sb.Append("		/// <summary>Begin" + CamCase(dr["ColumnName"].ToString()) + " Attribute type DateTime</summary>\n");
                            sb.Append("		private DateTime _dtBegin" + CamCase(dr["ColumnName"].ToString()) + " = dtNull;\n");
                            sb.Append("		/// <summary>End" + CamCase(dr["ColumnName"].ToString()) + " Attribute type DateTime</summary>\n");
                            sb.Append("		private DateTime _dtEnd" + CamCase(dr["ColumnName"].ToString()) + " = dtNull;\n");
                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " type: " + dr["ProviderType"].ToString());
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type unknown</summary>\n");
                            sb.Append("		private object _o" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("\n");
            sb.Append("		private static DateTime dtNull = new DateTime();\n");
            sb.Append("		private bool _hasError = false;\n");
            sb.Append("		private ErrorCode _errorCode = null;\n");
            sb.Append("\n");
            sb.Append("		/// <summary>Attribute of type string</summary>\n");
            sb.Append("		public static readonly string ENTITY_NAME = \"Enum" + csn + "\"; //Table name to abstract\n");
            sb.Append("\n");
            sb.Append("		// Attribute variables\n");

            sb.Append("		/// <summary>TAG_ID Attribute</summary>\n");
            sb.Append("		public static readonly string TAG_ID = \"" + csn + "ID\"; //Attribute id  name\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                {
                    if (dr["ProviderType"].ToString() == "4" || dr["ProviderType"].ToString() == "15")
                    {
                        sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                        sb.Append("		public static readonly string TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " = \"Begin" + CamCase(dr["ColumnName"].ToString()) + "\"; //Table " + CamCase(dr["ColumnName"].ToString()) + " field name\n");
                        sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                        sb.Append("		public static readonly string TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " = \"End" + CamCase(dr["ColumnName"].ToString()) + "\"; //Table " + CamCase(dr["ColumnName"].ToString()) + " field name\n");
                    }
                    else
                    {
                        sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " Attribute type string</summary>\n");
                        sb.Append("		public static readonly string TAG_" + dr["ColumnName"].ToString().ToUpper() + " = \"" + CamCase(dr["ColumnName"].ToString()) + "\"; //Table " + CamCase(dr["ColumnName"].ToString()) + " field name\n");
                    }
                }
            }

            sb.Append("\n\n");
            sb.Append("/*********************** CUSTOM NON-META BEGIN *********************/\n");
            sb.Append("\n");
            sb.Append("/*********************** CUSTOM NON-META END *********************/\n");
            sb.Append("\n\n");

            sb.Append("		//properties\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "12": // NVARCHAR
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "21": // VARBINARY
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type byte[]</summary>\n");
                            sb.Append("		public byte[] " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _byte" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_byte" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "18": // TEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "11": // NTEXT
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type String</summary>\n");
                            sb.Append("		public string " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _str" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_str" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                    sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                    sb.Append("		{\n");
                                    sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                    sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                    sb.Append("		}\n");
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type bool</summary>\n");
                                sb.Append("		public " + _strBoolText + " " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _b" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_b" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type long</summary>\n");
                                sb.Append("		public long " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _l" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_l" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type double</summary>\n");
                                sb.Append("		public double " + CamCase(dr["ColumnName"].ToString()) + " \n");
                                sb.Append("		{\n");
                                sb.Append("			get{return _d" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                                sb.Append("			set{_d" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                                sb.Append("		}\n");
                            }
                            break;

                        case "15": //SMALLDATETIME
                        case "4": //DATETETIME
                            sb.Append("		/// <summary>Begin" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type DateTime</summary>\n");
                            sb.Append("		public DateTime Begin" + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dtBegin" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dtBegin" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");

                            sb.Append("		/// <summary>End" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type DateTime</summary>\n");
                            sb.Append("		public DateTime End" + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _dtEnd" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_dtEnd" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");

                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("		/// <summary>HasError Property in class " + csn + " and is of type bool</summary>\n");
            sb.Append("		public  bool HasError \n");
            sb.Append("		{\n");
            sb.Append("			get{return _hasError;}\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Error Property in class " + csn + " and is of type ErrorCode</summary>\n");
            sb.Append("		public ErrorCode Error \n");
            sb.Append("		{\n");
            sb.Append("			get{return _errorCode;}\n");
            sb.Append("		}\n");
            sb.Append("//Constructors\n");
            sb.Append("		/// <summary>" + csn + " empty constructor</summary>\n");
            sb.Append("		public Enum" + csn + "()\n");
            sb.Append("		{\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>" + csn + " Constructor takes string pStrData and Config</summary>\n");
            sb.Append("		public Enum" + csn + "(string pStrData)\n");
            sb.Append("		{\n");
            sb.Append("			Parse(pStrData);\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///     Dispose of this object's resources.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		public void Dispose()\n");
            sb.Append("		{\n");
            sb.Append("			Dispose(true);\n");
            sb.Append("			GC.SuppressFinalize(true); // as a service to those who might inherit from us\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>\n");
            sb.Append("		///		Free the instance variables of this object.\n");
            sb.Append("		/// </summary>\n");
            sb.Append("		protected virtual void Dispose(bool disposing)\n");
            sb.Append("		{\n");
            sb.Append("			if (! disposing)\n");
            sb.Append("				return; // we're being collected, so let the GC take care of this object\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		// public methods\n");
            sb.Append("		/// <summary>ToString is overridden to display all properties of the " + csn + " Class</summary>\n");
            sb.Append("		public override string ToString() \n");
            sb.Append("		{\n");
            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");

            sb.Append("			sbReturn.Append(TAG_ID + \":  \" + " + csn + "ID.ToString() + \"\\n\");\n");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(Begin" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + Begin" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \":\\n\");\n");
                        sb.Append("			}\n");

                        sb.Append("			if (!dtNull.Equals(End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + End" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \":\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \":  \" + " + CamCase(dr["ColumnName"].ToString()) + " + \"\\n\");\n");
            }

            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Creates well formatted XML - includes all properties of " + csn + "</summary>\n");
            sb.Append("		public string ToXml() \n");
            sb.Append("		{\n");
            sb.Append("			StringBuilder sbReturn = null;\n");
            sb.Append("\n");
            sb.Append("			sbReturn = new StringBuilder();	\n");
            sb.Append("			sbReturn.Append(\"<\" + ENTITY_NAME + \">\\n\");\n");
            sb.Append("			sbReturn.Append(\"<\" + TAG_ID + \">\" + " + csn + "ID + \"</\" + TAG_ID + \">\\n\");\n");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProviderType"].ToString() == "29")
                {
                    continue;
                }
                if (dr["ColumnName"].ToString() != dbn + "_id")
                    if (dr["ProviderType"].ToString() == "4"
                        || dr["ProviderType"].ToString() == "15") //Dates
                    {
                        sb.Append("			if (!dtNull.Equals(Begin" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + Begin" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"</\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \"></\" + TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");

                        sb.Append("			if (!dtNull.Equals(End" + CamCase(dr["ColumnName"].ToString()) + "))\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + End" + CamCase(dr["ColumnName"].ToString()) + ".ToString() + \"</\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                        sb.Append("			else\n");
                        sb.Append("			{\n");
                        sb.Append("				sbReturn.Append(\"<\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \"></\" + TAG_END_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
                        sb.Append("			}\n");
                    }
                    else
                        sb.Append("			sbReturn.Append(\"<\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\" + " + CamCase(dr["ColumnName"].ToString()) + " + \"</\" + TAG_" + dr["ColumnName"].ToString().ToUpper() + " + \">\\n\");\n");
            }

            sb.Append("			sbReturn.Append(\"</\" + ENTITY_NAME + \">\" + \"\\n\");\n");
            sb.Append("\n");
            sb.Append("			return sbReturn.ToString();\n");
            sb.Append("		}\n");
            sb.Append("		/// <summary>Parse accepts a string in XML format and parses values</summary>\n");
            sb.Append("		public void Parse(string pStrXml)\n");
            sb.Append("		{\n");

            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				XmlDocument xmlDoc = null;\n");
            sb.Append("				string strXPath = null;\n");
            sb.Append("				XmlNodeList xNodes = null;\n");
            sb.Append("\n");
            sb.Append("				xmlDoc = new XmlDocument();\n");
            sb.Append("				xmlDoc.LoadXml(pStrXml);\n");
            sb.Append("\n");
            sb.Append("				// get the element\n");
            sb.Append("				strXPath = \"//\" + ENTITY_NAME;\n");
            sb.Append("				xNodes = xmlDoc.SelectNodes(strXPath);\n");
            sb.Append("				foreach (XmlNode xNode in xNodes)\n");
            sb.Append("				{\n");
            sb.Append("					Parse(xNode);\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch \n");
            sb.Append("			{\n");
            sb.Append("				_hasError = true;\n");
            sb.Append("				_errorCode = new ErrorCode();\n");
            sb.Append("			}\n");

            sb.Append("		}		\n");

            sb.Append("/// <summary>Parses XML, puts in arraylist</summary>\n");
            sb.Append("public static void ParseTo" + csn + "ArrayList(string pStrXml, ArrayList pRefArrayList)\n");
            sb.Append("		{\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				" + csn + " " + dbn + " = null;\n");
            sb.Append("				XmlDocument xmlDoc = null;\n");
            sb.Append("				string strXPath = null;\n");
            sb.Append("				XmlNodeList xNodes = null;\n");
            sb.Append("\n");
            sb.Append("				xmlDoc = new XmlDocument();\n");
            sb.Append("				xmlDoc.LoadXml(pStrXml);\n");
            sb.Append("\n");
            sb.Append("				// get the element\n");
            sb.Append("				strXPath = \"//\" + " + csn + ".ENTITY_NAME;\n");
            sb.Append("				xNodes = xmlDoc.SelectNodes(strXPath);\n");
            sb.Append("				foreach (XmlNode xNode in xNodes)\n");
            sb.Append("				{\n");
            sb.Append("					if (pRefArrayList != null)\n");
            sb.Append("					{\n");
            sb.Append("						" + dbn + " = new " + csn + "();\n");
            sb.Append("						" + dbn + ".Parse(xNode);\n");
            sb.Append("						pRefArrayList.Add(" + dbn + ");\n");
            sb.Append("					}\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			catch (Exception e)\n");
            sb.Append("			{\n");
            sb.Append("			}\n");
            sb.Append("		}\n");

            sb.Append("		/// <summary>Parse accepts an XmlNode and parses values</summary>\n");
            sb.Append("		public void Parse(XmlNode xNode)\n");
            sb.Append("		{\n");

            sb.Append("			XmlNode xResultNode = null;\n");
            sb.Append("			string strTmp = null;\n");
            sb.Append("\n");
            sb.Append("			try\n");
            sb.Append("			{\n");
            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_ID);\n");
            sb.Append("				strTmp = xResultNode.InnerText;\n");
            sb.Append("				" + csn + "ID = (long) Convert.ToInt32(strTmp);\n");
            sb.Append("			}\n");
            sb.Append("			catch  \n");
            sb.Append("			{\n");
            sb.Append("			}\n");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ProviderType"].ToString() == "29")
                    {
                        continue;
                    }

                    if (dr["ColumnName"].ToString() == dbn + "_id") continue;
                    switch (dr["ProviderType"].ToString())
                    {
                        case "22": // VARCHAR
                        case "12": // NVARCHAR
                        case "21": // VARBINARY
                        case "18": // TEXT
                        case "11": // NTEXT
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = xResultNode.InnerText;\n");
                            sb.Append("				if (" + CamCase(dr["ColumnName"].ToString()) + ".Trim().Length == 0)\n");
                            sb.Append("					" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = null;\n");
                            sb.Append("			}\n");
                            break;
                            break;

                        case "20":
                        case "2":
                        case "16":
                        case "8":
                        case "5": // NUMERIC
                            if (dr["ColumnName"].ToString().ToLower().EndsWith("_id"))
                            {
                                if (dr["ColumnName"].ToString() == (dbn + "_id"))
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                }
                                else
                                {
                                    // NUMERIC(10)
                                    sb.Append("\n");
                                    sb.Append("			try\n");
                                    sb.Append("			{\n");
                                    sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                    sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                    sb.Append("			}\n");
                                    sb.Append("			catch  \n");
                                    sb.Append("			{\n");
                                    sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                    sb.Append("			}\n");
                                    break;
                                }
                            }
                            else if (dr["NumericPrecision"].ToString() == "1" || dr["ProviderType"].ToString() == "16" || dr["ProviderType"].ToString() == "2" || dr["ProviderType"].ToString() == "20")
                            {
                                // BOOLEAN
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToBoolean(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = false;\n");
                                sb.Append("			}\n");
                            }
                            else if (dr["NumericScale"].ToString() == "0" || dr["NumericScale"].ToString() == "255")
                            {
                                // INT / LONG
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = (long) Convert.ToInt32(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                                break;
                            }
                            else
                            {
                                // DECIMAL
                                sb.Append("\n");
                                sb.Append("			try\n");
                                sb.Append("			{\n");
                                sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                                sb.Append("				" + CamCase(dr["ColumnName"].ToString()) + " = Convert.ToSingle(xResultNode.InnerText);\n");
                                sb.Append("			}\n");
                                sb.Append("			catch  \n");
                                sb.Append("			{\n");
                                sb.Append("			" + CamCase(dr["ColumnName"].ToString()) + " = 0;\n");
                                sb.Append("			}\n");
                            }
                            break;

                        case "4": //DATETETIME
                        case "15": //SMALLDATETIME
                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				Begin" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");

                            sb.Append("\n");
                            sb.Append("			try\n");
                            sb.Append("			{\n");
                            sb.Append("				xResultNode = xNode.SelectSingleNode(TAG_END_" + dr["ColumnName"].ToString().ToUpper() + ");\n");
                            sb.Append("				End" + CamCase(dr["ColumnName"].ToString()) + " = DateTime.Parse(xResultNode.InnerText);\n");
                            sb.Append("			}\n");
                            sb.Append("			catch  \n");
                            sb.Append("			{\n");
                            sb.Append("			}\n");

                            break;

                        default: // OTHER?
                            MessageBox.Show(dr["ColumnName"].ToString() + " in table: " + dbn + " has an unknown type");
                            sb.Append("		/// <summary>" + CamCase(dr["ColumnName"].ToString()) + " is a Property in the " + csn + " Class of type object</summary>\n");
                            sb.Append("		public object " + CamCase(dr["ColumnName"].ToString()) + " \n");
                            sb.Append("		{\n");
                            sb.Append("			get{return _o" + CamCase(dr["ColumnName"].ToString()) + ";}\n");
                            sb.Append("			set{_o" + CamCase(dr["ColumnName"].ToString()) + " = value;}\n");
                            sb.Append("		}\n");
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            sb.Append("		}\n");
            sb.Append("	}\n");
            sb.Append("}\n");
            sb.Append("\n");
            sb.Append("//END OF " + csn + " CLASS FILE\n");
            sb.Append("\n");

            return sb.ToString();
        }

        /// <summary>
        /// NSs the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        private string NS(string s)
        {
            switch (s.ToLower())
            {
                case "rule":
                case "version":
                case "attribute":
                    return DATA_NAMESPACE + s;

                default:
                    return s;
            }
        }

        /// <summary>
        /// Builds the biz facade.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        private string BuildBizFacade(string TableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string dbn = TableName;
            string csn = CamCase(TableName);
            bool Auto = false;

            SqlCommand cmdSP = new SqlCommand("select TOP 1 * from [" + dbn + "]", con2);
            con2.Close();
            if (con2.State == ConnectionState.Closed) con2.Open();
            DataTable dt = cmdSP.ExecuteReader().GetSchemaTable();
            Auto = dt.Select("ColumnName = '" + dbn + "_id'")[0]["IsAutoIncrement"].ToString().ToLower() == "true";

            sb = new System.Text.StringBuilder();

            sb.Append("\n");
            sb.Append("		public void " + csn + "Create(string pSerializedObject)\n");
            sb.Append("		{\n");
            //sb.Append("			_log(\"CREATE\", \"Received Create request:  \" + pSerializedObject);\n");
            sb.Append("\n");
            sb.Append("			// create a new object\n");
            sb.Append("			" + NS(csn) + " " + PascalCase(dbn) + " = null;\n");
            sb.Append("			" + PascalCase(dbn) + " = new " + NS(csn) + "(_config);\n");
            sb.Append("			// deserialize the input\n");
            sb.Append("			" + PascalCase(dbn) + ".Parse( pSerializedObject );\n");
            sb.Append("\n");
            sb.Append("			if ( !" + PascalCase(dbn) + ".HasError ) \n");
            sb.Append("			{\n");

            if (Auto)
            {
                sb.Append("				// make sure the primary id is set to 0 in case\n");
                sb.Append("				// attempting to inject a modify\n");
                sb.Append("				" + PascalCase(dbn) + "." + csn + "ID = 0;\n");
            }

            sb.Append("				SqlConnection conn = GetConnection();\n");
            sb.Append("				if ( conn != null )\n");
            sb.Append("				{\n");
            sb.Append("					bool bConn = false;\n");
            sb.Append("\n");
            sb.Append("					bConn = OpenConnection(conn);\n");
            sb.Append("					if ( bConn )\n");
            sb.Append("					{\n");
            sb.Append("						Bus" + csn + " bus" + csn + " = null;\n");
            sb.Append("						bus" + csn + " = new Bus" + csn + "(conn, _config);\n");
            sb.Append("						bus" + csn + ".Save(" + PascalCase(dbn) + ");\n");
            sb.Append("						// close the db connection\n");
            sb.Append("						bConn = CloseConnection(conn);\n");
            sb.Append("						_response.ResultData = " + PascalCase(dbn) + ".ToXml();\n");
            sb.Append("						if ( bus" + csn + ".HasError )\n");
            sb.Append("						{\n");
            sb.Append("							// error\n");
            sb.Append("							ErrorCode error = new ErrorCode();\n");
            sb.Append("							_response.ErrorArray.Add( error );\n");
            //sb.Append("							_log(\"ERROR\", error.ToString());\n");
            sb.Append("						}\n");
            sb.Append("					}\n");
            sb.Append("					else\n");
            sb.Append("					{\n");
            sb.Append("						// problem in opening the connection\n");
            sb.Append("						// error\n");
            sb.Append("						ErrorCode error = new ErrorCode();\n");
            sb.Append("						_response.ErrorArray.Add( error );\n");
            //sb.Append("						_log(\"ERROR\", error.ToString());\n");
            sb.Append("					}\n");
            sb.Append("					conn = null;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// error has occured, include in result output\n");
            sb.Append("				if ( " + PascalCase(dbn) + " != null )\n");
            sb.Append("				{\n");
            sb.Append("					// error\n");
            sb.Append("					ErrorCode error = new ErrorCode();\n");
            sb.Append("					_response.ErrorArray.Add( error );\n");
            //sb.Append("					_log(\"ERROR\", error.ToString());\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		public void " + csn + "Edit(string pSerializedObject)\n");
            sb.Append("		{\n");
            //sb.Append("			_log(\"EDIT\", \"Received edit request:  \" + pSerializedObject);\n");
            sb.Append("\n");
            sb.Append("			// create a new object\n");
            sb.Append("			" + NS(csn) + " " + PascalCase(dbn) + " = null;\n");
            sb.Append("			" + PascalCase(dbn) + " = new " + NS(csn) + "(_config);\n");
            sb.Append("			// deserialize the input\n");
            sb.Append("			" + PascalCase(dbn) + ".Parse( pSerializedObject );\n");
            sb.Append("\n");
            sb.Append("			if ( !" + PascalCase(dbn) + ".HasError ) \n");
            sb.Append("			{\n");
            sb.Append("				SqlConnection conn = GetConnection();\n");
            sb.Append("				if ( conn != null )\n");
            sb.Append("				{\n");
            sb.Append("					bool bConn = false;\n");
            sb.Append("\n");
            sb.Append("					bConn = OpenConnection(conn);\n");
            sb.Append("					if ( bConn )\n");
            sb.Append("					{\n");
            sb.Append("						Bus" + csn + " bus" + csn + " = null;\n");
            sb.Append("						bus" + csn + " = new Bus" + csn + "(conn, _config);\n");
            sb.Append("						bus" + csn + ".Update(" + PascalCase(dbn) + ");\n");
            sb.Append("						// close the db connection\n");
            sb.Append("						bConn = CloseConnection(conn);\n");
            sb.Append("						_response.ResultData = " + PascalCase(dbn) + ".ToXml();\n");
            sb.Append("						if ( bus" + csn + ".HasError )\n");
            sb.Append("						{\n");
            sb.Append("							// error\n");
            sb.Append("							ErrorCode error = new ErrorCode();\n");
            sb.Append("							_response.ErrorArray.Add( error );\n");
            //sb.Append("							_log(\"ERROR\", error.ToString());\n");
            sb.Append("						}\n");
            sb.Append("					}\n");
            sb.Append("					else\n");
            sb.Append("					{\n");
            sb.Append("						// problem in opening the connection\n");
            sb.Append("						// error\n");
            sb.Append("						ErrorCode error = new ErrorCode();\n");
            sb.Append("						_response.ErrorArray.Add( error );\n");
            //sb.Append("						_log(\"ERROR\", error.ToString());\n");
            sb.Append("					}\n");
            sb.Append("					conn = null;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// error has occured, include in result output\n");
            sb.Append("				if ( " + PascalCase(dbn) + " != null )\n");
            sb.Append("				{\n");
            sb.Append("					// error\n");
            sb.Append("					ErrorCode error = new ErrorCode();\n");
            sb.Append("					_response.ErrorArray.Add( error );\n");
            //sb.Append("					_log(\"ERROR\", error.ToString());\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		public void " + csn + "Remove(string pSerializedObject)\n");
            sb.Append("		{\n");
            //sb.Append("			_log(\"REMOVE\", \"Received edit request:  \" + pSerializedObject);\n");
            sb.Append("\n");
            sb.Append("			// create a new object\n");
            sb.Append("			" + NS(csn) + " " + PascalCase(dbn) + " = null;\n");
            sb.Append("			" + PascalCase(dbn) + " = new " + NS(csn) + "(_config);\n");
            sb.Append("			// deserialize the input\n");
            sb.Append("			" + PascalCase(dbn) + ".Parse( pSerializedObject );\n");
            sb.Append("\n");
            sb.Append("			if ( !" + PascalCase(dbn) + ".HasError ) \n");
            sb.Append("			{\n");
            sb.Append("				SqlConnection conn = GetConnection();\n");
            sb.Append("				if ( conn != null )\n");
            sb.Append("				{\n");
            sb.Append("					bool bConn = false;\n");
            sb.Append("\n");
            sb.Append("					bConn = OpenConnection(conn);\n");
            sb.Append("					if ( bConn )\n");
            sb.Append("					{\n");
            sb.Append("						Bus" + csn + " bus" + csn + " = null;\n");
            sb.Append("						bus" + csn + " = new Bus" + csn + "(conn, _config);\n");
            sb.Append("						bus" + csn + ".Delete(" + PascalCase(dbn) + ");\n");
            sb.Append("						// close the db connection\n");
            sb.Append("						bConn = CloseConnection(conn);\n");
            sb.Append("						_response.ResultData = " + PascalCase(dbn) + ".ToXml();\n");
            sb.Append("						if ( bus" + csn + ".HasError )\n");
            sb.Append("						{\n");
            sb.Append("							// error\n");
            sb.Append("							ErrorCode error = new ErrorCode();\n");
            sb.Append("							_response.ErrorArray.Add( error );\n");
            //sb.Append("							_log(\"ERROR\", error.ToString());\n");
            sb.Append("						}\n");
            sb.Append("					}\n");
            sb.Append("					else\n");
            sb.Append("					{\n");
            sb.Append("						// problem in opening the connection\n");
            sb.Append("						// error\n");
            sb.Append("						ErrorCode error = new ErrorCode();\n");
            sb.Append("						_response.ErrorArray.Add( error );\n");
            //sb.Append("						_log(\"ERROR\", error.ToString());\n");
            sb.Append("\n");
            sb.Append("					}\n");
            sb.Append("					conn = null;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// error has occured, include in result output\n");
            sb.Append("				if ( " + PascalCase(dbn) + " != null )\n");
            sb.Append("				{\n");
            sb.Append("					// error\n");
            sb.Append("					ErrorCode error = new ErrorCode();\n");
            sb.Append("					_response.ErrorArray.Add( error );\n");
            //sb.Append("					_log(\"ERROR\", error.ToString());\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		public void " + csn + "Get(string pSerializedObject)\n");
            sb.Append("		{\n");
            //sb.Append("			_log(\"GET\", \"Received get request:  \" + pSerializedObject);\n");
            sb.Append("\n");
            sb.Append("			// create a new object\n");
            sb.Append("			" + NS(csn) + " " + PascalCase(dbn) + " = null;\n");
            sb.Append("			" + PascalCase(dbn) + " = new " + NS(csn) + "(_config);\n");
            sb.Append("			// deserialize the input\n");
            sb.Append("			" + PascalCase(dbn) + ".Parse( pSerializedObject );\n");
            sb.Append("\n");
            sb.Append("			if ( !" + PascalCase(dbn) + ".HasError ) \n");
            sb.Append("			{\n");
            sb.Append("				SqlConnection conn = GetConnection();\n");
            sb.Append("				if ( conn != null )\n");
            sb.Append("				{\n");
            sb.Append("					bool bConn = false;\n");
            sb.Append("\n");
            sb.Append("					bConn = OpenConnection(conn);\n");
            sb.Append("					if ( bConn )\n");
            sb.Append("					{\n");
            sb.Append("						Bus" + csn + " bus" + csn + " = null;\n");
            sb.Append("						bus" + csn + " = new Bus" + csn + "(conn, _config);\n");
            sb.Append("						bus" + csn + ".Load(" + PascalCase(dbn) + ");\n");
            sb.Append("						// close the db connection\n");
            sb.Append("						bConn = CloseConnection(conn);\n");
            sb.Append("						_response.ResultData = " + PascalCase(dbn) + ".ToXml();\n");
            sb.Append("						if ( bus" + csn + ".HasError )\n");
            sb.Append("						{\n");
            sb.Append("							// error\n");
            sb.Append("							ErrorCode error = new ErrorCode();\n");
            sb.Append("							_response.ErrorArray.Add( error );\n");
            //sb.Append("							_log(\"ERROR\", error.ToString());\n");
            sb.Append("						}\n");
            sb.Append("					}\n");
            sb.Append("					else\n");
            sb.Append("					{\n");
            sb.Append("						// problem in opening the connection\n");
            sb.Append("						// error\n");
            sb.Append("						ErrorCode error = new ErrorCode();\n");
            sb.Append("						_response.ErrorArray.Add( error );\n");
            //sb.Append("						_log(\"ERROR\", error.ToString());\n");
            sb.Append("					}\n");
            sb.Append("					conn = null;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// error has occured, include in result output\n");
            sb.Append("				if ( " + PascalCase(dbn) + " != null )\n");
            sb.Append("				{\n");
            sb.Append("					// error\n");
            sb.Append("					ErrorCode error = new ErrorCode();\n");
            sb.Append("					_response.ErrorArray.Add( error );\n");
            //sb.Append("     _log(\"ERROR\", error.ToString());\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		public void " + csn + "GetList(string pSerializedObject)\n");
            sb.Append("		{\n");
            sb.Append("			_log(\"GET\", \"Received get list request:  \" + pSerializedObject);\n");
            sb.Append("\n");
            sb.Append("			// create a new object\n");
            sb.Append("			Enum" + csn + " enum" + csn + " = null;\n");
            sb.Append("			enum" + csn + " = new Enum" + csn + "();\n");
            sb.Append("			// deserialize the input\n");
            sb.Append("			enum" + csn + ".Parse( pSerializedObject );\n");
            sb.Append("			if ( !enum" + csn + ".HasError ) \n");
            sb.Append("			{\n");
            sb.Append("				SqlConnection conn = GetConnection();\n");
            sb.Append("				if ( conn != null )\n");
            sb.Append("				{\n");
            sb.Append("					bool bConn = false;\n");
            sb.Append("\n");
            sb.Append("					bConn = OpenConnection(conn);\n");
            sb.Append("					if ( bConn )\n");
            sb.Append("					{\n");
            sb.Append("						ArrayList " + PascalCase(dbn) + "s = null;\n");
            sb.Append("						Bus" + csn + " bus" + csn + " = null;\n");
            sb.Append("						bus" + csn + " = new Bus" + csn + "(conn, _config);\n");
            sb.Append("						" + PascalCase(dbn) + "s = bus" + csn + ".Get(enum" + csn + ");\n");
            sb.Append("						StringBuilder sbResultData = null;\n");
            sb.Append("						sbResultData = new StringBuilder();\n");
            sb.Append("						sbResultData.Append(\"<Count>\" + " + PascalCase(dbn) + "s.Count.ToString() + \"</Count>\");\n");
            sb.Append("						foreach (" + NS(csn) + " item in " + PascalCase(dbn) + "s)\n");
            sb.Append("						{\n");
            sb.Append("							sbResultData.Append(item.ToXml() );\n");
            sb.Append("						}\n");
            sb.Append("						// close the db connection\n");
            sb.Append("						bConn = CloseConnection(conn);\n");
            sb.Append("						_response.ResultData = sbResultData.ToString();\n");
            sb.Append("						sbResultData = null;\n");
            sb.Append("						if ( bus" + csn + ".HasError )\n");
            sb.Append("						{\n");
            sb.Append("							// error\n");
            sb.Append("							ErrorCode error = new ErrorCode();\n");
            sb.Append("							_response.ErrorArray.Add( error );\n");
            //sb.Append("							_log(\"ERROR\", error.ToString());\n");
            sb.Append("						}\n");
            sb.Append("					}\n");
            sb.Append("					else\n");
            sb.Append("					{\n");
            sb.Append("						// problem in opening the connection\n");
            sb.Append("						// error\n");
            sb.Append("						ErrorCode error = new ErrorCode();\n");
            sb.Append("						_response.ErrorArray.Add( error );\n");
            //sb.Append("						_log(\"ERROR\", error.ToString());\n");
            sb.Append("					}\n");
            sb.Append("					conn = null;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// error has occured, include in result output\n");
            sb.Append("				if ( enum" + csn + " != null )\n");
            sb.Append("				{\n");
            sb.Append("					// error\n");
            sb.Append("					ErrorCode error = new ErrorCode();\n");
            sb.Append("					_response.ErrorArray.Add( error );\n");
            //sb.Append("     _log(\"ERROR\", error.ToString());\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("\n");
            sb.Append("		public void " + csn + "Check(string pSerializedObject)\n");
            sb.Append("		{\n");
            //sb.Append("			_log(\"GET\", \"Received validate request:  \" + pSerializedObject);\n");
            sb.Append("\n");
            sb.Append("			// create a new object\n");
            sb.Append("			" + NS(csn) + " " + PascalCase(dbn) + " = null;\n");
            sb.Append("			" + PascalCase(dbn) + " = new " + NS(csn) + "(_config);\n");
            sb.Append("			// deserialize the input\n");
            sb.Append("			" + PascalCase(dbn) + ".Parse( pSerializedObject );\n");
            sb.Append("\n");
            sb.Append("			if ( !" + PascalCase(dbn) + ".HasError ) \n");
            sb.Append("			{\n");
            sb.Append("				Bus" + csn + " bus" + csn + " = null;\n");
            sb.Append("				bus" + csn + " = new Bus" + csn + "();\n");
            sb.Append("				bool bIsValid = false;\n");
            sb.Append("				bIsValid = bus" + csn + ".IsValid(" + PascalCase(dbn) + ");\n");
            sb.Append("				if ( !bIsValid )\n");
            sb.Append("				{\n");
            sb.Append("					_response.ColumnErrors = bus" + csn + ".ColumnErrors;\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("			else\n");
            sb.Append("			{\n");
            sb.Append("				// error has occured, include in result output\n");
            sb.Append("				if ( " + PascalCase(dbn) + " != null )\n");
            sb.Append("				{\n");
            sb.Append("					// error\n");
            sb.Append("					ErrorCode error = new ErrorCode();\n");
            sb.Append("					_response.ErrorArray.Add( error );\n");
            //sb.Append("     _log(\"ERROR\", error.ToString());\n");
            sb.Append("				}\n");
            sb.Append("			}\n");
            sb.Append("\n");
            sb.Append("		}\n");
            sb.Append("\n");

            return sb.ToString();
        }

        /// <summary>
        /// Grants the selected permission.
        /// </summary>
        /// <param name="pStrUsername">The p STR username.</param>
        private void grantSelectedPermission(string pStrUsername)
        {
            try
            {
                String strSpName = null;
                String strGrantLine = null;
                string tableName = null;
                foreach (string li in lstTables.SelectedItems)
                {
                    tableName = CamCase(li);
                    strSpName = "sp" + tableName + "Load";
                    strGrantLine = "GRANT EXECUTE ON " + strSpName + " TO " + pStrUsername + "\r\n" + "GO \r\n";
                    txtOutput.AppendText(strGrantLine);

                    strSpName = "sp" + tableName + "Update";
                    strGrantLine = "GRANT EXECUTE ON " + strSpName + " TO " + pStrUsername + "\r\n" + "GO \r\n";
                    txtOutput.AppendText(strGrantLine);

                    strSpName = "sp" + tableName + "Exist";
                    strGrantLine = "GRANT EXECUTE ON " + strSpName + " TO " + pStrUsername + "\r\n" + "GO \r\n";
                    txtOutput.AppendText(strGrantLine);

                    strSpName = "sp" + tableName + "Enum";
                    strGrantLine = "GRANT EXECUTE ON " + strSpName + " TO " + pStrUsername + "\r\n" + "GO \r\n";
                    txtOutput.AppendText(strGrantLine);

                    strSpName = "sp" + tableName + "Delete";
                    strGrantLine = "GRANT EXECUTE ON " + strSpName + " TO " + pStrUsername + "\r\n" + "GO \r\n";
                    txtOutput.AppendText(strGrantLine);

                    strSpName = "sp" + tableName + "Insert";
                    strGrantLine = "GRANT EXECUTE ON " + strSpName + " TO " + pStrUsername + "\r\n" + "GO \r\n";
                    txtOutput.AppendText(strGrantLine);
                }
            }
            catch (Exception ee)
            {
                lstTables.Items.Add("!! ERROR !!");
                MessageBox.Show(ee.Message);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Grants the permission.
        /// </summary>
        /// <param name="pStrUsername">The p STR username.</param>
        private void grantPermission(String pStrUsername)
        {
            System.Data.SqlClient.SqlConnection cnConn = null;
            try
            {
                String strSQL = null;

                strSQL = "SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
                //strSQL = "SELECT * FROM sys.objects WHERE objectproperty( object_id, N'IsMSShipped' ) = 0 " +
                //        "AND objectproperty( object_id, N'IsProcedure' ) = 1";
                cnConn = new System.Data.SqlClient.SqlConnection();
                cnConn.ConnectionString = txtCS.Text;
                //SqlCommand cmdSP = new SqlCommand("select [name] from sysobjects where type = 'P' ORDER BY [name] asc");
                SqlCommand cmdSP = new SqlCommand(strSQL);
                cmdSP.CommandType = CommandType.Text;
                cmdSP.Connection = cnConn;
                cnConn.Open();
                SqlDataReader rdr = cmdSP.ExecuteReader();
                String strSpName = null;
                String strGrantLine = null;
                //lstTables.Items.Clear();
                while (rdr.Read())
                {
                    strSpName = rdr["SPECIFIC_NAME"].ToString();
                    strGrantLine = "GRANT EXECUTE ON " + strSpName + " TO " + pStrUsername + "\r\n" + "GO \r\n";
                    txtOutput.AppendText(strGrantLine);
                    //lstTables.Items.Add(strSpName);
                }
            }
            catch (Exception ee)
            {
                lstTables.Items.Add("!! ERROR !!");
                MessageBox.Show(ee.Message);
            }
            finally
            {
                if (cnConn != null)
                {
                    cnConn.Close();
                }
                cnConn = null;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnGrant control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnGrant_Click(object sender, EventArgs e)
        {
            if (this.grantTextBox.Text != String.Empty)
            {
                grantPermission(this.grantTextBox.Text.Trim());
            }
        }

        /// <summary>
        /// Handles the Click event of the allSPButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void allSPButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the clearDBButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void clearDBButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string li in lstTables.SelectedItems)
            {
                sb.Append("DELETE FROM " + li + ";\n");
            }
            txtOutput.Text = sb.ToString();
        }

        /// <summary>
        /// Handles the Click event of the grantSelectedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void grantSelectedButton_Click(object sender, EventArgs e)
        {
            if (this.grantTextBox.Text != String.Empty)
            {
                grantSelectedPermission(this.grantTextBox.Text.Trim());
            }
        }

        /// <summary>
        /// Handles the Created event of the mainFileSystemWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        private void mainFileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
        }

        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        private void moveFile(FileSystemEventArgs e)
        {
            try
            {
                string path = e.FullPath;
                string destFilepath = this.solutionDirPathTextBox.Text.Trim() + Path.DirectorySeparatorChar + currentSolDirName + Path.DirectorySeparatorChar + e.Name;

                if (!File.Exists(path))
                {
                    // This statement ensures that the file is created,
                    // but the handle is not kept.
                    using (FileStream fs = File.Create(path)) { }
                }

                // Ensure that the target does not exist.
                if (File.Exists(destFilepath))
                    File.Delete(destFilepath);

                // Move the file.
                File.Move(path, destFilepath);
                MessageBox.Show(String.Format("{0} was moved to {1}.", path, destFilepath));

                // See if the original exists now.
                //if (File.Exists(path))
                //{
                //    MessageBox.Show("The original file still exists, which is unexpected.");
                //}
                //else
                //{
                //    MessageBox.Show("The original file no longer exists, which is expected.");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Changed event of the mainFileSystemWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        private void mainFileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            moveFile(e);
        }

        /// <summary>
        /// Builds the biz val exp class.
        /// </summary>
        /// <returns></returns>
        private string BuildBizValExpClass()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace Vetapp.BusinessFacadeLayer");
            sb.AppendLine("{");
            sb.AppendLine("    class BusValidationExpressions");
            sb.AppendLine("    {");
            sb.AppendLine("        // Looks for any character in an entire string that has a character outside the specified Ascii range ");
            sb.AppendLine("        // of 0x20 - 0x126 and is not a tab or a new line character. The pattern checks from the beginning of ");
            sb.AppendLine("        // the string to the end.");
            sb.AppendLine(
                "        // \"This string will not match (which is a valid case) because all the characters are in the specified range.");
            sb.AppendLine(
                "        // \"This string will match because the backspace character is (not valid) for the TEXT type validation pattern. \\b\"");
            sb.AppendLine("        public const string REGEX_TYPE_PATTERN_TEXT = @\"^([^ -~\\t\\r\\n])$\";");
            sb.AppendLine("");
            sb.AppendLine(
                "        // Looks for any character outside the specified range of A to Z upper and lower case letters ");
            sb.AppendLine("        // and the number range 0 to 9, tabs, returns and linefeeds or for strings over 255 characters long.");
            sb.AppendLine("        // The pattern checks from the beginning of the string to the end.");
            sb.AppendLine(
                "        // \"This string will not match (which is a valid case) because the ; is not in the specified range.\"");
            sb.AppendLine(
                "        // \"This string will match which is an invalid case because all characters are in the specified range\"");
            sb.AppendLine("        public const string REGEX_TYPE_PATTERN_NVARCHAR255 = @\"^([^a-zA-Z0-9\\t\\r\\n]{1,255}|.{256,257})$\";");
            sb.AppendLine("");
            sb.AppendLine(
                "        // Looks for a match of one to ten digits from the beginning of the string to the end.");
            sb.AppendLine("        // \"0123456789\" Successful match case, is a valid case.");
            sb.AppendLine("        // \"12.4\" Does not match, is invalid case.");
            sb.AppendLine("        // \"2310\" Successful match case, is a valid case.");
            sb.AppendLine("        // \"0x20\" Does not match, is invalid case.");
            sb.AppendLine("        public const string REGEX_TYPE_PATTERN_NUMERIC10 = @\"^(\\d{1,10})$\";");
            sb.AppendLine("");
            sb.AppendLine("        // Looks for a single \"0\" or \"1\" in the string. No other characters are allowed.");
            sb.AppendLine("        //\"1\" Successful match case, is valid case.");
            sb.AppendLine("        //\"0\" Successful match case, is valid case.");
            sb.AppendLine("        //\"10\" Does not match, is invalid case.");
            sb.AppendLine("        //\"0110\" Does not match, is invalid case.");
            sb.AppendLine("        //\"\" Does not match, is invalid case.");
            sb.AppendLine("        //\"A\" Does not match, is invalid case.");
            sb.AppendLine("        public const string REGEX_TYPE_PATTERN_BIT = @\"^(0|1)$\";");
            sb.AppendLine("");
            sb.AppendLine(
                "        // Looks for 0 to 1 negative signs, followed by one to 13 digits from the beginning of ");
            sb.AppendLine("        // the string to the end.");
            sb.AppendLine("        //\"0123456789012\" Successful match case, is valid case.");
            sb.AppendLine("        //\"0\" Successful match case, is valid case.");
            sb.AppendLine("        //\"-2013\" Successful match case, is valid case.");
            sb.AppendLine("        //\"1.2\" Does not match, is invalid case.");
            sb.AppendLine("        //\"\" Does not match, is invalid case.");
            sb.AppendLine("        //\"01234567890123\" Does not match, is invalid case.");
            sb.AppendLine("        //\"A\" Does not match, is invalid case.");
            sb.AppendLine("        //\"-\" Does not match, is invalid case.");
            sb.AppendLine("        public const string REGEX_TYPE_PATTERN_INT = @\"^-{0,1}\\d{1,13}$\";");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void btnFacCrud_Click(object sender, EventArgs e)
        {
            string templatePath = @"Templates/FacCoreTemplate.txt";
            string replacePattern1 = "<<REPLACE_1>>";
            string replacePattern2 = "<<REPLACE_2>>";
            string data = null;
            string template = File.ReadAllText(templatePath);
            if (lstTables.SelectedItems.Count == 0)
                MessageBox.Show("You have not selected any tables!");
            string objName = null;
            foreach (string li in lstTables.SelectedItems)
            {
                string csn = CamCase(li);
                data = template.Replace(replacePattern1, csn);
                data = data.Replace(replacePattern2, li.ToLower());
                this.txtOutput.AppendText(data + "\r");
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            string path = @"Data\";
            string fullFileName = path + cboDictionary.SelectedItem.ToString();

            if (File.Exists(fullFileName))
            {
                switch (cboDictionary.SelectedItem.ToString())
                {
                    case "timeWords0.txt":
                        //CreateSchemaTimeWords();
                        CreateInsertsTimeWords(fullFileName);
                        break;

                    case "badShortCuts0.txt":
                        //CreateSchemaBadShortCuts();
                        CreateInsertsBadShortCuts(fullFileName);
                        break;

                    case "realEstateTerms0.txt":
                        //CreateSchemaRealEstateTerms();
                        CreateInsertsRealEstateTerms(fullFileName);
                        break;

                    case "realEstateAdjectives0.txt":
                        //CreateSchemaRealestateAdjectves();
                        CreateInsertsRealEstateAdjectives(fullFileName);
                        break;

                    case "prepositions0.txt":
                        //CreateSchemaPrepositions();
                        CreateInsertsPrepositions(fullFileName);
                        break;

                    case "generalAdjectives0.txt":
                        //CreateSchemaGeneralAdjectives();
                        CreateInsertsGeneralAdjectives(fullFileName);
                        break;

                    case "conjunctions0.txt":
                        //CreateSchemaConjunctions();
                        CreateInsertsConjunctions(fullFileName);
                        break;

                    case "badDictionary0.txt":
                        //CreateSchemaBadDictionary();
                        CreateInsertsBadDictionary(fullFileName);
                        break;

                    case "numbersWords0.txt":
                        //CreateSchemaNumberWords();
                        CreateInsertsNumberWords(fullFileName);
                        break;
                }
            }
        }

        private void CreateSchemaTimeWords()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_time')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_time'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_time\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_time\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_time_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_time') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_time >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_time >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaBadShortCuts()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_negative_shortcuts')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_negative_shortcuts'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_negative_shortcuts\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_negative_shortcuts\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_negative_shortcuts_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_negative_shortcuts') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_negative_shortcuts >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_negative_shortcuts >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaRealEstateTerms()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_realestate')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_realestate'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_realestate\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_realestate\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_realestate_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_realestate') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_realestate >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_realestate >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaRealestateAdjectves()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_realestate_adjectives')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_realestate_adjectives'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_realestate_adjectives\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_realestate_adjectives\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_realestate_adjectives_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_realestate_adjectives') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_realestate_adjectives >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_realestate_adjectives >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaPrepositions()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_prepositions')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_prepositions'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_prepositions\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_prepositions\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_prepositions_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_prepositions') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_prepositions >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_prepositions >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaGeneralAdjectives()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_general_adjectives')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_general_adjectives'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_general_adjectives\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_general_adjectives\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_general_adjectives_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_general_adjectives') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_general_adjectives >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_general_adjectives >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaConjunctions()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_conjunctions')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_conjunctions'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_conjunctions\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_conjunctions\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_conjunctions_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_conjunctions') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_conjunctions >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_conjunctions >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaBadDictionary()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_negative')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_negative'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_negative\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_negative\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_negative_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_negative') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_negative >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_negative >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateSchemaNumberWords()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("/*******************************************************************************\n");
            sb.Append("**       Change History\n");
            sb.Append("*******************************************************************************\n");
            sb.Append("**       Date:       Author:     Description:\n");
            sb.Append("**       " + DateTime.UtcNow.ToShortDateString() + "        BH      Created\n");
            sb.Append("*******************************************************************************/\n");
            sb.Append("IF EXISTS (SELECT *\n");
            sb.Append("           FROM   sysobjects\n");
            sb.Append("           WHERE  type = 'U'\n");
            sb.Append("                  AND name = 'analysis_dictionary_number_words')\n");
            sb.Append("  BEGIN\n");
            sb.Append("      PRINT 'Dropping Table analysis_dictionary_number_words'\n");
            sb.Append("\n");
            sb.Append("      DROP TABLE analysis_dictionary_number_words\n");
            sb.Append("  END\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("CREATE TABLE analysis_dictionary_number_words\n");
            sb.Append("  (\n");
            sb.Append("     analysis_dictionary_number_words_id NUMERIC(10) NOT NULL PRIMARY KEY,\n");
            sb.Append("     date_created            DATETIME NULL,\n");
            sb.Append("     date_modified           DATETIME NULL,\n");
            sb.Append("     phrase                 NVARCHAR(255) NULL\n");
            sb.Append("  )\n");
            sb.Append("\n");
            sb.Append("go\n");
            sb.Append("\n");
            sb.Append("IF Object_id('analysis_dictionary_number_words') IS NOT NULL\n");
            sb.Append("  PRINT '<<< CREATED TABLE analysis_dictionary_number_words >>>'\n");
            sb.Append("ELSE\n");
            sb.Append("  PRINT '<<< FAILED CREATING TABLE analysis_dictionary_number_words >>>'\n");
            sb.Append("\n");
            sb.Append("go\n");
            txtOutput.Text = sb.ToString();
        }

        private void CreateInsertsTimeWords(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_time (analysis_dictionary_time_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsBadShortCuts(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_negative_shortcut (analysis_dictionary_negative_shortcut_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsRealEstateTerms(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_realestate_phrase (analysis_dictionary_realestate_phrase_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsRealEstateAdjectives(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_realestate_adjective (analysis_dictionary_realestate_adjective_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsPrepositions(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_preposition (analysis_dictionary_preposition_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsGeneralAdjectives(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_general_adjective (analysis_dictionary_general_adjective_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsConjunctions(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_conjunction (analysis_dictionary_conjunction_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsBadDictionary(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_negative_phrase (analysis_dictionary_negative_phrase_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void CreateInsertsNumberWords(string fullFileName)
        {
            StringBuilder sb = new StringBuilder();
            List<string> terms = new List<string>();
            int i = 1;

            foreach (string line in File.ReadLines(fullFileName))
            {
                if (!terms.Contains(line))
                {
                    sb.Append(
                        "INSERT INTO analysis_dictionary_numberword (analysis_dictionary_numberword_id, date_created, phrase) values(" +
                        i + ", GetDate(), '" + line.Replace("'", "''") + "')\n");
                    terms.Add(line);
                    i += 1;
                }
            }

            txtOutput.Text = sb + "\nGO\n";
        }

        private void btnKeysSqlGenerate_Click(object sender, EventArgs e)
        {
        }


        private void txtCS_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnKeysGnerate_Click(object sender, EventArgs e)
        {

        }

        private void getSQLProdDBButton_Click(object sender, EventArgs e)
        {

        }

        private void genPDFArraysButton_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = @"C:\MyData\Vet App\PDFMappings\neck-mappings.txt";
                using (StreamReader sr = new StreamReader(filepath))
                {
                    String line;
                    StringBuilder sb = new StringBuilder();
                    string s = null;
                    string key1 = "Export Value: ";
                    while ((line = sr.ReadLine()) != null)
                    {
                        // 1.  form1[0].#subform[0].Diagnoses11[0] - CheckBox - Export Value: 1
                        // 2.  form1[0].#subform[0].DateOfDiagnosis11[0] - TextField
                        // form1[0].#subform[0].ICDCode9[0] - TextField
                        // { 1, new PDFItem {Code="form1[0].#subform[0].Diagnoses11[0]", ExportValue="1", ItemType="CheckBox" }},
                        // { <<1>>, new PDFItem { Code = "<<2>>", ExportValue = "<<3>>", ItemType = "<<4>>" }},
                        s = "{ <<1>>, new PDFItem { Code = \"<<2>>\", ExportValue = \"<<3>>\", ItemType = \"<<4>>\" }},";
                        int ind1 = line.IndexOf(".");
                        if (ind1 != -1)
                        {
                            s = s.Replace("<<1>>", line.Substring(0, ind1));
                        }

                        int ind2 = line.IndexOf("-", ind1);
                        if (ind2 != -1)
                        {
                            s = s.Replace("<<2>>", line.Substring(ind1 + 1, ind2 - ind1 - 1).Trim());
                        }

                        int ind3 = line.IndexOf(key1, ind2);
                        if (ind3 != -1)
                        {
                            s = s.Replace("<<3>>", line.Substring(ind3 + key1.Length).Trim());
                        }
                        else
                        {
                            s = s.Replace("<<3>>", string.Empty);
                        }

                        int ind4 = line.IndexOf("CheckBox");
                        if (ind4 != -1)
                        {
                            s = s.Replace("<<4>>", "CheckBox");
                        }

                        int ind5 = line.IndexOf("TextField");
                        if (ind5 != -1)
                        {
                            s = s.Replace("<<4>>", "TextField");
                        }
                        sb.Append(s + Environment.NewLine);
                    }
                    txtOutput.AppendText(sb.ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    [DelimitedRecord(",")]
    public class ZipCSVData
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Zip;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string type;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PrimaryCity;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string State;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string AreaCodes;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Latitude;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Longitude;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string EstimatedPopulation;
    }

    [DelimitedRecord(",")]
    public class ZipCSVData2
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PostalCode;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PrimaryRecord;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Population;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string HouseholdsPerZipCode;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string WhitePopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string BlackPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string HispanicPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string AsianPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string HawaiianPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string IndianPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string OtherPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string MalePopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string FemalePopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PersonsPerHousehold;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string AverageHouseValue;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string IncomePerHousehold;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string MedianAge;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string MedianAgeMale;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string MedianAgeFemale;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Latitude;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Longitude;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Elevation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string State;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string StateFullName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityType;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityAliasAbbreviation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string AreaCodes;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PrimaryCity;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityAliasName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string County;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CountyFips;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string StateFips;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string TimeZone;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string DayLightSaving;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Msa;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Pmsa;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Csa;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Cbsa;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CbsaDiv;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CbsaType;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CbsaName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string MsaName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PmsaName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Region;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Division;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string MailingName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string NumberOfBusinesses;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string NumberOfEmployees;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string BusinessFirstQuarterPayroll;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string BusinessAnnualPayroll;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string BusinessEmploymentFlag;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string GrowthRank;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string GrowingCountiesA;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string GrowingCountiesB;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string GrowthincreaseNumber;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string GrowthIncreasePercentage;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CbsaPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CbsaDivisionPopulation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CongressionalDistrict;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CongressionalLandArea;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string DeliveryResidential;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string DeliveryBusiness;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string DeliveryTotal;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PreferredLastLineKey;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string ClassificationCode;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string MultiCounty;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CsaName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CbsaDivName;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityStateKey;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string PopulationEstimate;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string LandArea;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string WaterArea;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityAliasCode;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityMixedCase;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityAliasMixedCase;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string BoxCount;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Sfdu;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string Mfdu;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string StateAnsi;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CountyAnsi;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string ZipIntroDate;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string AliasIntroDate;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string FacilityCode;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CityDeliveryIndicator;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string CarrierRouteRateSortation;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string FinanceNumber;

        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string UniqueZipName;
    }
}