using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Cursovoy_project.ViewModel
{
    class ModerAcceptAccsPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private User Customer = new User();

        private List<User> Results = new List<User>();
        private User SelectUser = new User();
        private UsersDatum SelectUserData = new UsersDatum();
        private AutoServiceContext db;

        IMainWindowsCodeBehind _MainCodeBehind;
        public ModerAcceptAccsPageViewModel(IMainWindowsCodeBehind codeBehind, User customer)
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
                        SelectUser.TypeOfAccount = 21;
                        break;
                    case "Бухгалтер":
                        SelectUser.TypeOfAccount = 31;
                        break;
                    case "Мастер-приёмщик":
                        SelectUser.TypeOfAccount = 11;
                        break;
                    default:
                        break;
                }
                ListOfAccs = BuildAccs();
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedType)));
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
                UpdateBoxes();
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
            get { return  _UserID; }
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
            _MainCodeBehind.LoadModeratorPage(Moderator_Pages.Main, Customer);
        }
        private bool CanGoToMainMenu()
        {
            return true;
        }

        private RelayCommand _AcceptAccount;
        public RelayCommand AcceptAccount
        {
            get { return _AcceptAccount ??= new RelayCommand(OnAcceptAccount, CanAcceptAccount); }
        }
        private void OnAcceptAccount()
        {
            db = new AutoServiceContext();
            switch (SelectUser.TypeOfAccount)
            {
                case 21:
                    SelectUser.TypeOfAccount = 2;
                    break;
                case 31:
                    SelectUser.TypeOfAccount = 3;
                    break;
                case 41:
                    SelectUser.TypeOfAccount = 4;
                    break;
                default:
                    break;
            }
            try
            {
                db.Users.Update(SelectUser);
                db.SaveChanges();
                db.Dispose();
                ListOfAccs = BuildAccs();
                _MainCodeBehind.ShowMessageBox("Изменения сохранены успешно!");
            }
            catch (Exception e)
            {
                _MainCodeBehind.ShowMessageBox(e.Message);
                throw;
            }
        }
        private bool CanAcceptAccount()
        {
            return true;
        }

        //ДОДЕЛАТЬ УДАЛЕНИЕ АККАУНТА!!!!
    }
}
