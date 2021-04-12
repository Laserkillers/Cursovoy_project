using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cursovoy_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_login.Text == "Логин")
            {
                TextBox_login.Clear();
                TextBox_login.Opacity = 1.0;
            }
        }
        private void TextBox_login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_login.Text == "")
            {
                TextBox_login.BorderBrush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("Red");
                TextBox_login.Text = "Логин";
                TextBox_login.Opacity = 0.3;
            }
            else
                TextBox_login.BorderBrush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("#606060");
        }
        private void TextBox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_password.Text == "Пароль")
            {
                TextBox_password.Clear();
                TextBox_password.Opacity = 1.0;
            }
        }
        private void TextBox_password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_password.Text == "")
            {
                TextBox_password.BorderBrush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("Red");
                TextBox_password.Text = "Пароль";
                TextBox_password.Opacity = 0.3;
            }
            else
                TextBox_password.BorderBrush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("#606060");
        }
    }
}
