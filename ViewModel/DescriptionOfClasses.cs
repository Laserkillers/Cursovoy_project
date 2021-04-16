using System;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class User
    {
        public User() { }
        public enum TypeOfAccount
        {
            Master,
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

        private string _Login = "Not set";
        public string Login
        {
            get { return _Login; }
            set { _Login = value; }
        }
        private string _Password = "Not set";
        public string Password
        {
            get { return _Password; }
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
        public User_content() { }
        private string _Surname = "Not set";
        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }

        private string _Name = "Not set";
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _MiddleName = "Not set";
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        private string _DateBirth = "Not set";
        public string DateBirth
        {
            get { return _DateBirth; }
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
        public Car_info() { }
        private string _GosNumber = "Not set";
        public string GosNumber
        {
            get { return _GosNumber; }
            set { _GosNumber = value; }
        }

        private string _CarMark = "Not set";
        public string CarMark
        {
            get { return _CarMark; }
            set { _CarMark = value; }
        }

        private string _CarBrand = "Not set";
        public string CarBrand
        {
            get { return _CarBrand; }
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
