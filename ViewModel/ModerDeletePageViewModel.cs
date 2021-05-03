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
        private AutoServiceContext db;

        public void ModeratorMainPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Customer = customer;
        }

        private List<int> _ListOfRowToDelete;
        public List<int> ListOfRowToDelete
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
        private string _NumberOfRow;
        public string NumberOfRow
        {
            get { return _NumberOfRow; }
            set
            {
                _NumberOfRow = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumberOfRow)));
            }
        }
        private string _GosNumber;
        public string GosNumber
        {
            get { return _GosNumber; }
            set
            {
                _GosNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(GosNumber)));
            }
        }
        private string _IssureDelete;
        public string IssureDelete
        {
            get { return _IssureDelete; }
            set
            {
                _IssureDelete = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IssureDelete)));
            }
        }
        private string _DateInput;
        public string DateInput
        {
            get { return _DateInput; }
            set
            {
                _DateInput = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(DateInput)));
            }
        }

        private List<int> BuildList()
        {
            db = new AutoServiceContext();
            List<int> rows = new List<int>();
            //Доделать!
            return rows;
        }
    }
}
