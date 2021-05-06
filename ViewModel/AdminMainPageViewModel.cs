using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class AdminMainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private User Customer = new User();
        IMainWindowsCodeBehind _MainCodeBehind;

        public AdminMainPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Customer = customer;
        }

        private RelayCommand _Close;
        public RelayCommand Close
        {
            get { return _Close??= new RelayCommand(OnClose, CanClose); }
        }
        private void OnClose()
        {
            _MainCodeBehind.ClosePage();
        }
        private bool CanClose()
        {
            return true;
        }
        private RelayCommand _GoToLoginPage;
        public RelayCommand GoToLoginPage
        {
            get { return _GoToLoginPage ??= new RelayCommand(OnGoToLoginPage, CanGoToLoginPage); }
        }
        private void OnGoToLoginPage()
        {
            _MainCodeBehind.LoadWiew(View_number.Login);
        }
        private bool CanGoToLoginPage()
        {
            return true;
        }
        private RelayCommand _DeleteUserPage;
        public RelayCommand DeleteUserPage
        {
            get { return _DeleteUserPage ??= new RelayCommand(OnDeleteUserPage, CanDeleteUserPage); }
        }
        private void OnDeleteUserPage()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.DeleteUsers, Customer);
        }
        private bool CanDeleteUserPage()
        {
            return true;
        }
        private RelayCommand _DeleteRowsPage;
        public RelayCommand DeleteRowsPage
        {
            get { return _DeleteRowsPage ??= new RelayCommand(OnDeleteRowsPage, CanDeleteRowsPage); }
        }
        private void OnDeleteRowsPage()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.DeleteRows, Customer);
        }
        private bool CanDeleteRowsPage()
        {
            return true;
        }

        private RelayCommand _GoClerk;
        public RelayCommand GoClerk
        {
            get { return _GoClerk ??= new RelayCommand(OnGoClerk, CanGoClerk); }
        }
        private void OnGoClerk()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.LogClerk, Customer);
        }
        private bool CanGoClerk()
        {
            return true;
        }
        private RelayCommand _GoMaster;
        public RelayCommand GoMaster
        {
            get { return _GoMaster ??= new RelayCommand(OnGoMaster, CanGoMaster); }
        }
        private void OnGoMaster()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.LogMaster, Customer);
        }
        private bool CanGoMaster()
        {
            return true;
        }
        private RelayCommand _GoModer;
        public RelayCommand GoModer
        {
            get { return _GoModer ??= new RelayCommand(OnGoModer, CanGoModer); }
        }
        private void OnGoModer()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.LogModer, Customer);
        }
        private bool CanGoModer()
        {
            return true;
        }
        private RelayCommand _GoClient;
        public RelayCommand GoClient
        {
            get { return _GoClient ??= new RelayCommand(OnGoClient, CanGoClient); }
        }
        private void OnGoClient()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.LogClient, Customer);
        }
        private bool CanGoClient()
        {
            return true;
        }
    }
}
