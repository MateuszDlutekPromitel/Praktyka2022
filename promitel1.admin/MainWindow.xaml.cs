using Microsoft.Win32;
using Newtonsoft.Json;
using promitel1.admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace promitel1.admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Company company;

        public MainWindow()
        {
            InitializeComponent();




            company = new Company
            {
                Name = "Promitel",
                Cameras = new List<Camera>
                {
                new Camera
                {
                    Name = "Camera1",
                    SN = "10273",
                    MAC="12-AC-3A-5F-00-A2",
                    DataStart = new DateTime(2022,3,9),
                    DataEnd = new DateTime(2023,10,2)
                },
                new Camera
                {
                    Name = "Camera2",
                    SN = "23677",
                    MAC="AA-BB-CC-DD-EE-FF",
                    DataStart = new DateTime(2022,1,10),
                    DataEnd = new DateTime(2023,8,1)
                }
            }

            };

            MainVM.CameraList = company.Cameras;
            MainVM.CompanyName = company.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ;

            company.Cameras.Add(new Camera()
            {
                Name = "",
                SN = "",
                MAC = "",
                DataStart = DateTime.Now,
                DataEnd = DateTime.Now.AddYears(1)
            });

            MainVM.CameraList = company.Cameras;
        }

        private void Click_Button_Delete(object sender, RoutedEventArgs e)
        {
            Camera camera = ((Button)sender).CommandParameter as Camera;
            company.Cameras.Remove(camera);
            MainVM.CameraList = company.Cameras;
        }

        private void TextBoxMAC_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.Text.Length >= 17)
                {
                    e.Handled = true;
                    return;
                }

            }
        }

        private void Command_Open_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Command_Open_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            string path = "";

            OpenFileDialog theDialog = new OpenFileDialog
            {
                Title = "Open pro File",
                Filter = "pro files|*.pro",
                RestoreDirectory = true
            };
            if (theDialog.ShowDialog() == true)
            {
                path = theDialog.FileName;
            }

            if (string.IsNullOrWhiteSpace(path)) { return; }

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                Company newCompany = JsonConvert.DeserializeObject<Company>(json);

                company = newCompany;
                MainVM.CameraList = company.Cameras;

            }
        }

        private void Command_Safe_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Command_Safe_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

            string json = JsonConvert.SerializeObject(company);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save pro file",
                DefaultExt = "pro",
                Filter = "pro files(*.pro)| *.pro",
                RestoreDirectory = true
            };
            if (saveFileDialog1.ShowDialog() == true)
            {

                string fullPath = saveFileDialog1.FileName;
                File.WriteAllText(fullPath, json);
                MessageBox.Show("Zapisano plik");
            }
            else
            {
                MessageBox.Show("Anulowano zapisywanie");
            }



        }

    }
}
