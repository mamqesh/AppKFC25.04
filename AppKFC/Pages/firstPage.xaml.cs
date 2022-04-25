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
using AppKFC.Database;

namespace AppKFC.Pages
{
    /// <summary>
    /// Логика взаимодействия для firstPage.xaml
    /// </summary>
    public partial class firstPage : Page
    {
        public firstPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.pageLoginPageClient);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            danilEntities connection = new danilEntities();
            var countAccount = connection.Employee.Where(a => a.Role == 3).Count();
            if (countAccount == 0)
            {
                NavigationService.Navigate(MainWindow.pageRegistrationEmployee);

            }
            else
            {
                NavigationService.Navigate(MainWindow.pageRegistrationPage);

            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Close();
        }   
        
    }
}
