namespace CSharpNugetGenerator
{
    partial class CSharpNuget_Generator
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
            this.generate_Nuget = new System.Windows.Forms.Button();
            this.configurationList = new System.Windows.Forms.ComboBox();
            this.Save = new System.Windows.Forms.Button();
            this.ProjectConfigComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VSBuildConfiglabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.EnableMultiCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this._NuGetSpec = new System.Windows.Forms.TabPage();
            this.textBox_owner = new System.Windows.Forms.TextBox();
            this.Owner = new System.Windows.Forms.Label();
            this.textBox_author = new System.Windows.Forms.TextBox();
            this.author = new System.Windows.Forms.Label();
            this.VersionPreviewLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.browse_RuntimeFolder = new System.Windows.Forms.Button();
            this.descriptionPad = new System.Windows.Forms.RichTextBox();
            this.ReleasePad = new System.Windows.Forms.RichTextBox();
            this.fourthDigit = new System.Windows.Forms.TextBox();
            this.Remove_reference = new System.Windows.Forms.Button();
            this.Remove_runtime = new System.Windows.Forms.Button();
            this.browse_RunTime = new System.Windows.Forms.Button();
            this.browse_Dependencies = new System.Windows.Forms.Button();
            this.Runtime_Dependencies = new System.Windows.Forms.GroupBox();
            this._runtime = new System.Windows.Forms.DataGridView();
            this.References = new System.Windows.Forms.GroupBox();
            this._reference = new System.Windows.Forms.DataGridView();
            this.TargetPath = new System.Windows.Forms.TextBox();
            this.frameworkDropDown = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.OutputPath_Browser = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nugetName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ObfuscationPg = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.RemoveObfuscateDependency = new System.Windows.Forms.Button();
            this.AddObfuscateDependency = new System.Windows.Forms.Button();
            this.obfuscationDependency = new System.Windows.Forms.GroupBox();
            this.obfuscationDataGridView = new System.Windows.Forms.DataGridView();
            this.DescriptionAbtNuGetGenerator = new System.Windows.Forms.Label();
            this._selectCommonConfig = new System.Windows.Forms.GroupBox();
            this.commonRepoOutPutDir = new System.Windows.Forms.Button();
            this.textbox_RepoOutPutDir = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.commonRepoDllPath = new System.Windows.Forms.Button();
            this.textbox_RepoDllPath = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textbox_RepoPath = new System.Windows.Forms.TextBox();
            this.commonRepoPath = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this._logPage = new System.Windows.Forms.TabPage();
            this._selectforAllConfig = new System.Windows.Forms.GroupBox();
            this._log = new System.Windows.Forms.RichTextBox();
            this._obfusResult = new System.Windows.Forms.TabPage();
            this.ValidateNuGetTree = new System.Windows.Forms.TreeView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this._Obfuscate = new System.Windows.Forms.CheckBox();
            this.support_CLR = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this._NuGetSpec.SuspendLayout();
            this.Runtime_Dependencies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._runtime)).BeginInit();
            this.References.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._reference)).BeginInit();
            this.ObfuscationPg.SuspendLayout();
            this.obfuscationDependency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.obfuscationDataGridView)).BeginInit();
            this._selectCommonConfig.SuspendLayout();
            this._logPage.SuspendLayout();
            this._selectforAllConfig.SuspendLayout();
            this._obfusResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // generate_Nuget
            // 
            this.generate_Nuget.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.generate_Nuget.Location = new System.Drawing.Point(834, 488);
            this.generate_Nuget.Name = "generate_Nuget";
            this.generate_Nuget.Size = new System.Drawing.Size(114, 40);
            this.generate_Nuget.TabIndex = 11;
            this.generate_Nuget.Text = "Generate Nuget";
            this.generate_Nuget.UseVisualStyleBackColor = false;
            this.generate_Nuget.Click += new System.EventHandler(this.generate_Nuget_Click);
            // 
            // configurationList
            // 
            this.configurationList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.configurationList.FormattingEnabled = true;
            this.configurationList.Items.AddRange(new object[] {
            "-- Select Configuration--",
            "New"});
            this.configurationList.Location = new System.Drawing.Point(20, 26);
            this.configurationList.Name = "configurationList";
            this.configurationList.Size = new System.Drawing.Size(199, 21);
            this.configurationList.TabIndex = 32;
            this.configurationList.SelectedIndexChanged += new System.EventHandler(this.configurationList_SelectedIndexChanged);
            this.configurationList.SelectedValueChanged += new System.EventHandler(this.configurationList_SelectedValueChanged);
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Save.Location = new System.Drawing.Point(689, 488);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(114, 40);
            this.Save.TabIndex = 10;
            this.Save.Text = "Save Config";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // ProjectConfigComboBox
            // 
            this.ProjectConfigComboBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ProjectConfigComboBox.FormattingEnabled = true;
            this.ProjectConfigComboBox.Items.AddRange(new object[] {
            "-- Select Project Config--",
            "New"});
            this.ProjectConfigComboBox.Location = new System.Drawing.Point(238, 26);
            this.ProjectConfigComboBox.Name = "ProjectConfigComboBox";
            this.ProjectConfigComboBox.Size = new System.Drawing.Size(199, 21);
            this.ProjectConfigComboBox.TabIndex = 105;
            this.ProjectConfigComboBox.Text = "-- Select Project Config--";
            this.ProjectConfigComboBox.SelectedIndexChanged += new System.EventHandler(this.ProjectConfigComboBox_SelectedIndexChanged);
            this.ProjectConfigComboBox.SelectedValueChanged += new System.EventHandler(this.ProjectConfigComboBox_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 106;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // VSBuildConfiglabel
            // 
            this.VSBuildConfiglabel.AutoSize = true;
            this.VSBuildConfiglabel.ForeColor = System.Drawing.Color.DimGray;
            this.VSBuildConfiglabel.Location = new System.Drawing.Point(235, 10);
            this.VSBuildConfiglabel.Name = "VSBuildConfiglabel";
            this.VSBuildConfiglabel.Size = new System.Drawing.Size(80, 13);
            this.VSBuildConfiglabel.TabIndex = 107;
            this.VSBuildConfiglabel.Text = "VS Build Config";
            this.VSBuildConfiglabel.Click += new System.EventHandler(this.VSBuildConfiglabel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(17, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 108;
            this.label9.Text = "NuGet Config";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // EnableMultiCheckBox
            // 
            this.EnableMultiCheckBox.AutoSize = true;
            this.EnableMultiCheckBox.Location = new System.Drawing.Point(828, 30);
            this.EnableMultiCheckBox.Name = "EnableMultiCheckBox";
            this.EnableMultiCheckBox.Size = new System.Drawing.Size(134, 17);
            this.EnableMultiCheckBox.TabIndex = 109;
            this.EnableMultiCheckBox.Text = "Enable Multi VS Config";
            this.EnableMultiCheckBox.UseVisualStyleBackColor = true;
            this.EnableMultiCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl.Controls.Add(this._NuGetSpec);
            this.tabControl.Controls.Add(this.ObfuscationPg);
            this.tabControl.Controls.Add(this._logPage);
            this.tabControl.Controls.Add(this._obfusResult);
            this.tabControl.Location = new System.Drawing.Point(10, 66);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(966, 569);
            this.tabControl.TabIndex = 115;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // _NuGetSpec
            // 
            this._NuGetSpec.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._NuGetSpec.Controls.Add(this.textBox_owner);
            this._NuGetSpec.Controls.Add(this.Owner);
            this._NuGetSpec.Controls.Add(this.textBox_author);
            this._NuGetSpec.Controls.Add(this.author);
            this._NuGetSpec.Controls.Add(this.VersionPreviewLabel);
            this._NuGetSpec.Controls.Add(this.label10);
            this._NuGetSpec.Controls.Add(this.label3);
            this._NuGetSpec.Controls.Add(this.dateTimePicker);
            this._NuGetSpec.Controls.Add(this.browse_RuntimeFolder);
            this._NuGetSpec.Controls.Add(this.Save);
            this._NuGetSpec.Controls.Add(this.descriptionPad);
            this._NuGetSpec.Controls.Add(this.ReleasePad);
            this._NuGetSpec.Controls.Add(this.generate_Nuget);
            this._NuGetSpec.Controls.Add(this.fourthDigit);
            this._NuGetSpec.Controls.Add(this.Remove_reference);
            this._NuGetSpec.Controls.Add(this.Remove_runtime);
            this._NuGetSpec.Controls.Add(this.browse_RunTime);
            this._NuGetSpec.Controls.Add(this.browse_Dependencies);
            this._NuGetSpec.Controls.Add(this.Runtime_Dependencies);
            this._NuGetSpec.Controls.Add(this.References);
            this._NuGetSpec.Controls.Add(this.TargetPath);
            this._NuGetSpec.Controls.Add(this.frameworkDropDown);
            this._NuGetSpec.Controls.Add(this.label8);
            this._NuGetSpec.Controls.Add(this.OutputPath_Browser);
            this._NuGetSpec.Controls.Add(this.label7);
            this._NuGetSpec.Controls.Add(this.label6);
            this._NuGetSpec.Controls.Add(this.label5);
            this._NuGetSpec.Controls.Add(this.nugetName);
            this._NuGetSpec.Controls.Add(this.label4);
            this._NuGetSpec.Controls.Add(this.label1);
            this._NuGetSpec.Location = new System.Drawing.Point(4, 25);
            this._NuGetSpec.Name = "_NuGetSpec";
            this._NuGetSpec.Padding = new System.Windows.Forms.Padding(3);
            this._NuGetSpec.Size = new System.Drawing.Size(958, 540);
            this._NuGetSpec.TabIndex = 0;
            this._NuGetSpec.Text = "NuGet Spec";
            this._NuGetSpec.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // textBox_owner
            // 
            this.textBox_owner.Location = new System.Drawing.Point(753, 434);
            this.textBox_owner.Name = "textBox_owner";
            this.textBox_owner.Size = new System.Drawing.Size(195, 20);
            this.textBox_owner.TabIndex = 144;
            // 
            // Owner
            // 
            this.Owner.AutoSize = true;
            this.Owner.Location = new System.Drawing.Point(684, 437);
            this.Owner.Name = "Owner";
            this.Owner.Size = new System.Drawing.Size(38, 13);
            this.Owner.TabIndex = 146;
            this.Owner.Text = "Owner";
            // 
            // textBox_author
            // 
            this.textBox_author.Location = new System.Drawing.Point(753, 398);
            this.textBox_author.Name = "textBox_author";
            this.textBox_author.Size = new System.Drawing.Size(195, 20);
            this.textBox_author.TabIndex = 143;
            // 
            // author
            // 
            this.author.AutoSize = true;
            this.author.Location = new System.Drawing.Point(684, 405);
            this.author.Name = "author";
            this.author.Size = new System.Drawing.Size(38, 13);
            this.author.TabIndex = 145;
            this.author.Text = "Author";
            // 
            // VersionPreviewLabel
            // 
            this.VersionPreviewLabel.AutoSize = true;
            this.VersionPreviewLabel.Location = new System.Drawing.Point(436, 434);
            this.VersionPreviewLabel.Name = "VersionPreviewLabel";
            this.VersionPreviewLabel.Size = new System.Drawing.Size(10, 13);
            this.VersionPreviewLabel.TabIndex = 142;
            this.VersionPreviewLabel.Text = " ";
            this.VersionPreviewLabel.Click += new System.EventHandler(this.VersionPreviewLabel_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(458, 434);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 141;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 434);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 140;
            this.label3.Text = "BuildNumber";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(186, 431);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePicker.Size = new System.Drawing.Size(126, 20);
            this.dateTimePicker.TabIndex = 139;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // browse_RuntimeFolder
            // 
            this.browse_RuntimeFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.browse_RuntimeFolder.Location = new System.Drawing.Point(800, 14);
            this.browse_RuntimeFolder.Name = "browse_RuntimeFolder";
            this.browse_RuntimeFolder.Size = new System.Drawing.Size(67, 23);
            this.browse_RuntimeFolder.TabIndex = 138;
            this.browse_RuntimeFolder.Text = "Add Folder";
            this.browse_RuntimeFolder.UseVisualStyleBackColor = false;
            this.browse_RuntimeFolder.Click += new System.EventHandler(this.browse_RuntimeFolder_Click);
            // 
            // descriptionPad
            // 
            this.descriptionPad.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.descriptionPad.Location = new System.Drawing.Point(109, 466);
            this.descriptionPad.Name = "descriptionPad";
            this.descriptionPad.Size = new System.Drawing.Size(214, 62);
            this.descriptionPad.TabIndex = 121;
            this.descriptionPad.Text = "";
            this.descriptionPad.TextChanged += new System.EventHandler(this.descriptionPad_TextChanged);
            // 
            // ReleasePad
            // 
            this.ReleasePad.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ReleasePad.Location = new System.Drawing.Point(439, 466);
            this.ReleasePad.Name = "ReleasePad";
            this.ReleasePad.Size = new System.Drawing.Size(239, 62);
            this.ReleasePad.TabIndex = 123;
            this.ReleasePad.Text = "";
            this.ReleasePad.TextChanged += new System.EventHandler(this.ReleasePad_TextChanged);
            // 
            // fourthDigit
            // 
            this.fourthDigit.Location = new System.Drawing.Point(109, 431);
            this.fourthDigit.Name = "fourthDigit";
            this.fourthDigit.Size = new System.Drawing.Size(54, 20);
            this.fourthDigit.TabIndex = 118;
            this.fourthDigit.Text = "1";
            this.fourthDigit.TextChanged += new System.EventHandler(this.fourthDigit_TextChanged);
            this.fourthDigit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fourthDigit_KeyPress);
            // 
            // Remove_reference
            // 
            this.Remove_reference.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Remove_reference.Location = new System.Drawing.Point(342, 14);
            this.Remove_reference.Name = "Remove_reference";
            this.Remove_reference.Size = new System.Drawing.Size(58, 23);
            this.Remove_reference.TabIndex = 135;
            this.Remove_reference.Text = "Remove";
            this.Remove_reference.UseVisualStyleBackColor = false;
            this.Remove_reference.Click += new System.EventHandler(this.Remove_reference_Click);
            // 
            // Remove_runtime
            // 
            this.Remove_runtime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Remove_runtime.Location = new System.Drawing.Point(736, 14);
            this.Remove_runtime.Name = "Remove_runtime";
            this.Remove_runtime.Size = new System.Drawing.Size(58, 23);
            this.Remove_runtime.TabIndex = 134;
            this.Remove_runtime.Text = "Remove";
            this.Remove_runtime.UseVisualStyleBackColor = false;
            this.Remove_runtime.Click += new System.EventHandler(this.Remove_runtime_Click);
            // 
            // browse_RunTime
            // 
            this.browse_RunTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.browse_RunTime.Location = new System.Drawing.Point(873, 14);
            this.browse_RunTime.Name = "browse_RunTime";
            this.browse_RunTime.Size = new System.Drawing.Size(67, 23);
            this.browse_RunTime.TabIndex = 137;
            this.browse_RunTime.Text = "Add ";
            this.browse_RunTime.UseVisualStyleBackColor = false;
            this.browse_RunTime.Click += new System.EventHandler(this.browse_RunTime_Click);
            // 
            // browse_Dependencies
            // 
            this.browse_Dependencies.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.browse_Dependencies.Location = new System.Drawing.Point(406, 14);
            this.browse_Dependencies.Name = "browse_Dependencies";
            this.browse_Dependencies.Size = new System.Drawing.Size(65, 23);
            this.browse_Dependencies.TabIndex = 136;
            this.browse_Dependencies.Text = "Add";
            this.browse_Dependencies.UseVisualStyleBackColor = false;
            this.browse_Dependencies.Click += new System.EventHandler(this.browse_Dependencies_Click);
            // 
            // Runtime_Dependencies
            // 
            this.Runtime_Dependencies.Controls.Add(this._runtime);
            this.Runtime_Dependencies.Location = new System.Drawing.Point(483, 34);
            this.Runtime_Dependencies.Name = "Runtime_Dependencies";
            this.Runtime_Dependencies.Size = new System.Drawing.Size(463, 307);
            this.Runtime_Dependencies.TabIndex = 133;
            this.Runtime_Dependencies.TabStop = false;
            this.Runtime_Dependencies.Text = "Runtime Dependencies";
            this.Runtime_Dependencies.Enter += new System.EventHandler(this.Runtime_Dependencies_Enter);
            // 
            // _runtime
            // 
            this._runtime.AllowUserToAddRows = false;
            this._runtime.AllowUserToDeleteRows = false;
            this._runtime.AllowUserToResizeColumns = false;
            this._runtime.AllowUserToResizeRows = false;
            this._runtime.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._runtime.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this._runtime.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this._runtime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._runtime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._runtime.ColumnHeadersVisible = false;
            this._runtime.GridColor = System.Drawing.SystemColors.ButtonFace;
            this._runtime.Location = new System.Drawing.Point(6, 19);
            this._runtime.Name = "_runtime";
            this._runtime.ReadOnly = true;
            this._runtime.RowHeadersVisible = false;
            this._runtime.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._runtime.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._runtime.Size = new System.Drawing.Size(451, 282);
            this._runtime.TabIndex = 28;
            this._runtime.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._runtime_CellContentClick);
            // 
            // References
            // 
            this.References.Controls.Add(this._reference);
            this.References.Location = new System.Drawing.Point(6, 34);
            this.References.Name = "References";
            this.References.Size = new System.Drawing.Size(471, 307);
            this.References.TabIndex = 132;
            this.References.TabStop = false;
            this.References.Text = "References";
            this.References.Enter += new System.EventHandler(this.References_Enter);
            // 
            // _reference
            // 
            this._reference.AllowUserToAddRows = false;
            this._reference.AllowUserToDeleteRows = false;
            this._reference.AllowUserToResizeColumns = false;
            this._reference.AllowUserToResizeRows = false;
            this._reference.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._reference.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this._reference.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this._reference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._reference.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._reference.ColumnHeadersVisible = false;
            this._reference.GridColor = System.Drawing.SystemColors.ButtonFace;
            this._reference.Location = new System.Drawing.Point(6, 19);
            this._reference.Name = "_reference";
            this._reference.ReadOnly = true;
            this._reference.RowHeadersVisible = false;
            this._reference.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._reference.Size = new System.Drawing.Size(459, 282);
            this._reference.TabIndex = 26;
            this._reference.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._reference_CellContentClick);
            // 
            // TargetPath
            // 
            this.TargetPath.Location = new System.Drawing.Point(109, 395);
            this.TargetPath.Name = "TargetPath";
            this.TargetPath.Size = new System.Drawing.Size(368, 20);
            this.TargetPath.TabIndex = 117;
            this.TargetPath.TextChanged += new System.EventHandler(this.TargetPath_TextChanged);
            // 
            // frameworkDropDown
            // 
            this.frameworkDropDown.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.frameworkDropDown.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.frameworkDropDown.Items.AddRange(new object[] {
            "4.5.0",
            "4.5.1",
            "4.5.2",
            "4.6.0",
            "4.6.1",
            "4.6.2",
            "4.7.0",
            "4.7.1",
            "4.7.2",
            "4.8"});
            this.frameworkDropDown.Location = new System.Drawing.Point(582, 357);
            this.frameworkDropDown.Name = "frameworkDropDown";
            this.frameworkDropDown.Size = new System.Drawing.Size(364, 21);
            this.frameworkDropDown.TabIndex = 131;
            this.frameworkDropDown.Text = "4.5.2";
            this.frameworkDropDown.SelectedIndexChanged += new System.EventHandler(this.frameworkDropDown_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 398);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 130;
            this.label8.Text = "Target File Path";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // OutputPath_Browser
            // 
            this.OutputPath_Browser.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OutputPath_Browser.Location = new System.Drawing.Point(489, 393);
            this.OutputPath_Browser.Name = "OutputPath_Browser";
            this.OutputPath_Browser.Size = new System.Drawing.Size(73, 23);
            this.OutputPath_Browser.TabIndex = 129;
            this.OutputPath_Browser.Text = "Browse";
            this.OutputPath_Browser.UseVisualStyleBackColor = false;
            this.OutputPath_Browser.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 466);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 128;
            this.label7.Text = "Release Notes";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(339, 434);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 125;
            this.label6.Text = "Version Preview";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 469);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 124;
            this.label5.Text = "Description";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // nugetName
            // 
            this.nugetName.Location = new System.Drawing.Point(109, 357);
            this.nugetName.Name = "nugetName";
            this.nugetName.Size = new System.Drawing.Size(368, 20);
            this.nugetName.TabIndex = 115;
            this.nugetName.TextChanged += new System.EventHandler(this.nugetName_TextChanged_1);
            this.nugetName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nugetName_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(486, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 122;
            this.label4.Text = "TargetFramework";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 116;
            this.label1.Text = "Nuget Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ObfuscationPg
            // 
            this.ObfuscationPg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ObfuscationPg.Controls.Add(this.label14);
            this.ObfuscationPg.Controls.Add(this.RemoveObfuscateDependency);
            this.ObfuscationPg.Controls.Add(this.AddObfuscateDependency);
            this.ObfuscationPg.Controls.Add(this.obfuscationDependency);
            this.ObfuscationPg.Controls.Add(this.DescriptionAbtNuGetGenerator);
            this.ObfuscationPg.Controls.Add(this._selectCommonConfig);
            this.ObfuscationPg.Location = new System.Drawing.Point(4, 25);
            this.ObfuscationPg.Name = "ObfuscationPg";
            this.ObfuscationPg.Padding = new System.Windows.Forms.Padding(3);
            this.ObfuscationPg.Size = new System.Drawing.Size(958, 540);
            this.ObfuscationPg.TabIndex = 1;
            this.ObfuscationPg.Text = "Obfuscation Spec";
            this.ObfuscationPg.Click += new System.EventHandler(this.ObfuscationPg_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(13, 503);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 13);
            this.label14.TabIndex = 139;
            this.label14.Text = "Instructions";
            // 
            // RemoveObfuscateDependency
            // 
            this.RemoveObfuscateDependency.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RemoveObfuscateDependency.Location = new System.Drawing.Point(801, 193);
            this.RemoveObfuscateDependency.Name = "RemoveObfuscateDependency";
            this.RemoveObfuscateDependency.Size = new System.Drawing.Size(58, 23);
            this.RemoveObfuscateDependency.TabIndex = 137;
            this.RemoveObfuscateDependency.Text = "Remove";
            this.RemoveObfuscateDependency.UseVisualStyleBackColor = false;
            this.RemoveObfuscateDependency.Click += new System.EventHandler(this.RemoveObfuscateDependency_Click);
            // 
            // AddObfuscateDependency
            // 
            this.AddObfuscateDependency.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AddObfuscateDependency.Location = new System.Drawing.Point(865, 193);
            this.AddObfuscateDependency.Name = "AddObfuscateDependency";
            this.AddObfuscateDependency.Size = new System.Drawing.Size(65, 23);
            this.AddObfuscateDependency.TabIndex = 138;
            this.AddObfuscateDependency.Text = "Add";
            this.AddObfuscateDependency.UseVisualStyleBackColor = false;
            this.AddObfuscateDependency.Click += new System.EventHandler(this.AddObfuscateDependency_Click);
            // 
            // obfuscationDependency
            // 
            this.obfuscationDependency.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.obfuscationDependency.Controls.Add(this.obfuscationDataGridView);
            this.obfuscationDependency.Location = new System.Drawing.Point(16, 213);
            this.obfuscationDependency.Name = "obfuscationDependency";
            this.obfuscationDependency.Size = new System.Drawing.Size(932, 267);
            this.obfuscationDependency.TabIndex = 134;
            this.obfuscationDependency.TabStop = false;
            this.obfuscationDependency.Text = "Obfuscation Dependencies";
            this.obfuscationDependency.Enter += new System.EventHandler(this.obfuscationDependency_Enter);
            // 
            // obfuscationDataGridView
            // 
            this.obfuscationDataGridView.AllowUserToAddRows = false;
            this.obfuscationDataGridView.AllowUserToDeleteRows = false;
            this.obfuscationDataGridView.AllowUserToResizeColumns = false;
            this.obfuscationDataGridView.AllowUserToResizeRows = false;
            this.obfuscationDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.obfuscationDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.obfuscationDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.obfuscationDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.obfuscationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.obfuscationDataGridView.ColumnHeadersVisible = false;
            this.obfuscationDataGridView.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.obfuscationDataGridView.Location = new System.Drawing.Point(9, 19);
            this.obfuscationDataGridView.Name = "obfuscationDataGridView";
            this.obfuscationDataGridView.ReadOnly = true;
            this.obfuscationDataGridView.RowHeadersVisible = false;
            this.obfuscationDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.obfuscationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.obfuscationDataGridView.Size = new System.Drawing.Size(917, 238);
            this.obfuscationDataGridView.TabIndex = 28;
            this.obfuscationDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.obfuscationDataGridView_CellContentClick);
            // 
            // DescriptionAbtNuGetGenerator
            // 
            this.DescriptionAbtNuGetGenerator.AutoSize = true;
            this.DescriptionAbtNuGetGenerator.Location = new System.Drawing.Point(13, 537);
            this.DescriptionAbtNuGetGenerator.Name = "DescriptionAbtNuGetGenerator";
            this.DescriptionAbtNuGetGenerator.Size = new System.Drawing.Size(41, 13);
            this.DescriptionAbtNuGetGenerator.TabIndex = 6;
            this.DescriptionAbtNuGetGenerator.Text = "label12";
            this.DescriptionAbtNuGetGenerator.Click += new System.EventHandler(this.DescriptionAbtNuGetGenerator_Click);
            // 
            // _selectCommonConfig
            // 
            this._selectCommonConfig.Controls.Add(this.commonRepoOutPutDir);
            this._selectCommonConfig.Controls.Add(this.textbox_RepoOutPutDir);
            this._selectCommonConfig.Controls.Add(this.label13);
            this._selectCommonConfig.Controls.Add(this.commonRepoDllPath);
            this._selectCommonConfig.Controls.Add(this.textbox_RepoDllPath);
            this._selectCommonConfig.Controls.Add(this.label12);
            this._selectCommonConfig.Controls.Add(this.textbox_RepoPath);
            this._selectCommonConfig.Controls.Add(this.commonRepoPath);
            this._selectCommonConfig.Controls.Add(this.label11);
            this._selectCommonConfig.Location = new System.Drawing.Point(16, 31);
            this._selectCommonConfig.Name = "_selectCommonConfig";
            this._selectCommonConfig.Size = new System.Drawing.Size(932, 148);
            this._selectCommonConfig.TabIndex = 5;
            this._selectCommonConfig.TabStop = false;
            this._selectCommonConfig.Text = "Obproj  Settings";
            this._selectCommonConfig.Enter += new System.EventHandler(this._selectCommonConfig_Enter);
            // 
            // commonRepoOutPutDir
            // 
            this.commonRepoOutPutDir.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.commonRepoOutPutDir.Location = new System.Drawing.Point(814, 101);
            this.commonRepoOutPutDir.Name = "commonRepoOutPutDir";
            this.commonRepoOutPutDir.Size = new System.Drawing.Size(100, 23);
            this.commonRepoOutPutDir.TabIndex = 10;
            this.commonRepoOutPutDir.Text = "Repo Browser";
            this.commonRepoOutPutDir.UseVisualStyleBackColor = false;
            this.commonRepoOutPutDir.Click += new System.EventHandler(this.commonRepoOutPutDir_Click);
            // 
            // textbox_RepoOutPutDir
            // 
            this.textbox_RepoOutPutDir.Enabled = false;
            this.textbox_RepoOutPutDir.Location = new System.Drawing.Point(137, 104);
            this.textbox_RepoOutPutDir.Name = "textbox_RepoOutPutDir";
            this.textbox_RepoOutPutDir.Size = new System.Drawing.Size(661, 20);
            this.textbox_RepoOutPutDir.TabIndex = 9;
            this.textbox_RepoOutPutDir.TextChanged += new System.EventHandler(this.textbox_RepoOutPutDir_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Obproj Output Directory ";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // commonRepoDllPath
            // 
            this.commonRepoDllPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.commonRepoDllPath.Location = new System.Drawing.Point(814, 67);
            this.commonRepoDllPath.Name = "commonRepoDllPath";
            this.commonRepoDllPath.Size = new System.Drawing.Size(100, 23);
            this.commonRepoDllPath.TabIndex = 7;
            this.commonRepoDllPath.Text = "Repo Browser";
            this.commonRepoDllPath.UseVisualStyleBackColor = false;
            this.commonRepoDllPath.Click += new System.EventHandler(this.commonRepoDllPath_Click);
            // 
            // textbox_RepoDllPath
            // 
            this.textbox_RepoDllPath.Enabled = false;
            this.textbox_RepoDllPath.Location = new System.Drawing.Point(137, 70);
            this.textbox_RepoDllPath.Name = "textbox_RepoDllPath";
            this.textbox_RepoDllPath.Size = new System.Drawing.Size(661, 20);
            this.textbox_RepoDllPath.TabIndex = 6;
            this.textbox_RepoDllPath.TextChanged += new System.EventHandler(this.textbox_RepoDllPath_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Obproj DLL Directory";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // textbox_RepoPath
            // 
            this.textbox_RepoPath.Enabled = false;
            this.textbox_RepoPath.Location = new System.Drawing.Point(137, 35);
            this.textbox_RepoPath.Name = "textbox_RepoPath";
            this.textbox_RepoPath.Size = new System.Drawing.Size(661, 20);
            this.textbox_RepoPath.TabIndex = 2;
            this.textbox_RepoPath.TextChanged += new System.EventHandler(this.textbox_RepoPath_TextChanged);
            // 
            // commonRepoPath
            // 
            this.commonRepoPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.commonRepoPath.Location = new System.Drawing.Point(814, 31);
            this.commonRepoPath.Name = "commonRepoPath";
            this.commonRepoPath.Size = new System.Drawing.Size(100, 23);
            this.commonRepoPath.TabIndex = 4;
            this.commonRepoPath.Text = "Repo Browser";
            this.commonRepoPath.UseVisualStyleBackColor = false;
            this.commonRepoPath.Click += new System.EventHandler(this.commonRepoPath_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Obproj File";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // _logPage
            // 
            this._logPage.Controls.Add(this._selectforAllConfig);
            this._logPage.Location = new System.Drawing.Point(4, 25);
            this._logPage.Name = "_logPage";
            this._logPage.Size = new System.Drawing.Size(958, 540);
            this._logPage.TabIndex = 2;
            this._logPage.Text = "Output Log";
            this._logPage.UseVisualStyleBackColor = true;
            this._logPage.Click += new System.EventHandler(this._logPage_Click);
            // 
            // _selectforAllConfig
            // 
            this._selectforAllConfig.Controls.Add(this._log);
            this._selectforAllConfig.Location = new System.Drawing.Point(16, 19);
            this._selectforAllConfig.Name = "_selectforAllConfig";
            this._selectforAllConfig.Size = new System.Drawing.Size(923, 685);
            this._selectforAllConfig.TabIndex = 7;
            this._selectforAllConfig.TabStop = false;
            this._selectforAllConfig.Text = "Output Log";
            this._selectforAllConfig.Enter += new System.EventHandler(this._selectforAllConfig_Enter);
            // 
            // _log
            // 
            this._log.Location = new System.Drawing.Point(6, 20);
            this._log.Name = "_log";
            this._log.Size = new System.Drawing.Size(911, 654);
            this._log.TabIndex = 2;
            this._log.Text = "";
            this._log.TextChanged += new System.EventHandler(this._log_TextChanged_1);
            this._log.KeyDown += new System.Windows.Forms.KeyEventHandler(this._log_KeyDown);
            this._log.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._log_KeyPress);
            // 
            // _obfusResult
            // 
            this._obfusResult.Controls.Add(this.ValidateNuGetTree);
            this._obfusResult.Location = new System.Drawing.Point(4, 25);
            this._obfusResult.Name = "_obfusResult";
            this._obfusResult.Size = new System.Drawing.Size(958, 540);
            this._obfusResult.TabIndex = 3;
            this._obfusResult.Text = "Obfuscation Result";
            this._obfusResult.UseVisualStyleBackColor = true;
            this._obfusResult.Click += new System.EventHandler(this._obfusResult_Click);
            // 
            // ValidateNuGetTree
            // 
            this.ValidateNuGetTree.Location = new System.Drawing.Point(6, 16);
            this.ValidateNuGetTree.Name = "ValidateNuGetTree";
            this.ValidateNuGetTree.Size = new System.Drawing.Size(942, 679);
            this.ValidateNuGetTree.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.progressBar.Location = new System.Drawing.Point(0, 644);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(987, 10);
            this.progressBar.TabIndex = 116;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // _Obfuscate
            // 
            this._Obfuscate.AutoSize = true;
            this._Obfuscate.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this._Obfuscate.Location = new System.Drawing.Point(742, 30);
            this._Obfuscate.Name = "_Obfuscate";
            this._Obfuscate.Size = new System.Drawing.Size(75, 17);
            this._Obfuscate.TabIndex = 117;
            this._Obfuscate.Text = "Obfuscate";
            this._Obfuscate.UseVisualStyleBackColor = false;
            this._Obfuscate.CheckedChanged += new System.EventHandler(this._Obfuscate_CheckedChanged);
            // 
            // support_CLR
            // 
            this.support_CLR.AutoSize = true;
            this.support_CLR.Location = new System.Drawing.Point(635, 30);
            this.support_CLR.Name = "support_CLR";
            this.support_CLR.Size = new System.Drawing.Size(87, 17);
            this.support_CLR.TabIndex = 118;
            this.support_CLR.Text = "Support CLR";
            this.support_CLR.UseVisualStyleBackColor = true;
            this.support_CLR.CheckedChanged += new System.EventHandler(this.support_CLR_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(497, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 40);
            this.button1.TabIndex = 147;
            this.button1.Text = "Load Config";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // CSharpNuget_Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(987, 654);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.support_CLR);
            this.Controls.Add(this._Obfuscate);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.EnableMultiCheckBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.VSBuildConfiglabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProjectConfigComboBox);
            this.Controls.Add(this.configurationList);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSharpNuget_Generator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Nuget Generator";
            this.Load += new System.EventHandler(this.Nuget_Generator_Load);
            this.Click += new System.EventHandler(this.Nuget_Generator_Click);
            this.tabControl.ResumeLayout(false);
            this._NuGetSpec.ResumeLayout(false);
            this._NuGetSpec.PerformLayout();
            this.Runtime_Dependencies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._runtime)).EndInit();
            this.References.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._reference)).EndInit();
            this.ObfuscationPg.ResumeLayout(false);
            this.ObfuscationPg.PerformLayout();
            this.obfuscationDependency.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.obfuscationDataGridView)).EndInit();
            this._selectCommonConfig.ResumeLayout(false);
            this._selectCommonConfig.PerformLayout();
            this._logPage.ResumeLayout(false);
            this._selectforAllConfig.ResumeLayout(false);
            this._obfusResult.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button generate_Nuget;
        private System.Windows.Forms.ComboBox configurationList;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.ComboBox ProjectConfigComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label VSBuildConfiglabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox EnableMultiCheckBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage _NuGetSpec;
        private System.Windows.Forms.Label VersionPreviewLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button browse_RuntimeFolder;
        private System.Windows.Forms.RichTextBox descriptionPad;
        private System.Windows.Forms.RichTextBox ReleasePad;
        private System.Windows.Forms.TextBox fourthDigit;
        private System.Windows.Forms.Button Remove_reference;
        private System.Windows.Forms.Button Remove_runtime;
        private System.Windows.Forms.Button browse_RunTime;
        private System.Windows.Forms.Button browse_Dependencies;
        private System.Windows.Forms.GroupBox Runtime_Dependencies;
        private System.Windows.Forms.DataGridView _runtime;
        private System.Windows.Forms.GroupBox References;
        private System.Windows.Forms.DataGridView _reference;
        private System.Windows.Forms.TextBox TargetPath;
        private System.Windows.Forms.ComboBox frameworkDropDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button OutputPath_Browser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nugetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage ObfuscationPg;
        private System.Windows.Forms.GroupBox _selectCommonConfig;
        private System.Windows.Forms.TextBox textbox_RepoPath;
        private System.Windows.Forms.Button commonRepoPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button commonRepoOutPutDir;
        private System.Windows.Forms.TextBox textbox_RepoOutPutDir;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button commonRepoDllPath;
        private System.Windows.Forms.TextBox textbox_RepoDllPath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage _logPage;
        private System.Windows.Forms.TabPage _obfusResult;
        private System.Windows.Forms.GroupBox _selectforAllConfig;
        private System.Windows.Forms.RichTextBox _log;
        private System.Windows.Forms.CheckBox _Obfuscate;
        private System.Windows.Forms.Label DescriptionAbtNuGetGenerator;
        private System.Windows.Forms.Button RemoveObfuscateDependency;
        private System.Windows.Forms.Button AddObfuscateDependency;
        private System.Windows.Forms.GroupBox obfuscationDependency;
        private System.Windows.Forms.DataGridView obfuscationDataGridView;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox support_CLR;
        private System.Windows.Forms.TreeView ValidateNuGetTree;
        private System.Windows.Forms.TextBox textBox_owner;
        private System.Windows.Forms.Label Owner;
        private System.Windows.Forms.TextBox textBox_author;
        private System.Windows.Forms.Label author;
        private System.Windows.Forms.Button button1;
    }
}

