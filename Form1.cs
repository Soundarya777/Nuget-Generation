using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Utilities;

namespace CSharpNugetGenerator
{
    public partial class CSharpNuget_Generator : Form
    {
        private void WriteToConsoleWindow(string what, Color color)
        {
            _log.Select(_log.TextLength, 0);
            _log.SelectionColor = color;
            _log.AppendText(what);
            _log.AppendText(Environment.NewLine);
        }

        private void ClearConsoleWindow()
        {
            _log.Text = string.Empty;
        }
        private DataTable runtimeDataTable = null;
        private DataTable referenceDataTable = null;
        private DataTable ObfuscationDataTable = null;

        private Dictionary<string, string> PreviousConfiguration = new Dictionary<string, string>();
        private MetaData metaData = null;
        private static string logMessage = string.Empty;

        public void LoadNuConfig(string pattern, string startupText)
        {
            PreviousConfiguration.Clear();
            CommonFunctions.BackToDefaultProjectConfigComboBox(configurationList, startupText);
            CommonFunctions.LoadAllFilesInDirectory(PreviousConfiguration, metaData.ConfigPath, pattern);
            PreviousConfiguration.ToList().ForEach(x =>
            {
                configurationList.Items.Add(x.Key);
            });
        }

        public void UIStyles()
        {
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = Props.DateFormat_YMD;
            VSBuildConfiglabel.Visible = false;
            ProjectConfigComboBox.Visible = false;
            progressBar.Visible = false;

            configurationList.DropDownStyle = ComboBoxStyle.DropDownList;
            frameworkDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
            ProjectConfigComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LoadToolTip();
        }

        public CSharpNuget_Generator()
        {
            InitializeComponent();
            metaData = new MetaData();
            InitialiseDataTable();

            UIStyles();

            metaData.ConfigPath = FileSystemUtils.GetConfigPath(Props.NugetConfigurations_Folder);

            FileSystemUtils.CreateDirectory(metaData.ConfigPath);
            LoadNuConfig(Extensions.NuConfigExtension, Props.SelectConfig);

            if (!EnableMultiCheckBox.Checked)
            {
                SetData(Props.DefaultConfig);
            }

            metaData.TargetFramework = frameworkDropDown.Text;

        }

        private void InitialiseDataTable()
        {
            referenceDataTable = new DataTable();
            referenceDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
            _reference.DataSource = referenceDataTable;

            runtimeDataTable = new DataTable();
            runtimeDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
            _runtime.DataSource = runtimeDataTable;


            ObfuscationDataTable = new DataTable();
            ObfuscationDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
            obfuscationDataGridView.DataSource = ObfuscationDataTable;

        }

        private void LoadToolTip()
        {
            CommonFunctions.LoadToolTip(References, "The assemblies that needs to be referred inside the CsProj should be added here. " + Environment.NewLine +
                "In other words the dll added here will be directly added in the Reference in the project");
            _reference.ShowCellToolTips = false;
            CommonFunctions.LoadToolTip(_reference, "The assemblies that needs to be referred inside the CsProj should be added here. " + Environment.NewLine +
              "In other words the dll added here will be directly added in the Reference in the project");
            CommonFunctions.LoadToolTip(Runtime_Dependencies, "The assemblies that needs to be present in the output folder which is required during runtime, " + Environment.NewLine +
                "Example the Native C++ dll References, reference of reference dll should be added here. " + Environment.NewLine +
                "These Dlls will not be added in the refernce section but will be copied to the output directory directly during compile time");
            _runtime.ShowCellToolTips = false;
            CommonFunctions.LoadToolTip(_runtime, "The assemblies that needs to be present in the output folder which is required during runtime, " + Environment.NewLine +
                "Example the Native C++ dll References, reference of reference dll should be added here. " + Environment.NewLine +
                "These Dll will not be added in the refernce section but will be copied to the output directory directly during compile time");
            CommonFunctions.LoadToolTip(Save, "Save NuGet configuration for future use");
            CommonFunctions.LoadToolTip(generate_Nuget, "Generate NuGet");
            CommonFunctions.LoadToolTip(OutputPath_Browser, "Browse NuGet output folder");
            CommonFunctions.LoadToolTip(browse_Dependencies, "Browse Reference files");
            CommonFunctions.LoadToolTip(browse_RunTime, "Browse Runtime Dependency files");
            CommonFunctions.LoadToolTip(Remove_reference, "Remove selected reference files");
            CommonFunctions.LoadToolTip(Remove_runtime, "Remove selected Runtime dependency files");
            CommonFunctions.LoadToolTip(configurationList, "Select Configurations");
            CommonFunctions.LoadToolTip(ProjectConfigComboBox, "Select Project Configurations");
            CommonFunctions.LoadToolTip(frameworkDropDown, "Select FrameWork");
            CommonFunctions.LoadToolTip(frameworkDropDown, "Select FrameWork");
            CommonFunctions.LoadToolTip(tabControl, "Navigate through tabs to fill and specifications");
            CommonFunctions.LoadToolTip(_NuGetSpec, "NuGet Specification page");
            CommonFunctions.LoadToolTip(ObfuscationPg, "Obfuscation page");
            CommonFunctions.LoadToolTip(_logPage, "Log window");
            CommonFunctions.LoadToolTip(_Obfuscate, "Enable Obfuscation, this creates the NuGet with obfuscated Dlls. ");
            CommonFunctions.LoadToolTip(commonRepoDllPath, "Browse through SVN repo Browser,and choose dll path for obfuscation.");
            CommonFunctions.LoadToolTip(commonRepoPath, "Browse through SVN repo Browser,and choose Obproj for obfuscation.");
            CommonFunctions.LoadToolTip(commonRepoPath, "Browse through SVN repo Browser,and choose obfuscation Output directory.");


            DescriptionAbtNuGetGenerator.Text = //"Instructions:"
            //    + Environment.NewLine
            //    + Environment.NewLine+
               "* All Dlls Mentioned in the references gets obfuscated based on the .obproj mentioned respectively."
                + Environment.NewLine
                 + Environment.NewLine
                + "* NuGet Generated will contain obfuscated Dlls."
                + Environment.NewLine
                 + Environment.NewLine
                + "* Obfuscation settings to be provided based on VS Build Config."
                + Environment.NewLine
                 + Environment.NewLine
                + "* ObProj File mentioned will be saved in config.";

        }

        private void browse_Dependencies_Click(object sender, EventArgs e)
        {
            ProjectConfig projectConfig = null;
            projectConfig = GetProjectConfigBasedOnRequest();

            if (projectConfig != null)
            {
                List<string> listOfFiles = CommonFunctions.OpenFileDialog("Dynamic Link Library (*.DLL)|*.dll", "Dependencies Browser", true);

                if (listOfFiles != null)
                {
                    CommonFunctions.LoadFilesInDatatable(listOfFiles, projectConfig.ReferenceDataTable);
                    _reference.DataSource = projectConfig.ReferenceDataTable;
                    CommonFunctions.WarnDeletedFiles(_reference.Rows);
                    _reference.Columns[0].Width = 550;
                    ClearDataTableSelection();
                }
            }
            else
            {
                MessageBox.Show(PopUP_Messages.Create_New_ProjConfig);
            }

        }

        private ProjectConfig GetProjectConfigBasedOnRequest()
        {
            ProjectConfig projectConfig;
            if (EnableMultiCheckBox.Checked)
            {
                projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, ProjectConfigComboBox.Text);
            }
            else
            {
                if (!EnableMultiCheckBox.Checked)
                {
                    if (!metaData.ProjectConfigs.ContainsKey(Props.DefaultConfig))
                    {
                        AddProjectConfig(Props.DefaultConfig);
                    }
                }

                projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, Props.DefaultConfig);

            }

