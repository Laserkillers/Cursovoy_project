using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
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
        private UsersDatum Customer_data = new UsersDatum();
        private AutoServiceContext db;
        
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
            try
            {
                SendToDb();
                _MainCodeBehind.ShowMessageBox("Вы успешно зарегистрированы! Сейчас вы будете перенесены в свой профиль");
                _MainCodeBehind.LoadWiew(View_number.Login);//Заглушка
            }
            catch (Exception e)
            {
                _MainCodeBehind.ShowMessageBox(e.Message);
            }
            
            //_MainCodeBehind.LoadWiew(View_number.Login);
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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputLogin)));
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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputPassword)));
            }
        }
        /// <summary>
        /// Ввод фамилии
        /// </summary>
        private string _FamilyInput;
        public string FamilyInput
        {
            get { return _FamilyInput; }
            set
            {
                _FamilyInput = value;
                Customer_data.Surname = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FamilyInput)));
            }
        }
        /// <summary>
        /// Ввод имени
        /// </summary>
        private string _NameInput;
        public string NameInput
        {
            get { return _NameInput;}
            set
            {
                _NameInput = value;
                Customer_data.Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NameInput)));
            }
        }
        /// <summary>
        /// Ввод отчества
        /// </summary>
        private string _MidNameInput;
        public string MidNameInput
        {
            get { return _MidNameInput; }
            set
            {
                _MidNameInput = value;
                Customer_data.MiddleName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(MidNameInput)));
            }
        }
        /// <summary>
        /// Ввод даты рождения
        /// </summary>
        private DateTime? _InputDateBirth;
        public DateTime? InputDateBirth
        {
            get { return _InputDateBirth; }
            set
            {
                _InputDateBirth = value;
                Customer_data.DateBirth = (DateTime)value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputDateBirth)));
            }
        }
        ///<summary>
        /// Добавление обработки уровня доступа
        /// </summary>

        /*
        public enum TypeOfAccount
        {
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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Client_1)));
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
                    Customer.TypeOfAccount = 31;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Client_2)));
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
                    Customer.TypeOfAccount = 11;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Client_3)));
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
                    Customer.TypeOfAccount = 21;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Client_4)));
            }
        }
        /// <summary>
        /// Проверка логина на уникальность
        /// </summary>
        /// <param name="login">Проверка логина</param>
        /// <returns>Возвращает да или нет</returns>
        private bool CheckLogin(string login)
        {
            db = new AutoServiceContext();
            db.Users.Load();
            int temp = db.Users.Local
                .Where(p => (p.Login == login))
                .Count();
            db.Dispose();
            if (temp > 0)
                return false;
            return true;
        }
        private void SetId()
        {
            db = new AutoServiceContext();
            db.Users.Load();
            int numb_of_rows = db.Users.Local
                .Count();
            numb_of_rows--;

            int exist_space = db.Users.Local
                .Where(p => p.Id == numb_of_rows)
                .Count();
            while (exist_space > 0)
            {
                numb_of_rows++;
                exist_space = db.Users.Local
                .Where(p => p.Id == numb_of_rows)
                .Count();
            }
            Customer.Id = numb_of_rows;
            Customer_data.Id = numb_of_rows;
            Customer_data.IdNavigation = Customer;
            db.Dispose();
        }
        private bool SendToDb()
        {
            if (InputLogin == null) throw new Exception("Не введен логин!");
            if (InputPassword == null) throw new Exception("Не введен пароль!");
            if (!CheckLogin(Customer.Login)) throw new Exception("Логин не уникален! Введите другой");
            if (InputDateBirth == null) throw new Exception("Не введена дата рождения!");
            if (FamilyInput == null) throw new Exception("Не введена фамилия!");
            if (MidNameInput == null) throw new Exception("Не введено отчество!");
            if (NameInput == null) throw new Exception("Не введено имя!");
            if (Customer.TypeOfAccount == 0) throw new Exception("Не выбран тип профиля!");

            SetId();
            db = new AutoServiceContext();
            db.Users.Add(Customer);
            db.SaveChanges();
            db.UsersData.Add(Customer_data);
            db.SaveChanges();
            db.Dispose();

            return true;
        }
    }
}
