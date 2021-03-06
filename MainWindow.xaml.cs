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
        void LoadClerksPage(Clerk_view_number typeView, User customer);
        void LoadClientPage(Client_Page_Load typeView, User customer);
        void LoadMasterPage(Master_Page_Load typeView, User customer);
        void LoadModeratorPage(Moderator_Pages typeView, User customer);
        void LoadAdminPage(Admin_Pages typeView, User customer);
        void LoadWiew(View_number typeView);
        void ShowMessageBox(string message);
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

    public enum Client_Page_Load
    {
        Main,
        Profile,
        EnrollCar,
        ViewCarStatus
    }
    public enum Master_Page_Load
    {
        Main,
        Profile,
        ChangeEnroll,
        ViewEnrolls
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

    public enum Moderator_Pages
    {
        Main,
        Profile,
        AcceptAccount,
        DeleteRows
    }
    public enum Admin_Pages
    {
        Main,
        DeleteRows,
        DeleteUsers,
        LogClient,
        LogClerk,
        LogMaster,
        LogModer
    }
    /*
       public enum TypeOfAccount
        {
            Admin, 0
            Master, 1
            Moderator, 2
            Clerk, 3
            Client 4
        }
    */
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
        public void LoadClerksPage(Clerk_view_number typeView, User customer)
        {
            switch (typeView)
            {
                case Clerk_view_number.Main:
                    ClerkStartPage viewM = new ClerkStartPage();
                    ClerkStartPageViewModel viewModelM = new ClerkStartPageViewModel(this, customer);
                    viewM.DataContext = viewModelM;
                    this.OutputView.Content = viewM;
                    break;
                case Clerk_view_number.Profile:
                    ProfilePage viewP = new ProfilePage();
                    ProfilePageViewModel viewModelP = new ProfilePageViewModel(this, customer);
                    viewP.DataContext = viewModelP;
                    this.OutputView.Content = viewP;
                    break;
                case Clerk_view_number.Receive_Data:
                    ClerkPage viewRD = new ClerkPage();
                    ClerkPageViewModel viewModelRD = new ClerkPageViewModel(this, customer);
                    viewRD.DataContext = viewModelRD;
                    this.OutputView.Content = viewRD;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Загрузка страниц клиента
        /// </summary>
        /// <param name="typeView"></param>
        public void LoadClientPage(Client_Page_Load typeView, User customer)
        {
            switch (typeView)
            {
                case Client_Page_Load.Main:
                    ClientStartPage viewC = new ClientStartPage();
                    ClientStartPageViewModel viewModelC = new ClientStartPageViewModel(this, customer);
                    viewC.DataContext = viewModelC;
                    this.OutputView.Content = viewC;
                    break;
                case Client_Page_Load.Profile:
                    ProfilePage viewP = new ProfilePage();
                    ProfilePageViewModel viewModelP = new ProfilePageViewModel(this, customer);
                    viewP.DataContext = viewModelP;
                    this.OutputView.Content = viewP;
                    break;
                case Client_Page_Load.EnrollCar:
                    EnrollCarPage viewE = new EnrollCarPage(customer);
                    EnrollCarPageViewModel viewModelE = new EnrollCarPageViewModel(this, customer);
                    viewE.DataContext = viewModelE;
                    this.OutputView.Content = viewE;
                    break;
                case Client_Page_Load.ViewCarStatus:
                    StatusOfCarPage viewS = new StatusOfCarPage();
                    StatusOfCarPageViewModel viewModelS = new StatusOfCarPageViewModel(this, customer);
                    viewS.DataContext = viewModelS;
                    this.OutputView.Content = viewS;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Загрузка страниц мастера автосервиса
        /// </summary>
        /// <param name="typeView"></param>
        /// <param name="customer"></param>
        public void LoadMasterPage(Master_Page_Load typeView, User customer)
        {
            switch (typeView)
            {
                case Master_Page_Load.Main:
                    MasterMainPage viewM = new MasterMainPage();
                    MasterMainPageViewModel viewModelM = new MasterMainPageViewModel(this, customer);
                    viewM.DataContext = viewModelM;
                    this.OutputView.Content = viewM;
                    break;
                case Master_Page_Load.Profile:
                    ProfilePage viewP = new ProfilePage();
                    ProfilePageViewModel viewModelP = new ProfilePageViewModel(this, customer);
                    viewP.DataContext = viewModelP;
                    this.OutputView.Content = viewP;
                    break;
                case Master_Page_Load.ChangeEnroll:
                    ModifyEnrollPage viewME = new ModifyEnrollPage();
                    ModifyEnrollPageViewModel viewModelME = new ModifyEnrollPageViewModel(this, customer);
                    viewME.DataContext = viewModelME;
                    this.OutputView.Content = viewME;
                    break;
                case Master_Page_Load.ViewEnrolls:
                    ViewEnrollsPage viewE = new ViewEnrollsPage();
                    ViewEnrollsPageViewModel viewModelE = new ViewEnrollsPageViewModel(this, customer);
                    viewE.DataContext = viewModelE;
                    this.OutputView.Content = viewE;
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
                    BackGroudAdmin();
                    break;
                case Background_set.Moderator:
                    BackGroundModerator();
                    break;
                case Background_set.Client:
                    BackGroundClient();
                    break;
                case Background_set.Master:
                    BackGroundMaster();
                    break;
                default:
                    break;
            }
        }
        public void LoadModeratorPage(Moderator_Pages typeView, User customer)
        {
            switch (typeView)
            {
                case Moderator_Pages.Main:
                    ModeratorMainPage viewM = new ModeratorMainPage();
                    ModeratorMainPageViewModel viewModelM = new ModeratorMainPageViewModel(this, customer);
                    viewM.DataContext = viewModelM;
                    this.OutputView.Content = viewM;
                    break;
                case Moderator_Pages.Profile:
                    ProfilePage viewP = new ProfilePage();
                    ProfilePageViewModel viewModelP = new ProfilePageViewModel(this, customer);
                    viewP.DataContext = viewModelP;
                    this.OutputView.Content = viewP;
                    break;
                case Moderator_Pages.AcceptAccount:
                    ModerAcceptAccsPage viewA = new ModerAcceptAccsPage();
                    ModerAcceptAccsPageViewModel viewModelA = new ModerAcceptAccsPageViewModel(this, customer);
                    viewA.DataContext = viewModelA;
                    this.OutputView.Content = viewA;
                    break;
                case Moderator_Pages.DeleteRows:
                    ModerDeletePage viewD = new ModerDeletePage();
                    ModerDeletePageViewModel viewModelD = new ModerDeletePageViewModel(this, customer);
                    viewD.DataContext = viewModelD;
                    this.OutputView.Content = viewD;
                    break;
                default:
                    break;
            }
        }
        public void LoadAdminPage(Admin_Pages typeView, User customer)
        {
            switch (typeView)
            {
                case Admin_Pages.Main:
                    AdminMainPage viewA = new AdminMainPage();
                    AdminMainPageViewModel viewModelA = new AdminMainPageViewModel(this, customer);
                    viewA.DataContext = viewModelA;
                    this.OutputView.Content = viewA;
                    break;
                case Admin_Pages.DeleteRows:
                    AdminDeleteRowPage viewD = new AdminDeleteRowPage();
                    AdminDeleteRowPageViewModel viewModelD = new AdminDeleteRowPageViewModel(this, customer);
                    viewD.DataContext = viewModelD;
                    this.OutputView.Content = viewD;
                    break;
                case Admin_Pages.DeleteUsers:
                    AdminDeleteUserPage viewU = new AdminDeleteUserPage();
                    AdminDeleteUserPageViewModel viewModelU = new AdminDeleteUserPageViewModel(this, customer);
                    viewU.DataContext = viewModelU;
                    this.OutputView.Content = viewU;
                    break;
                case Admin_Pages.LogClient:
                    LoadClientPage(Client_Page_Load.Main, customer);
                    break;
                case Admin_Pages.LogClerk:
                    LoadClerksPage(Clerk_view_number.Main, customer);
                    break;
                case Admin_Pages.LogMaster:
                    LoadMasterPage(Master_Page_Load.Main, customer);
                    break;
                case Admin_Pages.LogModer:
                    LoadModeratorPage(Moderator_Pages.Main, customer);
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
            gradient.GradientStops.Add(SetPartGradient(84, 106, 123, 1.0));
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
        private void BackGroundClient()
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(SetPartGradient(0, 31, 84, 1.0));
            gradient.GradientStops.Add(SetPartGradient(3, 64, 120, 0.5));
            gradient.GradientStops.Add(SetPartGradient(18, 130, 162, 0.0));
            Background = gradient;
        }
        private void BackGroundMaster()
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(SetPartGradient(8, 61, 119, 0.0));
            gradient.GradientStops.Add(SetPartGradient(224, 221, 94, 0.5));
            gradient.GradientStops.Add(SetPartGradient(247, 135, 100, 1.0));
            Background = gradient;
        }
        private void BackGroundModerator()
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(SetPartGradient(12, 98, 145, 1));
            Background = gradient;
        }
        private void BackGroudAdmin()
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(SetPartGradient(0, 0, 34, 0));
            gradient.GradientStops.Add(SetPartGradient(12, 98, 145, 0.7));
            gradient.GradientStops.Add(SetPartGradient(231, 230, 247, 1));
            Background = gradient;
        }
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
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