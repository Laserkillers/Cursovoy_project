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
        void ClosePage();
        void Change_Background(Background_set background);
        void LoadClerksPage(Clerk_view_number typeView);
        void LoadWiew(View_number typeView);
    }
    /// <summary>
    /// Стартовая навигация
    /// </summary>
    public enum View_number
    {
        Main,
        Login,
        Register 
    }
    /// <summary>
    /// Навигация для Бухгалтера
    /// </summary>
    public enum Clerk_view_number
    {
        Main,
        Profile,
        Receive_Data
    }
    public enum Background_set
    {
        Main,
        Clerk,
        Admin,
        Moderator,
        Client,
        Master
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

        public void ClosePage()
        {this.Close();}

        /// <summary>
        /// Загрузка основных страниц
        /// </summary>
        /// <param name="typeView"></param>
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
                default:
                    break;
            }
        }
        /// <summary>
        /// Загрузка страниц Бухгалтера
        /// </summary>
        /// <param name="typeView"></param>
        public void LoadClerksPage(Clerk_view_number typeView)
        {
            switch (typeView)
            {
                case Clerk_view_number.Main:

                    break;
                case Clerk_view_number.Profile:
                    break;
                case Clerk_view_number.Receive_Data:
                    ClerkPage viewRD = new ClerkPage();
                    ClerkPageViewModel viewModelRD = new ClerkPageViewModel(this);
                    viewRD.DataContext = viewModelRD;
                    this.OutputView.Content = viewRD;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Установка заднего фона для каждой страницы
        /// </summary>
        /// <param name="background"></param>
        public void Change_Background(Background_set background)
        {
            switch (background)
            {
                case Background_set.Main:
                    BackGroundMain();
                    break;
                case Background_set.Clerk:
                    BackGroundClerk();
                    break;
                case Background_set.Admin:
                    break;
                case Background_set.Moderator:
                    break;
                case Background_set.Client:
                    break;
                case Background_set.Master:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Генерация элемента типа GradientStop для кситочки LinearGradientBrush 
        /// </summary>
        /// <param name="red">Красный</param>
        /// <param name="green">Зеленый</param>
        /// <param name="blue">Синий</param>
        /// <param name="offset">Что-то</param>
        /// <returns></returns>
        private GradientStop SetPartGradient(int red, int green, int blue, double offset)
        {
            GradientStop colour = new GradientStop();
            colour.Color = Color.FromRgb((byte)red, (byte)green, (byte)blue);
            colour.Offset = offset;
            return colour;
        }
        private void BackGroundClerk()
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(SetPartGradient(60, 172, 220, 1.0));
            this.Background = gradient;
        }
        private void BackGroundMain()
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(SetPartGradient(10, 198, 114, 1.0));
            gradient.GradientStops.Add(SetPartGradient(6, 77, 186, 0.5));
            gradient.GradientStops.Add(SetPartGradient(226, 219, 5, 0.0));
            Background = gradient;
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