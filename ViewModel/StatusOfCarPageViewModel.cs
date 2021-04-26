using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.ComponentModel;
namespace Cursovoy_project.ViewModel
{
    class StatusOfCarPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private User Customer = new User();
        private AutoService Record = new AutoService();
        List<AutoService> Result = new List<AutoService> { };

        private IMainWindowsCodeBehind _MainCodeBehind;
        AutoServiceContext db;

        public StatusOfCarPageViewModel(User customer)
        {
            Customer = customer;
        }
        public StatusOfCarPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Customer = customer;
            ListOfCars = ReturnList();
        }
        /// <summary>
        /// Список машин клиента
        /// </summary>
        private List<string> _ListOfCars;
        public List<string> ListOfCars
        {
            get { return _ListOfCars; }
            set { _ListOfCars = value; }
        }
        private List<string> ReturnList()
        {
            db = new AutoServiceContext();
            db.Clients.Load();
            db.ClientsCarData.Load();
            int customer_id = Customer.Id;
            int[] customer_cars = db.Clients.Local
                .Where(p => (p.ClientId == customer_id))
                .Select(p => (p.CarId))
                .ToArray();
            List<string> list = new List<string>();
            for (int i = 0; i < customer_cars.Length; i++)
            {
                var temporary = db.ClientsCarData.Local
                    .Where(p => (p.Id == customer_cars[i]))
                    .ToList();
                foreach (ClientsCarDatum c in temporary)
                {
                    list.Add(c.GosNumber);
                }
            }
            db.Dispose();
            return list;
        }

        private string _SelectedCar;
        public string SelectedCar
        {
            get { return _SelectedCar; }
            set
            {
                _SelectedCar = value;
                Record.GosNumber = value;
                ClearBlocks();
                ChangeTextBloks();
                SetCarStatus();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedCar)));
            }
        }

        private void ClearBlocks()
        {
            GosNumber = null;
            CarFault = null;
            FaultsCost = null;
            ReceptionCarTime = null;
            IssureCarTime = null;
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

        private string _CarFault;
        public string CarFault
        {
            get { return _CarFault; }
            set
            {
                _CarFault = value;
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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ReceptionCarTime)));
            }
        }

        private string _IssureCarTime;
        public string IssureCarTime
        {
            get { return _IssureCarTime; }
            set
            {
                _IssureCarTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IssureCarTime)));
            }
        }

        private string _CarStatus;
        public string CarStatus
        {
            get { return _CarStatus; }
            set
            {
                _CarStatus = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CarStatus)));
            }
        }

        private void SetCarStatus()
        {
            if (NumbPage < Result.Count)
            { 
                if (Result[NumbPage].ReceptionTime > DateTime.Now)
                    CarStatus = "В ожидании машины...";
                else
                    CarStatus = "Машина в ремонтируется";
                if (Result[NumbPage].IssureTime != null)
                    CarStatus = "Машина готова к выдаче " + Result[NumbPage].IssureTime.ToString();
            }

        }

        private void ChangeTextBloks()
        {
            db = new AutoServiceContext();
            db.AutoServices.Load();
            Result = db.AutoServices.Local
                .Where(p => ((p.ClientId == Customer.Id) && (p.GosNumber == Record.GosNumber)))
                .ToList();
            SetResult(0);
            db.Dispose();
        }

        private bool SetResult(int i)
        {
            if (i < Result.Count)
            {
                GosNumber = Result[i].GosNumber;
                CarFault = Result[i].Fault;
                FaultsCost = Result[i].Cost.ToString() + " р.";
                ReceptionCarTime = Result[i].ReceptionTime.ToString("f");
                IssureCarTime = Result[i].IssureTime.ToString();
                SetCarStatus();
                return true;
            }
            else
                return false;
        }

        private int _NumbPage = 0;
        public int NumbPage
        {
            get { return _NumbPage; }
            set { _NumbPage = value; }
        }
        private string _NumberPage = "0/0";
        public string NumberPage
        {
            get { return _NumberPage; }
            set
            {
                _NumberPage = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumberPage)));
            }
        }

        private RelayCommand _GoBack;
        public RelayCommand GoBack
        {
            get { return _GoBack ??= new RelayCommand(OnGoBack, CanGoBack); }
        }
        private void OnGoBack()
        {
            if (NumbPage > 0)
            {
                NumbPage--;
                SetResult(NumbPage);
                _NumberPage = NumbPage.ToString() + "/" + Result.Count.ToString();
            }
        }
        private bool CanGoBack()
        {
            return true;
        }

        private RelayCommand _GoForward;
        public RelayCommand GoForward
        {
            get { return _GoForward ??= new RelayCommand(OnGoForward, CanGoForward); }
        }

        private void OnGoForward()
        {
            if (NumbPage < Result.Count)
            {
                NumbPage++;
                SetResult(NumbPage);
                _NumberPage = NumbPage.ToString() + "/" + Result.Count.ToString();
            }
        }
        private bool CanGoForward() { return true; }

        private RelayCommand _GoMenu;
        public RelayCommand GoMenu
        {
            get { return _GoMenu ??= new RelayCommand(OnGoMenu, CanGoMenu); }
        }
        private void OnGoMenu()
        {
            _MainCodeBehind.LoadClientPage(Client_Page_Load.Main, Customer);
        }
        private bool CanGoMenu()
        {
            return true;
        }
    }
}
