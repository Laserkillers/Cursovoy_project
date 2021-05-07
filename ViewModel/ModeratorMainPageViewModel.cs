using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class ModeratorMainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        IMainWindowsCodeBehind _MainCodeBehind;
        User Customer;
        public ModeratorMainPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Customer = customer;
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
                return _GoToLoginPage ??= new RelayCommand(OnGoToLoginPage, CanGoToLoginPage);
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
        private RelayCommand _GoToShowDelete;
        public RelayCommand GoToShowDelete
        {
            get { return _GoToShowDelete ??= new RelayCommand(OnGoToShowDelete, CanGoToShowDelete); }
        }
        private void OnGoToShowDelete()
        {
            _MainCodeBehind.LoadModeratorPage(Moderator_Pages.DeleteRows, Customer);
        }
        private bool CanGoToShowDelete()
        {
            return true;
        }
        private RelayCommand _GoToCheckAccounts;
        public RelayCommand GoToCheckAccounts
        {
            get { return _GoToCheckAccounts ??= new RelayCommand(OnGoToCheckAccounts, CanGoToCheckAccounts); }
        }
        private void OnGoToCheckAccounts()
        {
            _MainCodeBehind.LoadModeratorPage(Moderator_Pages.AcceptAccount, Customer);
        }
        private bool CanGoToCheckAccounts()
        {
            return true;
        }
        private RelayCommand _GoToProfile;
        public RelayCommand GoToProfile
        {
            get { return _GoToProfile ??= new RelayCommand(OnGoToProfile, CanGoToProfile); }
        }
        private void OnGoToProfile()
        {
            _MainCodeBehind.LoadMasterPage(Master_Page_Load.Profile, Customer);
        }
        private bool CanGoToProfile()
        {
            return true;
        }
    }
}
