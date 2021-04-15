using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class RegisterPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;
        
        public RegisterPageViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
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
            _MainCodeBehind.LoadWiew(View_number.Login);
        }

    }
}
