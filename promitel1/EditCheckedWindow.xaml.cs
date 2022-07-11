using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace promitel1
{
    /// <summary>
    /// Interaction logic for EditCheckedWindow.xaml
    /// </summary>
    public partial class EditCheckedWindow : Window
    {
        MainWindow mainWin;
        public EditCheckedWindow()
        {
            InitializeComponent();
            mainWin = this.Owner as MainWindow;
        }

        public void Group_Edit_No()
        {
            //TextBoxGroupEdit.PreviewTextInput = "TextBoxNo_PreviewTextInput";
                //PreviewTextInput =
        }
        private void TextBoxNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            PrewTextImputConstraint.TextBoxNo_PreviewTextInput(sender, e);
        }
        private void TextBoxPlateNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            PrewTextImputConstraint.TextBoxPlateNo_PreviewTextInput(sender, e);
        }
        private void TextBoxGroup_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            PrewTextImputConstraint.TextBoxGroup_PreviewTextInput(sender, e);
        }
        private void TextBoxCardID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            PrewTextImputConstraint.TextBoxCardID_PreviewTextInput(sender, e);
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            mainWin = this.Owner as MainWindow;
            MessageBox.Show("ButtonEdit");

            if (cbNo.IsChecked == true)
            {
                MessageBox.Show("No");
                mainWin.Group_Edit_No(Int32.Parse(TextBoxNoFilter.Text));
                
            }
            if (cbPlateNo.IsChecked == true)
            {
                MessageBox.Show("Plate");
                mainWin.Group_Edit_PlateNo(TextBoxPlateNoFilter.Text.Trim().ToUpper());
                
            }
            if (cbGroup.IsChecked == true)
            {
                MessageBox.Show("Group");
                mainWin.Group_Edit_Group(Int32.Parse(TextBoxGroupFilter.Text));
                
            }
            if (cbStartDate.IsChecked == true)
            {
                MessageBox.Show("startdate");
                mainWin.Group_Edit_StartTime(DateTime.Parse(TextBoxStartDateFilter.Text));
                
            }
            if (cbEndDate.IsChecked == true)
            {
                MessageBox.Show("enddate");
                mainWin.Group_Edit_EndTime(DateTime.Parse(TextBoxEndDateFilter.Text));
            }
            if (cbCardID.IsChecked == true)
            {
                MessageBox.Show("cardId");
                mainWin.Group_Edit_CardID(TextBoxCardIDFilter.Text.Trim());
            }

            Close();
        }
    }
}
