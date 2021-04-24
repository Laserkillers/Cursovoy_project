using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class EnrollCarPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer;
        private AutoService record = new AutoService();
        AutoServiceContext db;
        public EnrollCarPageViewModel(User _Customer) { Customer = _Customer; }

        private IMainWindowsCodeBehind _MainCodeBehind;
        public EnrollCarPageViewModel(IMainWindowsCodeBehind codeBehind, User _Customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            Customer = _Customer;
            _MainCodeBehind = codeBehind;
            
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
                record.IssureTime = (DateTime)value;
                ListOfTimes = ReturnList();
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfTimes)));
            }
        }

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
            /*int Check_free_hours = db.AutoServices.Local
                .Where(p=>( ))*/
            TimeSpan time = new TimeSpan(8, 0, 0);
            DateTime Date_time = record.IssureTime;
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
        private RelayCommand _GoToClientStartPage;
        public RelayCommand GoToClientStartPage
        {
            get { return _GoToClientStartPage ??= new RelayCommand(OnGoToClientStartPage, CanGoToClientStartPage); }
        }
        private bool CanGoToClientStartPage()
        {
            return true;
        }
        private void OnGoToClientStartPage()
        {
            _MainCodeBehind.LoadClientPage(Client_Page_Load.Main, Customer);
        }
    }
}
