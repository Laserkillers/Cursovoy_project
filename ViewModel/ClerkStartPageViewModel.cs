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
        public ClerkStartPageViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
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
            _MainCodeBehind.LoadClerksPage(Clerk_view_number.Receive_Data);
        }
        private bool CanGoToRecieveData()
        {
            return true;
        }
    }
}
