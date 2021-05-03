using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Cursovoy_project.View;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class EnrollCarPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer;
        private ClientsCarDatum CarData = new ClientsCarDatum();
        private Client car_owner = new Client();
        private AutoService Record = new AutoService();
        AutoServiceContext db;
        public EnrollCarPageViewModel(User _Customer) { Customer = _Customer; }

        private IMainWindowsCodeBehind _MainCodeBehind;
        public EnrollCarPageViewModel(IMainWindowsCodeBehind codeBehind, User _Customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            Customer = _Customer;
            _MainCodeBehind = codeBehind;
            ListOfCars = ReturnCarList();
        }
        /// <summary>
        /// Реализация передачи данных из DatePicker
        /// </summary>
        private DateTime? _ReceptionTime;
        public DateTime? ReceptionTime
        {
            get { return _ReceptionTime; }
            set 
            { 
                _ReceptionTime = value;
                Record.ReceptionTime = (DateTime)value;
                ListOfTimes = ReturnList();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfTimes)));
            }
        }
        /// <summary>
        /// Список с временами приёма
        /// </summary>
        private List<TimeSpan> _ListOfTimes;
        public List<TimeSpan> ListOfTimes
        {
            get { return _ListOfTimes; }
            set {_ListOfTimes = value;}
        }
        /// <summary>
        /// Создаёт список данных на основе выбора даты пользователем
        /// </summary>
        /// <returns></returns>
        private List<TimeSpan> ReturnList()
        {
            db = new AutoServiceContext();
            db.AutoServices.Load();
            TimeSpan time = new TimeSpan(8, 0, 0);
            DateTime Date_time = Record.ReceptionTime;
            Date_time = Date_time.AddHours(time.Hours);
            List<TimeSpan> list = new List<TimeSpan>();
            for (int i = 0; Date_time.Hour <= 17; i++)
            {
                int Check_free_hours = db.AutoServices.Local
                .Where(p => (p.ReceptionTime == Date_time))
                .Count();
                if (Check_free_hours == 0)
                    list.Add(time);
                time += TimeSpan.FromHours(1);
                Date_time = Date_time.AddHours(1);
            }
            db.Dispose();
            return list;
        }
        /// <summary>
        /// Комманда отвечающая за возращение назад на главную страницу
        /// </summary>
        private RelayCommand _GoToClientStartPageWithOut;
        public RelayCommand GoToClientStartPageWithOut
        {
            get { return _GoToClientStartPageWithOut ??= new RelayCommand(OnGoToClientStartPage, CanGoToClientStartPage); }
        }
        private bool CanGoToClientStartPage()
        {
            return true;
        }
        private void OnGoToClientStartPage()
        {
            _MainCodeBehind.LoadClientPage(Client_Page_Load.Main, Customer);
        }
        /// <summary>
        /// Ввод госномера
        /// </summary>
        private string _InputGosNumber;
        public string InputGosNumber
        {
            get { return _InputGosNumber; }
            set 
            { 
                _InputGosNumber = value;
                CarData.GosNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputGosNumber)));
            }
        }
        /// <summary>
        /// Ввод неисправности
        /// </summary>
        private string _InputFault;
        public string InputFault
        {
            get { return _InputFault; }
            set
            {
                _InputFault = value;
                Record.Fault = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputFault)));
            }
        }
        /// <summary>
        /// Ввод бренда авто
        /// </summary>
        private string _InputCarBrend;
        public string InputCarBrend
        {
            get { return _InputCarBrend; }
            set
            {
                _InputCarBrend = value;
                CarData.CarBrend = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputCarBrend)));
            }
        }
        /// <summary>
        /// Ввод модели авто
        /// </summary>
        private string _InputCarModel;
        public string InputCarModel
        {
            get { return _InputCarModel; }
            set
            {
                _InputCarModel = value;
                CarData.CarModel = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputCarModel)));
            }
        }
        /// <summary>
        /// Ввод пробега машины
        /// </summary>
        private int _InputCarOdometr;
        public int InputCarOdometr
        {
            get { return _InputCarOdometr; }
            set
            {
                _InputCarOdometr = value;
                
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputCarOdometr)));
            }
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
        private List<string> ReturnCarList()
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
        /// <summary>
        /// Выбор пользователем машины
        /// </summary>
        private string _SelectedCar;
        public string SelectedCar 
        {
            get { return _SelectedCar; }
            set
            {
                _SelectedCar = value;
                Record.GosNumber = value;
                ChangeTextBoxes(value);
            }
        }
        private void ChangeTextBoxes(string gos_number)
        {
            db = new AutoServiceContext();
            db.ClientsCarData.Load();
            var car_data = db.ClientsCarData.Local
                .Where(p => (p.GosNumber == gos_number))
                .ToList();
            foreach (ClientsCarDatum c in car_data)
            {
                CarData.Id = c.Id;
                InputGosNumber = c.GosNumber;
                InputCarBrend = c.CarBrend;
                InputCarModel = c.CarModel;
                InputCarOdometr = (int)c.Odometr;
            }
        }

        private TimeSpan _SelectedTime;
        public TimeSpan SelectedTime
        {
            get { return _SelectedTime; }
            set
            {
                _SelectedTime = value;
                DateTime dateNoTime = new DateTime(Record.ReceptionTime.Year, Record.ReceptionTime.Month, Record.ReceptionTime.Day);
                dateNoTime = dateNoTime.AddHours(_SelectedTime.Hours);
                Record.ReceptionTime = dateNoTime;
            }
        }

        private RelayCommand _GoToClientStartPageWithSave;
        public RelayCommand GoToClientStartPageWithSave
        {
            get { return _GoToClientStartPageWithSave ??= new RelayCommand(OnGoToClientStartPageWithSave, CanGoToClientStartPageWithSave); }
        }

        private void OnGoToClientStartPageWithSave()
        {
            try
            {
                SaveChangesToDb();
                _MainCodeBehind.ShowMessageBox("Запись успешно добавлена!");
                _MainCodeBehind.LoadClientPage(Client_Page_Load.Main, Customer);
            }
            catch (Exception e)
            {
                _MainCodeBehind.ShowMessageBox(e.Message);
            }
            
        }
        private bool CanGoToClientStartPageWithSave()
        {
            return true;
        }
        /// <summary>
        /// Добавление записей в базу данных
        /// </summary>
        private void SaveChangesToDb()
        {
            if (CarData.CarBrend == null) throw new Exception("Не заполнено поле: марка авто");
            if (CarData.CarModel == null) throw new Exception("Не заполнено поле: модель авто");
            if (CarData.GosNumber == null) throw new Exception("Не заполнено поле: госномер авто");
            if (InputCarOdometr == 0) throw new Exception("Не заполнено поле: пробег авто");
            if (Record.Fault == null) throw new Exception("Не заполнено поле: поломка авто");
            if (SelectedTime == null) throw new Exception("Не заполнено поле: время записи");
            if (ReceptionTime == null) throw new Exception("Не заполнено поле: дата записи");

            db = new AutoServiceContext();
            Record.ClientId = Customer.Id;
            car_owner.ClientId = Customer.Id;
            if (((InputGosNumber != Record.GosNumber) || (CarData.Odometr != InputCarOdometr)) && (Record.GosNumber != null))
            {
                Record.GosNumber = InputGosNumber;
                CarData.Odometr = InputCarOdometr;
                if (CarData.Id != 0)
                {
                    db.Update(CarData);
                    db.SaveChanges();
                }
                db.AutoServices.Load();
                db.AutoServices.Add(Record);
                db.SaveChanges();
            }
            else if ((InputGosNumber == Record.GosNumber))
            {
                db.AutoServices.Load();
                db.AutoServices.Add(Record);
                db.SaveChanges();
            }
            else
            {
                Record.GosNumber = InputGosNumber;
                db.ClientsCarData.Load();
                db.AutoServices.Load();
                db.AutoServices.Add(Record);
                db.ClientsCarData.Add(CarData);
                db.SaveChanges();
                db.ClientsCarData.Load();
                db.Clients.Load();
                
                int record = db.ClientsCarData.Local
                    .Where(p => p.GosNumber == CarData.GosNumber)
                    .Select(p => p.Id)
                    .First();
                
                CarData.Id = record;
                car_owner.CarId = CarData.Id;
                db.Clients.Add(car_owner);
                db.SaveChanges();
            }
            db.Dispose();
        }
        /*
        private User Customer;
        private ClientsCarDatum CarData = new ClientsCarDatum();
        private Client car_owner = new Client();
        private AutoService Record = new AutoService();
         
         */
    }
}
