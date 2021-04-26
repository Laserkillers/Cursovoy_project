using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class MasterMainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer;
        IMainWindowsCodeBehind _MainCodeBehind;
        public MasterMainPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
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
    }
}
