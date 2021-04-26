using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class ModifyEnrollPageViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private User Customer = new User();
        private List<AutoService> Result = new List<AutoService> { };
        private IMainWindowsCodeBehind _MainCodeBehind;
        private AutoServiceContext db;

        public ModifyEnrollPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            BuildTime(DateTime.Now);
            _MainCodeBehind = codeBehind;
            ListOfCars = BuildListOfCars();
            Customer = customer;
        }

        private DateTime _StartDate;
        private DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private DateTime _EndDate;
        private DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private void BuildTime(DateTime dateTime)
        {
            StartDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
            EndDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }
        //Добавить кучу связей!!!
        /// <summary>
        /// Список машин, которые в ремонте
        /// </summary>
        private List<string> _ListOfCars;
        public List<string> ListOfCars
        {
            get { return _ListOfCars; }
            set
            {
                _ListOfCars = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfCars)));
            }
        }
        private List<string> BuildListOfCars()
        {
            db = new AutoServiceContext();
            db.AutoServices.Load();
            List<string> temporary = new List<string>();
            Result = db.AutoServices.Local
                .Where(p=>((p.IssureTime == null)||(p.Cost == null)||(p.IssureTime > DateTime.Now)))
                .ToList();
            foreach (AutoService c in Result)
            {
                temporary.Add(c.GosNumber);
            }
            return temporary;
        }

        private RelayCommand _GoMenu;
        public RelayCommand GoMenu
        {
            get { return _GoMenu ??= new RelayCommand(OnGoMenu, CanGoMenu); }
        }
        private void OnGoMenu()
        {
            _MainCodeBehind.LoadMasterPage(Master_Page_Load.Main, Customer);
        }
        private bool CanGoMenu()
        {
            return true;
        }
    }
}
