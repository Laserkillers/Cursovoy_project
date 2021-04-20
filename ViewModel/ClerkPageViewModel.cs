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
        private string FirstDate = "";
        private string SecondDate = "";

        public ClerkPageViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
        }
    
    }
}
