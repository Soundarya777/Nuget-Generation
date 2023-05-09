using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Utilities
{


    public static class CommonFunctions
    {
        static string logMessage = string.Empty;

        public static void BackToDefaultProjectConfigComboBox(ComboBox comboBox, String StartupText)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(StartupText);
            comboBox.Items.Add("New");
            comboBox.SelectedItem = StartupText;
        }

        public static string GetFileNameFromRepo(bool IsFile)
        {

            string tempFilename = Path.GetTempFileName();
            var command = String.Format("/command:repobrowser /path:\"{0}\"", @"https://192.168.1.200/svn/api/Products/Common%20Modules/branches/Encryption/Common%20dll");
            var final = string.Format(command + " /outfile:\"{0}\"", tempFilename);

            var info = new ProcessStartInfo(@"C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe", final);
            Process.Start(info).WaitForExit();

            var repoName = File.ReadAllLines(tempFilename);

            if (repoName.Length > 0)
            {
                if (IsFile)
                {
                    if (Path.GetExtension(repoName[0]) == ".obproj")
                    {

                        return RemoveDagPath(repoName[0]);
                    }
                }
                else
                {
                    // if (Path.GetExtension(repoName[0]) == "")
                    {
                        //var result = repoName[0].Replace("https://192.168.1.200/svn/api/Products/Common Modules/branches/Encryption/", "");
                        //return result;
                        return RemoveDagPath(repoName[0]);
                    }
                }
            }



            //https://192.168.1.200/svn/api/Products/Common Modules/branches/Encryption/
            return null;
        }

        public static string RemoveDagPath(string input)
        {
            var result = input.Replace("https://192.168.1.200/svn/api/Products/Common Modules/branches/Encryption/", "");
            return result;
        }


        public static void WarnDeletedFiles(DataGridViewRowCollection Rows)
        {
            foreach (DataGridViewRow row in Rows)
            {
                if (!File.Exists((string)row.Cells[0].Value))
                {
                    if (!Directory.Exists((string)row.Cells[0].Value))
                    {
                        row.Cells[0].Style.ForeColor = Color.Red;
                    }
                }
            }
        }

        public static void LoadToolTip(Control control, string toolTipString)
        {
            ToolTip _toolTip = new ToolTip();
            _toolTip.SetToolTip(control, toolTipString);
        }

        public static XmlWriterSettings GetXMLSettings()
        {
            XmlWriterSettings xmwr = new XmlWriterSettings();
            xmwr.NewLineOnAttributes = true;
            xmwr.Indent = true;
            xmwr.Encoding = Encoding.UTF8;
            return xmwr;
        }



        public static string PackNuget(string nuspecFullFileName)
        {
            logMessage = string.Empty;
            // var filename =  Path.GetFileName(nuspecFullFileName);
            string strCmdText = "pack " + "\"" + nuspecFullFileName + "\"";
            //   var procerss = Process.Start("nuget.exe", strCmdText);
            // procerss.WaitForExit();

            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = @"nuget.exe";
            process.StartInfo.Arguments = strCmdText;

            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += new DataReceivedEventHandler(MyProcOutputHandler);
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            return logMessage;
        }

        private static void MyProcOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                logMessage = logMessage + Environment.NewLine + outLine.Data;
            }
        }


        public static void LoadAllFilesInDirectory(Dictionary<string, string> configuration, string folderDir, string extention)
        {
            foreach (var filename in Directory.GetFiles(folderDir, "*" + extention).ToList())
            /// foreach (var filename in Directory.GetFiles(folderDir, "*.nuconfig").ToList())
            {
                // configurationList.Items.Add(Path.GetFileNameWithoutExtension(filename));
                if (!configuration.ContainsKey(Path.GetFileNameWithoutExtension(filename)))
                {
                    configuration.Add(Path.GetFileNameWithoutExtension(filename), filename);
                }
            }
        }

        public static void LoadFile(Dictionary<string, string> configuration, string filepath)
        {
            //foreach (var filename in Directory.GetFiles(folderDir, "*" + extention).ToList())
            /// foreach (var filename in Directory.GetFiles(folderDir, "*.nuconfig").ToList())
            {
                // configurationList.Items.Add(Path.GetFileNameWithoutExtension(filename));
                if (!configuration.ContainsKey(Path.GetFileNameWithoutExtension(filepath)))
                {
                    configuration.Add(Path.GetFileNameWithoutExtension(filepath), filepath);
                }
            }
        }


        public static ProjectConfig TryGetProject(Dictionary<string, ProjectConfig> ProjectConfigs, string name)
        {
            if (ProjectConfigs.ContainsKey(name))
            {
                var proj = ProjectConfigs[name];
                return proj;
            }
            else
            {
                return null;
            }
        }

        public static List<string> OpenFileDialog(string Filter, string Title, bool Multiselect)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();
            _openFileDialog.Filter = Filter;//"Dynamic Link Library (*.DLL)|*.dll";
            _openFileDialog.Multiselect = Multiselect;// true;
            _openFileDialog.Title = Title;//"Dependencies Browser";
            DialogResult dr = _openFileDialog.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                return _openFileDialog.FileNames.ToList();
            }
            else
            {
                return null;
            }
        }

        public static void LoadFilesInDatatable(List<string> listOfFiles, DataTable dataTable)
        {
            foreach (var item in listOfFiles)
            {
                dataTable.Rows.Add(new object[] { item });
            }
        }


        public static string GetFrameWorkName(string framework)
        {
            switch (framework)
            {
                case "4.5.0":
                    return "net450";
                case "4.5.1":
                    return "net451";
                case "4.5.2":
                    return "net452";
                case "4.6.0":
                    return "net460";
                case "4.6.1":
                    return "net461";
                case "4.6.2":
                    return "net462";
                case "4.7.0":
                    return "net470";
                case "4.7.1":
                    return "net471";
                case "4.7.2":
                    return "net472";

                default:
                    return "net452";
            }

        }


    }
}
