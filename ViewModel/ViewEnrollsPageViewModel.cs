using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class ViewEnrollsPageViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer;
        IMainWindowsCodeBehind _MainCodeBehind;
        public ViewEnrollsPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
            Customer = customer;
        }

        private DateTime? _SelectDate;
        public DateTime? SelectDate
        {
            get { return _SelectDate; }
            set
            {
                _SelectDate = value;

                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectDate)));
            }
        }

        private RelayCommand _GoToMasterMainPage;
        public RelayCommand GoToMasterMainPage
        {
            get { return _GoToMasterMainPage ??= new RelayCommand(OnGoToMasterMainPage, CanGoToMasterMainPage); }
        }

        private void OnGoToMasterMainPage()
        {
            _MainCodeBehind.LoadMasterPage(Master_Page_Load.Main, Customer);
        }
        private bool CanGoToMasterMainPage()
        {
            return true;
        }

        private RelayCommand _DoQuery;
        public RelayCommand DoQuery
        {
            get { return _DoQuery ??= new RelayCommand(OnDoQuery, CanDoQuery); }
        }

        private void OnDoQuery()
        {

        }
        private bool CanDoQuery()
        {
            return true;
        }

    }
}
