using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;

namespace Cursovoy_project.ViewModel
{
    class ModerDeletePageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate {};
        private User Customer;
        private IMainWindowsCodeBehind _MainCodeBehind;

        public void ModeratorMainPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Customer = customer;
        }

        private List<string> _ListOfRowToDelete;
        public List<string> ListOfRowToDelete
        {
            get { return _ListOfRowToDelete; }
            set
            {
                _ListOfRowToDelete = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfRowToDelete)));
            }
        }
        private string _SelectedRow;
        public string SelectedRow
        {
            get { return _SelectedRow; }
            set
            {
                _SelectedRow = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedRow)));
            }
        }
    }
}
