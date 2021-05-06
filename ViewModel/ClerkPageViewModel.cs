using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class ClerkPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;
        private User Customer = new User();

        public ClerkPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            Customer = customer;
            _MainCodeBehind = codeBehind;
        }

        private RelayCommand _GoToClerkMainPage;
        public RelayCommand GoToClerkMainPage
        {
            get { return _GoToClerkMainPage ??= new RelayCommand(OnGoToClerkMainPage, CanGoToClerkMainPage); }
        }

        private void OnGoToClerkMainPage()
        {
            _MainCodeBehind.LoadClerksPage(Clerk_view_number.Main, Customer);
        }
        private bool CanGoToClerkMainPage()
        {
            return true;
        }
    }
}
