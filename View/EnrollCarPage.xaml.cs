using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
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

namespace Cursovoy_project.View
{
    /// <summary>
    /// Логика взаимодействия для EnrollCarPage.xaml
    /// </summary>
    public partial class EnrollCarPage : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public EnrollCarPage(User _Customer)
        {
            InitializeComponent();
            Customer = _Customer;
        }
        private User Customer;
        
        AutoServiceContext db;

        
        /*
         * <!--ItemsSource="{Binding SelectedItem}" -->
        private void CreateListSpisok()
        {
            TimeSpan time = new TimeSpan(8, 0, 0);
            TimeSpan add_time = new TimeSpan(1, 0, 0);
            List<TimeSpan> list = new List<TimeSpan>() {time};
            for (int i = 0; i <= 9; i++)
            {
                time.Add(add_time);
                list.Add(time);
            }
        }
        */
    }
}
