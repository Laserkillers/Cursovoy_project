using Cursovoy_project.View;
using Cursovoy_project.ViewModel;
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
    public interface IMainWindowsCodeBehind
    {
        void LoadWiew(View_number typeView);
    }
    public enum View_number
    {
        Main,
        Login,
        Register,
        UserInterface
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindowsCodeBehind
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_loaded;
        }
        private void MainWindow_loaded(object sender, RoutedEventArgs e)
        {
            MenuViewModel vm = new MenuViewModel();
            vm.CodeBehind = this;
            this.DataContext = vm;

            LoadWiew(View_number.Main);
        }

        public void LoadWiew(View_number typeView)
        {
            switch (typeView)
            {
                case View_number.Main:
                    // Загружаем страницу
                    StartPage view = new StartPage();
                    StartPageViewModel viewModel = new StartPageViewModel(this);
                    //Связываем
                    view.DataContext = viewModel;
                    //Отображаем
                    this.OutputView.Content = view;
                    break;
                case View_number.Login:
                    LoginPage viewL = new LoginPage();
                    LoginPageViewModel viewModelL = new LoginPageViewModel(this);
                    viewL.DataContext = viewModelL;
                    this.OutputView.Content = viewL;
                    break;
                case View_number.Register:
                    RegisterPage viewR = new RegisterPage();
                    RegisterPageViewModel viewModelR = new RegisterPageViewModel(this);
                    viewR.DataContext = viewModelR;
                    this.OutputView.Content = viewR;
                    break;
                case View_number.UserInterface:
                    break;
                default:
                    break;
            }
        }
    }
}

/*
 * private void TextBox_login_GotFocus(object sender, RoutedEventArgs e)
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
*/