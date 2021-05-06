using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class AdminDeleteUserPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer = new User();

        private List<User> Results = new List<User>();
        private User SelectUser = new User();
        private UsersDatum SelectUserData = new UsersDatum();
        private AutoServiceContext db;

        IMainWindowsCodeBehind _MainCodeBehind;
        public AdminDeleteUserPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Customer = customer;
            ListOfTypeAccs = SetListOfAccs();
        }

        private List<string> _ListOfTypeAccs;
        public List<string> ListOfTypeAccs
        {
            get { return _ListOfTypeAccs; }
            set
            {
                _ListOfTypeAccs = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfTypeAccs)));
            }
        }
        private List<string> SetListOfAccs()
        {
            List<string> temp = new List<string>();
            temp.Add("Модератор");
            temp.Add("Бухгалтер");
            temp.Add("Мастер-приёмщик");
            temp.Add("Клиент");
            return temp;
        }

        private string _SelectedType;
        public string SelectedType
        {
            get { return _SelectedType; }
            set
            {
                _SelectedType = value;
                switch (value)
                {
                    case "Модератор":
                        SelectUser.TypeOfAccount = 2;
                        break;
                    case "Бухгалтер":
                        SelectUser.TypeOfAccount = 3;
                        break;
                    case "Мастер-приёмщик":
                        SelectUser.TypeOfAccount = 1;
                        break;
                    case "Клиент":
                        SelectUser.TypeOfAccount = 4;
                        break;
                    default:
                        break;
                }
                ListOfAccs = BuildAccs();
            }
        }

        private List<string> _ListOfAccs;
        public List<string> ListOfAccs
        {
            get { return _ListOfAccs; }
            set
            {
                _ListOfAccs = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ListOfAccs)));
            }
        }
        private List<string> BuildAccs()
        {
            List<string> temp = new List<string>();
            db = new AutoServiceContext();
            Results = db.Users.Where(p => (p.TypeOfAccount == SelectUser.TypeOfAccount))
                .ToList();
            foreach (var c in Results)
                temp.Add(c.Login);
            db.Dispose();
            return temp;
        }
        private string _SelectedAcc;
        public string SelectedAcc
        {
            get { return _SelectedAcc; }
            set
            {
                _SelectedAcc = value;
                SelectUser.Login = value;
                if (value != null)
                    UpdateBoxes();
                else
                {
                    UserID = "";
                    UserLog = "";
                    UserName = "";
                    UserSurName = "";
                    UserTelephone = "";
                    UserEmail = "";
                }
            }
        }
        private void UpdateBoxes()
        {
            for (int i = 0; i < Results.Count(); i++)
            {
                if (Results[i].Login == SelectUser.Login)
                {
                    SelectUser = Results[i];
                    break;
                }
            }
            db = new AutoServiceContext();
            SelectUserData = db.UsersData.Where(p => (p.Id == SelectUser.Id))
                .FirstOrDefault();
            UserID = SelectUser.Id.ToString();
            UserLog = SelectUser.Login;
            UserName = SelectUserData.Name;
            UserSurName = SelectUserData.Surname;
            UserTelephone = SelectUserData.Telephone;
            UserEmail = SelectUserData.Email;
            db.Dispose();
        }

        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set
            {
                _UserID = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(UserID)));
            }
        }
        private string _UserLog;
        public string UserLog
        {
            get { return _UserLog; }
            set
            {
                _UserLog = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(UserLog)));
            }
        }
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(UserName)));
            }
        }
        private string _UserSurName;
        public string UserSurName
        {
            get { return _UserSurName; }
            set
            {
                _UserSurName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(UserSurName)));
            }
        }
        private string _UserTelephone;
        public string UserTelephone
        {
            get { return _UserTelephone; }
            set
            {
                _UserTelephone = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(UserTelephone)));
            }
        }
        private string _UserEmail;
        public string UserEmail
        {
            get { return _UserEmail; }
            set
            {
                _UserEmail = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(UserEmail)));
            }
        }

        private RelayCommand _GoToMainMenu;
        public RelayCommand GoToMainMenu
        {
            get { return _GoToMainMenu ??= new RelayCommand(OnGoToMainMenu, CanGoToMainMenu); }
        }
        private void OnGoToMainMenu()
        {
            _MainCodeBehind.LoadAdminPage(Admin_Pages.Main, Customer);
        }
        private bool CanGoToMainMenu()
        {
            return true;
        }

        private RelayCommand _DeleteAccount;
        public RelayCommand DeleteAccount
        {
            get { return _DeleteAccount ??= new RelayCommand(OnDeleteAccount, CanDeleteAccount); }
        }
        private void OnDeleteAccount()
        {
            db = new AutoServiceContext();
            //int temp_id = SelectUser.Id;
            SelectUser.UsersDatum = SelectUserData;
            try
            {
                if (SelectUser.TypeOfAccount == 4)
                {
                    int user_id = SelectUser.Id;
                    db.Users.Remove(SelectUser);
                    List<Client> Temp = db.Clients.Where(p => (p.ClientId == user_id)).ToList();
                    List<int> user_cars = new List<int>();
                    List<ClientsCarDatum> User_cars = new List<ClientsCarDatum>();
                    List<AutoService> user_enrolls = db.AutoServices
                        .Where(p => ((p.ClientId == user_id) && (p.ReceptionTime > DateTime.Now) && (p.IssureTime == null)))
                        .ToList();
                    foreach (var c in Temp)
                        user_cars.Add(c.CarId);
                    for (int i = 0; i < user_cars.Count; i++)
                    {
                        User_cars.Add(db.ClientsCarData.Where(p => (p.Id == user_cars[i])).FirstOrDefault());
                    }
                    db.AutoServices.RemoveRange(user_enrolls);
                    db.Clients.RemoveRange(Temp);
                    db.ClientsCarData.RemoveRange(User_cars);
                    db.SaveChanges();
                }
                else
                {
                    db.Users.Remove(SelectUser);
                    db.SaveChanges();
                }
                ListOfAccs = BuildAccs();
                _MainCodeBehind.ShowMessageBox("Удаление прошло успешно!");
            }
            catch (Exception e)
            {
                _MainCodeBehind.ShowMessageBox(e.Message);
                throw;
            }
            db.Dispose();
        }
        private bool CanDeleteAccount()
        {
            return true;
        }
    }
}
