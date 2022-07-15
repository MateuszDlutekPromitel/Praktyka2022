using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace promitel1
{
    internal class MainVM : INotifyPropertyChanged
    {
        #region Property change
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private string companyName;

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Camera> obsCameraList;

        public ObservableCollection<Camera> ObsCameraList
        {
            get { return obsCameraList; }
            set { obsCameraList = value; OnPropertyChanged(); }
        }

        private List<Camera> cameraList;

        public List<Camera> CameraList
        {
            get { return cameraList; }
            set { cameraList = value; ObsCameraList = new ObservableCollection<Camera>(cameraList); }
        }

    }

}
