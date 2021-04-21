using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class StartPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public StartPageViewModel() { }

        private IMainWindowsCodeBehind _StartCodeBehind;

        public StartPageViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _StartCodeBehind = codeBehind;
        }
        private RelayCommand _GoLoginPage;
        public RelayCommand GoLoginPage
        {
            get
            {
                return _GoLoginPage = _GoLoginPage ?? new RelayCommand(OnLoadLoginPage, CanLoadLoginPage);
            }
        }
        private bool CanLoadLoginPage()
        {
            return true;
        }
        private void OnLoadLoginPage()
        {
            //_StartCodeBehind.Change_Background(Background_set.Clerk);
            _StartCodeBehind.LoadWiew(View_number.Login);
        }
    }
}
