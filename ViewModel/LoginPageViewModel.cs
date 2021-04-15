using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public LoginPageViewModel() { }

        private IMainWindowsCodeBehind _MainCodeBehind;

        public LoginPageViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        private RelayCommand _GoToRegisterPage;
        public RelayCommand GoToRegisterPage
        {
            get
            {
                return _GoToRegisterPage = _GoToRegisterPage ?? new RelayCommand(OnGoRegisterPage, CanGoRegisterPage);
            }
        }
        private bool CanGoRegisterPage()
        {
            return true;
        }
        private void OnGoRegisterPage()
        {
            _MainCodeBehind.LoadWiew(View_number.Register);
        }
    }
}
