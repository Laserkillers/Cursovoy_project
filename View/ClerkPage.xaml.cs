using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

namespace Cursovoy_project.View
{
    /// <summary>
    /// Логика взаимодействия для ClerkPage.xaml
    /// </summary>
    public partial class ClerkPage : UserControl
    {
        AutoServiceContext db;

        private DateTime Datefirst;
        private DateTime Datesecond;
        public ClerkPage()
        {
            InitializeComponent();
        }

        private void DateInBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Datefirst = (DateTime)DateInBox.SelectedDate;
        }

        private void DateOutBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Datesecond = (DateTime)DateOutBox.SelectedDate;
            Datesecond = new DateTime(Datesecond.Year, Datesecond.Month, Datesecond.Day, 23, 59, 59);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db = new AutoServiceContext();
            db.AutoServices.Load();
            autoserviceInfoGrid.ItemsSource = db.AutoServices.Local.ToBindingList().Where(p => (p.IssureTime > Datefirst) && (p.IssureTime < Datesecond));
            decimal? sum = db.AutoServices.Local
                .Where(p => (p.IssureTime > Datefirst) && (p.IssureTime < Datesecond))
                .Sum(p => p.Cost);
            SumOfServices.Text = " " + sum.ToString() + " р.";
            db.Dispose();
        }
    }

}
