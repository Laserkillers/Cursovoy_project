using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class ModifyEnrollPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer = new User();

        private List<AutoService> Result = new List<AutoService> { };
        private IMainWindowsCodeBehind _MainCodeBehind;
        private AutoServiceContext db;

        private ClientsCarDatum car = new ClientsCarDatum();
        private AutoService Record = new AutoService();

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
                .Where(p => ((p.IssureTime == null) || (p.Cost == null) || (p.IssureTime > DateTime.Now)))
                .ToList();
            foreach (AutoService c in Result)
            {
                temporary.Add(c.GosNumber);
            }
            db.Dispose();
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

        private string _SelectedCar;
        public string SelectedCar
        {
            get { return _SelectedCar; }
            set
            {
                _SelectedCar = value;
                car.GosNumber = value;
                Record.GosNumber = value;
                UpdateBoxes();
            }
        }

        private void UpdateBoxes()
        {
            db = new AutoServiceContext();
            db.AutoServices.Load();
            db.ClientsCarData.Load();
            Record = db.AutoServices.Local
                .Where(p => (p.GosNumber == Record.GosNumber)&&((p.IssureTime == null) || (p.Cost == null) || (p.IssureTime > DateTime.Now)))
                .FirstOrDefault();
            car = db.ClientsCarData.Local
                .Where(p=> (p.GosNumber == car.GosNumber))
                .FirstOrDefault();
            GosNumber = Record.GosNumber;
            CarBrend = car.CarBrend;
            CarModel = car.CarModel;
            CarFault = Record.Fault;
            FaultsCost = Record.Cost.ToString();
            ReceptionCarTime = Record.ReceptionTime.ToString();
            db.Dispose();
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
        private string _CarBrend;
        public string CarBrend
        {
            get { return _CarBrend; }
            set
            {
                _CarBrend = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CarBrend)));
            }
        }
        private string _CarModel;
        public string CarModel
        {
            get { return _CarModel; }
            set
            {
                _CarModel = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CarModel)));
            }
        }
        private string _CarFault;
        public string CarFault
        {
            get { return _CarFault; }
            set
            {
                _CarFault = value;
                Record.Fault = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CarFault)));
            }
        }
        private string _FaultsCost;
        public string FaultsCost
        {
            get { return _FaultsCost; }
            set
            {
                _FaultsCost = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FaultsCost)));
            }
        }
        private string _ReceptionCarTime;
        public string ReceptionCarTime
        {
            get { return _ReceptionCarTime; }
            set
            {
                _ReceptionCarTime = value;
                Record.ReceptionTime = Convert.ToDateTime(value);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ReceptionCarTime)));
            }
        }
        private DateTime? _IssureDate;
        public DateTime? IssureDate
        {
            get { return _IssureDate; }
            set
            {
                _IssureDate = value;
                Record.IssureTime = value;
                ListOfTimes = ReturnList();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IssureDate)));
            }
        }
        private TimeSpan _SelectedTime;
        public TimeSpan SelectedTime
        { 
            get { return _SelectedTime; }
            set
            {
                _SelectedTime = value;
                DateTime dateNoTime = (DateTime)_IssureDate;
                dateNoTime = dateNoTime.AddHours(_SelectedTime.Hours);
                Record.IssureTime = dateNoTime;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedTime)));
            }
        }

        private List<TimeSpan> _ListOfTimes;
        public List<TimeSpan> ListOfTimes
        {
            get { return _ListOfTimes; }
            set
            {
                _ListOfTimes = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfTimes)));
            }
        }
        private List<TimeSpan> ReturnList()
        {
            db = new AutoServiceContext();
            db.AutoServices.Load();
            TimeSpan time = new TimeSpan(8, 0, 0);
            DateTime Date_time = (DateTime)Record.IssureTime;
            Date_time = Date_time.AddHours(time.Hours);
            List<TimeSpan> list = new List<TimeSpan>();
            for (int i = 0; Date_time.Hour <= 17; i++)
            {
                int Check_free_hours = db.AutoServices.Local
                .Where(p => (p.IssureTime == Date_time))
                .Count();
                if (Check_free_hours == 0)
                    list.Add(time);
                time += TimeSpan.FromHours(1);
                Date_time = Date_time.AddHours(1);
            }
            db.Dispose();
            return list;
        }

        private RelayCommand _GoSave;
        public RelayCommand GoSave
        {
            get { return _GoSave ??= new RelayCommand(OnGoSave, CanGoSave); }
        }

        private void OnGoSave()
        {
            try
            {
                SaveChangesToBd();
                _MainCodeBehind.ShowMessageBox("Успешно сохранено в базе данных");
            }
            catch (Exception e)
            {
                _MainCodeBehind.ShowMessageBox(e.ToString());
            }
        }
        private bool CanGoSave()
        {
            return true;
        }

        private bool SaveChangesToBd()
        {
            if (Record.Fault == null) throw new Exception("Не заполнено поле поломка авто");
            Record.Cost = Convert.ToDecimal(FaultsCost);
            db = new AutoServiceContext();
            db.Update(Record);
            db.SaveChanges();

            db.Dispose();
            return true;
        }
    }
}
