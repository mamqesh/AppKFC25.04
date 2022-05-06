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
    /// Логика взаимодействия для AdministrationPage.xaml
    /// </summary>
    public partial class AdministrationPage : Page
    {
        public Database.Employee employee { get; set; }
        public List <Database.Employee> employees { get; set; } 
        public Database.Client client { get; set; }
        public List <Database.Client> clients { get; set; }
        public Database.Role role { get; set; }
        public List <Database.Role> roles { get; set; }
        
        danilEntities connection = new danilEntities();
        
        public AdministrationPage()
        {
            InitializeComponent();
            LoadRole();
            employees = connection.Employee.ToList();
            listBoxUsers.ItemsSource = employees;
            clients = connection.Client.ToList();
            listBoxClients.ItemsSource = clients;
            DataContext = this;
        }
        private void LoadRole()
        {
            var role = connection.Role.ToList();
            foreach (var _role in role)
            {
                comboBoxLoadRole.Items.Add(_role);
            }
        }
        private void ClearTextBox()
        {
            textBoxPhone.Clear();
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxPatronymic.Clear();
            textBoxPassword.Clear();
            textBoxNameUser.Clear();
            textBoxPhoneUser.Clear();
            textBoxAddressUser.Clear();
            listBoxClients.SelectedIndex = -1;
            listBoxUsers.SelectedIndex = -1;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//НАЗАД
        {
            ClearTextBox();
            NavigationService.GoBack();
        }
        void LoadUsersEmployee()
        {
            textBoxName.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            textBoxSurname.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            textBoxPatronymic.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            textBoxPhone.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            textBoxPassword.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            var roles = comboBoxLoadRole.GetBindingExpression(ComboBox.SelectedItemProperty);
            if (roles != null)
            {
                roles.UpdateTarget();
            }
        }
        void LoadUsers()
        {
            textBoxNameUser.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            textBoxPhoneUser.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            textBoxAddressUser.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }
        private void Button_Click(object sender, RoutedEventArgs e) //РЕДАКТИРОВАТЬ ДАННЫЕ У СОТРУДНИКОВ
        {
            int result = connection.SaveChanges();
            if (result == 1)
            {
                MessageBox.Show("Данные успешно обновлены!");
            }
            else
            {
                MessageBox.Show("Ошибка обновления данных!");
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e) //РЕДАКТИРОВАТЬ ДАННЫЕ У КЛИЕНТОВ 
        {
            int result = connection.SaveChanges();
            if (result == 1)
            {
                MessageBox.Show("Данные успешно обновлены!");
            }
            else
            {
                MessageBox.Show("Ошибка обновления данных!");
            }
        }
        private void listBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e) //СОТРУДНИКИ
        {

            employee = listBoxUsers.SelectedItem as Database.Employee;
            LoadUsersEmployee();
        }
        private void listBoxClients_SelectionChanged(object sender, SelectionChangedEventArgs e) //КЛИЕНТЫ
        {
            client = listBoxClients.SelectedItem as Database.Client;
            LoadUsers();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) //УДАЛИТЬ СОТРУДНИКА
        {     
            
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //УДАЛИТЬ ПОЛЬЗОВАТЕЛЯ
        {

        }

       
    }
}
