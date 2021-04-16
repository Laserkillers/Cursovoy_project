using System;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class User
    {
        public enum TypeOfAccount
        {
            Administrator,
            Moderator,
            Clerk,
            Client
        }

        private TypeOfAccount _AccountType = TypeOfAccount.Client;
        public TypeOfAccount AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }

        private string _Login;
        public string Login
        {
            get { return _Login ?? "Not set"; }
            set { _Login = value; }
        }
        private string _Password;
        public string Password
        {
            get { return _Password ?? "Not set"; }
            set { _Password = value; }
        }
        private bool _Validated = false;
        public bool Validated
        {
            get { return _Validated; }
            set { _Validated = value; }
        }
    }
    class User_content
    {
        private string _Surname;
        public string Surname
        {
            get { return _Surname ?? "Not set"; }
            set { _Surname = value; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name ?? "Not set"; }
            set { _Name = value; }
        }

        private string _MiddleName;
        public string MiddleName
        {
            get { return _MiddleName ?? "Not set"; }
            set { _MiddleName = value; }
        }

        private string _DateBirth;
        public string DateBirth
        {
            get { return _DateBirth ?? "Not set"; }
            set { _DateBirth = value; }
        }

        private bool _SendToDatabase = false;
        public bool SendToDatabase
        {
            get { return _SendToDatabase; }
            set { _SendToDatabase = value; }
        }

        private bool _GetFromDatabase = false;
        public bool GetFromDatabase
        {
            get { return _GetFromDatabase; }
            set { _GetFromDatabase = value; }
        }
    }
    class Car_info
    {
        private string _GosNumber;
        public string GosNumber
        {
            get { return _GosNumber ?? "Not set"; }
            set { _GosNumber = value; }
        }

        private string _CarMark;
        public string CarMark
        {
            get { return _CarMark ?? "Not set"; }
            set { _CarMark = value; }
        }

        private string _CarBrand;
        public string CarBrand
        {
            get { return _CarBrand ?? "Not set"; }
            set { _CarBrand = value; }
        }

        private bool _SendToDatabase = false;
        public bool SendToDatabase
        {
            get { return _SendToDatabase; }
            set { _SendToDatabase = value; }
        }

        private bool _GetFromDatabase = false;
        public bool GetFromDatabase
        {
            get { return _GetFromDatabase; }
            set { _GetFromDatabase = value; }
        }
    }
    class DescriptionOfClasses
    {
    }
}
