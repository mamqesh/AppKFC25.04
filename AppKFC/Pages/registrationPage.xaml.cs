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
    /// Логика взаимодействия для registrationPage.xaml
    /// </summary>
    public partial class registrationPage : Page
    {
        //FastFoodEntities connection = new FastFoodEntities();
        danilEntities connection = new danilEntities();

        public registrationPage()
        {
            InitializeComponent();
            //CountRole();
            textBoxLogin.ToolTip = "Введите ваш логин";
            passwordBoxPassword.ToolTip = "Введите ваш пароль";
        }

        //void CountRole()
        //{
        //    var countAccount = connection.Employee.Where(e => e.Role == 3).Count();
        //    if (countAccount == 0)
        //    {
        //        NavigationService.Navigate(MainWindow.pageRegistrationEmployee);
        //    }
        //}


        private void Button_Click(object sender, RoutedEventArgs e) //ВХОД В УЧЕТКУ
        {
            int tryExit=0;
            string login = textBoxLogin.Text.Trim();
            string password = passwordBoxPassword.Password.Trim();
            var employees = connection.Employee.ToList();
            foreach (var _employees in employees)
            {
                if (login == _employees.Phone)
                {
                    if (password == _employees.Password)
                    {
                        if (_employees.Role == 2)
                        {
                            NavigationService.Navigate(MainWindow.pageMainPage);
                            tryExit++;
                        }
                        if (_employees.Role == 3)
                        {
                            NavigationService.Navigate(MainWindow.pageRegistrationEmployee);
                            tryExit++;
                        }
                    }
                }
            }
            if (tryExit==0)
            {
                textBoxLogin.Clear();
                passwordBoxPassword.Clear();
                MessageBox.Show("Данная учетная запись не найдена");
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            textBoxLogin.Clear();
            passwordBoxPassword.Clear();
            NavigationService.Navigate(MainWindow.pageFirstPage);
        }
    }
}
