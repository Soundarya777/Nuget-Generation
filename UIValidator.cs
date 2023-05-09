using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities
{
    public static class UIValidator
    {
        public static bool ValidateVersion(TextBox firstDigit, TextBox secondDigit, TextBox thirdDigit, TextBox fourthDigit)
        {
            if (!Regex.IsMatch(firstDigit.Text, @"^[0-9]+$") || !Regex.IsMatch(secondDigit.Text, @"^[0-9]+$") || !Regex.IsMatch(thirdDigit.Text, @"^[0-9]+$") || !Regex.IsMatch(fourthDigit.Text, @"^[0-9]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ValidateVersion(TextBox fourthDigit)
        {
            if (!Regex.IsMatch(fourthDigit.Text, @"^[0-9]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static void ValidateKeyPress(TextBox textbox, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Back))
            {
                e.Handled = false;
                return;
            }

            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;

            if (textbox.Text.Length > 4)
            {
                e.Handled = true;
            }
        }

        public static void ValidateNuGetNameKeyPress(object sender, KeyPressEventArgs e,TextBox nugetName)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
            }

            if (nugetName.Text.Length > 150)
            {
                e.Handled = true;
            }
        }
        public static bool EmptyValidation(string input)
        {
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }

    }
}
