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

namespace AppKFC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Pages.mainPage pageMainPage;
        public static Pages.userPage pageUserPage;
        public static Pages.registrationPage pageRegistrationPage;
        public static Pages.registrationPageClient pageRegistrationPageClient;
        public static Pages.firstPage pageFirstPage;
        public static Pages.loginPageClient pageLoginPageClient;
        public static Pages.registrationEmployee pageRegistrationEmployee;
        public static Pages.AdministrationPage pageAdministrationPage;
        public static string Name;
        public static MainWindow Instance; 
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            pageRegistrationEmployee = new Pages.registrationEmployee();
            pageMainPage = new Pages.mainPage();
            pageUserPage = new Pages.userPage();
            pageRegistrationPage = new Pages.registrationPage();
            pageRegistrationPageClient = new Pages.registrationPageClient();
            pageFirstPage = new Pages.firstPage();
            pageLoginPageClient = new Pages.loginPageClient();
            pageAdministrationPage = new Pages.AdministrationPage();
            //MainFrame.Navigate(pageMainPage);
            //MainFrame.Navigate(pageUserPage);
            //MainFrame.Navigate(pageRegistrationPage);
            MainFrame.Navigate(pageFirstPage);
        }

      
    }
}
