using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;

namespace Cursovoy_project.ViewModel
{
    class ModerDeletePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer;
        private IMainWindowsCodeBehind _MainCodeBehind;
        private AutoServiceContext db;
        private List<AutoService> Results = new List<AutoService>();
        public ModerDeletePageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Customer = customer;
            ListOfRowToDelete = BuildList();
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
        private int _SelectedRow = 0;
        public int SelectedRow
        {
            get { return _SelectedRow; }
            set
            {
                _SelectedRow = value;
                NumberOfRow = value.ToString();
                UpdateBoxes(value);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedRow)));
            }
        }
        private void UpdateBoxes(int ID)
        {
            foreach (AutoService c in Results)
                if (c.Id == ID)
                {
                    GosNumber = c.GosNumber;
                    IssureDelete = c.Fault;
                    DateInput = c.ReceptionTime.ToString("D");
                    break;
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
        private RelayCommand _GoToMainMenu;
        public RelayCommand GoToMainMenu
        {
            get { return _GoToMainMenu ??= new RelayCommand(OnGoToMainMenu, CanGoToMainMenu); }
        }
        private void OnGoToMainMenu()
        {
            _MainCodeBehind.LoadModeratorPage(Moderator_Pages.Main, Customer);
        }
        private bool CanGoToMainMenu()
        {
            return true;
        }
        private List<int> BuildList()
        {
            db = new AutoServiceContext();
            List<int> rows = new List<int>();
            db.AutoServices.Load();
            Results = db.AutoServices.Local
                .Where(p => (p.NeedToDelete == true))
                .OrderBy(p => p.Id)
                .ToList();
            foreach (AutoService c in Results)
            {
                rows.Add(c.Id);
            }
            return rows;
        }

        private RelayCommand _DeleteRow;
        public RelayCommand DeleteRow
        {
            get { return _DeleteRow ??= new RelayCommand(OnDeleteRow, CanDeleteRow); }
        }
        private void OnDeleteRow()
        {
            db = new AutoServiceContext();
            if (_SelectedRow == 0)
                _MainCodeBehind.ShowMessageBox("Не выбрана запись");
            else
                try
                {
                    AutoService delete_row = db.AutoServices.Where(p => p.Id == SelectedRow).FirstOrDefault();
                    db.AutoServices.Remove(delete_row);
                    db.SaveChanges();
                    _MainCodeBehind.ShowMessageBox("Запись удалена успешно!");
                    ListOfRowToDelete = BuildList();
                }
                catch (Exception e)
                {
                    _MainCodeBehind.ShowMessageBox(e.Message);
                }

            db.Dispose();
        }
        private bool CanDeleteRow()
        {
            return true;
        }
    }
}
