using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Utilities
{
    public class ValidateNuget
    {
        public static void ValidateNuGet(TreeView TreeView, string sourceFileName)
        {
            // var tempWrkSpace = Path.GetTempPath() + "\\" + Guid.NewGuid();
            var tempWrkSpace = FileSystemUtils.GetUserDirectoryPath() + "\\" + Guid.NewGuid();
            Directory.CreateDirectory(tempWrkSpace);
            ZipFile.ExtractToDirectory(sourceFileName, tempWrkSpace);

            TreeNode node = new TreeNode(Path.GetFileName(sourceFileName));
            TreeView.Nodes.Add(node);

            ValidateNuGet(new DirectoryInfo(tempWrkSpace), node);

            FileSystemUtils.DeleteFolder(tempWrkSpace);

        }

        private static void ValidateNuGet(DirectoryInfo source, TreeNode node)
        {
            Directory.CreateDirectory(source.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                if (fi.Extension.ToLower() == ".dll")
                {
                    TreeNode treeNode = new TreeNode(fi.Name);

                    try
                    {
                        var assembly = Assembly.LoadFile(fi.FullName);
                        var allNamespace = assembly.GetTypes().Select(t => t.Namespace).Distinct().ToList();
                        if (allNamespace.Contains("A"))
                        {
                            treeNode.ForeColor = Color.Red;
                        }
                        node.Nodes.Add(treeNode);
                    }
                    catch (Exception exx)
                    {
                        if (exx.Message != "The module was expected to contain an assembly manifest. (Exception from HRESULT: 0x80131018)")
                        {
                            MessageBox.Show(exx.Message);
                        }
                    }
                }
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                TreeNode treeNode = new TreeNode(diSourceSubDir.Name);
                node.Nodes.Add(treeNode);
                ValidateNuGet(diSourceSubDir, treeNode);
            }
        }
    }
}
