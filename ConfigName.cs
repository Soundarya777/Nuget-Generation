using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpNugetGenerator
{
    public partial class ConfigName : Form
    {
        public static string Config { get; set; }
        public ConfigName()
        {
            Config = null;
            InitializeComponent();
        }

        private void ConfigName_Load(object sender, EventArgs e)
        {

        }

        private void SaveConfigButton_Click_1(object sender, EventArgs e)
        {
            if (ConfigNameTextBox.Text != null && ConfigNameTextBox.Text != "")
            {
                if (!string.IsNullOrWhiteSpace( ConfigNameTextBox.Text))
                {
                    Config = ConfigNameTextBox.Text;
                    Close();
                }
                else
                {
                    ConfigNameTextBox.Text = string.Empty;
                }
            }
        }
    }
}
