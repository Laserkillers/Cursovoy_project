using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Npgsql;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

namespace Cursovoy_project.ViewModel
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public LoginPageViewModel() { }

        AutoServiceContext db;

        private static string connectionString = "Host=127.0.0.1;Username=user_from_app;Password=12345;Database=AutoService;";

        private User Customer = new User();

        private IMainWindowsCodeBehind _MainCodeBehind;

        public LoginPageViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        /// <summary>
        /// Добавление обработки кнопки зарегистрироваться
        /// </summary>

        private RelayCommand _GoToRegisterPage;
        public RelayCommand GoToRegisterPage
        {
            get
            {
                return _GoToRegisterPage = _GoToRegisterPage ?? new RelayCommand(OnGoRegisterPage, CanGoRegisterPage);
            }
        }
        private bool CanGoRegisterPage()
        {
            return true;
        }
        private void OnGoRegisterPage()
        {
            _MainCodeBehind.Change_Background(Background_set.Main);
            _MainCodeBehind.LoadWiew(View_number.Register);
        }
        
        /// <summary>
        /// Добавление обработки текст бокса для логина
        /// </summary>
        private string _InputLoginLP = "Логин";
        public string InputLoginLP
        {
            get { return _InputLoginLP; }
            set
            {
                _InputLoginLP = value;
                Customer.Login = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_InputLoginLP)));
            }
        }

        /// <summary>
        /// Добавление обработки пассворд бокса
        /// </summary>
        private string _InputPasswordLP;
        public string InputPasswordLP
        {
            get { return _InputPasswordLP; }
            set
            {
                _InputPasswordLP = value;
                Customer.Password = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_InputPasswordLP)));
            }
        }
        private bool init_button = false;
        private RelayCommand _GoToUserInterface;
        public RelayCommand GoToUserInterface
        {
            get { return _GoToUserInterface = _GoToUserInterface ?? new RelayCommand(OnGoToUserInterface, CanGoToUserInterface); }
        }
        
        private bool CanGoToUserInterface()
        {
            if (!init_button)
            {
                init_button = true;
                return true;
            }
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
            try
            {
                npgsqlConnection.Open();
                string sqlcommand = "Select login, password FROM users Where login = '";
                sqlcommand += Customer.Login + "' and password = '" + Customer.Password + "';";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sqlcommand, npgsqlConnection);
                NpgsqlDataReader dataReader = npgsqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    npgsqlConnection.Close();
                    return true;
                }
                else
                {
                    npgsqlConnection.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                npgsqlConnection.Close();
                return false;
            }
        }
        
        private void OnGoToUserInterface()
        {
            db = new AutoServiceContext();
            db.Users.Load();
            int Check_login = db.Users.Local
                .Select(p => (p.Login, p.Password))
                .Where(p => ((p.Login == Customer.Login) && (p.Password == Customer.Password)))
                .Count();
            if (Check_login == 1)
            {
                var users = db.Users.ToList();
                foreach(User u in users)
                {
                    if ((u.Login == Customer.Login)&&(u.Password == Customer.Password))
                    {
                        Customer.Id = u.Id;
                        Customer.TypeOfAccount = u.TypeOfAccount;
                        break;
                    }
                }
               
                switch (Customer.TypeOfAccount)
                {
                    case 0://Admin
                        _MainCodeBehind.Change_Background(Background_set.Client);
                        _MainCodeBehind.LoadClientPage(Client_Page_Load.Main, Customer);
                        break;
                    case 1://Master
                        break;
                    case 2://Moderator
                        break;
                    case 3://Clerk
                        _MainCodeBehind.Change_Background(Background_set.Clerk);
                        _MainCodeBehind.LoadClerksPage(Clerk_view_number.Main);
                        break;
                    case 4://Client
                        _MainCodeBehind.Change_Background(Background_set.Client);
                        _MainCodeBehind.LoadClientPage(Client_Page_Load.Main, Customer);
                        break;
                    default:
                        break;
                }
            }
            db.Dispose();
        }
    }
}
