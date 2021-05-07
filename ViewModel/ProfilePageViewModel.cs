using System;
using System.ComponentModel;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class ProfilePageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private User Customer = new User();
        private UsersDatum CustomerData = new UsersDatum();
        private AutoServiceContext db;
        private IMainWindowsCodeBehind _MainCodeBehind;

        public ProfilePageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            _MainCodeBehind = codeBehind ?? throw new ArgumentNullException(nameof(codeBehind));
            Customer = customer;
            UpdateBoxes();
        }

        private void UpdateBoxes()
        {
            db = new AutoServiceContext();
            CustomerData = db.UsersData.Where(p => (p.Id == Customer.Id)).FirstOrDefault();
            db.Dispose();
            Greeting = CustomerData.Name + "!";
            SurName = CustomerData.Surname;
            Name = CustomerData.Name;
            SecondName = CustomerData.MiddleName;
            Email = CustomerData.Email ?? "";
            Telephone = CustomerData.Telephone ?? "";
            DateBirth = CustomerData.DateBirth.ToString("D");
        }

        private string _Greeting;
        public string Greeting
        {
            get { return _Greeting; }
            set
            {
                _Greeting = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Greeting)));
            }
        }
        private string _SurName;
        public string SurName
        {
            get { return _SurName; }
            set
            {
                _SurName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SurName)));
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Name))); 
            }
        }
        private string _SecondName;
        public string SecondName
        {
            get { return _SecondName; }
            set
            {
                _SecondName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SecondName)));
            }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }
        private string _Telephone;
        public string Telephone
        {
            get { return _Telephone; }
            set
            {
                _Telephone = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Telephone)));
            }
        }
        private string _DateBirth;
        public string DateBirth
        {
            get { return _DateBirth; }
            set
            {
                _DateBirth = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(DateBirth)));
            }
        }

        private RelayCommand _GoToUserInterface;
        public RelayCommand GoToUserInterface
        {
            get { return _GoToUserInterface ??= new RelayCommand(OnGoToUserInterface, CanGoToUserInterface); }
        }
        private void OnGoToUserInterface()
        {
            switch (Customer.TypeOfAccount)
            {
                case 1:
                    _MainCodeBehind.Change_Background(Background_set.Master);
                    _MainCodeBehind.LoadMasterPage(Master_Page_Load.Main, Customer);
                    break;
                case 2:
                    _MainCodeBehind.Change_Background(Background_set.Moderator);
                    _MainCodeBehind.LoadModeratorPage(Moderator_Pages.Main, Customer);
                    break;
                case 3:
                    _MainCodeBehind.Change_Background(Background_set.Clerk);
                    _MainCodeBehind.LoadClerksPage(Clerk_view_number.Main, Customer);
                    break;
                case 4:
                    _MainCodeBehind.Change_Background(Background_set.Client);
                    _MainCodeBehind.LoadClientPage(Client_Page_Load.Main, Customer);
                    break;
                default:
                    _MainCodeBehind.ShowMessageBox("Ваш аккаунт не подтвержден! Пожалуйста, подождите, пока модератор не проверит ваш аккаунт");
                    break;
            }
        }
        private bool CanGoToUserInterface()
        {
            return true;
        }

        private RelayCommand _SaveChanges;
        public RelayCommand SaveChanges
        {
            get { return _SaveChanges ??= new RelayCommand(OnSaveChanges, CanSaveChanges); }
        }
        private void OnSaveChanges()
        {
            try
            {
                ReceiveData();
                db = new AutoServiceContext();
                db.UsersData.Update(CustomerData);
                db.SaveChanges();
                _MainCodeBehind.ShowMessageBox("Изменения сохранены успешно!");
                UpdateBoxes();
            }
            catch (Exception e)
            {
                _MainCodeBehind.ShowMessageBox(e.Message);
            }
            db.Dispose();
        }
        private void ReceiveData()
        {
            CustomerData.Email = Email ?? throw new Exception("Не введён емаил");
            CustomerData.MiddleName = SecondName ?? throw new Exception("Не введено отчество");
            CustomerData.Name = Name ?? throw new Exception("Не введено имя");
            CustomerData.Surname = SurName ?? throw new Exception("Не введена фамилия");
            CustomerData.Telephone = Telephone ?? throw new Exception("Не введен телефон");
        }
        private bool CanSaveChanges()
        {
            return true;
        }

        private RelayCommand _GoToLoginPage;
        public RelayCommand GoToLoginPage
        {
            get { return _GoToLoginPage; }
        }
        private void OnGoToLoginPage()
        {
            _MainCodeBehind.LoadWiew(View_number.Main);
        }
        private bool CanGoToLoginPage()
        {
            return true;
        }
    }
}
