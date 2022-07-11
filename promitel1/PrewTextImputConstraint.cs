using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace promitel1
{
    public static class PrewTextImputConstraint
    {
        public static void TextBoxNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\d");
            e.Handled = !regex.IsMatch(e.Text);
        }
        public static void TextBoxPlateNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.Text.Length >= 16)
                {
                    e.Handled = true;
                    return;
                }

            }
        }
        public static void TextBoxGroup_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.Text.Length > 0)
                {
                    e.Handled = true;
                    return;
                }
            }
            Regex regex = new Regex("^[01]$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        public static void TextBoxCardID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.Text.Length >= 12)
                {
                    e.Handled = true;
                    return;
                }

            }
            Regex regex = new Regex(@"\d");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
