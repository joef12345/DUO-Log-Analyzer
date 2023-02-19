
namespace Duo_Log_Analyzer
{
    partial class FormSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxHideDuoSKey = new System.Windows.Forms.CheckBox();
            this.textBoxDuoHostName = new System.Windows.Forms.TextBox();
            this.textBoxDuoSKey = new System.Windows.Forms.TextBox();
            this.textBoxDuoiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.checkBoxHideIPWHOISKey = new System.Windows.Forms.CheckBox();
            this.textBoxIPWhoIsAPIKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.checkBoxHideAWSSecretKey = new System.Windows.Forms.CheckBox();
            this.textBoxAWSSecretAccessKey = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAWSAccessCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAWSSNSARN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panelRegEx = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRegExTest = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxRegEx = new System.Windows.Forms.TextBox();
            this.checkBoxUnenrolledUser = new System.Windows.Forms.CheckBox();
            this.checkBoxUserInBypass = new System.Windows.Forms.CheckBox();
            this.checkBoxRegExFilter = new System.Windows.Forms.CheckBox();
            this.checkBoxCountryEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxSuspiciousLocation = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxIgnoreIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.listBoxIgnoreIP = new System.Windows.Forms.ListBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panelGeoAlerts = new System.Windows.Forms.Panel();
            this.checkBoxGeoAlerts = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownDistance = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBoxCountryCode = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.panelOutsideCountry = new System.Windows.Forms.Panel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.radioButtonPrivateRelayTag = new System.Windows.Forms.RadioButton();
            this.radioButtonPrivateRelayIgnore = new System.Windows.Forms.RadioButton();
            this.checkBoxPrivateRelay = new System.Windows.Forms.CheckBox();
            this.panelPrivateRelay = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerClearStatus = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxIgnoreUser = new System.Windows.Forms.TextBox();
            this.listBoxIgnoreUser = new System.Windows.Forms.ListBox();
            this.panelIgnoreIPList = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelRegEx.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panelGeoAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).BeginInit();
            this.panelOutsideCountry.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panelPrivateRelay.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelIgnoreIPList.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxHideDuoSKey);
            this.groupBox1.Controls.Add(this.textBoxDuoHostName);
            this.groupBox1.Controls.Add(this.textBoxDuoSKey);
            this.groupBox1.Controls.Add(this.textBoxDuoiKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DUO API Settings";
            // 
            // checkBoxHideDuoSKey
            // 
            this.checkBoxHideDuoSKey.AutoSize = true;
            this.checkBoxHideDuoSKey.Checked = true;
            this.checkBoxHideDuoSKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideDuoSKey.Location = new System.Drawing.Point(370, 66);
            this.checkBoxHideDuoSKey.Name = "checkBoxHideDuoSKey";
            this.checkBoxHideDuoSKey.Size = new System.Drawing.Size(15, 14);
            this.checkBoxHideDuoSKey.TabIndex = 6;
            this.checkBoxHideDuoSKey.UseVisualStyleBackColor = true;
            this.checkBoxHideDuoSKey.CheckedChanged += new System.EventHandler(this.checkBoxHideDuoSKey_CheckedChanged);
            // 
            // textBoxDuoHostName
            // 
            this.textBoxDuoHostName.Location = new System.Drawing.Point(127, 97);
            this.textBoxDuoHostName.Name = "textBoxDuoHostName";
            this.textBoxDuoHostName.Size = new System.Drawing.Size(251, 20);
            this.textBoxDuoHostName.TabIndex = 5;
            // 
            // textBoxDuoSKey
            // 
            this.textBoxDuoSKey.Location = new System.Drawing.Point(127, 63);
            this.textBoxDuoSKey.Name = "textBoxDuoSKey";
            this.textBoxDuoSKey.Size = new System.Drawing.Size(237, 20);
            this.textBoxDuoSKey.TabIndex = 4;
            this.textBoxDuoSKey.UseSystemPasswordChar = true;
            // 
            // textBoxDuoiKey
            // 
            this.textBoxDuoiKey.Location = new System.Drawing.Point(127, 32);
            this.textBoxDuoiKey.Name = "textBoxDuoiKey";
            this.textBoxDuoiKey.Size = new System.Drawing.Size(251, 20);
            this.textBoxDuoiKey.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "API Hostname";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Secret key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Integration key";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.checkBoxHideIPWHOISKey);
            this.groupBox2.Controls.Add(this.textBoxIPWhoIsAPIKey);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 114);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IpWhoIs.IO Settings";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(128, 58);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(77, 23);
            this.button7.TabIndex = 3;
            this.button7.Text = "Test";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // checkBoxHideIPWHOISKey
            // 
            this.checkBoxHideIPWHOISKey.AutoSize = true;
            this.checkBoxHideIPWHOISKey.Checked = true;
            this.checkBoxHideIPWHOISKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideIPWHOISKey.Location = new System.Drawing.Point(370, 34);
            this.checkBoxHideIPWHOISKey.Name = "checkBoxHideIPWHOISKey";
            this.checkBoxHideIPWHOISKey.Size = new System.Drawing.Size(15, 14);
            this.checkBoxHideIPWHOISKey.TabIndex = 2;
            this.checkBoxHideIPWHOISKey.UseVisualStyleBackColor = true;
            this.checkBoxHideIPWHOISKey.CheckedChanged += new System.EventHandler(this.checkBoxHideIPWHOISKey_CheckedChanged);
            // 
            // textBoxIPWhoIsAPIKey
            // 
            this.textBoxIPWhoIsAPIKey.Location = new System.Drawing.Point(127, 32);
            this.textBoxIPWhoIsAPIKey.Name = "textBoxIPWhoIsAPIKey";
            this.textBoxIPWhoIsAPIKey.Size = new System.Drawing.Size(237, 20);
            this.textBoxIPWhoIsAPIKey.TabIndex = 1;
            this.textBoxIPWhoIsAPIKey.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "API Key";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.checkBoxHideAWSSecretKey);
            this.groupBox3.Controls.Add(this.textBoxAWSSecretAccessKey);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBoxAWSAccessCode);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxAWSSNSARN);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(13, 277);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 177);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AWS SNS Settings";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(127, 121);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 23);
            this.button6.TabIndex = 7;
            this.button6.Text = "Send Test Message";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkBoxHideAWSSecretKey
            // 
            this.checkBoxHideAWSSecretKey.AutoSize = true;
            this.checkBoxHideAWSSecretKey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxHideAWSSecretKey.Checked = true;
            this.checkBoxHideAWSSecretKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideAWSSecretKey.Location = new System.Drawing.Point(370, 97);
            this.checkBoxHideAWSSecretKey.Name = "checkBoxHideAWSSecretKey";
            this.checkBoxHideAWSSecretKey.Size = new System.Drawing.Size(15, 14);
            this.checkBoxHideAWSSecretKey.TabIndex = 6;
            this.checkBoxHideAWSSecretKey.UseVisualStyleBackColor = true;
            this.checkBoxHideAWSSecretKey.CheckedChanged += new System.EventHandler(this.checkBoxHideAWSSecretKey_CheckedChanged);
            // 
            // textBoxAWSSecretAccessKey
            // 
            this.textBoxAWSSecretAccessKey.Location = new System.Drawing.Point(127, 95);
            this.textBoxAWSSecretAccessKey.Name = "textBoxAWSSecretAccessKey";
            this.textBoxAWSSecretAccessKey.Size = new System.Drawing.Size(237, 20);
            this.textBoxAWSSecretAccessKey.TabIndex = 5;
            this.textBoxAWSSecretAccessKey.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Secret Access Key";
            // 
            // textBoxAWSAccessCode
            // 
            this.textBoxAWSAccessCode.Location = new System.Drawing.Point(127, 60);
            this.textBoxAWSAccessCode.Name = "textBoxAWSAccessCode";
            this.textBoxAWSAccessCode.Size = new System.Drawing.Size(251, 20);
            this.textBoxAWSAccessCode.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Access Code";
            // 
            // textBoxAWSSNSARN
            // 
            this.textBoxAWSSNSARN.Location = new System.Drawing.Point(127, 29);
            this.textBoxAWSSNSARN.Name = "textBoxAWSSNSARN";
            this.textBoxAWSSNSARN.Size = new System.Drawing.Size(251, 20);
            this.textBoxAWSSNSARN.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "SNS Topic ARN";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tabControl1);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.textBoxIgnoreIP);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.listBoxIgnoreIP);
            this.groupBox4.Location = new System.Drawing.Point(419, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(378, 441);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Alert Settings";
            // 
            // panelRegEx
            // 
            this.panelRegEx.Controls.Add(this.label11);
            this.panelRegEx.Controls.Add(this.label10);
            this.panelRegEx.Controls.Add(this.textBoxRegExTest);
            this.panelRegEx.Controls.Add(this.button5);
            this.panelRegEx.Controls.Add(this.textBoxRegEx);
            this.panelRegEx.Enabled = false;
            this.panelRegEx.Location = new System.Drawing.Point(1, 115);
            this.panelRegEx.Name = "panelRegEx";
            this.panelRegEx.Size = new System.Drawing.Size(339, 68);
            this.panelRegEx.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Test User Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Regex Expression";
            this.toolTip1.SetToolTip(this.label10, "Enter Regular expression to filter User Names. For Example, [0-9]+ would filter u" +
        "sernames that only contain numbers.");
            // 
            // textBoxRegExTest
            // 
            this.textBoxRegExTest.Location = new System.Drawing.Point(111, 36);
            this.textBoxRegExTest.Name = "textBoxRegExTest";
            this.textBoxRegExTest.Size = new System.Drawing.Size(157, 20);
            this.textBoxRegExTest.TabIndex = 17;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(274, 36);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(62, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Test";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // textBoxRegEx
            // 
            this.textBoxRegEx.Location = new System.Drawing.Point(111, 11);
            this.textBoxRegEx.Name = "textBoxRegEx";
            this.textBoxRegEx.Size = new System.Drawing.Size(225, 20);
            this.textBoxRegEx.TabIndex = 14;
            // 
            // checkBoxUnenrolledUser
            // 
            this.checkBoxUnenrolledUser.AutoSize = true;
            this.checkBoxUnenrolledUser.Location = new System.Drawing.Point(7, 11);
            this.checkBoxUnenrolledUser.Name = "checkBoxUnenrolledUser";
            this.checkBoxUnenrolledUser.Size = new System.Drawing.Size(184, 17);
            this.checkBoxUnenrolledUser.TabIndex = 13;
            this.checkBoxUnenrolledUser.Text = "Send Alert for an Unenrolled User";
            this.checkBoxUnenrolledUser.UseVisualStyleBackColor = true;
            this.checkBoxUnenrolledUser.CheckedChanged += new System.EventHandler(this.checkBoxUnenrolledUser_CheckedChanged);
            // 
            // checkBoxUserInBypass
            // 
            this.checkBoxUserInBypass.AutoSize = true;
            this.checkBoxUserInBypass.Location = new System.Drawing.Point(6, 32);
            this.checkBoxUserInBypass.Name = "checkBoxUserInBypass";
            this.checkBoxUserInBypass.Size = new System.Drawing.Size(127, 17);
            this.checkBoxUserInBypass.TabIndex = 12;
            this.checkBoxUserInBypass.Text = "User In Bypass Mode";
            this.checkBoxUserInBypass.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegExFilter
            // 
            this.checkBoxRegExFilter.AutoSize = true;
            this.checkBoxRegExFilter.Location = new System.Drawing.Point(9, 92);
            this.checkBoxRegExFilter.Name = "checkBoxRegExFilter";
            this.checkBoxRegExFilter.Size = new System.Drawing.Size(136, 17);
            this.checkBoxRegExFilter.TabIndex = 15;
            this.checkBoxRegExFilter.Text = "Unenrolled Regex Filter";
            this.checkBoxRegExFilter.UseVisualStyleBackColor = true;
            this.checkBoxRegExFilter.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBoxCountryEnabled
            // 
            this.checkBoxCountryEnabled.AutoSize = true;
            this.checkBoxCountryEnabled.Location = new System.Drawing.Point(6, 55);
            this.checkBoxCountryEnabled.Name = "checkBoxCountryEnabled";
            this.checkBoxCountryEnabled.Size = new System.Drawing.Size(101, 17);
            this.checkBoxCountryEnabled.TabIndex = 11;
            this.checkBoxCountryEnabled.Text = "Outside Country";
            this.checkBoxCountryEnabled.UseVisualStyleBackColor = true;
            this.checkBoxCountryEnabled.CheckedChanged += new System.EventHandler(this.checkBoxOutsideUS_CheckedChanged);
            // 
            // checkBoxSuspiciousLocation
            // 
            this.checkBoxSuspiciousLocation.AutoSize = true;
            this.checkBoxSuspiciousLocation.Location = new System.Drawing.Point(6, 9);
            this.checkBoxSuspiciousLocation.Name = "checkBoxSuspiciousLocation";
            this.checkBoxSuspiciousLocation.Size = new System.Drawing.Size(121, 17);
            this.checkBoxSuspiciousLocation.TabIndex = 10;
            this.checkBoxSuspiciousLocation.Text = "Suspicious Location";
            this.checkBoxSuspiciousLocation.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxIgnoreIP
            // 
            this.textBoxIgnoreIP.Location = new System.Drawing.Point(90, 30);
            this.textBoxIgnoreIP.Name = "textBoxIgnoreIP";
            this.textBoxIgnoreIP.Size = new System.Drawing.Size(183, 20);
            this.textBoxIgnoreIP.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Ignore IP List";
            // 
            // listBoxIgnoreIP
            // 
            this.listBoxIgnoreIP.FormattingEnabled = true;
            this.listBoxIgnoreIP.Location = new System.Drawing.Point(90, 62);
            this.listBoxIgnoreIP.Name = "listBoxIgnoreIP";
            this.listBoxIgnoreIP.Size = new System.Drawing.Size(273, 82);
            this.listBoxIgnoreIP.TabIndex = 0;
            this.listBoxIgnoreIP.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(678, 460);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(110, 23);
            this.buttonApply.TabIndex = 4;
            this.buttonApply.Text = "Save Settings";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(9, 150);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(359, 285);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelIgnoreIPList);
            this.tabPage1.Controls.Add(this.checkBoxUnenrolledUser);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(351, 259);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Unenrolled Alerts";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelOutsideCountry);
            this.tabPage2.Controls.Add(this.checkBoxSuspiciousLocation);
            this.tabPage2.Controls.Add(this.checkBoxCountryEnabled);
            this.tabPage2.Controls.Add(this.checkBoxUserInBypass);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(351, 259);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Security Alerts";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBoxGeoAlerts);
            this.tabPage3.Controls.Add(this.panelGeoAlerts);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(351, 259);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Geo Alerts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panelGeoAlerts
            // 
            this.panelGeoAlerts.Controls.Add(this.label18);
            this.panelGeoAlerts.Controls.Add(this.linkLabel1);
            this.panelGeoAlerts.Controls.Add(this.label15);
            this.panelGeoAlerts.Controls.Add(this.textBoxLongitude);
            this.panelGeoAlerts.Controls.Add(this.textBoxLatitude);
            this.panelGeoAlerts.Controls.Add(this.label14);
            this.panelGeoAlerts.Controls.Add(this.numericUpDownDistance);
            this.panelGeoAlerts.Controls.Add(this.label13);
            this.panelGeoAlerts.Controls.Add(this.label12);
            this.panelGeoAlerts.Enabled = false;
            this.panelGeoAlerts.Location = new System.Drawing.Point(4, 31);
            this.panelGeoAlerts.Name = "panelGeoAlerts";
            this.panelGeoAlerts.Size = new System.Drawing.Size(342, 116);
            this.panelGeoAlerts.TabIndex = 0;
            // 
            // checkBoxGeoAlerts
            // 
            this.checkBoxGeoAlerts.AutoSize = true;
            this.checkBoxGeoAlerts.Location = new System.Drawing.Point(4, 8);
            this.checkBoxGeoAlerts.Name = "checkBoxGeoAlerts";
            this.checkBoxGeoAlerts.Size = new System.Drawing.Size(111, 17);
            this.checkBoxGeoAlerts.TabIndex = 1;
            this.checkBoxGeoAlerts.Text = "Enable Geo Alerts";
            this.checkBoxGeoAlerts.UseVisualStyleBackColor = true;
            this.checkBoxGeoAlerts.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "From Latitude";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(225, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Generate alert if user\'s location is greater than:";
            // 
            // numericUpDownDistance
            // 
            this.numericUpDownDistance.Location = new System.Drawing.Point(238, 2);
            this.numericUpDownDistance.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownDistance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDistance.Name = "numericUpDownDistance";
            this.numericUpDownDistance.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownDistance.TabIndex = 2;
            this.numericUpDownDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(297, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "miles";
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Location = new System.Drawing.Point(81, 40);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLatitude.TabIndex = 4;
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Location = new System.Drawing.Point(81, 76);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLongitude.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Longitude";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(184, 81);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(146, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Use Ipwhois Current Location";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBoxCountryCode
            // 
            this.textBoxCountryCode.Location = new System.Drawing.Point(9, 2);
            this.textBoxCountryCode.MaxLength = 2;
            this.textBoxCountryCode.Name = "textBoxCountryCode";
            this.textBoxCountryCode.Size = new System.Drawing.Size(31, 20);
            this.textBoxCountryCode.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(45, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(227, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Two-letter (ISO 3166-1) country code (e.g. US)";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(9, 29);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(132, 13);
            this.linkLabel2.TabIndex = 15;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Use Ipwhois Country Code";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // panelOutsideCountry
            // 
            this.panelOutsideCountry.Controls.Add(this.label16);
            this.panelOutsideCountry.Controls.Add(this.linkLabel2);
            this.panelOutsideCountry.Controls.Add(this.textBoxCountryCode);
            this.panelOutsideCountry.Enabled = false;
            this.panelOutsideCountry.Location = new System.Drawing.Point(25, 78);
            this.panelOutsideCountry.Name = "panelOutsideCountry";
            this.panelOutsideCountry.Size = new System.Drawing.Size(287, 57);
            this.panelOutsideCountry.TabIndex = 16;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelPrivateRelay);
            this.tabPage4.Controls.Add(this.checkBoxPrivateRelay);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(351, 259);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "iCloud Private Relay";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // radioButtonPrivateRelayTag
            // 
            this.radioButtonPrivateRelayTag.AutoSize = true;
            this.radioButtonPrivateRelayTag.Location = new System.Drawing.Point(18, 51);
            this.radioButtonPrivateRelayTag.Name = "radioButtonPrivateRelayTag";
            this.radioButtonPrivateRelayTag.Size = new System.Drawing.Size(203, 17);
            this.radioButtonPrivateRelayTag.TabIndex = 1;
            this.radioButtonPrivateRelayTag.Text = "Tag alert with private relay information";
            this.radioButtonPrivateRelayTag.UseVisualStyleBackColor = true;
            // 
            // radioButtonPrivateRelayIgnore
            // 
            this.radioButtonPrivateRelayIgnore.AutoSize = true;
            this.radioButtonPrivateRelayIgnore.Checked = true;
            this.radioButtonPrivateRelayIgnore.Location = new System.Drawing.Point(18, 28);
            this.radioButtonPrivateRelayIgnore.Name = "radioButtonPrivateRelayIgnore";
            this.radioButtonPrivateRelayIgnore.Size = new System.Drawing.Size(206, 17);
            this.radioButtonPrivateRelayIgnore.TabIndex = 2;
            this.radioButtonPrivateRelayIgnore.TabStop = true;
            this.radioButtonPrivateRelayIgnore.Text = "Do not generate alert (Recommended)";
            this.radioButtonPrivateRelayIgnore.UseVisualStyleBackColor = true;
            // 
            // checkBoxPrivateRelay
            // 
            this.checkBoxPrivateRelay.AutoSize = true;
            this.checkBoxPrivateRelay.Location = new System.Drawing.Point(3, 18);
            this.checkBoxPrivateRelay.Name = "checkBoxPrivateRelay";
            this.checkBoxPrivateRelay.Size = new System.Drawing.Size(206, 17);
            this.checkBoxPrivateRelay.TabIndex = 3;
            this.checkBoxPrivateRelay.Text = "Enable iCloud Private Relay Detection";
            this.checkBoxPrivateRelay.UseVisualStyleBackColor = true;
            this.checkBoxPrivateRelay.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // panelPrivateRelay
            // 
            this.panelPrivateRelay.Controls.Add(this.label17);
            this.panelPrivateRelay.Controls.Add(this.radioButtonPrivateRelayIgnore);
            this.panelPrivateRelay.Controls.Add(this.radioButtonPrivateRelayTag);
            this.panelPrivateRelay.Enabled = false;
            this.panelPrivateRelay.Location = new System.Drawing.Point(6, 41);
            this.panelPrivateRelay.Name = "panelPrivateRelay";
            this.panelPrivateRelay.Size = new System.Drawing.Size(254, 100);
            this.panelPrivateRelay.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 490);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(805, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // timerClearStatus
            // 
            this.timerClearStatus.Interval = 3000;
            this.timerClearStatus.Tick += new System.EventHandler(this.timerClearStatus_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "User Ignore List";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(26, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(69, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "Remove";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(249, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // textBoxIgnoreUser
            // 
            this.textBoxIgnoreUser.Location = new System.Drawing.Point(106, 7);
            this.textBoxIgnoreUser.Name = "textBoxIgnoreUser";
            this.textBoxIgnoreUser.Size = new System.Drawing.Size(137, 20);
            this.textBoxIgnoreUser.TabIndex = 21;
            // 
            // listBoxIgnoreUser
            // 
            this.listBoxIgnoreUser.FormattingEnabled = true;
            this.listBoxIgnoreUser.Location = new System.Drawing.Point(108, 30);
            this.listBoxIgnoreUser.Name = "listBoxIgnoreUser";
            this.listBoxIgnoreUser.Size = new System.Drawing.Size(212, 56);
            this.listBoxIgnoreUser.TabIndex = 20;
            // 
            // panelIgnoreIPList
            // 
            this.panelIgnoreIPList.Controls.Add(this.button4);
            this.panelIgnoreIPList.Controls.Add(this.panelRegEx);
            this.panelIgnoreIPList.Controls.Add(this.label9);
            this.panelIgnoreIPList.Controls.Add(this.listBoxIgnoreUser);
            this.panelIgnoreIPList.Controls.Add(this.checkBoxRegExFilter);
            this.panelIgnoreIPList.Controls.Add(this.button3);
            this.panelIgnoreIPList.Controls.Add(this.textBoxIgnoreUser);
            this.panelIgnoreIPList.Enabled = false;
            this.panelIgnoreIPList.Location = new System.Drawing.Point(6, 34);
            this.panelIgnoreIPList.Name = "panelIgnoreIPList";
            this.panelIgnoreIPList.Size = new System.Drawing.Size(340, 202);
            this.panelIgnoreIPList.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(198, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "If Suspicious IP Is iCloud Private Relay...";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(188, 43);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(131, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Must Be In WGS84 format";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 512);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DUO Alerts - Setup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSetup_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panelRegEx.ResumeLayout(false);
            this.panelRegEx.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panelGeoAlerts.ResumeLayout(false);
            this.panelGeoAlerts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).EndInit();
            this.panelOutsideCountry.ResumeLayout(false);
            this.panelOutsideCountry.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panelPrivateRelay.ResumeLayout(false);
            this.panelPrivateRelay.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelIgnoreIPList.ResumeLayout(false);
            this.panelIgnoreIPList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxDuoHostName;
        private System.Windows.Forms.TextBox textBoxDuoSKey;
        private System.Windows.Forms.TextBox textBoxDuoiKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxIPWhoIsAPIKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxAWSSNSARN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAWSSecretAccessKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAWSAccessCode;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxIgnoreIP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBoxIgnoreIP;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.CheckBox checkBoxSuspiciousLocation;
        private System.Windows.Forms.CheckBox checkBoxCountryEnabled;
        private System.Windows.Forms.CheckBox checkBoxUserInBypass;
        private System.Windows.Forms.CheckBox checkBoxUnenrolledUser;
        private System.Windows.Forms.CheckBox checkBoxRegExFilter;
        private System.Windows.Forms.TextBox textBoxRegEx;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRegExTest;
        private System.Windows.Forms.Panel panelRegEx;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxHideDuoSKey;
        private System.Windows.Forms.CheckBox checkBoxHideIPWHOISKey;
        private System.Windows.Forms.CheckBox checkBoxHideAWSSecretKey;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBoxGeoAlerts;
        private System.Windows.Forms.Panel panelGeoAlerts;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericUpDownDistance;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxCountryCode;
        private System.Windows.Forms.Panel panelOutsideCountry;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RadioButton radioButtonPrivateRelayIgnore;
        private System.Windows.Forms.RadioButton radioButtonPrivateRelayTag;
        private System.Windows.Forms.Panel panelPrivateRelay;
        private System.Windows.Forms.CheckBox checkBoxPrivateRelay;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.Timer timerClearStatus;
        private System.Windows.Forms.Panel panelIgnoreIPList;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox listBoxIgnoreUser;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxIgnoreUser;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
    }
}