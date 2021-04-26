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
        AutoServiceContext db;
        IMainWindowsCodeBehind _MainCodeBehind;
        public ViewEnrollsPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
            Customer = customer;
            BuildTime(DateTime.Now);
        }

        private DateTime? _SelectDate;
        public DateTime? SelectDate
        {
            get { return _SelectDate; }
            set
            {
                _SelectDate = value;
                BuildTime((DateTime)value);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectDate)));
            }
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
            ListOfEnrolls = BuildList();
        }
        private bool CanDoQuery()
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

        private string _SumOfEnrolls;
        public string SumOfEnrolls
        {
            get { return _SumOfEnrolls; }
            set
            {
                _SumOfEnrolls = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SumOfEnrolls)));
            }
        }

        private List<AutoService> BuildList()
        {
            db = new AutoServiceContext();
            db.AutoServices.Load();
            List<AutoService> result = db.AutoServices.Local
                .Where(p => ((p.ReceptionTime > StartDate) && (p.ReceptionTime < EndDate)))
                .ToList();
            SumOfEnrolls = result.Count.ToString() + " машин";
            return result;
        }
    }
}
