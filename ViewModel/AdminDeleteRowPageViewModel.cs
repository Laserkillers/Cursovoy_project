using System;
using System.ComponentModel;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class AdminDeleteRowPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private User Customer = new User();
        private AutoServiceContext db;
        IMainWindowsCodeBehind _MainCodeBehind;

        public AdminDeleteRowPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            _MainCodeBehind = codeBehind ?? throw new ArgumentNullException(nameof(codeBehind));
            Customer = customer;
            ListOfId = SetListID(true);
            BuildListEnrolls(true);
        }

        private DateTime? _SelectedDate;
        public DateTime? SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                BuildDates();
                ListOfId = SetListID(false);
                BuildListEnrolls(false);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedDate)));
            }
        }
        private DateTime StartDate;
        private DateTime EndDate;
        private void BuildDates()
        {
            DateTime date = (DateTime)SelectedDate;
            StartDate = new DateTime(date.Year, date.Month, date.Day);
            EndDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        private List<int> _ListOfId;
        public List<int> ListOfId
        {
            get { return _ListOfId; }
            set
            {
                _ListOfId = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfId)));
            }
        }
        /// <summary>
        /// Выдаёт список ID для комбобокса
        /// </summary>
        /// <param name="standart_time">Использовать ли время сегодняшнее или нет</param>
        /// <returns>Лист ID</returns>
        private List<int> SetListID(bool standart_time)
        {
            db = new AutoServiceContext();
            List<int> temp = new List<int>();
            if (standart_time)
                temp = db.AutoServices.Where(p => (p.IssureTime < DateTime.Now))
                    .Select(p => (p.Id)).ToList();
            else
                temp = db.AutoServices.Where(p => ((p.IssureTime > StartDate) && (p.IssureTime < EndDate)))
                    .Select(p => (p.Id)).ToList();
            db.Dispose();
            return temp;
        }
        private int _SelectedId = -1;
        public int SelectedId
        {
            get { return _SelectedId; }
            set
            {
                _SelectedId = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedId)));
            }
        }

        private RelayCommand _GoToMain;
        public RelayCommand GoToMain
        {
            get { return _GoToMain ??= new RelayCommand(OnGoToMain, CanGoToMain); }
        }
        private void OnGoToMain()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.Main, Customer);
        }
        private bool CanGoToMain()
        {
            return true;
        }
        private RelayCommand _DeleteSelectedRow;
        public RelayCommand DeleteSelectedRow
        {
            get { return _DeleteSelectedRow ??= new RelayCommand(OnDeleteSelectedRow, CanDeleteSelectedRow); }
        }
        private void OnDeleteSelectedRow()
        {
            if (SelectedId == -1)
                _MainCodeBehind.ShowMessageBox("Не выбран ID");
            else
            {
                db = new AutoServiceContext();
                AutoService remove = db.AutoServices.Where(p => (p.Id == SelectedId)).FirstOrDefault();
                db.AutoServices.Remove(remove);
                db.SaveChanges();
                db.Dispose();
                SelectedId = -1;
                BuildListEnrolls(true);
                ListOfId = SetListID(true);
                _MainCodeBehind.ShowMessageBox("Запись удалена успешно!");
            }
        }
        private bool CanDeleteSelectedRow()
        {
            return true;
        }
        private List<AutoService> _ListOfEnrolls;
        public List<AutoService> ListOfEnrolls
        {
            get { return _ListOfEnrolls; }
            set
            {
                _ListOfEnrolls = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfEnrolls)));
            }
        }
        /// <summary>
        /// Построение таблицы записей
        /// </summary>
        /// <param name="SetDefault">Все записи или только сегодняшний день</param>
        private void BuildListEnrolls(bool SetDefault)
        {
            db = new AutoServiceContext();
            if (SetDefault)
                ListOfEnrolls = db.AutoServices.Where(p => (p.IssureTime < DateTime.Now)).ToList();
            else
                ListOfEnrolls = db.AutoServices.Where(p => ((p.IssureTime > StartDate) && (p.IssureTime < EndDate))).ToList();
            NumberOfRows = ListOfEnrolls.Count.ToString() + " записей";
            db.Dispose();
        }
        private string _NumberOfRows;
        public string NumberOfRows
        {
            get { return _NumberOfRows; }
            set
            {
                _NumberOfRows = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumberOfRows)));
            }
        }
    }
}
