using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for DuplicatesFiltersWindow.xaml
    /// </summary>
    public partial class DuplicatesFiltersWindow : Window
    {
        List<string> duplicatesPlateNoList = new List<string>();
        MainWindow mainWin;
        public DuplicatesFiltersWindow(List<string> PlateNoList)
        {
            InitializeComponent();
            mainWin = this.Owner as MainWindow;
            duplicatesPlateNoList = PlateNoList;
            plateNoDataGrid.ItemsSource = duplicatesPlateNoList;
        }

        private void Click_Button_Filter(object sender, RoutedEventArgs e)
        {
            //Product product = button.Tag as Product
            mainWin = this.Owner as MainWindow;
            string PlateNo = ((Button)sender).CommandParameter as string;
            mainWin.Add_Filter(new Predicate<object>(item => ((AccessPermision)item).PlateNo.Equals(PlateNo)));
            mainWin.Set_Value_TextBoxPlateNoFilter(PlateNo);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != Owner)
            {
                this.Owner.Activate();
            }
        }
    }
}
