using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Cursovoy_project.View
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void TextBox_login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login_box_LP.Text == "Логин")
                Login_box_LP.Clear();
        }
        private void TextBox_login_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((Login_box_LP.Text == "") || (Check_Correct_Login())) 
            {
                Login_box_LP.BorderBrush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("Red");
                if (Login_box_LP.Text == "")
                    Login_box_LP.Text = "Логин";
            }
            else
                Login_box_LP.BorderBrush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("Black");
        }

        private bool Check_Correct_Login()
        {
            if (Login_box_LP.Text == null)
                return true;
            Regex regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
            MatchCollection matches = regex.Matches(Login_box_LP.Text);
            if (matches.Count == 0)
                return true;
            else
                return false;
        }

        private void BindablePasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((PassBox.Password == "") || (Check_Correct_Password())) 
                MessageBox.Show("Неверно введён пароль!");
        }
        private bool Check_Correct_Password()
        {
            if (PassBox.Password == null)
                return true;
            Regex regex = new Regex(@"(?=^.{6,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            MatchCollection matches = regex.Matches(PassBox.Password);
            if (matches.Count == 0)
                return true;
            else
                return false;
        }
    }
}
