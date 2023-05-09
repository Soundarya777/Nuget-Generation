using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace CSharpNugetGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            System.Diagnostics.Debugger.Launch();
            Metadatas.MyAutoFillDatasProperty = args;

            string[] new_Args = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CSharpNuget_Generator());

        }
    }
}
