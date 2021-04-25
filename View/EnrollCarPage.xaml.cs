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
    public interface EnrollCarPageBehind
    {
        void ShowMessageBox(string message);
    }
    /// <summary>
    /// Логика взаимодействия для EnrollCarPage.xaml
    /// </summary>
    public partial class EnrollCarPage : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public EnrollCarPage(User _Customer)
        {
            InitializeComponent();
        }

    }
}
