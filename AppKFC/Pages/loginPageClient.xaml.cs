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
    /// Логика взаимодействия для loginPageClient.xaml
    /// </summary>
    public partial class loginPageClient : Page
    {
        public loginPageClient()
        {
            InitializeComponent();
        }
        danilEntities connection = new danilEntities();
        //FastFoodEntities connection = new FastFoodEntities();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tryExit = 0;
            string login = textBoxLogin.Text.Trim();
            var clients = connection.Client.ToList();
            foreach (var _clients in clients)
            {
                if (login == _clients.Phone)
                {
                        MainWindow.Name = login;
                        NavigationService.Navigate(MainWindow.pageUserPage);
                        textBoxLogin.Clear();
                        tryExit++;

                }
            }
            if (tryExit == 0)
            {
                textBoxLogin.Clear();
                MessageBox.Show("Данная учетная запись не найдена");
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.pageRegistrationPageClient);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            textBoxCode.Clear();
            textBoxLogin.Clear();
            NavigationService.Navigate(MainWindow.pageFirstPage);
        }

        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
