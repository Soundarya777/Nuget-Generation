using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities
{
   public static class LogHandler
    {
        public static void WriteLog(RichTextBox box , string Data )
        {
            box.Text = box.Text + Environment.NewLine + Data;
        }

    }
}
