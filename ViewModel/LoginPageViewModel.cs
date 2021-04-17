using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public LoginPageViewModel() { }

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
    }
}
