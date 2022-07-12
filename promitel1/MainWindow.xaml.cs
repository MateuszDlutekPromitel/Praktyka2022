using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;

namespace promitel1
{
    public partial class MainWindow : Window
    {
        List<AccessPermision> importedList = new List<AccessPermision>();
        public MainWindow()
        {
            InitializeComponent();
            xlsDataGrid.ItemsSource = importedList;

            //powrot
            
        }
        private void Button_Click_Export(object sender, RoutedEventArgs e)
        {
            string path = "";


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save Excel File";
            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "xls files(*.xls)| *.xls";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == true)
            {
                path = saveFileDialog1.FileName;
                ExportXLS(path);
            }
            else
            {
                MessageBox.Show("Anulowano exportowanie");
            }

        }
        private void ExportXLS(string path)
        {
            object misValue = System.Reflection.Missing.Value;

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(misValue);
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);


            for (int r = 1; r <= 6; r++)
            {
                xlWorksheet.Columns[r].NumberFormat = "@";
            }

            xlWorksheet.Cells[1, 1] = "No.";
            xlWorksheet.Cells[1, 2] = "Plate No.";
            xlWorksheet.Cells[1, 3] = "Group(0 BlockList, 1 AllowList)";
            xlWorksheet.Cells[1, 4] = "Effective Start Date (Format: YYYY-MM-DD, e.g., 2017-12-07)";
            xlWorksheet.Cells[1, 5] = "Effective End Date (Format: YYYY-MM-DD, e.g., 2017-12-07)";
            xlWorksheet.Cells[1, 6] = "Card ID ";

            int i = 2;
            foreach (AccessPermision accessPermision in importedList)
            {
                xlWorksheet.Cells[i, 1] = accessPermision.No.ToString();
                xlWorksheet.Cells[i, 2] = accessPermision.PlateNo.ToString();
                xlWorksheet.Cells[i, 3] = accessPermision.Group.ToString();
                xlWorksheet.Cells[i, 4] = accessPermision.StartDate.ToString("yyyy-MM-dd");
                xlWorksheet.Cells[i, 5] = accessPermision.EndDate.ToString("yyyy-MM-dd");
                xlWorksheet.Cells[i, 6] = accessPermision.CardID.ToString();
                i++;
            }

            try
            {
                xlWorkbook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                MessageBox.Show("Wyexportowano plik");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }

