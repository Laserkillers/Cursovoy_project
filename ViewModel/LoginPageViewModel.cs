using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Npgsql;
using System.Data.Common;

namespace Cursovoy_project.ViewModel
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public LoginPageViewModel() { }

        private readonly string connectionString = "Host=127.0.0.1;Username=user_from_app;Password=12345;Database=AutoService;";

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

        private RelayCommand _GoToUserInterface;
        public RelayCommand GoToUserInterface
        {
            get { return _GoToUserInterface ?? new RelayCommand(OnGoToUserInterface, CanGoToUserInterface); }
        }

        private bool CanGoToUserInterface()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
            try
            {
                npgsqlConnection.Open();
                string sqlcommand = "Select * FROM users Where ";
                sqlcommand += Customer.Login;
            }
            catch (Exception)
            {
            }
            return true;
        }

        private void OnGoToUserInterface()
        {

        }
    }
}
