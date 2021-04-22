using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class RegisterPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;

        private User Customer = new User();
        //private User_content _Customer_data;
        
        public RegisterPageViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
        }

        private RelayCommand _GoToLoginPage;
        public RelayCommand GoToLoginPage
        {
            get
            {
                return _GoToLoginPage = _GoToLoginPage ?? new RelayCommand(OnGoToLoginPage, CanGoToLoginPage);
            }
        }
        private bool CanGoToLoginPage()
        {
            return true;
        }
        private void OnGoToLoginPage()
        {
            _MainCodeBehind.LoadWiew(View_number.Login);
        }

        private RelayCommand _Register;

        public RelayCommand Register
        {
            get
            {
                return _Register ??= new RelayCommand(OnGoRegister, CanGoRegister);
            }
        }

        private bool CanGoRegister()
        {
            return true;
        }
        private void OnGoRegister()
        {
            _MainCodeBehind.LoadWiew(View_number.Login);
        }

        // Добавление обработки кнопок
        ///<summary>
        /// Добавление обработки кнопки логина
        /// </summary>
        private string _InputLogin;
        public string InputLogin
        {
            get { return _InputLogin; }
            set 
            { 
                _InputLogin = value;
                Customer.Login = _InputLogin;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_InputLogin)));
            }
        }
        ///<summary>
        /// Добавление обработки кнопки пароля
        /// </summary>
        private string _InputPassword;
        public string InputPassword
        {
            get { return _InputPassword; }
            set
            {
                _InputPassword = value;
                Customer.Password = _InputPassword;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_InputPassword)));
            }
        }
        ///<summary>
        /// Добавление обработки уровня доступа
        /// </summary>
 
        /*
        public enum TypeOfAccount
        {
            Admin, 0
            Master, 1
            Moderator, 2
            Clerk, 3
            Client 4
        }
        */
        private bool _Client_1;
        public bool Client_1
        {
            get { return _Client_1; }
            set
            {
                _Client_1 = value;
                if (value)
                    Customer.TypeOfAccount = 4;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Client_1)));
            }
        }
        private bool _Client_2;
        public bool Client_2
        {
            get { return _Client_2; }
            set
            {
                _Client_2 = value;
                if (value)
                    Customer.TypeOfAccount = 3;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Client_2)));
            }
        }
        private bool _Client_3;
        public bool Client_3
        {
            get { return _Client_3; }
            set
            {
                _Client_3 = value;
                if (value)
                    Customer.TypeOfAccount = 1;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Client_3)));
            }
        }
        private bool _Client_4;
        public bool Client_4
        {
            get { return _Client_4; }
            set
            {
                _Client_4 = value;
                if (value)
                    Customer.TypeOfAccount = 2;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Client_4)));
            }
        }
    }
}
