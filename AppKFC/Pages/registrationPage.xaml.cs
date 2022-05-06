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
using System.IO;

namespace AppKFC.Pages
{
    /// <summary>
    /// Логика взаимодействия для registrationPage.xaml
    /// </summary>
    public partial class registrationPage : Page
    {
        //FastFoodEntities connection = new FastFoodEntities();
        danilEntities connection = new danilEntities();
        private string code = "";
        private double blockTime = 0;
        private int blockInterval = 60000;
        private int failCounter = 0;
        private const string filename = "data.lock";
        public registrationPage()
        {
            InitializeComponent();
            //CountRole();
            textBoxLogin.ToolTip = "Введите ваш логин";
            passwordBoxPassword.ToolTip = "Введите ваш пароль";
            GenerateCode();
#if DEBUG
            textBoxLogin.Text = "9632551263";
            passwordBoxPassword.Password = "zxc(1000-7)";
            textBoxCode.Text = code;
#endif
        }

        void GenerateCode()//СОЗДАНИЕ КАПЧИ
        {
            code = "";
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789!@#$%^&*";//ВСЕ СИМВОЛЫ
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 6; i++)
            {
                int a = random.Next(0, 10);
                code += chars.Substring(a, 1);
            }
            labelCode.Content = code;
        }
        void WriteBlocking()
        {

        }
        void OnLogin()
        {
            if (failCounter>=3)
            {

                MessageBox.Show("Вход заблокирован на одну минуту");
                failCounter = 0;
                return;
            }
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
                        if (textBoxCode.Text==code)
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
                        else
                        {
                            textBoxLogin.Clear();
                            passwordBoxPassword.Clear();
                            MessageBox.Show("Неверный код проверки");
                            GenerateCode();
                            failCounter++;
                            return;
                        }
                    }
                }
            }
            if (tryExit==0)
            {
                textBoxLogin.Clear();
                passwordBoxPassword.Clear();
                MessageBox.Show("Данная учетная запись не найдена");
                GenerateCode();
                failCounter++;
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