            xlWorkbook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorksheet);
            Marshal.ReleaseComObject(xlWorkbook);
            Marshal.ReleaseComObject(xlApp);

            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
        private void Button_Click_Import(object sender, RoutedEventArgs e)
        {
            string path = "";

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Excel File";
            theDialog.Filter = "XLS files|*.xls";
            //theDialog.InitialDirectory = @"C:\";
            theDialog.RestoreDirectory = true;
            if (theDialog.ShowDialog() == true)
            {
                path = theDialog.FileName;
            }

            Console.WriteLine(path);

            importedList = ImportXLS(path);

            xlsDataGrid.ItemsSource = importedList;

        }
        private List<AccessPermision> ImportXLS(string path)
        {
            List<AccessPermision> apList = new List<AccessPermision>();

            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return null;
            }
            if (path == "")
            {
                MessageBox.Show("No file was chosen");
                return null;
            }
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            AccessPermision ap = null;
            int rowCount = xlRange.Rows.Count;

            for (int i = 2; i <= rowCount; i++)
            {
                ap = new AccessPermision();

                try {
                    ap.No = Int32.Parse(xlRange.Cells[i, 1].Value2);
                }
                catch (Exception ex) {
                    MessageBox.Show("Error No " + ex.Message);
                }
                try {
                    ap.PlateNo = xlRange.Cells[i, 2].Value2.ToString();
                }
                catch (Exception ex) {
                    MessageBox.Show("Error PlateNo " + ex.Message);
                }
                try {
                    ap.Group = Int32.Parse(xlRange.Cells[i, 3].Value2);
                }
                catch (Exception ex) {
                    MessageBox.Show("Error Group " + ex.Message);
                }
                try {
                    ap.StartDate = DateTime.Parse(xlRange.Cells[i, 4].Value2);
                }
                catch (Exception ex) {
                    MessageBox.Show("Error StartDate " + ex.Message);
                }
                try {
                    ap.EndDate = DateTime.Parse(xlRange.Cells[i, 5].Value2);
                }
                catch (Exception ex) {
                    MessageBox.Show("Error EndDate " + ex.Message);
                }
                try {
                    ap.CardID = xlRange.Cells[i, 6].Value2.ToString();
                }
                catch (Exception ex) {
                    MessageBox.Show("Error CardID " + ex.Message);
                }

                apList.Add(ap);
            }

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            //apList.ForEach(p => Console.WriteLine(p.No + " " + p.PlateNo + " " + p.Group + " " + p.StartDate.ToShortDateString() + " " + p.EndDate.ToShortDateString() + " " + p.CardID));
            MessageBox.Show("Zaimportowano plik");

            return apList;
        }
        private void Button_Click_Add_Row(object sender, RoutedEventArgs e)
        {
            importedList.Add(new AccessPermision());
            xlsDataGrid.ItemsSource = null;
            xlsDataGrid.ItemsSource = importedList;
        }
        private void Button_Click_Delete_Row(object sender, RoutedEventArgs e)
        {
            var todelete = importedList.Where(r => r.Selected).ToList();
            foreach (AccessPermision selectedObject in todelete)
            {
                importedList.Remove(selectedObject);
            }
            xlsDataGrid.ItemsSource = null;
            xlsDataGrid.ItemsSource = importedList;
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
        private void Button_Click_Check_Selected(object sender, RoutedEventArgs e)
        {
            foreach (AccessPermision potentiallySelectedObject in importedList)
            {
                if (xlsDataGrid.SelectedItems.Contains(potentiallySelectedObject))
                {
                    potentiallySelectedObject.Selected = true;

                }
            }
            xlsDataGrid.Items.Refresh();
        }
        private void Button_Click_Uncheck_Selected(object sender, RoutedEventArgs e)
        {
            foreach (AccessPermision potentiallySelectedObject in importedList)
            {
                if (xlsDataGrid.SelectedItems.Contains(potentiallySelectedObject))
                {
                    potentiallySelectedObject.Selected = false;

                }
            }

            xlsDataGrid.Items.Refresh();
        }
        private void Button_Click_Test(object sender, RoutedEventArgs e)
        {
            foreach(AccessPermision item in importedList.Where(c => c.Selected))
            {
                MessageBox.Show(item.No.ToString());
            }

        }
        private void Button_Click_Add_Filters(object sender, RoutedEventArgs e)
        {
          
            // Collection which will take your ObservableCollection
            var _itemSourceList = new CollectionViewSource() { Source = importedList };

            // ICollectionView the View/UI part 
            ICollectionView Itemlist = _itemSourceList.View;

            //now we add our Filter
            Itemlist.Filter = new Predicate<object>(Filter);

            xlsDataGrid.ItemsSource = Itemlist;
           
        }
        private bool Filter(object f)
        {
            int NoF = -1;
            Predicate<object> NoFilter = c => true;
            try
            {
                NoF = Int32.Parse(TextBoxNoFilter.Text);
                NoFilter = new Predicate<object>(item => ((AccessPermision)item).No.Equals(NoF));
            }
            catch (Exception) { }

            string PlatoNoF = TextBoxPlateNoFilter.Text.Trim().ToUpper();
            Predicate<object> PlateNoFilter = c => true;
            if (!String.IsNullOrEmpty(PlatoNoF))
            {
                PlateNoFilter = new Predicate<object>(item => ((AccessPermision)item).PlateNo.Contains(PlatoNoF));
            }

            int GroupF = -1;
            Predicate<object> GroupFilter = c => true;
            try
            {
                GroupF = Int32.Parse(TextBoxGroupFilter.Text);
                GroupFilter = new Predicate<object>(item => ((AccessPermision)item).Group.Equals(GroupF));
            }
            catch (Exception) { }

            DateTime StartDateF = new DateTime();
            Predicate<object> StartDateFilter = c => true;
            try
            {
                StartDateF = DateTime.Parse(TextBoxStartDateFilter.Text);
                StartDateFilter = new Predicate<object>(item => ((AccessPermision)item).StartDate.Equals(StartDateF));
            }
            catch (Exception) { }

            DateTime EndDateF = new DateTime();
            Predicate<object> EndDateFilter = c => true;
            try
            {
                EndDateF = DateTime.Parse(TextBoxEndDateFilter.Text);
                EndDateFilter = new Predicate<object>(item => ((AccessPermision)item).EndDate.Equals(EndDateF));
            }
            catch (Exception) { }

            string CardIDF = TextBoxCardIDFilter.Text.Trim();
            Predicate<object> CardIDFilter = c => true;
            if (!String.IsNullOrEmpty(CardIDF))
            {
                CardIDFilter = new Predicate<object>(item => ((AccessPermision)item).CardID.Contains(CardIDF));
            }


            return (NoFilter(f) && PlateNoFilter(f) && GroupFilter(f) && StartDateFilter(f) && EndDateFilter(f) && CardIDFilter(f));
        }
        private void Button_Click_Remove_Filters(object sender, RoutedEventArgs e)
        {
            var _itemSourceList = new CollectionViewSource() { Source = importedList };
            ICollectionView Itemlist = _itemSourceList.View;
            Itemlist.Filter = null;
            xlsDataGrid.ItemsSource = Itemlist;
            
        }
        private void Button_Click_Edit_Checked(object sender, RoutedEventArgs e)
        {
            EditCheckedWindow editCheckedWin = new EditCheckedWindow();
            editCheckedWin.Owner = this;
            editCheckedWin.Show();
        }
        public void Group_Edit_No(int i)
        {
            foreach(AccessPermision item in importedList.Where(c => c.Selected))
            {
                item.No = i;
            }
            xlsDataGrid.Items.Refresh();
        }
        public void Group_Edit_PlateNo(string s)
        {
            foreach (AccessPermision item in importedList.Where(c => c.Selected))
            {
                item.PlateNo = s;
            }
            xlsDataGrid.Items.Refresh();
        }
        public  void Group_Edit_Group(int i)
        {
            foreach (AccessPermision item in importedList.Where(c => c.Selected))
            {
                item.Group = i;
            }
            xlsDataGrid.Items.Refresh();
        }
        public void Group_Edit_StartTime(DateTime t)
        {
            foreach (AccessPermision item in importedList.Where(c => c.Selected))
            {
                item.StartDate = t;
            }
            xlsDataGrid.Items.Refresh();
        }
        public void Group_Edit_EndTime(DateTime t)
        {
            foreach (AccessPermision item in importedList.Where(c => c.Selected))
            {
                item.EndDate = t;
            }
            xlsDataGrid.Items.Refresh();
        }
        public void Group_Edit_CardID(string s)
        {
            foreach (AccessPermision item in importedList.Where(c => c.Selected))
            {
                item.CardID = s;
            }
            xlsDataGrid.Items.Refresh();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
