using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class ClientStartPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ClientStartPageViewModel() { }

        private User User_data;

        private IMainWindowsCodeBehind _MainCodeBehind;
        public ClientStartPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            User_data = customer;
            _MainCodeBehind = codeBehind;
        }

        private RelayCommand _GoToEnrollCar;
        public RelayCommand GoToEnrollCar
        {
            get { return _GoToEnrollCar ??= new RelayCommand(OnGoToEnrollCar, CanGoToEnrollCar); }
        }
        private void OnGoToEnrollCar()
        {
            _MainCodeBehind.LoadClientPage(Client_Page_Load.EnrollCar, User_data);
        }
        private bool CanGoToEnrollCar()
        {
            return true;
        }

        private RelayCommand _GoToRecieveStatusAuto;
        public RelayCommand GoToRecieveStatusAuto
        {
            get { return _GoToRecieveStatusAuto ??= new RelayCommand(OnGoToRecieveStatusAuto, CanGoToRecieveStatusAuto); }
        }
        private void OnGoToRecieveStatusAuto()
        {
            _MainCodeBehind.LoadClientPage(Client_Page_Load.ViewCarStatus, User_data);
        }
        private bool CanGoToRecieveStatusAuto()
        {
            return true;
        }
        private RelayCommand _Close;
        public RelayCommand Close
        {
            get { return _Close ??= new RelayCommand(OnGoToClosePage, CanGoClosePage); }
        }
        private void OnGoToClosePage()
        {
            _MainCodeBehind.ClosePage();
        }
        private bool CanGoClosePage()
        {
            return true;
        }
        private RelayCommand _GoToLoginPage;
        public RelayCommand GoToLoginPage
        {
            get
            {
                return _GoToLoginPage = _GoToLoginPage ?? new RelayCommand(OnGoToLoginPage, CanGoToLoginPage);
            }
        }
        private bool CanGoToLoginPage()
        {
            return true;
        }
        private void OnGoToLoginPage()
        {
            _MainCodeBehind.Change_Background(Background_set.Main);
            _MainCodeBehind.LoadWiew(View_number.Login);
        }
        private RelayCommand _GoToProfile;
        public RelayCommand GoToProfile
        {
            get { return _GoToProfile ??= new RelayCommand(OnGoToProfile, CanGoToProfile); }
        }
        private void OnGoToProfile()
        {
            _MainCodeBehind.LoadClientPage(Client_Page_Load.Profile, User_data);
        }
        private bool CanGoToProfile()
        {
            return true;
        }
    }
}
