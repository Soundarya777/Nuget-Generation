using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Utilities
{
    public class AutoFillerData : Form
    {
        private string NugetConfigName { get; set; }

        private string SelectedProjConfig { get; set; }

        private string LoadConfigPath { get; set; }

        private bool MultiVsConfig { get; set; }

        private MetaData metaData = null;

        private Dictionary<string, string> PreviousConfiguration = new Dictionary<string, string>();

        public AutoFillerData(string[] arg)
        {
            try
            {
                metaData = new MetaData();
                LoadConfigPath = arg[0];

                metaData.Version = SelectedProjConfig = "2022";
                string nugetName = NugetConfigName = "BXF_RevitSender";
                string loadedFilePath = LoadConfigPath;
                metaData.OutputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TempNuget");
                metaData.WorkingDirectory = metaData.OutputPath + Extensions.FolderSeperator + nugetName + Guid.NewGuid();
                BindDetailsToMetaData();

                string nuspecFullFileName = WriteNuspec();

                FileSystemUtils.CreateDirectory(metaData.WorkingDirectory);

                ///string nuspecFullFileName = metaData.WorkingDirectory + Extensions.FolderSeperator + nugetName + Extensions.NuspecExtension;
                CommonFunctions.PackNuget(nuspecFullFileName);

                if (File.Exists(nugetName + "." + metaData.Version + Extensions.NupkgExtension))
                {
                    File.Copy(nugetName + "." + metaData.Version + Extensions.NupkgExtension, metaData.OutputPath + Extensions.FolderSeperator + nugetName + "." + metaData.Version + Extensions.NupkgExtension, true);
                    File.Delete(nugetName + "." + metaData.Version + Extensions.NupkgExtension);
                }
                else
                {

                    var direc = Directory.GetCurrentDirectory();
                    if (File.Exists(direc + Extensions.FolderSeperator + nugetName + "." + metaData.Version + Extensions.NupkgExtension))
                    {
                        File.Copy(direc + Extensions.FolderSeperator + nugetName + "." + metaData.Version + Extensions.NupkgExtension, metaData.OutputPath + Extensions.FolderSeperator + nugetName + "." + metaData.Version + Extensions.NupkgExtension, true);
                        File.Delete(direc + Extensions.FolderSeperator + nugetName + "." + metaData.Version + Extensions.NupkgExtension);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string WriteNuspec()
        {
            string nuspec = SerialiseNuspec();

            foreach (var item in metaData.ProjectConfigs)
            {
                FileSystemUtils.DeleteFolder(metaData.WorkingDirectory + Extensions.FolderSeperator + item.Key);
            }


            string nuspecFullFileName = metaData.WorkingDirectory + Extensions.FolderSeperator + NugetConfigName + Extensions.NuspecExtension;
            File.WriteAllText(nuspecFullFileName, nuspec);
            return nuspecFullFileName;
        }

        private string SerialiseNuspec()
        {
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, CommonFunctions.GetXMLSettings());

            xmlWriter.WriteStartElement("package");
            xmlWriter.WriteStartElement("metadata");

            xmlWriter.WriteElementString("id", NugetConfigName);
            xmlWriter.WriteElementString("version", metaData.Version);
            xmlWriter.WriteElementString("authors", "Srinsoft");
            xmlWriter.WriteElementString("owners", "Srinsoft");
            xmlWriter.WriteElementString("requireLicenseAcceptance", "false");
            xmlWriter.WriteElementString("description", "Trial");
            xmlWriter.WriteElementString("releaseNotes", "Testing");
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

            if (MultiVsConfig)
            {
                libPath = metaData.WorkingDirectory + "\\Pack\\";// + GetFrameWorkName();
            }
            else
            {
                //if (support_CLR.Checked)
                //{
                //    libPath = metaData.WorkingDirectory + "\\Pack\\";// + GetFrameWorkName();
                //}
                libPath = metaData.WorkingDirectory + "\\lib\\" + CommonFunctions.GetFrameWorkName(metaData.TargetFramework);
            }

            FileSystemUtils.CreateDirectory(libPath);

            foreach (var config in metaData.ProjectConfigs)
            {
                string defaultDir = string.Empty;

                if (MultiVsConfig)
                {
                    defaultDir = libPath + Extensions.FolderSeperator + config.Key;
                }
                else
                {
                    //if (support_CLR.Checked)
                    //{
                    //    defaultDir = libPath + Extensions.FolderSeperator + config.Key;
                    //}
                    defaultDir = libPath;
                }

                FileSystemUtils.CreateDirectory(defaultDir);

                string LibNames = string.Empty;

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

                    if (MultiVsConfig)
                    {
                        defaultDir = runtime + Extensions.FolderSeperator + config.Key;
                    }
                    else
                    {
                        //if (support_CLR.Checked)
                        //{
                        //    defaultDir = runtime + Extensions.FolderSeperator + config.Key;
                        //}
                        defaultDir = runtime;
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
                    }
                }
            }

            BindBuildTargets(xmlWriter);

        }

        private void BindBuildTargets(XmlWriter xmlWriter)
        {
            string buildPath = metaData.WorkingDirectory + Extensions.FolderSeperator + "build";

            FileSystemUtils.CreateDirectory(buildPath);

            if (MultiVsConfig)
            {
                File.WriteAllText(buildPath + Extensions.FolderSeperator + NugetConfigName + Extensions.TargetExtension, SerialiseTargets());
                File.WriteAllText(buildPath + Extensions.FolderSeperator + NugetConfigName + Extensions.PropsExtension, Serialiseprops());
            }
            else
            {
                //if (support_CLR.Checked)
                //{
                //    File.WriteAllText(buildPath + Extensions.FolderSeperator + nugetName.Text + Extensions.TargetExtension, SerialiseTargets());
                //    File.WriteAllText(buildPath + Extensions.FolderSeperator + nugetName.Text + Extensions.PropsExtension, Serialiseprops());
                //}
                File.WriteAllText(buildPath + Extensions.FolderSeperator + NugetConfigName + Extensions.TargetExtension, SerialiseTargets());
            }
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
                //if (!support_CLR.Checked)
                //{
                    xmlWriter.WriteAttributeString("Condition", "'$(Configuration)|$(Platform)'=='" + configs.Key + "|x64'");
                //}

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
            }

            xmlWriter.WriteEndElement();//Project
            xmlWriter.Close();
            var target = stringBuilder.ToString();

            return target;
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

                if (MultiVsConfig)
                {
                    xmlWriter.WriteAttributeString("Condition", "'$(Configuration)|$(Platform)'=='" + configs.Key + "|x64'");
                }
                //else
                //{
                //    xmlWriter.WriteAttributeString("Condition", "'$(Platform)'=='x64'");
                //}

                foreach (DataRow row in configs.Value.RuntimeDataTable.Rows)
                {
                    string fullfilePath = row[Props.BasicInfo] as string;
                    string initialDir = string.Empty;

                    if (MultiVsConfig)
                    {
                        initialDir = @"$(MSBuildThisFileDirectory)..\runtimes\win-x64\" + configs.Key + Extensions.FolderSeperator;
                    }
                    else
                    {
                        //if (support_CLR.Checked)
                        //{
                        //    initialDir = @"$(MSBuildThisFileDirectory)..\runtimes\win-x64\" + configs.Key + Extensions.FolderSeperator;
                        //}
                        initialDir = @"$(MSBuildThisFileDirectory)..\runtimes\win-x64\";
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

                if (MultiVsConfig)
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
            }

            xmlWriter.WriteEndElement();//Project
            xmlWriter.Close();
            var target = stringBuilder.ToString();

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
            if (MultiVsConfig)
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

        private void BindDetailsToMetaData()
        {
            //metaData.OutputPath = TargetPath.Text;
            //metaData.WorkingDirectory = metaData.OutputPath + Extensions.FolderSeperator + nugetName.Text + Guid.NewGuid();
            //metaData.Version = VersionPreviewLabel.Text;
            //metaData.Authors = textBox_author.Text;
            //metaData.ID = nugetName.Text;
            //metaData.Owners = Owner.Text;
            //metaData.Description = descriptionPad.Text;
            //metaData.ReleaseNotes = ReleasePad.Text;
        }
    }

    public class Metadatas
    {
        public static string id { get; set; }
        public static string version { get; set; }
        public static string authors { get; set; }
        public static string owners { get; set; }
        public static string requireLicenseAcceptance { get; set; }
        public static string description { get; set; }
        public static string releaseNotes { get; set; }
        public static string copyright { get; set; }
        public static string[] MyAutoFillDatasProperty { get; set; }
    }
}