            return projectConfig;
        }

        private void browse_RunTime_Click(object sender, EventArgs e)
        {
            ProjectConfig projectConfig = null;

            projectConfig = GetProjectConfigBasedOnRequest();

            if (projectConfig != null)
            {
                List<string> listOfFiles = CommonFunctions.OpenFileDialog("All files (*.*)|*.*", "RunTime Browser", true);

                if (listOfFiles != null)
                {
                    BindRuntimeFilesToDataTable(projectConfig, listOfFiles);
                }
            }
            else
            {
                MessageBox.Show(PopUP_Messages.Create_New_ProjConfig);
            }
        }

        private void BindRuntimeFilesToDataTable(ProjectConfig projectConfig, List<string> listOfFiles)
        {
            CommonFunctions.LoadFilesInDatatable(listOfFiles, projectConfig.RuntimeDataTable);
            _runtime.DataSource = projectConfig.RuntimeDataTable;
            CommonFunctions.WarnDeletedFiles(_runtime.Rows);
            _runtime.Columns[0].Width = 550;
            ClearDataTableSelection();
        }

        private void browse_RuntimeFolder_Click(object sender, EventArgs e)
        {
            ProjectConfig projectConfig = null;

            projectConfig = GetProjectConfigBasedOnRequest();

            if (projectConfig != null)
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();

                dialog.Multiselect = true;
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    List<string> listOfFiles = dialog.FileNames.ToList();// CommonFunctions.OpenFileDialog("All files (*.*)|*.*", "RunTime Browser", true);

                    if (listOfFiles != null)
                    {
                        BindRuntimeFilesToDataTable(projectConfig, listOfFiles);
                    }
                }
            }
            else
            {
                MessageBox.Show(PopUP_Messages.Create_New_ProjConfig);
            }
        }

        private bool ValidateTextBox()
        {
            if (UIValidator.EmptyValidation(TargetPath.Text) && UIValidator.EmptyValidation(nugetName.Text) && UIValidator.EmptyValidation(fourthDigit.Text) && UIValidator.EmptyValidation(author.Text) && UIValidator.EmptyValidation(Owner.Text) && UIValidator.EmptyValidation(descriptionPad.Text) && UIValidator.EmptyValidation(ReleasePad.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SetProgressBar(int value)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = value;
            return true;
        }

        private bool DoesObfuscationSettingFilled(ProjectConfig config)
        {
            if (config.Obproj_DLL_Directory.Text != string.Empty && config.Obproj_File.Text != string.Empty && config.ObProj_OutPut_Directory.Text != string.Empty)
            {
                return true;
            }
            return false;
        }

        private void CopyDllToObfuscateDirectory()
        {
            foreach (var config in metaData.ProjectConfigs)
            {
                string ObfusDir = metaData.WorkingDirectory + Extensions.FolderSeperator + config.Key;
                FileSystemUtils.CreateDirectory(ObfusDir);
                CopyReferenceDlls(config, ObfusDir);
                CopyRuntimeDlls(config, ObfusDir);
                CopyObfuscationRuntimeDlls(config, ObfusDir);
            }
        }

        private static void CopyObfuscationRuntimeDlls(KeyValuePair<string, ProjectConfig> config, string targetDir)
        {
            foreach (DataRow row in config.Value.ObfucationRuntimeDataTable.Rows)
            {
                string fullfilePath = row[Props.BasicInfo] as string;
                if (File.Exists(fullfilePath))
                {
                    File.Copy(fullfilePath, targetDir + Extensions.FolderSeperator + Path.GetFileName(fullfilePath), true);
                }
            }
        }

        private static void CopyRuntimeDlls(KeyValuePair<string, ProjectConfig> config, string targetDir)
        {
            foreach (DataRow row in config.Value.RuntimeDataTable.Rows)
            {
                string fullfilePath = row[Props.BasicInfo] as string;

                if (File.Exists(fullfilePath))
                {
                    File.Copy(fullfilePath, targetDir + Extensions.FolderSeperator + Path.GetFileName(fullfilePath), true);
                }
                else if (Directory.Exists(fullfilePath))
                {
                    FileSystemUtils.CopyDirectory(fullfilePath, targetDir + Extensions.FolderSeperator + Path.GetFileName(fullfilePath));
                }
            }
        }

        private static void CopyReferenceDlls(KeyValuePair<string, ProjectConfig> config, string targetDir)
        {
            foreach (DataRow row in config.Value.ReferenceDataTable.Rows)
            {
                string fullfilePath = row[Props.BasicInfo] as string;
                if (File.Exists(fullfilePath))
                {
                    File.Copy(fullfilePath, targetDir + Extensions.FolderSeperator + Path.GetFileName(fullfilePath), true);
                }
            }
        }

        public bool IsProjectConfigValid()
        {
            if (metaData.ProjectConfigs.Count == 0)
            {
                MessageBox.Show(PopUP_Messages.Add_New_ProjConfig);
                return false;
            }


            foreach (var proj in metaData.ProjectConfigs)
            {
                if (proj.Value.ReferenceDataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No Reference Library added for - " + proj.Key + " config ");
                    return false;
                }

                if (_Obfuscate.Checked)
                {
                    if (!DoesObfuscationSettingFilled(proj.Value))
                    {
                        MessageBox.Show("Obfuscation " + PopUP_Messages.Specifications_Cannot_Be_Empty + " for - " + proj.Key + " config ");
                        return false;
                    }
                }
            }
            return true;
        }

        private void ObfuscateDlls()
        {
            foreach (var item in metaData.ProjectConfigs)
            {
                //ClientUtility clintUtility = new ClientUtility();
                //clintUtility.targetDllsDir = metaData.WorkingDirectory + Extensions.FolderSeperator + item.Key;
                //clintUtility.objProjPath = CommonFunctions.RemoveDagPath( item.Value.Obproj_File.Text);
                //clintUtility.objProjDllPath = CommonFunctions.RemoveDagPath(item.Value.Obproj_DLL_Directory.Text);
                //clintUtility.outputpath = CommonFunctions.RemoveDagPath(item.Value.ObProj_OutPut_Directory.Text);
                //clintUtility.InitiateProcess();
                WriteToConsoleWindow("Attempting to Obfuscate " + item.Key + " config", Color.Blue);

                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = "Obfuscator_Automation.exe";//@"D:\SVN\Sampath\SSTInHouseTools\trunk\NugetGenerator\CSharpNugetGenerator\bin\x64\Debug\Obfuscator_Automation.exe";
                process.StartInfo.Arguments = metaData.WorkingDirectory + Extensions.FolderSeperator + item.Key

                    + Extensions.StarSeperator

                    + CommonFunctions.RemoveDagPath(item.Value.Obproj_File.Text)

                    + Extensions.StarSeperator

                    + CommonFunctions.RemoveDagPath(item.Value.Obproj_DLL_Directory.Text)

                    + Extensions.StarSeperator

                    + CommonFunctions.RemoveDagPath(item.Value.ObProj_OutPut_Directory.Text);

                process.StartInfo.RedirectStandardOutput = true;
                process.OutputDataReceived += new DataReceivedEventHandler(MyProcOutputHandler);
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();

                WriteToConsoleWindow(logMessage, Color.Black);
                WriteToConsoleWindow(Environment.NewLine, Color.Black);
            }
        }

        private bool CheckNecessaryTextBox()
        {
            if (UIValidator.ValidateVersion(fourthDigit))
            {
                MessageBox.Show(PopUP_Messages.Invalid_Version);
                return false;
            }

            if (!Regex.IsMatch(nugetName.Text, @"^[a-zA-Z0-9_-]+$"))
            {
                MessageBox.Show(PopUP_Messages.Nuget_Name_Constrain);
                return false;
            }

            if (!Path.IsPathRooted(TargetPath.Text))
            {
                MessageBox.Show(PopUP_Messages.Enter_Valid_Target_Directory);
                return false;
            }
            return true;
        }

        private void VerifyNuget(string nugetPath)
        {

        }

        string loadedFilePath;

        private void button1_Click_1(object sender, EventArgs e)
        {
            loadedFilePath = string.Empty;
            string filepath = GetFilePath();
            if (!string.IsNullOrEmpty(filepath))
            {
                loadedFilePath = filepath;
                LoadNuConfig();
            }
        }

        public void LoadNuConfig()
        {
            PreviousConfiguration.Clear();
            CommonFunctions.BackToDefaultProjectConfigComboBox(configurationList, Props.SelectConfig);
            CommonFunctions.LoadFile(PreviousConfiguration, loadedFilePath);
            PreviousConfiguration.ToList().ForEach(x =>
            {
                configurationList.Items.Add(x.Key);
            });
        }

        private string GetFilePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "nuxconfig files (*.nuxconfig)|*.nuxconfig|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.DefaultExt = "nuxconfig";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return string.Empty;
        }

        private string GetSaveasFilePath()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "nuxconfig files (*.nuxconfig)|*.nuxconfig|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.DefaultExt = "nuxconfig";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }
            return string.Empty;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTextBox())
                {
                    loadedFilePath = string.Empty;
                    string filepath = GetSaveasFilePath();
                    if (!string.IsNullOrEmpty(filepath))
                    {
                        loadedFilePath = filepath;
                        //BindVersionName();
                        WriteConfig(loadedFilePath);
                        AddconfigNames(loadedFilePath);
                    }
                }
                else
                {
                    MessageBox.Show(PopUP_Messages.Specifications_Cannot_Be_Empty);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void generate_Nuget_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateNuGetTree.Nodes.Clear();
                if (!IsProjectConfigValid())
                    return;

                if (ValidateTextBox())
                {
                    if (!CheckNecessaryTextBox())
                        return;

                    if (string.IsNullOrEmpty(loadedFilePath))
                        GetSaveasFilePath();
                    if (string.IsNullOrEmpty(loadedFilePath))
                        return;

                    tabControl.SelectedIndex = 2;
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressBar.MarqueeAnimationSpeed = 100;

                    ClearConsoleWindow();
                    WriteToConsoleWindow("Building NuGet...", Color.Blue);
                    BindDetailsToMetaData();

                    FileSystemUtils.CreateDirectory(metaData.WorkingDirectory);
                    WriteToConsoleWindow("Updating NuGet Config...", Color.Black);

                    WriteConfig(loadedFilePath);
                    AddconfigNames(loadedFilePath);

                    WriteToConsoleWindow("NuGet Config Updated.", Color.Green);
                    WriteToConsoleWindow("Copying files to working Directory...", Color.Black);
                    CopyDllToObfuscateDirectory();

                    //if (_Obfuscate.Checked)
                    //{
                    //    ObfuscateDlls();
                    //}

                    WriteToConsoleWindow("Updating Nuspec...", Color.Black);
                    string nuspecFullFileName = WriteNuspec();
                    WriteToConsoleWindow("Nuspec Updated.", Color.Green);
                    WriteToConsoleWindow(CommonFunctions.PackNuget(nuspecFullFileName), Color.Black);
                    FileSystemUtils.DeleteFolder(metaData.WorkingDirectory);

                    if (File.Exists(nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension))
                    {
                        File.Copy(nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension, metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension, true);
                        File.Delete(nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension);
                    }
                    else
                    {

                        var direc = Directory.GetCurrentDirectory();
                        if (File.Exists(direc + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension))
                        {
                            File.Copy(direc + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension, metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension, true);
                            File.Delete(direc + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension);
                        }
                    }

                    VerifyNuget(metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension);
                    WriteToConsoleWindow("NuGet Created successfully..!!!", Color.Green);
                    WriteToConsoleWindow(metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension, Color.Blue);
                }
                else
                {
                    progressBar.MarqueeAnimationSpeed = 0;
                    MessageBox.Show(PopUP_Messages.Specifications_Cannot_Be_Empty);
                    progressBar.Visible = false;
                    WriteToConsoleWindow("NuGet creation failed..!!!", Color.Red);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteToConsoleWindow("NuGet creation failed..!!!", Color.Red);
            }
            progressBar.MarqueeAnimationSpeed = 0;
            progressBar.Visible = false;
        }

        private void BindDetailsToMetaData()
        {
            metaData.OutputPath = TargetPath.Text;
            metaData.WorkingDirectory = metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + Guid.NewGuid();
            metaData.Version = VersionPreviewLabel.Text;
            metaData.Authors = textBox_author.Text;
            metaData.ID = nugetName.Text;
            metaData.Owners = Owner.Text;
            metaData.Description = descriptionPad.Text;
            metaData.ReleaseNotes = ReleasePad.Text;
        }

        //private string  BindVersionName()
        //{
        //   // MyversionName = VersionPreviewLabel.Text;
        //   return VersionPreviewLabel.Text;
        //}

        private void AddconfigNames(string configFilePath)
        {
            if (!PreviousConfiguration.ContainsKey(Path.GetFileNameWithoutExtension(configFilePath)))
            {
                PreviousConfiguration.Add(Path.GetFileNameWithoutExtension(configFilePath), configFilePath);
                MessageBox.Show(PopUP_Messages.Config_Saved);
                configurationList.Items.Add(Path.GetFileNameWithoutExtension(configFilePath));
                configurationList.Text = Path.GetFileNameWithoutExtension(configFilePath);
            }
        }

        private string WriteNuspec()
        {
            string nuspec = SerialiseNuspec();

            foreach (var item in metaData.ProjectConfigs)
            {
                FileSystemUtils.DeleteFolder(metaData.WorkingDirectory + Extensions.FolderSeperator + item.Key);
            }


            string nuspecFullFileName = metaData.WorkingDirectory + Extensions.FolderSeperator + nugetName.Text + Extensions.NuspecExtension;
            File.WriteAllText(nuspecFullFileName, nuspec);
            return nuspecFullFileName;
        }

        private string WriteConfig(string configFilePath)
        {
            StringBuilder configBuilder = new StringBuilder();
            XmlWriter configWriter = XmlWriter.Create(configBuilder, CommonFunctions.GetXMLSettings());

            configWriter.WriteStartElement("Metadata");
            configWriter.WriteElementString("id", nugetName.Text);
            configWriter.WriteElementString("name", metaData.Name);
            configWriter.WriteElementString("version", metaData.Version);
            configWriter.WriteElementString("authors", textBox_author.Text);
            configWriter.WriteElementString("owners", textBox_owner.Text);
            configWriter.WriteElementString("requireLicenseAcceptance", "false");
            configWriter.WriteElementString("description", descriptionPad.Text);
            configWriter.WriteElementString("releaseNotes", ReleasePad.Text);
            configWriter.WriteElementString("copyright", "Copyright 2019");
            configWriter.WriteElementString("targetFramework", metaData.TargetFramework);
            configWriter.WriteElementString("OutputPath", TargetPath.Text);
            configWriter.WriteStartElement("ProjectConfigs");


            foreach (var projects in metaData.ProjectConfigs)
            {

                configWriter.WriteStartElement(projects.Key);

                configWriter.WriteStartElement("Reference");
                foreach (DataRow row in projects.Value.ReferenceDataTable.Rows)
                {
                    string fullfilePath = row[Props.BasicInfo] as string;
                    configWriter.WriteElementString("string", fullfilePath);
                }
                configWriter.WriteEndElement();//Reference


                configWriter.WriteStartElement("Runtime");
                foreach (DataRow row in projects.Value.RuntimeDataTable.Rows)
                {
                    string fullfilePath = row[Props.BasicInfo] as string;
                    configWriter.WriteElementString("string", fullfilePath);
                }
                configWriter.WriteEndElement();//Runtime

                configWriter.WriteStartElement("ObfuscationRuntime");
                foreach (DataRow row in projects.Value.ObfucationRuntimeDataTable.Rows)
                {
                    string fullfilePath = row[Props.BasicInfo] as string;
                    configWriter.WriteElementString("string", fullfilePath);
                }

                configWriter.WriteEndElement();//Runtime



                configWriter.WriteElementString("Obproj_File", projects.Value.Obproj_File.Text);
                configWriter.WriteElementString("Obproj_DLL_Directory", projects.Value.Obproj_DLL_Directory.Text);
                configWriter.WriteElementString("ObProj_OutPut_Directory", projects.Value.ObProj_OutPut_Directory.Text);

                configWriter.WriteEndElement();//ConfigName
            }

            configWriter.WriteEndElement();//ProjectConfigs
            configWriter.WriteEndElement();//Metadata
            configWriter.Close();

            var nuconfig = configBuilder.ToString();

            File.WriteAllText(configFilePath, nuconfig);

            return configFilePath;
        }

        private string SerialiseNuspec()
        {
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, CommonFunctions.GetXMLSettings());

            xmlWriter.WriteStartElement("package");
            xmlWriter.WriteStartElement("metadata");

            xmlWriter.WriteElementString("id", nugetName.Text);
            xmlWriter.WriteElementString("version", metaData.Version);
            xmlWriter.WriteElementString("authors", textBox_author.Text);
            xmlWriter.WriteElementString("owners", textBox_owner.Text);
            xmlWriter.WriteElementString("requireLicenseAcceptance", "false");
            xmlWriter.WriteElementString("description", descriptionPad.Text);
            xmlWriter.WriteElementString("releaseNotes", ReleasePad.Text);
            xmlWriter.WriteElementString("copyright", "Copyright 2019");

            xmlWriter.WriteStartElement("dependencies");
            xmlWriter.WriteStartElement("group");
            xmlWriter.WriteAttributeString("targetFramework", ".NETFramework" + metaData.TargetFramework); //NB: a value given as an attribute name (consider: WriteAttributeString)!

            xmlWriter.WriteEndElement(); //group
            xmlWriter.WriteEndElement(); //dependencies

            if (metaData.ProjectConfigs.Values.All(x => x.ReferenceDataTable.Rows.Count > 0))
            {
                BindReferncesDlls(xmlWriter);
                BindRuntimeDlls(xmlWriter);
                // xmlWriter.WriteEndElement(); //files
            }

            xmlWriter.WriteEndElement(); //metadata
            xmlWriter.WriteEndElement(); //package
            xmlWriter.Close();

            var nuspec = stringBuilder.ToString();
            return nuspec;
        }

        private void BindReferncesDlls(XmlWriter xmlWriter)
        {
            string libPath = string.Empty;

            if (EnableMultiCheckBox.Checked)
            {
                libPath = metaData.WorkingDirectory + "\\Pack\\";// + GetFrameWorkName();
            }
            else
            {
                if (support_CLR.Checked)
                {
                    libPath = metaData.WorkingDirectory + "\\Pack\\";// + GetFrameWorkName();
                }
                else
                {
                    libPath = metaData.WorkingDirectory + "\\lib\\" + CommonFunctions.GetFrameWorkName(metaData.TargetFramework);
                }
            }

            FileSystemUtils.CreateDirectory(libPath);

            foreach (var config in metaData.ProjectConfigs)
            {
                string defaultDir = string.Empty;

                if (EnableMultiCheckBox.Checked)
                {
                    defaultDir = libPath + Extensions.FolderSeperator + config.Key;
                }
                else
                {
                    if (support_CLR.Checked)
                    {
                        defaultDir = libPath + Extensions.FolderSeperator + config.Key;
                    }
                    else
                    {
                        defaultDir = libPath;
                    }
                }

                FileSystemUtils.CreateDirectory(defaultDir);

                string LibNames = string.Empty;
                //foreach (DataRow row in config.Value.ReferenceDataTable.Rows)
                //{
                //    string fullfilePath = row[Props.BasicInfo] as string;

                //    // CopyFiles(_reference.Rows, libPath);

                //    if (File.Exists(fullfilePath))
                //    {
                //        File.Copy(fullfilePath, Path.Combine(defaultDir, Path.GetFileName(fullfilePath)), true);
                //        LibNames = LibNames + Path.GetFileName(fullfilePath) + ";";
                //    }
                //}



                foreach (DataRow row in config.Value.ReferenceDataTable.Rows)
                {
                    string fullfilePath = row[Props.BasicInfo] as string;

                    var obfusDir = metaData.WorkingDirectory + Extensions.FolderSeperator + config.Key;

                    foreach (var Obfusfile in Directory.GetFiles(obfusDir))
                    {
                        if (Path.GetFileName(Obfusfile) == Path.GetFileName(fullfilePath))
                        {
                            if (File.Exists(Obfusfile))
                            {
                                File.Copy(Obfusfile, Path.Combine(defaultDir, Path.GetFileName(Obfusfile)), true);
                                LibNames = LibNames + Path.GetFileName(fullfilePath) + ";";
                            }
                        }
                    }
                }
            }
        }

        private void BindRuntimeDlls(XmlWriter xmlWriter)
        {
            string runtime = string.Empty;

            if (metaData.ProjectConfigs.All(x => x.Value.RuntimeDataTable.Rows.Count > 0))
            {
                runtime = metaData.WorkingDirectory + "\\runtimes\\win-x64";

                foreach (var config in metaData.ProjectConfigs)
                {
                    string defaultDir = string.Empty;

                    if (EnableMultiCheckBox.Checked)
                    {
                        defaultDir = runtime + Extensions.FolderSeperator + config.Key;
                    }
                    else
                    {
                        if (support_CLR.Checked)
                        {
                            defaultDir = runtime + Extensions.FolderSeperator + config.Key;
                        }
                        else
                        {
                            defaultDir = runtime;
                        }
                    }

                    FileSystemUtils.CreateDirectory(defaultDir);

                    foreach (DataRow row in config.Value.RuntimeDataTable.Rows)
                    {
                        string fullfilePath = row[Props.BasicInfo] as string;

                        if (File.Exists(fullfilePath))
                        {
                            var obfusDir = metaData.WorkingDirectory + Extensions.FolderSeperator + config.Key;

                            foreach (var Obfusfile in Directory.GetFiles(obfusDir))
                            {
                                if (Path.GetFileName(Obfusfile) == Path.GetFileName(fullfilePath))
                                {
                                    if (File.Exists(Obfusfile))
                                    {
                                        File.Copy(Obfusfile, Path.Combine(defaultDir, Path.GetFileName(Obfusfile)), true);
                                    }
                                    else if (Directory.Exists(Obfusfile))
                                    {
                                        FileSystemUtils.CopyDirectory(Obfusfile, Path.Combine(defaultDir, Path.GetFileName(Obfusfile)));
                                    }
                                }
                            }
                        }
                        else if (Directory.Exists(fullfilePath))
                        {
                            FileSystemUtils.CopyDirectory(fullfilePath, Path.Combine(defaultDir, Path.GetFileName(fullfilePath)));
                        }

                        //if (File.Exists(fullfilePath))
                        //{
                        //    File.Copy(fullfilePath, Path.Combine(defaultDir, Path.GetFileName(fullfilePath)), true);
                        //}
                        //else if (Directory.Exists(fullfilePath))
                        //{
                        //    FileSystemUtils.CopyDirectory(fullfilePath, Path.Combine(defaultDir, Path.GetFileName(fullfilePath)));
                        //}
                    }
                }
            }

            BindBuildTargets(xmlWriter);

        }

        private void BindBuildTargets(XmlWriter xmlWriter)
        {
            string buildPath = metaData.WorkingDirectory + Extensions.FolderSeperator + "build";

            FileSystemUtils.CreateDirectory(buildPath);

            if (EnableMultiCheckBox.Checked)
            {
                File.WriteAllText(buildPath + Extensions.FolderSeperator + nugetName.Text + Extensions.TargetExtension, SerialiseTargets());
                File.WriteAllText(buildPath + Extensions.FolderSeperator + nugetName.Text + Extensions.PropsExtension, Serialiseprops());
            }
            else
            {
                if (support_CLR.Checked)
                {
                    File.WriteAllText(buildPath + Extensions.FolderSeperator + nugetName.Text + Extensions.TargetExtension, SerialiseTargets());
                    File.WriteAllText(buildPath + Extensions.FolderSeperator + nugetName.Text + Extensions.PropsExtension, Serialiseprops());
                }
                else
                {
                    File.WriteAllText(buildPath + Extensions.FolderSeperator + nugetName.Text + Extensions.TargetExtension, SerialiseTargets());
                }
            }
        }

        private string SerialiseTargets()
        {
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, CommonFunctions.GetXMLSettings());
            xmlWriter.WriteStartElement("Project");
            xmlWriter.WriteAttributeString("ToolsVersion", "4.0");
            //  xmlWriter.WriteAttributeString("xmlns", @"https:///schemas.microsoft.com/developer/msbuild/2003"); //NB: a value given as an attribute name (consider: WriteAttributeString)!

            foreach (var configs in metaData.ProjectConfigs)
            {
                xmlWriter.WriteStartElement("ItemGroup");
                WriteToConsoleWindow("Serialising Targets for " + configs.Key + "|x64'", Color.Black);

                if (EnableMultiCheckBox.Checked)
                {
                    if (!support_CLR.Checked)
                    {
                        xmlWriter.WriteAttributeString("Condition", "'$(Configuration)|$(Platform)'=='" + configs.Key + "|x64'");
                    }
                }
                //else
                //{
                //    xmlWriter.WriteAttributeString("Condition", "'$(Platform)'=='x64'");
                //}

                foreach (DataRow row in configs.Value.RuntimeDataTable.Rows)
                {
                    string fullfilePath = row[Props.BasicInfo] as string;
                    string initialDir = string.Empty;

                    if (EnableMultiCheckBox.Checked)
                    {
                        initialDir = @"$(MSBuildThisFileDirectory)..\runtimes\win-x64\" + configs.Key + Extensions.FolderSeperator;
                    }
                    else
                    {
                        if (support_CLR.Checked)
                        {
                            initialDir = @"$(MSBuildThisFileDirectory)..\runtimes\win-x64\" + configs.Key + Extensions.FolderSeperator;
                        }
                        else
                        {
                            initialDir = @"$(MSBuildThisFileDirectory)..\runtimes\win-x64\";
                        }
                    }

                    if (File.Exists(fullfilePath))
                    {
                        if (Path.GetExtension(fullfilePath) != Extensions.NupkgExtension)
                        {
                            // if (Path.GetFileNameWithoutExtension(fullfilePath)[0] == '.')
                            {
                                BindTargetIncludes(string.Empty, xmlWriter, configs, initialDir, fullfilePath);
                            }
                        }
                    }
                    else if (Directory.Exists(fullfilePath))
                    {
                        IterateRuntime(string.Empty, initialDir, fullfilePath, xmlWriter, configs);
                    }
                }

                if (EnableMultiCheckBox.Checked)
                {
                    foreach (DataRow row in configs.Value.ReferenceDataTable.Rows)
                    {
                        xmlWriter.WriteStartElement("Content");
                        string fullfilePath = row[Props.BasicInfo] as string;
                        xmlWriter.WriteAttributeString("Include", @"$(MSBuildThisFileDirectory)..\Pack\" + configs.Key + Extensions.FolderSeperator + Path.GetFileName(fullfilePath));
                        xmlWriter.WriteElementString("Link", Path.GetFileName(fullfilePath));
                        xmlWriter.WriteElementString("CopyToOutputDirectory", "PreserveNewest");
                        xmlWriter.WriteElementString("Pack", "False");
                        xmlWriter.WriteEndElement();//Content
                    }

                }
                else if (support_CLR.Checked)
                {
                    foreach (DataRow row in configs.Value.ReferenceDataTable.Rows)
                    {
                        xmlWriter.WriteStartElement("Content");
                        string fullfilePath = row[Props.BasicInfo] as string;
                        xmlWriter.WriteAttributeString("Include", @"$(MSBuildThisFileDirectory)..\Pack\" + configs.Key + Extensions.FolderSeperator + Path.GetFileName(fullfilePath));
                        xmlWriter.WriteElementString("Link", Path.GetFileName(fullfilePath));
                        xmlWriter.WriteElementString("CopyToOutputDirectory", "PreserveNewest");
                        xmlWriter.WriteElementString("Pack", "False");
                        xmlWriter.WriteEndElement();//Content
                    }
                }


                xmlWriter.WriteEndElement();//ItemGroup

                WriteToConsoleWindow("Serialising Targets for " + configs.Key + "|x64'  --  Completed", Color.Black);

            }

            xmlWriter.WriteEndElement();//Project
            xmlWriter.Close();
            var target = stringBuilder.ToString();
            WriteToConsoleWindow("Build Target Serialised", Color.Green);

            return target;
        }

        private void IterateRuntime(string parentBinFolder, string initialDir, string fullfilePath, XmlWriter xmlWriter, KeyValuePair<string, ProjectConfig> configs)
        {

            var ParentFolder = Path.GetFileName(fullfilePath.TrimEnd(Path.DirectorySeparatorChar));

            string currentDir = initialDir + ParentFolder + Extensions.FolderSeperator;


            if (parentBinFolder != string.Empty)
            {
                parentBinFolder = parentBinFolder + Extensions.FolderSeperator + ParentFolder;
            }
            else
            {
                parentBinFolder = ParentFolder;
            }

            foreach (var files in Directory.GetFiles(fullfilePath))
            {
                BindTargetIncludes(parentBinFolder, xmlWriter, configs, currentDir, files);
            }
            foreach (var folder in Directory.GetDirectories(fullfilePath))
            {
                IterateRuntime(parentBinFolder, currentDir, folder, xmlWriter, configs);
            }
        }

        private void BindTargetIncludes(string parentBinFolder, XmlWriter xmlWriter, KeyValuePair<string, ProjectConfig> configs, string initialDir, string fullfilePath)
        {
            xmlWriter.WriteStartElement("Content");
            if (EnableMultiCheckBox.Checked)
            {
                xmlWriter.WriteAttributeString("Include", initialDir + Path.GetFileName(fullfilePath));
            }
            else
            {
                xmlWriter.WriteAttributeString("Include", initialDir + Path.GetFileName(fullfilePath));
            }


            if (parentBinFolder != string.Empty)
            {
                xmlWriter.WriteElementString("Link", parentBinFolder + Extensions.FolderSeperator + Path.GetFileName(fullfilePath));

            }
            else
            {
                xmlWriter.WriteElementString("Link", Path.GetFileName(fullfilePath));
            }

            xmlWriter.WriteElementString("CopyToOutputDirectory", "PreserveNewest");
            xmlWriter.WriteElementString("Pack", "False");
            xmlWriter.WriteEndElement();//Content

        }

        private string Serialiseprops()
        {
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, CommonFunctions.GetXMLSettings());
            xmlWriter.WriteStartElement("Project");
            xmlWriter.WriteAttributeString("ToolsVersion", "4.0");
            //  xmlWriter.WriteAttributeString("xmlns", @"https:///schemas.microsoft.com/developer/msbuild/2003"); //NB: a value given as an attribute name (consider: WriteAttributeString)!

            foreach (var configs in metaData.ProjectConfigs)
            {
                xmlWriter.WriteStartElement("ItemGroup");
                if (!support_CLR.Checked)
                {
                    xmlWriter.WriteAttributeString("Condition", "'$(Configuration)|$(Platform)'=='" + configs.Key + "|x64'");
                    WriteToConsoleWindow("Serialising Props for " + configs.Key + " | x64'", Color.Black);
                }

                foreach (DataRow row in configs.Value.ReferenceDataTable.Rows)
                {
                    xmlWriter.WriteStartElement("Reference");
                    string fullfilePath = row["BasicInfo"] as string;

                    xmlWriter.WriteAttributeString("Include", Path.GetFileNameWithoutExtension(fullfilePath));
                    xmlWriter.WriteElementString("SpecificVersion", "False");
                    xmlWriter.WriteElementString("HintPath", @"$(MSBuildThisFileDirectory)\..\Pack\" + configs.Key + Extensions.FolderSeperator + Path.GetFileName(fullfilePath));
                    xmlWriter.WriteElementString("Private", "false");
                    xmlWriter.WriteEndElement();//Reference
                }

                xmlWriter.WriteEndElement();//ItemGroup
                WriteToConsoleWindow("Serialising Props for " + configs.Key + " | x64' -- Completed", Color.Black);
            }

            xmlWriter.WriteEndElement();//Project
            xmlWriter.Close();
            var target = stringBuilder.ToString();

            WriteToConsoleWindow("Props Serialised", Color.Green);

            return target;
        }

        private bool CopyFiles(DataGridViewRowCollection RowCollection, string destinationFile)
        {
            foreach (DataGridViewRow row in RowCollection)
            {
                string fullfilePath = row.Cells[0].Value.ToString();
                string fileName = Path.GetFileName(fullfilePath);

                if (File.Exists(fullfilePath))
                {
                    File.Copy(fullfilePath, destinationFile + Extensions.FolderSeperator + Path.GetFileName(fileName), true);
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                metaData.OutputPath = folderBrowserDialog.SelectedPath;
                TargetPath.Text = folderBrowserDialog.SelectedPath;

            }
        }

        private void frameworkDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            metaData.TargetFramework = frameworkDropDown.Text;
        }

        private void Remove_reference_Click(object sender, EventArgs e)
        {
            ProjectConfig projectConfig = null;

            if (EnableMultiCheckBox.Checked)
            {
                projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, ProjectConfigComboBox.Text);
            }
            else
            {

                if (!EnableMultiCheckBox.Checked)
                {
                    if (metaData.ProjectConfigs.ContainsKey(Props.DefaultConfig))
                    {
                        projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, Props.DefaultConfig);
                    }
                }
            }

            if (projectConfig != null)
            {
                foreach (DataGridViewRow row in _reference.SelectedRows)
                {
                    int index = row.Index;
                    _reference.Rows.RemoveAt(index);
                }
            }
        }

        private void Remove_runtime_Click(object sender, EventArgs e)
        {

            ProjectConfig projectConfig = null;

            if (EnableMultiCheckBox.Checked)
            {
                projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, ProjectConfigComboBox.Text);
            }
            else
            {

                if (!EnableMultiCheckBox.Checked)
                {
                    if (metaData.ProjectConfigs.ContainsKey(Props.DefaultConfig))
                    {
                        projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, Props.DefaultConfig);
                    }
                }
            }
            if (projectConfig != null)
            {
                foreach (DataGridViewRow row in _runtime.SelectedRows)
                {
                    int index = row.Index;
                    _runtime.Rows.RemoveAt(index);
                }
            }
        }

        private void configurationList_SelectedValueChanged(object sender, EventArgs e)
        {
            ClearConsoleWindow();
            metaData.ProjectConfigs.Clear();

            BindCurrentDateTimeAsVersion(DateTime.Now);

            CommonFunctions.BackToDefaultProjectConfigComboBox(ProjectConfigComboBox, Props.SelectProjectConfig);

            if (configurationList.Text == Props.SelectConfig || configurationList.Text == Props.NewConfig)
            {
                if (configurationList.Text == Props.NewConfig)
                {
                    ConfigName l = new ConfigName();
                    l.ShowDialog();

                    string configName = ConfigName.Config;
                    if (configName != null)
                    {
                        metaData.Name = configName;
                        nugetName.Text = configName;
                    }
                    else
                    {
                        nugetName.Text = string.Empty;
                        metaData.Name = string.Empty;
                        configurationList.SelectedItem = Props.SelectConfig;
                    }
                }
                else
                {
                    metaData.Name = string.Empty;
                    nugetName.Text = string.Empty;
                }

                //firstDigit.Text = "1";
                //secondDigit.Text = "0";
                //thirdDigit.Text = "0";
                BindCurrentDateTimeAsVersion(DateTime.Now);

                ReleasePad.Text = string.Empty;
                textBox_author.Text = "Srinsoft Technologies";
                textBox_owner.Text = "Srinsoft Technologies";
                descriptionPad.Text = string.Empty;
                TargetPath.Text = string.Empty;
                frameworkDropDown.Text = "4.5.2";
                InitialiseDataTable();

                if (!EnableMultiCheckBox.Checked)
                {
                    if (!metaData.ProjectConfigs.ContainsKey(Props.DefaultConfig))
                    {
                        AddProjectConfig(Props.DefaultConfig);
                    }
                }

                BindConfigNameToDT();

                return;
            }

            InitialiseDataTable();

            var currentConfig = PreviousConfiguration[configurationList.Text];
            if (File.Exists(currentConfig))
            {
                var xmlString = File.ReadAllText(currentConfig);
                if (!string.IsNullOrEmpty(xmlString) && !string.IsNullOrWhiteSpace(xmlString))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xmlString);
                    XmlNodeList categoryList = xdoc.GetElementsByTagName("Metadata");

                    categoryList.OfType<XmlNode>().ToList().ForEach(x =>
                    {
                        if (x.HasChildNodes)
                        {
                            GetData(x);
                        }
                    });
                }
            }

            foreach (var proj in metaData.ProjectConfigs)
            {
                if (!ProjectConfigComboBox.Items.Contains(proj.Key))
                {
                    ProjectConfigComboBox.Items.Add(proj.Key);
                }
            }

            if (EnableMultiCheckBox.Checked)
            {
                ProjectConfigComboBox.SelectedItem = Props.SelectProjectConfig;
            }
            else if (!EnableMultiCheckBox.Checked)
            {
                SetDataToTable();
            }
            BindConfigNameToDT();

        }

        private void BindVersion(List<string> version_list)
        {
            // fourthDigit.Text = "0";
            //for (int i = 0; i < version_list.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        firstDigit.Text = version_list[i];
            //    }
            //    else if (i == 1)
            //    {
            //        secondDigit.Text = version_list[i];

            //    }
            //    else if (i == 2)
            //    {
            //        thirdDigit.Text = version_list[i];

            //    }
            //    else if (i == 3)
            //    {
            //        fourthDigit.Text = version_list[i];
            //    }
            //}

            BindCurrentDateTimeAsVersion(DateTime.Now);



        }
        //public void BindDateTimeAsVersion()
        //{
        //    firstDigit.Text = dateTimePicker.Value.Date.Year.ToString();
        //    secondDigit.Text = dateTimePicker.Value.Date.Month > 9 ? dateTimePicker.Value.Date.Month.ToString() : "0" + dateTimePicker.Value.Date.Month.ToString();
        //    thirdDigit.Text = dateTimePicker.Value.Date.Day > 9 ? dateTimePicker.Value.Date.Day.ToString() : "0" + dateTimePicker.Value.Date.Day.ToString();
        //    fourthDigit.Text = "00";
        //}

        public void BindCurrentDateTimeAsVersion(DateTime dateTime)
        {
            string versionPreview = string.Empty;
            versionPreview += dateTime.Date.ToString("yyyy.M.d");
            if (fourthDigit.Text != string.Empty && fourthDigit.Text != null)
            {
                var buildNumber = int.Parse(fourthDigit.Text);
                if (buildNumber != 0)
                {
                    versionPreview += ".";
                    versionPreview += buildNumber;//buildNumber > 9 ? buildNumber.ToString() : "0" + buildNumber;
                }
            }

            //if (fourthDigit.Text == string.Empty)
            //{
            //    versionPreview += "00";
            //}

            VersionPreviewLabel.Text = versionPreview;

        }

        private void GetData(XmlNode xmlNode)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                switch (x.Name)
                {
                    case "id":
                        nugetName.Text = x.InnerText;
                        break;

                    case "name":
                        metaData.Name = x.InnerText;
                        break;

                    case "version":
                        BindVersion(x.InnerText.Split('.').ToList());
                        break;

                    case "description":
                        descriptionPad.Text = x.InnerText;
                        break;

                    case "authors":
                        textBox_author.Text = x.InnerText;
                        break;

                    case "owners":
                        textBox_owner.Text = x.InnerText;
                        break;

                    case "releaseNotes":
                        ReleasePad.Text = x.InnerText;
                        break;

                    case "targetFramework":
                        frameworkDropDown.Text = x.InnerText;
                        break;

                    case "OutputPath":
                        TargetPath.Text = x.InnerText;
                        metaData.OutputPath = x.InnerText;
                        break;


                    case "ProjectConfigs":
                        GetProjectConfigs(x);
                        break;

                    default:
                        break;
                }
            });

            if (metaData.Name == string.Empty)
            {
                metaData.Name = nugetName.Text;
            }

        }

        private void GetProjectConfigs(XmlNode xmlNode)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                string configName = x.Name;
                if (!metaData.ProjectConfigs.ContainsKey(configName))
                {
                    ProjectConfig config = new ProjectConfig();
                    config.ReferenceDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
                    config.RuntimeDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) }); config.ObfucationRuntimeDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
                    BindLibAndDll(x, config);
                    metaData.ProjectConfigs.Add(configName, config);
                }
            });
        }

        private void BindLibAndDll(XmlNode xmlNode, ProjectConfig config)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                switch (x.Name)
                {
                    case "Reference":
                        GetReferenceData(x, config);
                        break;

                    case "Runtime":
                        GetRuntimeData(x, config);
                        break;

                    case "ObfuscationRuntime":
                        GetObfuscationRuntimeData(x, config);
                        break;


                    case "Obproj_File":
                        config.Obproj_File.Text = x.InnerText;
                        break;

                    case "Obproj_DLL_Directory":
                        config.Obproj_DLL_Directory.Text = x.InnerText;

                        break;

                    case "ObProj_OutPut_Directory":
                        config.ObProj_OutPut_Directory.Text = x.InnerText;

                        break;

                    default:
                        break;
                }
            });

        }

        private void GetReferenceData(XmlNode xmlNode, ProjectConfig config)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                switch (x.Name)
                {
                    case "string":
                        config.ReferenceDataTable.Rows.Add(new object[] { x.InnerText });
                        break;

                    default:
                        break;
                }
            });

        }

        private void GetRuntimeData(XmlNode xmlNode, ProjectConfig config)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                switch (x.Name)
                {
                    case "string":
                        config.RuntimeDataTable.Rows.Add(new object[] { x.InnerText });
                        break;

                    default:
                        break;
                }
            });

            //_runtime.DataSource = runtimeDataTable;
            //WarnDeletedFiles(_runtime.Rows);
            //_runtime.Columns[0].Width = 550;
            //ClearDataTableSelection();

        }

        private void GetObfuscationRuntimeData(XmlNode xmlNode, ProjectConfig config)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                switch (x.Name)
                {
                    case "string":
                        config.ObfucationRuntimeDataTable.Rows.Add(new object[] { x.InnerText });
                        break;

                    default:
                        break;
                }
            });

            //_runtime.DataSource = runtimeDataTable;
            //WarnDeletedFiles(_runtime.Rows);
            //_runtime.Columns[0].Width = 550;
            //ClearDataTableSelection();

        }

        private void GetRuntimeData(XmlNode xmlNode)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                switch (x.Name)
                {
                    case "string":
                        runtimeDataTable.Rows.Add(new object[] { x.InnerText });
                        break;

                    default:
                        break;
                }
            });

            _runtime.DataSource = runtimeDataTable;
            CommonFunctions.WarnDeletedFiles(_runtime.Rows);
            _runtime.Columns[0].Width = 550;
            ClearDataTableSelection();

        }

        private void GetReferenceData(XmlNode xmlNode)
        {
            xmlNode.ChildNodes.OfType<XmlNode>().ToList().ForEach(x =>
            {
                switch (x.Name)
                {
                    case "string":
                        referenceDataTable.Rows.Add(new object[] { x.InnerText });
                        break;

                    default:
                        break;
                }
            });

            _reference.DataSource = referenceDataTable;
            CommonFunctions.WarnDeletedFiles(_reference.Rows);
            _reference.Columns[0].Width = 550;
            ClearDataTableSelection();
        }

        private void fourthDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            UIValidator.ValidateKeyPress(fourthDigit, e);
        }


        private void nugetName_KeyPress(object sender, KeyPressEventArgs e)
        {
            UIValidator.ValidateNuGetNameKeyPress(sender, e, nugetName);
        }

        private void frameWork_TextChanged_1(object sender, EventArgs e)
        {
            nugetName.Text = nugetName.Text.Replace(" ", string.Empty);
        }

        private void fourthDigit_TextChanged(object sender, EventArgs e)
        {
            BindCurrentDateTimeAsVersion(DateTime.Now);
        }

        private void Nuget_Generator_Click(object sender, EventArgs e)
        {
            ClearDataTableSelection();
        }

        private void ClearDataTableSelection()
        {
            _runtime.ClearSelection();
            _reference.ClearSelection();
            obfuscationDataGridView.ClearSelection();

        }

        private void InitialiseRuntimeAndRefernceDataTable()
        {
            referenceDataTable = new DataTable();
            referenceDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
            _reference.DataSource = referenceDataTable;

            runtimeDataTable = new DataTable();
            runtimeDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
            _runtime.DataSource = runtimeDataTable;

            ObfuscationDataTable = new DataTable();
            ObfuscationDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });
            obfuscationDataGridView.DataSource = ObfuscationDataTable;

        }

        private void ProjectConfigComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (EnableMultiCheckBox.Checked)
            {
                if (ProjectConfigComboBox.Text == Props.NewConfig)
                {
                    if (ProjectConfigComboBox.Text != Props.SelectConfig)
                    {
                        InitialiseRuntimeAndRefernceDataTable();

                        ConfigName l = new ConfigName();
                        l.ShowDialog();

                        string configName = ConfigName.Config;
                        if (configName != null)
                        {
                            if (!metaData.ProjectConfigs.ContainsKey(configName))
                            {
                                AddProjectConfig(configName);

                            }
                        }
                        else
                        {
                            ProjectConfigComboBox.SelectedItem = Props.SelectProjectConfig;
                        }
                        return;
                    }
                }
                SetDataToTable();
            }
            //else
            //{
            //    if (!ProjectConfigs.ContainsKey("Default"))
            //    {
            //        AddProjectConfig("Default");
            //    }
            //}

        }

        private void SetData(string key)
        {
            ProjectConfig config = CommonFunctions.TryGetProject(metaData.ProjectConfigs, key);
            if (config != null)
            {
                _reference.DataSource = config.ReferenceDataTable;
                _runtime.DataSource = config.RuntimeDataTable;
                obfuscationDataGridView.DataSource = config.ObfucationRuntimeDataTable;

                textbox_RepoOutPutDir.Text = config.ObProj_OutPut_Directory.Text;
                textbox_RepoDllPath.Text = config.Obproj_DLL_Directory.Text;
                textbox_RepoPath.Text = config.Obproj_File.Text;

                CommonFunctions.WarnDeletedFiles(_reference.Rows);
                CommonFunctions.WarnDeletedFiles(_runtime.Rows);
                CommonFunctions.WarnDeletedFiles(obfuscationDataGridView.Rows);
                _reference.Columns[0].Width = 550;
                _runtime.Columns[0].Width = 650;
                obfuscationDataGridView.Columns[0].Width = 650;

                ClearDataTableSelection();
            }
        }

        private void SetDataToTable()
        {
            if (!EnableMultiCheckBox.Checked)
            {
                SetData(Props.DefaultConfig);
                return;
            }

            if ((ProjectConfigComboBox.Text != Props.NewConfig && ProjectConfigComboBox.Text != Props.SelectProjectConfig))
            {
                string key = ProjectConfigComboBox.Text;
                ProjectConfigComboBox.SelectedItem = ProjectConfigComboBox.Text;
                SetData(key);
            }
        }

        private void AddProjectConfig(string configName)
        {
            ProjectConfigComboBox.Items.Add(configName);

            ProjectConfig projectConfig = null;

            projectConfig = new ProjectConfig();

            projectConfig.ReferenceDataTable = new DataTable();
            projectConfig.ReferenceDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });

            projectConfig.RuntimeDataTable = new DataTable();
            projectConfig.RuntimeDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });

            projectConfig.ObfucationRuntimeDataTable = new DataTable();
            projectConfig.ObfucationRuntimeDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo) });


            metaData.ProjectConfigs.Add(configName, projectConfig);

            _reference.DataSource = projectConfig.ReferenceDataTable;
            CommonFunctions.WarnDeletedFiles(_reference.Rows);

            _runtime.DataSource = projectConfig.RuntimeDataTable;
            CommonFunctions.WarnDeletedFiles(_runtime.Rows);

            obfuscationDataGridView.DataSource = projectConfig.ObfucationRuntimeDataTable;
            CommonFunctions.WarnDeletedFiles(obfuscationDataGridView.Rows);

            ProjectConfigComboBox.SelectedItem = configName;

            ClearDataTableSelection();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableMultiCheckBox.Checked == true)
            {
                support_CLR.Checked = false;
                support_CLR.Visible = false;

                LoadNuConfig(Extensions.NuxConfigExtension, Props.SelectConfig);
                CommonFunctions.BackToDefaultProjectConfigComboBox(ProjectConfigComboBox, Props.SelectProjectConfig);
                textbox_RepoPath.Text = string.Empty;
                textbox_RepoDllPath.Text = string.Empty;
                textbox_RepoOutPutDir.Text = string.Empty;
                metaData.ProjectConfigs.Clear();
                ProjectConfigComboBox.Visible = true;
                VSBuildConfiglabel.Visible = true;
            }
            else if (EnableMultiCheckBox.Checked == false)
            {
                support_CLR.Checked = false;
                support_CLR.Visible = true;

                LoadNuConfig(Extensions.NuConfigExtension, Props.SelectConfig);
                textbox_RepoPath.Text = string.Empty;
                textbox_RepoDllPath.Text = string.Empty;
                textbox_RepoOutPutDir.Text = string.Empty;
                metaData.ProjectConfigs.Clear();
                ProjectConfigComboBox.Visible = false;
                VSBuildConfiglabel.Visible = false;
            }

            BindConfigNameToDT();

        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            BindCurrentDateTimeAsVersion(dateTimePicker.Value);
            // BindDateTimeAsVersion();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void UseSameObprojFile_CheckedChanged(object sender, EventArgs e)
        {


            //if (EnableMultiCheckBox.Checked)
            //{
            //    _selectforAllConfig.Visible = true;
            //}
            //else
            //{
            //    _selectforAllConfig.Visible = false;
            //}

            //if (UseDiffObprojFile.Checked)
            //{
            //    if (EnableMultiCheckBox.Checked)
            //    {
            //        _selectforAllConfig.Visible = false;
            //    }
            //}
            //else
            //{
            //    _selectforAllConfig.Visible = false;

            //}
            //_selectCommonConfig.Enabled = false;

        }

        private void ObfusRepo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    string repoPath = CommonFunctions.GetFileNameFromRepo(true);

            //    if (repoPath != null)
            //    {
            //        var row = metaData.RepoPathTable.Rows[e.RowIndex];
            //        string configName = row[Props.BasicInfo] as string;
            //        var config = CommonFunctions.TryGetProject(metaData.ProjectConfigs, configName);
            //        if (config != null)
            //        {
            //            config.Obproj_File = repoPath;
            //            row[Props.RepoPath] = config.Obproj_File;
            //        }
            //    }
            //}
        }

        public void BindConfigNameToDT()
        {
            metaData.RepoPathTable = new DataTable();

            if (EnableMultiCheckBox.Checked)
            {
                metaData.RepoPathTable.Columns.AddRange(new DataColumn[] { new DataColumn(Props.BasicInfo), new DataColumn(Props.RepoPath) });

                foreach (var config in metaData.ProjectConfigs)
                {
                    metaData.RepoPathTable.Rows.Add(new object[] { config.Key });
                }
            }
            // ObfusRepoTable.DataSource = metaData.RepoPathTable;

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateNuGetTree.Nodes.Clear();

            if (tabControl.SelectedTab == tabControl.TabPages["_obfusResult"])
            {
                try
                {

                    if (File.Exists(metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension))
                    {
                        ValidateNuget.ValidateNuGet(ValidateNuGetTree, metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + "." + metaData.Version + Extensions.NupkgExtension);
                    }

                }
                catch (Exception ex)
                {
                    WriteToConsoleWindow(ex.Message, Color.Black);
                }

            }

            BindConfigNameToDT();
        }



        private void browse_RunTime_Click_1(object sender, EventArgs e)
        {

        }

        private void Remove_runtime_Click_1(object sender, EventArgs e)
        {

        }

        private void _log_TextChanged(object sender, EventArgs e)
        {

        }


        private static void MyProcOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                logMessage = logMessage + Environment.NewLine + outLine.Data;
                // WriteToMyRichTextBox(logMessage);
                //frmReference._log.AppendText(outLine.Data);
                // frmReference._log.AppendText(Environment.NewLine);
            }
        }

        private void commonRepoOutPutDir_Click(object sender, EventArgs e)
        {

            ProjectConfig projectConfig = GetProjectConfigBasedOnRequest();

            if (projectConfig != null)
            {
                string repoPath = CommonFunctions.GetFileNameFromRepo(false);
                textbox_RepoOutPutDir.Text = repoPath;

                projectConfig.ObProj_OutPut_Directory.Text = textbox_RepoOutPutDir.Text;
            }
            else
            {
                MessageBox.Show(PopUP_Messages.Create_New_ProjConfig);
            }

        }

        private void commonRepoDllPath_Click(object sender, EventArgs e)
        {

            ProjectConfig projectConfig = GetProjectConfigBasedOnRequest();

            if (projectConfig != null)
            {
                string repoPath = CommonFunctions.GetFileNameFromRepo(false);
                textbox_RepoDllPath.Text = repoPath;

                projectConfig.Obproj_DLL_Directory.Text = textbox_RepoDllPath.Text;
            }
            else
            {
                MessageBox.Show(PopUP_Messages.Create_New_ProjConfig);
            }

        }

        private void commonRepoPath_Click(object sender, EventArgs e)
        {

            ProjectConfig projectConfig = GetProjectConfigBasedOnRequest();

            if (projectConfig != null)
            {
                string repoPath = CommonFunctions.GetFileNameFromRepo(true);
                textbox_RepoPath.Text = repoPath;

                projectConfig.Obproj_File.Text = textbox_RepoPath.Text;
            }
            else
            {
                MessageBox.Show(PopUP_Messages.Create_New_ProjConfig);
            }

        }

        private void AddObfuscateDependency_Click(object sender, EventArgs e)
        {
            ProjectConfig projectConfig = null;
            projectConfig = GetProjectConfigBasedOnRequest();

            if (projectConfig != null)
            {
                List<string> listOfFiles = CommonFunctions.OpenFileDialog("All files (*.*)|*.*", "Obfuscation Dependencies Browser", true);

                if (listOfFiles != null)
                {
                    CommonFunctions.LoadFilesInDatatable(listOfFiles, projectConfig.ObfucationRuntimeDataTable);
                    obfuscationDataGridView.DataSource = projectConfig.ObfucationRuntimeDataTable;
                    CommonFunctions.WarnDeletedFiles(obfuscationDataGridView.Rows);
                    obfuscationDataGridView.Columns[0].Width = 550;
                    ClearDataTableSelection();
                }
            }
            else
            {
                MessageBox.Show(PopUP_Messages.Create_New_ProjConfig);
            }

        }

        private void RemoveObfuscateDependency_Click(object sender, EventArgs e)
        {
            ProjectConfig projectConfig = null;

            if (EnableMultiCheckBox.Checked)
            {
                projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, ProjectConfigComboBox.Text);
            }
            else
            {

                if (!EnableMultiCheckBox.Checked)
                {
                    if (metaData.ProjectConfigs.ContainsKey(Props.DefaultConfig))
                    {
                        projectConfig = CommonFunctions.TryGetProject(metaData.ProjectConfigs, Props.DefaultConfig);
                    }
                }
            }

            if (projectConfig != null)
            {
                foreach (DataGridViewRow row in _reference.SelectedRows)
                {
                    int index = row.Index;
                    obfuscationDataGridView.Rows.RemoveAt(index);
                }
            }
        }



        #region Unused
        private void DateTimeButton_Click(object sender, EventArgs e)
        {
            //var picker = new DateTimePicker();
            //Form f = new Form();
            //f.Controls.Add(picker);

            //var result = f.ShowDialog();
            //if (result == DialogResult.OK)
            //{

            //    //get selected date
            //}
        }

        private void support_CLR_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void nugetName_TextChanged(object sender, EventArgs e)
        {
        }

        private void nugetName_Leave(object sender, EventArgs e)
        {
        }

        private void version_Leave(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Owner_Click(object sender, EventArgs e)
        {

        }

        private void frameWork_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  CommonFunctions.ValidateKeyPress(firstDigit, e);
        }

        private void secondDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // CommonFunctions.ValidateKeyPress(secondDigit, e);
        }

        private void thirdDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            //CommonFunctions.ValidateKeyPress(thirdDigit, e);
        }

        private void version_TextChanged(object sender, EventArgs e)
        {
        }

        private void secondDigit_TextChanged(object sender, EventArgs e)
        {
        }

        private void thirdDigit_TextChanged(object sender, EventArgs e)
        {
        }
        private void Nuget_Generator_Load(object sender, EventArgs e)
        {
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_author_TextChanged(object sender, EventArgs e)
        {

        }

        private void configurationList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ProjectConfigComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void VSBuildConfiglabel_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void VersionPreviewLabel_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void descriptionPad_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReleasePad_TextChanged(object sender, EventArgs e)
        {

        }

        private void Runtime_Dependencies_Enter(object sender, EventArgs e)
        {

        }

        private void _runtime_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void References_Enter(object sender, EventArgs e)
        {

        }

        private void _reference_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TargetPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox_owner_TextChanged(object sender, EventArgs e)
        {

        }

        private void Owner_Click_1(object sender, EventArgs e)
        {

        }

        private void author_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void nugetName_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ObfuscationPg_Click(object sender, EventArgs e)
        {

        }

        private void DescriptionAbtNuGetGenerator_Click(object sender, EventArgs e)
        {

        }

        private void _selectCommonConfig_Enter(object sender, EventArgs e)
        {

        }

        private void textbox_RepoOutPutDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textbox_RepoDllPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textbox_RepoPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void _logPage_Click(object sender, EventArgs e)
        {

        }

        private void _selectforAllConfig_Enter(object sender, EventArgs e)
        {

        }

        private void _log_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void _obfusResult_Click(object sender, EventArgs e)
        {

        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }

        private void _Obfuscate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void obfuscationDependency_Enter(object sender, EventArgs e)
        {

        }

        private void obfuscationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion

        private void _log_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void _log_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            // allows only numbers

            if (!char.IsNumber(e.KeyChar))

            {

                e.Handled = true;

            }
            if (!char.IsLetter(e.KeyChar))

            {

                e.Handled = true;

            }
        }
    }
}
