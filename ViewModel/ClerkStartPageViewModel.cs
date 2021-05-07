using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class ClerkStartPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;
        private User Customer = new User();
        public ClerkStartPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            Customer = customer;
            _MainCodeBehind = codeBehind;
        }

        /// <summary>
        /// Реализация кнопки закрыть программу
        /// </summary>
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

        private RelayCommand _GoToRecieveData;
        public RelayCommand GoToRecieveData
        {
            get { return _GoToRecieveData ??= new RelayCommand(OnGoToRecieveData, CanGoToRecieveData); }
        }
        private void OnGoToRecieveData()
        {
            _MainCodeBehind.LoadClerksPage(Clerk_view_number.Receive_Data, Customer);
        }
        private bool CanGoToRecieveData()
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
            _MainCodeBehind.LoadClerksPage(Clerk_view_number.Main, Customer);
        }
        private bool CanGoToProfile()
        {
            return true;
        }
    }
}
